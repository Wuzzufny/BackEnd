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
        private readonly IUnitOfWork _uow;
        private readonly IIdentityServices _identitySer;
        public ClientService(IUnitOfWork uow, IIdentityServices identitySer)
        {
            _uow= uow;
            _identitySer = identitySer;
        }
        public async Task<AuthenticationResult> RegisterEmployer(EmployerRegisterationRequest data)
        {
            try
            {
                var client = new Client()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Title = data.Title,
                    Mobile = data.Mobile,
                    Email = data.Email,
                    CompanyName = data.CompanyName,
                    CompanyPhone = data.CompanyPhone,
                    CompanyWebSite = data.CompanyWebSite,
<<<<<<< HEAD
                    CountryId = data.CountryId,
                    IndustryId = data.IndustryId,
                    CompanySizaId = data.CompanySizaId
                };
                var user = new ApplicationUser
=======
                    CountryID = data.CountryID,
                    IndustryID = data.IndustryID,
                    CompanySizaID = data.CompanySizaID
                };
                ApplicationUser user = new ApplicationUser
>>>>>>> 914449123e20e4de4e5e3d0069bc6fb6e7886dca
                {
                    Email = data.Email,
                    //EmployeeID=Employee.ID,
                    UserName = data.Email,
                    IsActive = true
                };
<<<<<<< HEAD
                var result = await _identitySer.RegisterAsync(user, data.Password, "employer");
                if (result.Success)
                {
                    client.UserId = result.User.Id;

                    _uow.Repository<Client>().Insert(client);
                    if (_uow.Save() == 200)
                    {
                        var mailResult = await _identitySer.SendEmailWithCode("Wuzzufny Verification Code",
                                                            "Kindly copy this code to use in <br>  mobile app Verification Code Page ",
                                                            result.User);
                        return mailResult;
=======
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

>>>>>>> 914449123e20e4de4e5e3d0069bc6fb6e7886dca
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
