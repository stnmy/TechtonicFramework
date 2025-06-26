using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos;
using TechtonicFramework.Dtos.Order;
using TechtonicFramework.Models.OrderModels;
using TechtonicFramework.Dtos.Management.OrderRelated;
using TechtonicFramework.Dtos.ProductList;
using TechtonicFramework.Data.Enums;

namespace TechtonicFramework.Repository
{
    public class OrderManagementRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderManagementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderPageResultDto> GetOrders(
            string status,
            string search,
            string period,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Orders.Include(o => o.User).AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
            {
                var lowerStatus = status.ToLower();
                if (Enum.TryParse(lowerStatus, true, out OrderStatus parsedStatus))
                {
                    query = query.Where(o => o.Status == parsedStatus);
                }
            }

            if (!string.IsNullOrWhiteSpace(search) && int.TryParse(search, out int orderNumber))
            {
                query = query.Where(o => o.OrderNumber == orderNumber);
            }

            if (!string.IsNullOrWhiteSpace(period))
            {
                DateTime now = DateTime.UtcNow;
                DateTime fromDate = DateTime.MinValue;
                var lowerPeriod = period.ToLower();

                if (lowerPeriod == "lastweek")
                    fromDate = now.AddDays(-7);
                else if (lowerPeriod == "lastmonth")
                    fromDate = now.AddMonths(-1);
                else if (lowerPeriod == "lastyear")
                    fromDate = now.AddYears(-1);

                if (fromDate != DateTime.MinValue)
                {
                    query = query.Where(o => o.OrderDate >= fromDate);
                }
            }

            var totalCount = await query.CountAsync();

            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var orderDtos = orders.Select(o => new OrderCardDto
            {
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                Subtotal = o.Subtotal,
                PaymentStatus = o.PaymentStatus,
                Status = o.Status,
                UserEmail = o.User.Email
            }).ToList();

            return new OrderPageResultDto
            {
                Orders = orderDtos,
                PaginationData = new PaginationDataDto
                {
                    totalCount = totalCount,
                    start = ((pageNumber - 1) * pageSize) + 1,
                    end = Math.Min(pageNumber * pageSize, totalCount),
                    pageSize = pageSize,
                    currentPage = pageNumber,
                    totalPageNumber = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            };
        }

        public async Task<bool> UpdateOrderStatusByOrderNumber(int orderNumber, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
            if (order == null)
                return false;

            if (!Enum.TryParse(status, true, out OrderStatus parsedStatus))
                return false;

            order.Status = parsedStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
