# UWPGridPatternTest

Disclaimer: Some (or all) of what I say below may be wrong.

For grid UI to be accessible, a screen reader must be able to announce the row and column data associated with the items that are encountered as users navigate through the grid. Technically that row and column data could be embedded in the name or help details for each item in the grid, but on Windows, the correct way of supporting accessibility is for the grid to support the UI Automation (UIA) Grid pattern, and each item to support the UIA GridItem pattern. 

At the time I write this, is seems that the UWP GridView control does not support the UIA Grid pattern, and neither do any of the other UWP container controls that present a grid-like layout of items. This UWP XAML app explores how support for the UIA Grid pattern might be added to UWP GridView UI. I would not recommend a shipping app takes the action this test app does, but maybe someone knows of a more robust way for UWP XAML apps to achieve these goals.

UWP XAML has considerable support for accessibility, including options for adding support for specific UIA patterns to UI elements. In this app, a GridView is contained within a UserControl presented on the main page of the app. In order to add support for the UIA Grid pattern to the GridView, a new GridView-derived TestGridView class has its OnCreateAutomationPeer() overridden to provide a new GridViewAutomationPeer-derived TestGridViewAutomationPeer. That TestGridViewAutomationPeer declares itself as supporting the UIA Grid pattern, and implements (most of) the members of the IGridPattern interface. It also overrides a few properties to return details that more closely match this control's true purpose then the GridView's default values.

Note that while strings in this demo code are hard-coded English, a shipping app would localise all the strings presented to the user regardless of whether they're presented visually or via a screen reader.

The following image shows the Accessibility Insights for Windows tool reporting that the running TestGridView control supports the UIA Grid pattern, and through that pattern the control declares that it the grid has 9 rows and 9 columns.

<img width="875" alt="Accessibility Insights for Windows reporting that a custom GridView control supports the UIA Grid pattern." src="https://user-images.githubusercontent.com/77085891/216389825-c8b21662-2c58-46c7-b036-461fe8ff67ed.png">


A similar approach is taken to adding support for the UIA GridViewItem pattern to the GridView items in the grid, with one important caveat. The items defined in a GridView have a container element created by UWP XAML to host the item data layout. It is that container element that needs to have support for the UIA GridItem pattern. I was unable to find a way to achieve that, but maybe someone more expert in UWP XAML that I would know of a way. (I'm not aware of away of using GetContainerForItemOverride() to get the default container for an item and then adding support for the UIA GridItem pattern to that container.) So for this demo app, the item contains a GridViewItem-derived item which supports the UIA GridItem pattern. These inner items are made tabbable, and whenever an item in the grid is selected (such as when arrowing to an item in the grid), action is taken to move focus to the inner item which supports the UIA GridItem pattern.


The end result is that as users navigate through the grid, focus moves to the new item which supports the UIA GridItem pattern. The UIA Name property for that item is customised to announce either a number shown visually on the item, or a general string saying that no number is shown. Importantly, if the user requests that the row and column details for an item should be announced, that is now supported.

The following image shows the Accessibility Insights for Windows tool reporting the UIA hierarchy of the grid UI in the demo app. The grid is a parent of multiple list items whose names are all "Test Square", and each of those list items is a parent of an element whose LocalizedControlType is "Square" and whose names are either the number showing visually on the square, or "No number shown". Accessibility Insights is highlighting one of those squares, and shows that it supports the UIA GridItem pattern, which declares that the square is in row 4 and column 0.

<img width="935" alt="Accessibility Insights for Windows reporting that a custom GridViewItem control supports the UIA GridItem pattern." src="https://user-images.githubusercontent.com/77085891/216389882-ff0aae41-49b3-442b-87e5-48da1f3426a6.png">


The end result is that a screen reader can navigate through the items in the grid and announce custom UIA Name properties set on the items, and also the row and column details associated with the items.

The following image shows the NVDA Speech Viewer feature with the announcements made by NVDA as I arrow through the grid. The announcements for each item start with either the number shown visually on the item, or "No number shown". The announcements end with the row and column indices associated with the item.

<img width="577" alt="The NVDA Speech Viewer feature showing the announcements made by NVDA as focus moves around a grid." src="https://user-images.githubusercontent.com/77085891/216389931-962c4512-5833-4da9-80d8-8b059628c1ad.png">
