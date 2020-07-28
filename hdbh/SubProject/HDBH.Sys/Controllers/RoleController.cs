using HDBH.Authorize;
using HDBH.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.Sys.Controllers
{
    public class RoleController : BasedController
    {
        private IUserService _userService;
        public RoleController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _getListRole(string[] selected = null)
        {
            var result = _userService.getListRole();
            List<SelectListItem> lsOption = new List<SelectListItem>();
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    SelectListItem _option = new SelectListItem();
                    if(selected != null)
                    {
                        _option.Selected = selected.ToList().Any(x => x.ToUpper() == item.roleCode.ToUpper());
                    }
                    _option.Value = item.roleCode;
                    _option.Text = item.roleName;
                    lsOption.Add(_option);
                }
            }
            return PartialView("~/Views/Shared/_renderOptionColumn.cshtml", lsOption);
        }
    }
}