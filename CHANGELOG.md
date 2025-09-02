## 2025-09-02

### Features
*   **Activities Module**:
    *   Added `Activities` page with scheduler and new components: `ActivitySchedulerItem`, `NewActivityFormComponent`.
    *   New models: `ActivityAppointment`, `NewActivityFormModel`.
    *   Added page-specific CSS and layout for activities.
*   **API Client — refactor**:
    *   Split `ApiServiceClient.cs` into partial files for modularity: `ApiServiceClient.Activities.cs`, `ApiServiceClient.Devices.cs`, `ApiServiceClient.Dictionaries.cs`, `ApiServiceClient.Settings.cs`, `ApiServiceClient.Users.cs`, `ApiServiceClient.WorkOrders.cs`.
    *   Improves organization and maintainability of client API calls.
*   **Home / Navigation**:
    *   Added a data refresh button on the Home page.
    *   Adjusted `NavMenu.razor` and `Home.razor` so main content and navigation are shown only when user is authenticated.
*   **Localization**:
    *   Updated `UIStrings.resx` and `UIStrings.pl-PL.resx` to include keys used by the new activities UI.

## 2025-09-01
### Features
*   **SMB File Access and Credentials**:
    *   Added support for accessing files on network shares (SMB) with user-provided credentials.
    *   Implemented a new UI in settings for configuring SMB server, username, and password.
    *   Added a feature to check SMB credentials validity.
*   **Enhanced Notification System**:
    *   Introduced a new notification system using Telerik Notification component for displaying various types of messages (Info, Success, Warning, Error) to the user.
*   **Improved Activity Form**:
    *   Enhanced date handling in activity forms, allowing for more flexible minimum and maximum activity dates based on work order properties.
    *   Workload input now has a validation range of 0 to 24 hours.
    *   Added new localization strings for network share settings and workload validation.
*   **User Profile and Settings**:
    *   User profile display now includes `RightMatrixID` and `DepartmentID`.
    *   Enhanced the loading of user settings upon login, including network share credentials and color scheme preferences.

### Fixed / Improvements
*   **Activity Form**: Conditionally displays minimum and maximum activity dates for better user clarity.
*   **Login Process**: Optimized the login process by centralizing user settings loading into a dedicated API client method, ensuring consistent state management.
*   **File Download**: Changed file download links to open in a new tab (`target="_blank"`).
*   **Navigation Menu**: The navigation menu is now only visible when the user is authenticated.
*   **Home Page**: The main content of the home page is now only visible when the user is authenticated.
*   **Admin Privileges**: Admin check now includes `RightMatrixID == 1`.
*   **Dependencies**: Replaced `SharpCifs.Std` with `SMBLibrary` for SMB functionality.

## 2025-08-28
### Fixed / Improvements
*   **Device Display Component**:
    *   Updated general information fields to use `TelerikTextBox` for consistent display.
    *   Improved image placeholder when no device images are available.
    *   Refactored documentation file list to use custom HTML/CSS for better styling within the Telerik Window.
    *   Enhanced image loading with loading indicators and optimized full-size image loading in the modal.
    *   Added functionality to open  file  from documentation list.
*   **Devices Page**:
    *   Adjusted the minimum width of the device details window for better responsiveness.
    *   Implemented saving and loading of Telerik Grid state to persist user preferences across sessions.
    *   Enhanced device list display with dynamic row coloring based on device status.
    *   Added new filter options for Device Category, State Name, Type, Location, Location Name, Set Name, and Owner.
    *   Introduced `AssetNoShort` column.
*   **API Client**:
    *   Refactored `GetDeviceImageAsync` to use `SingleResponse<DeviceImage>`.
    *   Added `GetDeviceInfoForGridAsync` and `GetListDeviceInfoForGrid` for optimized device information retrieval for grid display.
    *   Added `GetWorkOrderDirectoryFiles` and `GetWorkOrderFile` methods for documentation file handling.
    *   Added `GetFileDownloadUrl` for direct file downloads.
    *   Removed `AddNewWODict` (deprecated).
*   **Server**:
    *   Added `DeviceController` endpoints for `GetDirectoryFiles`, `GetFile`, and `DownloadFile` to serve documentation files.
*   **Localization**:
    *   Added numerous new localization keys for device-related UI elements, including `AssetNoShort`, and various `DeviceDisplay_` prefixes.
*   **Navigation Menu**:
    *   Updated navigation links to use `TelerikSvgIcon` for improved visual consistency.
*   **Work Order Component**:
    *   Adjusted description field editability to allow editing for new work orders regardless of specific permissions.
*   **Internal**:
    *   Added fallback for `Authorization` header in `ServerAuthTokenService.cs` for local development.
    *   Introduced `ColorHelper` utility class for softened color generation.
    *   Updated `FullDeviceInfo` model with `LastState`, `StateHistory`, `LastStatus`, `HaveFiles`, `StateName` properties and initialized `Images` and `DirectoryFiles`.
    *   Added `WorkOrderFileItem` model.
    *   Updated `ViewSettingsService` with `DevicesPage` key.

## 2025-08-26
### Features
*   **Work Order Files**: Implemented listing and viewing of work order documentation files. This includes new API client methods (`GetWorkOrderDirectoryFiles`, `GetWorkOrderFile`), an enhanced `FullDeviceInfo` model to include file lists, and an updated `DeviceDisplay` component to show these files with appropriate icons and opening functionality.
*   **File model**: Added `WorkOrderFileItem` model to represent documentation files in the client.
*   **Server**: Added `DeviceController.GetFile` endpoint to serve individual work order files (with path normalization).
*   **API Client**: Refactored `GetAllDevicesCachedAsync` to `GetAllDevicesAsync` and updated its usages across `OrdersGrid`, `WorkOrderComponent`, and `DevicesPage`.
*   **Orders Page**: Added a "Reload All Data" button and a `HardLoadData` method for explicit data refresh.

### Fixed / Improvements
*   **Devices Page**: Implemented saving and loading of Telerik Grid state on `DevicesPage` to persist user preferences across sessions.
*   **Device Display Component**:
    *   Added localization for various texts and duration formatting.
    *   Improved image loading with loading indicators and optimized full-size image loading in the modal.
    *   Enhanced the device details window with improved styling, `MaxHeight`, `ThemeColor`, and `CloseOnOverlayClick` functionality.
*   **Work Order Component**: Adjusted description field editability to allow editing for new work orders regardless of specific permissions.
*   **API Client**: Refactored `GetDeviceImageAsync` and `GetFullDeviceInfoAsync` to use `SingleResponse<DeviceImage>` for better consistency and added a `skipImageLoad` option for `GetFullDeviceInfoAsync`.
*   **Localization**: Added numerous new localization keys for device-related UI elements.
*   **Bug Fix**: Corrected filtering logic for work order categories by device category in `ApiServiceClient`.

## 2025-08-25
### Features
* Implemented comprehensive device management features, including new API client methods (`GetDeviceDetailAsync`, `GetDeviceStateAsync`, `GetDeviceStatusAsync`, `GetSingleDeviceAsync`, `GetDeviceImageAsync`, `GetDeviceDictionariesAsync`, `GetFullDeviceInfoAsync`), new server-side controllers (`GetDetail`, `GetState`, `GetStatus`, `Get`, `GetImage`, `GetDict`), and new data models (`DeviceDetailProperty.cs`, `DeviceDict.cs`, `DeviceImage.cs`, `DeviceState.cs`, `DeviceStatus.cs`, `FullDeviceInfo.cs`).
* Enhanced Devices Page with a default `PageSize` of 30 for the Telerik Grid.
* Added "NavMenu_Devices" localization entries for English and Polish.
* Introduced new `DeviceDisplay` component and its associated CSS for improved device information presentation.
* Updated `DevicesPage.razor` to integrate the new `DeviceDisplay` component and enhance device-related UI.

## 2025-08-22
### Features
* Added caching for work order files (`ApiServiceClient.GetWorkOrderFilesAsync`, `ApiServiceClient.GetWorkOrderFileAsync`) and a public method to invalidate these caches (`ApiServiceClient.InvalidateWorkOrderFilesCache`).

### Fixed / Improvements
* Added method to retrieve a single work order file (`WorkOrderFileData` model, `ApiServiceClient.GetWorkOrderFileAsync`, `WoController.GetFile`).
* Added method to retrieve work order files (`WorkOrderFile` model, `ApiServiceClient.GetWorkOrderFilesAsync`, `WoController.GetFiles`).
* Added user information display on the Settings page.
* Enhanced activity display and user interface features.
* Updated `ApiServiceClient` and `WoController` to use `ApiResponse<WorkOrderFile>`.
* Changed activity list sorting to descending.
* Fixed auto-saving of pane sizes on the Home page.

## 2025-08-21
### Features
* Implemented user permission checks across components to control UI element visibility and functionality based on user roles.

### Fixed / Improvements
* Fixed a bug where `person_Take_Date` was always null when taking a work order.
* Added a function to retrieve WorkOrder categories and reasons by device ID and used it when creating/editing work orders; applied the same for Activity categories.
* Enhanced `TelerikComboBox`/list-picker components to support typing-based filtering for faster selection.

## 2025-08-20
### Performance Improvements
*   Improved responsiveness on the Home page when collapsing/expanding sections. The application now feels smoother as saving settings to your browser's local storage is optimized to happen less frequently.

## 2025-08-19
### UI/UX Improvements
* Added virtual scrolling support to `OrdersGrid` for smoother data browsing.
* Restructured the Home page with new grid containers and responsive CSS adjustments.
* Enabled search/filter in `TelerikMultiSelect` on the Scheduler page.
* Persisted splitter pane positions and collapsible panel states across sessions.

### Localization
* Added new localized strings for UI enhancements.

## 2025-08-18
### UI/UX Improvements
* Added collapsible sections on Home page grids (Assigned, Taken, Department)
 * Added expand/collapse toggles ("+" / "-") in grid headers
 * Added multi-select filters on Orders and Scheduler pages

### Localization
* Fixed translation for AddActivity error messages

### Code Changes
* Updated `Home.razor` to wrap grid content in collapsible containers with dynamic CSS classes
* Updated `Home.razor.css` to reset container height when collapsed
* Extended `ActivityController` with ActDict-based methods for adding new activities

## 2025-08-12 - 2025-08-13
### UI/UX Improvements
*   **Context Menu for Grids:** Implemented a right-click context menu for grids on the Home page, enhancing user interaction.
*   **New Order Button:** Added a button for creating new orders on the Home page.
*   **Localization:** Added translations for the newly introduced context menu.

### Code and Performance Changes
*   **Event Handling:** Implemented the `OnRowContextMenu` event for relevant components to support the new context menu functionality.

## 2025-08-11
### UI/UX Improvements
*   **View State Persistence:** The application now remembers the state of grids and filters on the "Home", "Orders", and "Scheduler" pages. After a page reload or new login, all sorting, filtering, and pagination settings will be restored.
*   **Home Page:** An indicator has been added to show whether a user can take multiple work orders simultaneously.
*   **Scheduler:** An "Add Activity" button has been added directly to the appointment editor window, simplifying the process of adding new activities to a work order.
*   **Add Activity Form:** Improved logic for calculating the suggested work time.

### Code and Performance Changes
*   **Implemented `ViewSettingsService`:** A new service has been created to manage user-specific view settings. It handles saving and loading component states (grids, filters) from the browser's local storage and synchronizing them with the server.
*   **Extended API:** New endpoints have been added to `SettingsController` to save and retrieve view settings for a specific user.
*   **Component Refactoring:** The `OrdersGrid` component has been refactored for better state management. Data loading and filter application logic on the `HomePage`, `OrdersPage`, and `SchedulerPage` have been improved for a smoother experience.
*   **State Management Optimization:** Centralized view state management via `ViewSettingsService`, reducing code duplication and simplifying maintenance.

## 2025-08-07 - 2025-08-08
### Fixed
- Corrected a minor logic bug in the `WorkOrderComponent` that affected state change notifications.
- Resolved an `AntiforgeryValidationException` on server deployment by configuring persistent data protection keys, ensuring stable sessions across application restarts.

### Changed
- Refactored forms (`AddActivityForm`, `AppointmentEditor`) for better layout, validation, and improved user interaction logic.
- Enhanced the `OrdersGrid` and `Home` page to provide better real-time data updates and a more responsive user experience.
- Updated UI elements, including button labels and navigation icons, for improved clarity and a more modern look.
- Modified `WorkOrderComponent` to correctly display assigned users and handle data refresh more efficiently.

### Added
- Implemented a "Take Work Order" feature, allowing users to assign themselves to an order directly from the UI.
- Added new API endpoints and request models to support the "Take Work Order" functionality.
- Introduced a method in `AppointmentService` to refresh individual appointments, improving data consistency.

## 2025-08-06
### Added
- **Orders Page**: A new, separate `OrdersGrid` component has been created to display the list of orders, improving modularity.
- **Orders Page**: Work orders are now editable directly from the orders list.
- **Scheduler Timeline**: Added quick selection buttons to set the timeline view to 3, 5, 7, or 14 days.
- **Home Page**: Implemented a session check for users.

### Changed
- **Scheduler Timeline**: The timeline view now only displays persons who have been selected in the filter.
- **Localization**: Added new translations for UI elements in both English and Polish.
- **Model Renaming**: The `Dict` data model has been renamed to `WODict` for better clarity.

### Fixed
- **Orders Page**: Corrected the sorting of orders to be descending by ID.
- **Orders Page**: Data in the grid now refreshes automatically after a work order is edited.

## 2025-08-05
### Added
- Added "Clear Filters" button to Activity and Work Order lists.
- Implemented multi-select filtering for grid columns (e.g., AssetNo in Scheduler).
- Added date range filtering (Last Year, Quarter, Month) to the "Add Date" column in the Scheduler's untaken orders grid.
- Added "Description" column to the Orders page grid.
- Added `ListTypeEnum` model (internal change supporting future categorized lists).

### Changed
- Improved UI/UX across ActivityDisplay, ActivityList, WorkOrderComponent, OrdersPage, and SchedulerPage with various styling updates for better readability and visual appeal.
- Standardized localization key references in UI components for better maintainability.
- Removed "Select All Devices" button from Orders page.
- Enhanced Work Order details window on Orders page with dark theme and custom close action.
- Improved clarity of Work Order ID in the details window title.
- Standardized column titles in Scheduler's untaken orders grid using localized strings.
- Updated row styles on Orders page for better visual separation and compactness.

### Fixed
- Corrected filtering logic in Scheduler to properly include "Not Assigned" persons when selected.

## 2025-08-04
### Added
- **Join Existing Activity**: Users can now be added to an existing activity via a new form in the work order view.
- **Add Activity**: Implemented a feature to add new activities directly from the work order view, including a dedicated form and API endpoint.

### Fixed
- **Scheduler TimeSlot Bug**: Corrected an issue with TimeSlot calculations on the Scheduler page.
- **Activity List Refresh**: The activity list now updates automatically after a new activity is added.
- **Work Order Data Persistence**: Corrected a bug where work order details (like activity count) would revert to a previous state after UI interactions.

### Changed
- **UI/UX**: Improved the layout and styling of the `WorkOrderComponent` for better readability and usability.

## 2025-08-01
### Added
- **Work Order Reason Editing**: The `WOReason` field is now editable in the work order editor.
- **Assigned Person View**: Added the ability to view assigned persons in the work order details.
- **Polish Language Support**: Released Polish language support. The language can be switched after login and in the Settings.
### Changed
- **Work Order Validation**: Modified the validation for work order data in the editor window.
- **Work Order Dates**: The `StartDate` and `EndDate` fields in the work order editor are now editable.
### Fixed
- **Department List Bug**: Fixed an issue with loading the department list.
- **New Work Order Bug**: Fixed a bug related to creating a new work order (appointment).

## 2025-07-30
### Added
- **Advanced Grid Filters**: Implemented advanced filtering capabilities in `TelerikGrid` on the `OrdersPage`, including filters for Department, Reason, Modified Person, Assigned Person, Work Order State, Work Order Level, Add Date, and Start Date.
- **Person View in Activities**: Added the ability to view assigned persons by name in `ActivityDisplay` and `ActivityList` components.
- **Person Data Caching**: Implemented caching for person data in `ApiServiceClient` to improve performance.
### Changed
- **UI/UX - Orders Page**: Old filters for Device Type and Order State were removed.
- **UI/UX - Work Order Status Colors**: Updated the display logic for workorder status colors to be based on `StateID` for consistency. Introduced new, lighter background colors for better readability.
### Fixed
- **Authentication Bug**:old user in cache. Modified the authentication header handling to clear user state upon login requests. 

## 2025-07-29
### Added
- **Scheduler Context Menu**: Implemented a right-click context menu in the Scheduler. Users can now:
    - Create new appointments by right-clicking on an empty time slot.
    - Open, remove, or duplicate existing appointments by right-clicking on them.	
### Changed
- **Settings**: Changing the API address on the Settings page now automatically reloads the application to ensure the new settings are applied correctly.

- **Color Scheme**: Refactored the status color logic to be based on State IDs, ensuring consistent color representation across the application (respecting the "Use original colors" setting). New, lighter background colors have been introduced for better readability.

### Fixed
- **Authentication**: Fixed an issue where a session check could incorrectly trigger on the login page, causing a redirect loop.
- **Appointment Editor**: Canceling the creation of a new appointment now correctly closes the editor window.

## 2025-07-28
### Added
- **Color Scheme Switcher**: Added a "Use original colors" checkbox in the navigation menu. This allows users to switch between the original colors provided by the API and a custom color palette.

### Changed
- **UI/UX**:
    - Replaced basic loading indicators with `TelerikLoaderContainer` on the `OrdersPage` and `SchedulerPage`.
    - The total order count is now displayed in the `OrdersPage` header.
- **Refactoring**:
    - Centralized color management into `AppStateService` to ensure a consistent color scheme across all components (`SchedulerSummary`, `OrdersPage`, `SchedulerPage`).
    - Components now subscribe to `AppStateService.OnChange` to dynamically update their appearance when the color scheme is changed.
- **API Address Change**: Implemented a mechanism to restart the application after changing the external API address on the Settings page. This ensures that the new address is correctly applied.
- **Confirmation for API Change**: Added a Telerik confirmation dialog on the Settings page, which appears when a user attempts to change the API address. The dialog displays both the old and new addresses and warns that the application will restart.
- **Refactoring**: 
    - Replaced the obsolete `ListTypeEnum` with the correct `WOListTypeEnum` across the client-side application.
    - Improved data loading and filtering logic on the `SchedulerPage` for departments and device categories.
    - Refactored `ApiServiceClient` to correctly handle nullable `personID` in dictionary-related methods.

### Fixed
- **Error Handling**: Improved exception handling in `UserSettings.cs` to ensure errors are properly thrown and logged.

## 2025-07-24
### Changed
- **API Address Configuration**: The external API address can now be configured on the Settings page by the "MES" user. The address is stored globally for the application in the `appsettings.json` file on the server.
- **API Address Handling**: The application's server-side calls were unified to correctly handle API server addresses with or without the `/api/v1` suffix, using the centralized address from the configuration.

### Fixed
- **Scheduler Drag & Drop**: Fixed a critical bug where dragging a work order from the grid to the Scheduler would fail if the Scheduler was not in "Timeline" view. The assignment logic now correctly applies only to the Timeline view, preventing errors in other views.

## 2025-07-23
### Added
- **Build Date Display**: Implemented a mechanism to display the application's build date in the main layout, providing clear versioning information.
- **Scheduler Summary Table**: Added a summary table on the Scheduler page (Timeline view) that shows work order counts per person, broken down by status. This provides an at-a-glance overview of each team member's workload.
- **Orders Page Filtering**: Introduced a new filter for "Assigned persons" on the Orders page, allowing users to narrow down the work order list by specific team members.

### Fixed
- **Scheduler Drag & Drop**: Corrected an issue where dragging a work order from the grid to the Scheduler's timeline did not correctly assign the appointment to the target person (resource).
- **Filter UI Behavior**: The "Assigned persons" filter on the Orders page now updates the list instantly when cleared via the "Clear" button.
- **Filter Label Alignment**: Fixed the alignment of the "Assigned persons" filter label on the Orders page to ensure it is positioned correctly above the multiselect component, consistent with other filters.

## 2025-07-22
## Added
- **Scheduler Timeline View:** Introduced a new Timeline view for the scheduler, allowing for horizontal display of appointments.
- **Dynamic Timeline View Settings:** Added UI controls to dynamically adjust `Column Width`, `Slot Duration`, and `Slot Divisions` for the Timeline view, providing greater flexibility in how time intervals are displayed.
- **Scheduler Item Component:** Introduced a reusable `SchedulerItem` component to standardize the rendering of appointments across different scheduler views.
- **Scheduler Loading Indicator:** Added a loading indicator to the scheduler to provide visual feedback during data fetching and authorization processes.

## Changed
- **Scheduler Grouping Logic:** Resolved `KeyNotFoundException` in Timeline view grouping by ensuring the correct resource field (`AssignedPerson`) is referenced in `SchedulerGroupSettings`.
- **Scheduler UI Enhancements:** Applied custom styling to scheduler items and day cells in Month, Day, and Week views for improved visual appearance.
- **Cache Management Refactoring:** Refactored internal cache management for appointments, leading to more efficient data handling and smoother UI updates in the scheduler.
- **Appointment Service Method Rename:** Renamed `CloseAppointment` method to `RemoveAppointment` in the `AppointmentService` for clearer semantics.

## Fixed
- `KeyNotFoundException`: Addressed the `KeyNotFoundException` occurring when switching to the Telerik Scheduler Timeline view, specifically when grouping by assigned persons.


## 2025-07-21
- **Refactoring & Bug Fixes:**
    - Refactored cache management in `ApiServiceClient` for `WorkOrder` objects to ensure data consistency. Implemented centralized methods for updating, invalidating, and refreshing the cache (`UpdateWorkOrderInCache`, `InvalidateWorkOrdersCache`, `RefreshWorkOrderInCacheAsync`).
    - All `WorkOrder` manipulation methods (`Update`, `Save`, `Close`) now use the new centralized cache logic.
    - Corrected the UI update logic in `SchedulerPage.razor`. The local appointment collection (`_allAppointments`) is now correctly modified when an appointment is changed or closed, ensuring the UI reflects the current state.
    - Enhanced `AppointmentEditor.razor` to support closing work orders and properly propagate UI updates.
    - Added the `WorkOrderInfo.cs` model and updated `WoController` to support fetching detailed work order information.

## 2025-07-09
- **Functionality & UI:**
    - Implemented the complete lifecycle for Work Orders, allowing users to create, update, and close them through the API.
    - Added robust validation for required fields (like Description, Level) in the Work Order editor, with visual highlighting for missing data.
    - Closing a Work Order now requires at least one associated activity.
    - Pop-up notifications now display specific error messages from the API, providing clearer user feedback.
- **Technical & Refactoring:**
    - Implemented a server-side session check on application load. Users are now automatically logged out if their server session is invalid, preventing API errors.
    - Refactored `ApiServiceClient` methods (`Save`, `Update`, `Close`) to return a `SingleResponse<T>` object, providing detailed error information.
    - Added dedicated API endpoints and request models (`WorkOrderCreateRequest`, `UpdateWorkOrderRequest`, `CloseWorkOrderRequest`) for all Work Order operations.
    - Switched from using string names to IDs for entities like Categories and Levels, improving data integrity.

## 2025-07-08
- **Work Order Management Enhancement**: Implemented client-side validation and mapping of `WorkOrder` to `WorkOrderCreateRequest` in `ApiServiceClient.SaveWorkOrderAsync`. The `WoController.Create` endpoint now directly accepts `WorkOrderCreateRequest`. `ApiServiceClient.UpdateWorkOrderAsync` now utilizes `SaveWorkOrderAsync` for new work orders, ensuring consistent API saving. Added `ReasonID`, `CategoryID`, `DepartmentID`, and `AssignedPersonID` fields to `WorkOrderCreateRequest`.
- **Client-Server Token Synchronization**: Implemented a robust mechanism where the Blazor server retrieves external API tokens from its cache using the `PersonID` provided by the client in a custom `X-Person-ID` HTTP header. This eliminates the need for the client to directly send the external API token to the Blazor server.
- **Client-side Initialization Enhancement**: Ensured that `UserState` data is fully loaded from `localStorage` before any client-side API calls are made. This was achieved by awaiting `UserState.InitializationTask` in `AuthHeaderHandler` and relevant Razor pages (`SchedulerPage`, `OrdersPage`, `Settings`).
- **Server-side Token Management**: The `IdentityController` now caches `IdentityData` (including the external API token) upon successful user login. The `ServerAuthTokenService` has been refactored to retrieve these tokens from the server's `IMemoryCache` based on the `PersonID` extracted from the `X-Person-ID` header.
- **API Error Handling**: Added `try-catch` blocks to all HTTP request methods within `ApiServiceClient` to gracefully handle network and API errors, logging them to the console and returning empty/default values.
- **Dependency Refinement and Cleanup**: Removed obsolete JWT Bearer authentication configurations and hardcoded login credentials from the server-side `Program.cs`, streamlining the authentication and dependency injection setup.
- **API Documentation**: Added `API-info.md` to provide clear documentation of API endpoints and their authorization types.

## 2025-07-07
- **UI and Functionality:**
    - Improved login experience with a searchable dropdown for username and basic input validation.
    - Enhanced Work Order editor with dynamic selection for categories and levels, and improved device/category display.
    - Automatic caching of new dictionary entries (categories, levels, departments).
    - Refined work order filtering logic on the Scheduler page.
    - Improved API client error handling.
- **Code Changes:**
    - Removed `ListDicts` parameter from `AppointmentEditor.razor`.
    - Updated `WorkOrderComponent.razor` to use `ListCategories` and `ListLevels` for TelerikComboBox, removed `ListDicts` initialization logic, and added `selectedDevice` update logic.
    - Replaced direct `ListDicts` manipulations with `apiService.AddNewWODict` calls for categories, departments, and levels.
    - Replaced username input with `TelerikComboBox` in `Login.razor`.
    - Added loading indicator and removed `ListDicts` parameter from `SchedulerPage.razor`.
    - Modified work order filtering logic in `SchedulerPage.razor`.
    - Added `_dictCache` and new dictionary-related methods (`GetWODictionaries`, `GetWODictionariesCached`, `GetWOCategories`, `GetWOStates`, `GetWOLevels`, `GetWOReasons`, `GetWODepartments`, `AddNewWODict`) to `ApiServiceClient.cs`.
    - Improved error handling in `PostSingleAsync` in `ApiServiceClient.cs`.
    - Added a parameterless constructor to `UserState.cs`.
    - Changed login endpoint to `api/v1/identity/loginpass` and method to GET in `IdentityController.cs`.
    - Registered `UserState` as a Scoped service in `Program.cs`.

## 2025-07-04
- **UI and Functionality:**
    - Implemented user authorization via `Login.razor` page.
    - Integrated `UserState` for managing current user's login and token.
- **Code Changes:**
    - Added `Password` property to `UserState.cs`.
    - Refactored `AuthHeaderHandler.cs` to retrieve user credentials from `UserState` for token refresh.
    - Modified `BlazorTool.Client/Program.cs` to correctly inject `UserState` into `AuthHeaderHandler`.
    - Added `PostSingleAsync` method to `ApiServiceClient.cs` for handling single object API responses.
    - Updated `Login.razor` to use `ApiServiceClient.PostSingleAsync` for authentication and to update `UserState` upon successful login.
    - Completed `RightMatrix` model to correctly deserialize user rights.
    - Modified `IdentityController.cs` to return full `IdentityData` from external API.
    - Added `Expires` property to `IdentityData` model.
    - Refactored `AuthHeaderHandler` to use `IdentityData` and `ApiResponse<IdentityData>` for token management.
    - Enhanced `UserState` to persist `IdentityData` to `localStorage` and load it on startup.
    - Modified `Login.razor` to explicitly save `IdentityData` to `UserState` (and thus `localStorage`) after successful login.

## 2025-07-03
- **UI and Functionality:**
    - Implemented the creation of new appointments in the Scheduler.
    - Added a combobox for selecting a Device when creating a new appointment.
    - Implemented validation for required fields in the AppointmentEditor.
    - Added a popup notification to alert users about unfilled required fields.
    - Renamed the "Title" field to "AssetNo" in the AppointmentEditor and highlighted it if empty.
    - Improved `ChangelogPage` to render Markdown for better readability.
- **Code Changes:**
    - Added logic to `AppointmentService` to handle the creation of new appointments.
    - Implemented saving new appointments via `ApiServiceClient`.
    - Added a dropdown list for `Device` selection in `WorkOrderComponent` for new appointments.
    - Added validation logic in `AppointmentEditor.razor`.
    - Implemented a Telerik Popup for notifications in `AppointmentEditor.razor`.
    - Updated styles in `AppointmentEditor.razor.css`.
    - Added `Markdig` package to render Markdown in `ChangelogPage`.

## 2025-07-02
- **UI and Functionality:**
    - Added a status filter to the `OrdersPage`.
    - Improved filtering logic on the `OrdersPage`.
    - Added descriptive labels for the filters.
    - Added total workload to the activity list header.
    - The `WorkOrderComponent` now expands to show the full list of activities without scrolling.
    - Aligned the column sizes between the header in `ActivityList` and the items in `ActivityDisplay` for a consistent look.
    - Users can now click on the "Actions" statistic in the work order view to see a detailed list of activities.
    - The activity list is displayed in a compact, readable table format.
- **Code Changes:**
    - Added `totalWorkload` calculation in `ActivityList.razor`.
    - Used a conditional CSS class in `WorkOrderComponent.razor` to adjust `max-height` when activities are visible.
    - Corrected JSON deserialization for the `Activity` model by adding `[JsonConstructor]` to the default constructor.
    - Created `Activity.cs` model.
    - Added `ActivityController` to fetch activity data from the external API.
    - Implemented `GetActivityByWO` in `ApiServiceClient` to retrieve activities.
    - Developed `ActivityList` and `ActivityDisplay` Blazor components to show the activities.
    - Modified `WorkOrderComponent` to display the activity list on user interaction.
    - Beautify ChangelogPage

## 2025-07-01
- **UI and Functionality:**
    - On `SchedulerPage`, added a `Device` filter by `AssetNo` after the `Department` filter.
    - Made `WorkOrderComponent` editable, adding a Telerik ComboBox for the `WOCategory`, `WOLevel` fields.
    - In `WorkOrderComponent`, highlighted empty `Department` and `StartDate` fields.
    - On `SchedulerPage`, changed the logic for displaying taken/untaken orders.
    - On `SchedulerPage`, customized the scheduler item's text and color based on its state.
- **Code Changes:**
    - Added a new endpoint `api/v1/wo/getdict` to the `WoController` to retrieve work order categories.
    - Updated `ApiServiceClient` to fetch `Dict` objects.
    - Added `Dict.cs` model.
- **Bug Fixes:**
    - Fixed an issue in `WorkOrderComponent` where `Department` and `assignedPersons` were not displaying correctly.

## 2025-06-30
- **UI and Functionality:**
    - Made `WorkOrderComponent` editable, adding Telerik components for fields `assignedPersons`, `Department`, and `Description`.
    - On `OrderPage`, added a 'Device' column.
    - On `SchedulerPage`, added `OnClick` functionality for orders (to view WorkOrders in the order list) and implemented header filters for orders.
- **Code Changes:**
    - made and added `telerik_manual.md` for Telerik component usage instructions.

## 2025-06-27
- Added Changelog page and updated logging.
- Introduced `ChangelogPage.razor` and `CHANGELOG.md` for release notes.
- Replaced `Debug.Print` with `Console.WriteLine` for consistent logging across client and server.
- Minor UI adjustments in `MainLayout.razor`, `OrdersPage.razor`.
- Configured `CHANGELOG.md` to be copied to `wwwroot` for client-side access.
- Implemented display of `CHANGELOG.md` content on `ChangelogPage.razor`.
- Configured default `HttpClient` in `Program.cs` with `BaseAddress` for static file access.
- Corrected `Link` attribute in `BlazorTool.Client.csproj` for `CHANGELOG.md` to ensure correct placement in `wwwroot`.
- Reverted `app.UseBlazorFrameworkFiles()` in `BlazorTool/Program.cs` as it caused application loading issues.
- Re-added `HttpClient` registration with `BaseAddress` and `serverBaseUrl` check in `BlazorTool.Client/Program.cs`.
- **SchedulerPage.razor:**
    - Added device category filter to the untaken orders grid.
    - Implemented a checkbox to show/hide completed orders in the untaken orders grid.
    - Added `OnChange` event for assigned persons multiselect to trigger immediate display updates.
    - Adjusted layout for filter controls and order display.

## 2025-06-26
- Enhanced Scheduler UI and updated Blazor render mode.
- Configured client HttpClient for dynamic server base URL.
- Gitignore updated.
- Implemented PersonController and refactored API client for external calls.

## 2025-06-25
- In process: adding Filters to Scheduler.
- WorkOrderComponent: added scrolling, more compact data view, dynamic status color. OrdersPage: aligned filter items inline.

## 2025-06-24
- Added filters to Orders Page, including multiSelect.
- Fixed AUTH problems, added different http clients.
- Refactored Program.cs: DI, added AuthHeaderHandler for getting token, removed getting token for ApiServiceClient, added HostAddress in appSettings for server.
- Aligned SchedulerEditor buttons.

## 2025-06-23
- Implemented AppointmentService Save/Delete, ApiServiceClient Save/DeleteAppointment.
- Added Appointment view in Scheduler, and Scheduler - Move/Update items.
- Updated SchedulerAppointment to inherit from WorkOrder.
- Made WorkOrderComponent view only.

## 2025-06-20
- Configured OrdersPage with 3 columns.
- Enabled opening orders on click.
- Implemented API check to Server, Save and Read settings.
- Added Settings controller.
- Added Settings page, TestApiAddress settings.razor, and CheckApiAddress in ApiServiceClient.
- Added Order page.
