using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Cart
{
    public class CartItemDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }
    }
}