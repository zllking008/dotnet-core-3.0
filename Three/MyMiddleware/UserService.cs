using AspNetCoreCurrentRequestContext;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMiddleware
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime LoginTime { get; set; }
    }
    public class UserService
    {
        public const string userSignInIdentity = "middleware_Identity";
        public static async Task SignInAsync(User model)
        {
            await SignOut();

            var claims = new[] {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.NameIdentifier, model.ID.ToString()),
                new Claim(ClaimTypes.UserData, ConvertToJson(model))
            };
            //var userPrincipal = new ClaimsPrincipal (new ClaimsIdentity (claims, "userLogin"));
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,userSignInIdentity));
            
            var task = AspNetCoreHttpContext.Current.SignInAsync(userSignInIdentity, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1), //票据缓存时间
                IsPersistent = true,
                AllowRefresh = true
            });

        }

        public static async Task SignOut()
        {
            await AspNetCoreHttpContext.Current.SignOutAsync(userSignInIdentity);
        }

        protected static T ConvertToModel<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            //return Jil.JSON.Deserialize<T>(json); jil 不能序列化枚举类型
        }

        protected static string ConvertToJson(object o)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(o);
            //return Jil.JSON.Serialize(o); 
        }

        private static async Task<string> GetAccountInfo(string claimType)
        {
            var t = await AspNetCoreHttpContext.Current.AuthenticateAsync(userSignInIdentity);
            //AspNetCoreCompatibility.CompatibilityHttpContextAccessor.Current.AuthenticateAsync(userSignInIdentity);
            if (t.Succeeded)
            {
                ClaimsIdentity _identity = (ClaimsIdentity)t.Principal.Identity;
                var userData = _identity.FindFirst(claimType);
                if (string.IsNullOrEmpty(userData.Value)) return null;
                return userData.Value;

            }
            return null;
        }

        /// <summary>
        /// 当前票据json字符串
        /// </summary>
        /// <value></value>
        public static string CurrentJson
        {
            get
            {
                return GetAccountInfo(ClaimTypes.UserData).Result;
            }
        }

        public static bool IsLogin
        {
            get { 
                return !string.IsNullOrEmpty(CurrentJson);
            }
        }
    }
}
