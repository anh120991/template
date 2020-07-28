using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.CounterParty
{
    public class CounterPartyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CounterParty";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CounterParty_default",
                "CounterParty/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "HDBH.CounterParty.Controllers" }
            );
        }
    }
}