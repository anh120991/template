using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HDBH.Authorize
{
    [DefaultAuthorizeAttribute]
    public class BasedController : Controller
    {
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected static string RenderPartialViewToString(string viewName, string controllerName, object viewData)
        {
            var context = System.Web.HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();

            routeData.Values.Add("controller", controllerName);
            routeData.DataTokens.Add("namespaces", new string[] { "hDBH.Web.Controllers" });
            var controllerContext = new ControllerContext(contextBase,
                                                          routeData,
                                                          new EmptyController());

            var razorViewEngine = new RazorViewEngine();
            var razorViewResult = razorViewEngine.FindView(controllerContext,
                                                           viewName,
                                                           "",
                                                           false);

            var writer = new StringWriter();
            var viewContext = new ViewContext(controllerContext,
                                              razorViewResult.View,
                                              new ViewDataDictionary(viewData),
                                              new TempDataDictionary(),
                                              writer);
            razorViewResult.View.Render(viewContext, writer);

            return writer.ToString();
        }
    }
    class EmptyController : ControllerBase
    {
        protected override void ExecuteCore() { }
    }
}
