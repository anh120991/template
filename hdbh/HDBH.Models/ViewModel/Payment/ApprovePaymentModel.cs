using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.Payment
{
    public class ApprovePaymentModel
    {
        public long repaymentId { get; set; }
        public string approveStatus { get; set; }
        public string approveContent { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
    }
}
