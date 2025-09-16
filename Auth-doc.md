# Схема работы авторизации и контроля UserState в BlazorTool

Этот документ описывает, как реализована система авторизации и управления состоянием пользователя (`UserState`) в приложении BlazorTool, включая взаимодействие между клиентской и серверной частями, а также внешним API.

## 1. Обзор компонентов

*   **`UserState` (BlazorTool.Client/Services/UserState.cs)**: Клиентский сервис, который хранит данные текущего пользователя (имя, `PersonID`, токен внешнего API, матрица прав) и управляет их сохранением/загрузкой из `localStorage` браузера. Содержит `InitializationTask` для асинхронной загрузки данных.
*   **`AuthHeaderHandler` (BlazorTool.Client/Services/AuthHeaderHandler.cs)**: Клиентский `DelegatingHandler`, который перехватывает исходящие HTTP-запросы от клиента Blazor к серверу Blazor. Он добавляет пользовательский заголовок `X-Person-ID` к запросам, используя `PersonID` из `UserState`. **Важно**: он НЕ добавляет токен внешнего API в заголовок `Authorization` для запросов к серверу Blazor.
*   **`ApiServiceClient` (BlazorTool.Client/Services/ApiServiceClient.cs)**: Клиентский сервис для выполнения HTTP-запросов к серверу Blazor. Он использует `HttpClient`, который настроен с `AuthHeaderHandler`.
*   **`IdentityController` (BlazorTool/Controllers/IdentityController.cs)**: Контроллер на сервере Blazor, отвечающий за аутентификацию пользователя с внешним API. После успешной аутентификации он кэширует полученные `IdentityData` (включая токен внешнего API) в `IMemoryCache` сервера.
*   **`ServerAuthTokenService` (BlazorTool/Services/ServerAuthTokenService.cs)**: Серверный сервис, который извлекает токен внешнего API из `IMemoryCache` сервера. Он использует `IHttpContextAccessor` для доступа к заголовкам входящего запроса и получения `PersonID` из заголовка `X-Person-ID`.
*   **`ServerAuthHeaderHandler` (BlazorTool/Services/ServerAuthHeaderHandler.cs)**: Серверный `DelegatingHandler`, который перехватывает исходящие HTTP-запросы от сервера Blazor к внешнему API. Он использует `ServerAuthTokenService` для получения токена и добавляет его в заголовок `Authorization: Bearer`.
*   **`IMemoryCache` (Microsoft.Extensions.Caching.Memory)**: Встроенный в ASP.NET Core механизм кэширования в памяти, используемый на сервере Blazor для временного хранения `IdentityData` пользователя.

## 2. Процесс входа в систему (Login Flow)

1.  **Пользователь вводит учетные данные**: На странице `Login.razor` пользователь вводит имя пользователя и пароль.
2.  **Отправка запроса на аутентификацию (Клиент -> Сервер Blazor)**:
    *   `Login.razor` вызывает метод `LoginAsync` в `ApiServiceClient`, передавая `LoginRequest` (имя пользователя, пароль).
    *   `ApiServiceClient` отправляет POST-запрос на `api/v1/identity/loginpassword` на сервер Blazor.
    *   **`AuthHeaderHandler` (клиент)**: На этом этапе `AuthHeaderHandler` может быть вызван, но `UserState.PersonID` будет `null`, так как пользователь еще не аутентифицирован. Заголовок `X-Person-ID` не будет добавлен.
3.  **Обработка запроса на сервере Blazor (`IdentityController`)**:
    *   `IdentityController` получает `LoginRequest`.
    *   Он создает `HttpClient` (через `IHttpClientFactory`) для взаимодействия с внешним API. Этот `HttpClient` настроен с `ExternalApiBasicAuthClient` для базовой аутентификации с внешним API.
    *   `IdentityController` отправляет запрос на аутентификацию (обычно POST-запрос с учетными данными) к внешнему API.
    *   **Внешний API** аутентифицирует пользователя и возвращает `IdentityData`, содержащие `PersonID`, токен внешнего API и другие данные.
    *   **Кэширование `IdentityData`**: Если аутентификация с внешним API успешна, `IdentityController` кэширует ��олученные `IdentityData` в `IMemoryCache` сервера, используя ключ вида `IdentityData_{PersonID}`. Время жизни кэша обычно устанавливается на 1 час.
    *   `IdentityController` возвращает `ApiResponse<IdentityData>` клиенту.
4.  **Обработка ответа на клиенте (`Login.razor`)**:
    *   `Login.razor` получает ответ от `ApiServiceClient`.
    *   Если аутентификация успешна, `Login.razor` вызывает `UserState.SetIdentityDataAsync()`, передавая полученные `IdentityData`.
    *   `UserState` сохраняет `IdentityData` (включая `PersonID` и токен внешнего API) в `localStorage` браузера.
    *   `Login.razor` перенаправляет пользователя на домашнюю страницу или другую защищенную страницу.

## 3. Контроль UserState на клиенте

1.  **Инициализация `UserState`**:
    *   В `BlazorTool.Client/Program.cs`, `UserState` регистрируется как `Scoped` сервис.
    *   В конструкторе `UserState` запускается `LoadIdentityDataAsync()`, который асинхронно загружает `IdentityData` из `localStorage`. Результат этой асинхронной операции сохраняется в `UserState.InitializationTask`.
2.  **Ожидание инициализации на страницах**:
    *   На защищенных страницах Blazor (например, `SchedulerPage.razor`, `OrdersPage.razor`, `Settings.razor`), перед выполнением любых API-вызовов, которые зависят от `UserState`, добавляется `await UserState.InitializationTask;`. Это гарантирует, что `UserState` полностью загружен и `PersonID` доступен.
    *   Также на этих страницах проверяется `UserState.IsAuthenticated`, и если пользователь не аутентифицирован, происходит перенаправление на страницу входа.
3.  **`AuthHeaderHandler` и `X-Person-ID`**:
    *   `AuthHeaderHandler` также ожидает `await _userState.InitializationTask;` в своем методе `SendAsync`.
    *   После инициализации `UserState`, если `_userState.PersonID.HasValue` истинно, `AuthHeaderHandler` добавляет заголовок `X-Person-ID` к исходящему HTTP-запросу. Этот заголовок содержит `PersonID` текущего пользователя.
    *   **Важно**: `AuthHeaderHandler` на клиенте НЕ добавляет токен внешнего API в заголовок `Authorization`. Это сделано намеренно для повышения безопасности и разделения ответственности.

## 4. Авторизация запросов к внешнему API (Server-side External API Calls)

Этот процесс происходит, когда сервер Blazor должен сделать запрос к внешнему API от имени аутентифицированного пользователя.

1.  **Клиентский запрос к серверу Blazor**:
    *   Клиент (например, `ApiServiceClient` или любая Razor-страница) отправляет запрос к серверу Blazor (например, `DeviceController.GetList` или `WoController.GetList`).
    *   `AuthHeaderHandler` на клиенте добавляет заголовок `X-Person-ID` к этому запросу.
2.  **Обработка запроса на сервере Blazor**:
    *   Контроллер на сервере Blazor (например, `DeviceController` или `WoController`) получает запрос.
    *   Контроллер испо��ьзует `IHttpClientFactory` для создания `HttpClient` с именем `"ExternalApiBearerAuthClient"`. Этот клиент настроен на использование `ServerAuthHeaderHandler`.
    *   Контроллер выполняет запрос к внешнему API, используя этот `HttpClient`.
3.  **`ServerAuthHeaderHandler` (сервер)**:
    *   `ServerAuthHeaderHandler` перехватывает исходящий запрос от сервера Blazor к внешнему API.
    *   Он вызывает `ServerAuthTokenService.GetTokenAsync()`.
4.  **`ServerAuthTokenService.GetTokenAsync()` (сервер)**:
    *   Этот метод использует `IHttpContextAccessor` для доступа к текущему `HttpContext` и извлекает `PersonID` из заголовка `X-Person-ID` входящего запроса от клиента.
    *   Затем он пытается получить `IdentityData` из `IMemoryCache` сервера, используя ключ `IdentityData_{PersonID}`.
    *   Если `IdentityData` найдена в кэше и содержит действительный токен, этот токен возвращается.
    *   Если токен не найден в кэше (например, истек или кэш был очищ��н), метод возвращает `null`.
5.  **Добавление токена к запросу (сервер)**:
    *   `ServerAuthHeaderHandler` получает токен от `ServerAuthTokenService`.
    *   Если токен действителен, `ServerAuthHeaderHandler` добавляет его в заголовок `Authorization: Bearer [токен]` исходящего запроса к внешнему API.
    *   Запрос отправляется к внешнему API.
6.  **Внешний API** обрабатывает запрос, используя предоставленный Bearer-токен для авторизации.

## 5. Процесс выхода из системы (Logout Flow)

1.  **Пользователь инициирует выход**: Пользователь нажимает кнопку "Выход" или аналогичный элемент управления.
2.  **Вызов `UserState.LogoutAsync()`**:
    *   Этот метод очищает свойства `UserName`, `Password`, `Token`, `PersonID` и `RightMatrix` в `UserState`.
    *   Он также удаляет `identityData` из `localStorage` браузера.
3.  **Перенаправление**: После выхода пользователь обычно перенаправляется на страницу входа.