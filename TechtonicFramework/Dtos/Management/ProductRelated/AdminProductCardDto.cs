using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Dtos.Product;
using TechtonicFramework.Dtos.ProductList;

namespace TechtonicFramework.Dtos.Management.ProductRelated
{
    public class AdminProductCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Image { get; set; }
        public List<ProductAttributeValueDto> Attributes { get; set; }

        public int StockQuantity { get; set; }
        public int UnitsSold { get; set; }
    }

    public class AdminProductCardPageResult
    {
        public PaginationDataDto paginationData { get; set; }
        public List<AdminProductCardDto> productCardDtos { get; set; }
    }
}