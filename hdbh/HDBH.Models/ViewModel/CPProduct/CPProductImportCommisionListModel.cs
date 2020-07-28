using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class CPProductImportCommisionListModel
    {
        public CPProductImportCommisionListModel()
        {
            isShowAdd = true;
            isShowRemove = true;
        }
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
        public bool isShowAdd { get; set; }
        public bool isShowRemove { get; set; }
        public string errorMessage { get; set; }
        public long productAutoId { get; set; }
        public long isUsed { get; set; }
        public object SelectList { get; set; }
    }
}
