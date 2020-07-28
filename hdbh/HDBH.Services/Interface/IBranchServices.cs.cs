using HDBH.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Services.Interface
{
    public interface IBranchService
    {
        List<BranchDetailModel> getListBranch(string branchCode);
    }
}
