using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public partial class BrideAndMaid : AuditableEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int WeddingId { get; set; }
        public bool IsBride { get; set; } = false;
        [MaxLength(250)]
        public string ImageUrl { get; set; }
        public int? RelationWithBride { get; set; }
        [MaxLength(1250)]
        public string About { get; set; }
        [MaxLength(250)]
        public string FbUrl { get; set; }
        [MaxLength(500)]
        public string GoogleUrl { get; set; }
        [MaxLength(250)]
        public string InstagramUrl { get; set; }
        [MaxLength(250)]
        public string LinkedinUrl { get; set; }

        public virtual Wedding Wedding { get; set; }
    }
}
