using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechtonicFramework.Dtos.Order;
using TechtonicFramework.Dtos.ProductList;

namespace TechtonicFramework.Dtos.Management.OrderRelated
{
    public class OrderPageResultDto
    {
        public List<OrderCardDto> Orders { get; set; }
        public PaginationDataDto PaginationData { get; set; }
    }
}

