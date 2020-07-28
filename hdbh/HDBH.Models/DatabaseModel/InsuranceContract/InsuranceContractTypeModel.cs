using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{

    // Model ContractType
    public class InsuranceContractTypeModel
    {
        public string contractTypeCode { get; set; }
        public string contractTypeName { get; set; }
        public int isNonlifeInsurance { get; set; }
        public string contractTypeStatus { get; set; }

        public string lastUserUpdated { get; set; }

        public DateTime? lastDatetimeUpdated { get; set; }
    }

    public class ListInsuranceContractTypeModel
    {
        public ListInsuranceContractTypeModel()
        {
            resultList = new List<InsuranceContractTypeModel>();
        }
        public List<InsuranceContractTypeModel> resultList { get; set; }
    }

    // Model ContractForm
    public class ListInsuranceContractFormModel
    {
        public ListInsuranceContractFormModel()
        {
            resultList = new List<InsuranceContractFormModel>();
        }
        public List<InsuranceContractFormModel> resultList { get; set; }
    }
}
