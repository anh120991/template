using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class SearchInsuranceContract : SearchPagging
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string insuranceContractNo { get; set; }
        public string refInsuranceNo { get; set; }
        public string contractTypeCode { get; set; }
        public string contractFormCode { get; set; }
        public string userUpdated { get; set; }
        public List<string> _processStatusList { get; set; }
        public string processStatusList { get; set; }
        public string customerCif { get; set; }
        public string branchCode { get; set; }
        public string userId { get; set; }
        public int isInau { get; set; }
        public int renewStatus { get; set; }
    }

    public class ModelTest
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string insuranceContractNo { get; set; }
        public string refInsuranceNo { get; set; }
        public string contractTypeCode { get; set; }
        public string contractFormCode { get; set; }
        public string userUpdated { get; set; }
        public List<string> processStatusList { get; set; }
        public string customerCif { get; set; }
        public string branchCode { get; set; }
        public string userId { get; set; }
        public int isInau { get; set; }
        public int renewStatus { get; set; }
    }
}
