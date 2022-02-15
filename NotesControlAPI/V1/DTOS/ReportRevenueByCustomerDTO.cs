using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class ReportRevenueByCustomerDTO
    {
        public List<ReportByCustomerDTO> Revenue { get; set; }
        public double Max_Revenue_Amount { get; set; }
    }
}
