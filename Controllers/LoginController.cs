using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using SEP_Web.Helper;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsersServices _usersServices;
        private readonly IUsersSession _session;

        public LoginController(ILogger<LoginController> logger, IUsersServices usersServices, IUsersSession session)
        {
            _logger = logger;
            _usersServices = usersServices;
            _session = session;
        }

        public IActionResult Index()
        {

            if (_session.SearchUserSession() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Users users = _usersServices.UserSignIn(login.LoginName);

                    if(users != null)
                    {
                        if(users.ValidPassword(login.Password))
                        {
                            _session.UserCheckIn(users);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["ErrorMessage"] = $"Senha inválido";

                    }

                    TempData["ErrorMessage"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }

                return View("Index");
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível realizar o login {e.Message}.";
                _logger.LogError($"Não foi possível realizar o login {e.Message}");
                return RedirectToAction("Index");
            }
        }
        public IActionResult Logout()
        {
            _session.UserCheckOut();
            return RedirectToAction("Index", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}