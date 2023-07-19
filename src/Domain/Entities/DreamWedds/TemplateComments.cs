using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class TemplateComments : AuditableEntity<int>
    {
        [Required]
        [MaxLength(2000)]
        public string Comment { get; set; }
        public int TemplateId { get; set; }
        public string UserId { get; set; }
        [MaxLength(500)]
        public string Image { get; set; }
        public bool IsHtml { get; set; } = false;
        public DateTime Date { get; set;} = DateTime.Now;
        public bool IsApproved { get; set;} = false;
        [MaxLength(500)]
        public string Icon { get; set; }
        public virtual TemplateMaster Template { get; set; }  
    }
}
