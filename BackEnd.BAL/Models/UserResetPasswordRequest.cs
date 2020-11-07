using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class UserResetPasswordRequest
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string NewPassword { get; set; }
    
    [Required]
    public string Code { get; set; }

   }
}
