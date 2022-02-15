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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly NotesControlContext _context;

        public ExpenseRepository(NotesControlContext context)
        {
            _context = context;
        }

        public int InsertExpense(ExpenseModel expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return expense.Expense_Id;
        }

        public void UpdateExpense(ExpenseModel expense)
        {
            _context.Update(expense);
            _context.SaveChanges();
        }

        public void DeleteExpense(int expense_id, int userId)
        {
            var expense = _context.Expenses.Where(x => x.Expense_Id == expense_id).FirstOrDefault();
            _context.Entry(expense).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public ExpenseModel GetExpenseById(int expense_id, int userId)
        {
            var expense = _context.Expenses.AsNoTracking().Where(x => x.Expense_Id == expense_id && x.UserId == userId).FirstOrDefault();
            return expense;
        }
    }
}
