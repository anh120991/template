using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CounterPartyListSeachModel : SearchPagging
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string cif { get; set; }
        public string shortName { get; set; }
        public string userUpdated { get; set; }
        public string status { get; set; }

        public string cpGroupCode { get; set; }


    }
}
