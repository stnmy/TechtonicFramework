using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class OrderCountDto
    {
        public int TotalOrders { get; set; }
        public int CancelledOrders { get; set; }
        public decimal CancellationRate { get; set; }
    }
}