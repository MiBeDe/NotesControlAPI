using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public  interface IRevenueRepository
    {
        int InsertRevenue(RevenueModel revenue, int customerId);
        string UpdateRevenue(RevenueModel revenue, int revenue_id, int userId);
        string DeleteRevenue(int revenue_id, int userId);
    }
}
