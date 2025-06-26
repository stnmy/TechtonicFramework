using System;
using System.Collections.Generic;
using System.Linq;
using TechtonicFramework.Models.ProductModels;

namespace TechtonicFramework.Models.CartModels
{
    public class Cart
    {
        public int Id { get; set; }
        public string CartCookieId { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public DateTime CreatedAt { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
            CreatedAt = DateTime.UtcNow;
        }

        public void AddCartItem(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Quantity should be greater than zero", nameof(quantity));

            var existingItem = FindItem(product.Id);

            if (existingItem == null)
            {
                CartItems.Add(new CartItem
                {
                    Product = product,
                    ProductId = product.Id,
                    Quantity = quantity
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }
        }

        public CartItem RemoveItem(int productId, int quantity)
        {
            var item = FindItem(productId);
            if (item == null) return null;

            item.Quantity -= quantity;
            if (item.Quantity <= 0)
            {
                CartItems.Remove(item);
                return item;
            }

            return null;
        }

        private CartItem FindItem(int productId)
        {
            return CartItems.FirstOrDefault(item => item.ProductId == productId);
        }
    }
}
