using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Models.ProductModels
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? FilterAttributeValueId { get; set; }
        public virtual Product Product { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Value { get; set; }
        public string SpecificationCategory { get; set; }
    }
}