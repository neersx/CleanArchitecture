using BlazorHero.CleanArchitecture.Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public partial class SubscriptionMaster : AuditableEntity<int>
    {
        public SubscriptionMaster()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.UserWeddingSubscriptions = new HashSet<UserWeddingSubscription>();
        }
   
        public int SubsType { get; set; }
        [MaxLength(100)]
        public string SubsName { get; set; }
        [MaxLength(10)]
        public string SubsCode { get; set; }
        public int Days { get; set; }
    
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<UserWeddingSubscription> UserWeddingSubscriptions { get; set; }
    }
}
