using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface IExpenseRepository
    {
        int InsertExpense(ExpenseModel expense);
        void UpdateExpense(ExpenseModel expense);
        void DeleteExpense(int expense_id, int userId);
        ExpenseModel GetExpenseById(int expense_id, int userId);
    }
}
