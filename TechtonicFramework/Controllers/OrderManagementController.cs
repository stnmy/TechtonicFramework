using System;
using System.Threading.Tasks;
using System.Web.Http;
using TechtonicFramework.Repository;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos.Management.OrderRelated;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/orderManagement")]
    public class OrderManagementController : ApiController
    {
        private readonly OrderManagementRepository _orderManagementRepository;

        public OrderManagementController()
        {
            _orderManagementRepository = new OrderManagementRepository(new ApplicationDbContext());
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetOrders(
            string status = null,
            string search = null,
            string period = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                var result = await _orderManagementRepository.GetOrders(status, search, period, pageNumber, pageSize);

                if (result?.Orders == null || result.Orders.Count == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[OrderManagement:GetOrders] Error: {ex.Message}", ex));
            }
        }

        [HttpPut]
        [Route("{orderNumber:int}/status")]
        public async Task<IHttpActionResult> UpdateStatusByOrderNumber(int orderNumber, UpdateOrderStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _orderManagementRepository.UpdateOrderStatusByOrderNumber(orderNumber, dto.Status);

            if (!success)
                return BadRequest("Order not found or invalid status.");

            return Ok(new
            {
                message = "Order status updated successfully",
                orderNumber = orderNumber,
                newStatus = dto.Status
            });
        }
    }
}
