using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransportPricesInMoscow.Data;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента контракта "Поиск" задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234240

namespace TransportPricesInMoscow
{
    // TODO: Изменить манифест, включив поиск
    //
    // Манифест пакета невозможно обновить автоматически.  Откройте файл манифеста
    // пакета и проверьте, что включена поддержка активации поиска.

    // TODO: Ответить на запрос активации для результатов поиска
    //
    // Следующий код не удалось автоматически добавить в подкласс приложения
    // из-за невозможности найти соответствующий класс или из-за существования метода с
    // таким же именем.  Убедитесь, что соответствующий код поддерживает активацию,
    // отобразив результаты поиска для указанного поискового запроса.
    //
    //         /// <summary>
    //         /// Вызывается при активации приложения для отображения результатов поиска.
    //         /// </summary>
    //         /// <param name="args">Сведения о запросе на активацию.</param>
    //         protected async override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
    //         {
    //             // TODO: Регистрация события Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().QuerySubmitted
    //             // в OnWindowCreated для ускорения поиска во время выполнения приложения
    // 
    //             // Если в окне еще не используется навигация по фреймам, вставьте собственный фрейм
    //             var previousContent = Window.Current.Content;
    //             var frame = previousContent as Frame;
    // 
    //             // Если приложение не содержит фрейм верхнего уровня, то, возможно, это
    //             // первый запуск приложения. Обычно этот метод и метод OnLaunched 
    //             // из файла App.xaml.cs могут вызывать общий метод.
    //             if (frame == null)
    //             {
    //                 // Создание фрейма, играющего роль контекста навигации, и его связывание с
    //                 // ключом SuspensionManager
    //                 frame = new Frame();
    //                 TransportPricesInMoscow.Common.SuspensionManager.RegisterFrame(frame, "AppFrame");
    // 
    //                 if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
    //                 {
    //                     // Восстановление сохраненного состояния сеанса только при необходимости
    //                      try
    //                     {
    //                         await TransportPricesInMoscow.Common.SuspensionManager.RestoreAsync();
    //                     }
    //                     catch (TransportPricesInMoscow.Common.SuspensionManagerException)
    //                     {
    //                         //Возникли ошибки при восстановлении состояния.
    //                         //Предполагаем, что состояние отсутствует, и продолжаем
    //                     }
    //                 }
    //             }
    // 
    //             frame.Navigate(typeof(SearchResultsPage), args.QueryText);
    //             Window.Current.Content = frame;
    // 
    //             // Обеспечение активности текущего окна
    //             Window.Current.Activate();
    //         }
    /// <summary>
    /// На этой странице отображаются результаты поиска, когда в данное приложение направляются результаты глобального поиска.
    /// </summary>
    public sealed partial class SearchResultsPage : TransportPricesInMoscow.Common.LayoutAwarePage
    {

        public SearchResultsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Заполняет страницу содержимым, передаваемым в процессе навигации. Также предоставляется любое сохраненное состояние
        /// при повторном создании страницы из предыдущего сеанса.
        /// </summary>
        /// <param name="navigationParameter">Значение параметра, передаваемое
        /// <see cref="Frame.Navigate(Type, Object)"/> при первоначальном запросе этой страницы.
        /// </param>
        /// <param name="pageState">Словарь состояния, сохраненного данной страницей в ходе предыдущего
        /// сеанса. Это значение будет равно NULL при первом посещении страницы.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var queryText = navigationParameter as String;

            // TODO: Логика поиска, зависящая от приложения. Процесс поиска отвечает за
            //       создание списка выбираемых пользователем категорий результатов:
            //
            //       filterList.Add(new Filter("<filter name>", <result count>));
            //
            //       Только первый фильтр, как правило "Все", должен передавать значение true в качестве третьего аргумента,
            //       чтобы запускаться в активном состоянии.  Результаты для активного фильтра представлены ниже в
            //       Filter_SelectionChanged.

            var filterList = new List<Filter>();
            filterList.Add(new Filter("Все", 0, true));

            //var results = from item in SampleDataSource._sampleDataSource.AllGroups;
            var itemList = new List<SampleDataItem>();
            foreach (var groupData in SampleDataSource._sampleDataSource.AllGroups)
            {
                var groupResults = from item in groupData.Items
                                   where item.Title.ToLower().Contains(queryText.ToLower())
                                    || item.Description.ToLower().Contains(queryText.ToLower())
                                    || item.Subtitle.ToLower().Contains(queryText.ToLower())
                                   select item;
                foreach (var item1 in groupResults)
                {
                    itemList.Add(item1);
                };
            };

            this.DefaultViewModel["Results"] = itemList;

            // Передавать результаты через модель представлений
            this.DefaultViewModel["QueryText"] = '\u201c' + queryText + '\u201d';
            this.DefaultViewModel["Filters"] = filterList;
            this.DefaultViewModel["ShowFilters"] = filterList.Count > 1;
        }

        /// <summary>
        /// Вызывается при выборе фильтра с помощью поля со списком в состоянии прикрепленного представления.
        /// </summary>
        /// <param name="sender">Экземпляр ComboBox.</param>
        /// <param name="e">Данные о событии, описывающие, каким образом был изменен выбранный фильтр.</param>
        void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Определить, какой фильтр был выбран
            var selectedFilter = e.AddedItems.FirstOrDefault() as Filter;
            if (selectedFilter != null)
            {
                // Зеркальное отображение результатов в соответствующий объект Filter, чтобы представление
                // RadioButton, используемое без прикрепления, могло отразить изменение
                selectedFilter.Active = true;

                // TODO: Ответить на изменение в активном фильтре, задав для this.DefaultViewModel["Results"]
                //       коллекцию элементов с привязываемыми свойствами Image, Title, Subtitle и Description

                // Убедитесь, что результаты найдены
                object results;
                ICollection resultsCollection;
                if (this.DefaultViewModel.TryGetValue("Results", out results) &&
                    (resultsCollection = results as ICollection) != null &&
                    resultsCollection.Count != 0)
                {
                    VisualStateManager.GoToState(this, "ResultsFound", true);
                    return;
                }
            }

            // Отображение информационного текста, который выводится при отсутствии результатов поиска.
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

        /// <summary>
        /// Вызывается, если фильтр выбирается с помощью RadioButton без прикрепления.
        /// </summary>
        /// <param name="sender">Выбранный экземпляр RadioButton.</param>
        /// <param name="e">Данные о событии, описывающие, каким образом было выбрано значение RadioButton.</param>
        void Filter_Checked(object sender, RoutedEventArgs e)
        {
            // Зеркальное отражение изменений в объект CollectionViewSource, используемый соответствующим элементом ComboBox,
            // чтобы при прикреплении изменения отражались
            if (filtersViewSource.View != null)
            {
                var filter = (sender as FrameworkElement).DataContext;
                filtersViewSource.View.MoveCurrentTo(filter);
            }
        }

        /// <summary>
        /// Просмотр модели, описывающей один или несколько фильтров, доступных для просмотра результатов поиска.
        /// </summary>
        private sealed class Filter : TransportPricesInMoscow.Common.BindableBase
        {
            private String _name;
            private int _count;
            private bool _active;

            public Filter(String name, int count, bool active = false)
            {
                this.Name = name;
                this.Count = count;
                this.Active = active;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Name
            {
                get { return _name; }
                set { if (this.SetProperty(ref _name, value)) this.OnPropertyChanged("Description"); }
            }

            public int Count
            {
                get { return _count; }
                set { if (this.SetProperty(ref _count, value)) this.OnPropertyChanged("Description"); }
            }

            public bool Active
            {
                get { return _active; }
                set { this.SetProperty(ref _active, value); }
            }

            public String Description
            {
                get { return String.Format("{0} ({1})", _name, _count); }
            }
        }
    }
}
