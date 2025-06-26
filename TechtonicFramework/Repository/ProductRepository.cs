using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos;
using TechtonicFramework.Dtos.Product;
using TechtonicFramework.Mappers;
using TechtonicFramework.Models.ProductModels;

using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using TechtonicFramework.Dtos.ProductList;
using TechtonicFramework.Extensions;

namespace TechtonicFramework.Repository
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductWithRelatedProductsDto> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.SubCategory.Category)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.AttributeValues)
                .Include(p => p.Reviews)
                .Include(p => p.Questions)
                .Include(p => p.Visits)
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            _context.ProductVisits.Add(new ProductVisit
            {
                ProductId = product.Id,
                VisitTime = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            var relatedProducts = await GetRelatedProducts(product);

            return new ProductWithRelatedProductsDto
            {
                Product = product.toProductDto(),
                RelatedProducts = relatedProducts.Select(rp => rp.toRelatedProductCardDto()).ToList()
            };
        }



        private async Task<List<Product>> GetRelatedProducts(Product product)
        {
            const decimal upperLimit = 1.10M;
            const decimal lowerLimit = 0.91M;

            decimal lowerPrice = product.DiscountPrice.HasValue
                ? product.DiscountPrice.Value * lowerLimit
                : product.Price * lowerLimit;

            decimal upperPrice = product.DiscountPrice.HasValue
                ? product.DiscountPrice.Value * upperLimit
                : product.Price * upperLimit;

            return await _context.Products
                .Where(p => !p.IsDeleted)
                .Where(p => p.Id != product.Id)
                .Where(p => p.CategoryId == product.CategoryId)
                .Where(p =>
                    p.DiscountPrice.HasValue
                    ? p.DiscountPrice.Value >= lowerPrice && p.DiscountPrice.Value <= upperPrice
                    : p.Price >= lowerPrice && p.Price <= upperPrice)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.Reviews.Any()
                    ? p.Reviews.Average(r => r.Rating)
                    : 0)
                .Take(4)
                .ToListAsync();
        }


        public async Task<ProductCardPageResult> GetProducts(
    string orderBy = null,
    string filters = null,
    int? pageNumber = 1,
    int? pageSize = 5,
    string search = null,
    string priceRange = null)
        {
            try
            {
                var query = _context.Products
                    .Where(p => !p.IsDeleted)
                    .Include("ProductImages")
                    .Include("AttributeValues")
                    .Include("Reviews")
                    .Include("Visits");

                query = ProductQueryHelper.ApplyPriceRangeFilter(query, priceRange);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = ProductQueryHelper.ApplySearch(query, search);
                }
                else if (!string.IsNullOrWhiteSpace(filters))
                {
                    query = ProductQueryHelper.ApplyAttributeFilters(query, _context, filters);
                }

                query = ProductQueryHelper.ApplyPublicOrdering(query, orderBy);

                var count = await query.CountAsync();

                var paginationResult = ProductQueryHelper.ApplyPagination(query, pageNumber, pageSize);
                var actualPageNumber = paginationResult.Item1;
                var actualPageSize = paginationResult.Item2;
                var paginationQuery = paginationResult.Item3;

                var products = await paginationQuery.ToListAsync();
                return ProductMapper.toProductCardPageResult(count, actualPageNumber, actualPageSize, products);
            }
            catch (Exception ex)
            {
                throw new Exception($"[Repository:GetProducts] Error during product query: {ex.Message}", ex);
            }
        }

        public async Task AddReviewAsync(int productId, string reviewerEmail, ProductReviewPostDto dto)
        {
            using (var db = new ApplicationDbContext())
            {
                var product = await db.Products.FindAsync(productId);
                if (product == null) return;

                
                var reviewerName = reviewerEmail.Contains("@")
                    ? reviewerEmail.Substring(0, reviewerEmail.IndexOf("@"))
                    : reviewerEmail;

                var review = new ProductReview
                {
                    ProductId = productId,
                    ReviewerName = reviewerName,
                    Comment = dto.Comment,
                    Rating = dto.Rating,
                    CreatedAt = DateTime.UtcNow
                };

                db.ProductReviews.Add(review);
                await db.SaveChangesAsync();
            }
        }


        public async Task<List<Product>> GetProductsBySlugs(string categorySlug, string subCategorySlug, string brandSlug, List<int> filterIds) // Removed '?'
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p => p.AttributeValues)
                .Include(p => p.ProductImages)
                .AsQueryable();

            if (!string.IsNullOrEmpty(categorySlug))
            {
                query = query.Where(p => p.Category.Slug.ToLower() == categorySlug.ToLower());
            }
            if (!string.IsNullOrEmpty(brandSlug))
            {
                query = query.Where(p => p.Brand.Slug.ToLower() == brandSlug.ToLower());
            }
            if (!string.IsNullOrEmpty(subCategorySlug))
            {
                query = query.Where(p => p.SubCategory != null && p.SubCategory.Slug.ToLower() == subCategorySlug.ToLower());
            }

            if (filterIds != null && filterIds.Count > 0 && !string.IsNullOrEmpty(categorySlug))
            {
                var filterValues = await _context.Categories
                    .Where(c => c.Slug.ToLower() == categorySlug.ToLower())
                    .SelectMany(c => c.Filters)
                    .SelectMany(fa => fa.DefaultValues)
                    .Where(fav => filterIds.Contains(fav.Id))
                    .Select(fav => fav.Value)
                    .ToListAsync();

                if (filterValues.Count == 0)
                {
                    return new List<Product>();
                }
                query = query.Where(p => p.AttributeValues
                    .Any(av =>
                        av.FilterAttributeValueId.HasValue && filterIds.Contains(av.FilterAttributeValueId.Value) 
                    )
                );
            }
            return await query.ToListAsync();
        }


        public async Task<List<FilterDto>> GetFiltersAttributesAsync(string categorySlug)
        {
            var category = await _context.Categories
                .Include("Filters.DefaultValues")
                .FirstOrDefaultAsync(c => c.Slug == categorySlug);

            if (category == null)
                return new List<FilterDto>();

            return category.Filters
                .Select(f => new FilterDto
                {
                    FilterName = f.FilterName,
                    FilterSlug = f.FilterSlug,
                    Values = f.DefaultValues
                        .Select(df => new FilterValueDto
                        {
                            Id = df.Id,
                            Value = df.Value
                        })
                        .ToList()
                })
                .ToList();
        }

        public async Task<PriceRangeDto> GetPriceRangeAsync(string categorySlug)
        {
            var products = await _context.Products
                .Where(p => p.Category.Slug == categorySlug && !p.IsDeleted)
                .ToListAsync();

            if (!products.Any())
                return null;

            return new PriceRangeDto
            {
                minPrice = (int)products.Min(p => p.Price),
                maxPrice = (int)products.Max(p => p.Price)
            };
        }


        public async Task<Product> GetDealOfTheDayAsync()
        {
            var product = await _context.Products
                .Include(p => p.AttributeValues)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.IsDealOfTheDay == true);
            return product;
        }

        public async Task<List<Product>> GetMostVisitedProductsAsync(int count, DateTime? fromDate = null)
        {
            var visitedProductsQuery = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Visits)
                .Include(p => p.AttributeValues)
                .Where(p => fromDate == null || p.Visits.Any(v => v.VisitTime >= fromDate))
                .OrderByDescending(p => p.Visits.Count)
                .Take(count);

            var visitedProducts = await visitedProductsQuery.ToListAsync();

            if (visitedProducts.Count >= count)
            {
                return visitedProducts;
            }

            var visitedProductIds = visitedProducts.Select(p => p.Id).ToList();

            var additionalProductsNeeded = count - visitedProducts.Count;
            var additionalProducts = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.AttributeValues)
                .Where(p => !visitedProductIds.Contains(p.Id))
                .OrderBy(p => p.Id)
                .Take(additionalProductsNeeded)
                .ToListAsync();

            return visitedProducts.Concat(additionalProducts).ToList();
        }

        private Tuple<int, int> parsePriceRange(string priceRange)
        {
            if (!string.IsNullOrWhiteSpace(priceRange))
            {
                var priceRangeParts = priceRange.Split('-');
                if (priceRangeParts.Length == 2)
                {
                    int minPrice, maxPrice;
                    if (int.TryParse(priceRangeParts[0], out minPrice) &&
                        int.TryParse(priceRangeParts[1], out maxPrice))
                    {
                        return Tuple.Create(minPrice, maxPrice);
                    }
                }
            }
            throw new ArgumentException("Invalid Price Range Format", nameof(priceRange));
        }

        public async Task<ProductQuestion> AskQuestionAsync(int productId, string question)
        {

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return null;
            }

            var productQuestion = new ProductQuestion
            {
                ProductId = productId,
                Question = question,
                CreatedAt = DateTime.UtcNow
            };

            _context.ProductQuestions.Add(productQuestion);
            await _context.SaveChangesAsync();
            return productQuestion;
        }

        public async Task<List<Product>> GetTopDiscountedProductsAsync(int count = 4)
        {
            var products = await _context.Products
                .Where(p => p.DiscountPrice.HasValue && !p.IsDeleted && p.Price > 0) 
                .OrderByDescending(p => (p.Price - p.DiscountPrice.Value) / p.Price) 
                .Take(count)
                .Include(p => p.ProductImages)
                .ToListAsync();

            return products;
        }


        public async Task<List<Product>> GetAllDiscountedProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.DiscountPrice.HasValue && !p.IsDeleted && p.Price > 0) 
                .OrderByDescending(p => (p.Price - p.DiscountPrice.Value) / p.Price)
                .Include(p => p.ProductImages)
                .ToListAsync();

            return products;
        }
    }
}