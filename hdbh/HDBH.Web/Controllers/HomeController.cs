using HDBG.Log;
using HDBH.Authorize;
using HDBH.Authorize.Models;
using HDBH.Log.Enums;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Services.Interface;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HDBH.Web.Controllers
{
    public class HomeController : BasedController
    {
        private IModuleService _moduleService;
        private ICached _cached;
        public HomeController(IModuleService moduleService, ICached cached)
        {
            _moduleService = moduleService;
            _cached = cached;
        }
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _loadMenu()
        {
            var result = _cached.Get<List<ModuleModel>>(CachedKey.loginModuleKeyCache + RDAuthorize.UserId);
            return PartialView(result);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult _childMenu(string ParentID, bool IsRootMenu = false)
        {
            List<ModuleModel> lsMenu = new List<ModuleModel>();
            var allmenu = _cached.Get<List<ModuleModel>>(CachedKey.loginModuleKeyCache + RDAuthorize.UserId);
            if (allmenu != null && allmenu.Any())
            {
                lsMenu = allmenu.Where(x => x.parentModuleId.Equals(ParentID)).ToList();
            }
            ViewBag.AllMenus = allmenu;
            return PartialView(lsMenu);
        }

    }
}