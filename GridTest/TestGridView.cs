using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace GridTest
{
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

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new TestGridViewAutomationPeer(this);
        }

        // PrepareContainerForItemOverride() is very handy for setting specific AutomationProperties 
        // properties on the UIA element at the root of each item.
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            // Users are not intended to often encounter this element. Rather all the useful
            // data is accessible through its child TestGridViewItem. So give this element
            // a generic name which relates to its purpose.
            AutomationProperties.SetName(element, "Test Square");
        }
    }

    public class TestGridViewAutomationPeer : GridViewAutomationPeer, IGridProvider
    {
        public TestGridViewAutomationPeer(TestGridView owner) : base(owner)
        {
        }

        protected override string GetNameCore()
        {
            return "Test";
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

        // UIS Grid Pattern support for accessing a specific item is not yet implemented.
        public IRawElementProviderSimple GetItem(int row, int column)
        {
            return null;
        }
    }

    public class TestGridViewItem : GridViewItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new TestGridViewItemAutomationPeer(this);
        }
    }

    public class TestGridViewItemAutomationPeer : GridViewItemAutomationPeer, IGridItemProvider
    {
        private TestGridViewItem gridViewItem;

        public TestGridViewItemAutomationPeer(TestGridViewItem owner) : base(owner)
        {
            gridViewItem = owner;
        }

        protected override string GetNameCore()
        {
            var item = (gridViewItem.DataContext as TestItem);

            return (item.NumberShown ? item.Name: "No number shown");
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }

        protected override string GetLocalizedControlTypeCore()
        {
            return "Square";
        }

        protected override object GetPatternCore(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.GridItem)
            {
                return this;
            }

            return base.GetPatternCore(patternInterface);
        }

        public int Row { get => ((gridViewItem.DataContext as TestItem).Index % TestGridView.RowColumnCount); }
        public int Column { get => ((gridViewItem.DataContext as TestItem).Index / TestGridView.RowColumnCount); }

        public int RowSpan { get => 1; }
        public int ColumnSpan { get => 1; }

        public IRawElementProviderSimple ContainingGrid
        {
            get
            {
                IRawElementProviderSimple reps = null;

                var peer = FrameworkElementAutomationPeer.FromElement(TestGridView.GridViewDemo);
                if (peer != null)
                {
                    reps = this.ProviderFromPeer(peer);
                }

                return reps;
            }
        }
    }
}
