using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1ExpenseTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Team1ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseEntryPage : ContentPage
    {

       
        public ObservableCollection<Model.Category> EnumCategories { get; set; } = new ObservableCollection<Model.Category>();

        public ExpenseEntryPage()
        {
            InitializeComponent();

            EnumCategories = new ObservableCollection<Model.Category>(Enum.GetValues(typeof(Model.Category)).OfType<Model.Category>().ToList());
            Category.ItemsSource = EnumCategories;
        }

        

        async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;

            var strSelectedCategory = Category.SelectedItem;

            if (strSelectedCategory == null || expense.Name==null || expense.Amount==0 || expense.Date==null)
            {
                await DisplayAlert("Alert", "All Fields Required", "OK");
            }
            else
            {
                string selectedCategory = (string)strSelectedCategory.ToString();

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(App.FileName, true))
                {
                    var expenseString = expense.Name + ' ' + expense.Amount + ' ' + selectedCategory + ' ' + expense.Date.ToString("yyyy/M/dd");

                    file.WriteLine(expenseString);

                }
                await Navigation.PopAsync();

            }

        }
        async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(App.FileName))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    var splitwords = line.Split(' ');
                    if (splitwords[0] != expense.Name)
                        sw.WriteLine(line);
                }
            }
            File.Copy(tempFile, App.FileName, true);
            expense.Name = string.Empty;
            await Navigation.PopAsync();
        }
    }
}