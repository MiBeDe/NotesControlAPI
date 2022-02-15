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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("")]
        public ActionResult InsertCustomer([FromBody] CustomerDTO customer)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (customer == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            CustomerModel customerModel = _mapper.Map<CustomerDTO, CustomerModel>(customer);          

            customerModel.UserId = Convert.ToInt32(userId);

            var customer_id = _repository.InsertCustomer(customerModel);

            if (customer_id == -1)
            {
                return StatusCode(400, new { error = "Invalid CNPJ" });
            }
            else if (customer_id == -2)
            {
                return StatusCode(400, new { error = "CNPJ already exists" });
            }

            return Ok(new { customer_id = customer_id });
        }

        [Authorize]
        [HttpGet("{customerID}")]
        public ActionResult GetCustomerById(int customerID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var customer = _repository.GetCustomerById(customerID, Convert.ToInt32(userId));

            if(customer == null)
            {
                return NotFound(new { error = "Customer Id not found to this User" });
            }
            else
            {
                return Ok(customer);
            }
        }

        [Authorize]
        [HttpGet("")]
        public ActionResult GetCustomerByNameCnpj(string name, string cnpj)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            string cnpjReplace = null;
            if (cnpj != null)
            {
                cnpjReplace = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            }

            var customer = _repository.GetCustomerByNameCnpj(name, cnpjReplace, Convert.ToInt32(userId));
            var qtdReg = customer.Count;

            return Ok(new { count = qtdReg, customer = customer });
        }

        [Authorize]
        [HttpPut("{customerID}/archives")]
        public ActionResult CustomerArchives()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            return Ok();
        }

        [Authorize]
        [HttpPut("{customerID}")]
        public ActionResult UpdateCustomer([FromBody] CustomerDTO customer , int customerID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var customerReg = _repository.GetCustomerById(customerID, Convert.ToInt32(userId));
            if(customerReg == null)
                return NotFound(new { error = "Customer Id not found to this User" });

            CustomerModel customerModel = _mapper.Map<CustomerDTO, CustomerModel>(customer);

            customerModel.Customer_Id = customerReg.Customer_Id;
            customerModel.UserId = customerReg.UserId;

            var updateCustomer = _repository.UpdateCustomer(customerModel);

            if (updateCustomer == -1)
            {
                return StatusCode(400, new { error = "Invalid CNPJ" });
            }
            else if (updateCustomer == -2)
            {
                return StatusCode(400, new { error = "CNPJ already exists" });
            }

            return Ok();
        }
    }
}
