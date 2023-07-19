using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public partial class OrderDetail : AuditableEntity<int>
    {
        public int? Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderID { get; set; }
        public int TemplateID { get; set; }
        public Nullable<int> SubscrptionID { get; set; }
    
        public virtual OrderMaster OrderMaster { get; set; }
        public virtual SubscriptionMaster SubscriptionMaster { get; set; }
        public virtual TemplateMaster TemplateMaster { get; set; }
    }
}
