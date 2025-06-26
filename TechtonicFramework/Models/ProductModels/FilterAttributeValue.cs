using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Models.ProductModels
{
    public class FilterAttributeValue
    {
        public int Id { get; set; }
        public int FilterAttributeId { get; set; }
        public string Value { get; set; } = string.Empty;
        public FilterAttribute FilterAttribute { get; set; }
    }
}