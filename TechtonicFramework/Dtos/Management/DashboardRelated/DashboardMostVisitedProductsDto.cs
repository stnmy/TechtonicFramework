using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardMostVisitedProductsDto
    {
        public List<MostVisitedProductDto> ThisWeek { get; set; } = new List<MostVisitedProductDto>();
        public List<MostVisitedProductDto> ThisMonth { get; set; } = new List<MostVisitedProductDto>();
        public List<MostVisitedProductDto> ThisYear { get; set; } = new List<MostVisitedProductDto>();
        public List<MostVisitedProductDto> AllTime { get; set; } = new List<MostVisitedProductDto>();
    }
}