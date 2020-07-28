using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CounterPartySelectItemModel
    {
        public long counterPartyId { get; set; }
        public string counterPartyName { get; set; }
        public string cifCounterParty { get; set; }
        public string shortName { get; set; }
        public string paymentAccount { get; set; }
    }
}
