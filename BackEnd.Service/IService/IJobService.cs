using BackEnd.BAL.Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface IJobService
    {
        Task<IEnumerable<JobResponseDto>> RetrieveJobs(int? pageNumber , int? pageSize);
    }
}
