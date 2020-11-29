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
    public class StaticPageController : ControllerBase
    {
        private readonly IStaticPageService _iStaticPageService;
        public StaticPageController(IStaticPageService StaticPageService)
        {
            _iStaticPageService = StaticPageService;
        }

        [HttpPost(ApiRoute.StaticPage.GetStaticPageContent)]
        public async Task<IActionResult> GetStaticPageContent(int pageID)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ModelState.Values);

            }
            var staticPagesResponse = await _iStaticPageService.GetStaticPageContent(pageID);
            if (!staticPagesResponse.Success)
            {
                return BadRequest(
                    new AuthFaildResponse
                    {
                        Errors = staticPagesResponse.Errors
                    }
                  );
            }

            return Ok(staticPagesResponse);
        }
    }
}