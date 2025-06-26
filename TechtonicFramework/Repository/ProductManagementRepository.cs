using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TechtonicFramework.Data;
using TechtonicFramework.Data.Enums;
using TechtonicFramework.Dtos.Management.DashboardRelated;
using TechtonicFramework.Dtos.Management.ProductRelated;
using TechtonicFramework.Dtos.Product;
using TechtonicFramework.Dtos.ProductList;
using TechtonicFramework.Extensions;
using TechtonicFramework.Mappers;
using TechtonicFramework.Models.OrderModels;
using TechtonicFramework.Models.ProductModels;
using TechtonicFramework.Services;
namespace TechtonicFramework.Repository
{
    public class ProductManagementRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public ProductManagementRepository()
        {
            _context = new ApplicationDbContext();
            _imageService = new ImageService();
        }

        public async Task<Product> CreateProductAsync(CreateProductDto productDto)
        {
            await ValidateForeignKeysAsync(productDto);

            var uploadedImages = new List<ProductImage>();
            foreach (var file in productDto.ProductImages)
            {
                var uploadResult = await _imageService.AddImageAsync(file);
                if (uploadResult.Error != null)
                {
                    throw new ArgumentException($"Image upload failed: {uploadResult.Error.Message}");
                }

                uploadedImages.Add(new ProductImage
                {
                    ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                    publicId = uploadResult.PublicId
                });
            }

            var product = productDto.mapToProduct(uploadedImages);

            var attributeEntities = productDto.AttributeValues.Select(attr => new ProductAttributeValue
            {
                FilterAttributeValueId = attr.FilterAttributeValueId,
                Name = attr.Name,
                Slug = HelperMethods.GenerateSlug(attr.Name),
                Value = attr.Value,
                SpecificationCategory = attr.SpecificationCategory,
                Product = product
            }).ToList();

            product.AttributeValues = attributeEntities;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<AdminProductCardPageResult> GetAdminProducts(
    string sortBy = null,
    string filters = null,
    int? pageNumber = 1,
    int? pageSize = 10,
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

                query = ProductQueryHelper.ApplyAdminOrdering(query, _context, sortBy);

                var count = await query.CountAsync();

                var paginationResult = ProductQueryHelper.ApplyPagination(query, pageNumber, pageSize);
                var actualPageNumber = paginationResult.Item1;
                var actualPageSize = paginationResult.Item2;
                var paginationQuery = paginationResult.Item3;

                var products = await paginationQuery.ToListAsync();
                var productIds = products.Select(p => p.Id).ToList();

                var completedOrderItems = await _context.OrderItems
                    .Include("Order")
                    .Where(oi => productIds.Contains(oi.ProductId))
                    .ToListAsync();

                var unitsSoldDict = completedOrderItems
                    //.Where(oi => oi.Order?.PaymentStatus == PaymentStatus.Completed)
                    .GroupBy(oi => oi.ProductId)
                    .ToDictionary(g => g.Key, g => g.Sum(oi => oi.Quantity));

                var adminDtos = products
                    .Select(p => p.toAdminProductCardDto(unitsSoldDict.TryGetValue(p.Id, out var sold) ? sold : 0))
                    .ToList();

                return new AdminProductCardPageResult
                {
                    paginationData = new PaginationDataDto
                    {
                        totalCount = count,
                        start = ((actualPageNumber - 1) * actualPageSize) + 1,
                        end = actualPageNumber * actualPageSize <= count ? actualPageNumber * actualPageSize : count,
                        pageSize = actualPageSize,
                        currentPage = actualPageNumber,
                        totalPageNumber = (int)Math.Ceiling((double)count / actualPageSize)
                    },
                    productCardDtos = adminDtos
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[Repository:GetAdminProducts] Error during admin product query: {ex.Message}", ex);
            }
        }





        public async Task<Product> UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.AttributeValues)
                .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (product == null)
                return null;

            foreach (var image in product.ProductImages)
            {
                if (!string.IsNullOrEmpty(image.publicId) && image.ImageUrl.StartsWith("https://res.cloudinary.com"))
                {
                    await _imageService.DeleteImageAsync(image.publicId);
                }
            }

            _context.ProductImages.RemoveRange(product.ProductImages);

            _context.ProductAttributeValues.RemoveRange(product.AttributeValues);

            var uploadedImages = new List<ProductImage>();
            foreach (var file in dto.ProductImages)
            {
                var uploadResult = await _imageService.AddImageAsync(file);
                if (uploadResult.Error != null)
                {
                    throw new ArgumentException($"Image upload failed: {uploadResult.Error.Message}");
                }

                uploadedImages.Add(new ProductImage
                {
                    ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                    publicId = uploadResult.PublicId
                });
            }

            var attributeEntities = dto.AttributeValues.Select(attr => new ProductAttributeValue
            {
                FilterAttributeValueId = attr.FilterAttributeValueId,
                Name = attr.Name,
                Slug = HelperMethods.GenerateSlug(attr.Name),
                Value = attr.Value,
                SpecificationCategory = attr.SpecificationCategory,
                Product = product 
            }).ToList();

            product.updateProductFromDto(dto);
            product.ProductImages = uploadedImages;
            product.AttributeValues = attributeEntities;

            await _context.SaveChangesAsync();
            return product;
        }

        private async Task ValidateForeignKeysAsync(CreateProductDto productDto)
        {
            if (!await _context.Brands.AnyAsync(b => b.Id == productDto.BrandId))
                throw new ArgumentException($"Brand with ID {productDto.BrandId} does not exist.");

            if (!await _context.Categories.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new ArgumentException($"Category with ID {productDto.CategoryId} does not exist.");

            if (productDto.SubCategoryId.HasValue)
            {
                if (!await _context.SubCategories.AnyAsync(s => s.Id == productDto.SubCategoryId.Value))
                    throw new ArgumentException($"SubCategory with ID {productDto.SubCategoryId.Value} does not exist.");
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null || product.IsDeleted)
                return false;

            product.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BrandDto>> GetBrandsAsync()
        {
            return await _context.Brands
                .Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Slug = b.Slug
                })
                .ToListAsync();
        }

        public async Task<Brand> CreateBrandAsync(CreateBrandDto dto)
        {
            var brand = new Brand
            {
                Name = dto.Name,
                Slug = HelperMethods.GenerateSlug(dto.Name)
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return brand;
        }

        public async Task<FilterAttribute> CreateFilterAttributeAsync(CreateFilterAttributeDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == "laptop");
            if (category == null)
                throw new ArgumentException("Laptop category not found.");

            var filter = new FilterAttribute
            {
                FilterName = dto.FilterName,
                FilterSlug = HelperMethods.GenerateSlug(dto.FilterName),
                CategoryId = category.Id,
                DefaultValues = dto.Values
                    .Select(v => new FilterAttributeValue { Value = v })
                    .ToList()
            };

            _context.FilterAttributes.Add(filter);
            await _context.SaveChangesAsync();

            return filter;
        }

        public async Task<FilterAttributeValue> AddFilterValueAsync(int filterId, CreateFilterAttributeValueDto dto)
        {
            var filterExists = await _context.FilterAttributes.AnyAsync(f => f.Id == filterId);
            if (!filterExists)
                throw new ArgumentException("Filter attribute not found.");

            var value = new FilterAttributeValue
            {
                FilterAttributeId = filterId,
                Value = dto.Value
            };

            _context.FilterAttributeValues.Add(value);
            await _context.SaveChangesAsync();

            return value;
        }

        public async Task<UpdateProductViewDto> GetProductForUpdateAsync(int id)
        {
            var product = await _context.Products
                .Include("ProductImages")
                .Include("AttributeValues")
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return product.toUpdateProductViewDto();
        }

        public async Task<DashboardSalesDto> GetDashboardSalesSummaryAsync()
        {
            var now = DateTime.Now;

            var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            if (startOfWeek > now)
            {
                startOfWeek = startOfWeek.AddDays(-7);
            }

            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfYear = new DateTime(now.Year, 1, 1);

            var allCurrentYearOrders = await _context.Orders
                .Where(o => o.OrderDate >= startOfYear)
                .ToListAsync();

            var allTimeCompletedOrders = await _context.Orders
                .Where(o => o.PaymentStatus == PaymentStatus.Completed)
                .ToListAsync();

            var allTimePendingOrders = await _context.Orders
                .Where(o => o.PaymentStatus == PaymentStatus.Pending)
                .ToListAsync();

            var allTimeCancelledOrders = await _context.Orders
                .Where(o => o.IsCancellationRequested && o.CancelApprovedByModeratorId != null)
                .ToListAsync();

            var salesDto = new DashboardSalesDto();

            Func<List<Order>, DateTime?, decimal> CalculateSalesTotal = (orders, startDate) =>
            {
                if (startDate.HasValue)
                {
                    return orders.Where(o => o.OrderDate >= startDate.Value).Sum(o => o.Subtotal);
                }
                return orders.Sum(o => o.Subtotal);
            };

            var completedOrders = allCurrentYearOrders.Where(o => o.PaymentStatus == PaymentStatus.Completed).ToList();
            var pendingOrders = allCurrentYearOrders.Where(o => o.PaymentStatus == PaymentStatus.Pending).ToList();
            var cancelledOrders = allCurrentYearOrders
                .Where(o => o.IsCancellationRequested && o.CancelApprovedByModeratorId != null).ToList();

            salesDto.CompletedSales.ThisWeek = CalculateSalesTotal(completedOrders, startOfWeek);
            salesDto.CompletedSales.ThisMonth = CalculateSalesTotal(completedOrders, startOfMonth);
            salesDto.CompletedSales.ThisYear = CalculateSalesTotal(completedOrders, startOfYear);
            salesDto.CompletedSales.AllTime = CalculateSalesTotal(allTimeCompletedOrders, null);

            salesDto.PendingSales.ThisWeek = CalculateSalesTotal(pendingOrders, startOfWeek);
            salesDto.PendingSales.ThisMonth = CalculateSalesTotal(pendingOrders, startOfMonth);
            salesDto.PendingSales.ThisYear = CalculateSalesTotal(pendingOrders, startOfYear);
            salesDto.PendingSales.AllTime = CalculateSalesTotal(allTimePendingOrders, null);

            salesDto.CancelledSales.ThisWeek = CalculateSalesTotal(cancelledOrders, startOfWeek);
            salesDto.CancelledSales.ThisMonth = CalculateSalesTotal(cancelledOrders, startOfMonth);
            salesDto.CancelledSales.ThisYear = CalculateSalesTotal(cancelledOrders, startOfYear);
            salesDto.CancelledSales.AllTime = CalculateSalesTotal(allTimeCancelledOrders, null);

            return salesDto;
        }

        public async Task<DashboardTopSellingProductDto> GetTopSellingProductsSummaryAsync()
        {
            var now = DateTime.Now;

            var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            if (startOfWeek > now)
            {
                startOfWeek = startOfWeek.AddDays(-7);
            }
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfYear = new DateTime(now.Year, 1, 1);

            var completedOrderItems = await _context.OrderItems
                .Include("Order")
                .ToListAsync();

            var topSellingProductsSummary = new DashboardTopSellingProductDto();

            Func<List<OrderItem>, DateTime?, int, List<TopSellingProductDto>> GetTopProducts =
                (items, startDate, count) =>
                {
                    var filteredItems = startDate.HasValue
                        ? items.Where(oi => oi.Order.OrderDate >= startDate.Value)
                        : items;

                    return filteredItems
                        .GroupBy(oi => new { oi.ProductId, oi.ProductName })
                        .Select(g => new TopSellingProductDto
                        {
                            ProductName = g.Key.ProductName,
                            UnitsSold = g.Sum(oi => oi.Quantity)
                        })
                        .OrderByDescending(x => x.UnitsSold)
                        .Take(count)
                        .ToList();
                };

            topSellingProductsSummary.ThisWeek = GetTopProducts(completedOrderItems, startOfWeek, 5);
            topSellingProductsSummary.ThisMonth = GetTopProducts(completedOrderItems, startOfMonth, 5);
            topSellingProductsSummary.ThisYear = GetTopProducts(completedOrderItems, startOfYear, 5);
            topSellingProductsSummary.AllTime = GetTopProducts(completedOrderItems, null, 5);

            return topSellingProductsSummary;
        }

        public async Task<DashboardMostVisitedProductsDto> GetMostVisitedProductsSummaryAsync()
        {
            var now = DateTime.Now;
            var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            if (startOfWeek > now) startOfWeek = startOfWeek.AddDays(-7);
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfYear = new DateTime(now.Year, 1, 1);

            var allProductVisits = await _context.ProductVisits
                .Include("Product")
                .ToListAsync();

            var summary = new DashboardMostVisitedProductsDto();

            Func<List<ProductVisit>, DateTime?, int, List<MostVisitedProductDto>> GetTopVisited = (visits, startDate, count) =>
            {
                var filtered = startDate.HasValue
                    ? visits.Where(pv => pv.VisitTime >= startDate.Value)
                    : visits;

                return filtered
                    .GroupBy(pv => new { pv.ProductId, pv.Product.Name })
                    .Select(g => new MostVisitedProductDto
                    {
                        ProductName = g.Key.Name,
                        VisitCount = g.Count()
                    })
                    .OrderByDescending(x => x.VisitCount)
                    .Take(count)
                    .ToList();
            };

            summary.ThisWeek = GetTopVisited(allProductVisits, startOfWeek, 5);
            summary.ThisMonth = GetTopVisited(allProductVisits, startOfMonth, 5);
            summary.ThisYear = GetTopVisited(allProductVisits, startOfYear, 5);
            summary.AllTime = GetTopVisited(allProductVisits, null, 5);

            return summary;
        }

        public async Task<DashboardTopCartedProductsDto> GetTopCartedProductsSummaryAsync()
        {
            var fifteenDaysAgo = DateTime.UtcNow.AddDays(-15);

            var cartItems = await _context.CartItems
                .Include("Cart")
                .Include("Product")
                .Where(ci => ci.Cart.CreatedAt >= fifteenDaysAgo)
                .ToListAsync();

            var result = new DashboardTopCartedProductsDto
            {
                TopProductsCurrentlyInActiveCarts = cartItems
                    .GroupBy(ci => new { ci.ProductId, ci.Product.Name })
                    .Select(g => new TopCartedProductDto
                    {
                        ProductName = g.Key.Name,
                        TotalQuantityCarted = g.Sum(ci => ci.Quantity)
                    })
                    .OrderByDescending(x => x.TotalQuantityCarted)
                    .Take(5)
                    .ToList()
            };

            return result;
        }

        public async Task<DashboardProductsOverviewDto> GetProductOverviewSummaryAsync()
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            if (startOfWeek > now) startOfWeek = startOfWeek.AddDays(-7);

            var result = new DashboardProductsOverviewDto
            {
                TotalProductCount = await _context.Products
                    .Where(p => !p.IsDeleted)
                    .CountAsync(),

                AddedThisMonthCount = await _context.Products
                    .Where(p => p.CreatedAt >= startOfMonth && !p.IsDeleted)
                    .CountAsync(),

                AddedThisWeekCount = await _context.Products
                    .Where(p => p.CreatedAt >= startOfWeek && !p.IsDeleted)
                    .CountAsync()
            };

            return result;
        }

        public async Task<DashboardOrderSummaryDto> GetOrderCountSummaryAsync()
        {
            var now = DateTime.Now;
            var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            if (startOfWeek > now) startOfWeek = startOfWeek.AddDays(-7);
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfYear = new DateTime(now.Year, 1, 1);

            var allOrders = await _context.Orders.ToListAsync();

            Func<List<Order>, DateTime?, OrderCountDto> CalculateOrderCounts = (orders, startDate) =>
            {
                var filtered = startDate.HasValue
                    ? orders.Where(o => o.OrderDate >= startDate.Value).ToList()
                    : orders;

                var total = filtered.Count;
                var cancelled = filtered.Count(o => o.IsCancellationRequested && o.CancelApprovedByModeratorId != null);
                decimal rate = total > 0 ? (decimal)cancelled / total * 100 : 0;

                return new OrderCountDto
                {
                    TotalOrders = total,
                    CancelledOrders = cancelled,
                    CancellationRate = Math.Round(rate, 2)
                };
            };

            var summary = new DashboardOrderSummaryDto
            {
                ThisWeek = CalculateOrderCounts(allOrders, startOfWeek),
                ThisMonth = CalculateOrderCounts(allOrders, startOfMonth),
                ThisYear = CalculateOrderCounts(allOrders, startOfYear),
                AllTime = CalculateOrderCounts(allOrders, null)
            };

            return summary;
        }

        public async Task<DashboardTopSellingBrandsDto> GetTopSellingBrandsSummaryAsync()
        {
            var now = DateTime.Now;
            var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            if (startOfWeek > now) startOfWeek = startOfWeek.AddDays(-7);
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfYear = new DateTime(now.Year, 1, 1);

            var allCompletedOrderItemsWithBrand = await _context.OrderItems
                .Include("Order")
                .Include("Product.Brand")
                .ToListAsync();

            var result = new DashboardTopSellingBrandsDto();

            Func<List<OrderItem>, DateTime?, int, List<TopSellingBrandDto>> GetTopBrands = (items, startDate, count) =>
            {
                var filtered = startDate.HasValue
                    ? items.Where(oi => oi.Order.OrderDate >= startDate.Value)
                    : items;

                return filtered
                    .Where(oi => oi.Product != null && oi.Product.Brand != null)
                    .GroupBy(oi => new { oi.Product.BrandId, oi.Product.Brand.Name })
                    .Select(g => new TopSellingBrandDto
                    {
                        BrandName = g.Key.Name,
                        UnitsSold = g.Sum(oi => oi.Quantity)
                    })
                    .OrderByDescending(x => x.UnitsSold)
                    .Take(count)
                    .ToList();
            };

            result.ThisWeek = GetTopBrands(allCompletedOrderItemsWithBrand, startOfWeek, 3);
            result.ThisMonth = GetTopBrands(allCompletedOrderItemsWithBrand, startOfMonth, 3);
            result.ThisYear = GetTopBrands(allCompletedOrderItemsWithBrand, startOfYear, 3);
            result.AllTime = GetTopBrands(allCompletedOrderItemsWithBrand, null, 3);

            return result;
        }


        public async Task<DashboardLowStockProductsDto> GetLowStockProductsSummaryAsync()
        {
            var result = new DashboardLowStockProductsDto();

            result.LaptopsLowInStock = await _context.Products
                .Where(p => !p.IsDeleted && p.StockQuantity < 10)
                .OrderBy(p => p.StockQuantity)
                .Select(p => new LowStockProductDto
                {
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<UnansweredQuestionDto>> GetUnansweredProductQuestionsAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                var questions = db.ProductQuestions
                    .Where(q => string.IsNullOrEmpty(q.Answer))
                    .OrderByDescending(q => q.CreatedAt)
                    .Select(q => new UnansweredQuestionDto
                    {
                        QuestionId = q.Id,
                        ProductId = q.Product.Id,
                        ProductName = q.Product.Name,
                        ProductImageUrl = q.Product.ProductImages.FirstOrDefault().ImageUrl,
                        Question = q.Question,
                        AskedAt = q.CreatedAt
                    });

                return await questions.ToListAsync();
            }
        }

        public async Task<bool> AnswerProductQuestionAsync(int questionId, string answer)
        {
            using (var db = new ApplicationDbContext())
            {
                var question = await db.ProductQuestions.FindAsync(questionId);
                if (question == null) return false;

                question.Answer = answer;
                await db.SaveChangesAsync();

                return true;
            }
        }

        public async Task<List<AdminProductReviewDto>> GetProductReviews(
            string searchReviewerName = null,
            string productIdString = null,
            string orderBy = null)
        {
            var query = _context.ProductReviews.AsQueryable();

            int? productId = null;
            if (!string.IsNullOrWhiteSpace(productIdString))
            {
                if (int.TryParse(productIdString, out int id))
                {
                    productId = id;
                }
                else
                {
                    return null;
                }
            }


            if (!string.IsNullOrWhiteSpace(searchReviewerName))
            {
                query = query.Where(pr => pr.ReviewerName.Contains(searchReviewerName));
            }

            if (productId.HasValue && productId.Value > 0)
            {
                query = query.Where(pr => pr.ProductId == productId.Value);
            }
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "latest":
                        query = query.OrderByDescending(pr => pr.CreatedAt);
                        break;
                    case "earliest":
                        query = query.OrderBy(pr => pr.CreatedAt);
                        break;
                    default:
                        query = query.OrderByDescending(pr => pr.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(pr => pr.CreatedAt);
            }

            var reviews = await query.ToListAsync();

            return reviews.Select(pr => pr.ToProductReviewDto()).ToList();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.ProductReviews.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _context.ProductReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AdminFilterDto>> GetAdminFiltersAsync(string categorySlug)
        {
            var category = await _context.Categories
                .Include("Filters.DefaultValues")
                .FirstOrDefaultAsync(c => c.Slug == categorySlug);

            if (category == null)
                return new List<AdminFilterDto>();

            return category.Filters
                .Select(f => new AdminFilterDto
                {
                    Id = f.Id,
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


        public async Task<bool> DeleteFilterAsync(int filterId)
        {
            var filter = await _context.FilterAttributes
                .Include(f => f.DefaultValues)
                .FirstOrDefaultAsync(f => f.Id == filterId);

            if (filter == null)
                return false;

            _context.FilterAttributeValues.RemoveRange(filter.DefaultValues);

            _context.FilterAttributes.Remove(filter);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFilterValueAsync(int valueId)
        {
            var value = await _context.FilterAttributeValues.FindAsync(valueId);

            if (value == null)
                return false;

            _context.FilterAttributeValues.Remove(value);
            await _context.SaveChangesAsync();
            return true;
        }





    }
}
