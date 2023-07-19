using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{   
    public partial class WeddingEvent : AuditableEntity<int>
    {
        public WeddingEvent()
        {
            this.Venue = new EventVenue();
        }

        public DateTime EventDate { get; set; }
        [MaxLength(150)]
        public string OrganiserName { get; set; }
        [MaxLength(20)]
        public string OrganiserMobile { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public int WeddingId { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [MaxLength(1500)]
        public string AboutEvent { get; set; }
        [MaxLength(500)]
        public string BackGroundImage { get; set; }
    
        public virtual Wedding Wedding { get; set; }
        public virtual EventVenue Venue { get; set; }
    }
}
