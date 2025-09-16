# BlazorTool API Endpoints Overview

This document provides an overview of the API endpoints used in the BlazorTool application, detailing the HTTP method, the responsible server-side controller, and the authorization type required for calls to the external API.

## Client-Side `ApiServiceClient` Endpoints and Server-Side Controller Mappings

The `ApiServiceClient` on the client-side (`BlazorTool.Client/Services/ApiServiceClient.cs`) makes calls to the BlazorTool server's internal API. The BlazorTool server then, for certain endpoints, acts as a proxy to an external API, handling the necessary authorization.

Here's a breakdown of each endpoint:

---

### 1. `api/v1/identity/loginpassword`

*   **HTTP Method:** `POST`
*   **Server-Side Controller:** `BlazorTool/Controllers/IdentityController.cs`
*   **Authorization Type (for External API calls):** Basic Auth (uses `ExternalApiBasicAuthClient`). **No token required from client for this specific server-to-external API call.**
*   **Note:** This endpoint is used for the initial authentication process, where the client sends username and password to the BlazorTool server. The BlazorTool server then uses Basic Authentication to obtain a token from the external API, which is subsequently sent back to the client.

---

### 2. `api/v1/other/getuserslist`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/PersonController.cs`
*   **Authorization Type (for External API calls):** Basic Auth (uses `ExternalApiBasicAuthClient`). **No token required from client for this specific server-to-external API call.**
*   **Note:** This endpoint is used to retrieve a list of users, likely for display on the login page or for user selection.

---

### 3. `api/v1/activity/getlist`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/ActivityController.cs`
*   **Authorization Type (for External API calls):** Bearer Token (uses `ExternalApiBearerAuthClient`). **Token required.**
*   **Note:** The BlazorTool server uses the Bearer token (obtained from the client's request) to authorize its call to the external API for activity data.

---

### 4. `api/v1/device/getlist`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/DeviceController.cs`
*   **Authorization Type (for External API calls):** Bearer Token (uses `ExternalApiBearerAuthClient`). **Token required.**
*   **Note:** The BlazorTool server uses the Bearer token (obtained from the client's request) to authorize its call to the external API for device data.

---

### 5. `api/v1/wo/getlist`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/WoController.cs`
*   **Authorization Type (for External API calls):** Bearer Token (uses `ExternalApiBearerAuthClient`). **Token required.**
*   **Note:** The BlazorTool server uses the Bearer token (obtained from the client's request) to authorize its call to the external API for work order list data.

---

### 6. `api/v1/wo/getdict`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/WoController.cs`
*   **Authorization Type (for External API calls):** Bearer Token (uses `ExternalApiBearerAuthClient`). **Token required.**
*   **Note:** The BlazorTool server uses the Bearer token (obtained from the client's request) to authorize its call to the external API for work order dictionary data.

---

### 7. `api/v1/wo/getfiles`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/WoController.cs`
*   **Authorization Type (for External API calls):** Bearer Token (uses `ExternalApiBearerAuthClient`). **Token required.**
*   **Note:** The BlazorTool server uses the Bearer token (obtained from the client's request) to authorize its call to the external API for work order files data.

---

### 8. `api/v1/wo/getfile`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/WoController.cs`
*   **Authorization Type (for External API calls):** Bearer Token (uses `ExternalApiBearerAuthClient`). **Token required.**
*   **Note:** The BlazorTool server uses the Bearer token (obtained from the client's request) to authorize its call to the external API for a single work order file data.

---

### 9. `api/v1/settings/get`

*   **HTTP Method:** `GET`
*   **Server-Side Controller:** `BlazorTool/Controllers/SettingsController.cs`
*   **Authorization Type (for External API calls):** Not Applicable (this controller reads local settings, does not call an external API). **No token required.**

---

### 10. `api/v1/settings/set`

*   **HTTP Method:** `POST`
*   **Server-Side Controller:** `BlazorTool/Controllers/SettingsController.cs`
*   **Authorization Type (for External API calls):** Not Applicable (this controller saves local settings, does not call an external API). **No token required.**

---

### 11. `api/v1/settings/check`

*   **HTTP Method:** `POST`
*   **Server-Side Controller:** `BlazorTool/Controllers/SettingsController.cs`
*   **Authorization Type (for External API calls):** Not Applicable (this controller checks external API accessibility, but does not use a token for authorization). **No token required.**

---

**Summary of Changes and Verification:**

The recent modifications ensure that server-side API calls to the external API correctly utilize either Basic Authentication (for initial token acquisition) or the client-provided Bearer token (for subsequent authorized requests). The client-side `ApiServiceClient` is now correctly configured to communicate with the BlazorTool server's internal API.

To verify these changes, please:
1.  Run the BlazorTool application.
2.  Log in via `Login.razor`.
3.  Navigate to pages that trigger server-side calls to the external API (e.g., Scheduler page, which fetches work orders and device data).
4.  Confirm that data loads correctly and no authorization errors (like HTTP 500 with underlying authorization failures) occur.
