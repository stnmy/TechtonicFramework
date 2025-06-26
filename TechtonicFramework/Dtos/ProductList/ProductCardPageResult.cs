using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.ProductList
{
    public class ProductCardPageResult
    {
        public PaginationDataDto paginationData { get; set; }
        public List<ProductCardDto> productCardDtos { get; set; }
    }
}
