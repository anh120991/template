using ClosedXML.Excel;
using HDBH.Authorize;
using HDBH.Language;
using HDBH.Lib;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
using HDBH.Models.ViewModel.InsuranceContractModel;
using HDBH.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.InsuranceContract.Controllers
{
    public class ManagerController : BasedController
    {
        private IInsuranceContractService _insuranceContractService;
        private IBranchService _branch;
        private IMDService _mdServices;
        // GET: Manager

        public ManagerController(IInsuranceContractService insuranceContractService, IBranchService branch, IMDService mdServices)
        {
            _insuranceContractService = insuranceContractService;
            _branch = branch;
            _mdServices = mdServices;
        }
        public ActionResult Index()
        {
            ViewBag.lstBranch = _branch.getListBranch(null);
            ViewBag.lstContractType = _mdServices.getListContractType(null, -1);
            return View();
        }

        // Search Function
        [HttpPost]
        public JsonResult SearchList(SearchInsuranceContract input)
        {
            var result = new ListInsuranceContractDetailViewModel();
            input.userId = RDAuthorize.UserId;
            if (input._processStatusList != null)
                input.processStatusList = JsonConvert.SerializeObject(input._processStatusList);
            else
                input.processStatusList = "";
            result = _insuranceContractService.searchInsuranceContract(input);
            return Json(new
            {
                data = result.resultList,
                recordsTotal = result.TotalRecord,
                recordsFiltered = result.TotalRecord,
            });
        }

        #region 5.7 Quản lý HĐBH chờ duyệt – Trưởng Đơn Vị

        /// <summary>
        /// dutp 
        /// 18/7/20
        /// </summary>
        /// <returns></returns>
        public ActionResult InsuranceInauManagementL1()
        {
            ViewBag.lstContractForm = _mdServices.getListContractForm(null, -1);
            ViewBag.isInau = 1;
            return View();
        }

        [HttpPost]
        public JsonResult searchInsuranceContractInau(SearchInsuranceContract input)
        {
            var result = new ListInsuranceContractDetailViewModel();
            input.userId = RDAuthorize.UserId;
            if (input._processStatusList != null)
                input.processStatusList = JsonConvert.SerializeObject(input._processStatusList);
            else
                input.processStatusList = "";
            result = _insuranceContractService.searchInsuranceContractInau(input);
            return Json(new
            {
                data = result.resultList,
                recordsTotal = result.TotalRecord,
                recordsFiltered = result.TotalRecord,
            });
        }
        #endregion

        #region 5.9 Quản lý HĐBH chờ duyệt – BP. Bảo hiểm
        /// <summary>
        /// dutp
        /// 21/7/20
        /// </summary>
        /// <returns></returns>
        public ActionResult InsuranceInauManagementL2()
        {
            ViewBag.lstContractForm = _mdServices.getListContractForm(null, -1);
            ViewBag.isInau = 1;
            return View();
        }
        #endregion

        #region 5.6 Duyệt HĐBH có TSĐB
        [HttpGet]
        public ActionResult ApproveInsuranceContract(InputApproveInsuranceContractModel _param)
        {
            InputApproveInsuranceContractModel input = new InputApproveInsuranceContractModel();
            input.insuranceContractNo = _param.insuranceContractNo;
            input.insuranceId = _param.insuranceId;
            input.userId = RDAuthorize.UserId;
            input.pApproveLevel = _param.pApproveLevel;
            var result = _insuranceContractService.getDetailInsurContract(input);
            InsuranceContractViewModel model = new InsuranceContractViewModel();
            if (result != null)
            {
                Library.TransferData(result, ref model);
                List<InsuranceContractCollateralViewModel> lsCollateral = new List<InsuranceContractCollateralViewModel>();
                if (result.collateralList != null && result.collateralList.Any())
                {
                    Library.TransferData(result.collateralList, ref lsCollateral);
                }
                model.collateralList = lsCollateral;
                List<InsuranceContractProductViewModel> productList = new List<InsuranceContractProductViewModel>();
                if (result.productList != null && result.productList.Any())
                {
                    Library.TransferData(result.productList, ref productList);
                }
                model.productList = productList;
                List<CPProductDetailtAttachmenViewItemModel> fileList = new List<CPProductDetailtAttachmenViewItemModel>();
                if (result.fileList != null && result.fileList.Any())
                {
                    Library.TransferData(result.fileList, ref fileList);
                }
                model.fileList = fileList;
                List<ScheduleListViewModel> lsSchedule = new List<ScheduleListViewModel>();
                if (result.scheduleList != null && result.scheduleList.Any())
                {
                    Library.TransferData(result.scheduleList, ref lsSchedule);
                }
                List<ScheduleItemViewModel> lsPaymentDetail = new List<ScheduleItemViewModel>();
                if (result.paymentDetailList != null && result.paymentDetailList.Any())
                {
                    Library.TransferData(result.paymentDetailList, ref lsPaymentDetail);
                }
                foreach (var schedule in lsSchedule)
                {
                    schedule.scheduleDate = schedule.schedulePaymentDate;
                    schedule.scheduleQuanlity = int.Parse(schedule.scheduleId.ToString());
                    schedule.productList = lsPaymentDetail.Where(x => x.scheduleId == schedule.scheduleId).ToList();
                }
                ViewBag.strScheDuleTable = RenderPartialViewToString("~/Areas/InsuranceContract/Views/Shared/_cheduleList.cshtml", lsSchedule);
            }


            ViewBag.TypeCode = model.contractTypeCode;
            ViewBag.FormCode = model.contractFormCode;
            ViewBag.ExceptCode = model.exceptTypeCode;
            ViewBag.viewMode = ViewModeCons.APPROVE;
            model.viewMode = ViewModeCons.APPROVE;

            ViewBag.ApproveLevel = _param.pApproveLevel;

            return View(model);
        }

        //Function Duyệt 1
        [HttpPost]
        public JsonResult Approve(InputApprove input)
        {
            var result = new ResultModel();
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            if (input.pApproveLevel == 1)
                result = _insuranceContractService.approveInsurContract(input.insuranceId, input.approveStatus, input.approveContent, input.userId, input.branchCode).readResult();
            else if(input.pApproveLevel ==2)
                result = _insuranceContractService.approveInsurContractLv2(input.insuranceId, input.approveStatus, input.approveContent, input.userId, input.branchCode).readResult();
            return Json(result);
        }
        #endregion

        public ActionResult Create(string pFormCode, string pTypeCode, string pTypeName, string pExceptCode, string pContractNo)
        {
            InsuranceContractViewModel model = new InsuranceContractViewModel();
            ViewBag.TypeCode = pTypeCode;
            ViewBag.TypeName = pTypeName;
            ViewBag.FormCode = pFormCode;
            ViewBag.ExceptCode = pExceptCode;
            ViewBag.ContractNo = pContractNo;
            model.viewMode = ViewModeCons.CREATE;
            return View(model);
        }

        //Get update
        public ActionResult Update(long insuranceId, string insuranceContractNo)
        {
            InputApproveInsuranceContractModel input = new InputApproveInsuranceContractModel();
            input.insuranceContractNo = insuranceContractNo;
            input.insuranceId = insuranceId;
            input.userId = RDAuthorize.UserId;
            var result = _insuranceContractService.getDetailInsurContract(input);
            InsuranceContractViewModel model = new InsuranceContractViewModel();
            if (result != null)
            {
                Library.TransferData(result, ref model);
                List<InsuranceContractCollateralViewModel> lsCollateral = new List<InsuranceContractCollateralViewModel>();
                if (result.collateralList != null && result.collateralList.Any())
                {
                    Library.TransferData(result.collateralList, ref lsCollateral);
                }
                model.collateralList = lsCollateral;
                List<InsuranceContractProductViewModel> productList = new List<InsuranceContractProductViewModel>();
                if (result.productList != null && result.productList.Any())
                {
                    Library.TransferData(result.productList, ref productList);
                }
                model.productList = productList;
                List<HDBH.Models.ViewModel.AttachmentViewModel> fileList = new List<AttachmentViewModel>();
                if (result.fileList != null && result.fileList.Any())
                {
                    Library.TransferData(result.fileList, ref fileList);
                }
                ViewBag.fileList = fileList;
                List<ScheduleListViewModel> lsSchedule = new List<ScheduleListViewModel>();
                if (result.scheduleList != null && result.scheduleList.Any())
                {
                    Library.TransferData(result.scheduleList, ref lsSchedule);
                }
                List<ScheduleItemViewModel> lsPaymentDetail = new List<ScheduleItemViewModel>();
                if (result.paymentDetailList != null && result.paymentDetailList.Any())
                {
                    Library.TransferData(result.paymentDetailList, ref lsPaymentDetail);
                }
                foreach (var schedule in lsSchedule)
                {
                    schedule.scheduleDate = schedule.schedulePaymentDate;
                    schedule.scheduleQuanlity = int.Parse(schedule.scheduleId.ToString());
                    schedule.productList = lsPaymentDetail.Where(x => x.scheduleId == schedule.scheduleId).ToList();
                }
                ViewBag.strScheDuleTable = RenderPartialViewToString("~/Areas/InsuranceContract/Views/Shared/_cheduleList.cshtml", lsSchedule);
            }



            ViewBag.TypeCode = model.contractTypeCode;
            ViewBag.FormCode = model.contractFormCode;
            ViewBag.ExceptCode = model.exceptTypeCode;
            model.viewMode = ViewModeCons.UPDATE;
            ViewBag.viewMode = ViewModeCons.UPDATE;
            return View("Create", model);
        }


        public ActionResult View(long insuranceId, string insuranceContractNo)
        {
            InputApproveInsuranceContractModel input = new InputApproveInsuranceContractModel();
            input.insuranceContractNo = insuranceContractNo;
            input.insuranceId = insuranceId;
            input.userId = RDAuthorize.UserId;
            var result = _insuranceContractService.getDetailInsurContract(input);
            InsuranceContractViewModel model = new InsuranceContractViewModel();
            if (result != null)
            {
                Library.TransferData(result, ref model);
                List<InsuranceContractCollateralViewModel> lsCollateral = new List<InsuranceContractCollateralViewModel>();
                if (result.collateralList != null && result.collateralList.Any())
                {
                    Library.TransferData(result.collateralList, ref lsCollateral);
                }
                model.collateralList = lsCollateral;
                List<InsuranceContractProductViewModel> productList = new List<InsuranceContractProductViewModel>();
                if (result.productList != null && result.productList.Any())
                {
                    Library.TransferData(result.productList, ref productList);
                }
                model.productList = productList;
                List<HDBH.Models.ViewModel.AttachmentViewModel> fileList = new List<AttachmentViewModel>();
                if (result.fileList != null && result.fileList.Any())
                {
                    Library.TransferData(result.fileList, ref fileList);
                }
                ViewBag.fileList = fileList;
                List<ScheduleListViewModel> lsSchedule = new List<ScheduleListViewModel>();
                if (result.scheduleList != null && result.scheduleList.Any())
                {
                    Library.TransferData(result.scheduleList, ref lsSchedule);
                }
                List<ScheduleItemViewModel> lsPaymentDetail = new List<ScheduleItemViewModel>();
                if (result.paymentDetailList != null && result.paymentDetailList.Any())
                {
                    Library.TransferData(result.paymentDetailList, ref lsPaymentDetail);
                }
                foreach (var schedule in lsSchedule)
                {
                    schedule.scheduleDate = schedule.schedulePaymentDate;
                    schedule.scheduleQuanlity = int.Parse(schedule.scheduleId.ToString());
                    schedule.productList = lsPaymentDetail.Where(x => x.scheduleId == schedule.scheduleId).ToList();
                }
                ViewBag.strScheDuleTable = RenderPartialViewToString("~/Areas/InsuranceContract/Views/Shared/_cheduleList.cshtml", lsSchedule);
            }



            ViewBag.TypeCode = model.contractTypeCode;
            ViewBag.FormCode = model.contractFormCode;
            ViewBag.ExceptCode = model.exceptTypeCode;
            model.viewMode = ViewModeCons.VIEW;
            ViewBag.viewMode = ViewModeCons.VIEW;
            return View("Create", model);
        }
        public PartialViewResult _getListContractType(string contractTypeCode, string selected = "", int isNonlifeInsurance = -1)
        {
            var listContractType = _insuranceContractService.getListContractType(contractTypeCode, isNonlifeInsurance);
            List<SelectListItem> lsSelect = listContractType.Select(x => new SelectListItem()
            {
                Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.contractTypeCode) && selected.ToUpper().Trim().Equals(x.contractTypeCode.ToUpper()),
                Text = x.contractTypeName,
                Value = x.contractTypeCode
            }).ToList();
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }

        public JsonResult getListExceptType(string insuranceContractType, string selected = "")
        {
            List<SelectListItem> lsSelect = new List<SelectListItem>();
            var listExceptType = _insuranceContractService.getListExceptType(insuranceContractType);
            if(listExceptType  != null && listExceptType.Any())
            {
                lsSelect = listExceptType.Select(x => new SelectListItem()
                {
                    Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.exceptTypeCode) && selected.ToUpper().Trim().Equals(x.exceptTypeCode.ToUpper()),
                    Text = x.exceptTypeName,
                    Value = x.exceptTypeCode
                }).ToList();
            }
            return Json(RenderPartialViewToString("~/Views/Shared/_renderSelectBox.cshtml", lsSelect), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _getListExceptType(string insuranceContractType, string selected = "")
        {
            List<SelectListItem> lsSelect = new List<SelectListItem>();
            var listExceptType = _insuranceContractService.getListExceptType(insuranceContractType);
            if (listExceptType != null && listExceptType.Any())
            {
               lsSelect = listExceptType.Select(x => new SelectListItem()
                {
                    Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.exceptTypeCode) && selected.ToUpper().Trim().Equals(x.exceptTypeCode.ToUpper()),
                    Text = x.exceptTypeName,
                    Value = x.exceptTypeCode
                }).ToList();
            }
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }
        public JsonResult getListContractForm(string insuranceContractType, int isNonlifeInsurance = -1, string selected = "")
        {
            var listExceptType = _insuranceContractService.getListContractForm(insuranceContractType, isNonlifeInsurance);
            List<SelectListItem> lsSelect = listExceptType.Select(x => new SelectListItem()
            {
                Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.contractFormCode) && selected.ToUpper().Trim().Equals(x.contractFormCode.ToUpper()),
                Text = x.contractFormName,
                Value = x.contractFormCode
            }).ToList();
            return Json(RenderPartialViewToString("~/Views/Shared/_renderSelectBox.cshtml", lsSelect), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _getListContractForm(string insuranceContractType, int isNonlifeInsurance = -1, string selected = "")
        {
            var listExceptType = _insuranceContractService.getListContractForm(insuranceContractType, isNonlifeInsurance);
            List<SelectListItem> lsSelect = listExceptType.Select(x => new SelectListItem()
            {
                Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.contractFormCode) && selected.ToUpper().Trim().Equals(x.contractFormCode.ToUpper()),
                Text = x.contractFormName,
                Value = x.contractFormCode
            }).ToList();
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }
        public ActionResult getListBranch(string selected = "")
        {
            var lstBranch = _branch.getListBranch(null);
            List<SelectListItem> lsSelect = lstBranch.Select(x => new SelectListItem()
            {
                Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.branchCode) && selected.ToUpper().Trim().Equals(x.branchCode.ToUpper()),
                Text = x.branchName,
                Value = x.branchCode
            }).ToList();
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }

        [HttpPost]
        public JsonResult _getListStatus(int inau)
        {
            var result = new List<MDProcessStatus>();
            result = _mdServices.getListStatus(inau);
            if (result == null)
            {
                return Json(0);
            }
            else
                return Json(result);
        }

        public ActionResult _ExportExcel(SearchInsuranceContract input)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var result = new ListInsuranceContractDetailViewModel();
                result = _insuranceContractService.searchInsuranceContract(input);
                var data = result.resultList;
                if (data.Any())
                {
                    wb.Worksheets.Add(MapToTable(data), "Hợp đồng Bảo hiểm");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", $"attachment;filename={DateTime.Now:yyyyMMdd}_InsuranceContractTemplate.xlsx");
                    using (MemoryStream myMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(myMemoryStream);
                        myMemoryStream.WriteTo(Response.OutputStream);
                        Response.End();
                        return Content("Thành công");
                    }
                }
                return Content("Không có dữ liệu");
            }
        }

        private DataTable MapToTable(List<InsuranceContractDetailViewModel> data)
        {
            //  GLinks is the new DataTable I want to construct and bind to a DataList, Repeater, ListView  
            DataTable gLinks = new DataTable();
            DataRow dr = null;
            gLinks.Columns.Add(new DataColumn("Số HĐBH", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Khách hàng", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Giá trị HĐ", typeof(decimal?)));
            gLinks.Columns.Add(new DataColumn("Ngày hiệu lực", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Ngày hết hiệu lực", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Trạng thái", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Đến hạn tái tục", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Người cập nhật", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Ngày cập nhật", typeof(string)));

            foreach (var row in data)
            {
                dr = gLinks.NewRow();
                dr["Số HĐBH"] = row.insuranceContractNo;
                dr["Khách hàng"] = row.customerInfo;
                dr["Giá trị HĐ"] = row.insuranceValue;
                dr["Ngày hiệu lực"] = row.effectiveDate.ToString();
                dr["Ngày hết hiệu lực"] = row.dueDate.ToString();
                dr["Trạng thái"] = row.processStatusName;
                dr["Đến hạn tái tục"] = row.isRenewSchedule == 1 ? "X" : "";
                dr["Người cập nhật"] = row.lastUserUpdated;
                dr["Ngày cập nhật"] = row.lastDatetimeUpdated;
                gLinks.Rows.Add(dr);

            }
            return gLinks;
        }

        public JsonResult getListCpProdForContract(long counterPartyId, string selected = "")
        {
            var resultList = _insuranceContractService.getListCpProdForContract(counterPartyId, null, null, null, RDAuthorize.UserId);
            string strHtmlProduct = string.Empty;
            string strHtmlCommision = string.Empty;
            if (resultList != null)
            {
                if (resultList.productList != null && resultList.productList.Any())
                {
                    List<SelectListItem> lsSelect = new List<SelectListItem>();
                    lsSelect = resultList.productList.Select(x => new SelectListItem()
                    {
                        Selected = x.productId.ToString() == selected,
                        Value = x.productId.ToString(),
                        Text = x.productCode,
                        Group = new SelectListGroup() { Name = JsonConvert.SerializeObject(x) },
                    }).ToList();
                    strHtmlProduct = RenderPartialViewToString("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
                }
            }
            return Json(strHtmlProduct, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _getListCpProdForContract(long counterPartyId, string selected = "")
        {
            var resultList = _insuranceContractService.getListCpProdForContract(counterPartyId, null, null, null, RDAuthorize.UserId);
            string strHtmlProduct = string.Empty;
            string strHtmlCommision = string.Empty;
            List<SelectListItem> lsSelect = new List<SelectListItem>();
            if (resultList != null)
            {
                if (resultList.productList != null && resultList.productList.Any())
                {
                    lsSelect = resultList.productList.Select(x => new SelectListItem()
                    {
                        Selected = x.productId.ToString() == selected,
                        Value = x.productId.ToString(),
                        Text = x.productCode,
                        Group = new SelectListGroup() { Name = JsonConvert.SerializeObject(x) },
                    }).ToList();
                }
            }
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }

        public JsonResult getDetailCollateral(string collateralCode)
        {
            var result = _insuranceContractService.getDetailCollateral(collateralCode, RDAuthorize.UserId);
            List<InsuranceContractCollateralViewModel> lsResult = new List<InsuranceContractCollateralViewModel>();
            InsuranceContractCollateralViewModel item = new InsuranceContractCollateralViewModel();
            int errCode = -1;
            if(lsResult != null && lsResult.Any()){
                Library.TransferData(result, ref item);
                lsResult.Add(item);
                errCode = 0;
            }
            return Json(new { Code = errCode, Data= RenderPartialViewToString("~/Areas/InsuranceContract/Views/Shared/_templateCollateralItem.cshtml", lsResult) });
        }

        [HttpGet]
        public JsonResult calScheduleList(int scheduleQuanlity, int scheduleTerm, string dtpStartDate, string strProduct)
        {
            List<ScheduleItemViewModel> lsProduct = new List<ScheduleItemViewModel>();
            lsProduct = JsonConvert.DeserializeObject<List<ScheduleItemViewModel>>(strProduct);
            List<ScheduleListViewModel> lsResult = new List<ScheduleListViewModel>();
            if (lsProduct != null && lsProduct.Any())
            {
                for (int i = 0; i < scheduleQuanlity; i++)
                {
                    ScheduleListViewModel item = new ScheduleListViewModel();
                    int index = i + 1;
                    item.scheduleQuanlity = index;
                    DateTime dtmDate = dtpStartDate != null ? DateTime.Parse(dtpStartDate.ToString()) : new DateTime();
                    item.scheduleDate = dtmDate.AddMonths((scheduleTerm * i)); // cap so cong cua so thang - thang dau tien la 0 - thang tiep theo la term*i
                    item.productList = lsProduct;
                    item.scheduleId = index;
                    if (item.productList != null && item.productList.Any())
                    {
                        foreach (var product in item.productList)
                        {
                            decimal tempTotal = product.tbfeeAmount;
                            decimal tempVal = product.tbVATPercent;
                            product.schedulePaymentAmount = tempTotal / scheduleQuanlity;
                            product.scheduleVatAmount = (product.schedulePaymentAmount * tempVal) / 100;
                            product.scheduleTotalAmount = product.schedulePaymentAmount + product.scheduleVatAmount;
                            product.scheduleId = index;
                        }
                    }
                    lsResult.Add(item);
                }
            }
            return Json(RenderPartialViewToString("~/Areas/InsuranceContract/Views/Shared/_cheduleList.cshtml", lsResult), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult insertInsurContractCollateral(InsuranceContractCollateralInsertModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            input.jsonCollateralList = input.collateralList != null ? JsonConvert.SerializeObject(input.collateralList) : null;
            input.jsonFileList = input.fileList != null ? JsonConvert.SerializeObject(input.fileList) : null;
            input.jsonPaymentDetailList = input.paymentDetailList != null ? JsonConvert.SerializeObject(input.paymentDetailList) : null;
            input.jsonProductList = input.productList != null ? JsonConvert.SerializeObject(input.productList) : null;
            input.jsonScheduleList = input.scheduleList != null ? JsonConvert.SerializeObject(input.scheduleList) : null;
            var result = _insuranceContractService.insertInsurContractCollateral(input).readResult();
            return Json(result);
        }

        [HttpPost]
        public JsonResult insertInsurContract(InsuranceContractCollateralInsertModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            input.jsonFileList = input.fileList != null ? JsonConvert.SerializeObject(input.fileList) : null;
            input.jsonPaymentDetailList = input.paymentDetailList != null ? JsonConvert.SerializeObject(input.paymentDetailList) : null;
            input.jsonProductList = input.productList != null ? JsonConvert.SerializeObject(input.productList) : null;
            input.jsonScheduleList = input.scheduleList != null ? JsonConvert.SerializeObject(input.scheduleList) : null;
            var result = _insuranceContractService.insertInsurContract(input).readResult();
            return Json(result);
        }

        [HttpPost]
        public JsonResult updateInsurContractCollateral(InsuranceContractCollateralInsertModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            input.jsonCollateralList = input.collateralList != null ? JsonConvert.SerializeObject(input.collateralList) : null;
            input.jsonFileList = input.fileList != null ? JsonConvert.SerializeObject(input.fileList) : null;
            input.jsonPaymentDetailList = input.paymentDetailList != null ? JsonConvert.SerializeObject(input.paymentDetailList) : null;
            input.jsonProductList = input.productList != null ? JsonConvert.SerializeObject(input.productList) : null;
            input.jsonScheduleList = input.scheduleList != null ? JsonConvert.SerializeObject(input.scheduleList) : null;
            input.processStatus = "INAU";
            var result = _insuranceContractService.updateInsurContractCollateral(input).readResult();
            return Json(result);
        }
        [HttpPost]
        public JsonResult updateInsurContract(InsuranceContractCollateralInsertModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            input.jsonFileList = input.fileList != null ? JsonConvert.SerializeObject(input.fileList) : null;
            input.jsonPaymentDetailList = input.paymentDetailList != null ? JsonConvert.SerializeObject(input.paymentDetailList) : null;
            input.jsonProductList = input.productList != null ? JsonConvert.SerializeObject(input.productList) : null;
            input.jsonScheduleList = input.scheduleList != null ? JsonConvert.SerializeObject(input.scheduleList) : null;
            input.processStatus = "INAU";
            var result = _insuranceContractService.updateInsurContract(input).readResult();
            return Json(result);
        }

        [HttpPost]
        public JsonResult updateCloseInsurContract(string insuranceId, string insuranceContractNo)
        {
            var result = _insuranceContractService.updateCloseInsurContract(insuranceId, insuranceContractNo, RDAuthorize.UserId, RDAuthorize.BranchCode).readResult();
            return Json(result);
        }
    }
}