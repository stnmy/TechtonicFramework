using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Data.Enums;

namespace TechtonicFramework.Dtos.Order
{
    public class OrderDto
    {
        public int OrderNumber { get; set; }

        public string UserEmail { get; set; }

        public string OrderDate { get; set; }

        public string ShippingAddress { get; set; }

        public decimal Subtotal { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}