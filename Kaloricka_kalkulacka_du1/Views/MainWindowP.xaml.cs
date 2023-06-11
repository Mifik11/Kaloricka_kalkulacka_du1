using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Kaloricka_kalkulacka_du1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;



namespace Kaloricka_kalkulacka_du1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainWindowP : ContentPage
    {
        public double protein, carbohydrates, fat, sugar;
        private MainWindowVM _mainWindowVM;
        public void value_to_pc(string input)
        {
            itemPc.Items.Add(input);
        }
        public void limit_value(double pro, double car, double sug, double fat)
        {
            lbProteinLim.Text = Convert.ToString(pro);
            lbCarbohydratesLim.Text = Convert.ToString(car);
            lbSugarLim.Text = Convert.ToString(sug);
            lbFatLim.Text = Convert.ToString(fat);
        }
        public MainWindowP()
        {
            InitializeComponent();
            AddNewFoodP._mainWindowP= this;
            ChangeLimitP._changeLimit = this;
            _mainWindowVM = new MainWindowVM();
            BindingContext = _mainWindowVM;
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "food.txt");
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                string[] foodNames = new string[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] spread = lines[i].Split(' ');
                    foodNames[i] = spread[0];
                }
                itemPc.Items.Clear();
                foreach (string foodName in foodNames)
                {
                    itemPc.Items.Add(foodName);
                }
            }
            string filePath1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dailyfood.txt");
            File.Delete(filePath1);
            string filePath2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dailyamount.txt");
            string filePath3 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "limit.txt");
            string filePath4 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "time.txt");
            string[] array = new string[MainWindowVM.AllFood.Count];
            if (File.Exists(filePath3))
            {
                string[] limit = File.ReadAllLines(filePath3);
                lbProteinLim.Text = limit[0];
                lbCarbohydratesLim.Text = limit[1];
                lbFatLim.Text = limit[2];
                lbSugarLim.Text = limit[3];
            }
            if (File.Exists(filePath2))
            {
                DateTime dt = DateTime.Now;
                string[] data = File.ReadAllLines(filePath2);
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[0] == dt.ToString("MM.dd.yyyy"))
                    {
                        lbProtein.Text = data[1];
                        lbCarbohydrates.Text = data[2];
                        lbFat.Text = data[3];
                        lbSugar.Text = data[4];
                    }
                    else
                    {
                        File.Delete(filePath2);
                        File.Delete(filePath1);
                    }
                }
            }
            if (File.Exists(filePath1))
            {
                string[] data = File.ReadAllLines(filePath1);
                for (int i = 0; i < data.Length; i++)
                {
                    string[] line = data[i].Split(' ');
                    MainWindowVM e = new MainWindowVM();
                    e.foodname = line[0];
                    e.amount = Convert.ToDouble(line[1]);
                    e._ID = Guid.Parse(line[2]);
                    MainWindowVM.AllFood.Add(e);
                }
            }
            DateTime datet = DateTime.Now;
            if (File.Exists(filePath4))
            {
                string[] time = File.ReadAllLines(filePath4);
                lbRow.Text = time[1];
            }

            _mainWindowVM = new MainWindowVM();
            _mainWindowVM._ID = Guid.NewGuid();
            lv.ItemsSource = MainWindowVM.AllFood;
            BindingContext = _mainWindowVM;
        }

        //private void BtChange_Clicked(object sender, EventArgs e)
        //{
        //    _mainWindowVM.foodname = (((Button)sender).BindingContext as MainWindowVM).foodname;
        //    _mainWindowVM.amount = (((Button)sender).BindingContext as MainWindowVM).amount;
        //    _mainWindowVM._ID = (((Button)sender).BindingContext as MainWindowVM)._ID;
        //}

        private void UnfocusedEn(object sender, FocusEventArgs e)
        {
            _mainWindowVM.amount = Convert.ToDouble(tbAmount.Text);
            if ((_mainWindowVM.amount > 0) || (_mainWindowVM.amount != default))
            {
                lbError.IsVisible = false;
            }
            else
            {
                
                lbError.IsVisible = true;
            }
        }
        //private void btDelete_Click(object sender, EventArgs e)
        //{
        //    MainWindowVM toDelete = ((Button)sender).BindingContext as MainWindowVM;
        //    MainWindowVM.AllFood.Remove(toDelete);
        //    lv.ItemsSource = null;
        //    lv.ItemsSource = MainWindowVM.AllFood;
        //    string[] array = new string[MainWindowVM.AllFood.Count];
        //    for (int i = 0; i < MainWindowVM.AllFood.Count; i++)
        //    {
        //        array[i] = MainWindowVM.AllFood[i].ToString();
        //    }
        //    File.WriteAllLines($"dailyfood.txt", array);
        //    string foodname;
        //    double amount;
        //    string[] data = File.ReadAllLines($"dailyfood.txt");
        //    protein = carbohydrates = fat = sugar = 0;
        //    for (int i = 0; i < data.Length; i++) //projede to tolik krát kolik jídla za dnešek snědl
        //    {
        //        string[] line = data[i].Split(' ');
        //        foodname = line[0];
        //        amount = Convert.ToDouble(line[1]);
        //        double xprotein, xcarbohydrates, xsugar, xfat;
        //        string xfoodname;
        //        string[] foodinfo = File.ReadAllLines($"food.txt");
        //        for (int d = 0; d < foodinfo.Length; d++) // projíždí a kontroluje o jaké jídlo se jedná               
        //        {
        //            string[] lineinfo = foodinfo[d].Split(' ');
        //            xfoodname = lineinfo[0];
        //            xprotein = Convert.ToDouble(lineinfo[1]);
        //            xcarbohydrates = Convert.ToDouble(lineinfo[2]);
        //            xfat = Convert.ToDouble(lineinfo[3]);
        //            xsugar = Convert.ToDouble(lineinfo[4]);
        //            if (xfoodname == foodname)
        //            {
        //                protein += xprotein / 100 * amount;
        //                carbohydrates += xcarbohydrates / 100 * amount;
        //                fat += xfat / 100 * amount;
        //                sugar += xsugar / 100 * amount;
        //            }
        //        }
        //    }
        //    lbProtein.Text = Convert.ToString(protein);
        //    lbCarbohydrates.Text = Convert.ToString(carbohydrates);
        //    lbFat.Text = Convert.ToString(fat);
        //    lbSugar.Text = Convert.ToString(sugar);
        //    DateTime dt = new DateTime();
        //    dt = DateTime.Now;
        //    string[] dailyinfo = new string[] { dt.ToString("MM.dd.yyyy"), protein.ToString(), carbohydrates.ToString(), sugar.ToString(), fat.ToString() };
        //    File.WriteAllLines($"Data/dailyamount.txt", dailyinfo);
        //}
        private void btAdd_Click(object sender, EventArgs e)
        {
            async void ShowMessageBox()
            {
                await DisplayAlert("Chyba", "Jsou požadovaná povinná data!", "OK");
            }
            if (lbError.IsVisible == true)
            {
                ShowMessageBox();
            }
            else
            {
                MainWindowVM q = MainWindowVM.AllFood.Find(t => t._ID == _mainWindowVM._ID);
                int qIndex = MainWindowVM.AllFood.IndexOf(q);
                if (q != null)
                {
                    MainWindowVM.AllFood[qIndex] = MainWindowVM.Copy(_mainWindowVM);
                }
                else
                {
                    MainWindowVM.AllFood.Add(MainWindowVM.Copy(_mainWindowVM));
                    MainWindowVM.Clear(_mainWindowVM);
                }
                lv.ItemsSource = null;
                lv.ItemsSource = MainWindowVM.AllFood;
            }
            
            string filePath1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dailyfood.txt");
            string filePath2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dailyamount.txt");
            string filePath3 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "limit.txt");
            string filePath4 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "time.txt");
            string[] array = new string[MainWindowVM.AllFood.Count];
            for (int i = 0; i < MainWindowVM.AllFood.Count; i++)
            {
                array[i] = MainWindowVM.AllFood[i].ToString();
            }
            File.WriteAllLines(filePath1, array);

            if (File.Exists(filePath1))
            {
                string foodname;
                double amount;
                string[] data = File.ReadAllLines(filePath1);
                double protein = 0, carbohydrates = 0, fat = 0, sugar = 0;
                for (int i = 0; i < data.Length; i++) // Loop through the consumed food items
                {
                    string[] line = data[i].Split(' ');
                    foodname = line[0];
                    amount = Convert.ToDouble(line[1]);
                    double xprotein, xcarbohydrates, xsugar, xfat;
                    string xfoodname;
                    string[] foodinfo = File.ReadAllLines(filePath1);
                    for (int d = 0; d < foodinfo.Length; d++) // Loop through the food information
                    {
                        string[] lineinfo = foodinfo[d].Split(' ');
                        xfoodname = lineinfo[0];
                        xprotein = Convert.ToDouble(lineinfo[1]);
                        xcarbohydrates = Convert.ToDouble(lineinfo[2]);
                        xfat = Convert.ToDouble(lineinfo[3]);
                        xsugar = Convert.ToDouble(lineinfo[4]);
                        if (xfoodname == foodname)
                        {
                            protein += xprotein / 100 * amount;
                            carbohydrates += xcarbohydrates / 100 * amount;
                            fat += xfat / 100 * amount;
                            sugar += xsugar / 100 * amount;
                        }
                    }
                }

                lbProtein.Text = Convert.ToString(protein);
                lbCarbohydrates.Text = Convert.ToString(carbohydrates);
                lbFat.Text = Convert.ToString(fat);
                lbSugar.Text = Convert.ToString(sugar);

                DateTime dt = DateTime.Now;
                string[] dailyinfo = new string[] { dt.ToString("MM.dd.yyyy"), protein.ToString(), carbohydrates.ToString(), sugar.ToString(), fat.ToString() };
                File.WriteAllLines(filePath2, dailyinfo);

                if (File.Exists(filePath3))
                {
                    string[] limit = File.ReadAllLines(filePath3);
                    if ((protein >= Convert.ToDouble(limit[0])) && (carbohydrates >= Convert.ToDouble(limit[1])) && (fat >= Convert.ToDouble(limit[2])) && (sugar >= Convert.ToDouble(limit[3])))
                    {
                        if (File.Exists(filePath4))
                        {
                            string day;
                            string[] time = File.ReadAllLines(filePath4);
                            if (time[0] == Convert.ToString(dt.AddDays(-1)))
                            {
                                day = time[1];
                                int row = Convert.ToInt32(day);
                                row++;
                                day = Convert.ToString(row);
                                string[] xtime = new string[] { dt.ToString("MM.dd.yyyy"), day };
                                File.WriteAllLines(filePath4, xtime);
                                lbRow.Text = day;
                            }
                        }
                        else
                        {
                            string day = "1";
                            string[] time = new string[] { dt.ToString("MM.dd.yyyy"), day };
                            File.WriteAllLines(filePath4, time);
                            lbRow.Text = day;
                        }
                    }
                }
            }
        }
    }

}