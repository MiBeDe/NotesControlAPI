using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public UserDTO User { get; set; }
    }
}
