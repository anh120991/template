using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.Payment
{
    public class SearchPaymentModel : SearchPagging
    {
        public int isInau { get; set; }
        public int externalRepayment { get; set; }
        public string insuranceContractNo { get; set; }
        public string ftttNo { get; set; }
        public string userUpdated { get; set; }
        public List<string> _processStatusList { get; set; }
        public string processStatusList { get; set; }
        public string branchCode { get; set; }
        public string userId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

    }

    public class ResultSearchPaymentDetail
    {
        public long repaymentId { get; set; }
        public long scheduleId { get; set; }
        public long insuranceId { get; set; }
        public int isInau { get; set; }
        public int isExternalRepayment { get; set; }
        public string insuranceContractNo { get; set; }
        public string processStatusCode { get; set; }
        public string processStatusName { get; set; }
        public string lastUserUpdated { get; set; }
        public decimal? totalAmount { get; set; }
        public DateTime? paymentDate { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
        public string _lastDatetimeUpdated
        {
            get
            {
                return this.lastDatetimeUpdated != null ? this.lastDatetimeUpdated.Value.ToShortDateString() : "";
            }
        }

        public string _paymentDate
        {
            get
            {
                return this.paymentDate != null ? this.paymentDate.Value.ToShortDateString() : "";
            }
        }
        public string ftttNo { get; set; }
    }

    public class ResultSearchListPayment
    {
        public int TotalRecord { get; set; }
        public ResultSearchListPayment()
        {
            resultList = new List<ResultSearchPaymentDetail>();
        }
        public List<ResultSearchPaymentDetail> resultList { get; set; }
    }
}
