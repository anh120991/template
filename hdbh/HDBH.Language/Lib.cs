using HDBH.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Language
{
    public static class Lib
    {
        public static ResultModel readResult(this ResultModel result)
        {
            result.errorMessage = Localization.Get("err" + result.errorCode);
            return result;
        }
    }
}
