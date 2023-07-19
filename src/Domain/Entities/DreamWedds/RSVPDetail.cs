using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class RsvpDetail : AuditableEntity<int>
    {
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string MiddleName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
        public bool IsComing { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? GuestCount { get; set; }
        public byte PreferredFood { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }
        [MaxLength(1000)]
        public string SpecialNote { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public int WeddingID { get; set; }
        [MaxLength(150)]
        public string ParticipatingInEvents { get; set; }
        [MaxLength(150)]
        public string ComingFromCity { get; set; }
        public int? PreferredStayIn { get; set; }
    
        public virtual Wedding Wedding { get; set; }
    }
}
