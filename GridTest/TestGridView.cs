using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace GridTest {
    public class TestGridView : GridView
    {
        // Note: The code through this demo app assumes a grid with fixed column and row counts of 9x9.
        static public int RowColumnCount = 9;

        // For this demo code, use a static for quick access to the GridView.
        static public TestGridView GridViewDemo;

        public TestGridView()
        {
            TestGridView.GridViewDemo = this;
        }

        protected override DependencyObject GetContainerForItemOverride() {
            return new TestGridViewItem();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new TestGridViewAutomationPeer(this);
        }
    }
}
