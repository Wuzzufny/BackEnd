using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IidentityServices
  {
    Task<AuthenticationResult> RegisterAsync(int? employeeId, string UserName,string Email,string Password, string Roles);
    Task<AuthenticationResultObj> RegisterAsync( ApplicationUser user,string Password, string Roles);
    Task<AuthenticationResult> LoginAsync(string Email, string Password);
    Task<AuthenticationResult> ForgotPassword(string Email);
    Task<AuthenticationResult> ResetPassword(string Email, string Code, string NewPassword);
    Task<AuthenticationResult> VerifyCode(string Email, string Code);
    Task<AuthenticationResult> sendEmailWithCode(string subject, string body, ApplicationUser user);
  }
}
