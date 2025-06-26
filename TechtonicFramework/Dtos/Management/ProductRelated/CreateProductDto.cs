using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.ProductRelated
{
    public class CreateProductDto
    {
        [Required]
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
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public List<HttpPostedFileBase> ProductImages { get; set; } = new List<HttpPostedFileBase>();

        public List<ProductAttributeValueCreateDto> AttributeValues { get; set; } = new List<ProductAttributeValueCreateDto>();
    }

    public class ProductAttributeValueCreateDto
    {
        public int? FilterAttributeValueId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string SpecificationCategory { get; set; }
    }
    public class ProductImageCreateDto
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}
 