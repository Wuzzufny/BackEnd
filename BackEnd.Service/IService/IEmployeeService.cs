﻿using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface IEmployeeService
    {
        Task<AuthenticationResult> RegisterEmployee(EmployeeRegisterationRequest data);
        Task<IEnumerable<Job>> RetrieveJobs();
    }
}
