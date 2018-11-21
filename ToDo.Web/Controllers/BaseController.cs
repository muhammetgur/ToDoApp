using System.Web.Mvc;
using System.Web.Security;
using ToDo.Core.Service.Web.Interfaces;
using ToDo.Dto.Web;

namespace ToDo.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;
        private UserDto _currentUser;

        public BaseController()
        {
            _userService = Core.Ioc.Bootstrapper.Resolve<IUserService>();
        }

        public UserDto CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;

                if (HttpContext == null)
                    return null;

                var sessionToken = HttpContext.User.Identity.Name;

                if (!string.IsNullOrEmpty(sessionToken))
                {
                    var sessionTokenResult = _userService.GetBySessionToken(sessionToken);
                    if (sessionTokenResult.HasError)
                    {
                        SetErrorMessage("Hata");
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    _currentUser = sessionTokenResult.Data;
                }

                return _currentUser;
            }
        }

        protected void SetSuccessMessage(string message)
        {
            TempData["successMessage"] = message;
        }

        protected void SetErrorMessage(string message)
        {
            TempData["errorMessage"] = message;
        }
    }
}