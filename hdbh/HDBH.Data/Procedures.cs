using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Data
{
    public static class Procedures
    {
        #region DW_COMPANY
        public static readonly string SP_SEACH_DW_COMPANY_LIST = "SP_SEACH_DW_COMPANY_LIST";
        public static readonly string SP_INSERT_BRANCH = "SP_INSERT_BRANCH";
        public static readonly string SP_GET_DW_COMPANY_BYID = "SP_GET_DW_COMPANY_BYID";
        public static readonly string SP_UPDATE_BRANCH = "SP_UPDATE_BRANCH";

        #endregion

        #region PHANQUYEN_USER
        public static readonly string SP_GET_USER_BYUSERID = "SP_GET_USER_BYUSERID";
        public static readonly string SP_SEARCH_PHANQUYEN_USER_LIST = "SP_SEARCH_PHANQUYEN_USER_LIST";
        public static readonly string SP_INSERT_USER = "SP_INSERT_USER";
        public static readonly string SP_UPDATE_USER = "SP_UPDATE_USER";
        #endregion

        #region BRANCH PERMISSION
        public static readonly string SP_GET_PHANQUYEN_CN_BYUSERID = "SP_GET_PHANQUYEN_CN_BYUSERID";
        #endregion

        #region NON LIFE INSURANCE
        public static readonly string SP_SEARCH_INSURANCE_LIST = "PKG_HDBH.SP_SEARCH_INSURANCE_LIST";
        public static readonly string SP_INSERT_INSURANCE = "PKG_HDBH.SP_INSERT_INSURANCE";
        public static readonly string SP_UPDATE_CLOSE_STATUS = "PKG_HDBH.SP_UPDATE_CLOSE_STATUS";
        public static readonly string SP_GET_INSURANCE_DETAIL = "PKG_HDBH.SP_GET_INSURANCE_DETAIL";
        public static readonly string SP_SEARCH_INSUR_INAU_LV1 = "PKG_HDBH.SP_SEARCH_INSUR_INAU_LV1";
        public static readonly string SP_SEARCH_INSUR_INAU_LV2 = "PKG_HDBH.SP_SEARCH_INSUR_INAU_LV2";
        public static readonly string SP_EDIT_INSURANCE = "PKG_HDBH.SP_EDIT_INSURANCE";

        public static readonly string SP_APPROVE_INSUR_LV1 = "PKG_HDBH.SP_APPROVE_INSUR_LV1";
        public static readonly string SP_APPROVE_INSUR_LV2 = "PKG_HDBH.SP_APPROVE_INSUR_LV2";

        #endregion

        #region CONTRACT
        public static readonly string SP_GET_CONTRACT_FORM = "PKG_HDBH.SP_GET_CONTRACT_FORM";
        public static readonly string SP_GET_PROCESS_STATUS = "PKG_HDBH.SP_GET_PROCESS_STATUS";
        public static readonly string SP_GET_CONTRACT_TYPE = "PKG_HDBH.SP_GET_CONTRACT_TYPE";
        #endregion

        #region EXPECT TYPE
        public static readonly string SP_GET_EXPECT_TYPE = "PKG_HDBH.SP_GET_EXPECT_TYPE";
        #endregion


        #region COUNTER PARTY
        public static readonly string SP_GET_COUNTERPARTY_LIST = "PKG_HDBH.SP_GET_COUNTERPARTY_LIST";
        public static readonly string SP_GET_PRODUCT_COUNTERPARTY = "PKG_HDBH.SP_GET_PRODUCT_COUNTERPARTY";
        public static readonly string SP_SEARCH_COUNTERPARTY_LIST = "PKG_HDBH.SP_SEARCH_COUNTERPARTY_LIST";
        public static readonly string getCounterPartyByCif = "PKG_HDBH.getCounterPartyByCif";
        public static readonly string SP_INSERT_COUNTERPARTY = "PKG_HDBH.SP_INSERT_COUNTERPARTY";
        public static readonly string SP_EDIT_COUNTERPARTY = "PKG_HDBH.SP_EDIT_COUNTERPARTY";
        public static readonly string SP_GET_COUNTERPARTY_DETAIL = "PKG_HDBH.SP_GET_COUNTERPARTY_DETAIL";
        public static readonly string SP_SEARCH_COUNTERPARTY_INAU = "PKG_HDBH.SP_SEARCH_COUNTERPARTY_INAU";
        public static readonly string SP_APPROVE_COUNTERPARTY = "PKG_HDBH.SP_APPROVE_COUNTERPARTY";
        public static readonly string SP_DELETE_COUNTERPARTY = "PKG_HDBH.SP_DELETE_COUNTERPARTY";

        #endregion

        #region RECIPIENT 
        public static readonly string SP_SEARCH_RECIPIENT_LIST = "PKG_HDBH.SP_SEARCH_RECIPIENT_LIST";
        public static readonly string SP_GET_RECIPIENT_DETAIL = "PKG_HDBH.SP_GET_RECIPIENT_DETAIL";
        public static readonly string SP_SEARCH_RECIPIENT_INAU_LV1 = "PKG_HDBH.SP_SEARCH_RECIPIENT_INAU_LV1";
        public static readonly string SP_SEARCH_RECIPIENT_INAU_LV2 = "PKG_HDBH.SP_SEARCH_RECIPIENT_INAU_LV2";
        #endregion

        #region RECEIPT
        public static readonly string SP_SEARCH_RECEIPT_LIST = "PKG_HDBH.SP_SEARCH_RECEIPT_LIST";
        #endregion

        #region COLATERAL
        public static readonly string getCollateralByCifT24 = "PKG_HDBH.getCollateralByCifT24";
        #endregion

        #region PAYMENT SCHEDULE
        public static readonly string SP_SEARCH_PAYMENT_SCHEDULE = "PKG_HDBH.SP_SEARCH_PAYMENT_SCHEDULE";
        #endregion

        #region ROLE
        public static readonly string SP_SEARCH_ROLE = "PKG_HDBH.SP_SEARCH_ROLE";
        public static readonly string SP_GET_ROLE_BY_USER = "PKG_HDBH.SP_GET_ROLE_BY_USER";
        public static readonly string SP_SEARCH_MODULE = "PKG_HDBH.SP_SEARCH_MODULE";

        #endregion

        #region USER
        public static readonly string SP_GET_USER_LOGIN = "PKG_HDBH.SP_GET_USER_LOGIN";
        #endregion
    }

}
