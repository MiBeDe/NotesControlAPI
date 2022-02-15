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
    public class RevenuesController : ControllerBase
    {
        private readonly IRevenueRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public RevenuesController(IRevenueRepository repository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        [Authorize]
        [HttpPost("{customerID}")]
        public ActionResult InsertRevenue([FromBody] RevenueDTO revenue, int customerID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            RevenueModel revenueModel = _mapper.Map<RevenueDTO, RevenueModel>(revenue);

            var customer = _customerRepository.GetCustomerById(customerID, Convert.ToInt32(userId));

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (customer == null)
                return NotFound(new { error = "Customer Id not found to this User" });

            var revenueId = _repository.InsertRevenue(revenueModel, customerID);

            return Ok(new { revenue_id = revenueId });
        }

        [Authorize]
        [HttpPut("{revenueID}")]
        public ActionResult UpdateRevenue([FromBody] RevenueDTO revenue, int revenueID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            RevenueModel revenueModel = _mapper.Map<RevenueDTO, RevenueModel>(revenue);

            var update = _repository.UpdateRevenue(revenueModel, revenueID, Convert.ToInt32(userId));

            if(update != "")
            {
                return NotFound(new { error = update });
            }

            return Ok();
        }
        [Authorize]
        [HttpDelete("{revenueID}")]
        public ActionResult DeleteRevenue(int revenueID)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var delete = _repository.DeleteRevenue(revenueID, Convert.ToInt32(userId));

            if(delete != "")
            {
                return NotFound(new { error = delete });
            }

            return Ok();
        }
    }
}
