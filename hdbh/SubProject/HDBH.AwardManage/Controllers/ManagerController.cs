using ClosedXML.Excel;
using HDBH.Authorize;
using HDBH.Language;
using HDBH.Lib;
using HDBH.Lib.Interface;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Models.Extensions;
using HDBH.Models.ViewModel;
using HDBH.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HDBH.CPProduct.Controllers
{
    public class ManagerController : BasedController
    {
        // GET: Manager
        string _strSectionProduct = "_strSectionProduct";
        string _strSectionCommisionList = "_strSectionCommisionList";
        private ICounterPartyService _counterPartyService;
        private ICPProductService _cPProductService;
        public ManagerController(ICounterPartyService counterPartyService, ICPProductService cPProductService)
        {
            _counterPartyService = counterPartyService;
            _cPProductService = cPProductService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Inau()
        {
            return View();
        }

        public ActionResult Create(string counterPartyGroup, long counterPartyId, string cifCounterParty)
        {
            CounterPartyGetDetailModel input = new CounterPartyGetDetailModel();
            input.counterPartyId = counterPartyId;
            input.cifCounterParty = cifCounterParty;
            input.userId = RDAuthorize.UserId;
            var result = _counterPartyService.getDetailCounterParty(input);
            CPProductViewModel model = new CPProductViewModel();
            Library.TransferData(result, ref model);
            TheSession.Remove(_strSectionProduct); //xóa section truoc
            return View(model);
        }

        public ActionResult Update(string counterPartyGroup, long counterPartyId, string cifCounterParty, int isInau)
        {
            TheSession.Remove(_strSectionProduct); //xóa section list product
            CPProductGetDetailViewModel input = new CPProductGetDetailViewModel();
            input.cifCounterParty = cifCounterParty;
            input.counterPartyGroup = counterPartyGroup;
            input.counterPartyId = counterPartyId;
            input.isInau = isInau;
            input.userId = RDAuthorize.UserId;
            var result = _cPProductService.getDetailCpProdCommis(input);

            #region
            CPProductViewModel model = new CPProductViewModel();
            List<CPProductImportProductModel> productList = new List<CPProductImportProductModel>();
            List<CPProductImportCommisionListModel> commisionList = new List<CPProductImportCommisionListModel>();
            List<AttachmentViewModel> fileList = new List<AttachmentViewModel>();

            if (result != null)
            {
                Library.TransferData(result, ref model);//main
            }

            if (result.fileList != null && result.fileList.Any())
            {
                Library.TransferData(result.fileList, ref fileList);
            }

            if (result.productList != null && result.productList.Any())
            {
                Library.TransferData(result.productList, ref productList);
                foreach (var item in result.productList)
                {
                    if (item.commisionList != null && item.commisionList.Any())
                    {
                        foreach (var item1 in item.commisionList)
                        {
                            CPProductImportCommisionListModel tempModel = new CPProductImportCommisionListModel();
                            Library.TransferData(item1, ref tempModel);
                            tempModel.SelectList = productList;
                            tempModel.productCode = productList.SingleOrDefault(x => x.productId == tempModel.productId).productCode;
                            commisionList.Add(tempModel);
                        }
                    }
                }
            }
            model.commisionList = commisionList;
            model.productList = productList;
            model.viewMode = ViewModeCons.UPDATE;
            ViewBag.viewMode = ViewModeCons.UPDATE;
            ViewBag.fileList = fileList;
            #endregion
            return View("Create", model);
        }

        public ActionResult View(string counterPartyGroup, long counterPartyId, string cifCounterParty, int isInau)
        {
            TheSession.Remove(_strSectionProduct); //xóa section list product
            CPProductGetDetailViewModel input = new CPProductGetDetailViewModel();
            input.cifCounterParty = cifCounterParty;
            input.counterPartyGroup = counterPartyGroup;
            input.counterPartyId = counterPartyId;
            input.isInau = isInau;
            input.userId = RDAuthorize.UserId;
            var result = _cPProductService.getDetailCpProdCommis(input);

            #region
            CPProductViewModel model = new CPProductViewModel();
            List<CPProductImportProductModel> productList = new List<CPProductImportProductModel>();
            List<CPProductImportCommisionListModel> commisionList = new List<CPProductImportCommisionListModel>();
            List<AttachmentViewModel> fileList = new List<AttachmentViewModel>();

            if (result != null)
            {
                Library.TransferData(result, ref model);//main
            }

            if (result.fileList != null && result.fileList.Any())
            {
                Library.TransferData(result.fileList, ref fileList);
            }

            if (result.productList != null && result.productList.Any())
            {
                Library.TransferData(result.productList, ref productList);
                foreach (var item in result.productList)
                {
                    if (item.commisionList != null && item.commisionList.Any())
                    {
                        foreach (var item1 in item.commisionList)
                        {
                            CPProductImportCommisionListModel tempModel = new CPProductImportCommisionListModel();
                            Library.TransferData(item1, ref tempModel);
                            tempModel.SelectList = productList;
                            tempModel.productCode = productList.SingleOrDefault(x => x.productId == tempModel.productId).productCode;
                            commisionList.Add(tempModel);
                        }
                    }
                }
            }
            model.commisionList = commisionList;
            model.productList = productList;
            model.viewMode = ViewModeCons.VIEW;
            ViewBag.viewMode = ViewModeCons.VIEW;
            ViewBag.fileList = fileList;
            #endregion
            return View("Create", model);
        }


        public ActionResult Approve(string counterPartyGroup, long counterPartyId, string cifCounterParty, int isInau)
        {
            CPProductGetDetailViewModel input = new CPProductGetDetailViewModel();
            input.cifCounterParty = cifCounterParty;
            input.counterPartyGroup = counterPartyGroup;
            input.counterPartyId = counterPartyId;
            input.isInau = isInau;
            input.userId = RDAuthorize.UserId;
            var result = _cPProductService.getDetailCpProdCommis(input);

            #region
            CPProductViewModel model = new CPProductViewModel();
            List<CPProductImportProductModel> productList = new List<CPProductImportProductModel>();
            List<CPProductImportCommisionListModel> commisionList = new List<CPProductImportCommisionListModel>();
            List<AttachmentViewModel> fileList = new List<AttachmentViewModel>();

            if (result != null)
            {
                Library.TransferData(result, ref model);//main
            }

            if (result.fileList != null && result.fileList.Any())
            {
                Library.TransferData(result.fileList, ref fileList);
            }

            if (result.productList != null && result.productList.Any())
            {
                Library.TransferData(result.productList, ref productList);
                foreach (var item in result.productList)
                {
                    if (item.commisionList != null && item.commisionList.Any())
                    {
                        foreach (var item1 in item.commisionList)
                        {
                            CPProductImportCommisionListModel tempModel = new CPProductImportCommisionListModel();
                            Library.TransferData(item1, ref tempModel);
                            tempModel.SelectList = productList;
                            tempModel.productCode = productList.SingleOrDefault(x => x.productId == tempModel.productId).productCode;
                            commisionList.Add(tempModel);
                        }
                    }
                }
            }
            model.commisionList = commisionList;
            model.productList = productList;
            model.viewMode = ViewModeCons.APPROVE;
            ViewBag.viewMode = ViewModeCons.APPROVE;
            ViewBag.fileList = fileList;
            #endregion
            return View("Create", model);
        }

        [HttpPost]
        public JsonResult ImportProduct(HttpPostedFileBase importProduct)
        {
            string fileName = "";
            object ls;
            object retList = null;
            try
            {
                var file = importProduct;
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
                    ls = reader.GetData<CPProductImportTempProductModel>(podata);
                    if (ls != null)
                    {
                        int idx = 1;
                        List<CPProductImportProductModel> lsImport = new List<CPProductImportProductModel>();
                        foreach (var itx in ls as List<CPProductImportTempProductModel>)
                        {
                            if (!string.IsNullOrEmpty(itx.productCode))
                            {
                                string strProductCode = itx.productCode.FilterSpecial();
                                string strProductName = itx.productName.FilterSpecial();
                                string strMessage = string.Empty;

                                if (string.IsNullOrEmpty(strProductCode))
                                {
                                    strMessage += " Chưa nhập Product Code";
                                }
                                else
                                {
                                    if (HasSpecialChars(strProductCode))
                                        strMessage += " Lỗi ký tự đặc biệt";
                                    if (HasUnicode(strProductCode))
                                        strMessage += " Lỗi ký unicode";
                                    if (HasSpace(strProductCode))
                                        strMessage += " Lỗi khoảng trắng";
                                }

                                var item = new CPProductImportProductModel
                                {
                                    productCode = strProductCode,
                                    productName = strProductName,
                                    errorMessage = strMessage

                                };

                                lsImport.Add(item);
                                idx++;
                            }
                        }
                        retList = lsImport;
                    }
                    TheSession.TrySet(_strSectionProduct, retList);
                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ManagerController => ImportProduct", ex);
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                    System.IO.File.Delete(fileName);
            }
            return Json(RenderPartialViewToString("~/Areas/CPProduct/Views/Shared/_templateProductImport.cshtml", retList));
        }


        public bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }
        public bool HasUnicode(string input)
        {
            return !Regex.IsMatch(input, "^[a-zA-Z0-9]*$");
        }

        public bool HasSpace(string input)
        {
            return input.Contains(" ");
        }

        private List<CPProductImportProductModel> validateProduct(List<CPProductImportProductModel> lsFile)
        {
            if (lsFile != null && lsFile.Any())
            {
                foreach (var item in lsFile)
                {
                    item.errorMessage = string.Empty;
                    if (HasSpecialChars(item.productCode))
                    {
                        item.errorMessage += " Lỗi ký tự đặc biết";
                    }
                    if (HasUnicode(item.productCode))
                    {
                        item.errorMessage += " Lỗi ký ký tự có dấu";
                    }
                    if (HasSpace(item.productCode))
                    {
                        item.errorMessage += " Lỗi khoảng trắng";
                    }
                }
            }
            return lsFile;
        }

        [HttpPost]
        public JsonResult ImportCommisionList(HttpPostedFileBase importCommisionList)
        {
            string fileName = "";
            object ls;
            object retList = null;
            try
            {
                var file = importCommisionList;
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
                    ls = reader.GetData<CPProductImportTempCommisionListModel>(podata);
                    if (ls != null)
                    {
                        int idx = 1;
                        List<CPProductImportCommisionListModel> lsImport = new List<CPProductImportCommisionListModel>();
                        List<CPProductImportProductModel> lsProduct = new List<CPProductImportProductModel>();
                        object temp;
                        TheSession.TryGet(_strSectionProduct, out temp);
                        if (temp != null)
                        {
                            lsProduct = (List<CPProductImportProductModel>)temp;
                        }
                        if(lsProduct != null && lsProduct.Any())
                        {
                            foreach (var itx in ls as List<CPProductImportTempCommisionListModel>)
                            {
                                if (!string.IsNullOrEmpty(itx.productCode))
                                {
                                    string strProductCode = itx.productCode.FilterSpecial();
                                    string strMessage = string.Empty;
                                    bool isExists = false;

                                    if (lsProduct != null && lsProduct.Any())
                                    {
                                        lsProduct = lsProduct.Where(x => string.IsNullOrEmpty(x.errorMessage)).ToList();
                                        isExists = lsProduct.Any(x => x.productCode.ToUpper().Trim().Equals(strProductCode.ToUpper().Trim()));
                                    }
                                    else
                                    {
                                        strMessage += " Không tìm thấy danh sách mã sản phẩm";
                                    }
                                    if (!isExists)
                                    {
                                        strMessage += " Mã sản phẩm không tồn tại";
                                    }

                                    if (itx.effectedFromDate == null)
                                    {
                                        strMessage += " Thiếu trường hiệu lực từ ngày";
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(itx.effectedFromDate.ToString()).Ticks < DateTime.Now.Date.Ticks)
                                        {
                                            strMessage += " Ngày hiệu lực từ ngày phải lớn hơn ngày hiện tại";
                                        }
                                    }
                                    if (itx.effectedToDate == null)
                                    {
                                        strMessage += " Thiếu trường hiệu lực đến ngày";
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(itx.effectedToDate.ToString()).Ticks < DateTime.Now.Date.Ticks)
                                        {
                                            strMessage += " Ngày hiệu lực đến ngày phải lớn hơn ngày hiện tại";
                                        }
                                    }
                                    var item = new CPProductImportCommisionListModel
                                    {
                                        productCode = strProductCode,
                                        productName = itx.productName,
                                        effectedFromDate = itx.effectedFromDate != null ? DateTime.Parse(itx.effectedFromDate.ToString()) : new DateTime(),
                                        effectedToDate = itx.effectedToDate != null ? DateTime.Parse(itx.effectedToDate.ToString()) : new DateTime(),
                                        totalCommisRate = (itx.agencyCommisRate ?? 0) + (itx.supportCommisRate ?? 0) + (itx.serviceCostRate ?? 0),
                                        agencyCommisRate = (itx.agencyCommisRate ?? 0),
                                        supportCommisRate = (itx.supportCommisRate ?? 0),
                                        serviceCostRate = (itx.serviceCostRate ?? 0),
                                        commisRate = (itx.commisRate ?? 0),
                                        errorMessage = strMessage
                                    };
                                    item.remainRate = item.totalCommisRate - item.commisRate;
                                    item.SelectList = new List<SelectListItem>();
                                    item.SelectList = lsProduct;
                                    lsImport.Add(item);
                                    idx++;
                                }

                            }
                            retList = lsImport;
                            TheSession.TrySet(_strSectionCommisionList, retList);
                            return Json(new { Code = 0, Message = RenderPartialViewToString("~/Areas/CPProduct/Views/Shared/_templateCommissionListImport.cshtml", retList) });
                        }
                        else
                        {
                            return Json(new { Code = -1, Message = "Không tìm thấy danh sách sản phẩm" });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ManagerController => ImportCommisionList", ex);
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                    System.IO.File.Delete(fileName);
            }
            return Json(new { Code = -1, Message = "Không tìm thấy danh sách sản phẩm" });
        }


        #region Export Excel
        public ActionResult ExportCommisionList()
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    List<CPProductImportCommisionListModel> lsImport = new List<CPProductImportCommisionListModel>();
                    object temp;
                    TheSession.TryGet(_strSectionCommisionList, out temp);
                    if (temp != null)
                    {
                        lsImport = (List<CPProductImportCommisionListModel>)temp;
                    }
                    if (lsImport.Any())
                    {
                        wb.Worksheets.Add(MapModelToNewTableCommision(lsImport), "CommisionListTemplate");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", $"attachment;filename={DateTime.Now:yyyyMMdd}_CounterPartyTemplate.xlsx");
                        using (MemoryStream myMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(myMemoryStream);
                            myMemoryStream.WriteTo(Response.OutputStream);
                            Response.End();
                            return Content("Success");
                        }
                    }
                    return Content("No Data");
                }
            }
            catch (Exception ex)
            {
                HDBH.Log.WriteLog.Error("ManagerController => ExportCommisionList", ex);
                return Content("No Data");
            }
        }

        private DataTable MapModelToNewTableCommision(List<CPProductImportCommisionListModel> table)
        {
            //  GLinks is the new DataTable I want to construct and bind to a DataList, Repeater, ListView  
            DataTable gLinks = new DataTable();
            DataRow dr = null;
            //gLinks.Columns.Add(new DataColumn("Mã KH", typeof(int)));
            gLinks.Columns.Add(new DataColumn("Mã sản phẩm", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Tên sản phẩm", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Hiệu lực từ ngày", typeof(DateTime)));
            gLinks.Columns.Add(new DataColumn("Hiệu lực đến ngày", typeof(DateTime)));
            gLinks.Columns.Add(new DataColumn("Hoa hồng tổng %", typeof(decimal)));
            gLinks.Columns.Add(new DataColumn("Đối tác (hoa hồng đại lý %)", typeof(decimal)));
            gLinks.Columns.Add(new DataColumn("Đối tác (hỗ trợ thi đua %)", typeof(decimal)));
            gLinks.Columns.Add(new DataColumn("Đối tác (chi phí dịch vụ %)", typeof(decimal)));
            gLinks.Columns.Add(new DataColumn("Chi thưởng %", typeof(decimal)));
            gLinks.Columns.Add(new DataColumn("Còn lại", typeof(decimal)));
            var tblnew = table;
            foreach (var row in tblnew)
            {
                dr = gLinks.NewRow();
                //dr["Mã KH"] = row.counterPartyId;
                dr["Mã sản phẩm"] = row.productCode;
                dr["Tên sản phẩm"] = row.productName;
                dr["Hiệu lực từ ngày"] = row.effectedFromDate.ToString("dd/MM/yyyy");
                dr["Hiệu lực đến ngày"] = row.effectedToDate.ToString("dd/MM/yyyy");
                dr["Hoa hồng tổng %"] = row.totalCommisRate;
                dr["Đối tác (hoa hồng đại lý %)"] = row.agencyCommisRate;
                dr["Đối tác (hỗ trợ thi đua %)"] = row.supportCommisRate;
                dr["Đối tác (chi phí dịch vụ %)"] = row.serviceCostRate;
                dr["Chi thưởng %"] = row.commisRate;
                dr["Còn lại"] = row.remainRate;
                gLinks.Rows.Add(dr);
            }

            return gLinks;
        }
        #endregion

        public JsonResult LoadCounterPartyByGroupId(string groupCode)
        {
            string userId = RDAuthorize.UserId;
            var result = _counterPartyService.getListCounterParty(groupCode, userId);
            return Json(RenderPartialViewToString("~/Areas/CPProduct/Views/Shared/_renderCounterPartySelectBox.cshtml", result), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertProduct(CPProductInsertViewModel input)
        {
            CPProductInsertModel model = new CPProductInsertModel();
            List<CPProductInsertProductItemModel> productList = new List<CPProductInsertProductItemModel>();
            List<CPProductInsertCommisionItemModel> commisionList = new List<CPProductInsertCommisionItemModel>();
            List<CPProductInsertAttachmenItemModel> fileList = new List<CPProductInsertAttachmenItemModel>();

            Library.TransferData(input, ref model);
            Library.TransferData(input.fileList, ref fileList);
            Library.TransferData(input.productList, ref productList);
            Library.TransferData(input.commisionList, ref commisionList);

            model.productList = productList;
            model.fileList = fileList;
            model.commisionList = commisionList;


            model.userId = RDAuthorize.UserId;
            model.branchCode = RDAuthorize.BranchCode;
            model.jsonProductList = JsonConvert.SerializeObject(model.productList);
            model.jsonCommisionList = JsonConvert.SerializeObject(model.commisionList);
            model.jsonFileList = JsonConvert.SerializeObject(model.fileList);
            var result = _cPProductService.insertCpProdCommis(model).readResult();
            return Json(result);
        }

        public JsonResult Search(CPProductSearchViewModel input)
        {
            var result = new CPProductSearchModel();
            result = _cPProductService.searchCpProdCommis(input);
            return Json(new
            {
                recordsTotal = result.totalRecord,
                recordsFiltered = result.totalRecord,
                data = result.resultList
            });
        }

        public JsonResult UpdateProduct(CPProductInsertViewModel input)
        {
            CPProductInsertModel model = new CPProductInsertModel();
            List<CPProductInsertProductItemModel> productList = new List<CPProductInsertProductItemModel>();
            List<CPProductInsertCommisionItemModel> commisionList = new List<CPProductInsertCommisionItemModel>();
            List<CPProductInsertAttachmenItemModel> fileList = new List<CPProductInsertAttachmenItemModel>();

            Library.TransferData(input, ref model);
            Library.TransferData(input.fileList, ref fileList);
            Library.TransferData(input.productList, ref productList);
            Library.TransferData(input.commisionList, ref commisionList);

            model.productList = productList;
            model.fileList = fileList;
            model.commisionList = commisionList;


            model.userId = RDAuthorize.UserId;
            model.branchCode = RDAuthorize.BranchCode;
            model.jsonProductList = model.productList.Count > 0 ? JsonConvert.SerializeObject(model.productList): null;
            model.jsonCommisionList = model.commisionList.Count > 0 ? JsonConvert.SerializeObject(model.commisionList) : null;
            model.jsonFileList = model.fileList.Count > 0 ? JsonConvert.SerializeObject(model.fileList) : null;
            var result = _cPProductService.updateCpProdCommis(model).readResult();
            return Json(result);

        }

        public JsonResult SearchInau(CPProductSearchViewModel input)
        {
            var result = new CPProductSearchModel();
            result = _cPProductService.searchCpProdCommisInau(input);
            return Json(new
            {
                recordsTotal = result.totalRecord,
                recordsFiltered = result.totalRecord,
                data = result.resultList
            });
        }

        public JsonResult ApproveProduct(CPProductApproveModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
             var result = _cPProductService.approveCpProdCommis(input).readResult();
            return Json(result);

        }

        public PartialViewResult LoadCounterPartyForContract(long counterPartyIdSelected)
        {
            string userId = RDAuthorize.UserId;
            var result = _counterPartyService.getListCounterParty("CONTRACTED", userId);
            List<SelectListItem> lsSelect = result.Select(x => new SelectListItem()
            {
                Selected = counterPartyIdSelected == x.counterPartyId,
                Text = x.counterPartyName,
                Value = x.counterPartyId.ToString()
            }).ToList();
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }
        [HttpPost]
        public JsonResult deleteCpProdCommis(long counterPartyId, string counterPartyGroup, string status)
        {
            var result = _cPProductService.deleteCpProdCommis(counterPartyId, counterPartyGroup, status, RDAuthorize.UserId, RDAuthorize.BranchCode).readResult();
            return Json(result);
        }
    }


}