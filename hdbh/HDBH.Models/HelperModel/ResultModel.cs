using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.HelperModel
{
    public class ResultModel
    {
        public ResultModel()
        {
            errorCode = -1;
            errorMessage = "Lỗi chưa được thực thi";
        }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
