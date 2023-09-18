using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SEP_Web.Models;

namespace SEP_Web.Helper
{
    public class UsersSession : IUsersSession
    {
        private readonly IHttpContextAccessor _httpContext;

        public UsersSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Users SearchUserSession()
        {
            string userSession = _httpContext.HttpContext.Session.GetString("userCheckIn");

            if (string.IsNullOrEmpty(userSession)) return null;

            return JsonConvert.DeserializeObject<Users>(userSession);
        }

        public void UserCheckIn(Users users)
        {
            string value = JsonConvert.SerializeObject(users);
            _httpContext.HttpContext.Session.SetString("userCheckIn", value);
        }

        public void UserCheckOut()
        {
            _httpContext.HttpContext.Session.Remove("userCheckIn");
        }

    }
}