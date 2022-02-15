using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class RevenueDTO
    {
        public double Amount { get; set; }
        public string Invoice_Id { get; set; }
        public string Description { get; set; }
        [Required]
        public Nullable<DateTime> Accrual_Date { get; set; }
        [Required]
        public Nullable<DateTime> Transaction_Date { get; set; }

    }
}
