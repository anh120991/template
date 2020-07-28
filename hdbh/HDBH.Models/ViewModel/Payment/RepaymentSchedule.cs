using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.Payment
{
    /// <summary>
    /// dutp
    /// 22/7/20
    /// </summary>
    public class SearchRepaymentSchedule : SearchPagging
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string paymentStatus { get; set; }
        public string branchCode { get; set; }
        public string userId { get; set; }
        public List<string> _paymentStatus { get; set; }
    }

    public class ResultSearchRepaymentSchedule
    {
        public long insuranceId { get; set; }
        public long scheduleId { get; set; }
        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public string paymentStatus { get; set; }
        public DateTime? schedulePaymentDate { get; set; }
        public decimal scheduleTotalAmount { get; set; }
        public string _schedulePaymentDate
        {
            get
            {
                return this.schedulePaymentDate != null ? this.schedulePaymentDate.Value.ToShortDateString() : "";
            }
        }

        public string customerInfo
        {
            get
            {
                return this.customerCif != null ? this.customerCif : "" + " " + this.customerName != null ? this.customerName : "";
            }
        }
    }

    public class ResultSearchListRepaymentSchedule
    {
        public ResultSearchListRepaymentSchedule()
        {
            resultList = new List<ResultSearchRepaymentSchedule>();
        }
        public List<ResultSearchRepaymentSchedule> resultList { get; set; }
        public int TotalRecord { get; set; }
    }
}
