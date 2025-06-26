using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class LowStockProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
    }
}