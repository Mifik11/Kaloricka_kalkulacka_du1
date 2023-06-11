using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kaloricka_kalkulacka_du1.ViewModels
{
    public class AddNewFoodVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _food;
        private double _proteinnw;
        private double _carbohydratesnw;
        private double _sugarnw;
        private double _fatnw;
        public string food
        {
            get => _food;
            set
            {
                _food = value;
                OnPropertyChanged("food");
                OnPropertyChanged("Status");
            }
        }
        public double proteinnw
        {
            get => _proteinnw;
            set
            {
                _proteinnw = value;
                OnPropertyChanged("proteinnw");
                OnPropertyChanged("Status");
            }
        }
        public double carbohydratesnw
        {
            get => _carbohydratesnw;
            set
            {
                _carbohydratesnw = value;
                OnPropertyChanged("carbohydratesnw");
                OnPropertyChanged("Status");
            }
        }
        public double sugarnw
        {
            get => _sugarnw;
            set
            {
                _sugarnw = value;
                OnPropertyChanged("sugarnw");
                OnPropertyChanged("Status");
            }
        }
        public double fatnw
        {
            get => _fatnw;
            set
            {
                _fatnw = value;
                OnPropertyChanged("fatnw");
                OnPropertyChanged("Status");
            }
        }
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public string Status
        {
            get => $"{food} {proteinnw} {carbohydratesnw} {sugarnw} {fatnw}";
        }
        public override string ToString()
        {
            return $"{food} {proteinnw} {carbohydratesnw} {sugarnw} {fatnw}";
        }
        public AddNewFoodVM()
        {
            
        }
        
    }
}