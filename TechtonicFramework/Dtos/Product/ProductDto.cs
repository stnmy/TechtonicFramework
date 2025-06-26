using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Product
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // Removed 'required' and '!'
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDealOfTheDay { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        // Changed new() to new List<string>()
        public List<string> Images { get; set; } = new List<string>();
        // Changed new() to new List<ProductAttributeValueDto>()
        public List<ProductAttributeValueDto> Attributes { get; set; } = new List<ProductAttributeValueDto>();
        // Changed new() to new List<ReviewDto>()
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        // Changed new() to new List<QuestionDto>()
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
        public int VisitCount { get; set; }
    }

    public class ProductAttributeValueDto
    {
        // Removed 'required' and '!'
        public string Name { get; set; }
        public string Value { get; set; }
        public string SpecificationCategory { get; set; }
    }

    public class ProductReviewPostDto
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
    }

    public class ReviewDto
    {
        // Removed 'required' and '!'
        public string ReviewerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FilterDto
    {

        public string FilterName { get; set; }
        public string FilterSlug { get; set; }
        // Changed new() to new List<FilterValueDto>()
        public List<FilterValueDto> Values { get; set; } = new List<FilterValueDto>();
    }

    public class FilterValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty; // String.Empty is fine
    }

    public class PriceRangeDto
    {
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
    }

    public class TotalFilterDto
    {
        public PriceRangeDto priceRangeDto { get; set; }
        public List<FilterDto> filterDtos { get; set; } = new List<FilterDto>();
    }

    public class ProductQuestionDto
    {
        public string Question { get; set; } = string.Empty;
    }

    public class ProductWithRelatedProductsDto
    {
        public ProductDetailDto Product { get; set; }
        public List<RelatedProductCardDto> RelatedProducts { get; set; }
    }
    public class RelatedProductCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Image { get; set; }
    }
}