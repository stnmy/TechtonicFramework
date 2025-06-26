using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Models.OrderModels;

namespace TechtonicFramework.Models.Users
{
    public class User : IdentityUser
    {
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}