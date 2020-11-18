using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.Helpers;
using BackEnd.Service.IService;
using BackEnd.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Service.Service
{
  public class IdentityServices : IIdentityServices
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationSettings _jwtSettings;
    private readonly BakEndContext _dataContext;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    public IdentityServices(UserManager<ApplicationUser> userManager,
  ApplicationSettings jwtSettings,
  RoleManager<IdentityRole> roleManager,
  BakEndContext dataContext,
  IEmailSender emailSender,
  IPasswordHasher<ApplicationUser> passwordHasher
  )
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _jwtSettings = jwtSettings;
      _dataContext = dataContext;
      _emailSender = emailSender;
      _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
    {
      var user = await _userManager.FindByEmailAsync(Email);
      //check if user exist
      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User does not Exist" }
        };
      }

      //check password

      var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);

      if (!userHasValidPassword)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User/Password combination wrong" }
        };
      }

      //check if user active
      if (!user.IsActive)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User is not Active" }
        };
      }

      //check if user verified
      if (user.IsVerified) return await GenerateAutheticationForResultForUser(user);
      var result = await SendEmailWithCode("Wuzzufny Verification Code",
          "Kindly copy this code to use in <br>  mobile app Verification Code Page ",
          user);

      if (result.Success)
          return new AuthenticationResult
          {
              Errors = new[] { "User is not Verified,Email Sent With code for verification" }
          };
      else
          return result;
      //here user is valid to login
    }
    public async Task<AuthenticationResult> ForgotPassword(string Email)
    {
      var user = await _userManager.FindByEmailAsync(Email);
      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User does not Exist" }
        };
      }
      else
      {
        return await SendEmailWithCode("Wuzzufny Forgot Password Code",
                              "Kindly copy this code to use in <br>  mobile app Reset Password Page ",
                               user);
      }

    }

    public async Task<AuthenticationResult> ResetPassword(string email, string Code, string newPassword)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User does not Exist" }
        };
      }
      else if (user.Code == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "Code does not Exist" }
        };
      }
      else
      {

        // IdentityResult result = await _userManager.ResetPasswordAsync(user, Code,NewPassword);
        if (user.Code != Code)
            return new AuthenticationResult
            {
                Success = false,
                Errors = new[] {"Code does not Match"}
            };
        var hasedPassword = _passwordHasher.HashPassword(user, newPassword);
        user.PasswordHash = hasedPassword;

        var result = await _userManager.UpdateAsync(user);

        return new AuthenticationResult
        {
            Success = result.Succeeded,
            Errors = result.Errors.Select(x => x.Description)
        };
      }

    }
    public async Task<AuthenticationResult> VerifyCode(string Email, string Code)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not Exist" }
                };
            }
            else if (user.Code == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Code does not Exist" }
                };
            }
            else
            {
                if (user.Code != Code)
                    return new AuthenticationResult
                    {
                        Success = false,
                        Errors = new[] {"Code does not Match"}
                    };
                user.IsVerified = true;

                var result = await _userManager.UpdateAsync(user);

                return new AuthenticationResult
                {
                    Success = result.Succeeded,
                    Errors = result.Errors.Select(x => x.Description)
                };
            }

        }


    public async Task<AuthenticationResult> RegisterAsync(int? employeeId, string UserName, string Email, string Password, string Roles)
     {
      var existingUser = await _userManager.FindByEmailAsync(Email);
      if (existingUser != null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User with this email address already Exist" }
        };
      }

      var newUser = new ApplicationUser
      {
        Email = Email,
        UserName = UserName,
        IsActive=true,
      };

      var createdUser = await _userManager.CreateAsync(newUser, Password);

      if (!createdUser.Succeeded)
      {
        return new AuthenticationResult
        {
          Errors = createdUser.Errors.Select(x => x.Description)
        };
      }

      //-----------------------------add Role to token------------------
      if (!string.IsNullOrEmpty(Roles))
      {
        await _userManager.AddToRoleAsync(newUser, Roles);
      }
      //-----------------------------------------------------------------

      return await GenerateAutheticationForResultForUser(newUser);


    }
        public async Task<AuthenticationResultObj> RegisterAsync( ApplicationUser user,string password, string Roles)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return new AuthenticationResultObj
                {
                    Errors = new[] { "User with this email address already Exist" }
                };
            }

            var createdUser = await _userManager.CreateAsync(user, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResultObj
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            //-----------------------------add Role to token------------------
            if (!string.IsNullOrEmpty(Roles))
            {
                await _userManager.AddToRoleAsync(user, Roles);
            }
            //-----------------------------------------------------------------

            // return await GenerateAutheticationForResultForUser(newUser);
 
             return new AuthenticationResultObj
            {
               Success=true,

               User= user
              }; ;

        }
        private async Task<AuthenticationResult> GenerateAutheticationForResultForUser(ApplicationUser user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSettings.JwtSecret);
      var claims = new List<Claim> {
          new Claim(JwtRegisteredClaimNames.Sub,user.Email),
          new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Email,user.Email),
          new Claim("id",user.Id)
          };

      //get claims of user---------------------------------------
      var userclaims = await _userManager.GetClaimsAsync(user);
      claims.AddRange(userclaims);
      //------------------------Add Roles to claims-----------------------------------
      var userRols = await _userManager.GetRolesAsync(user);

      foreach (var userRole in userRols)
      {
        claims.Add(new Claim(ClaimTypes.Role, userRole));
        var role = await _roleManager.FindByNameAsync(userRole);
        if (role == null) continue;
        var roleClaims = await _roleManager.GetClaimsAsync(role);
        claims.AddRange(roleClaims);
      }
      //---------------------------------------------------------
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      await _dataContext.SaveChangesAsync();
      return new AuthenticationResult
      {
        Success = true,
        Token = tokenHandler.WriteToken(token)

      };
    }
    public async Task<AuthenticationResult> SendEmailWithCode(string subject, string body, ApplicationUser user)
    {
     //generate reset password token
      var resetToken = RandomCodeGenerator.RandomNumber();
      user.Code = resetToken.ToString();
      //save code in user table
      var codeSavedResult = await _userManager.UpdateAsync(user);
      if (codeSavedResult.Succeeded)
      {
        //send email with this code to email
        //string subject = "Wuzzufny Forgot Password Code";
        //string body = "Kindly copy this code to use in <br>  mobile app Reset Password Page ,<br>  the code is  <br>" + resetToken;
        var message = new Message(new List<string> { user.Email }, subject, body + ",<br>  the code is  <br>" + resetToken);
        await _emailSender.SendEmail(message);

        return new AuthenticationResult
        {
          Success = true,
          
          //Token = resetToken
        };

      }
      else
      {
        return new AuthenticationResult
        {
          Success = false,
          Errors = codeSavedResult.Errors.Select(x => x.Description)
        };
      }
    }
  }
}
