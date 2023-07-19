using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public partial class UserWeddingSubscription : AuditableEntity<int>
    {
        public int UserId { get; set; }
        public int TemplateId { get; set; }
        public int? InvoiceNo { get; set; }
        public int? WeddingId { get; set; }
        public int SubscriptionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(250)]
        public string ReasonOfUpdate { get; set; }
        public int SubscriptionStatus { get; set; }
    
        public virtual OrderMaster OrderMaster { get; set; }
        public virtual SubscriptionMaster SubscriptionMaster { get; set; }
        public virtual TemplateMaster TemplateMaster { get; set; }
        public virtual Wedding Wedding { get; set; }
    }
}
