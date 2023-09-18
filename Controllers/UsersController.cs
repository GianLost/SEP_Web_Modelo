using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEP_Web.Filters;
using SEP_Web.Models;
using SEP_Web.Services;

namespace SEP_Web.Controllers
{
    [UserAdminFilter]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersServices _usersServices;

        public UsersController(ILogger<UsersController> logger, IUsersServices usersServices)
        {
            _logger = logger;
            _usersServices = usersServices;
        }

        public IActionResult Index()
        {
            ICollection<Users> users= _usersServices.ListUsers();
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Users users)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _usersServices.RegisterUsers(users);
                    TempData["SuccessMessage"] = "Usuário cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(users);
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível cadsatrar o usuário {e.Message}.";
                _logger.LogError($"Não foi possível cadsatrar o usuário {e.Message}");
                return RedirectToAction("Index");
            }

        }

        public IActionResult Edit(int id)
        {
            Users users = _usersServices.SearchForId(id);
            return View(users);
        }

        [HttpPost]
        public IActionResult Edit(Users users)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _usersServices.EditUser(users);
                    TempData["SuccessMessage"] = "Usuário editado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(users);
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível editar o usuário {e.Message}.";
                _logger.LogError($"Não foi possível editar o usuário {e.Message}");
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            Users users = _usersServices.SearchForId(id);
            return View(users);
        }
        
        public IActionResult DeleteUsers(int id)
        {
            try
            {
                bool deleted = _usersServices.DeleteUser(id);

                if(deleted)
                {
                    TempData["SuccessMessage"] = "Usuário excluído com sucesso.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível excluir o usuário.";
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"Não foi possível excluir o usuário {e.Message}.";
                _logger.LogError($"Não foi possível excluir o usuário {e.Message}");
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}