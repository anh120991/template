using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class BranchDetailModel
    {
        public DateTime lastDatetimeUpdated { get; set; }

        public int isHo { get; set; }

        public string branchAddress { get; set; }

        public string branchCode { get; set; }

        public string branchMnemonic { get; set; }

        public string branchName { get; set; }

        public string branchStatus { get; set; }

        public string lastUserUpdated { get; set; }

        public string parentBranchCode { get; set; }

        public string taxCode { get; set; }
    }

    public class ListBranchDetailModel
    {
        public ListBranchDetailModel()
        {
            resultList = new List<BranchDetailModel>();
        }
        public List<BranchDetailModel> resultList { get; set; }
    }
}
