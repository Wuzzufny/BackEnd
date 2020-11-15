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
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using BackEnd.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Service.Service
{
  public class IdentityServices : IidentityServices
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationSettings _jwtSettings;
    private readonly TokenValidationParameters _TokenValidationParameters;
    private readonly BakEndContext _dataContext;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public IdentityServices(UserManager<ApplicationUser> userManager,
      ApplicationSettings jwtSettings,
      TokenValidationParameters TokenValidationParameters,
      RoleManager<IdentityRole> roleManager,
      BakEndContext dataContext,
      IEmailSender emailSender,
      IPasswordHasher<ApplicationUser> passwordHasher
      )
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _jwtSettings = jwtSettings;
      _TokenValidationParameters = TokenValidationParameters;
      _dataContext = dataContext;
      _emailSender = emailSender;
      _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
    {
      var user = await _userManager.FindByEmailAsync(Email);
      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User does not Exist" }
        };
      }

      var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
      if (!userHasValidPassword)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User/Password combination wrong" }
        };
      }
      return await GenerateAutheticationForResultForUser(user);
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
                //generate reset password token
                int resetToken =  RandomCodeGenerator.RandomNumber();
                user.Code = resetToken.ToString();
                //save code in user table
                IdentityResult codeSavedResult = await _userManager.UpdateAsync(user);
                if (codeSavedResult.Succeeded)
                {
                    //send email with this code to email
                    string subject = "Wuzzufny Forgot Password Code";
                    string body = "Kindly copy this code to use in <br>  mobile app Reset Password Page ,<br>  the code is  <br>" + resetToken;
                    Message message = new Message(new List<string> { Email }, subject, body);
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
                        Errors= codeSavedResult.Errors.Select(x=>x.Description)
                    };
                }
               
            }

        }
      
        public async Task<AuthenticationResult> ResetPassword(string Email, string Code,string NewPassword)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not Exist" }
                };
            } else if(user.Code == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Code does not Exist" }
                };
            }
            else
            {
               
               // IdentityResult result = await _userManager.ResetPasswordAsync(user, Code,NewPassword);
               if( user.Code == Code)
               {
                    string hasedPassword = _passwordHasher.HashPassword(user, NewPassword);
                    user.PasswordHash = hasedPassword;

                    IdentityResult result = await  _userManager.UpdateAsync(user);
                    
                    return new AuthenticationResult
                    {
                        Success = result.Succeeded,
                        Errors = result.Errors.Select(x => x.Description)
                    };
                    
               }
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "Code does not Match" }
                };
            }

        }

    public async Task<AuthenticationResult> RegisterAsync(string UserName,string Email, string Password,string Roles)
    {
      var existingUser = await _userManager.FindByEmailAsync(Email);
      if (existingUser != null) {
        return new AuthenticationResult{
          Errors=new[] {"User with this email adress already Exist"}
        };
      }

      var newUser = new ApplicationUser
      {
        Email =Email,
        UserName= UserName
      };

      var createdUser = await _userManager.CreateAsync(newUser, Password);

      if (!createdUser.Succeeded) {
        return new AuthenticationResult {
          Errors = createdUser.Errors.Select(x=>x.Description)
        };
      }

      //-----------------------------add Role to token------------------
      if (!string.IsNullOrEmpty(Roles)) { 
      await _userManager.AddToRoleAsync(newUser,Roles);
      }
      //-----------------------------------------------------------------

      return await GenerateAutheticationForResultForUser(newUser);


    }

    private async Task<AuthenticationResult> GenerateAutheticationForResultForUser(ApplicationUser user) {
      var TokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSettings.JWT_Secret);
      var claims = new List<Claim> {
          new Claim(JwtRegisteredClaimNames.Sub,user.Email),
          new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Email,user.Email),
          new Claim("id",user.Id)
          };

      //get claims of user---------------------------------------
      var Userclaims = await _userManager.GetClaimsAsync(user);
      claims.AddRange(Userclaims);
      //------------------------Add Roles to claims-----------------------------------
      var userRols = await _userManager.GetRolesAsync(user);

      foreach (var userRole in userRols)
      {
        claims.Add(new Claim(ClaimTypes.Role, userRole));
        var role = await _roleManager.FindByNameAsync(userRole);
        if (role != null)
        {
          var roleClaims = await _roleManager.GetClaimsAsync(role);
          foreach (Claim roleClaim in roleClaims)
          {
            claims.Add(roleClaim);
          }
        }
      }
      //---------------------------------------------------------
      var TokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = TokenHandler.CreateToken(TokenDescriptor);
      await _dataContext.SaveChangesAsync();
      return new AuthenticationResult
      {
        Success = true,
        Token = TokenHandler.WriteToken(token)

      };
    }
  }
}
