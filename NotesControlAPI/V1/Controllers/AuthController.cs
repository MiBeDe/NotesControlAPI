using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesControlAPI.V1.DTOS;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
    }

        [HttpPost("")]
        public ActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if(loginDTO == null)
                return UnprocessableEntity(new { error = "Unprocessable Entity" });

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (loginDTO.Login != null && loginDTO.Password != null)
            {
                var user = _repository.Login(loginDTO.Login, loginDTO.Password);

                if(user == null)
                {
                    return NotFound(new { error = "Invalid Login or Password" });
                }
                else
                {
                    //buid JWT
                    var token = BuildJWT(user);

                    return Ok(token);
                }
            }
            else
            {
                return UnprocessableEntity(new { error = "Unprocessable Entity" });
            }
        }

        [HttpPost("sso")]
        public ActionResult LoginSSO([FromBody] LoginSsoDTO loginSSO)
        {
            if (loginSSO == null)
                return UnprocessableEntity(new { error = "Unprocessable Entity" });

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (loginSSO.Login != null && loginSSO.App_Token != null)
            {
                var user = _repository.LoginSSO(loginSSO.Login, loginSSO.App_Token);

                if(user == null)
                {
                    return NotFound(new { error = "Invalid Login or App Token" });
                }
                else
                {
                    //buid JWT
                    var token = BuildJWT(user);

                    return Ok(token);
                }
            }
            else
            {
                return UnprocessableEntity(new { error = "Unprocessable Entity" });
            }

            return Ok();
        }

        private TokenDTO BuildJWT(UserModel userModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Sub, userModel.UserId.ToString())
            };

            var api_key = _config.GetValue<string>("Api_Key:DefaultKey");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(api_key));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: exp,
                signingCredentials: sign
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            UserDTO userDTO = _mapper.Map<UserModel, UserDTO>(userModel);
            
            TokenDTO tokenDTO = new TokenDTO();
            tokenDTO.Token = tokenString;
            tokenDTO.User = userDTO;

            return tokenDTO;
        }
    }
}
