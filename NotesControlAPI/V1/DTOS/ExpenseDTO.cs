using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class ExpenseDTO
    {
        public int? Customer_Id { get; set; }
        public double Amount { get; set; }      
        public string Description { get; set; }
        [Required]
        public Nullable<DateTime> Accrual_Date { get; set; }
        [Required]
        public Nullable<DateTime> Transaction_Date { get; set; }

    }
}
