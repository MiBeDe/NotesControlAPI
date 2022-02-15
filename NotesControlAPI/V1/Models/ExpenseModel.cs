using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Models
{
    public class ExpenseModel
    {
        [Key]
        public int Expense_Id { get; set; }
        public int? Customer_Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Accrual_Date { get; set; }
        public DateTime Transaction_Date { get; set; }

    }
}
