# Telerik Blazor Components Manual

This manual provides a list of Telerik UI for Blazor components with links to their official documentation.

## Data Management
- [Filter](https://docs.telerik.com/blazor-ui/components/filter/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | Class | string | The class that will be rendered on the outermost element. |
    | Value | CompositeFilterDescriptor | Sets the value of the Filter component. |
- [Grid](https://docs.telerik.com/blazor-ui/components/grid/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | AdaptiveMode | AdaptiveMode enum (None) | Defines the adaptive mode of the Grid. When set to Auto, and the window width is below 768px or RootComponentAdaptiveSettings.Medium, the Grid will render its inner popups (for example, FilterMenu, ContextMenu and more) as an ActionSheet. |
    | Class | string | Additional CSS class for the <div class="k-grid"> element. Use it to apply custom styles or override the theme. For example, change the Grid font size. |
    | Height | string | A height style in any supported CSS unit. You can also make the Grid height change automatically with the browser window. |
    | Navigable | bool | Enables keyboard navigation. |
    | Width | string | A width style in any supported CSS unit. The Grid has no default width, but expands horizontally to fill its container. |
- [ListView](https://docs.telerik.com/blazor-ui/components/listview/overview)
  - **Parameters**
    | Parameter | Type and Default Value | Description |
    |---|---|---|
    | Class | string | The class attribute of the `<div class="k-listview">` element. Use it to apply custom styles or override the theme. |
    | Data | IEnumerable<TItem> | The ListView component data collection. |
    | EnableLoaderContainer | bool (true) | The ListView loading container that is shown when there are long-running operations. |
    | Height | string | The height style of the component in any supported CSS unit. The default ListView dimensions depend on the CSS theme. |
    | Page | int (1) | The current page of the ListView component. |
    | Pageable | bool (false) | Determines if the ListView allows paging. |
    | PageSize | int (10) | The number of items to display per page in the ListView. |
    | Width | string | The width style of the component in any supported CSS unit. The default ListView dimensions depend on the CSS theme. |
- [Pager](https://docs.telerik.com/blazor-ui/components/pager/overview)
  - **Parameters**
    | Parameter | Type | Description |
    | --- | --- | --- |
    | **Adaptive** | `bool` | Defines whether pager elements a new line is createdchange depending based to the on screen the size. When enabled, the Pager will hide its Pager Info and PageSize Dropdownlist if they cannot fit in the available space. In the smallest resolution, the page buttons will be rendered as a select element. This parameter will be deprecated in the next major version in favor of the new Responsive parameter. |
    | **AdaptiveMode** | `AdaptiveMode enum` | Defines the adaptive mode of the Pager. When set to Auto , and the window width is below 768px or RootComponentAdaptiveSettings.Medium , components with popups will render them as an ActionSheet . In this case, the page sizes dropdown only. |
    | **ButtonCount** | `int` | The maximum number of page buttons that will be visible. To take effect, ButtonCount must be smaller than the page count ( ButtonCount < Total / PageSize ). |
    | **Class** | `string` | Renders a custom CSS class to the `<div class="k-pager-wrap">` element. |
    | **Page** | `int` | Represents the current page of the pager. The first page has an index of 1 . Supports two-way binding. If no value is provided, the parameter will default to the first page (1), but you should always use this parameter value in order to successfully use the component. If you don't use two-way binding and you don't update the value of the parameter after the user action, the pager UI will not reflect the change and will revert to the previous value (page index). |
    | **PageSize** | `int` | The number of items to display on a page. Supports two-way binding. |
    | **PageSizes** | `List<int?>` | Allows users to change the page size via a DropDownList. The attribute configures the DropDownList options. A null item in the PageSizes List will render an "All" option. By default, the Pager DropDownList is not displayed. You can also set PageSizes to null programmatically to remove the DropDownList at any time. |
    | **InputType** | `PagerInputType enum` | Determines if the pager will show numeric buttons to go to a specific page, or a textbox to type the page index. The arrow buttons are always visible. The PagerInputType enum accepts values Buttons (default) or Input . When Input is used, the page index will change when the textbox is blurred, or when the user hits Enter. This is to avoid unintentional data requests. |
    | **Responsive** | `bool` | Defines whether pager elements change based on the screen size. When enabled, the Pager will hide its Pager Info and PageSize Dropdownlist if they cannot fit in the available space. In the smallest resolution, the page buttons will be rendered as a select element. |
    | **ShowInfo** | `bool` | Defines whether the information about the current page and the total number of records is present. |
    | **Total** | `int` | Represents the total count of items in the pager. |
- [PivotGrid](https://docs.telerik.com/blazor-ui/components/pivotgrid/overview)
  - **Parameters**
    ### Grid Parameters
    The following table lists the `TelerikPivotGrid` parameters.
    | Parameter | Type and Default Value | Description |
    | :--- | :--- | :--- |
    | `Class` | `string` | A custom CSS class for the `<div class="k-pivotgrid">` element. Use it to override theme styles. |
    | `ColumnHeadersWidth` | `string` | The width of each column in any supported CSS unit. |
    | `Data` | `IEnumerable<TItem>` | The Pivot Grid component data. Use only with Local `DataProviderType`. |
    | `DataProviderType` | `PivotGridDataProviderType` enum (`Local`) | The type of data source that the Pivot Grid will use. |
    | `EnableLoaderContainer` | `bool` (`true`) | Defines if a built-in LoaderContainer will show during long-running operations (over 600ms). |
    | `Height` | `string` | A height style in any supported CSS unit. |
    | `LoadOnDemand` | `bool` (`true`) | Defines if the PivotGrid will request only the data to display in the current view, or all data. |
    | `RowHeadersWidth` | `string` | The width of all row headers in any supported CSS unit. |
    | `TItem` | `object` | The PivotGrid `@typeparam`. Required if the data item type cannot be inferred at compile-time. |
    | `Width` | `string` | A width style in any supported CSS unit. |

    ### Row, Column and Measure Parameters
    The following table lists parameters of the `PivotGridRow`, `PivotGridColumn` and `PivotGridMeasure` tags.
    | Parameter | Type and Default Value | Description |
    | :--- | :--- | :--- |
    | `Aggregate` | `PivotGridAggregateType` enum (`Sum`) | The nature of the calculated aggregate values. Applies to `PivotGridMeasure` only. |
    | `Format` | `string` | The display format of the calculated aggregate values, for example "{0:C2}". Applies to `PivotGridMeasure` only. |
    | `HeaderClass` | `string` | Adds a custom CSS class to the respective row header, column header or measure. |
    | `Name` | `string` | The field name of the respective row, column or measure. |
    | `Title` | `string` | The label to be displayed in the Configurator for the respective row, column or measure. |

    ### Configurator Parameters
    The following table lists parameters of the `TelerikPivotGridConfigurator` component.
    | Parameter | Type | Description |
    | :--- | :--- | :--- |
    | `Class` | `string` | A custom CSS class for the `<div class="k-pivotgrid-configurator">` element. |
    | `EnableLoaderContainer` | `bool` (`true`) | Defines if a built-in LoaderContainer will show during long-running operations (over 600ms). |

    ### Button Parameters
    The following table lists parameters of the `TelerikPivotGridConfiguratorButton` component.
    | Parameter | Type | Description |
    | :--- | :--- | :--- |
    | `Class` | `string` | A custom CSS class for the `<div class="k-pivotgrid-configurator-button">` element. |

    ### Container Parameters
    The following table lists parameters of the `TelerikPivotGridContainer` component.
    | Parameter | Type | Description |
    | :--- | :--- | :--- |
    | `Class` | `string` | A custom CSS class for the Container `<div>` element. |
- [Spreadsheet](https://docs.telerik.com/blazor-ui/components/spreadsheet/overview)
  - **Parameters**
    | A | B | C |
    |---|---|---|
    | **Class** | string | The custom CSS classes to render for the `<div class="k-spreadsheet">` element. |
    | **ColumnHeaderHeight** | double (20) | The pixel height of the column headers that display the letters A, B, C, and so on. |
    | **ColumnsCount** | int (50) | The initial number of columns to render. |
    | **ColumnWidth** | double (64) | The initial pixel width of the columns. |
    | **Data** | byte[] | The Excel file to display in the Spreadsheet component. |
    | **EnableLoaderContainer** | bool (true) | Defines if the component will show a built-in LoaderContainer while loading Excel files. |
    | **Height** | string | The height style of the `<div class="k-spreadsheet">` element. |
    | **RowHeaderWidth** | double (32) | The pixel width of the row headers that display the row numbers. |
    | **RowHeight** | double (20) | The initial pixel height of the rows. |
    | **RowsCount** | int (200) | The initial number of rows to render. |
    | **Tools** | SpreadsheetToolSet (SpreadsheetToolSets.All) | The available tabs and tools that users can use to manipulate the Excel file content. |
    | **Width** | string | The width style of the `<div class="k-spreadsheet">` element. |
- [TreeList](https://docs.telerik.com/blazor-ui/components/treelist/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | Class | string | The additional CSS class that will be rendered to the `div.k-treelist` element. Use it to apply custom styles or override the theme. For example, change the TreeList font size. |
    | Height | string | The height value in any supported CSS unit. |
    | Navigable | bool | Whether keyboard navigation is enabled. |
    | Width | string | The width value in any supported CSS unit. The TreeList has no default width, but expands horizontally to fill its container. |

## File Management
- [DropZone](https://docs.telerik.com/blazor-ui/components/dropzone/overview)
  - **Parameters**
    ### Parameters
    | Parameter | Type and Default Value | Description |
    |---|---|---|
    | `Id` | `string` | The id of the DropZone. Assign the same value of the corresponding Upload or FileSelect component to the `DropZoneId`. |
    | `Enabled` | `bool` (`true`) | Specifies whether the DropZone is enabled. |
    | `HintText` | `string` | The text for the hint of the DropZone. If not provided, the DropZone will render a default value ("Drag and drop files here to upload"). The label text is also localizable. |
    | `NoteText` | `string` | Optional content inside the DropZone. Use it to render any additional information below the hint. The label text is also localizable. |
    | `Multiple` | `bool` (`true`) | Enables the user to drop several files at the same time. |

    ### Styling and Appearance
    | Parameter | Type | Description |
    |---|---|---|
    | `Class` | `string` | The CSS class that will be rendered on the main wrapping element of the DropZone (`<div class="k-external-dropzone">`). Use it for overriding the theme or applying custom styles. |
    | `DragOverClass` | `string` | The CSS class that will be rendered on the main wrapping element of the DropZone when a file is dragged over it. Use it for conditionally styling the component during a dragover action. |
    | `Width` | `string` | The width of the DropZone. Accepts a valid CSS value. |
    | `Height` | `string` | The height of the DropZone. Accepts a valid CSS value. |
- [FileManager](https://docs.telerik.com/blazor-ui/components/filemanager/overview)
  - **Parameters**
    ### FileManager Parameters
    | Parameter | Type | Description |
    |---|---|---|
    | **Data** | `IEnumerable<TItem>` | Allows providing data source to the component. |
    | **EnableLoaderContainer** | `bool` | Specifies if loader container should be shown on slow async operations. |
    | **Path** | `string` | The current path. Updated when the user navigates. Two-way bindable. Handle the `PathChanged` event if you need to react to the user navigation. |
    | **View** | `FileManagerViewType` enum (`ListView`) | The layout of the FileManager main section. It can show the files and folders as table rows or as thumbnails. |

    ### Styling and Appearance
    | Parameter | Type | Description |
    |---|---|---|
    | **Class** | `string` | The CSS class that will be rendered on the topmost wrapping element of the component. |
    | **Width** | `string` | The width of the component. |
    | **Height** | `string` | The height of the component. |
- [FileSelect](https://docs.telerik.com/blazor-ui/components/fileselect/overview)
  - **Parameters**
    | Parameter | Type and Default Value | Description |
    | --- | --- | --- |
    | `Accept` | `string` | The `accept` HTML attribute of the file `<input>`. It controls what file types and MIME types the browser will allow users to select. |
    | `AllowedExtensions` | `List<string>` | The list of allowed file types. The component will check if the selected files are compliant after selection. |
    | `Capture` | `string` | The `capture` HTML attribute of the `<input type="file" />` element. It enables users to provide a file directly from their device camera. |
    | `Class` | `string` | Renders a custom CSS class to the `<div class="k-upload">` element. |
    | `DropZoneId` | `string` | The `id` that is used to connect the FileSelect to an external DropZone. |
    | `Enabled` | `bool` (true) | Enables file selection. |
    | `Id` | `string` | Renders an `id` attribute to the `<input type="file" />` element. Can be used together with a `<label>`. |
    | `MaxFileSize` | `long?` | Sets the maximum allowed file size in bytes. |
    | `MinFileSize` | `long?` | Sets the minimum allowed file size in bytes. |
    | `Multiple` | `bool` (true) | Sets if the user can select several files at the same time. |
    | `Files` | `IEnumerable<FileSelectFileInfo>` | Collection of files that will be initially displayed in the FileSelect file list. |
- [Upload](https://docs.telerik.com/blazor-ui/components/upload/overview)
  - **Parameters**
    | Parameter | Type and Default Value | Description |
    |---|---|---|
    | **Accept** | `string` | The `accept` HTML attribute of the file `<input>`. It controls what file types and MIME types the browser will allow users to select. |
    | **AllowedExtensions** | `List<string>` | The list of allowed file types. The component will check if the selected files are compliant after selection. |
    | **AutoUpload** | `bool` (true) | When `true`, the upload process starts automatically after file selection. When `false`, the component renders an upload button. |
    | **Capture** | `string` | The `capture` HTML attribute of the `<input type="file" />` element. It enables users to provide a file directly from their device camera. |
    | **Class** | `string` | Renders a custom CSS class to the `<div class="k-upload">` element. |
    | **DropZoneId** | `string` | The `id` that is used to connect the Upload to an external DropZone. |
    | **Enabled** | `bool` (true) | Enables file selection and upload. |
    | **Id** | `string` | Renders an `id` attribute to the `<input type="file" />` element. Can be used together with a `<label>`. |
    | **MaxFileSize** | `long?` | The maximum allowed file size in bytes. |
    | **MinFileSize** | `long?` | The minimum allowed file size in bytes. |
    | **Multiple** | `bool` (true) | Sets if the user can select several files at the same time. The component always uploads files one by one, and the controller method receives them separately. |
    | **RemoveField** | `string` ("files") | Sets the `FormData` key, which contains the file name submitted for deletion to the `RemoveUrl` endpoint. |
    | **RemoveUrl** | `string` | The URL which receives the file names for deletion. |
    | **SaveField** | `string` ("files") | Sets the `FormData` key, which contains the file submitted to the `SaveUrl` endpoint. |
    | **SaveUrl** | `string` | The URL which receives the uploaded files. |
    | **WithCredentials** | `bool` | Controls if the Upload will send credentials such as cookies or HTTP headers for cross-site requests. |
    | **Files** | `IEnumerable<UploadFileInfo>` | Collection of files that will be initially displayed in the Upload file list. |

## Navigation
- [Breadcrumb](https://docs.telerik.com/blazor-ui/components/breadcrumb/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | **Class** | `string` | The CSS class that will be rendered on the main wrapping element of the Breadcrumb. |
    | **CollapseMode** | `BreadcrumbCollapseMode` | Specifies how the Breadcrumb items are displayed if they cannot fit on a single line. |
    | **Data** | `IEnumerable<T>` | A collection of flat data for all items in the Breadcrumb. |
    | **Height** | `string` | The height of the Breadcrumb component. |
    | **ItemTemplate** | `RenderFragment<T>` | Defines a custom template for the Items of the Breadcrumb. |
    | **SeparatorTemplate** | `RenderFragment` | Defines a custom template for the Breadcrumb Separator. |
    | **Size** | `string` | The size of the Breadcrumb component. |
    | **Width** | `string` | The width of the Breadcrumb component. |
- [Button](https://docs.telerik.com/blazor-ui/components/button/overview)
  - **Parameters**
    | Parameter | Type | Description |
    | --- | --- | --- |
    | **ButtonType** | `ButtonType` enum (`ButtonType.Submit`) | The `type` attribute of the Button. |
    | **Class** | `string` | The CSS class that will be rendered on the main wrapping element of the Button (`<button class="k-button>`). |
    | **Enabled** | `bool` (`true`) | Whether the Button is enabled. |
    | **Form** | `string` | The ID of the associated form. Allows using a submit button outside a form. |
    | **Id** | `string` | The `id` attribute of the Button. |
    | **Icon** | `object` | The icon rendered in the Button. Can be set to a predefined Telerik icon or a custom one. |
    | **Title** | `string` | The `title` attribute of the Button. |
    | **Visible** | `bool` (`true`) | Whether the Button is visible. |
- [Button Group](https://docs.telerik.com/blazor-ui/components/buttongroup/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | **Class** | string | Additional CSS class to the `<button class="k-button">` element. Use it to apply custom styles or override the theme. |
    | **Enabled** | bool | Whether the ButtonGroup is enabled and accepts clicks. The default value is `true`. |
    | **SelectionMode** | ButtonGroupSelectionMode | The selection mode of the ButtonGroup. The default value is `ButtonGroupSelectionMode.Single`. |
    | **Width** | string | The width of the ButtonGroup. |
- [Toggle Button](https://docs.telerik.com/blazor-ui/components/togglebutton/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | AriaLabel | string | Renders an `aria-label` HTML attribute to the button element. |
    | Class | string | Renders additional CSS class to the `<button class="k-button">` element. |
    | Enabled | bool | Determines if the button is enabled and accepts clicks. |
    | Id | string | Renders an `id` HTML attribute to the button element. |
    | TabIndex | int | Renders a `tabindex` attribute. |
    | Title | string | Renders a `title` attribute. |
- [Floating Action Button](https://docs.telerik.com/blazor-ui/components/floatingactionbutton/overview)
- [Drawer](https://docs.telerik.com/blazor-ui/components/drawer/overview)
  - **Parameters**
    | Parameter | Type and Default Value | Description |
    | --- | --- | --- |
    | **Class** | string | Renders a custom CSS class to the `<div class="k-drawer-container">` element. |
    | **Expanded** | bool | Specifies whether the Drawer is expanded or collapsed. If this parameter is used to expand or collapse the component the animations will not be available. To use animations you have to use the Drawer's Methods. It is possible, however, to use the value to implement custom layouts in the drawer templates or in your own layout. |
    | **Mode** | DrawerMode enum (Overlay) | Controls whether the Drawer is in Push or Overlay mode. |
    | **MiniMode** | bool | Controls whether there is mini view when the Drawer is collapsed. |
    | **Position** | DrawerPosition enum (Start) | Determines on which side of the DrawerContent the item list will render. |
    | **Width** | string (240px) | The width of the Drawer when expanded. |
- [DropDownButton](https://docs.telerik.com/blazor-ui/components/dropdownbutton/overview)
  - **Parameters**
    ### DropDownButton Parameters
    | Parameter | Type | Description |
    |---|---|---|
    | **AriaDescribedBy** | `string` | Sets the `aria-describedby` attribute of the primary button element. |
    | **AriaLabel** | `string` | Sets the `aria-label` attribute of the primary button element. |
    | **AriaLabelledBy** | `string` | Sets the `aria-labelledby` attribute of the primary button element. |
    | **Class** | `string` | Renders a custom CSS class to the main component element. |
    | **Enabled** | `bool` | Defines whether the primary button is enabled. The default is `true`. |
    | **Id** | `string` | Sets the `id` attribute of the primary button element. |
    | **ShowArrowButton** | `bool` | Sets the visibility of the Arrow button that displays the popup of the component. |
    | **TabIndex** | `int` | Sets the `tabindex` of the primary button element. |
    | **Title** | `string` | Sets the `title` attribute of the primary button element. |

    ### Popup Settings
    These settings are configured within the `<DropDownButtonPopupSettings>` tag.
    | Parameter | Type | Description |
    |---|---|---|
    | **AnimationDuration** | `int` | Sets the dropdown animation duration in milliseconds. The default is `300`. |
    | **Class** | `string` | An additional CSS class to customize the appearance of the popup. |
    | **Height** | `string` | The height of the popup. The default is `"200px"`. |
    | **MinHeight** | `string` | The minimum height of the popup. |
    | **MinWidth** | `string` | The minimum width of the popup. |
    | **MaxHeight** | `string` | The maximum height of the popup. |
    | **MaxWidth** | `string` | The maximum width of the popup. |
    | **Width** | `string` | The width of the popup. |

    ### Item Settings
    These are the parameters for the `<DropDownButtonItem>`.
    | Parameter | Type | Description |
    |---|---|---|
    | **Class** | `string` | Renders a custom CSS class to the dropdown item's element. |
    | **Enabled** | `bool` | Defines whether the item is enabled. The default is `true`. |
- [Menu](https://docs.telerik.com/blazor-ui/components/menu/overview)
  - **Parameters**
    ### Menu Parameters
    | Parameter | Type | Description |
    |---|---|---|
    | `Class` | `string` | Renders an additional CSS class to the main wrapping element of the component. Use it to apply custom styles or override the theme. |
    | `CloseOnClick` | `bool` | Determines whether the Menu popups should close when they are clicked. |
    | `ShowOn` | `MenuShowEvent` enum (default: `MouseEnter`) | The browser event that will trigger child Menu items to show (mouse enter or click). |
    | `HideOn` | `MenuHideEvent` enum (default: `MouseLeave`) | The browser event that will trigger child Menu items to hide (mouse leave or click). |

    ### Popup Settings
    These settings are available within a `<MenuSettings>` tag inside the main `<TelerikMenu>` component.
    | Parameter | Type | Description |
    |---|---|---|
    | `HorizontalCollision` | `PopupCollision` enum (default: `Fit`) | Sets the behavior of the Popup when it doesn't fit in the viewport based on the horizontal plane. |
    | `VerticalCollision` | `PopupCollision` enum (default: `Fit`) | Defines the behavior of the Popup when it doesn't fit in the viewport based on the vertical plane. |
- [Context Menu](https://docs.telerik.com/blazor-ui/components/contextmenu/overview)
  - **Parameters**
    ### Context Menu Parameters
    | Parameter | Type | Description |
    | --- | --- | --- |
    | Class | string | Renders an additional CSS class to the main wrapping element of the component. Use it to apply custom styles or override the theme. |
    | Selector | string | A CSS selector of the target elements where the Context Menu will be shown. |

    ### Popup Settings
    These settings are available within the `ContextMenuPopupSettings` tag.
    | Parameter | Type | Description |
    | --- | --- | --- |
    | HorizontalCollision | PopupCollision enum (Fit) | Sets the behavior of the Popup when it doesn't fit in the viewport on the horizontal plane. |
    | VerticalCollision | PopupCollision enum (Fit) | Defines the behavior of the Popup when it doesn't fit in the viewport on the vertical plane. |
- [PanelBar](https://docs.telerik.com/blazor-ui/components/panelbar/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | **Class** | `string` | The CSS class that will be rendered on the main wrapping element of the component. |
    | **ExpandedItems** | `IEnumerable<Object>` | A collection of the expanded PanelBar items. It supports two-way binding. |
    | **ExpandMode** | `PanelBarExpandMode` enum (`PanelBarExpandMode.Multiple`) | Determines if the PanelBar will allow single or multiple items to be expanded at a time. |
- [SplitButton](https://docs.telerik.com/blazor-ui/components/splitbutton/overview)
- [Stepper](https://docs.telerik.com/blazor-ui/components/stepper/overview)
  - **Parameters**
    | Parameter | Type | Description |
    | --- | --- | --- |
    | `Value` | `int` | Defines the current step index. |
    | `Orientation` | `StepperOrientation` enum (Default: `Horizontal`) | Defines the orientation of the Stepper. |
    | `Linear` | `bool` | Enables/disables linear flow. |
    | `StepType` | `StepperStepType` enum (Default: `Steps`) | Defines the display mode of the Stepper. |
- [Tab Strip](https://docs.telerik.com/blazor-ui/components/tabstrip/overview)
  - **Parameters**
    ### TabStrip Parameters
    | Parameter | Type | Description |
    | --- | --- | --- |
    | `ActiveTabIndex` | `int` | The index of the currently shown tab. Supports two-way binding. This parameter is marked as obsolete and will be deprecated in future versions. Do not use together with `ActiveTabId`. |
    | `ActiveTabId` | `int` | The index of the currently active tab. If it is not set, the first tab will be active. Do not use it together with `ActiveTabIndex`. |
    | `PersistTabContent` | `bool` | Whether to remove the content of inactive tabs from the DOM (if `false`), or just hide it with CSS (if `true`). |
    | `Scrollable` | `bool` | Whether the tabs will be scrollable. |
    | `ScrollButtonsPosition` | `TabStripScrollButtonsPosition` enum | Specifies the position of the buttons when the TabStrip is scrollable. The default is `TabStripScrollButtonsPosition.Split`. |
    | `ScrollButtonsVisibility` | `TabStripScrollButtonsVisibility` enum | Specifies the visibility of the buttons when the TabStrip is scrollable. The default is `TabStripScrollButtonsVisibility.Visible`. |
    | `Size` | `string` | Controls the size of the tabs. The default is `ThemeConstants.TabStrip.Size.Medium`. |
    | `TabPosition` | `TabPosition` enum | Controls the position of the tabs. The default is `TabPosition.Top`. |
    | `TabAlignment` | `TabStripTabAlignment` enum | Controls the alignment of the tabs. The default is `TabStripTabAlignment.Start`. |

    ### Styling and Appearance
    | Parameter | Type | Description |
    | --- | --- | --- |
    | `Class` | `string` | The CSS class that will be rendered on the main wrapping element of the component. |
    | `Width` | `string` | The width of the component. You can set the `Width` parameter to any of the supported units. |
    | `Height` | `string` | The height of the Component. You can set the `Height` parameter to any of the supported units. |
- [ToolBar](https://docs.telerik.com/blazor-ui/components/toolbar/overview)
  - **Parameters**
    | Parameter | Type | Description |
    |---|---|---|
    | Adaptive (deprecated) | bool ( true ) | Toggles the overflow popup of the ToolBar. The component displays an additional anchor on its side, where it places all items which do not fit and overflow. Template items don't participate in this mechanism and they are always rendered in the ToolBar itself. This parameter is deprecated in favor of OverflowMode . |
    | Class | string | The CSS class to be rendered on the main wrapping element of the ToolBar component, which is `<div class="k-toolbar">` . Use for styling customizations. |
    | OverflowMode | ToolBarOverflowMode ( Menu ) | The adaptive mode of the Toolbar. |
    | ScrollButtonsPosition | ToolBarScrollButtonsPosition enum ( Split ) | Specifies the position of the buttons when the ToolBar scroll adaptive mode is enabled. |
    | ScrollButtonsVisibility | ToolBarScrollButtonsVisibility enum ( Visible ) | Specifies the visibility of the buttons when the ToolBar scroll adaptive mode is enabled. |
  - **Styling and Appearance**
    | Parameter | Type | Description |
    |---|---|---|
    | Size | Telerik.Blazor.ThemeConstants.ToolBar.Size | Adjust the size of the ToolBar |
- [TreeView](https://docs.telerik.com/blazor-ui/components/treeview/overview)
- [Wizard](https://docs.telerik.com/blazor-ui/components/wizard/overview)

## Maps
- [Map](https://docs.telerik.com/blazor-ui/components/map/overview)

## Editors
- [AutoComplete](https://docs.telerik.com/blazor-ui/components/autocomplete/overview)
- [CheckBox](https://docs.telerik.com/blazor-ui/components/checkbox/overview)
- [Color Gradient](https://docs.telerik.com/blazor-ui/components/colorgradient/overview)
- [Color Palette](https://docs.telerik.com/blazor-ui/components/colorpalette/overview)
- [Color Picker](https://docs.telerik.com/blazor-ui/components/colorpicker/overview)
- [Flat Color Picker](https://docs.telerik.com/blazor-ui/components/flatcolorpicker/overview)
- [ComboBox](https://docs.telerik.com/blazor-ui/components/combobox/overview)
- [MultiColumnComboBox](https://docs.telerik.com/blazor-ui/components/multicolumncombobox/overview)
- [Date Input](https://docs.telerik.com/blazor-ui/components/dateinput/overview)
- [Date Picker](https://docs.telerik.com/blazor-ui/components/datepicker/overview)
- [Time Picker](https://docs.telerik.com/blazor-ui/components/timepicker/overview)
- [DateTime Picker](https://docs.telerik.com/blazor-ui/components/datetimepicker/overview)
- [DateRange Picker](https://docs.telerik.com/blazor-ui/components/daterangepicker/overview)
- [DropDownList](https://docs.telerik.com/blazor-ui/components/dropdownlist/overview)
- [HTML Editor](https://docs.telerik.com/blazor-ui/components/editor/overview)
- [ListBox](https://docs.telerik.com/blazor-ui/components/listbox/overview)
- [Masked Textbox](https://docs.telerik.com/blazor-ui/components/maskedtextbox/overview)
- [MultiSelect](https://docs.telerik.com/blazor-ui/components/multiselect/overview)
- [Numeric Textbox](https://docs.telerik.com/blazor-ui/components/numerictextbox/overview)
- [Radio Button Group](https://docs.telerik.com/blazor-ui/components/radiogroup/overview)
- [RangeSlider](https://docs.telerik.com/blazor-ui/components/rangeslider/overview)
- [Signature](https://docs.telerik.com/blazor-ui/components/signature/overview)
- [Slider](https://docs.telerik.com/blazor-ui/components/slider/overview)
- [Switch](https://docs.telerik.com/blazor-ui/components/switch/overview)
- [TextArea](https://docs.telerik.com/blazor-ui/components/textarea/overview)
- [TextBox](https://docs.telerik.com/blazor-ui/components/textbox/overview)
- [Validation Tools](https://docs.telerik.com/blazor-ui/components/validation/overview)

## Labels
- [FloatingLabel](https://docs.telerik.com/blazor-ui/components/floatinglabel/overview)
- [Badge](https://docs.telerik.com/blazor-ui/components/badge/overview)

## Scheduling
- [Calendar](https://docs.telerik.com/blazor-ui/components/calendar/overview)
- [Gantt](https://docs.telerik.com/blazor-ui/components/gantt/overview)
- [Scheduler](https://docs.telerik.com/blazor-ui/components/scheduler/overview)

## Charts
- [Charts Overview](https://docs.telerik.com/blazor-ui/components/charts/overview)
- [Area Charts](https://docs.telerik.com/blazor-ui/components/charts/types/area)
- [Bar Charts](https://docs.telerik.com/blazor-ui/components/charts/types/bar)
- [Bubble Charts](https://docs.telerik.com/blazor-ui/components/charts/types/bubble)
- [Candlestick Charts](https://docs.telerik.com/blazor-ui/components/charts/types/candlestick)
- [Column Charts](https://docs.telerik.com/blazor-ui/components/charts/types/column)
- [Donut Charts](https://docs.telerik.com/blazor-ui/components/charts/types/donut)
- [Heatmap Charts](https://docs.telerik.com/blazor-ui/components/charts/types/heatmap)
- [Line Charts](https://docs.telerik.com/blazor-ui/components/charts/types/line)
- [OHLC Charts](https://docs.telerik.com/blazor-ui/components/charts/types/ohlc)
- [Pie Charts](https://docs.telerik.com/blazor-ui/components/charts/types/pie)
- [Radar Area Charts](https://docs.telerik.com/blazor-ui/components/charts/types/radar-area)
- [Radar Column Charts](https://docs.telerik.com/blazor-ui/components/charts/types/radar-column)
- [Radar Line Charts](https://docs.telerik.com/blazor-ui/components/charts/types/radar-line)
- [Scatter Charts](https://docs.telerik.com/blazor-ui/components/charts/types/scatter)
- [Scatter Line Charts](https://docs.telerik.com/blazor-ui/components/charts/types/scatter-line)
- [Stock Chart](https://docs.telerik.com/blazor-ui/components/stockchart/overview)

## Gauges
- [Arc Gauge](https://docs.telerik.com/blazor-ui/components/arcgauge/overview)
- [Circular Gauge](https://docs.telerik.com/blazor-ui/components/circulargauge/overview)
- [Linear Gauge](https://docs.telerik.com/blazor-ui/components/lineargauge/overview)
- [Radial Gauge](https://docs.telerik.com/blazor-ui/components/radialgauge/overview)

## Barcodes
- [Barcode](https://docs.telerik.com/blazor-ui/components/barcode/overview)
- [QR Code](https://docs.telerik.com/blazor-ui/components/qrcode/overview)

## Layout
- [AppBar](https://docs.telerik.com/blazor-ui/components/appbar/overview)
- [Animation Container](https://docs.telerik.com/blazor-ui/components/animationcontainer/overview)
- [Card](https://docs.telerik.com/blazor-ui/components/card/overview)
- [Carousel](https://docs.telerik.com/blazor-ui/components/carousel/overview)
- [Dialog](https://docs.telerik.com/blazor-ui/components/dialog/overview)
- [Dock Manager](https://docs.telerik.com/blazor-ui/components/dockmanager/overview)
- [Form](https://docs.telerik.com/blazor-ui/components/form/overview)
- [Grid Layout](https://docs.telerik.com/blazor-ui/components/gridlayout/overview)
- [Media Query](https://docs.telerik.com/blazor-ui/components/mediaquery/overview)
- [Tile Layout](https://docs.telerik.com/blazor-ui/components/tilelayout/overview)
- [Splitter](https://docs.telerik.com/blazor-ui/components/splitter/overview)
- [Stack Layout](https://docs.telerik.com/blazor-ui/components/stacklayout/overview)
- [Window](https://docs.telerik.com/blazor-ui/components/window/overview)

## Interactivity and UX
- [AIPrompt](https://docs.telerik.com/blazor-ui/components/aiprompt/overview)
- [Loader](https://docs.telerik.com/blazor-ui/components/loader/overview)
- [LoaderContainer](https://docs.telerik.com/blazor-ui/components/loadercontainer/overview)
- [Skeleton](https://docs.telerik.com/blazor-ui/components/skeleton/overview)
- [Notification](https://docs.telerik.com/blazor-ui/components/notification/overview)
- [Progress Bar](https://docs.telerik.com/blazor-ui/components/progressbar/overview)
- [Chunk Progress Bar](https://docs.telerik.com/blazor-ui/components/chunkprogressbar/overview)
- [Popover](https://docs.telerik.com/blazor-ui/components/popover/overview)
- [Tooltip](https://docs.telerik.com/blazor-ui/components/tooltip/overview)
- [Popup](https://docs.telerik.com/blazor-ui/components/popup/overview)

## Documents
- [Document Processing](https://docs.telerik.com/blazor-ui/framework/document-processing/overview)
- [PdfProcessing](https://docs.telerik.com/blazor-ui/framework/document-processing/pdfprocessing/overview)
- [SpreadProcessing](https://docs.telerik.com/blazor-ui/framework/document-processing/spreadprocessing/overview)
- [SpreadStreamProcessing](https://docs.telerik.com/blazor-ui/framework/document-processing/spreadstreamprocessing/overview)
- [WordsProcessing](https://docs.telerik.com/blazor-ui/framework/document-processing/wordsprocessing/overview)
- [ZipLibrary](https://docs.telerik.com/blazor-ui/framework/document-processing/ziplibrary/overview)
