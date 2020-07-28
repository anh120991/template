using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CPProductImportProductModel
    {
        public CPProductImportProductModel()
        {
            isShowAdd = true;
            isShowRemove = true;
        }
        public long productAutoId { get; set; }
        public long commisAutoId { get; set; }
        public long isUsed { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public long productId { get; set; }
        public string description { get; set; }
        public string productStatus { get; set; }
        public string branchCode { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
        public bool isShowAdd { get; set; }
        public bool isShowRemove { get; set; }
        public string errorMessage { get; set; }
    }

    public class CPProductImportTempProductModel
    {
        public string productCode { get; set; }
        public string productName { get; set; }
    }
}
