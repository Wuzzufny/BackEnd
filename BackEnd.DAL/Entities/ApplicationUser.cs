using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
