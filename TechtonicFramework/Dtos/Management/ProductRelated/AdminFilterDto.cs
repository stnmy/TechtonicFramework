using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Dtos.Product;

namespace TechtonicFramework.Dtos.Management.ProductRelated
{
    public class AdminFilterDto
    {
        public int Id { get; set; }
        public string FilterName { get; set; }
        public string FilterSlug { get; set; }
        public List<FilterValueDto> Values { get; set; } = new List<FilterValueDto>();
    }
}