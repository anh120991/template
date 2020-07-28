using HDBH.Models;
using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
using HDBH.Models.ViewModel.InsuranceContractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services.Interface
{
    public interface IInsuranceContractService
    {
        List<InsuranceContractTypeModel> getListContractType(string contractTypeCode, int isNonlifeInsurance);
        List<InsuranceExceptTypeModel> getListExceptType(string insuranceContractType);
        List<InsuranceContractFormModel> getListContractForm(string contractTypeCode, int isNonlifeInsurance);
        ListInsuranceContractDetailViewModel searchInsuranceContract(SearchInsuranceContract input);
        CPProductDetailModel getListCpProdForContract(long counterPartyId, string cifCounterParty, string counterPartyGroup, string dataDate, string userId);
        ListInsuranceContractDetailViewModel searchInsuranceContractInau(SearchInsuranceContract input);
        GetDetailInsuranceContractModel getDetailInsurContract(InputApproveInsuranceContractModel input);
        CollateralItemModel getDetailCollateral(string collateralCode, string userId);
        ResultModel insertInsurContractCollateral(InsuranceContractCollateralInsertModel input);
        ResultModel updateInsurContractCollateral(InsuranceContractCollateralInsertModel input);
        ResultModel approveInsurContract(long insuranceId, string approveStatus, string approveContent, string userId, string branchCode);
        ResultModel approveInsurContractLv2(long insuranceId, string approveStatus, string approveContent, string userId, string branchCode);

        ResultModel insertInsurContract(InsuranceContractCollateralInsertModel input);
        ResultModel updateInsurContract(InsuranceContractCollateralInsertModel input);
        ResultModel updateCloseInsurContract(string insuranceId, string insuranceContractNo, string userId, string branchCode);
    }
}
