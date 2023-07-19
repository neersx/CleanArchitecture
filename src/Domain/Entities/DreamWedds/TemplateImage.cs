using BlazorHero.CleanArchitecture.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{  
    public partial class TemplateImage : AuditableEntity<int>
    {
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(150)]
        public string TagLine { get; set; }
        [MaxLength(500)]
        public string Url { get; set; }
        [MaxLength(500)]
        public string FolderPath { get; set; }
        public bool IsBannerImage { get; set; }
        public int TemplateID { get; set; }
        public int? ImageType { get; set; }
    
        public virtual TemplateMaster TemplateMaster { get; set; }
    }
}
