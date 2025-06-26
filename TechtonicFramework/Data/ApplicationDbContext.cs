using System.Data.Entity;
using TechtonicFramework.Models.ProductModels;
using TechtonicFramework.Models.CartModels;
using Microsoft.AspNet.Identity.EntityFramework;
using TechtonicFramework.Models.Users;
using TechtonicFramework.Models.OrderModels;

namespace TechtonicFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("name=DefaultConnection") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<FilterAttributeValue> FilterAttributeValues { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductQuestion> ProductQuestions { get; set; }
        public DbSet<ProductVisit> ProductVisits { get; set; }
        public DbSet<FilterAttribute> FilterAttributes { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal")
                .HasPrecision(18, 2)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountPrice)
                .HasColumnType("decimal")
                .HasPrecision(18, 2)
                .IsOptional();

            modelBuilder.Entity<ProductAttributeValue>()
                .HasRequired(pav => pav.Product)
                .WithMany(p => p.AttributeValues)
                .HasForeignKey(pav => pav.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductVisit>()
                .HasRequired(pv => pv.Product)
                .WithMany(p => p.Visits)
                .HasForeignKey(pv => pv.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SubCategory>()
                .HasRequired(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId)
                .WillCascadeOnDelete(false);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
