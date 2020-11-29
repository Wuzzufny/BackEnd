using BackEnd.BAL.Models;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface IStaticPageService
    {
        Task<RequestResponse> GetStaticPageContent(int pageID);
    }
}
