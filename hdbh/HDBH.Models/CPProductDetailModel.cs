using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models
{
    public class CPProductDetailModel
    {
        public CPProductDetailModel()
        {
            productList = new List<CPProductDetailProductItemModel>();
            fileList = new List<CPProductDetailtAttachmenItemModel>();
        }
        public long counterPartyId { get; set; }
        public string counterPartyName { get; set; }
        public string cifCounterParty { get; set; }
        public string counterPartyGroup { get; set; }
        public string counterPartyGroupName { get; set; }
        public string shortName { get; set; }
        public DateTime? signedContractDate { get; set; }
        public string paymentAccount { get; set; }
        public string counterPartyDesc { get; set; }
        public string version { get; set; }
        public string productStatus { get; set; }
        public string contentApproved { get; set; }
        public string userApproved { get; set; }
        public DateTime? datetimeApproved { get; set; }
        public int isInau { get; set; }
        public List<CPProductDetailProductItemModel> productList { get; set; }
        public List<CPProductDetailtAttachmenItemModel> fileList { get; set; }
    }

    public class CPProductDetailCommisionItemModel
    {
        public long commisAutoId { get; set; }
        public int isUsed { get; set; }
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
    }

    public class CPProductDetailProductItemModel
    {
        public CPProductDetailProductItemModel()
        {
            commisionList = new List<CPProductDetailCommisionItemModel>();
        }
        public long productAutoId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public long productId { get; set; }
        public string description { get; set; }
        public string productStatus { get; set; }
        public string branchCode { get; set; }
        public int isUsed { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
        public List<CPProductDetailCommisionItemModel> commisionList { get; set; }
    }

    public class CPProductDetailtAttachmenItemModel
    {
        public long fileId { get; set; }
        public long fileAutoId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileDescription { get; set; }
        public string cdnPath { get; set; }
        public string fileUserUpdated { get; set; }
        public DateTime? fileDatetimeUpdated { get; set; }
    }
}
