using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.Models
{
    public class ConfigurationModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Max_Revenue_Amount { get; set; }
        public bool Sms_Notification { get; set; }
        public bool Email_Notification { get; set; }
    }
}
