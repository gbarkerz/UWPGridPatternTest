using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;

namespace GridTest {
    class TestGridDataTemplateSelector : DataTemplateSelector {

        public DataTemplate CellDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            TestItem ti = item as TestItem;
            GridViewItem gvi = container as GridViewItem;
            gvi.DataContext = ti;
            return CellDataTemplate;
        }
    }
}
