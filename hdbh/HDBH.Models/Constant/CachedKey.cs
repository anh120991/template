using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.Constant
{
    public class CachedKey
    {
        public const string loginModuleKeyCache = "_hdbh_login_Module_Key";
        public const string loginPermissionKeyCache = "_hdbh_login_Permission_Key";

        #region COUNTER PARTY
        public const string counterPartyGroupList = "_hdbh_counter_party_group_list_Key";
        #endregion

        #region INSURANCE CONTRACT
        public const string ContractTypeList = "_hdbh_contract_type_list_Key";
        #endregion

        #region ROLE
        public const string RoleList = "_hdbh_role_list_Key";
        #endregion

    }
}
