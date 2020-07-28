using ClosedXML.Excel;
using HDBH.Authorize;
using HDBH.Language;
using HDBH.Log.Enums;
using HDBH.Models.Constant;
using HDBH.Models.DatabaseModel;
using HDBH.Models.Extensions;
using HDBH.Models.ViewModel;
using HDBH.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.CounterParty.Controllers
{
    public class ManagerController : BasedController
    {
        ICounterPartyService _counterPartyService;
        public ManagerController(ICounterPartyService counterPartyService)
        {
            _counterPartyService = counterPartyService;
        }
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            CounterPartyDetailModel model = new CounterPartyDetailModel();
            ViewBag.IsCreate = true;
            return View(model);
        }
        public ActionResult Update(long counterPartyId = 0, string cifCounterParty = "")
        {
            CounterPartyGetDetailModel input = new CounterPartyGetDetailModel();
            input.counterPartyId = counterPartyId;
            input.cifCounterParty = cifCounterParty;
            input.userId = RDAuthorize.UserId;
            var result = _counterPartyService.getDetailCounterParty(input);
            result.viewMode = ViewModeCons.UPDATE;
            return View("Create", result);
        }
        public ActionResult ViewDetail(long counterPartyId = 0, string cifCounterParty = "")
        {
            CounterPartyGetDetailModel input = new CounterPartyGetDetailModel();
            input.counterPartyId = counterPartyId;
            input.cifCounterParty = cifCounterParty;
            input.userId = RDAuthorize.UserId;
            var result = _counterPartyService.getDetailCounterParty(input);
            result.viewMode = ViewModeCons.VIEW;
            return View("Create", result);
        }

        [HttpPost]
        public JsonResult Insert(CounterPartyDetailInsertModel data)
        {
            data.counterPartyStatus = data.counterPartyStatus == "1" ? CounterPartyStatusConst.A : CounterPartyStatusConst.CLOSED;
            data.userId = RDAuthorize.UserId;
            data.branchCode = RDAuthorize.BranchCode;

            var result = _counterPartyService.insertCounterParty(data).readResult();
            return Json(result);
        }

        [HttpPost]
        public JsonResult Update(CounterPartyDetailInsertModel data)
        {
            data.counterPartyStatus = data.counterPartyStatus == "1" ? CounterPartyStatusConst.A : CounterPartyStatusConst.CLOSED;
            data.userId = RDAuthorize.UserId;
            data.branchCode = RDAuthorize.BranchCode;

            var result = _counterPartyService.updateCounterParty(data).readResult();
            return Json(result);
        }


        [HttpGet]
        public JsonResult loadForm(string counterPartyGroup)
        {
            CounterPartyDetailModel model = new CounterPartyDetailModel();
            model.counterPartyGroup = counterPartyGroup;
            return Json(RenderPartialViewToString("~/Areas/CounterParty/Views/Manager/_detailForm.cshtml", model), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(CounterPartyListSeachModel model)
        {
            var result = new CounterPartySearchModel();
            result = _counterPartyService.searchCounterParty(model);
            return Json(new
            {
                recordsTotal = result.totalRecord,
                recordsFiltered = result.totalRecord,
                data = result.resultList
            });
        }

        #region Export Excel
        public ActionResult ExportCounterParty(CounterPartyListSeachModel input)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                //input.fromDate = DateTime.ParseExact(input.fromDate.Value.ToString("MM/dd/yyyy"), "dd/MM/yyyy", null);
                //input.toDate = DateTime.ParseExact(input.toDate.Value.ToString("MM/dd/yyyy"), "dd/MM/yyyy", null);
                input.length = 0;
                var data = _counterPartyService.searchCounterParty(input);
                if (data.resultList.Any())
                {
                    wb.Worksheets.Add(MapModelToNewTableCounterParty(data.resultList), "CounterPartyTemplate");

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

        private DataTable MapModelToNewTableCounterParty(List<CounterPartySearchItemModel> table)
        {
            //  GLinks is the new DataTable I want to construct and bind to a DataList, Repeater, ListView  
            DataTable gLinks = new DataTable();
            DataRow dr = null;
            //gLinks.Columns.Add(new DataColumn("Mã KH", typeof(int)));
            gLinks.Columns.Add(new DataColumn("Số Cif", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Tên đối tác", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Tài khoản chuyên thu", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Loại đối tác", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Trạng thái", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Người cập nhật", typeof(string)));
            gLinks.Columns.Add(new DataColumn("Ngày cập nhật", typeof(string)));
            var tblnew = table;
            foreach (var row in tblnew)
            {
                dr = gLinks.NewRow();
                //dr["Mã KH"] = row.counterPartyId;
                dr["Số Cif"] = row.cifCounterParty;
                dr["Tên đối tác"] = row.counterPartyName;
                dr["Tài khoản chuyên thu"] = row.paymentAccount;
                dr["Loại đối tác"] = row.counterPartyGroupName;
                dr["Trạng thái"] = row.counterPartyStatusName;
                dr["Người cập nhật"] = row.lastUserUpdated;
                dr["Ngày cập nhật"] = row.lastDatetimeUpdated.ToShortPattern("dd/MM/yyyy");
                gLinks.Rows.Add(dr);
            }

            return gLinks;
        }
        #endregion

        public PartialViewResult _loadCounterPartyGroup(string selected = "")
        {
            var lsCounterPartyGroup = _counterPartyService.getListCounterPartyGroup();
            List<SelectListItem> lsSelect = new List<SelectListItem>();
            if (lsCounterPartyGroup != null && lsCounterPartyGroup.Any())
            {
                lsSelect = lsCounterPartyGroup.Select(x => new SelectListItem()
                {
                    Selected = !string.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(x.cpGroupCode) && selected.ToUpper().Trim().Equals(x.cpGroupCode.ToUpper()),
                    Text = x.cpGroupName,
                    Value = x.cpGroupCode
                }).ToList();
            }
            return PartialView("~/Views/Shared/_renderSelectBox.cshtml", lsSelect);
        }

        public PartialViewResult _getListCounterPartyComboBox(string cpGroupCode , int selected = 0)
        {
            var lsCounterPartyGroup = _counterPartyService.getListCounterParty(cpGroupCode, RDAuthorize.UserId);
            ViewBag.Selected = selected;
            return PartialView("~/Areas/CounterParty/Views/Shared/_getListCounterPartyComboBox.cshtml", lsCounterPartyGroup);
        }
        public JsonResult getListCounterPartyComboBox(string cpGroupCode)
        {
            var lsCounterPartyGroup = _counterPartyService.getListCounterParty(cpGroupCode, RDAuthorize.UserId);
            string strHtml = RenderPartialViewToString("~/Areas/CounterParty/Views/Shared/_getListCounterPartyComboBox.cshtml", lsCounterPartyGroup);
            return Json(strHtml);
        }
    }
}