using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class InsuranceContractViewModel
    {
        public InsuranceContractViewModel()
        {
            productList = new List<InsuranceContractProductViewModel>();
            collateralList = new List<InsuranceContractCollateralViewModel>();
            fileList = new List<CPProductDetailtAttachmenViewItemModel>();
        }
        public long insuranceId { get; set; }
        public string contractTypeCode { get; set; }
        public string insuranceContractNo { get; set; }
        public string contractFormCode { get; set; }
        public string insuranceCertification { get; set; }
        public string refInsuranceNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public string shortName { get; set; }
        public decimal insuranceValue { get; set; }
        public DateTime? signedDate { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? dueDate { get; set; }
        public string consultantOfficerCode { get; set; }
        public string consultantOfficerName { get; set; }
        public string consultantBranchCode { get; set; }
        public string exceptTypeCode { get; set; }
        public string noncifCustomerName { get; set; }
        public bool isContractOcb { get; set; }
        public string contractDescription { get; set; }
        public List<InsuranceContractProductViewModel> productList { get; set; }
        public List<InsuranceContractCollateralViewModel> collateralList { get; set; }
        public List<CPProductDetailtAttachmenViewItemModel> fileList { get; set; }
        public string viewMode { get; set; }
        public string exceptionTypeCode { get; set; }
    }
}
