using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds
{
    public class ContactUs : AuditableEntity<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Mobile { get; set; }
        [MaxLength(1000)]
        public string Message { get; set; }
        [MaxLength(250)]
        public string Subject { get; set; }
        [MaxLength(20)]
        public string ContactFor { get; set; } // Wedding, Billing, Account  
    }
}
