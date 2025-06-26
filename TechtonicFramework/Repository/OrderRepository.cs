using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos.Order;
using TechtonicFramework.Mappers;
using TechtonicFramework.Models.CartModels;
using TechtonicFramework.Models.OrderModels;
using TechtonicFramework.Models.Users;
using TechtonicFramework.Data.Enums;

namespace TechtonicFramework.Repository
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderCardDto>> GetOrders(string username)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Email == username)
                .ToListAsync();

            return orders.Select(o => o.ToOrderCardDto()).ToList();
        }

        public async Task<OrderDto> GetOrderDetails(string username, int id)
        {
            var order = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.OrderItems.Select(oi => oi.Product.ProductImages))
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.User.Email == username && o.OrderNumber == id);

            return order?.ToOrderDto();
        }

        public async Task<Order> CreateOrder(string cartCookieId, string userId, CreateOrderDto dto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems.Select(ci => ci.Product))
                .FirstOrDefaultAsync(c => c.CartCookieId == cartCookieId);

            if (cart == null || !cart.CartItems.Any())
                return null;

            var orderItems = CreateOrderItems(cart.CartItems.ToList());
            if (orderItems == null) return null;

            var subtotal = orderItems.Sum(x => x.UnitPrice * x.Quantity);
            var addressId = await GetDeliveryAddress(userId, dto);

            var order = new Order
            {
                OrderNumber = await GetNextOrderNumber(),
                UserId = userId,
                OrderDate = DateTime.Now,
                AddressId = addressId,
                IsCustomShippingAddress = dto.IsCustomShippingAddress,
                Subtotal = subtotal,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentMethod == PaymentMethod.CashOnDelivery ? PaymentStatus.Pending : PaymentStatus.Completed,
                Status = OrderStatus.pending,
                OrderItems = new List<OrderItem>()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
                order.OrderItems.Add(item);
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == order.Id);
        }

        private async Task<int> GetDeliveryAddress(string userId, CreateOrderDto dto)
        {
            if (dto.IsCustomShippingAddress)
            {
                var address = new Address
                {
                    Line1 = dto.ShippingAddress.Line1,
                    City = dto.ShippingAddress.City,
                    PostalCode = dto.ShippingAddress.PostalCode
                };
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                return address.Id;
            }
            else
            {
                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                return user?.AddressId ?? 0;
            }
        }

        private List<OrderItem> CreateOrderItems(List<CartItem> cartItems)
        {
            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cartItems)
            {
                if (cartItem.Product.StockQuantity < cartItem.Quantity)
                    return null;

                var item = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.Product.Name,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price,
                    TotalPrice = cartItem.Quantity * cartItem.Product.Price
                };
                orderItems.Add(item);
                cartItem.Product.StockQuantity -= cartItem.Quantity;
            }
            return orderItems;
        }

        private async Task<int> GetNextOrderNumber()
        {
            var lastOrder = await _context.Orders.OrderByDescending(o => o.OrderNumber).FirstOrDefaultAsync();
            return lastOrder != null ? lastOrder.OrderNumber + 1 : 1000;
        }
    }
}
