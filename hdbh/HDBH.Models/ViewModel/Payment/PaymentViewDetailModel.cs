using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.Payment
{
    public class PaymentViewDetailModel
    {
        public long repaymentId { get; set; }
        public long scheduleId { get; set; }
        public long insuranceId { get; set; }

        public int isExternalRepayment { get; set; }

        public decimal totalAmount { get; set; }

        public DateTime? paymentDate { get; set; }
        public DateTime? datetimeCreated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
        public DateTime? datetimeApproved { get; set; }

        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public string ftttNo { get; set; }
        public string processStatusCode { get; set; }
        public string processStatusName { get; set; }
        public string repaymentStatus { get; set; }
        public string branchCode { get; set; }
        public string userCreated { get; set; }
        public string lastUserUpdated { get; set; }
        public string contentApproved { get; set; }
        public string userApproved { get; set; }

        public List<ScheduleInsertViewModelExtend> scheduleList { get; set; }
        public List<SchedulePaymentDetailItemViewModel> paymentDetailList { get; set; }
        public List<_file> fileList { get; set; }

        public PaymentViewDetailModel()
        {
            scheduleList = new List<ScheduleInsertViewModelExtend>();
            paymentDetailList = new List<SchedulePaymentDetailItemViewModel>();
            fileList = new List<_file>();
        }

        public string viewMode {get;set;}
    }

}
