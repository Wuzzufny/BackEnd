using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
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
        private readonly IUnitOfWork uow;
        private readonly IidentityServices identitySer;
        public EmployeeService(IUnitOfWork _uow, IidentityServices _identitySer)
        {
            uow= _uow;
            identitySer = _identitySer;
        }
        public async Task<AuthenticationResult> RegisterEmployee(EmployeeRegisterationRequest data)
        {
            try
            {
                Employee Employee = new Employee()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Title = data.Title,
                    Email = data.Email,
                };

                uow.Repository<Employee>().Insert(Employee);

                if (uow.Save() == 200)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        Email=data.Email,
                        EmployeeID=Employee.ID,
                        UserName=data.Email
                    };
                    AuthenticationResultObj result= await identitySer.RegisterAsync( user, data.Password, "employee");
                    if (result.Success)
                    {
                        AuthenticationResult mailResult = await identitySer.sendEmailWithCode("Wuzzufny Verification Code",
                             "Kindly copy this code to use in <br>  mobile app Verification Code Page ",
                              result.user);
                       
                        return mailResult;
                    }
                    else
                        return new AuthenticationResult
                        {
                            Errors = result.Errors.Select(x => x)
                        };
                }
                else
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "Can not save in Database" }
                    }; 
                }
            }
            catch(Exception ex)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Can not save in Database" }
                };
            }
        }
    }
}
