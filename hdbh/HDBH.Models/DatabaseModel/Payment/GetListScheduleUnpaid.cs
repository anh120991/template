using HDBH.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel.Payment
{
    public class GetListScheduleUnpaid
    {
        public long insuranceId { get; set; }
        public string insuranceContractNo { get; set; }
        public string customerCif { get; set; }
        public string customerName { get; set; }
        public GetListScheduleUnpaid()
        {
            scheduleList = new List<ScheduleInsertViewModelExtend>();
            paymentDetailList = new List<SchedulePaymentDetailItemViewModel>();
        }
        public List<ScheduleInsertViewModelExtend> scheduleList { get; set; }
        public List<SchedulePaymentDetailItemViewModel> paymentDetailList { get; set; }
        //public string viewMode { get; set; }
    }
}
