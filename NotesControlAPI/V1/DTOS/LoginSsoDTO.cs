using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class LoginSsoDTO
    {
        public string Login { get; set; }
        public string App_Token { get; set; }

    }
}
