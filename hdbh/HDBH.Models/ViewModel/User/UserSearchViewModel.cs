using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class UserSearchViewModel : SearchPagging
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string branchCode { get; set; }
        public string isActive { get; set; }
        public string isDelete { get; set; }
        public string userId { get; set; }
    }
}
