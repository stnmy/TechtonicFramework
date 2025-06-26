using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardProductsOverviewDto
    {
        public int TotalProductCount { get; set; }
        public int AddedThisMonthCount { get; set; }
        public int AddedThisWeekCount { get; set; }
    }
}