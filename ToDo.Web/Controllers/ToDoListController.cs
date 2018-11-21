using System.Web.Mvc;
using System.Web.UI;
using ToDo.Core.Service.Web.Interfaces;
using ToDo.Dto.Web;
using ToDo.Web.Helper;

namespace ToDo.Web.Controllers
{
    public class ToDoListController : BaseController
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [TokenValidation]
        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new ToDoListDto());
        }

        [TokenValidation]
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

        [TokenValidation]
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

        [TokenValidation]
        [HttpPost]
        public ActionResult Edit(ToDoListDto model)
        {
            var result = _toDoListService.Update(model);
            if (result.HasError)
            {
                return View(model);
            }

            ClearCache(model.Id);
            return RedirectToAction("List");
        }

        
        [OutputCache(Duration = 180, VaryByParam = "none")]
        [TokenValidation]
        [HttpGet]
        public ActionResult List()
        {
            var result = _toDoListService.List();

            return View(result.Data);
        }

        [TokenValidation]
        [OutputCache(Duration = 180, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Search(string searchText = null)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return RedirectToAction("List");
            var result = _toDoListService.List(searchText);

            return View("List",result.Data);
        }

        [TokenValidation]
        [OutputCache(Duration = 180, VaryByParam = "id")]
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var result = _toDoListService.Get(id);
            if (result.HasError)
                return RedirectToAction("List");
            return View(result.Data);
        }

        [TokenValidation]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = _toDoListService.Delete(id);
            if (result.HasError)
                SetErrorMessage(result.Message);
            ClearCache(id);
            return RedirectToAction("List");
        }

        private void ClearCache(int id)
        {
            string url1 = Url.Action("List", "ToDoList");
            string url2 = Url.Action("Detail", "ToDoList",new{id = id});

            Response.RemoveOutputCacheItem(url1);
            Response.RemoveOutputCacheItem(url2);
        }
    }
}