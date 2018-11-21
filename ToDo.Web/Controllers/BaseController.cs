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

        public string CurrentSessionToken
        {
            get
            {
                var sessionToken = this.Request.Cookies.Get("SessionToken")?.Value;

                return sessionToken;
            }
        }

        public int CurrentUserID
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.Id;
                }

                return 0;
            }
        }

        public UserDto CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;

                if (HttpContext == null)
                    return null;

                if (!string.IsNullOrEmpty(CurrentSessionToken))
                {
                    _currentUser = _userService.GetBySessionToken(CurrentSessionToken).Data;
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