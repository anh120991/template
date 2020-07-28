using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class ScheduleListViewModel
    {
        public ScheduleListViewModel()
        {
            productList = new List<ScheduleItemViewModel>();
        }
        public int scheduleQuanlity { get; set; }
        
        public List<ScheduleItemViewModel> productList { get; set; }
        public DateTime schedulePaymentDate { get; set; }
        public long scheduleId { get; set; }
        public DateTime scheduleDate { get; set; }

    }
    public class ScheduleItemViewModel
    {
        public long scheduleId { get; set; }
        public long productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public decimal? schedulePaymentAmount { get; set; }
        public decimal? scheduleVatAmount { get; set; }
        public decimal? scheduleTotalAmount { get; set; }

        public decimal tbfeeAmount { get; set; }
        public decimal tbVATPercent { get; set; }
    }

}
