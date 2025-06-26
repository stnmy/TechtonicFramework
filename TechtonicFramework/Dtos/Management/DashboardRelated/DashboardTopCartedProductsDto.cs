using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardTopCartedProductsDto
    {
        public List<TopCartedProductDto> TopProductsCurrentlyInActiveCarts { get; set; } = new List<TopCartedProductDto>();
    }
}