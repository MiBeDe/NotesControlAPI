using NotesControlAPI.V1.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface IReportRepository
    {
        ReportTotalRevenueDTO TotalRevenue(int fiscalYear, int userId);
        ReportRevenueByMonthDTO RevenueByMonth(int fiscalYear, int userId);
        ReportRevenueByCustomerDTO RevenueByCustomer(int fiscalYear, int userId);
    }
}
