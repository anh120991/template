using HDBH.Authorize;
using HDBH.CounterParty.Model;
using HDBH.Lib.Helper;
using HDBH.Models.ViewModel;
using HDBH.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HDBH.CounterParty.Controllers
{
    public class CounterPartyManagerController : BasedController
    {

        private ICounterPartyListService _counterPartyListService;
        private ICounterPartyCifService _counterPartyCifService;
        private ICounterPartyService _counterPartyService;

        public CounterPartyManagerController(ICounterPartyListService counterPartyListService, ICounterPartyCifService counterPartyCifService, ICounterPartyService counterPartyService)
        {
            _counterPartyListService = counterPartyListService;
            _counterPartyCifService = counterPartyCifService;
            _counterPartyService = counterPartyService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new HDBH.CounterParty.Model.CounterPartyViewModel();
            model.ViewModel = "CREATE";
            return View(model);
        }

        [HttpPost]
        public JsonResult Search(DateTime? fromDate, DateTime? toDate, int? isInau, string counterPartyType, string cifCounterParty, string counterPartyName, string bancasRepresentative, string status = null) 
        {
            int pageNumber = 1;
            int totalNumber = 0;
            int pageSize = ConfigHelper.ItemPerPage();
            DateTime dtmFromDate = fromDate != null ? DateTime.Parse(fromDate.ToString()) : DateTime.Now;
            DateTime dtmTodate = toDate != null ? DateTime.Parse(toDate.ToString()) : DateTime.Now;
            var result = _counterPartyListService.searchCounterPartyList(dtmFromDate, dtmTodate, isInau, counterPartyType, cifCounterParty, counterPartyName, bancasRepresentative, status, pageNumber, pageSize, ref totalNumber);
            return Json(new { content = RenderPartialViewToString("~/Areas/CounterParty/Views/CounterPartyManager/_counterPartyList.cshtml", result), totalRow = totalNumber });
        }

        [HttpPost]
        public JsonResult CallBackNonLifeCounterPartyList(DateTime fromDate, DateTime toDate, int isInau, string counterPartyType, string cifCounterParty, string counterPartyName, string bancasRepresentative, int pageNumber)
        {
            pageNumber++;
            int totalNumber = 0;
            int pageSize = ConfigHelper.ItemPerPage();
            DateTime dtmFromDate = fromDate != null ? DateTime.Parse(fromDate.ToString()) : DateTime.Now;
            DateTime dtmTodate = toDate != null ? DateTime.Parse(toDate.ToString()) : DateTime.Now;
            var result = _counterPartyListService.searchCounterPartyList(dtmFromDate, dtmTodate, isInau, counterPartyType, cifCounterParty, counterPartyName, bancasRepresentative, "", pageNumber, pageSize, ref totalNumber);
            return Json(new { content = RenderPartialViewToString("~/Areas/CounterParty/Views/CounterPartyManager/_counterPartyList.cshtml", result), totalRow = totalNumber });

        }

        [HttpPost]
        public JsonResult SearchCounterPartyByCif(string cif)
        {
            var result = _counterPartyCifService.getCounterPartyByCif(cif);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Insert(CounterPartyViewModel input)
        {
            input.userId = RDAuthorize.user;
            var obj1 = new { product = input.productList };
            var obj2 = new { file = input.fileList };

            input.strFileList = JsonConvert.SerializeObject(obj2);
            input.strProductList = JsonConvert.SerializeObject(obj1);

            var result = _counterPartyService.InsertCounterParty(input.cifCounterParty, input.counterPartyName, input.counterPartyType, input.bancasRepresentative1, input.representativePhone1, input.bancasRepresentative2,
                input.representativePhone2, input.signedContractDate, input.paymentAccount, input.description, input.userId, input.strProductList, input.strFileList);
            return Json(result);
        }

        public ActionResult Update(int counterPartyId, string cifCounterParty, int isInau)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;

            var result = _counterPartyService.getCounterPartyDetail(counterPartyId, cifCounterParty, isInau);
            CounterPartyViewModel model = new CounterPartyViewModel();
            List<ProductListModel> lsProduct = new List<ProductListModel>();
            List<AttachmentViewModel> lsFile = new List<AttachmentViewModel>();
            Library.TransferData(result, ref model);
            Library.TransferData(result.productList, ref lsProduct);
            Library.TransferData(result.fileList, ref lsFile);  
            model.productList = lsProduct;
            model.fileList = lsFile;
            model.ViewModel = "EDIT";
            return View(model);
        }


        [HttpPost]
        public JsonResult Update(CounterPartyViewModel input)
        {
            input.userId = RDAuthorize.user;
            var obj1 = new { product = input.productList };
            var obj2 = new { file = input.fileList };

            input.strFileList = JsonConvert.SerializeObject(obj2);
            input.strProductList = JsonConvert.SerializeObject(obj1);

            var r = JsonConvert.SerializeObject(input);

            var result = _counterPartyService.UpdateCounterParty(input.counterPartyId, input.cifCounterParty, input.counterPartyName, input.counterPartyType, input.bancasRepresentative1, input.representativePhone1, input.bancasRepresentative2,
                input.representativePhone2, input.signedContractDate, input.paymentAccount, input.description, input.userId, input.strProductList, input.strFileList);
            return Json(result);
        }

        public ActionResult Inau()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchInau(DateTime? fromDate, DateTime? toDate, string counterPartyType, string cifCounterParty, string counterPartyName, string bancasRepresentative)
        {
            int pageNumber = 1;
            int totalNumber = 0;
            int pageSize = ConfigHelper.ItemPerPage();
            DateTime dtmFromDate = fromDate != null ? DateTime.Parse(fromDate.ToString()) : DateTime.Now;
            DateTime dtmTodate = toDate != null ? DateTime.Parse(toDate.ToString()) : DateTime.Now;
            var result = _counterPartyListService.searchCounterPartyInauList(dtmFromDate, dtmTodate, counterPartyType, cifCounterParty, counterPartyName, bancasRepresentative, pageNumber, pageSize, ref totalNumber);
            return Json(new { content = RenderPartialViewToString("~/Areas/CounterParty/Views/CounterPartyManager/_counterPartyInauList.cshtml", result), totalRow = totalNumber });
        }

        [HttpPost]
        public JsonResult CallBackNonLifeCounterPartyInauList(DateTime fromDate, DateTime toDate, string counterPartyType, string cifCounterParty, string counterPartyName, string bancasRepresentative, int pageNumber)
        {
            pageNumber++;
            int totalNumber = 0;
            int pageSize = ConfigHelper.ItemPerPage();
            DateTime dtmFromDate = fromDate != null ? DateTime.Parse(fromDate.ToString()) : DateTime.Now;
            DateTime dtmTodate = toDate != null ? DateTime.Parse(toDate.ToString()) : DateTime.Now;
            var result = _counterPartyListService.searchCounterPartyInauList(dtmFromDate, dtmTodate, counterPartyType, cifCounterParty, counterPartyName, bancasRepresentative, pageNumber, pageSize, ref totalNumber);
            return Json(new { content = RenderPartialViewToString("~/Areas/CounterParty/Views/CounterPartyManager/_counterPartyInauList.cshtml", result), totalRow = totalNumber });

        }

        public ActionResult Approve(int counterPartyId, string cifCounterParty)
        {
            var result = _counterPartyService.getCounterPartyDetail(counterPartyId, cifCounterParty, 1);
            CounterPartyViewModel model = new CounterPartyViewModel();
            List<ProductListModel> lsProduct = new List<ProductListModel>();
            List<AttachmentViewModel> lsFile = new List<AttachmentViewModel>();
            Library.TransferData(result, ref model);
            Library.TransferData(result.productList, ref lsProduct);
            Library.TransferData(result.fileList, ref lsFile);
            model.productList = lsProduct;
            model.fileList = lsFile;
            model.ViewModel = "APPROVE";
            return View(model);
        }

        [HttpPost]
        public JsonResult Approve(int counterPartyId, string cifCounterParty, string approvedStatus, string contentApproved)
        {
            var result = _counterPartyService.ApproveCounterParty(counterPartyId, cifCounterParty, approvedStatus, contentApproved, RDAuthorize.user);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Remove(int counterPartyId, string cifCounterParty)
        {
            var result = _counterPartyService.RemoveCounterParty(counterPartyId, cifCounterParty, RDAuthorize.user);
            return Json(result);
        }

        public ActionResult Detail(int counterPartyId, string cifCounterParty, int isInau)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;

            var result = _counterPartyService.getCounterPartyDetail(counterPartyId, cifCounterParty, isInau);
            CounterPartyViewModel model = new CounterPartyViewModel();
            List<ProductListModel> lsProduct = new List<ProductListModel>();
            List<AttachmentViewModel> lsFile = new List<AttachmentViewModel>();
            Library.TransferData(result, ref model);
            Library.TransferData(result.productList, ref lsProduct);
            Library.TransferData(result.fileList, ref lsFile);
            model.productList = lsProduct;
            model.fileList = lsFile;
            model.ViewModel = "VIEW";
            return View("Update", model);
        }
    }
}