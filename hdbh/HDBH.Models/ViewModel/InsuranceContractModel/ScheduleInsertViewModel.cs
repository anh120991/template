using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class ScheduleInsertViewModel
    {
        public DateTime? schedulePaymentDate { get; set; }
        public long scheduleId { get; set; }
        public decimal paymentAmount { get; set; }
        public DateTime? paymentDate { get; set; }
        public string paymentReceipt { get; set; }
        public int isPaid { get; set; }
        public string PschedulePaymentDate
        {
            get
            {
                return this.schedulePaymentDate != null ? this.schedulePaymentDate.Value.ToShortDateString() : "";
            }
        }
    }
    public class SchedulePaymentDetailItemViewModel
    {
        public long scheduleId { get; set; }
        public long productId { get; set; }
        public decimal schedulePaymentAmount { get; set; }
        public decimal scheduleVatAmount { get; set; }
        public decimal scheduleTotalAmount { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
    }

    public class ScheduleInsertViewModelExtend
    {
        public DateTime? schedulePaymentDate { get; set; }
        public long scheduleId { get; set; }
        public decimal paymentAmount { get; set; }
        public DateTime? paymentDate { get; set; }
        public string paymentReceipt { get; set; }
        public int isPaid { get; set; }
        public string _schedulePaymentDate
        {
            get
            {
                return this.schedulePaymentDate != null ? this.schedulePaymentDate.Value.ToShortDateString() : "";
            }
        }
    }

}