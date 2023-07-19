using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
   
    public partial class TemplateMergeField : AuditableEntity<int>
    {
        [MaxLength(100)]
        public string MERGEFIELD_NAME { get; set; }
        [MaxLength(100)]
        public string SRC_FIELD { get; set; }
        [MaxLength(100)]
        public string SRC_FIELD_VALUE { get; set; }
        public int? TemplateID { get; set; }
        public Nullable<int> Sequence { get; set; }
        public int? TemplateCode { get; set; }
    
        public virtual TemplateMaster TemplateMaster { get; set; }
    }
}
