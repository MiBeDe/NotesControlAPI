﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class ReportRevenueByMonthDTO
    {
        public List<ReportByMonthDTO> Revenue { get; set; }
        public double Max_Revenue_Amount { get; set; }
    }
}
