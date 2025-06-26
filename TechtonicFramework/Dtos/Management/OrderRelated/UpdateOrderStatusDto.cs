using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.OrderRelated
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public string Status { get; set; }
    }
}