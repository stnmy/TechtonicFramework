using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TechtonicFramework.Data;
using TechtonicFramework.Models.CartModels;
using TechtonicFramework.Models.ProductModels;

namespace TechtonicFramework.Repository
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _context.Products
                .Include("Brand")
                .Include("Category")
                .Include("ProductImages")
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Cart> RetrieveCart(string cartCookieId)
        {
            return await _context.Carts
                .Include("CartItems.Product.Brand")
                .Include("CartItems.Product.Category")
                .Include("CartItems.Product.ProductImages")
                .FirstOrDefaultAsync(x => x.CartCookieId == cartCookieId);
        }

        public void RemoveCartItem(CartItem item)
        {
            _context.CartItems.Remove(item);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
