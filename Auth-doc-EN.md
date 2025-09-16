# BlazorTool Authorization and UserState Control Scheme

This document describes how the authorization and user state (`UserState`) management system is implemented in the BlazorTool application, including the interaction between the client and server parts, as well as the external API.

## 1. Component Overview

*   **`UserState` (BlazorTool.Client/Services/UserState.cs)**: A client-side service that stores the current user's data (name, `PersonID`, external API token, rights matrix) and manages its saving/loading from the browser's `localStorage`. It contains an `InitializationTask` for asynchronous data loading.
*   **`AuthHeaderHandler` (BlazorTool.Client/Services/AuthHeaderHandler.cs)**: A client-side `DelegatingHandler` that intercepts outgoing HTTP requests from the Blazor client to the Blazor server. It adds a custom `X-Person-ID` header to requests, using the `PersonID` from `UserState`. **Important**: It does NOT add the external API token to the `Authorization` header for requests to the Blazor server.
*   **`ApiServiceClient` (BlazorTool.Client/Services/ApiServiceClient.cs)**: A client-side service for making HTTP requests to the Blazor server. It uses an `HttpClient` configured with `AuthHeaderHandler`.
*   **`IdentityController` (BlazorTool/Controllers/IdentityController.cs)**: A controller on the Blazor server responsible for authenticating the user with the external API. After successful authentication, it caches the received `IdentityData` (including the external API token) in the server's `IMemoryCache`.
*   **`ServerAuthTokenService` (BlazorTool/Services/ServerAuthTokenService.cs)**: A server-side service that retrieves the external API token from the server's `IMemoryCache`. It uses `IHttpContextAccessor` to access the incoming request's headers and obtain the `PersonID` from the `X-Person-ID` header.
*   **`ServerAuthHeaderHandler` (BlazorTool/Services/ServerAuthHeaderHandler.cs)**: A server-side `DelegatingHandler` that intercepts outgoing HTTP requests from the Blazor server to the external API. It uses `ServerAuthTokenService` to obtain the token and adds it to the `Authorization: Bearer` header.
*   **`IMemoryCache` (Microsoft.Extensions.Caching.Memory)**: An in-built ASP.NET Core in-memory caching mechanism used on the Blazor server for temporary storage of user's `IdentityData`.

## 2. Login Flow

1.  **User Enters Credentials**: On the `Login.razor` page, the user enters their username and password.
2.  **Authentication Request (Client -> Blazor Server)**:
    *   `Login.razor` calls the `LoginAsync` method in `ApiServiceClient`, passing the `LoginRequest` (username, password).
    *   `ApiServiceClient` sends a POST request to `api/v1/identity/loginpassword` on the Blazor server.
    *   **`AuthHeaderHandler` (client)**: At this stage, `AuthHeaderHandler` might be invoked, but `UserState.PersonID` will be `null` as the user is not yet authenticated. The `X-Person-ID` header will not be added.
3.  **Request Processing on Blazor Server (`IdentityController`)**:
    *   `IdentityController` receives the `LoginRequest`.
    *   It creates an `HttpClient` (via `IHttpClientFactory`) to interact with the external API. This `HttpClient` is configured with `ExternalApiBasicAuthClient` for basic authentication with the external API.
    *   `IdentityController` sends an authentication request (typically a POST request with credentials) to the external API.
    *   **External API** authenticates the user and returns `IdentityData` containing the `PersonID`, external API token, and other data.
    *   **Caching `IdentityData`**: If authentication with the external API is successful, `IdentityController` caches the received `IdentityData` in the server's `IMemoryCache` using a key like `IdentityData_{PersonID}`. The cache lifetime is typically set to 1 hour.
    *   `IdentityController` returns an `ApiResponse<IdentityData>` to the client.
4.  **Response Handling on Client (`Login.razor`)**:
    *   `Login.razor` receives the response from `ApiServiceClient`.
    *   If authentication is successful, `Login.razor` calls `UserState.SetIdentityDataAsync()`, passing the received `IdentityData`.
    *   `UserState` saves the `IdentityData` (including `PersonID` and the external API token) to the browser's `localStorage`.
    *   `Login.razor` redirects the user to the home page or another protected page.

## 3. UserState Control on Client

1.  **`UserState` Initialization**:
    *   In `BlazorTool.Client/Program.cs`, `UserState` is registered as a `Scoped` service.
    *   In the `UserState` constructor, `LoadIdentityDataAsync()` is initiated, which asynchronously loads `IdentityData` from `localStorage`. The result of this asynchronous operation is stored in `UserState.InitializationTask`.
2.  **Waiting for Initialization on Pages**:
    *   On protected Blazor pages (e.g., `SchedulerPage.razor`, `OrdersPage.razor`, `Settings.razor`), before making any API calls that depend on `UserState`, `await UserState.InitializationTask;` is added. This ensures that `UserState` is fully loaded and `PersonID` is available.
    *   These pages also check `UserState.IsAuthenticated`, and if the user is not authenticated, a redirection to the login page occurs.
3.  **`AuthHeaderHandler` and `X-Person-ID`**:
    *   `AuthHeaderHandler` also awaits `await _userState.InitializationTask;` in its `SendAsync` method.
    *   After `UserState` initialization, if `_userState.PersonID.HasValue` is true, `AuthHeaderHandler` adds the `X-Person-ID` header to the outgoing HTTP request. This header contains the current user's `PersonID`.
    *   **Important**: The client-side `AuthHeaderHandler` does NOT add the external API token to the `Authorization` header. This is intentionally done for enhanced security and separation of concerns.

## 4. External API Request Authorization (Server-side External API Calls)

This process occurs when the Blazor server needs to make a request to the external API on behalf of an authenticated user.

1.  **Client Request to Blazor Server**:
    *   The client (e.g., `ApiServiceClient` or any Razor page) sends a request to the Blazor server (e.g., `DeviceController.GetList` or `WoController.GetList`).
    *   The client-side `AuthHeaderHandler` adds the `X-Person-ID` header to this request.
2.  **Request Processing on Blazor Server**:
    *   A controller on the Blazor server (e.g., `DeviceController` or `WoController`) receives the request.
    *   The controller uses `IHttpClientFactory` to create an `HttpClient` named `"ExternalApiBearerAuthClient"`. This client is configured to use `ServerAuthHeaderHandler`.
    *   The controller executes the request to the external API using this `HttpClient`.
3.  **`ServerAuthHeaderHandler` (server)**:
    *   `ServerAuthHeaderHandler` intercepts the outgoing request from the Blazor server to the external API.
    *   It calls `ServerAuthTokenService.GetTokenAsync()`.
4.  **`ServerAuthTokenService.GetTokenAsync()` (server)**:
    *   This method uses `IHttpContextAccessor` to access the current `HttpContext` and extracts the `PersonID` from the `X-Person-ID` header of the incoming request from the client.
    *   It then attempts to retrieve `IdentityData` from the server's `IMemoryCache` using the key `IdentityData_{PersonID}`.
    *   If `IdentityData` is found in the cache and contains a valid token, that token is returned.
    *   If the token is not found in the cache (e.g., expired or cache cleared), the method returns `null`.
5.  **Adding Token to Request (server)**:
    *   `ServerAuthHeaderHandler` receives the token from `ServerAuthTokenService`.
    *   If the token is valid, `ServerAuthHeaderHandler` adds it to the `Authorization: Bearer [token]` header of the outgoing request to the external API.
    *   The request is sent to the external API.
6.  **External API** processes the request using the provided Bearer token for authorization.

## 5. Logout Flow

1.  **User Initiates Logout**: The user clicks a "Logout" button or similar control.
2.  **`UserState.LogoutAsync()` Call**:
    *   This method clears the `UserName`, `Password`, `Token`, `PersonID`, and `RightMatrix` properties in `UserState`.
    *   It also removes `identityData` from the browser's `localStorage`.
3.  **Redirection**: After logout, the user is typically redirected to the login page.
