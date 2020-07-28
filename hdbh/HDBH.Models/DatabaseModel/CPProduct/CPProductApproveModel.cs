using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CPProductApproveModel
    {
        public long counterPartyId { get; set; }
        public string counterPartyGroup { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public string approveStatus { get; set; }
        public string approveContent { get; set; }
    }
}
