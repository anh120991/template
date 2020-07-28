using HDBG.Log;
using HDBH.Authorize;
using HDBH.Authorize.Models;
using HDBH.Log.Enums;
using HDBH.Models;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Models.ViewModel;
using HDBH.Services.Interface;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
namespace HDBH.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IModuleService _moduleService;
        private ICached _icached;
        public AccountController(IUserService userService, IModuleService moduleService, ICached cached)
        {
            _userService = userService;
            _moduleService = moduleService;
            _icached = cached;
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            try
            {

                UserLoginModel user = new UserLoginModel();
                ViewData["isNotLogin"] = true;
                if (ModelState.IsValid)
                {
                    bool isValid = false;
                    bool isExist = true;
                    string ActiveDomain = WebConfigurationManager.AppSettings["ActiveDomain"].ToString();
                    string _userId = frm["UserId"];
                    string _passWord = frm["Password"];

                    if ((!String.IsNullOrWhiteSpace(_userId)) && (!String.IsNullOrWhiteSpace(_passWord)))
                    {
                        isValid = true;
                        HDBH.Log.WriteLog.Error("isValid: " + isValid.ToString());
                        //                        using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ActiveDomain))
                        //                        {
                        //                            // validate the credentials
                        //#if DEBUG
                        //                            //                            string debug_username = WebConfigurationManager.AppSettings["debug_username"].ToString();
                        //                            //                          string debug_password = WebConfigurationManager.AppSettings["debug_password"].ToString();
                        //                            //isValid = pc.ValidateCredentials(frm["UserName"].ToString().ToLower(), frm["Password"].ToString(), ContextOptions.Negotiate);
                        //                            isValid = true;
                        //#else

                        //                            // string debug_username = WebConfigurationManager.AppSettings["debug_username"].ToString();
                        //                            //string debug_password = WebConfigurationManager.AppSettings["debug_password"].ToString();
                        //                            //isValid = pc.ValidateCredentials(debug_username, debug_password, ContextOptions.Negotiate);
                        //                           isValid = pc.ValidateCredentials(frm["UserName"].ToString().ToLower(), frm["Password"].ToString(), ContextOptions.Negotiate);
                        //#endif
                        //                        }
                        if (isValid)
                        {
                            user = _userService.getAllPermissionUser(_userId);
                            if (user != null && !string.IsNullOrEmpty(user.userName))
                            {
                                RDAuthorize.Set(user);
                                SetLogin(user);
                                isExist = true;
                            }
                            else
                            {
                                isExist = false;
                            }
                        }

                    }

                    if (isValid && isExist)
                    {
                        return Json(new { Result = 0, Url = TempData["returnUrl"] != null ? TempData["returnUrl"] : Url.Action("Index", "Home") });
                    }
                    else
                    {
                        return Json(new { Result = 1, Url = Url.Action("Login", "Account") });
                    }
                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("AccountController => Login", ex);
            }
            return View();
        }

        public void SetLogin(UserLoginModel user)
        {
            List<ModuleModel> lsModule = _moduleService.getListModule();
            if(user != null && user.permissionList.Any())
            {
                lsModule = lsModule.Where(x => user.permissionList.Any(y => y.permissionCode.ToUpper().Equals(x.modulePermission))).ToList();
                _icached.Set(CacheModeEnum.Add, CachedKey.loginModuleKeyCache + user.userName, lsModule, new TimeSpan(1, 0, 0, 0));
                _icached.Set(CacheModeEnum.Add, CachedKey.loginPermissionKeyCache + user.userName, user.permissionList, new TimeSpan(1, 0, 0, 0));
            }
        }

        public ActionResult LogOut()
        {
            //clearCookies();

            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();


            #region clear cache
            //_cached.RemoveCached(Uitil.GenKeyCacheByUser(RDAuthorize.UserId, Resources.KeyCache.keyCurrentmenuUser));
            //_cached.RemoveCached(Uitil.GenKeyCacheByUser(RDAuthorize.UserId, Resources.KeyCache.keyCurrentpermisisonUser));
            //GlobalCachingProvider.Instance.RemoveItem(Uitil.GenKeyCacheByUser(RDAuthorize.UserId, Resources.KeyCache.keyCurrentmenuUser));
            //GlobalCachingProvider.Instance.RemoveItem(Uitil.GenKeyCacheByUser(RDAuthorize.UserId, Resources.KeyCache.keyCurrentpermisisonUser));
            #endregion
            return RedirectToAction("Login");
        }
    }
}