using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.DatabaseModel
{
    public class UserSearchModel
    {
        public UserSearchModel()
        {
            resultList = new List<UserSearchItemModel>();
        }
        public List<UserSearchItemModel> resultList { get; set; }
        public int totalRecord { get; set; }
        public int totalPage { get; set; }
    }
    public class UserSearchItemModel
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string officerCode { get; set; }
        public string userBranchCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public int isActive { get; set; }
        public int isDelete { get; set; }
        public string lastUserUpdated { get; set; }
        public DateTime? lastDatetimeUpdated { get; set; }
    }
}
