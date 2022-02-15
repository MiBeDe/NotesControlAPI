using NotesControlAPI.Database;
using NotesControlAPI.V1.DTOS;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly NotesControlContext _context;

        public ReportRepository(NotesControlContext context)
        {
            _context = context;
        }
        public ReportTotalRevenueDTO TotalRevenue(int fiscalYear, int userId)
        {
           var totalAmount = _context.Revenues.Join(_context.Customers, revenue => revenue.Customer_Id, customer => customer.Customer_Id, (revenue, customer) => new { revenue, customer })
                       .Where(x => x.customer.UserId == userId && x.revenue.Accrual_Date.Year == fiscalYear).Sum(x => x.revenue.Amount);

            ReportTotalRevenueDTO report = new ReportTotalRevenueDTO();
            report.Total_Revenue = totalAmount;
            report.Max_Revenue_Amount = totalAmount;

            return report;
        }

        public ReportRevenueByMonthDTO RevenueByMonth(int fiscalYear, int userId)
        {
            List<ReportByMonthDTO> revenueByMoth = _context.Revenues.Join(_context.Customers, revenue => revenue.Customer_Id, customer => customer.Customer_Id, (revenue, customer) => new { revenue, customer })
                                                                     .Where(x => x.customer.UserId == userId && x.revenue.Accrual_Date.Year == fiscalYear).GroupBy(x => x.revenue.Accrual_Date.Month).Select( r => new ReportByMonthDTO
                                                                     {
                                                                            Month_Name = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(r.Key),
                                                                            Month_Revenue = r.Sum(x => x.revenue.Amount)
                                                                     }).ToList();

            var max_revenue_amount = revenueByMoth.Sum(x => x.Month_Revenue);

            ReportRevenueByMonthDTO report = new ReportRevenueByMonthDTO();
            report.Revenue = revenueByMoth;
            report.Max_Revenue_Amount = max_revenue_amount;

            return report;
        }
        public ReportRevenueByCustomerDTO RevenueByCustomer(int fiscalYear, int userId)
        {
            List<ReportByCustomerDTO> revenueByCustomer = _context.Revenues.Join(_context.Customers, revenue => revenue.Customer_Id, customer => customer.Customer_Id, (revenue, customer) => new { revenue, customer })
                                                                     .Where(x => x.customer.UserId == userId && x.revenue.Accrual_Date.Year == fiscalYear).GroupBy(x => x.customer.Legal_Name).Select(r => new ReportByCustomerDTO
                                                                     {
                                                                         Customer_Name = r.Key,
                                                                         Revenue = r.Sum(x => x.revenue.Amount)
                                                                     }).ToList();

            var max_revenue_amount = revenueByCustomer.Sum(x => x.Revenue);

            ReportRevenueByCustomerDTO report = new ReportRevenueByCustomerDTO();
            report.Revenue = revenueByCustomer;
            report.Max_Revenue_Amount = max_revenue_amount;

            return report;

        }

    }
}
