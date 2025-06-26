using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class MostVisitedProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int VisitCount { get; set; }
    }
}