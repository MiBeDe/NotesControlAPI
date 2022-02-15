using Microsoft.Extensions.Configuration;
using NotesControlAPI.Database;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly NotesControlContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(NotesControlContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public UserModel Login(string email, string password)
        {
            var user = _context.Users.Where(x => x.Email == email).FirstOrDefault();

            if(user == null)
            {
                return null;
            }


            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (passwordValid)
            {
                return user;
            }
            else
            {
                return null;
            }
            
        }

        public UserModel LoginSSO(string email, string appToken)
        {
            var api_key = _configuration.GetValue<string>("Api_Key:DefaultKey");

            if (email == "admin" && appToken == api_key)
            {
                UserModel userModel = new UserModel();

                userModel.UserId = 0;
                userModel.Name = "Admin";
                userModel.Email = "admin@admin.com.br";
                userModel.IsAdmin = true;

                return userModel;
            }
            else
            {
                return null;
            }

        }
    }
}
