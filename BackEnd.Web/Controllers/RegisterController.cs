using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IClientService _iClientService;
        public RegisterController(IClientService ClientService)
        {
            _iClientService = ClientService;
        }

        [HttpPost(ApiRoute.Register.Employer)]
        public async Task<IActionResult> Employer([FromBody] EmployerRegisterationRequest Employer_request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ModelState.Values);

            }
            var RegisterResponse = await _iClientService.RegisterEmployer(Employer_request);
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