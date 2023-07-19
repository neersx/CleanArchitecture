using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class WeddingGalleryImages : AuditableEntity<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int WeddingId { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public DateTime? DateTaken { get; set; }
        [MaxLength(100)]
        public string Place { get; set; }

        public virtual Wedding Wedding { get; set; }
    }
}
