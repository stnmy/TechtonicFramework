using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.ProductRelated
{
    public class UnansweredQuestionDto
    {
        public int QuestionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public string Question { get; set; }
        public DateTime AskedAt { get; set; }
    }
}
