using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardLowStockProductsDto
    {
        public List<LowStockProductDto> LaptopsLowInStock { get; set; } = new List<LowStockProductDto>();
    }
}