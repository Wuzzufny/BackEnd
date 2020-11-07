using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class UserForgotPasswordRequest
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

  }
}
