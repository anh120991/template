using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.InsuranceContract
{
    public class InsuranceContractAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "InsuranceContract";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "InsuranceContract_default",
              "InsuranceContract/{controller}/{action}/{id}",
              new { controller = "Manager", action = "Index", id = UrlParameter.Optional },
              new string[] { "HDBH.InsuranceContract.Controllers" }
          );
        }
    }
}