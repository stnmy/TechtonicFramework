using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardTopSellingProductDto
    {
        public List<TopSellingProductDto> ThisWeek { get; set; } = new List<TopSellingProductDto>();
        public List<TopSellingProductDto> ThisMonth { get; set; } = new List<TopSellingProductDto>();
        public List<TopSellingProductDto> ThisYear { get; set; } = new List<TopSellingProductDto>();
        public List<TopSellingProductDto> AllTime { get; set; } = new List<TopSellingProductDto>();
    }
}