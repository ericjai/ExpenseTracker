using System;
using System.Collections.Generic;
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
    public partial class ExpensesPage : ContentPage
    {
        static Budget budget;
        static TotalExpense totalexpense;


        public ExpensesPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ReadBudget();

            ReadExpense();

            ReadTotalExpense();

            BalanceRemaining();

        }

        private void BalanceRemaining()
        {
            
            var remaininAmount = budget.BudgetAmount - App.total;

            totalexpense.RemainingBalance = 0;

            App.total = 0;

            totalexpense.RemainingBalance = remaininAmount;

            stacklayout3.BindingContext = totalexpense;

            Remaininglable.SetBinding(Label.TextProperty, "RemainingBalance");




        }

        private void ReadTotalExpense()
        {
            totalexpense = new TotalExpense();

            totalexpense.AllExpenseAdded = App.total;

            stacklayout2.BindingContext = totalexpense;

            ExpenseLable.SetBinding(Label.TextProperty, "AllExpenseAdded");
        }

        private void ReadBudget()
        {

            try
            {
                budget = new Budget();
                string savedbudgetamount = File.ReadAllText(App.BudgetFileName);
                

                if (!string.IsNullOrWhiteSpace(savedbudgetamount))
                {
                    budget.BudgetAmount = float.Parse(savedbudgetamount);
                }

                stacklayout1.BindingContext = budget;
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(App.BudgetFileName, String.Empty);
            }


        }

        public void ReadExpense()
        {
           

            var expenses = new List<Expense>();
            try
            {
                var file = new System.IO.FileInfo(App.FileName);
                string[] lines = File.ReadAllLines(App.FileName);
                foreach (var line in lines)
                {
                    string[] words = line.Split(' ');
                    var expense = new Expense();
                    if (words.Length > 0)
                    {
                        expense.Name = words[0];
                    }
                    if (words.Length > 1)
                    {
                        float f = float.Parse(words[1]);
                        expense.Amount = f;
                        App.total = App.total + f;
                    }
                    if (words.Length > 2)
                    {
                        expense.Category = (Category)Enum.Parse(typeof(Category), words[2]);
                    }

                    if (words.Length > 3)
                    {
                        var dateTime = DateTime.Parse(words[3]);
                        expense.DisplayDate = dateTime.Date.ToShortDateString();
                    }


                    expenses.Add(expense);
                }

                Expenselistview.ItemsSource = expenses.ToList();
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(App.FileName, String.Empty);
            }
        }
        async void OnExpenseAdded_Clicked(object sender, EventArgs e)
        {


            if (budget.BudgetAmount == 0)
            {
                await DisplayAlert("Alert", "You have not set the budget", "OK");

            }
            else
            {
                await Navigation.PushAsync(new ExpenseEntryPage
                { BindingContext = new Expense() });
            }


        }

        private async void Listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ExpenseEntryPage
            {
                BindingContext = e.SelectedItem as Expense
            });
        }

        private async void OnBudgetButton_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new BudgetEntryPage { BindingContext = new Budget() });
        }
    }
}