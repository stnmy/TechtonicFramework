using System;
using System.Collections.Generic;
using System.Linq;
using TechtonicFramework.Dtos.Order;
using TechtonicFramework.Models.OrderModels;

namespace TechtonicFramework.Mappers
{
    public static class OrderMapper
    {
        public static OrderCardDto ToOrderCardDto(this Order order)
        {
            return new OrderCardDto
            {
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                Subtotal = order.Subtotal,
                PaymentStatus = order.PaymentStatus,
                UserEmail = order.User?.Email ?? "Mr. X"
            };
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            var address = order.Address;
            return new OrderDto
            {
                OrderNumber = order.OrderNumber,
                UserEmail = order.User?.Email ?? "Mr. X",
                OrderDate = order.OrderDate.ToString("yyyy-MM-dd HH:mm"),
                ShippingAddress = $"{address.Line1},{address.City},{address.PostalCode}",
                Subtotal = order.Subtotal,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderItems = order.OrderItems.Select(x => x.ToOrderItemDto()).ToList()
            };
        }

        public static OrderItemDto ToOrderItemDto(this OrderItem item)
        {
            return new OrderItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductImageUrl = item.Product?.ProductImages?.FirstOrDefault()?.ImageUrl ?? "cantfind.png",
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            };
        }
    }
}
