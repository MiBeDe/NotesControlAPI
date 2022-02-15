using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class ReportByMonthDTO
    {
        public string Month_Name { get; set; }
        public double Month_Revenue { get; set; }
    }
}
