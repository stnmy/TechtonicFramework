using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Data.Enums;

namespace TechtonicFramework.Dtos.Order
{
    public class OrderCardDto
    {
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Subtotal { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public OrderStatus Status { get; set; }

        public string UserEmail { get; set; }
    }
}