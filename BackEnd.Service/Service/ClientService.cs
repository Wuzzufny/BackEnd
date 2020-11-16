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
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork uow;
        private readonly IidentityServices identitySer;
        public ClientService(IUnitOfWork _uow, IidentityServices _identitySer)
        {
            uow= _uow;
            identitySer = _identitySer;
        }
        public async Task<AuthenticationResult> RegisterEmployer(EmployerRegisterationRequest data)
        {
            try
            {
                Client client = new Client()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Title = data.Title,
                    Mobile = data.Mobile,
                    Email = data.Email,
                    CompanyName = data.CompanyName,
                    CompanyPhone = data.CompanyPhone,
                    CompanyWebSite = data.CompanyWebSite,
                    CountryID = data.CountryID,
                    IndustryID = data.IndustryID,
                    CompanySizaID = data.CompanySizaID
                };
                ApplicationUser user = new ApplicationUser
                {
                    Email = data.Email,
                    //EmployeeID=Employee.ID,
                    UserName = data.Email,
                    IsActive = true
                };
                AuthenticationResultObj result = await identitySer.RegisterAsync(user, data.Password, "employer");
                if (result.Success)
                {
                    client.UserID = result.user.Id;

                    uow.Repository<Client>().Insert(client);
                    if (uow.Save() == 200)
                    {
                        AuthenticationResult mailResult = await identitySer.sendEmailWithCode("Wuzzufny Verification Code",
                                                            "Kindly copy this code to use in <br>  mobile app Verification Code Page ",
                                                            result.user);

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
                {
                    return new AuthenticationResult
                    {
                        Errors = result.Errors.Select(x => x)
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
