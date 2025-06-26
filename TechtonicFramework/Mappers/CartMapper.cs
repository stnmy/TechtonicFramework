using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Dtos.Cart;
using TechtonicFramework.Models.CartModels;

namespace TechtonicFramework.Mappers
{
    public static class CartMapper
    {
        public static CartDto toCartDto(this Cart cart)
        {
            return new CartDto
            {

                CartCookieId = cart.CartCookieId,
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    ProductId = item.Product.Id,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.ProductImages.FirstOrDefault()?.ImageUrl ?? "",
                    Brand = item.Product.Brand?.Name ?? "Unknown",
                    Category = item.Product.Category?.Name ?? "Unknown",
                    Quantity = item.Quantity
                }).ToList()
            };

        }
    }
}
