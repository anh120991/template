using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.Sys
{
    public class SysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "Sys_default",
                "Sys/{controller}/{action}/{id}",
                new { controller = "User", action = "Index", id = UrlParameter.Optional },
                new string[] { "HDBH.Sys.Controllers" }
            );


        }
    }
}