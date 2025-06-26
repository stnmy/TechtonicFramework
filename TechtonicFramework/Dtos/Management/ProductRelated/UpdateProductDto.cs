using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.ProductRelated
{
    public class UpdateProductDto
    {
        [Required]
        public int Id { get; set; }

        public decimal? DiscountPrice { get; set; }

        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, long.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsDealOfTheDay { get; set; }

        [Required]
        public List<HttpPostedFileBase> ProductImages { get; set; } = new List<HttpPostedFileBase>();

        public List<ProductAttributeValueCreateDto> AttributeValues { get; set; } = new List<ProductAttributeValueCreateDto>();
    }

    public class UpdateProductViewDto
    {
        public int Id { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDealOfTheDay { get; set; }

        public List<string> ProductImageUrls { get; set; } = new List<string>();
        public List<ProductAttributeValueCreateDto> AttributeValues { get; set; } = new List<ProductAttributeValueCreateDto>();
    }
}
