using HDBH.Authorize;
using HDBH.Language;
using HDBH.Lib;
using HDBH.Lib.Interface;
using HDBH.Models.DatabaseModel;
using HDBH.Models.ViewModel;
using HDBH.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.Sys.Controllers
{
    public class UserController : BasedController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult Search(UserSearchViewModel input)
        {
            var result = new UserSearchModel();
            result = _userService.searchUser(input);
            return Json(new
            {
                recordsTotal = result.totalRecord,
                recordsFiltered = result.totalRecord,
                data = result.resultList
            });
        }

        public JsonResult insertUser(UserInsertViewModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            if(input.userRoleList != null && input.userRoleList.Any())
            {
                input.strUserRoleList = JsonConvert.SerializeObject(input.userRoleList);
            }
            var result = _userService.insertUser(input).readResult();
            return Json(result);
        }

        [HttpPost]
        public JsonResult ImportUserList(HttpPostedFileBase btnImportUser)
        {
            string fileName = "";
            object ls;
            try
            {
                var file = btnImportUser;
                if (file != null)
                {
                    string[] extFile = new string[] { "xlsx" };
                    fileName = string.Format("{0}\\{1}{2}", Server.MapPath("~/Content/Uploads"), DateTime.Now.ToString("yyyyMMddHHmmss"), System.IO.Path.GetExtension(file.FileName));
                    if (fileName.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
                    {
                        file.SaveAs(fileName);
                    }

                    Reader reader = new Reader(fileName);
                    IDataTransformer podata = new DataTransformer();
                    ls = reader.GetData<UserImportItemModel>(podata);
                    if (ls != null)
                    {
                        int idx = 1;
                        List<UserImportItemModel> lsImport = new List<UserImportItemModel>();
                        foreach (var itx in ls as List<UserImportItemModel>)
                        {
                            if (!string.IsNullOrEmpty(itx.userName))
                            {
                                var item = new UserImportItemModel
                                {
                                   userName = itx.userName,
                                    fullName = itx.fullName,
                                    email = itx.email,
                                    officerCode = itx.officerCode,
                                    phoneNumber = itx.phoneNumber,
                                    userBranchCode = itx.userBranchCode,
                                };
                                lsImport.Add(item);
                                idx++;
                            }

                        }
                        UserImportModel model = new UserImportModel();
                        model.userId = RDAuthorize.UserId;
                        model.branchCode = RDAuthorize.BranchCode;
                        if(lsImport != null && lsImport.Any())
                        {
                            model.strUserList = JsonConvert.SerializeObject(lsImport);
                        }

                        return Json(new { Code = 0, Message="Thành công!" });
                    }

                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ManagerController => ImportCommisionList", ex);
                return Json(new { Code = 1, Message = "Đã có lỗi xảy ra" });
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                    System.IO.File.Delete(fileName);
            }
            return Json(new { Code = -1, Message = "Không tìm thấy danh sách sản phẩm" });
        }
    }
}