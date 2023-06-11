using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kaloricka_kalkulacka_du1.ViewModels
{
    public class ChangeLimitVM : INotifyPropertyChanged
    {
        public ChangeLimitVM()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private double _protein;
        private double _carbohydrates;
        private double _sugar;
        private double _fat;
        public double protein
        {
            get => _protein;
            set
            {
                _protein = value;
                OnPropertyChanged("protein");
                OnPropertyChanged("Status");
            }
        }
        public double carbohydrates
        {
            get => _carbohydrates;
            set
            {
                _carbohydrates = value;
                OnPropertyChanged("protein");
                OnPropertyChanged("Status");
            }
        }
        public double sugar
        {
            get => _sugar;
            set
            {
                _sugar = value;
                OnPropertyChanged("protein");
                OnPropertyChanged("Status");
            }
        }
        public double fat
        {
            get => _fat;
            set
            {
                _fat = value;
                OnPropertyChanged("protein");
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
            get => $"{protein} {carbohydrates} {sugar} {fat}";
        }
        public override string ToString()
        {
            return $"{protein} {carbohydrates} {sugar} {fat}";
        }
    }
}