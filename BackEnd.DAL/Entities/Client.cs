using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class Client
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyPhone { get; set; }
        public string CompanyWebSite { get; set; }

        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("CompanyIndustry")]
        public int IndustryID { get; set; }
        public virtual CompanyIndustry CompanyIndustry { get; set; }

        [ForeignKey("CompanySize")]
        public int CompanySizaID { get; set; }
        public virtual CompanySize CompanySize { get; set; }

        public string Ref_Question { get; set; }
        public string Ref_Answer { get; set; }
    }
}
