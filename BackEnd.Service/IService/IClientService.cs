using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface IClientService
    {
        Task<AuthenticationResult> RegisterEmployer(EmployerRegisterationRequest data);
    }
}
