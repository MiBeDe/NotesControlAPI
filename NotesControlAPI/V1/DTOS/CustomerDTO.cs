using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class CustomerDTO
    {
        public string Legal_Name { get; set; }
        public string Commercial_Name { get; set; }
        public string Cnpj { get; set; }

    }
}
