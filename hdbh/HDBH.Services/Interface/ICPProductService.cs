using HDBH.Models;
using HDBH.Models.DatabaseModel;
using HDBH.Models.HelperModel;
using HDBH.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services.Interface
{
    public interface ICPProductService
    {
        ResultModel insertCpProdCommis(CPProductInsertModel model);
        ResultModel updateCpProdCommis(CPProductInsertModel model);
        ResultModel approveCpProdCommis(CPProductApproveModel model);
        CPProductSearchModel searchCpProdCommis(CPProductSearchViewModel model);
        CPProductSearchModel searchCpProdCommisInau(CPProductSearchViewModel model);
        CPProductDetailModel getDetailCpProdCommis(CPProductGetDetailViewModel input);

        CPProductDetailModel getListCpProdForContract(long counterPartyId, string cifCounterParty, string counterPartyGroup, DateTime? dataDate, string userId);
        ResultModel deleteCpProdCommis(long counterPartyId, string counterPartyGroup, string status, string userId, string branchCode);
    }
}
