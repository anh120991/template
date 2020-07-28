using HDBH.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.InsuranceContractModel
{
    public class InputApproveInsuranceContractModel
    {
        public string mode { get; set; }
        public long insuranceId { get; set; }
        public string userId { get; set; }
        public string insuranceContractNo { get; set; }
        public int pApproveLevel { get; set; }
    }

    public class GetDetailInsuranceContractModel
    {
        public long insuranceId { get; set; }
        public string insuranceContractNo { get; set; }
        public string contractTypeCode { get; set; }
        public string contractTypeName { get; set; }
        public string contractFormCode { get; set; }
        public string contractFormName { get; set; }
        public string insuranceCertification { get; set; }
        public string refInsuranceNo { get; set; }
        public string exceptionTypeCode { get; set; }
        public string exceptionTypeName { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public string noncifCustomerName { get; set; }
        public string contactNumber { get; set; }
        public string consultantOfficerCode { get; set; }
        public string consultantOfficerName { get; set; }
        public string consultantBranchCode { get; set; }
        public string consultantBranchName { get; set; }
        public int isContractOcb { get; set; }
        public decimal insuranceValue { get; set; }
        public decimal totalFeeAmount { get; set; }
        public DateTime? signedDate { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? dueDate { get; set; }
        public string contractDescription { get; set; }
        public string processStatusCode { get; set; }
        public string processStatusName { get; set; }
        public string contractStatus { get; set; }
        public int isCompletedPayment { get; set; }
        public string branchCode { get; set; }
        public string userCreated { get; set; }
        public DateTime? datetimeCreated { get; set; }
        public string contentApproved1 { get; set; }
        public string userApproved1 { get; set; }
        public DateTime? datetimeApproved1 { get; set; }
        public string contentApproved2 { get; set; }
        public string userApproved2 { get; set; }
        public DateTime? datetimeApproved2 { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }

        //collateralList 
        public List<CollateralDetailModel> collateralList { get; set; }

        //productList
        public List<ProductDetailModel> productList { get; set; }

        //scheduleList 
        public List<ScheduleDetailModel> scheduleList { get; set; }


        //paymentDetailList 
        public List<PaymentDetailModel> paymentDetailList { get; set; }

        //fileList
        public List<fileDetailModel> fileList { get; set; }

    }

    public class CollateralDetailModel
    {
        public long insCollId { get; set; }
        public string collateralCode { get; set; }
        public string collateralTypeCode { get; set; }
        public Decimal? collateralValue { get; set; }
        public Decimal? executionValue { get; set; }
        public string manBranchCode { get; set; }
    }

    public class ProductDetailModel
    {
        public long insProdId { get; set; }
        public long counterPartyId { get; set; }
        public long productId { get; set; }
        public long commisionId { get; set; }
        public string cifCounterParty { get; set; }
        public string paymentAccount { get; set; }
        public string subCompanyName { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public decimal? commisRate { get; set; }
        public decimal? insurancePercent { get; set; }
        public decimal? feeAmount { get; set; }
        public decimal? feeVatAmount { get; set; }
    }

    public class ScheduleDetailModel
    {
        public long scheduleId { get; set; }
        public DateTime? schedulePaymentDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int isPaid { get; set; }
        public string paymentReceipt { get; set; }
        public decimal? paymentAmount { get; set; }
    }

    public class PaymentDetailModel
    {
        public long scheduleId { get; set; }
        public long productId { get; set; }
        public decimal? schedulePaymentAmount { get; set; }
        public decimal? scheduleVatAmount { get; set; }
        public decimal? scheduleTotalAmount { get; set; }
    }

    public class fileDetailModel
    {
        public long scheduleId { get; set; }
        public string fileName { get; set; }
        public string fileDescription { get; set; }
        public string filePath { get; set; }
        public string cdnPath { get; set; }
    }

    public class InputApprove
    {
        public long insuranceId { get; set; }
        public string approveContent { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public string approveStatus { get; set; }
        public int pApproveLevel { get; set; }
    }

}
