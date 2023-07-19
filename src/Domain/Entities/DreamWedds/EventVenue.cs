using BlazorHero.CleanArchitecture.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class EventVenue : AuditableEntity<int>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        [MaxLength(500)]
        public string BannerImageUrl { get; set; }
        [MaxLength(500)]
        public string Website { get; set; }
        [MaxLength(150)]
        public string OwnerName { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }
        public int? WeddingEventId { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(1500)]
        public string GoogleMapUrl { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(10)]
        public string PinCode { get; set; }

        public virtual WeddingEvent WeddingEvent { get; set; }
    }
}
