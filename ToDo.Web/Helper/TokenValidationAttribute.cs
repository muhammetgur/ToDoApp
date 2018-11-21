using System;
using System.Web.Mvc;
using System.Web.Routing;
using ToDo.Core.Service.Web.Interfaces;

namespace ToDo.Web.Helper
{
    public class TokenValidationAttribute : ActionFilterAttribute
    {
         private readonly IUserService _userService;

        public TokenValidationAttribute()
        {
            _userService = Core.Ioc.Bootstrapper.Resolve<IUserService>();
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

            var sessionToken = actionContext.HttpContext.Request.Cookies.Get("SessionToken")?.Value;

            if (!string.IsNullOrEmpty(sessionToken))
            {
                var validToken = _userService.CheckToken(sessionToken);
                if (validToken == null)
                {
                    actionContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Account", action = "Login" }));
                }
                else
                {
                    if (validToken.ExpireDate < DateTime.Now.AddDays(-1))
                    {
                        actionContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Account", action = "Login", }));
                    }
                    else
                    {
                        base.OnActionExecuting(actionContext);
                    }
                }
            }
            else
            {

                actionContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }

        public object TryGetValue { get; set; }
    }
}