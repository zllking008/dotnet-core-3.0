using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyMiddleware.ApiControllers
{
    [Route("Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize(AuthenticationSchemes =UserService.userSignInIdentity)]
        [Route("GetInc"),HttpGet]
        public IActionResult GetInc()
        {
            return Content("ok");
        }
    }
}