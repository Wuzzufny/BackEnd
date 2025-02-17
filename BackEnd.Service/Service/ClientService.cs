﻿using BackEnd.BAL.Interfaces;
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
                    CountryId = data.CountryId,
                    IndustryId = data.IndustryId,
                    CompanySizaId = data.CompanySizaId,
                    ReferalId=data.ReferalId
                };
                ApplicationUser user = new ApplicationUser
                {
                    Email = data.Email,
                    //EmployeeID=Employee.ID,
                    UserName = data.Email,
                    IsActive = true
                };
                AuthenticationResultObj result = await _identitySer.RegisterAsync(user, data.Password, "employer");
                if (result.Success)
                {
                    client.UserID = result.User.Id;

                    _uow.Repository<Client>().Insert(client);
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
