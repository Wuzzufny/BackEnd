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
    public class StaticPageService : IStaticPageService
    {
        private readonly IUnitOfWork _uow;
        public StaticPageService(IUnitOfWork uow)
        {
            _uow= uow;
        }
        public async Task<RequestResponse> GetStaticPageContent(int pageID)
        {
            try
            {
                StaticPages staticPage = await _uow.Repository<StaticPages>().GetAsyncByID(pageID);

                if (staticPage != null)
                {
                    return new RequestResponse() {  Success= true,Result = staticPage.PageContent };
                }
                else
                {
                    return new RequestResponse() { Success = false, Errors = new[] { "Page Not Found" } };
                }
            }
            catch(Exception ex)
            {
                return new RequestResponse() { Success = false, Errors = new[] { "Error in Getting Page" } };
            }
        }
    }
}
