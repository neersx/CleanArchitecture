
using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class Blog : AuditableEntity<int>
    {
        public Blog()
        {
            this.MetaTags = new HashSet<MetaTags>();
            this.Comments = new HashSet<BlogComments>();
        }
        [Required]
        [MaxLength(150)]
        public string BlogName { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string BlogSubject { get; set; }
        [MaxLength(500)]
        public string Quote { get; set; }
        [MaxLength(150)]
        public string AuthorName { get; set; }
        public string Content { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public int BlogType { get; set; } = 0;
        [MaxLength(250)]
        public string SpecialNote { get; set; }
        public virtual ICollection<MetaTags> MetaTags { get; set; }
        public virtual ICollection<BlogComments> Comments { get; set; }

    }
    public class BlogComments : AuditableEntity<int>
    {
        [Required]
        [MaxLength(2000)]
        public string Comment { get; set; }
        public int BlogId { get; set; }
        public string UserId { get; set; }
        [MaxLength(500)]
        public string Image { get; set; }
        public bool IsHtml { get; set; } = false;
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; } = false;
        [MaxLength(500)]
        public string Icon { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
