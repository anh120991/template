using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class UserImportModel
    {
        public UserImportModel()
        {
            userList = new List<UserImportItemModel>();
        }
        public string userId { get; set; }
        public string branchCode { get; set; }
        public string strUserList { get; set; }
        public List<UserImportItemModel> userList { get; set; }
    }
    public class UserImportItemModel
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string officerCode { get; set; }
        public string userBranchCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
    }
}
