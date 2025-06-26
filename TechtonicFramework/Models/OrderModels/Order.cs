using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TechtonicFramework.Data.Enums;
using TechtonicFramework.Models.Users;

namespace TechtonicFramework.Models.OrderModels
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public bool IsCustomShippingAddress { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public OrderStatus Status { get; set; }

        public bool IsCancellationRequested { get; set; }

        public int? CancelApprovedByModeratorId { get; set; }

        public bool IsRefundRequested { get; set; }

        public int? RefundApprovedByModeratorId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}