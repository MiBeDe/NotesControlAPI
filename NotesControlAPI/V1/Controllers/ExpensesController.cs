using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesControlAPI.V1.DTOS;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ExpensesController(IExpenseRepository repository, IMapper mapper, ICategoryRepository categoryRepository, ICustomerRepository customerRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _customerRepository = customerRepository;
        }

        [Authorize]
        [HttpPost("{categoryID}")]
        public ActionResult InsertExpense([FromBody] ExpenseDTO expense, int categoryID)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (categoryID == 0)
                return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var category = _categoryRepository.GetCategoryById(categoryID, Convert.ToInt32(userId));
            if (category == null)         
                return NotFound(new { error = "Category Id not found to this User" });

            if(expense.Customer_Id != null)
            {
                var customer = _customerRepository.GetCustomerById((int)expense.Customer_Id, Convert.ToInt32(userId));
                if(customer == null)
                    return NotFound(new { error = "Customer Id not found to this User" });
            }

            ExpenseModel expenseModel = _mapper.Map<ExpenseDTO, ExpenseModel>(expense);

            expenseModel.CategoryId = category.Category_Id;
            expenseModel.UserId = Convert.ToInt32(userId);

            var expense_id = _repository.InsertExpense(expenseModel);

            return Created("", new { expense_id = expense_id });
        }

        [Authorize]
        [HttpPut("{expenseID}")]
        public ActionResult UpdateExpanseById([FromBody] ExpenseDTO expense, int expenseID)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (expenseID == 0)
                return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var expenseReg = _repository.GetExpenseById(expenseID, Convert.ToInt32(userId));
            if(expenseReg == null)
                return NotFound(new { error = "Expense Id not found to this User" });

            if (expense.Customer_Id != null)
            {
                var customer = _customerRepository.GetCustomerById((int)expense.Customer_Id, Convert.ToInt32(userId));
                if (customer == null)
                    return NotFound(new { error = "Customer Id not found to this User" });
            }


            ExpenseModel expenseModel = _mapper.Map<ExpenseDTO, ExpenseModel>(expense);
            expenseModel.Expense_Id = expenseReg.Expense_Id;
            expenseModel.CategoryId = expenseReg.CategoryId;
            expenseModel.UserId = expenseReg.UserId;

            _repository.UpdateExpense(expenseModel);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{expenseID}")]
        public ActionResult DeleteExpense(int expenseID)
        {
            if (expenseID == 0)
                return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var expenseReg = _repository.GetExpenseById(expenseID, Convert.ToInt32(userId));
            if (expenseReg == null)
                return NotFound(new { error = "Expense Id not found to this User" });

            _repository.DeleteExpense(expenseID, Convert.ToInt32(userId));

            return Ok();

        }
    }
}
