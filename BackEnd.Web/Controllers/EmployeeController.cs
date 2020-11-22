using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using BackEnd.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _iEmployeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _iEmployeeService = employeeService;
        }

        [HttpPost(ApiRoute.Employee.Register)]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterationRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ModelState.Values);

            }
            var registerResponse = await _iEmployeeService.RegisterEmployee(employeeRequest);
            if (!registerResponse.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = registerResponse.Errors
                    }
                  );
            }

            return Ok(new AuthSuccessResponse
            {
                Token = registerResponse.Token
            });
        }

        [HttpGet(ApiRoute.Employee.EmployeeHome)]
        public async Task<IEnumerable<Job>> EmployeeHome()
        {
            var res =await _iEmployeeService.RetrieveJobs();
            return res;
        }
    }
}