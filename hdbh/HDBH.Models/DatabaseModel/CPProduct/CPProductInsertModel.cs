using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CPProductInsertModel
    {
        public long counterPartyId { get; set; }
        public string cifCounterParty { get; set; }
        public string counterPartyGroup { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public string counterPartyStatus { get; set; }
        public string jsonProductList { get; set; }
        public string jsonFileList { get; set; }
        public string jsonCommisionList { get; set; }
        public int isInau { get; set; }
        public List<CPProductInsertProductItemModel> productList { get; set; }
        public List<CPProductInsertCommisionItemModel> commisionList { get; set; }
        public List<CPProductInsertAttachmenItemModel> fileList { get; set; }
    }
    public class CPProductInsertProductItemModel
    {
        public string productCode { get; set; }
        public string productName { get; set; }
        public long productId { get; set; }
        public long productAutoId { get; set; }
        public long isUsed { get; set; }
    }

    public class CPProductInsertCommisionItemModel
    {
        public long commisAutoId { get; set; }
        public long commisionId { get; set; }
        public long productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public DateTime effectedFromDate { get; set; }
        public DateTime effectedToDate { get; set; }
        public decimal totalCommisRate { get; set; }
        public decimal agencyCommisRate { get; set; }
        public decimal supportCommisRate { get; set; }
        public decimal serviceCostRate { get; set; }
        public decimal commisRate { get; set; }
        public decimal remainRate { get; set; }
        public int isUsed { get; set; }
    }

    public class CPProductInsertAttachmenItemModel
    {
        public long fileAutoId { get; set; }
        public long fileId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}
