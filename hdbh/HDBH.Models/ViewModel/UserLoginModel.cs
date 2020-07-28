using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class UserLoginModel
    {
        public UserLoginModel()
        {
            permissionList = new List<UserLoginPermission>();
        }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string officerCode { get; set; }
        public string branchCode { get; set; }
        public int isHO { get; set; }
        public string phoneNumber { get; set; }
        public int isActive { get; set; }
        public int isDelete { get; set; }
        public List<UserLoginPermission> permissionList { get; set; }
    }

    public class UserLoginPermission
    {
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public string permissionCode { get; set; }
        public string permissionName { get; set; }
    }
}
