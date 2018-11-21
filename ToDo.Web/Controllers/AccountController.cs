using System;
using System.Web;
using System.Web.Mvc;
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
            if (CurrentUser != null)
                return RedirectToAction("List", "ToDoList");

            var errorMessage = Request.Cookies.Get("ErrorMessage")?.Value;
            SetErrorMessage(errorMessage);
            return View(new LoginDto());
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

            SetCookie(result.SessionToken);

            return RedirectToAction("List","ToDoList");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View(new UserDto());
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
            SetCookie(result.SessionToken);

            return RedirectToAction("List", "ToDoList");
        }


        [HttpGet]
        public ActionResult Logout()
        {
            _userService.Logout(CurrentSessionToken);

            return View("Login", new LoginDto());
        }

        private void SetCookie(string sessionToken)
        {
            HttpCookie cookie = new HttpCookie("SessionToken", sessionToken);
            Response.Cookies.Add(cookie);
        }

    }
}