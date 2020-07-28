using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Lib.Interface
{
    public interface IDataTransformer
    {
        List<T> GetList<T>(DataTable dt);
    }
}
