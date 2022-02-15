using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Models
{
    public class CustomerModel
    {
        [Key]
        public int Customer_Id { get; set; }
        public int UserId { get; set; }
        public string Legal_Name { get; set; }
        public string Commercial_Name { get; set; }
        public string Cnpj { get; set; }
    }
}
