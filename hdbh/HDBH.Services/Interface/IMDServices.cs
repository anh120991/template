using HDBH.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services.Interface
{
    public interface IMDService
    {
        List<InsuranceContractTypeModel> getListContractType(string contractTypeCode, int isNonlifeInsurance);
        List<InsuranceContractFormModel> getListContractForm(string contractTypeCode, int isNonlifeInsurance);
        List<MDProcessStatus> getListStatus(int isStatusInau);
    }
}
