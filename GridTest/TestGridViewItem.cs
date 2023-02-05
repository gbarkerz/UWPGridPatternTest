using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;

namespace GridTest {
    public class TestGridViewItem : GridViewItem {
        protected override AutomationPeer OnCreateAutomationPeer() {
            return new TestGridViewItemAutomationPeer(this);
        }
    }
}
