using HDBH.Authorize;
using HDBH.Lib;
using HDBH.Models.DatabaseModel;
using HDBH.Models.ViewModel;
using HDBH.Services.Interface;
using HDBH.Web.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HDBH.Web.Controllers
{
    public class CommonController : BasedController
    {

        private ICustomerService _customerService;

        public CommonController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public JsonResult  searchPopupCustomer(CustomerInputSearchModel input)
        {
            var result = new CustomerModel();
            result = _customerService.searchCustomer(input);
            return Json(new
            {
                recordsTotal = result.TotalRecord,
                recordsFiltered = result.TotalRecord,
                data = result.resultList
            });
        }
        [HttpGet]
        public JsonResult loadFormByView(string viewName, object model = null)
        {
            return Json(RenderPartialViewToString(viewName, model), JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFile(string url, string fileName)
        {
            string urlEncode = Library.decode(url);
            byte[] fileBytes = System.IO.File.ReadAllBytes(urlEncode);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, DateTime.Now.ToString("ddMMyyyy") + fileName);
        }

        public JsonResult UploadFile()
        {
            List<AttachmentViewModel> lsResult = new List<AttachmentViewModel>();
            if (Request.Files.Count > 0)
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["uploadFilePath"] + "/" + DateTime.Now.ToString("ddMMyyyy") + "/" + RDAuthorize.UserId + "/";
                bool exists = Directory.Exists(Server.MapPath(path));
                if (!exists)
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                                //Use the following properties to get file's name, size and MIMEType
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    //To save file, use SaveAs method
                    try
                    {
                        file.SaveAs(Server.MapPath(path + "/") + fileName);
                        lsResult.Add(new AttachmentViewModel()
                        {
                            fileName = fileName,
                            filePath = Library.encode(Server.MapPath(path + "/") + fileName),
                            extention = Library.getExtentionFile(path + "/" + fileName),
                            isRemove = true
                        }); ;
                    }
                    catch
                    {
                        lsResult.Add(new AttachmentViewModel()
                        {
                            fileName = "Error",
                            filePath = fileName + ": File bị lỗi"
                        });
                    }
                }
            }
            return Json(lsResult);
        }

        public JsonResult RemoveFile(string filePath)
        {
            string urlEncode = Library.decode(filePath);
            try
            {
                if (System.IO.File.Exists(urlEncode))
                {
                    System.IO.File.Delete(urlEncode);
                    return Json(new  { Code = 1, Message = "Delete success" });
                }
                return Json(new { Code = 0, Message = "File not found" });
            }
            catch (IOException ioExp)
            {
                HDBH.Log.WriteLog.Error("ManagerController => ExportCommisionList", ioExp);
                return Json(new { Code = 0, Message = "Has Error" });
            }
        }


        // GET: Common
        //private IBranchListService _branchListService;
        //private IContractFormService _contractFormService;
        //private IContractStatusService _contractStatusService;
        //private IExpectTypeService _expectTypeService;
        //private ICounterPartyService _counterPartyService;
        //private IProductService _productService;
        //private IContractTypeService _contractTypeService;
        //private IProductCounterPartyService _productCounterPartyService;


        //public CommonController(IBranchListService branchListService, IContractFormService contractFormService,
        //    IContractStatusService contractStatusService, IExpectTypeService expectTypeService, ICounterPartyService counterPartyService, IProductService productService, IContractTypeService contractTypeService
        //    , IProductCounterPartyService productCounterPartyService)
        //{
        //    _branchListService = branchListService;
        //    _contractFormService = contractFormService;
        //    _contractStatusService = contractStatusService;
        //    _expectTypeService = expectTypeService;
        //    _counterPartyService = counterPartyService;
        //    _productService = productService;
        //    _contractTypeService = contractTypeService;
        //    _productCounterPartyService = productCounterPartyService;
        //}
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public PartialViewResult _GetBranchListToComboBox(List<string> lsSelected)
        //{
        //    var result = _branchListService.SeachBranch(0, 1, "");
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.companyId,
        //        Text = x.companyName
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}

        //public PartialViewResult _GetContractFormListToComboBox(List<string> lsSelected)
        //{
        //    var result = _contractFormService.getContractForm();
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.contractFormCode,
        //        Text = x.contractFormName
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}

        //public PartialViewResult _GetContractStatusListToComboBox(List<string> lsSelected)
        //{
        //    var result = _contractStatusService.getContractStatus();
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.processStatusCode,
        //        Text = x.processStatusName
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}

        //public PartialViewResult _GetCounterPartyListToComboBox(List<string> lsSelected)
        //{
        //    var result = _counterPartyService.getCounterPartyList(null);
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.counterPartyId.ToString(),
        //        Text = x.counterPartyName,
        //        Param = x.cifCounterParty
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}

        //public JsonResult getProductCounterPartyListToComboBox(int? counterPartyId, string cifCounterParty, List<string> lsSelected)
        //{
        //    int intCounterPartyId = counterPartyId != null ? int.Parse(counterPartyId.ToString()) : 0;
        //    var result = _productCounterPartyService.getProductCounterPartyList(intCounterPartyId, cifCounterParty);
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls.Add(new SelectListModel()
        //    {
        //        Value = "",
        //        Text = "-- Chọn sản phẩm --"
        //    });
        //    foreach(var item in result)
        //    {
        //        ls.Add(new SelectListModel()
        //        {
        //            Value = item.productId,
        //            Text = item.productName,
        //        });
        //    }
        //    return Json(RenderPartialViewToString("~/Views/Common/_ComboBox.cshtml", ls));
        //}

        //public PartialViewResult _GetProductCounterPartyToComboBox(string counterPartyId, string cifCounterParty, List<string> lsSelected)
        //{
        //    var result = _productService.getProductCounterParty(counterPartyId, cifCounterParty);
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.productCode,
        //        Text = x.productName
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}
        //public PartialViewResult _GetExpectTypeListToComboBox(List<string> lsSelected)
        //{
        //    var result = _expectTypeService.getExpectType();
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.expectTypeCode,
        //        Text = x.expectTypeName
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}

        //public PartialViewResult _GetContractTypeListToComboBox(int isNonlifeInsurance, List<string> lsSelected)
        //{
        //    var result = _contractTypeService.getContractType(isNonlifeInsurance);
        //    ViewBag.ListSelected = lsSelected;
        //    List<SelectListModel> ls = new List<SelectListModel>();
        //    ls = result.Select(x => new SelectListModel()
        //    {
        //        Value = x.contractTypeCode,
        //        Text = x.contractTypeName
        //    }).ToList();
        //    return PartialView("~/Views/Common/_ComboBox.cshtml", ls);
        //}


        //public FileResult DownloadFile(string url, string fileName)
        //{
        //    string urlEncode = Library.decode(url);
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(urlEncode);
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, DateTime.Now.ToString("ddMMyyyy") + fileName );
        //}

        //public JsonResult UploadFile(){
        //    List<AttachmentViewModel> lsResult = new List<AttachmentViewModel>();
        //    if (Request.Files.Count > 0)
        //    {
        //        string path = System.Configuration.ConfigurationManager.AppSettings["uploadFilePath"] + "/" + DateTime.Now.ToString("ddMMyyyy") + "/" + RDAuthorize.sessionId + "/";
        //        bool exists = Directory.Exists(Server.MapPath(path));
        //        if (!exists)
        //        {
        //            Directory.CreateDirectory(Server.MapPath(path));
        //        }
        //        for (int i = 0; i < Request.Files.Count; i++)
        //        {
        //            HttpPostedFileBase file = Request.Files[i]; //Uploaded file
        //                                                        //Use the following properties to get file's name, size and MIMEType
        //            int fileSize = file.ContentLength;
        //            string fileName = file.FileName;
        //            string mimeType = file.ContentType;
        //            System.IO.Stream fileContent = file.InputStream;
        //            //To save file, use SaveAs method
        //            try
        //            {
        //                file.SaveAs(Server.MapPath(path + "/") + fileName);
        //                lsResult.Add(new AttachmentViewModel()
        //                {
        //                    fileName = fileName,
        //                    filePath = Library.encode(Server.MapPath(path + "/") + fileName),
        //                    extention = Library.getExtentionFile(path + "/" + fileName),
        //                    isRemove = true
        //                }); ;
        //            }
        //            catch
        //            {
        //                lsResult.Add(new AttachmentViewModel()
        //                {
        //                    fileName = "Error",
        //                    filePath = fileName + ": File bị lỗi"
        //                });
        //            }
        //        }
        //    }
        //    return Json(lsResult);
        //}

        //public JsonResult RemoveFile(string filePath)
        //{
        //    string urlEncode = Library.decode(filePath);
        //    try
        //    {
        //        if (System.IO.File.Exists(urlEncode))
        //        {
        //            System.IO.File.Delete(urlEncode);
        //            return Json(new ResultViewModel() { Code = 1, Message= "Delete success" });
        //        }
        //        return Json(new ResultViewModel() { Code = 0, Message = "File not found" });
        //    }
        //    catch (IOException ioExp)
        //    {
        //        return Json(new ResultViewModel() { Code = 0, Message = "Has Error" });
        //    }
        //}

        //public PartialViewResult _GetStatusContract()
        //{
        //    DataSet ContentDataSet = new DataSet();
        //    var file = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/status_contract.xml");
        //    ContentDataSet.ReadXml(file);
        //    DataTable STATUS = new DataTable();
        //    STATUS = ContentDataSet.Tables["STATUS"];
        //    string selected = string.Empty;
        //    var query = from p in STATUS.AsEnumerable()
        //                select new InsContractViewModel
        //                {
        //                    status = p.Field<string>("STATUS_VALUE"),
        //                    statusValue = Utilities.IncheckStatus(p.Field<string>("STATUS_VALUE"))
        //                };
        //    return PartialView(query);
        //}

    }
}