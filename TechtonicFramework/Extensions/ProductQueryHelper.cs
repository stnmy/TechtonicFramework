using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Data;
using TechtonicFramework.Models.ProductModels;

namespace TechtonicFramework.Extensions
{
    public static class ProductQueryHelper
    {
        public static IQueryable<Product> ApplySearch(IQueryable<Product> query, string searchValue)
        {
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                var lower = searchValue.Trim().ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(lower) ||
                    (p.Description != null && p.Description.ToLower().Contains(lower))
                );
            }
            return query;
        }

        public static IQueryable<Product> ApplyFiltering(IQueryable<Product> query, ApplicationDbContext context, List<int> filterIds)
        {
            if (filterIds == null || filterIds.Count == 0)
                return query;

            var filterPairs = context.FilterAttributeValues
                .Include("FilterAttribute")
                .Where(fv => filterIds.Contains(fv.Id))
                .Select(fv => new { Group = fv.FilterAttribute.FilterSlug, valueId = fv.Id })
                .ToList();

            var grouped = filterPairs.GroupBy(x => x.Group);

            foreach (var group in grouped)
            {
                var groupIds = group.Select(g => g.valueId).ToList();
                query = query.Where(p =>
                    p.AttributeValues.Any(av =>
                        av.FilterAttributeValueId.HasValue && groupIds.Contains(av.FilterAttributeValueId.Value)
                    )
                );
            }

            return query;
        }

        public static IQueryable<Product> ApplyAttributeFilters(IQueryable<Product> query, ApplicationDbContext context, string filters)
        {
            if (!string.IsNullOrWhiteSpace(filters))
            {
                var filterIds = filters.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(s => int.TryParse(s, out int id) ? id : -1)
                                       .Where(id => id > 0)
                                       .ToList();
                query = ApplyFiltering(query, context, filterIds);
            }
            return query;
        }

        public static IQueryable<Product> ApplyPriceRangeFilter(IQueryable<Product> query, string priceRange)
        {
            if (!string.IsNullOrWhiteSpace(priceRange))
            {
                var range = ParsePriceRange(priceRange);
                var min = range.Item1;
                var max = range.Item2;
                query = query.Where(p => (p.DiscountPrice ?? p.Price) >= min && (p.DiscountPrice ?? p.Price) <= max);
            }
            return query;
        }

        public static Tuple<int, int> ParsePriceRange(string priceRange)
        {
            var parts = priceRange.Split('-');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int min) &&
                int.TryParse(parts[1], out int max))
            {
                return Tuple.Create(min, max);
            }
            throw new ArgumentException("Invalid Price Range Format", nameof(priceRange));
        }

        public static Tuple<int, int, IQueryable<Product>> ApplyPagination(
            IQueryable<Product> query, int? pageNumber, int? pageSize, int minPageSize = 5, int maxPageSize = 20)
        {
            int actualPage = (pageNumber.HasValue && pageNumber.Value >= 1) ? pageNumber.Value : 1;
            int actualSize = (pageSize.HasValue && pageSize.Value >= minPageSize && pageSize.Value <= maxPageSize)
                ? pageSize.Value : 10;

            var paginated = query.Skip((actualPage - 1) * actualSize).Take(actualSize);
            return Tuple.Create(actualPage, actualSize, paginated);
        }

        public static IQueryable<Product> ApplyPublicOrdering(IQueryable<Product> query, string orderBy)
        {
            if (orderBy == "priceasc")
                return query.OrderBy(x => x.DiscountPrice ?? x.Price);
            if (orderBy == "pricedesc")
                return query.OrderByDescending(x => x.DiscountPrice ?? x.Price);
            if (orderBy == "ratinghigh")
                return query.OrderByDescending(x => x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 4);
            if (orderBy == "ratinglow")
                return query.OrderBy(x => x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 4);
            if (orderBy == "mostpopular")
                return query.OrderByDescending(x => x.Visits.Count);
            if (orderBy == "leastpopular")
                return query.OrderBy(x => x.Visits.Count);
            if (orderBy == "name")
                return query.OrderBy(x => x.Name);

            return query.OrderBy(x => x.Name);
        }

        public static IQueryable<Product> ApplyAdminOrdering(IQueryable<Product> query, ApplicationDbContext context, string sortBy)
        {
            switch (sortBy?.ToLower())
            {
                case "priceasc":
                    return query.OrderBy(x => x.DiscountPrice ?? x.Price);

                case "pricedesc":
                    return query.OrderByDescending(x => x.DiscountPrice ?? x.Price);

                case "ratinghigh":
                    return query.OrderByDescending(x => x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 4);

                case "ratinglow":
                    return query.OrderBy(x => x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 4);

                case "mostpopular":
                    return query.OrderByDescending(x => x.Visits.Count);

                case "leastpopular":
                    return query.OrderBy(x => x.Visits.Count);

                case "name":
                    return query.OrderBy(x => x.Name);

                case "mostsold":
                    return query.OrderByDescending(p =>
                        context.OrderItems
                            .Where(oi => oi.ProductId == p.Id)
                            .Sum(oi => (int?)oi.Quantity) ?? 0);

                case "lowstock":
                    return query.Where(p => p.StockQuantity <= 10).OrderBy(p => p.StockQuantity);

                case "instock":
                    return query.Where(p => p.StockQuantity > 0).OrderByDescending(p => p.StockQuantity);

                default:
                    return query.OrderBy(x => x.Name);
            }
        }

    }
}
