using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardSalesDto
    {
        public TotalSalesSummaryDto CompletedSales { get; set; } = new TotalSalesSummaryDto();
        public TotalSalesSummaryDto PendingSales { get; set; } = new TotalSalesSummaryDto();
        public TotalSalesSummaryDto CancelledSales { get; set; } = new TotalSalesSummaryDto();
    }
}