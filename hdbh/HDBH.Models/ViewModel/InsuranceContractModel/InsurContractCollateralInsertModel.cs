using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class InsuranceContractCollateralInsertModel
    {
        public InsuranceContractCollateralInsertModel()
        {
            productList = new List<InsuranceContractProductViewModel>();
            collateralList = new List<InsuranceContractCollateralViewModel>();
            fileList = new List<CPProductDetailtAttachmenViewItemModel>();
            scheduleList = new List<ScheduleInsertViewModel>();
            paymentDetailList = new List<SchedulePaymentDetailItemViewModel>();
        }
        public long insuranceId { get; set; }
        public string insuranceContractNo { get; set; }
        public string contractTypeCode { get; set; }
        public string contractFormCode { get; set; }
        public string insuranceCertification { get; set; }
        public string refInsuranceNo { get; set; }
        public string exceptionTypeCode { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public string noncifCustomerName { get; set; }
        public string contactNumber { get; set; }
        public string consultantOfficerCode { get; set; }
        public string consultantOfficerName { get; set; }
        public string consultantBranchCode { get; set; }
        public int isContractOcb { get; set; }
        public decimal insuranceValue { get; set; }
        public decimal totalFeeAmount { get; set; }
        public DateTime signedDate { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime dueDate { get; set; }
        public string contractDescription { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public List<InsuranceContractProductViewModel> productList { get; set; }
        public List<InsuranceContractCollateralViewModel> collateralList { get; set; }
        public List<CPProductDetailtAttachmenViewItemModel> fileList { get; set; }
        public List<ScheduleInsertViewModel> scheduleList { get; set; }
        public List<SchedulePaymentDetailItemViewModel> paymentDetailList { get; set; }

        public string jsonProductList { get; set; }
        public string jsonCollateralList { get; set; }
        public string jsonFileList { get; set; }
        public string jsonScheduleList { get; set; }
        public string jsonPaymentDetailList { get; set; }

        public string processStatus { get; set; }
    }
}
