using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface IUserRepository
    {
        int InsertUser(UserModel user);
        UserModel GetUser(int id);
        bool IsAdmin(int id);
        int UpdateUser(UserModel user);
    }
}
