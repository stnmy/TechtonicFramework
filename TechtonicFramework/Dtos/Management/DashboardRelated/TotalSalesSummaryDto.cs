using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class TotalSalesSummaryDto
    {
        public decimal ThisWeek { get; set; }
        public decimal ThisMonth { get; set; }
        public decimal ThisYear { get; set; }
        public decimal AllTime { get; set; }
    }
}