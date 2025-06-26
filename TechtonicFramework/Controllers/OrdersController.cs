using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos.Order;
using TechtonicFramework.Mappers;
using TechtonicFramework.Models.OrderModels;
using TechtonicFramework.Repository;
using TechtonicFramework.Extensions;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly OrderRepository _orderRepository;

        public OrdersController()
        {
            _orderRepository = new OrderRepository(new ApplicationDbContext());
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetOrders()
        {
            var username = HttpContext.Current.User.GetUsername();
            var orderDtos = await _orderRepository.GetOrders(username);

            if (orderDtos == null || orderDtos.Count == 0)
            {
                return NotFound();
            }

            return Ok(orderDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetOrderDetails(int id)
        {
            var username = HttpContext.Current.User.GetUsername();
            var orderDto = await _orderRepository.GetOrderDetails(username, id);

            if (orderDto == null)
            {
                return NotFound();
            }

            return Ok(orderDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateOrder(CreateOrderDto orderDto)
        {
            var cartId = HttpContext.Current.Request.Cookies["cartCookieId"]?.Value;
            var userId = HttpContext.Current.User.GetUserId(); 

            if (string.IsNullOrEmpty(cartId))
            {
                return BadRequest("Invalid Cart");
            }

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var order = await _orderRepository.CreateOrder(cartId, userId, orderDto);

            if (order == null)
            {
                return BadRequest("Order could not be created. Check stock or cart status.");
            }

            return Created($"api/orders/{order.Id}", order.ToOrderDto());
        }
    }
}
