using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Team1ExpenseTracker
{
    public partial class App : Application
    {
        public static string FileName { get; internal set; }
        public static string BudgetFileName { get; set; }
     //  public static string ExpenseFileName { get; set; }

        public static float total = 0;

        


        public App()
        {
            InitializeComponent();
             
            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NewExpensefile.txt");
            BudgetFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NewBudgetfile.txt");
         //   ExpenseFileName= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExpenseFile20.txt");

           

            MainPage = new NavigationPage(new ExpensesPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
