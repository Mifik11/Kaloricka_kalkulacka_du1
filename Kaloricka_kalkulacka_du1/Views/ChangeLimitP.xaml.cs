using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaloricka_kalkulacka_du1.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;
using Xamarin.Forms.Shapes;
using System.Xml.Serialization;


namespace Kaloricka_kalkulacka_du1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeLimitP : ContentPage
    {
        private ChangeLimitVM _changeLimitVM;
        public static MainWindowP _changeLimit;
        public ChangeLimitP()
        {
            InitializeComponent();
            _changeLimitVM = new ChangeLimitVM();
            BindingContext = _changeLimitVM;
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
            string name = "limit.txt";
            var status = await Permissions.CheckStatusAsync<StorageWrite>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<StorageWrite>();
            }

            if (status == PermissionStatus.Granted)
            {
                string[] array = new string[4] { _changeLimitVM.protein.ToString(), _changeLimitVM.carbohydrates.ToString(), _changeLimitVM.sugar.ToString(), _changeLimitVM.fat.ToString() };
                string filePath = System.IO.Path.Combine(folderPath, name);
                File.WriteAllLines(filePath, array);
                _changeLimit.limit_value(_changeLimitVM.protein, _changeLimitVM.carbohydrates, _changeLimitVM.sugar, _changeLimitVM.fat);
            }
        }
        private void UnfocusedEn6(object sender, FocusEventArgs e)
        {
            if ((_changeLimitVM.protein > 0) || (_changeLimitVM.protein != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        private void UnfocusedEn7(object sender, FocusEventArgs e)
        {
            if ((_changeLimitVM.carbohydrates > 0) || (_changeLimitVM.carbohydrates != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        private void UnfocusedEn8(object sender, FocusEventArgs e)
        {
            if ((_changeLimitVM.sugar > 0) || (_changeLimitVM.sugar != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
        private void UnfocusedEn9(object sender, FocusEventArgs e)
        {
            if ((_changeLimitVM.fat > 0) || (_changeLimitVM.fat != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                lbError.IsVisible = true;
            }
        }
    }
}