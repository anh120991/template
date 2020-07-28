using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.Payment
{
    public class InputGetDetailPaymentApprove
    {
        public long repaymentId { get; set; }
        public string userId { get; set; }
        public string mode { get; set; }
    }

    public class GetPaymentDetailModel
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


        public List<ScheduleInsertViewModel> scheduleList { get; set; }
        public List<SchedulePaymentDetailItemViewModel> paymentDetailList { get; set; }
        public List<_file> fileList { get; set; }

        public GetPaymentDetailModel()
        {
            scheduleList = new List<ScheduleInsertViewModel>();
            paymentDetailList = new List<SchedulePaymentDetailItemViewModel>();
            fileList = new List<_file>();
        }
    }

    public class _file
    {
        public long fileId { get; set; }
        public long fileAutoId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string extention { get; set; }
        public bool isRemove { get; set; }
        public string downloadUrl { get; set; }
    }

    public class cusInfo_Payment
    {
        public long insuranceId { get; set; }
        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
    }

    public class PaymentDetailSum
    {
        public decimal? sumSchedulePaymentAmount { get; set; }
        public decimal? sumScheduleVatAmount { get; set; }
        public decimal? sumScheduleTotalAmount { get; set; }
    }
}
