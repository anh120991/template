using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel.User
{
    public class DetailUserViewModel
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string officerCode { get; set; }
        public string userBranchCode { get; set; }

        public string userId { get; set; }
        public string branchCode { get; set; }
    }
}
