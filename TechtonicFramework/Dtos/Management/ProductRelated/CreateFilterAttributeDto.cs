using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.ProductRelated
{
    public class CreateFilterAttributeDto
    {
        public string FilterName { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>();
    }
}