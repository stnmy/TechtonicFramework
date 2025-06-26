using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Dtos.Product;

namespace TechtonicFramework.Dtos.ProductList
{
    public class ProductCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Image { get; set; }
        public List<ProductAttributeValueDto> Attributes { get; set; } = new List<ProductAttributeValueDto>();
    }
}