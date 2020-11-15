using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IidentityServices
  {
    Task<AuthenticationResult> RegisterAsync(string UserName,string Email,string Password, string Roles);
    Task<AuthenticationResult> LoginAsync(string Email, string Password);
    Task<AuthenticationResult> ForgotPassword(string Email);
    Task<AuthenticationResult> ResetPassword(string Email, string Code, string NewPassword);
  }
}
