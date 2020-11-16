using BackEnd.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealState.DAL.Context;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BackEnd.DAL.Context
{
  public class BakEndContext : IdentityDbContext<ApplicationUser>, IBackEndContext
  {
        public DbSet<Client> Client { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<CompanySize> CompanySize { get; set; }
        public DbSet<CompanyIndustry> CompanyIndustry { get; set; }
        public BakEndContext(DbContextOptions<BakEndContext> options) : base(options)
    {
      
    }

  }
}
