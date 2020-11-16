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
        private readonly IClientService _iClientService;
        public RegisterController(IClientService clientService)
        {
            _iClientService = clientService;
        }

        [HttpPost(ApiRoute.Register.Employer)]
        public async Task<IActionResult> Employer([FromBody] EmployerRegisterationRequest employerRequest)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ModelState.Values);

            }
            var registerResponse = await _iClientService.RegisterEmployer(employerRequest);
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
    }
}