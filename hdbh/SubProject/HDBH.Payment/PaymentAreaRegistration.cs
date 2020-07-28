using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.Payment
{
    public class PaymentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Payment";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {


            context.MapRoute(
                "Payment_default",
                "Payment/{controller}/{action}/{id}",
                new { controller = "Manager", action = "Index", id = UrlParameter.Optional },
                new string[] { "HDBH.Payment.Controllers" }
            );


        }
    }
}