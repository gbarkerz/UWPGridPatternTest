using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;

namespace GridTest {
    public class TestGridViewItemAutomationPeer : GridViewItemAutomationPeer, IGridItemProvider
    {
        private readonly TestGridViewItem _gridViewItem;

        public TestGridViewItemAutomationPeer(TestGridViewItem owner) : base(owner) {
            _gridViewItem = owner;
        }

        protected override string GetNameCore() {
            var item = _gridViewItem.DataContext as TestItem;
            return item.NumberShown ? item.Name : "No number shown";
        }

        protected override AutomationControlType GetAutomationControlTypeCore() {
            return AutomationControlType.Custom;
        }

        protected override string GetLocalizedControlTypeCore() {
            return "Cell";
        }

        protected override object GetPatternCore(PatternInterface patternInterface) {
            if (patternInterface == PatternInterface.GridItem) {
                return this;
            }

            return base.GetPatternCore(patternInterface);
        }

        public int Row { get => (_gridViewItem.DataContext as TestItem).X; }
        public int Column { get => (_gridViewItem.DataContext as TestItem).Y; }

        public int RowSpan { get => 1; }
        public int ColumnSpan { get => 1; }

        public IRawElementProviderSimple ContainingGrid {
            get {
                IRawElementProviderSimple reps = null;

                var peer = FrameworkElementAutomationPeer.FromElement(TestGridView.GridViewDemo);
                if (peer != null) {
                    reps = this.ProviderFromPeer(peer);
                }

                return reps;
            }
        }
    }
}
