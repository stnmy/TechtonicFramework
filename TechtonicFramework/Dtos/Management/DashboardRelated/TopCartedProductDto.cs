using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class TopCartedProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantityCarted { get; set; }
    }
}