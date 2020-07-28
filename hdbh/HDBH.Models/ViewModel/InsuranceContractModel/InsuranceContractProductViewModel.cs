using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class InsuranceContractProductViewModel
    {
        public InsuranceContractProductViewModel()
        {
            isShowAdd = true;
            isShowRemove = true;
        }
        public long counterPartyId { get; set; }
        public string cifCounterParty { get; set; }
        public string paymentAccount { get; set; }
        public string subCompanyName { get; set; }
        public long productId { get; set; }
        public long commisionId { get; set; }
        public decimal commisRate { get; set; }
        public int insurancePercent { get; set; }
        public decimal feeAmount { get; set; }
        public decimal feeVatAmount { get; set; }
        public bool isShowAdd { get; set; }
        public bool isShowRemove { get; set; }
        public long insProdId { get; set; }

    }
}
