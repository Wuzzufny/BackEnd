using BackEnd.BAL.Interfaces;
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
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _uow;
        public JobService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IEnumerable<JobResponseDto>> RetrieveJobs(int? pageNumber , int? pageSize)
        {
            var result = _uow.Repository<Job>().Get().OrderByDescending(a => a.CreatedDate).Select(a => new JobResponseDto
            { 
                Id = a.Id,
                JobTitle = a.JobTitle,
                JobTypeResponse = new JobTypeResponseDto { jobtype = a.JobType.jobtype, Id = a.JobType.Id },
                careerLevel = new CareerLevelResponseDto { Id = a.Id,careerlevel=a.CareerLevel.careerlevel},
                jobRole = new JobRoleResponseDto { Id = a.Id ,jobrole =a.JobRole.jobrole},
                companyResponse =new CompanyResponseDto { Id = a.Id ,CompanyName =a.Company.CompanyName , CompanyImage =a.Company.CompanyImage},
                JobLocation = a.JobLocation,
                MinYearsEx = a.MinYearsEx,
                MaxYearsEx = a.MaxYearsEx,
                ReceiveApplicants = a.ReceiveApplicants,
                skills = a.skills,
                JobQuestionsDto = a.JobQuestions.Where(j => j.jobid == a.Id).Select(s => new JobQuestionsDto { Id = s.Id, jobquestions = s.jobquestions }).ToList()
            }).Take((int)pageNumber).Skip((int)pageSize);
            return result;

        }
    }
}
