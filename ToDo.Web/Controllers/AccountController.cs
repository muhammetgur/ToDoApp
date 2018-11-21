using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDo.Core.Service.Web.Interfaces;
using ToDo.Dto.Web;

namespace ToDo.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginDto loginDto)
        {
            var result = _userService.Login(loginDto);

            if (result.HasError)
            {
                SetErrorMessage(result.Message);
                return View(loginDto);
            }

            FormsAuthentication.SetAuthCookie(result.SessionToken,true);

            return RedirectToAction("List","ToDoList");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDto userDto)
        {
            var result = _userService.Register(userDto);

            if (result.HasError)
            {
                SetErrorMessage(result.Message);
                return View(userDto);
            }
            FormsAuthentication.SetAuthCookie(result.SessionToken, false);

            return RedirectToAction("List", "ToDoList");
        }
    }
}