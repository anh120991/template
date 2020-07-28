using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class MDContractType
    {
        public DateTime? LAST_DATETIME_UPDATED { get; set; }
        public int IS_NONLIFE_INSURANCE { get; set; }
        public string CONTRACT_TYPE_CODE { get; set; }
        public string CONTRACT_TYPE_NAME { get; set; }
        public string CONTRACT_TYPE_STATUS { get; set; }
        public string LAST_USER_UPDATED { get; set; }
    }

    public class MDContractForm
    {
        public DateTime? LAST_DATETIME_UPDATED { get; set; }
        public string CONTRACT_FORM_CODE { get; set; }
        public string CONTRACT_FORM_NAME { get; set; }
        public string CONTRACT_FORM_STATUS { get; set; }
        public string LAST_USER_UPDATED { get; set; }
    }

    public class MDProcessStatus
    {
        public string processStatusCode { get; set; }
        public string processStatusName { get; set; }
        public int? isStatusInau { get; set; }
    }
}
