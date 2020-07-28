using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.CustomUI;
using HDBH.Authorize;
using HDBH.Lib;
using HDBH.Models.DatabaseModel.Payment;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
using HDBH.Models.ViewModel.Payment;
using HDBH.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataTable = System.Data.DataTable;

namespace HDBH.Payment.Controllers
{
    public class ManagerController : BasedController
    {
        private IInsuranceContractService _insuranceContractService;
        private IBranchService _branch;
        private IMDService _mdServices;
        private IPaymentService _paymentservice;

        public ManagerController(IInsuranceContractService insuranceContractService, IBranchService branch, IMDService mdServices, IPaymentService paymentservice)
        {
            _insuranceContractService = insuranceContractService;
            _branch = branch;
            _mdServices = mdServices;
            _paymentservice = paymentservice;
        }

        // GET: ManagerController
        public ActionResult Index()
        {
            return View();
        }

        #region 6.1	Màn hình Quản lý danh sách phiếu thu

        /// <summary>
        /// Hàm Search Payment
        /// Author dutp
        /// Date 21/7
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SearchList(SearchPaymentModel input)
        {
            var result = new ResultSearchListPayment();
            input.userId = RDAuthorize.UserId;
            if (input._processStatusList != null)
                input.processStatusList = JsonConvert.SerializeObject(input._processStatusList);
            else
                input.processStatusList = "";
            result = _paymentservice.searchRepayment(input);
            return Json(new
            {
                data = result.resultList,
                recordsTotal = result.TotalRecord,
                recordsFiltered = result.TotalRecord,
            });
        }
        #endregion

        #region 6.2	Thêm mới thông tin thu phí BH
        [HttpGet]
        public ActionResult CreateRepayment(InputGetDetailPaymentInsert input)
        {
            #region Code rule
            ///Nếu code == 0
            ///=> Thêm mới phiếu thu hoàn toàn
            ///Nếu code == 1
            ///=> Thêm mới phiếu thu từ hợp đồng đã có
            #endregion
            InsertModelView model = new InsertModelView();
            GetListScheduleUnpaid result = new GetListScheduleUnpaid();
            if (input.code == 1)
            {
                input.userId = RDAuthorize.UserId;
                result = _paymentservice.getListScheduleUnpaid(input);
                if (result != null)
                {
                    Library.TransferData(result, ref model);
                }
            }
            List<AttachmentViewModel> fileList = new List<AttachmentViewModel>();
            ViewBag.fileList = fileList;

            List<SchedulePaymentDetailItemViewModel> paymentList = new List<SchedulePaymentDetailItemViewModel>();
            ViewBag.lstPayment = paymentList;


            model.viewMode = "CREATE";
            return View(model);
        }


        public JsonResult _LoadDataPaymentSchedule(List<SchedulePaymentDetailItemViewModel> lstPaymentObj)
        {
            var str = String.Empty;
            if (lstPaymentObj != null)
                str = RenderPartialViewToString("_schedulePaymentInfo", lstPaymentObj);
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(InputInsertUpdatePayment input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            input.stringJsonfileList = input.fileList != null ? JsonConvert.SerializeObject(input.fileList) : null;
            var result = _paymentservice.insertRepayment(input);
            return Json(result);
        }
        #endregion

        #region 6.3	Cập nhật thông tin thu phí BH 
        [HttpGet]
        public ActionResult UpdateRepayment(InputGetDetailPaymentApprove input)
        {
            input.userId = RDAuthorize.UserId;
            var result = _paymentservice.getDetailRepayment(input);
            PaymentViewDetailModel model = new PaymentViewDetailModel();
            var cus = new cusInfo_Payment();
            model.viewMode = input.mode;

            if (result != null)
            {
                Library.TransferData(result, ref model);

                List<ScheduleInsertViewModelExtend> lstSchedule = new List<ScheduleInsertViewModelExtend>();
                if (result.scheduleList != null && result.scheduleList.Any())
                {
                    Library.TransferData(result.scheduleList, ref lstSchedule);
                }
                model.scheduleList = lstSchedule;
                ViewBag.lstSchedule = lstSchedule;

                List<SchedulePaymentDetailItemViewModel> lstpaymentDetail = new List<SchedulePaymentDetailItemViewModel>();
                if (result.paymentDetailList != null && result.paymentDetailList.Any())
                {
                    Library.TransferData(result.paymentDetailList, ref lstpaymentDetail);
                }
                model.paymentDetailList = lstpaymentDetail;
                var sum = new PaymentDetailSum();
                sum.sumSchedulePaymentAmount = 0;
                sum.sumScheduleTotalAmount = 0;
                sum.sumScheduleVatAmount = 0;
                foreach (var item in lstpaymentDetail)
                {
                    sum.sumSchedulePaymentAmount += item.schedulePaymentAmount;
                    sum.sumScheduleTotalAmount += item.scheduleTotalAmount;
                    sum.sumScheduleVatAmount += item.scheduleVatAmount;
                }
                ViewBag.SchedulePaymentSum = sum;

                List<AttachmentViewModel> fileList = new List<AttachmentViewModel>();
                if (result.fileList != null && result.fileList.Any())
                {
                    Library.TransferData(result.fileList, ref fileList);
                }
                //model.fileList = fileList;


                ViewBag.fileList = fileList;


                //Field data to customer info
                cus.insuranceId = result.insuranceId;
                cus.customerName = result.customerName;
                cus.customerCif = result.customerCif;
                cus.insuranceContractNo = result.insuranceContractNo;
                ViewBag.CusInfo = cus;
            }

            ViewBag.viewMode = input.mode;

            return View(model);
        }

        [HttpPost]
        public JsonResult Update(InputInsertUpdatePayment input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            input.stringJsonfileList = input.fileList != null ? JsonConvert.SerializeObject(input.fileList) : null;
            var result = _paymentservice.updateRepayment(input);
            return Json(result);
        }
        #endregion

        #region 6.4	Duyệt thông tin thu phí BH
        [HttpGet]
        public ActionResult ApproveRepayment(InputGetDetailPaymentApprove input)
        {
            input.userId = RDAuthorize.UserId;
            var result = _paymentservice.getDetailRepayment(input);
            PaymentViewDetailModel model = new PaymentViewDetailModel();
            var cus = new cusInfo_Payment();
            model.viewMode = input.mode;

            if (result != null)
            {
                Library.TransferData(result, ref model);

                List<ScheduleInsertViewModelExtend> lstSchedule = new List<ScheduleInsertViewModelExtend>();
                if (result.scheduleList != null && result.scheduleList.Any())
                {
                    Library.TransferData(result.scheduleList, ref lstSchedule);
                }
                model.scheduleList = lstSchedule;
                ViewBag.lstSchedule = lstSchedule;

                List<SchedulePaymentDetailItemViewModel> lstpaymentDetail = new List<SchedulePaymentDetailItemViewModel>();
                if (result.paymentDetailList != null && result.paymentDetailList.Any())
                {
                    Library.TransferData(result.paymentDetailList, ref lstpaymentDetail);
                }
                model.paymentDetailList = lstpaymentDetail;
                var sum = new PaymentDetailSum();
                sum.sumSchedulePaymentAmount = 0;
                sum.sumScheduleTotalAmount = 0;
                sum.sumScheduleVatAmount = 0;
                foreach (var item in lstpaymentDetail)
                {
                    sum.sumSchedulePaymentAmount += item.schedulePaymentAmount;
                    sum.sumScheduleTotalAmount += item.scheduleTotalAmount;
                    sum.sumScheduleVatAmount += item.scheduleVatAmount;
                }
                ViewBag.SchedulePaymentSum = sum;

                List<_file> lstFile = new List<_file>();
                if (result.fileList != null && result.fileList.Any())
                {
                    Library.TransferData(result.fileList, ref lstFile);
                }
                model.fileList = lstFile;

                //Field data to customer info
                cus.insuranceId = result.insuranceId;
                cus.customerName = result.customerName;
                cus.customerCif = result.customerCif;
                cus.insuranceContractNo = result.insuranceContractNo;
                ViewBag.CusInfo = cus;
            }

            ViewBag.viewMode = input.mode;

            return View(model);
        }

        [HttpPost]
        public JsonResult Approve(ApprovePaymentModel input)
        {
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            var result = new ResultModel();
            result = _paymentservice.approveRepayment(input);
            return Json(result);
        }
        #endregion

        #region 6.6	Màn hình Quản lý danh sách phiếu thu chờ duyệt
        /// dutp
        /// Date 21/7

        public ActionResult RepaymentInau()
        {
            return View();
        }

        [HttpPost]
        public JsonResult searchRepaymentInau(SearchPaymentModel input)
        {
            var result = new ResultSearchListPayment();
            input.userId = RDAuthorize.UserId;
            if (input._processStatusList != null)
                input.processStatusList = JsonConvert.SerializeObject(input._processStatusList);
            else
                input.processStatusList = "";
            result = _paymentservice.searchRepaymentInau(input);
            return Json(new
            {
                data = result.resultList,
                recordsTotal = result.TotalRecord,
                recordsFiltered = result.TotalRecord,
            });
        }
        #endregion


        #region 6.7	Màn hình Theo dõi tình hình thanh toán phí
        ///dutp
        ///22/7/20

        public ActionResult PaymentScheduleManagement()
        {
            ViewBag.lstBranch = _branch.getListBranch(null);
            return View();
        }

        [HttpPost]
        public JsonResult searchRepaymentSchedule(SearchRepaymentSchedule input)
        {
            var result = new ResultSearchListRepaymentSchedule();
            input.userId = RDAuthorize.UserId;
            input.branchCode = RDAuthorize.BranchCode;
            if (input._paymentStatus != null)
                input.paymentStatus = JsonConvert.SerializeObject(input._paymentStatus);
            else
                input.paymentStatus = "";

            result = _paymentservice.searchRepaymentSchedule(input);

            return Json(new
            {
                data = result.resultList,
                recordsTotal = result.TotalRecord,
                recordsFiltered = result.TotalRecord,
            });
        }
        #endregion

        #region Xuất Excel

        ///Xuất Excel
        ///dutp
        ///24/7

        public DataTable MapModelToTable(List<ResultSearchPaymentDetail> data)
        {
            DataTable gLinks = new System.Data.DataTable();
            DataRow dr = null;
            gLinks.Columns.Add(new DataColumn("SỐ FT-TT", typeof(string)));
            gLinks.Columns.Add(new DataColumn("SỐ HĐBH", typeof(string)));
            gLinks.Columns.Add(new DataColumn("KỲ SỐ (VNĐ)", typeof(int)));

            gLinks.Columns.Add(new DataColumn("TỔNG TIỀN", typeof(decimal)));
            gLinks.Columns.Add(new DataColumn("THU PHÍ NGOÀI", typeof(string)));
            gLinks.Columns.Add(new DataColumn("NGÀY ĐÓNG PHÍ", typeof(string)));

            gLinks.Columns.Add(new DataColumn("TRẠNG THÁI", typeof(string)));
            gLinks.Columns.Add(new DataColumn("NGƯỜI CẬP NHẬT", typeof(string)));
            gLinks.Columns.Add(new DataColumn("NGÀY CẬP NHẬT", typeof(string)));

            foreach (var row in data)
            {
                dr = gLinks.NewRow();
                dr["SỐ FT-TT"] = row.ftttNo;
                dr["SỐ HĐBH"] = row.insuranceContractNo;
                dr["KỲ SỐ (VNĐ)"] = row.repaymentId;
                dr["TỔNG TIỀN"] = row.totalAmount;
                dr["THU PHÍ NGOÀI"] = row.isExternalRepayment == 1 ? "X" : "";
                dr["NGÀY ĐÓNG PHÍ"] = row._paymentDate;
                dr["TRẠNG THÁI"] = row.processStatusName;
                dr["NGƯỜI CẬP NHẬT"] = row.lastUserUpdated;
                dr["NGÀY CẬP NHẬT"] = row._lastDatetimeUpdated;

                gLinks.Rows.Add(dr);
            }

            return gLinks;
        }

        [HttpPost]
        public ActionResult ExportRepaymentManagement(SearchPaymentModel input)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var result = new ResultSearchListPayment();
                result = _paymentservice.searchRepayment(input);

                if (result.resultList.Any())
                {
                    wb.Worksheets.Add(MapModelToTable(result.resultList), "QUẢN LÝ THANH TOÁN");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", $"attachment;filename={DateTime.Now:yyyyMMdd}_RepaymentManagement.xlsx");
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
        #endregion
    }
}