using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
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
        private IEmployeeService _iEmployeeService;
        public EmployeeController(IEmployeeService EmployeeService)
        {
            _iEmployeeService = EmployeeService;
        }

        [HttpPost(ApiRoute.Employee.Register)]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterationRequest Employee_request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ModelState.Values);

            }
            var RegisterResponse = await _iEmployeeService.RegisterEmployee(Employee_request);
            if (!RegisterResponse.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = RegisterResponse.Errors
                    }
                  );
            }

            return Ok(new AuthSuccessResponse
            {
                Token = RegisterResponse.Token
            });
        }
    }
}