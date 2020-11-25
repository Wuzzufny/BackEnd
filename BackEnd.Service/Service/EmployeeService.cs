using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.BAL.Models.DTOs.Response;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityServices _identitySer;
        public EmployeeService(IUnitOfWork uow, IIdentityServices identitySer)
        {
            _uow= uow;
            _identitySer = identitySer;
        }
        public async Task<AuthenticationResult> RegisterEmployee(EmployeeRegisterationRequest data)
        {
            try
            {
                var employee = new Employee()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Title = data.Title,
                    Email = data.Email,
                };


                ApplicationUser user = new ApplicationUser
                {
                    Email=data.Email,
                    UserName=data.Email,
                    IsActive=true
                };

                AuthenticationResultObj result= await _identitySer.RegisterAsync( user, data.Password, "employee");
                if (result.Success)
                {
                    employee.UserId = result.User.Id;
                    _uow.Repository<Employee>().Insert(employee);

                    if (_uow.Save() == 200)
                    {
                        AuthenticationResult mailResult = await _identitySer.SendEmailWithCode("Wuzzufny Verification Code",
                                                        "Kindly copy this code to use in <br>  mobile app Verification Code Page ",
                                                        result.User);
 
                        return mailResult;
                    } 
                    else
                    {
                        return new AuthenticationResult
                        {
                            Errors = new[] { "Can not save in Database" }
                        };
                    }

                }
                else
                    return new AuthenticationResult
                    {
                        Errors = result.Errors.Select(x => x)
                    };
            }
            catch(Exception)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Can not save in Database" }
                };
            }
        }
 
    }
}
