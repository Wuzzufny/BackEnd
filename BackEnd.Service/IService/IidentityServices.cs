using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IIdentityServices
  {
    Task<AuthenticationResult> RegisterAsync(int? employeeId, string userName,string email,string password, string roles);
    Task<AuthenticationResultObj> RegisterAsync( ApplicationUser user,string password, string roles);
    Task<AuthenticationResult> LoginAsync(string email, string password);
    Task<AuthenticationResult> ForgotPassword(string email);
    Task<AuthenticationResult> ResetPassword(string email, string code, string newPassword);
    Task<AuthenticationResult> VerifyCode(string email, string code);
    Task<AuthenticationResult> SendEmailWithCode(string subject, string body, ApplicationUser user);
  }
}
