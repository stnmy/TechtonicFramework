using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Models.ProductModels
{
    public class Category
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Slug { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
        [JsonIgnore]
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public ICollection<FilterAttribute> Filters { get; set; } = new List<FilterAttribute>();
    }
}