using Microsoft.EntityFrameworkCore;
using NotesControlAPI.Database;
using NotesControlAPI.V1.Helpers;
using NotesControlAPI.V1.Models;
using NotesControlAPI.V1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesControlContext _context;

        public UserRepository(NotesControlContext context)
        {
            _context = context;
        }

        public int InsertUser(UserModel user)
        {
            var cnpj = user.Cnpj;
            var isValidCnpj = CheckCNPJ.IsCnpj(cnpj);
            var cnpjReplace = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            if (!isValidCnpj)
            {
                return -1;
            }          
            if(_context.Users.Where(x => x.Cnpj == cnpjReplace).Any())
            {
                return -2;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            

            user.Password = passwordHash;
            user.IsAdmin = false;
            user.Cnpj = cnpjReplace;
            _context.Users.Add(user);
            _context.SaveChanges();

            ConfigurationModel config = new ConfigurationModel();
            config.UserId = user.UserId;
            config.Max_Revenue_Amount = 0;
            config.Sms_Notification = false;
            config.Email_Notification = false;
            _context.Configuration.Add(config);
            _context.SaveChanges();


            return user.UserId;
        }

        public UserModel GetUser(int id)
        {
            var user = _context.Users.AsNoTracking().Where(x => x.UserId == id).AsQueryable();

            return user.FirstOrDefault<UserModel>();
        }

        public bool IsAdmin(int id)
        {
            var isAdmin = _context.Users.Where(x => x.UserId == id).FirstOrDefault().IsAdmin;

            return isAdmin;
        }

        public int UpdateUser(UserModel user)
        {
            var cnpj = user.Cnpj;
            var cnpjReplace = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            var isValidCnpj = CheckCNPJ.IsCnpj(cnpj);
            user.Cnpj = cnpjReplace;

            if (!isValidCnpj)
            {
                return -1;
            }
            if (_context.Users.Where(x => x.Cnpj == cnpjReplace && x.UserId != user.UserId).Any())
            {
                return -2;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;

            _context.Users.Update(user);
            _context.SaveChanges();

            return 1;
        }
    }
}
