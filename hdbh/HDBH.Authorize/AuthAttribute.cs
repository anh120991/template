using HDBG.Log;
using HDBH.Authorize.Models;
using HDBH.Models;
using HDBH.Models.Constant;
using HDBH.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace HDBH.Authorize
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthAttribute : AuthorizeAttribute
    {
        private ICached _cached;
        public List<string> PermissionIDs { get; set; }
        public AuthAttribute(params string[] permissions) : base()
        {

            this.PermissionIDs = permissions.Select(p => p).ToList();
        }


        public AuthAttribute(ICached cached)
        {
            _cached = cached;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                if (!filterContext.HasAttribute(typeof(AllowAnonymousAttribute)))
                {
                    string area = filterContext.RouteData.DataTokens["area"] != null ? filterContext.RouteData.DataTokens["area"].ToString().ToLower() : "";
                    string controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
                    string action = filterContext.RouteData.Values["action"].ToString().ToLower();
                    string returnUrl = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri;


                    var username = HttpContext.Current.Session[SessionConstant.userid];
                    if (username == null)
                    {
                        // Redirect to Login Page
                        FormsAuthentication.SignOut();

                        HttpContext.Current.Session[SessionConstant.SessionPreviousUrl] = filterContext.HttpContext.Request.Url;

                        filterContext.RedirectToLogin();

                    }
                    else //nếu đang còn session
                    {
                        string method = HttpContext.Current.Request.HttpMethod;
                        bool check = HDBH.Lib.Library.checkAction(area, controller, action, HttpContext.Current.Request.HttpMethod);
                        if (!check)
                        {
                            bool isAllowAccess = false;
                            string currentUrl = "/" + area + "/" + controller + "/" + action;
                            var lsUserPermission = _cached.Get<List<UserLoginPermission>>(CachedKey.loginModuleKeyCache + RDAuthorize.UserId); 
                            if (lsUserPermission != null && lsUserPermission.Any())
                            {
                                isAllowAccess = lsUserPermission.Any(x => PermissionIDs.Contains(x.permissionCode.ToUpper().Trim()));
                            }
                            else
                            {

                                filterContext.RedirectToLogin();
                            }
                            if (!isAllowAccess)
                            {
                                HDBH.Log.WriteLog.Error("Permission => " + currentUrl + " - User: " + RDAuthorize.UserId, null);
                                filterContext.RedirectTo403();
                            }
                        }
                    }
                }
            }
        }

    }
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
    {
        public string PermissionIDs { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HasAttribute(typeof(AllowAnonymousAttribute)))
                return;

            var username = HttpContext.Current.Session[SessionConstant.userid];
            if (username == null)
            {
                filterContext.RedirectToLogin();
            }
            else
            {
                if (filterContext.NonCheckAction())
                    return;

            }



        }
    }
    public static class ExtensionMethods
    {
        private static List<string> _lsActionNonCheck = new List<string> { "index" }; //action non-check permisison
        private static List<string> _lsControllerNonCheck = new List<string> { "home" }; //controller non-check permission
        private static List<string> _lsAreaNoncheck = new List<string> { "" }; //controller non-check permission


        public static bool HasAttribute(this AuthorizationContext context, Type attribute)
        {
            var actionDesc = context.ActionDescriptor;
            var controllerDesc = actionDesc.ControllerDescriptor;

            bool allowAnon =
                actionDesc.IsDefined(attribute, true) ||
                controllerDesc.IsDefined(attribute, true);

            return allowAnon;
        }





        public static void RedirectToLogin(this AuthorizationContext filterContext)
        {
            FormsAuthentication.SignOut();

            HttpContext.Current.Session[SessionConstant.SessionPreviousUrl] = filterContext.HttpContext.Request.Url;
            string returnUrl = filterContext.HttpContext.Request.RawUrl;
            var ActionResult = new RouteValueDictionary(new
            {
                action = "Login",
                controller = "Account",
                returnUrl = filterContext.HttpContext.Request.RawUrl,
                area = String.Empty
            });


            filterContext.Result = new RedirectToRouteResult(ActionResult);
        }

        public static void RedirectTo403(this AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpStatusCodeResult(403);

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                       { "action", "AccessDenied" },
                                       { "controller", "Error" },
                                       { "Area", String.Empty }
                                });
        }

        public static bool NonCheckAction(this AuthorizationContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString().ToLower(); //get action filter
            string action = filterContext.RouteData.Values["action"].ToString().ToLower(); //get controller filter
            string area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"] != null ? HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString() : ""; //get area filter

            if (_lsActionNonCheck.Any(x => x.ToUpper().Equals(action.ToUpper()))
                  && _lsControllerNonCheck.Any(x => x.ToUpper().Equals(controller.ToUpper()))
                  && _lsAreaNoncheck.Any(x => x.ToUpper().Equals(area.ToUpper()))
                  )
            {
                return true;
            }
            return false;
        }
    }

}
