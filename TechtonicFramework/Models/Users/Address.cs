using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Models.Users
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Line1 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }
    }
}