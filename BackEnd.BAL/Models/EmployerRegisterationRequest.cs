using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.BAL.Models
{
    public class EmployerRegisterationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyPhone { get; set; }
        public string CompanyWebSite { get; set; }

       
        public int CountryId { get; set; }
        public int IndustryId { get; set; }
        public int CompanySizaId { get; set; }

     
    }
}
