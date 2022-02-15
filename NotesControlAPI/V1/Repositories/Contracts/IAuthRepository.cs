using NotesControlAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Repositories.Contracts
{
    public interface IAuthRepository
    {
        UserModel Login(string email, string password);
        UserModel LoginSSO(string email, string appToken);

    }
}
