using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class InsuranceContractCollateralViewModel
    {
        public string collateralCode { get; set; }
        public string collateralTypeCode { get; set; }
        public Decimal? collateralValue { get; set; }
        public Decimal? executionValue { get; set; }
        public string manBranchCode { get; set; }
        public long insCollId { get; set; }
    }
}
