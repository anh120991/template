using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CounterPartySearchModel
    {
        public CounterPartySearchModel()
        {
            resultList = new List<CounterPartySearchItemModel>();
        }
        public List<CounterPartySearchItemModel> resultList { get; set; }
        public int totalRecord { get; set; }
        public int totalPage { get; set; }
    }
    public class CounterPartySearchItemModel
    {
        public long counterPartyId { get; set; }
        public string counterPartyName { get; set; }
        public string cifCounterParty { get; set; }
        public string shortName { get; set; }
        public string counterPartyGroup { get; set; }
        public string counterPartyGroupName { get; set; }
        public string paymentAccount { get; set; }
        public string counterPartyStatus { get; set; }
        public string counterPartyStatusName { get; set; }
        public string branchCode { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }

    }
}
