using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace TechtonicFramework.Models.ProductModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public int BrandId { get; set; }

        [JsonIgnore]
        public virtual Brand Brand { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }

        public int? SubCategoryId { get; set; }
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public virtual SubCategory SubCategory { get; set; }

        public int StockQuantity { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDealOfTheDay { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>();
        public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<ProductQuestion> Questions { get; set; } = new List<ProductQuestion>();
        public virtual ICollection<ProductVisit> Visits { get; set; } = new List<ProductVisit>();
    }
}
