using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Automation.Peers;

namespace GridTest
{
    public sealed partial class UserGridControl : UserControl
    {
        private TestViewModel viewModel;
        private string demoNotificationId = "33188EE9-B3C7-4B76-9968-938A7E424FDC";

        public UserGridControl()
        {
            this.InitializeComponent();

            viewModel = new TestViewModel();
            TestCVS.Source = viewModel.TestListCollection;
        }

        public void Refresh()
        {
            viewModel.Refresh();
        }

        private void TestGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as TestItem;
            if (item != null)
            {
                item.NumberShown = !item.NumberShown;

                // Because keyboard focus doesn't change during this operation, by default a 
                // screen reader won't announce anything at all. As such, raise an event in 
                // order to encourage screen readers to announce the result of the action.

                var itemContainer = (sender as TestGridView).ContainerFromItem(item);

                var itemContainerPeer = FrameworkElementAutomationPeer.FromElement(itemContainer as UIElement);
                if (itemContainerPeer != null)
                {
                    var msg = "Now " + itemContainerPeer.GetName();

                    itemContainerPeer.RaiseNotificationEvent(
                        AutomationNotificationKind.ActionCompleted, 
                        AutomationNotificationProcessing.ImportantMostRecent,
                        msg,
                        demoNotificationId);
                }
            }
        }
    }
}
