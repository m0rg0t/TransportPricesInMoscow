using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using TransportPricesWP.ViewModel;
using Telerik.Windows.Data;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Tasks;

namespace TransportPricesWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            App.button.IsAppBarEnabled = true;
            App.button.Open();

            // Set the data context of the listbox control to the sample data
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            this.ItemsList.GroupDescriptors.Add(GroupedBadgesList);
            this.ItemsList.SortDescriptors.Add(Sort);

            this.SearchItemsList.GroupDescriptors.Add(GroupedBadgesList);
            this.SearchItemsList.SortDescriptors.Add(Sort);

            
        }

        public GenericGroupDescriptor<PriceItem, string> GroupedBadgesList = new GenericGroupDescriptor<PriceItem, string>(item => item.GroupName);
        public GenericSortDescriptor<PriceItem, string> Sort = new GenericSortDescriptor<PriceItem, string>(item => item.GroupName);


        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainPanorama.DefaultItem = MainPanorama.Items[0];
            if (ViewModelLocator.MainStatic.Items.Count < 1)
            {
                //if (!App.ViewModel.IsDataLoaded)
                //{
                ViewModelLocator.MainStatic.LoadData();
            };
            //}
        }

        /// <summary>
        /// Добавить оценку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RateAppMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var marketplaceReviewTask = new MarketplaceReviewTask();
                marketplaceReviewTask.Show();
            }
            catch
            {
            };
        }

        /// <summary>
        /// Политика конфиденциальности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrivacyMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var messagePrompt = new MessagePrompt
                {
                    Title = "Политика конфиденциальности",
                    Body = new TextBlock
                    {
                        Text = "Приложение не собирает никаких данных без вашего ведома.\nПриложение не собирает и не хранит информацию, которая связана с определенным именем. Мы также делаем все возможное, чтобы обезопасить хранимые данные.\nПринимая условия, которые включают эту политику вы соглашаетесь с данной политикой конфиденциальности.\nКонтакты m0rg0t.Anton@gmail.com",
                        MaxHeight = 500,
                        TextWrapping = TextWrapping.Wrap
                    },
                    IsAppBarVisible = false,
                    IsCancelVisible = false
                };
                messagePrompt.Show();
            }
            catch { };
        }


        private void AboutTile_Tap(object sender, GestureEventArgs e)
        {
            try
            {
                var messagePrompt = new MessagePrompt
                {
                    Title = "О приложении",
                    Body = new TextBlock
                    {
                        Text = "Просмотр цен на различные виды общественного транспорта Москвы на основе данных единой мобильной платформы (ЕМП) Москвы",
                        MaxHeight = 500,
                        TextWrapping = TextWrapping.Wrap
                    },
                    IsAppBarVisible = false,
                    IsCancelVisible = false
                };
                messagePrompt.Show();
            }
            catch { };
        }

        private void SearchTile_Tap(object sender, GestureEventArgs e)
        {
            try
            {
                //NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.Relative));
                InputPrompt input = new InputPrompt();
                input.Completed += input_Completed;
                input.Title = "Поиск";
                input.Message = "ВВедите текст для поиска:";
                input.Show();
            }
            catch { };
        }

        private void input_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            try
            {
                ViewModelLocator.MainStatic.SearchQuery = e.Result.ToString();
                MainPanorama.DefaultItem = MainPanorama.Items[2];
            }
            catch { };
        }

        private void PricesTile_Tap(object sender, GestureEventArgs e)
        {
            try
            {
                MainPanorama.DefaultItem = MainPanorama.Items[1];
            }
            catch { };
        }
    }
}