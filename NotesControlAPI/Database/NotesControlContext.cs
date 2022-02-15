using Microsoft.EntityFrameworkCore;
using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.Database
{
    public class NotesControlContext : DbContext
    {
        public NotesControlContext(DbContextOptions<NotesControlContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<RevenueModel> Revenues { get; set; }
        public DbSet<ConfigurationModel> Configuration { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ExpenseModel> Expenses { get; set; }
    }
}
