using System.Web.Mvc;
using ToDo.Core.Service.Web.Interfaces;

namespace ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoListService _toDoListService;

        public HomeController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}