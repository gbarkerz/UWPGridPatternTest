using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace GridTest
{
    public sealed partial class UserGridControl : UserControl
    {
        private TestViewModel viewModel;

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
            }
        }

        private void TestGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When an item becomes selected in the grid, move focus to the inner element
            // which supports the UIA GridItem pattern.
            
            GridViewItem selectedGridViewItem = null;

            var grid = sender as GridView;
            if (grid != null)
            {
                var selectedGridItem = grid.SelectedItem as TestItem;
                if (selectedGridItem != null)
                {
                    selectedGridViewItem = TestGridView.GridViewDemo.ContainerFromItem(selectedGridItem) as GridViewItem;
                }
            }

            TestGridViewItem testGridViewItem = null;
            if (selectedGridViewItem != null)
            {
                // Find the inner Test grid item which supports the UIA GridItem patten.
                var selectedGridViewItemChildrenCount = VisualTreeHelper.GetChildrenCount(selectedGridViewItem);
                if (selectedGridViewItemChildrenCount > 0)
                {
                    var listViewItemPresenter = VisualTreeHelper.GetChild(selectedGridViewItem, 0);
                    if (listViewItemPresenter != null)
                    {
                        var listViewItemPresenterChildrenCount = VisualTreeHelper.GetChildrenCount(listViewItemPresenter);
                        if (listViewItemPresenterChildrenCount > 0)
                        {
                            testGridViewItem = VisualTreeHelper.GetChild(listViewItemPresenter, 0) as TestGridViewItem;
                        }
                    }
                }
            }

            if (testGridViewItem != null)
            {
                testGridViewItem.Focus(FocusState.Programmatic);
            }
        }
    }
}
