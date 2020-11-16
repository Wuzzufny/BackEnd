using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthenticationResult
  {
    public string Token { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
  }
    public class AuthenticationResultObj
    {
        public ApplicationUser User { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
