using HDBH.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CPProductSearchViewModel : SearchPagging
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string cif { get; set; }
        public string shortName { get; set; }
        public string cpGroupCode { get; set; }
        public string userUpdated { get; set; }
        public string statusList { get; set; }
        public string branchCode { get; set; }
        public int isInau { get; set; }
        public string userId { get; set; }
    }

}

