using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using AspNetCoreCurrentRequestContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyMiddleware.Controllers
{
    public class UserController : Controller
    {
        public IActionResult GameList()
        {
            
            return Content("list ok");
        }

        public IActionResult Login()
        {
            if (UserService.IsLogin)
                //return Redirect($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/index");
                return RedirectToPage("/index", new { name="abc",id=333 });
            return Content("login fail");
        }
    }
}