using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        //public string Password { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyPhone { get; set; }
        public string CompanyWebSite { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("CompanyIndustry")]
        public int IndustryId { get; set; }
        public virtual CompanyIndustry CompanyIndustry { get; set; }

        [ForeignKey("CompanySize")]
        public int CompanySizaId { get; set; }
        public virtual CompanySize CompanySize { get; set; }

        public string RefQuestion { get; set; }
        public string RefAnswer { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
