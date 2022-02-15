using Microsoft.EntityFrameworkCore;
using NotesControlAPI.Database;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly NotesControlContext _context;

        public RevenueRepository(NotesControlContext context)
        {
            _context = context;
        }

        public int InsertRevenue(RevenueModel revenue, int customerId)
        {
            revenue.Customer_Id = customerId;

            _context.Revenues.Add(revenue);
            _context.SaveChanges();

            return revenue.Revenue_Id;
        }


        public string UpdateRevenue(RevenueModel revenue, int revenue_id , int userId)
        {
            var revenueReg = (from r in _context.Revenues
                           join c in _context.Customers on r.Customer_Id equals c.Customer_Id
                           where r.Revenue_Id == revenue_id
                           select new { c.UserId, r.Customer_Id, r.Revenue_Id }).FirstOrDefault();

            if (revenueReg == null)
                return "Revenue not found to this User";

            if (revenueReg.UserId != userId)
                return "Revenue not found to this User";

            revenue.Revenue_Id = revenueReg.Revenue_Id;
            revenue.Customer_Id = revenueReg.Customer_Id;
            _context.Revenues.Update(revenue);
            _context.SaveChanges();

            return "";
        }

        public string DeleteRevenue(int revenue_id, int userId)
        {
            var revenueReg = (from r in _context.Revenues
                              join c in _context.Customers on r.Customer_Id equals c.Customer_Id
                              where r.Revenue_Id == revenue_id
                              select new { c.UserId }).FirstOrDefault();

            if (revenueReg == null)
                return "Revenue not found to this User";

            if (revenueReg.UserId != userId)
                return "Revenue not found to this User";

            var revenue = _context.Revenues.Where(x => x.Revenue_Id == revenue_id).FirstOrDefault();
            _context.Entry(revenue).State = EntityState.Deleted;
            _context.SaveChanges();

            return "";
        }
    }
}
