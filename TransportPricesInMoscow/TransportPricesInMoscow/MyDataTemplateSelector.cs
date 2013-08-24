using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportPricesInMoscow.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TransportPricesInMoscow
{
    class MyDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }
        //NewsItemTemplate

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            try
            {
                SampleDataItem dataItem = item as SampleDataItem;

                if (dataItem.Group.Title.Contains("Универсальные проездные билеты"))
                //dataItem.Group.UniqueId.Contains("http://rybinsk.ru/news-2013?format=feed") || 
                {
                    return Template2;
                }
                else
                {
                    return Template1;
                };
            }
            catch {
                return Template1;
            };
        }
    }
}
