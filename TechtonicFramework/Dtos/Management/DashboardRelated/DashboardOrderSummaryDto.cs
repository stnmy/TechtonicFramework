using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardOrderSummaryDto
    {
        public OrderCountDto ThisWeek { get; set; } = new OrderCountDto();
        public OrderCountDto ThisMonth { get; set; } = new OrderCountDto();
        public OrderCountDto ThisYear { get; set; } = new OrderCountDto();
        public OrderCountDto AllTime { get; set; } = new OrderCountDto();
    }
}