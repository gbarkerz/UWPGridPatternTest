using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using System.Collections.Specialized;

namespace GridTest {
    public class TestGridViewAutomationPeer : GridViewAutomationPeer, IGridProvider
    {
        private TestGridView _testGridView;

        public TestGridViewAutomationPeer(TestGridView owner) : base(owner)
        {
            _testGridView = owner;
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
            IRawElementProviderSimple reps = null;

            var itemIndex = (row * TestGridView.RowColumnCount) + column;
            if ((itemIndex >= 0) && (itemIndex < _testGridView.Items.Count))
            {
                var item = _testGridView.Items[itemIndex];
                var itemContainer = _testGridView.ContainerFromItem(item);

                var itemContainerPeer = FrameworkElementAutomationPeer.FromElement(itemContainer as UIElement);
                if (itemContainerPeer != null)
                {
                    reps = this.ProviderFromPeer(itemContainerPeer);
                }
            }

            return reps;
        }
    }
}
