using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.Payment
{
    public class InputInsertUpdatePayment
    {
        public long scheduleId { get; set; }
        public long insuranceId { get; set; }
        public int isExternalRepayment { get; set; }
        public string ftttNo { get; set; }
        public decimal totalAmount { get; set; }
        public DateTime? paymentDate { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public InputInsertUpdatePayment()
        {
            fileList = new List<fileInsert>();
        }
        public List<fileInsert> fileList { get; set; }
        public string stringJsonfileList { get; set; }
        public long repaymentId { get; set; }
    }

    public class fileInsert
    {
        public string fileName { get; set; }
        public string fileDescription { get; set; }
        public string filePath { get; set; }
        public string cdnPath { get; set; }
    }

    public class InputGetDetailPaymentInsert //model input get insert update
    {
        public string insuranceContractNo { get; set; }
        public string userId { get; set; }
        public int code
        {
            get
            {
                return this.insuranceContractNo != null ? 1 : 0;
            }
        }
        public string mode { get; set; }
        public long scheduleId { get; set; }
    }

    public class InsertModelView
    {
        public long insuranceId { get; set; }
        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }

        public List<ScheduleInsertViewModelExtend> scheduleList { get; set; }
        public List<SchedulePaymentDetailItemViewModel> paymentDetailList { get; set; }

        public InsertModelView()
        {
            scheduleList = new List<ScheduleInsertViewModelExtend>();
            paymentDetailList = new List<SchedulePaymentDetailItemViewModel>();
        }
        public string viewMode { get; set; }
    }
}
