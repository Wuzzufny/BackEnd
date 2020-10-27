using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.ISercice;
using Microsoft.AspNetCore.Identity;
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
    public IdentityServices(UserManager<ApplicationUser> userManager,
      ApplicationSettings jwtSettings,
      TokenValidationParameters TokenValidationParameters,
      RoleManager<IdentityRole> roleManager,
      BakEndContext dataContext)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _jwtSettings = jwtSettings;
      _TokenValidationParameters = TokenValidationParameters;
      _dataContext = dataContext;
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

  
    private ClaimsPrincipal GetPrincipalFromToken(string Token) {
      var tokenHandler = new JwtSecurityTokenHandler();
      try {
        var principal = tokenHandler.ValidateToken(Token, _TokenValidationParameters, out var validtionToken);
        if (!IsJwtWithValidationSecurityAlgorithm(validtionToken)) {
          return null;
        }
        return principal;
      }
      catch {
        return null;
      }
    }

    private bool IsJwtWithValidationSecurityAlgorithm(SecurityToken validatedToken) {
      return (validatedToken is JwtSecurityToken jwtSecurityToken)&&
        jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
        StringComparison.InvariantCultureIgnoreCase);
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
