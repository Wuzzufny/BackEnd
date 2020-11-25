using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.ApiRoute;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _iJobService;
        public JobController(IJobService jobService)
        {
            _iJobService = jobService;
        }
        //  retrieve all jobs 
        [HttpGet(ApiRoute.Job.JobHome)]
        public async Task<IActionResult> EmployeeHome(int? pageNumber, int? pageSize)
        {
            var res = await _iJobService.RetrieveJobs( pageNumber , pageSize);
            return Ok(res);
        }
    }
}
