using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Models.ProductModels
{
    public class FilterAttribute
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string FilterName { get; set; } = string.Empty;
        public string FilterSlug { get; set; } = string.Empty;
        public List<FilterAttributeValue> DefaultValues { get; set; } = new List<FilterAttributeValue>();
    }
}