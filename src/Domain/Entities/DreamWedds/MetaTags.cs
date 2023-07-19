using BlazorHero.CleanArchitecture.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class MetaTags :  AuditableEntity<int>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Property { get; set; }
        [MaxLength(20)]
        public string TagPrefix { get; set; } // fb, og, twitter
        public string Content { get; set; }
        [MaxLength(200)]
        public string PageName { get; set; }
        [MaxLength(250)]
        public string PageTitle { get; set; }
        public bool IsImage { get; set; }
        [MaxLength(20)]
        public string Type { get; set; } // meta, stylesheet, preload, dns-prefetch 
        public string TypeId { get; set; } // id if link type
        public bool IsDeleted { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual TemplateMaster Template { get; set; }

    }
}
