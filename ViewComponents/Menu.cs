using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP_Web.Models;

namespace SEP_Web.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("userCheckIn");

            if (string.IsNullOrEmpty(userSession)) return null;

            Users users = JsonConvert.DeserializeObject<Users>(userSession);
            return View(users);
        }
    }
}