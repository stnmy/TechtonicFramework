using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos;
using TechtonicFramework.Mappers;
using TechtonicFramework.Models.CartModels;
using TechtonicFramework.Repository;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly CartRepository _cartRepository;

        public CartController()
        {
            _cartRepository = new CartRepository(new ApplicationDbContext());
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCart()
        {
            var cart = await RetrieveCart();
            if (cart == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(cart.toCartDto());
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddItemToCart(int productId, int quantity)
        {
            var cart = await RetrieveCart() ?? CreateCart();

            var product = await _cartRepository.GetProduct(productId);
            if (product == null)
            {
                return BadRequest("Could not add product to cart");
            }

            cart.AddCartItem(product, quantity);
            var result = await _cartRepository.SaveChangesAsync();
            if (result)
            {
                return Created("api/cart", cart.toCartDto());
            }

            return BadRequest("Product is already in the cart");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteItemFromCart(int productId, int quantity)
        {
            var cart = await RetrieveCart();
            if (cart == null)
            {
                return BadRequest("Unable to retrieve cart.");
            }

            var removedItem = cart.RemoveItem(productId, quantity);
            if (removedItem != null)
            {
                _cartRepository.RemoveCartItem(removedItem);
            }

            var result = await _cartRepository.SaveChangesAsync();
            if (result)
            {
                return Ok();
            }

            return BadRequest("Problem updating the cart.");
        }

        private Cart CreateCart()
        {
            var cartCookieId = Guid.NewGuid().ToString();

            var cookie = new HttpCookie("cartCookieId", cartCookieId)
            {
                Expires = DateTime.UtcNow.AddDays(30),
                HttpOnly = true
            };

            HttpContext.Current.Response.Cookies.Add(cookie);

            var cart = new Cart { CartCookieId = cartCookieId };
            _cartRepository.AddCart(cart);

            return cart;
        }

        private async Task<Cart> RetrieveCart()
        {
            var cookie = HttpContext.Current.Request.Cookies["cartCookieId"];
            if (cookie == null)
            {
                return null;
            }

            return await _cartRepository.RetrieveCart(cookie.Value);
        }
    }
}
