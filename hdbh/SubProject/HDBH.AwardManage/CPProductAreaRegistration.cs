using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.AwardManage
{
    public class CPProductAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CPProduct";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "CPProduct",
                "CPProduct/{controller}/{action}/{id}",
                new { controller = "Manager", action = "Index", id = UrlParameter.Optional },
                new string[] { "HDBH.CPProduct.Controllers" }
            );


        }
    }
}