using Kaloricka_kalkulacka_du1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kaloricka_kalkulacka_du1.ViewModels
{    
    public class MainWindowVM : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;
        public static List<MainWindowVM> AllFood { get; set; } = new List<MainWindowVM>();
        private string _foodname;
        private double _amount;
        public Guid _ID { get; set; }
        public static MainWindowVM Copy(MainWindowVM p)
        {
            MainWindowVM q = new MainWindowVM()
            {
                amount = p.amount,
                foodname = p.foodname,
                _ID = Guid.NewGuid(),
            };
            return q;
        }
        public static void Clear(MainWindowVM p)
        {
            p.foodname = string.Empty;
            p.amount = 0;
        }
        public string foodname
        {
            get => _foodname;
            set
            {
                _foodname = value;
                OnPropertyChanged("foodname");
                OnPropertyChanged("Status");
            }
        }
        public double amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged("amount");
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
            get => $"{foodname} {amount}";
        }
        public override string ToString()
        {
            return $"{foodname} {amount} {_ID}";
        }
        public MainWindowVM()
        {

        }
        
    }
}

