using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class CollateralItemModel
    {
        public string collateralCode { get; set; }
        public string collateralDesc { get; set; }
        public string collateralTypeCode { get; set; }
        public decimal? collateralValue { get; set; }
        public decimal? executionValue { get; set; }
        public string ownerCif { get; set; }
        public string ownerName { get; set; }
        public string manBranchCode { get; set; }
    }
}
