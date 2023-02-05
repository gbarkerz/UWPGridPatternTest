using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;

namespace GridTest {
    public class TestGridViewAutomationPeer : GridViewAutomationPeer, IGridProvider
    {
        public TestGridViewAutomationPeer(TestGridView owner) : base(owner)
        {
        }

        protected override string GetNameCore()
        {
            return "Sudoku";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }

        protected override string GetLocalizedControlTypeCore()
        {
            return "Grid";
        }

        protected override object GetPatternCore(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Grid)
            {
                return this;
            }

            return base.GetPatternCore(patternInterface);
        }

        // UIA Grid pattern properties for the row and column counts in the grid.
        public int RowCount { get => TestGridView.RowColumnCount; }
        public int ColumnCount { get => TestGridView.RowColumnCount; }

        // UIA Grid Pattern support for accessing a specific item is not yet implemented.
        public IRawElementProviderSimple GetItem(int row, int column)
        {
            return null;
        }
    }
}
