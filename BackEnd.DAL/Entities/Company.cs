using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class Company
    {
 
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyImage { get; set; }
        public string CompanyPhone{ get; set; }
        public string CompanyWebsite { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        [ForeignKey("CompanyIndustry")]
        public int CompanyIndustryId { get; set; }
        public virtual CompanyIndustry CompanyIndustry { get; set; }
        [ForeignKey("CompanySize")]
        public int CompanySizeId { get; set; }
        public virtual CompanySize CompanySize { get; set; }

    }
}
