using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportPricesWP.ViewModel
{
    public class PriceItem: ViewModelBase
    {
        public PriceItem()
        {
        }

        private string _title;
        /// <summary>
        /// Название
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private string _description;
        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { 
                _description = value;
                RaisePropertyChanged("Description");
            }
        }
        

        private string _price;
        /// <summary>
        /// Цена билета
        /// </summary>
        public string Price
        {
            get { return _price; }
            set { 
                _price = value;
                RaisePropertyChanged("Price");
            }
        }

        private string _groupName;
        /// <summary>
        /// Имя группы
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
            set { 
                _groupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        private string _shortGroupName;
        /// <summary>
        /// Имя группы краткое
        /// </summary>
        public string ShortGroupName
        {
            get { return _shortGroupName; }
            set
            {
                _shortGroupName = value;
                RaisePropertyChanged("ShortGroupName");
            }
        }
        
        
    }
}
