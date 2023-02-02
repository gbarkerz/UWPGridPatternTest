using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GridTest
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            TestGrid.Refresh();
        }
    }
}
