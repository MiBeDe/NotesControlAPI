using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesControlAPI.V1.DTOS
{
    public class ConfigurationDTO
    {
        public double Max_Revenue_Amount { get; set; }
        public bool Sms_Notification { get; set; }
        public bool Email_Notification { get; set; }

    }
}
