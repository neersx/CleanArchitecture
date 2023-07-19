using BlazorHero.CleanArchitecture.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class Faq : AuditableEntity<int>
    {
        [MaxLength(500)]
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        [MaxLength(200)]
        public string Website { get; set; }
        public bool IsMainQue { get; set; } = false;
        public int? ParentQuestionId { get; set; }
        public int? Sequence { get; set; }
    }
}
