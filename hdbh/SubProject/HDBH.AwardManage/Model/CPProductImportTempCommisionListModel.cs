using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    //để đúng thứ tự import
    public class CPProductImportTempCommisionListModel
    {
        public CPProductImportTempCommisionListModel()
        {
            isShowAdd = true;
            isShowRemove = true;
        }
        public string productCode { get; set; }
        public DateTime? effectedFromDate { get; set; }
        public DateTime? effectedToDate { get; set; }
       
        public decimal? agencyCommisRate { get; set; }
        public decimal? supportCommisRate { get; set; }
        public decimal? serviceCostRate { get; set; }
        public decimal? commisRate { get; set; }
        public decimal? remainRate { get; set; }
        public bool isShowAdd { get; set; }
        public bool isShowRemove { get; set; }
        public string errorMessage { get; set; }

        public object SelectList { get; set; }
        public string productName { get; set; }
        public decimal? totalCommisRate { get; set; }
    }
}
