using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Models
{
    public class RevenueModel
    {
        [Key]
        public int Revenue_Id { get; set; }
        public int Customer_Id { get; set; }
        public double Amount { get; set; }
        public string Invoice_Id { get; set; }
        public string Description { get; set; }
        public DateTime Accrual_Date { get; set; }
        public DateTime Transaction_Date { get; set; }

    }
}
