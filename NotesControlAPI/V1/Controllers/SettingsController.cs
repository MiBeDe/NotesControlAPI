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
    public class SettingsController : ControllerBase
    {
        private readonly IConfigurationRepository _repository;
        private readonly IMapper _mapper;

        public SettingsController(IConfigurationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut("")]
        public ActionResult UpdateSettings([FromBody] ConfigurationDTO configuration)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (configuration == null)
                return BadRequest();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            ConfigurationModel configurationModel = _mapper.Map<ConfigurationDTO, ConfigurationModel>(configuration);

            var config = _repository.updateConfiguration(configurationModel, Convert.ToInt32(userId));

            ConfigurationDTO configurationDTO = _mapper.Map<ConfigurationModel, ConfigurationDTO>(config);

            return Ok(configurationDTO);
        }

        [Authorize]
        [HttpGet("")]
        public ActionResult GetSettings()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == "0")
                return NotFound(new { error = "Invalid user to execute this function, please the login again." });

            var config = _repository.getConfiguration(Convert.ToInt32(userId));

            ConfigurationDTO configurationDTO = _mapper.Map<ConfigurationModel, ConfigurationDTO>(config);

            return Ok(configurationDTO);

        }
    }
}
