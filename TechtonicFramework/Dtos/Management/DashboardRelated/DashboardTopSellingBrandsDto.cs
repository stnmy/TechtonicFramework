using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardTopSellingBrandsDto
    {
        public List<TopSellingBrandDto> ThisWeek { get; set; } = new List<TopSellingBrandDto>();
        public List<TopSellingBrandDto> ThisMonth { get; set; } = new List<TopSellingBrandDto>();
        public List<TopSellingBrandDto> ThisYear { get; set; } = new List<TopSellingBrandDto>();
        public List<TopSellingBrandDto> AllTime { get; set; } = new List<TopSellingBrandDto>();
    }
}