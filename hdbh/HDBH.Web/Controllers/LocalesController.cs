
using HDBH.Language;
using System;
using System.Web.Mvc;

namespace HDBH.Web.Controllers
{
    public class LocalesController : Controller
    {

        public ActionResult Index(string lang = "vi_VN")
        {
            Response.Cookies["CacheLang"].Value = lang;
            if (Request.UrlReferrer != null)
                Response.Redirect(Request.UrlReferrer.ToString());
			var message = Localization.Get("changedlng");

            return Content(message);
        }

    }
}