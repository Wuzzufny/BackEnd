using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{

    public class IdentityController : Controller
    {
        private readonly IIdentityServices _identityService;
        private readonly BakEndContext _bakEndContext;
        public IdentityController(IIdentityServices identityServices,
          BakEndContext context)
        {
            _identityService = identityServices;
            _bakEndContext = context;
        }
        [HttpPost(ApiRoute.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterationRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ModelState.Values);

            }
            var authResponse = await _identityService.RegisterAsync(null, request.UserName, request.Email, request.Password, request.Roles);
            if (!authResponse.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = authResponse.Errors
                    }
                  );
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(ApiRoute.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = authResponse.Errors
                    }
                  );
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpGet(ApiRoute.Identity.Roles)]
        public List<IdentityRole> GetAllROles()
        {
            return _bakEndContext.Roles.ToList();
        }

        [HttpPost(ApiRoute.Identity.ForgotPassword)]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordRequest userForgotPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                var modelStateErrorMsgs = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(modelStateErrorMsgs);

            }
            var result = await _identityService.ForgotPassword(userForgotPasswordRequest.Email);
            if (!result.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = result.Errors
                    }
                    );
            }

            return Ok(true);
        }

        [HttpPost(ApiRoute.Identity.ResetPassword)]
        public async Task<IActionResult> ResetPassword(UserResetPasswordRequest userResetPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                var modelStateErrorMsgs = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(modelStateErrorMsgs);

            }
            var result = await _identityService.ResetPassword(
                                                    userResetPasswordRequest.Email,
                                                    userResetPasswordRequest.Code,
                                                    userResetPasswordRequest.NewPassword);
            if (!result.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = result.Errors
                    }
                    );
            }

            return Ok(true);
        }

        [HttpPost(ApiRoute.Identity.VerifyRegistrationCode)]
        public async Task<IActionResult> VerifyRegistrationCode(UserVerifyCodeRequest userVerifyCodeRequest)
        {
            if (!ModelState.IsValid)
            {
                var modelStateErrorMsgs = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(modelStateErrorMsgs);

            }
            var result = await _identityService.VerifyCode(
                                                    userVerifyCodeRequest.Email,
                                                    userVerifyCodeRequest.Code);
            if (!result.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = result.Errors
                    }
                    );
            }

            return Ok(true);
        }
    }
}
