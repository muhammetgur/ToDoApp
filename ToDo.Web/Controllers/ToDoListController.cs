using System.Web.Mvc;
using System.Web.UI;
using ToDo.Core.Service.Web.Interfaces;
using ToDo.Dto.Web;

namespace ToDo.Web.Controllers
{
    public class ToDoListController : BaseController
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new ToDoListDto());
        }

        [HttpPost]
        public ActionResult Create(ToDoListDto model)
        {
            var result = _toDoListService.Create(model);

            if (result.HasError)
            {
                return View("Edit", model);
            }

            return RedirectToAction("List");
        }

        [OutputCache(Duration = 180, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = _toDoListService.Get(id);
            if (result.HasError)
            {
                return RedirectToAction("List");
            }

            return View(result.Data);
        }

        [HttpPost]
        public ActionResult Edit(ToDoListDto model)
        {
            var result = _toDoListService.Update(model);
            if (result.HasError)
            {
                return View(model);
            }

            return RedirectToAction("List");
        }

        [OutputCache(Duration = 180, VaryByParam = "none")]
        [HttpGet]
        public ActionResult List()
        {
            var result = _toDoListService.List();

            return View(result.Data);
        }

        [Route("List")]
        [OutputCache(Duration = 180, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Search(string searchText = null)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return RedirectToAction("List");
            var result = _toDoListService.List(searchText);

            return View("List",result.Data);
        }

        [OutputCache(Duration = 180, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var result = _toDoListService.Get(id);
            if (result.HasError)
                return RedirectToAction("List");
            return View(result.Data);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = _toDoListService.Delete(id);
            if (result.HasError)
                SetErrorMessage(result.Message);
            return RedirectToAction("List");
        }
    }
}