using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Cart
{
    public class CartDto
    {
        public string CartCookieId { get; set; }

        public List<CartItemDto> CartItems { get; set; }

        public CartDto()
        {
            CartItems = new List<CartItemDto>();
        }
    }
}