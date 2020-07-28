using HDBH.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CounterPartyDetailModel
    {
        public long counterPartyId { get; set; }
        public string counterPartyName { get; set; }
        public string cifCounterParty { get; set; }
        public string counterPartyGroup { get; set; }
        public string counterPartyGroupName { get; set; }
        public string shortName { get; set; }
        public DateTime? signedContractDate { get; set; }
        public string paymentAccount { get; set; }
        public string counterPartyDesc { get; set; }
        public int? version { get; set; }
        public string counterPartyStatus { get; set; }
        public string branchCode { get; set; }
        public string userCreated { get; set; }
        public DateTime? datetimeCreated { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
        public string contentApproved { get; set; }
        public string userApproved { get; set; }
        public string datetimeApproved { get; set; }
        private string _viewMode = ViewModeCons.CREATE;
        public string viewMode { get => _viewMode; set => _viewMode = value; }

    }
}
