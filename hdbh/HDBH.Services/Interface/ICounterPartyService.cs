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
    public interface ICounterPartyService
    {
        List<CounterPartyGroupModel> getListCounterPartyGroup();

        ResultModel insertCounterParty(CounterPartyDetailInsertModel model);

        CounterPartySearchModel searchCounterParty(CounterPartyListSeachModel model);

        CounterPartyDetailModel getDetailCounterParty(CounterPartyGetDetailModel input);

        ResultModel updateCounterParty(CounterPartyDetailInsertModel model);

        List<CounterPartySelectItemModel> getListCounterParty(string cpGroupCode, string userId);
    }
}
