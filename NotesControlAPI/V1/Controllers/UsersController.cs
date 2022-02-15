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
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("")]
        public ActionResult InsertUser([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (user == null)
            {
                return BadRequest();
            }
   
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string email =  User.FindFirst(ClaimTypes.Email)?.Value;
            //var isAdmin = _repository.IsAdmin(Convert.ToInt32(userId));

            UserModel userModel = _mapper.Map<UserDTO, UserModel>(user);

            if (userId == "0" && email == "admin@admin.com.br")
            {
                var idUser = _repository.InsertUser(userModel);
                if(idUser == -1)
                {
                    return StatusCode(400, new { error = "Invalid CNPJ" });
                }
                else if(idUser == -2)
                {
                    return StatusCode(400, new { error = "CNPJ already exists" });
                }


                return Created("", new { user_id = idUser });
            }
            else
            {
                return Unauthorized(new { error = "User not is Admin" });
            }
            
        }

        [Authorize]
        [HttpGet("{ID}")]
        public ActionResult GetUserById(int ID)
        {
            var user = _repository.GetUser(ID);

            UserDTO userDTO = _mapper.Map<UserModel, UserDTO>(user);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var isAdmin = _repository.IsAdmin(Convert.ToInt32(userId));

            if (userDTO == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (userId == "0" && email == "admin@admin.com.br")
            {
                return Ok(userDTO);
            }
            else
            {
                return Unauthorized(new { error = "User not is Admin" });
            }
        }

        [Authorize]
        [HttpPut("{ID}")]
        public ActionResult UpdateUser([FromBody] UserDTO user, int ID)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (user == null)
            {
                return BadRequest();
            }

            var userReg = _repository.GetUser(ID);
            if (userReg == null)
                return NotFound(new { error = "User not found" });

            UserModel userModel = _mapper.Map<UserDTO, UserModel>(user);

            userModel.UserId = userReg.UserId;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string email = User.FindFirst(ClaimTypes.Email)?.Value;           

            if (userId == "0" && email == "admin@admin.com.br")
            {
                var idUser = _repository.UpdateUser(userModel);
                if (idUser == -1)
                {
                    return StatusCode(400, new { error = "Invalid CNPJ" });
                }
                else if (idUser == -2)
                {
                    return StatusCode(400, new { error = "CNPJ already exists" });
                }

                return Ok();
            }
            else
            {
                return Unauthorized(new { error = "User not is Admin" });
            }

        }

    }
}
