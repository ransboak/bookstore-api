using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore2.Models
{
    public class WhitelistedIP
    {
        public int Id { get; set; } // Primary key
        public string IPAddress { get; set; } // Stores the IP address
    }
}