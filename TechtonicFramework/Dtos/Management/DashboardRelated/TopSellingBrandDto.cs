using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class TopSellingBrandDto
    {
        public string BrandName { get; set; } = string.Empty;
        public int UnitsSold { get; set; }
    }
}