using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public ReportsController(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("total-revenue")]
        public ActionResult TotalRevenue([FromBody] FiscalYear fiscal )
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var report = _repository.TotalRevenue(fiscal.fiscal_year, Convert.ToInt32(userId));

            return Ok(report);
        }

        [Authorize]
        [HttpPost("revenue-by-month")]
        public ActionResult RevenueByMonth([FromBody]FiscalYear fiscal)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var report = _repository.RevenueByMonth(fiscal.fiscal_year, Convert.ToInt32(userId));

            return Ok(report);
        }

        [Authorize]
        [HttpPost("revenue-by-customer")]
        public ActionResult RevenueByCustomer([FromBody] FiscalYear fiscal)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var report = _repository.RevenueByCustomer(fiscal.fiscal_year, Convert.ToInt32(userId));

            return Ok(report);
        }


        public class FiscalYear
        {
            public int fiscal_year { get; set; }
        }
    }
}
