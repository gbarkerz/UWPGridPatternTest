# UWPGridPatternTest

*Important: This demo app has been made possible thanks to Fons Sonnemans, who provided all the code to override the default container for GridViewItems with a custom item which could provide screen readers with the appropriate UI Automation properties and patterns.*


For grid UI to be accessible, a screen reader must be able to announce the row and column data associated with the items that are encountered as users navigate through the grid. Technically that row and column data could be embedded in the name or help details for each item in the grid, but on Windows, the correct way of supporting accessibility is for the grid to support the UI Automation (UIA) Grid pattern, and each item to support the UIA GridItem pattern. 

At the time I write this, is seems that the UWP GridView control does not support the UIA Grid pattern, and neither do any of the other UWP container controls that present a grid-like layout of items. This UWP XAML app demonstrates how support for the UIA Grid pattern can be added to UWP GridView UI, and support for the UIA GridItem pattern can be added to the items in that GridView.

UWP XAML has considerable support for accessibility, including options for adding support for specific UIA patterns to UI elements. In this app, a GridView is contained within a UserControl presented on the main page of the app. In order to add support for the UIA Grid pattern to the GridView, a new GridView-derived TestGridView class has its OnCreateAutomationPeer() overridden to provide a new GridViewAutomationPeer-derived TestGridViewAutomationPeer. That TestGridViewAutomationPeer declares itself as supporting the UIA Grid pattern, and implements (most of) the members of the IGridPattern interface. It also overrides a few properties to return details that more closely match this control's true purpose then the GridView's default values.

Note that while strings in this demo code are hard-coded English, a shipping app would localise all the strings presented to the user regardless of whether they're presented visually or via a screen reader.

The following image shows the Accessibility Insights for Windows tool reporting that the running TestGridView control supports the UIA Grid pattern, and through that pattern the control declares that the grid has 9 rows and 9 columns.

![image Alt="Accessibility Insights reporting the UIA details for the grid."](https://user-images.githubusercontent.com/77085891/216817057-54397d00-421b-4d93-9a5b-6434412eaee4.png)


A similar approach is taken when adding support for the UIA GridViewItem pattern to the GridView items in the grid. Importantly, action is taken to override the default container for the items in the GridView, and once that's done, a custom TestGridViewItemAutomationPeer is used to override the default accessibility of the items.

The following image shows the Accessibility Insights for Windows tool reporting the UIA hierarchy of the grid UI in the demo app. The grid is a parent of multiple items whose LocalizedControlType is "Cell" and whose names are either the number showing visually on the square, or "No number shown". Accessibility Insights is highlighting one of those items, and shows that it supports the UIA GridItem pattern, which declares that the item is in row 1 and column 5.

![image Alt="Accessibility Insights reporting the UIA details for the items in the grid."](https://user-images.githubusercontent.com/77085891/216817067-2c6dcf43-d201-40b2-9d82-e085eda3be25.png)


Below are some notes related to the behavior of the grid and its items in this demo app.

* Keyboard navigation through the items via arrow key use, and a press of Space or Enter will trigger the action on an item in the grid.

* The UIA hierarchy has all the items as direct children of the grid, and the items contain no other distracting elements.

* The Grid and its items have an appropriate UIA Name, ControlType, and LocalizedControlType.

* The Grid supports the UIA Grid pattern, and the items support the UIA GridItem pattern. Note that because the items' row and column data can be accessed through the UIA GridItem pattern, that data is not embedded in another UIA property for the item. When Narrator is at an item in the grid, a press of Shift+Ctrl+Alt+ForwardSlash will have the item's row and column data announced.

* Screen readers may choose to announce the index of the item in the grid and the number of the items in the grid, but typically app's do not try to take action to influence that behaviour. Narrator will announce that information if its verbosity level is 3 or higher.

* When an item's action is triggered, the app raises a UIA Notification event to have a screen reader announce the result of that action. Without that Notification event, a screen reader might not announce anything when the action is taken.

* The grid intentionally has its items invokable but not selectable. This means when using Narrator touch, if a double-tap gesture is made, then Narrator will invoke the item rather than select it. As such, the item's action is triggered following the double-tap gesture.

* Because all the items are unique UIA elements, the Windows Speech Recognition's "Show Numbers" feature can highlight and invoke each item.


The following image shows the NVDA Speech Viewer feature showing the announcements made by NVDA as it moves between items in the grid. The announcements includes the number shown on the item, or "No number shown" if there is no number shown. It also includes the row and column data for item, and its index in the set of items. NVDA also announces the notification made by the app when action is trigged on the item.

![image Alt="The NVDA Speech Viewer showing announcements made by NVDA."](https://user-images.githubusercontent.com/77085891/216817078-c7e151d1-4e4e-4649-9d59-be947555f1d7.png)


The following image shows the Windows Speech Recognition feature showing a number over all the invokable elements in the app, including all the grid items.

![image Alt="Windows Speech Recognition showing numbers of elements in the app."](https://user-images.githubusercontent.com/77085891/216817086-9b0c9e25-d98c-4bab-9d71-326e7921c2aa.png)
