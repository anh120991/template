using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CounterPartyGetDetailModel
    {
        public long counterPartyId { get; set; }
        public string cifCounterParty { get; set; }
        public string userId { get; set; }
        public int isInau { get; set; }

        public string counterPartyGroup { get; set; }
    }
}
