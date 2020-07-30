using System;
using System.Collections.Generic;
using System.Text;

namespace Team1ExpenseTracker.Model
{
    public enum Category
    {
        Food,
        Transportation,
        Personal,
        Housing,
        Miscellaneous
    };

    public class Expense
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public string DisplayDate { get; set; }
    }
}
