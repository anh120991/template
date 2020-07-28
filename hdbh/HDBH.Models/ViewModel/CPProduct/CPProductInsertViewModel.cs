using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CPProductInsertViewModel
    {
        public CPProductInsertViewModel()
        {
            productList = new List<CPProductImportProductModel>();
            commisionList = new List<CPProductImportCommisionListModel>();
            fileList = new List<AttachmentViewModel>();
        }
        public long counterPartyId { get; set; }
        public string cifCounterParty { get; set; }
        public string counterPartyGroup { get; set; }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public string counterPartyStatus { get; set; }
        public List<CPProductImportProductModel> productList { get; set; }
        public List<CPProductImportCommisionListModel> commisionList { get; set; }
        public List<AttachmentViewModel> fileList { get; set; }
        public int isInau { get; set; }
    }
}
