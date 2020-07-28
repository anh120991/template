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
    public interface IUserService
    {
        UserLoginModel getAllPermissionUser(string userId);
        List<RoleModel> getListRole();
        ResultModel insertUser(UserInsertViewModel model);
        UserSearchModel searchUser(UserSearchViewModel input);

        ResultModel insertUserList(UserImportModel model);
    }
}
