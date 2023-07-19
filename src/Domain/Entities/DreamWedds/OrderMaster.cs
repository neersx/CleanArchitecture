using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    
    public partial class OrderMaster : AuditableEntity<int>
    {
        public OrderMaster()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.UserWeddingSubscriptions = new HashSet<UserWeddingSubscription>();
        }
        [Required]
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        [MaxLength(50)]
        public string OrderStatus { get; set; }
        public string BillingAddress { get; set; }
        public int? CGST { get; set; }
        public int? SGST { get; set; }
        public int? Discount { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public int? PaymentMode { get; set; }
        [MaxLength(50)]
        public string PaymentTerms { get; set; }
        [MaxLength(250)]
        public string OderNote { get; set; }
    
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<UserWeddingSubscription> UserWeddingSubscriptions { get; set; }
        //public virtual User UserMaster { get; set; }
    }
}
