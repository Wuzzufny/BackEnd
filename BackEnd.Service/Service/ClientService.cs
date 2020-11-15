using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
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
                    LastName = data.FirstName,
                    Title = data.FirstName,
                    Mobile = data.FirstName,
                    Email = data.FirstName,
                    Password = data.FirstName,
                    CompanyName = data.FirstName,
                    CompanyPhone = data.FirstName,
                    CompanyWebSite = data.FirstName,
                    CountryID = data.CountryID,
                    IndustryID = data.IndustryID,
                    CompanySizaID = data.CompanySizaID
                };
                uow.Repository<Client>().Insert(client);
                if (uow.Save() == 200)
                {
                    return await identitySer.RegisterAsync(client.Email, client.Email, client.Password, "employer");
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
