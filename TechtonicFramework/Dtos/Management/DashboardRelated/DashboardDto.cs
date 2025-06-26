using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.DashboardRelated
{
    public class DashboardDto
    {
        public DashboardSalesDto DashboardSales { get; set; } = new DashboardSalesDto();
        public DashboardTopSellingProductDto TopSellingProducts { get; set; } = new DashboardTopSellingProductDto();
        public DashboardMostVisitedProductsDto MostVisitedProducts { get; set; } = new DashboardMostVisitedProductsDto();
        public DashboardTopCartedProductsDto TopCartedProducts { get; set; } = new DashboardTopCartedProductsDto();
        public DashboardProductsOverviewDto ProductsOverviewDto { get; set; } = new DashboardProductsOverviewDto();
        public DashboardOrderSummaryDto OrderSummaryDto { get; set; } = new DashboardOrderSummaryDto();
        public DashboardTopSellingBrandsDto TopSellingBrands { get; set; } = new DashboardTopSellingBrandsDto();
        public DashboardLowStockProductsDto LowStockProducts { get; set; } = new DashboardLowStockProductsDto();
    }
}