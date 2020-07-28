using HDBH.Authorize.Models;
using HDBH.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class RDAuthorize
{

    private static Random random = new Random();
    public static void Set(UserLoginModel item)
    {
        HttpContext.Current.Session[SessionConstant.userid] = item.userName; //userid
        HttpContext.Current.Session[SessionConstant.fullname] = item.fullName; //full name
        HttpContext.Current.Session[SessionConstant.branchcode] = item.branchCode;
        HttpContext.Current.Session[SessionConstant.listpermission] = item.permissionList; //permission of login user
        HttpContext.Current.Session[SessionConstant.isHO] = item.isHO; //check HO of login user

    }
    public static string UserId => Convert.ToString(HttpContext.Current.Session[SessionConstant.userid]);
    public static string FullName => Convert.ToString(HttpContext.Current.Session[SessionConstant.fullname]);
    public static string BranchName => Convert.ToString(HttpContext.Current.Session[SessionConstant.branchname]);
    public static string BranchCode => Convert.ToString(HttpContext.Current.Session[SessionConstant.branchcode]);
    public static string lstPermission => Convert.ToString(HttpContext.Current.Session[SessionConstant.listpermission]);
    public static int isHO => Convert.ToInt32(HttpContext.Current.Session[SessionConstant.isHO]);
}
