using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyMiddleware
{
    public class IndexModel : PageModel
    {
     
        //[Authorize( AuthenticationSchemes = UserService.userSignInIdentity)] 不能用这种方式
        public async Task OnGet(string name,int id)
        {
            //if (UserService.IsLogin)
            //    await Response.WriteAsync(UserService.CurrentJson);
        }
    }
}
