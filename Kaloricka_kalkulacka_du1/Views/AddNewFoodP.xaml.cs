using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaloricka_kalkulacka_du1.ViewModels;
using Kaloricka_kalkulacka_du1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using System.Xml.Serialization;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace Kaloricka_kalkulacka_du1.Views
{
    public partial class AddNewFoodP : ContentPage
    {
        private AddNewFoodVM _addNewFoodVM;
        public static MainWindowP _mainWindowP;
        public AddNewFoodP()
        {
            InitializeComponent();            
            _addNewFoodVM = new AddNewFoodVM();
            BindingContext = _addNewFoodVM;
        }

        private void UnfocusedEn1(object sender, FocusEventArgs e)
        {
            if ((_addNewFoodVM.food == null) || (_addNewFoodVM.food == ""))
            {
                lbError.IsVisible = true;
            }
            else
            {
                lbError.IsVisible = false;
            }
        }
        private void UnfocusedEn2(object sender, FocusEventArgs e)
        {
            if ((_addNewFoodVM.proteinnw > 0) || (_addNewFoodVM.proteinnw != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        private void UnfocusedEn3(object sender, FocusEventArgs e)
        {
            if ((_addNewFoodVM.carbohydratesnw > 0)|| (_addNewFoodVM.carbohydratesnw != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        private void UnfocusedEn4(object sender, FocusEventArgs e)
        {
            if ((_addNewFoodVM.fatnw > 0)|| (_addNewFoodVM.fatnw != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        private void UnfocusedEn5(object sender, FocusEventArgs e)
        {
            if ((_addNewFoodVM.sugarnw > 0)|| (_addNewFoodVM.sugarnw != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        async void ShowMessageBox()
        {
            await DisplayAlert("Chyba", "Jsou požadovaná povinná data!", "OK");
        }
        private void Button_Click(object sender, EventArgs e)
        {
            
            if (lbError.IsVisible == true)
            {
                ShowMessageBox();
            }
            else
            {
                RequestWritePermission();
            }
        }
        private async Task RequestWritePermission()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string name = "food.txt";
            var status = await Permissions.CheckStatusAsync<StorageWrite>();
            
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<StorageWrite>();
            }

            if (status == PermissionStatus.Granted)
            {
                string line = _addNewFoodVM.food + " " + _addNewFoodVM.proteinnw + " " + _addNewFoodVM.carbohydratesnw + " " + _addNewFoodVM.fatnw + " " + _addNewFoodVM.sugarnw;
                string filePath = System.IO.Path.Combine(folderPath, name);
                using (StreamWriter sw = new StreamWriter(filePath,true))
                {
                    sw.WriteLine(line);
                    sw.Close();
                }
                _mainWindowP.value_to_pc(_addNewFoodVM.food);
            }
        }
    }
}