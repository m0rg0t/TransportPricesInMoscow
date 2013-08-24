using TransportPricesInMoscow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TransportPricesInMoscow
{
    class VariableSizeGridView : GridView
    {
        private int rowVal;
        private int colVal;

        /*protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            try
            {
                SampleDataItem dataItem = item as SampleDataItem;

                //int group = -1;
                //if (dataItem.Group.UniqueId == "MainNews")
                //{
                //    group = 1;
                //};

                colVal = 1;
                rowVal = 1;

                VariableSizedWrapGrid.SetRowSpan(element as UIElement, rowVal);
                VariableSizedWrapGrid.SetColumnSpan(element as UIElement, colVal);
            }
            catch { };
            base.PrepareContainerForItemOverride(element, item);
        }*/
    }
}
