using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CPProductViewModel
    {
        public CPProductViewModel()
        {
            productList = new List<CPProductImportProductModel>();
            commisionList = new List<CPProductImportCommisionListModel>();
            fileList = new List<CPProductDetailtAttachmenViewItemModel>();
        }
        public long counterPartyId { get; set; }
        public string cifCounterParty { get; set; }
        public string counterPartyName { get; set; }
        public string counterPartyGroup { get; set; }
        public string shortName { get; set; }
        public DateTime signedContractDate { get; set; }
        public string paymentAccount { get; set; }
        public string userId { get; set; }
        public int isInau { get; set; }
        public string branchCode { get; set; }
        public string counterPartyStatus { get; set; }
        public List<CPProductImportProductModel> productList { get; set; }
        public List<CPProductImportCommisionListModel> commisionList { get; set; }
        public List<CPProductDetailtAttachmenViewItemModel> fileList { get; set; }
        public string viewMode { get; set; }

    }
    public class CPProductDetailtAttachmenViewItemModel
    {
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileDescription { get; set; }
        public string cdnPath { get; set; }
        public string fileUserUpdated { get; set; }
        public DateTime? fileDatetimeUpdated { get; set; }
    }
}
