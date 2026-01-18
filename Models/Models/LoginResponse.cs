using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } // Add this
    }
}
