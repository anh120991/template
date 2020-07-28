using HDBH.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class InsuranceContractDetailViewModel
    {
        public long insuranceId { get; set; }
        public string contractTypeCode { get; set; }
        public string contractTypeName { get; set; }
        public string contractFormCode { get; set; }
        public string contractFormName { get; set; }
        public string exceptionTypeCode { get; set; }
        public string processStatusCode { get; set; }
        public string processStatusName { get; set; }
        public string lastUserUpdated { get; set; }
        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public decimal insuranceValue { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? dueDate { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
        public int isInau { get; set; }
        public int isRenewSchedule { get; set; }
        public string customerInfo
        {
            get
            {
                return customerCif != null ? customerCif : "" + " - " + customerName != null ? customerName : "";
            }
        }
        public string _effectiveDate
        {
            get
            {
                return this.effectiveDate != null ? this.effectiveDate.Value.ToShortDateString() : "";
            }
        }

        public string _dueDate
        {
            get
            {
                return this.dueDate != null ? this.dueDate.Value.ToShortDateString() : "";
            }
        }

        public string _lastDatetimeUpdated
        {
            get
            {
                return this.lastDatetimeUpdated != null ? this.lastDatetimeUpdated.Value.ToShortDateString() : "";
            }
        }


    }

    public class ListInsuranceContractDetailViewModel
    {
        public ListInsuranceContractDetailViewModel()
        {
            resultList = new List<InsuranceContractDetailViewModel>();
        }
        public List<InsuranceContractDetailViewModel> resultList { get; set; }
        public int TotalRecord { get; set; }
    }
}
