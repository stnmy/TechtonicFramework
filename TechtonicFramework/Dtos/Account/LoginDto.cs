using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Account
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}