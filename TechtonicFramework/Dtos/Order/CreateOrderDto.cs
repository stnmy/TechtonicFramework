using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TechtonicFramework.Data.Enums;
using TechtonicFramework.Models.Users;

namespace TechtonicFramework.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public Address ShippingAddress { get; set; }

        public bool IsCustomShippingAddress { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}