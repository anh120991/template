using HDBH.Lib.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Lib
{
    public class DataTransformer : IDataTransformer
    {
        public List<T> GetList<T>(DataTable dt)
        {
            List<T> ret = null;
            ret = Activator.CreateInstance<List<T>>();
            int lenCol = dt.Columns.Count;
            var rows = dt.Rows;

            for (int i = 0; i < rows.Count; i++)
            {

                T it = default(T);
                it = Activator.CreateInstance<T>();
                Type t = it.GetType();
                PropertyDescriptorCollection infos = TypeDescriptor.GetProperties(t);

                if (infos != null)
                {

                    for (int j = 0; j < lenCol; j++)
                    {
                        TypeConverter typeConverter = infos[j].Converter;
                        object objVal = rows[i][j];
                        if (typeConverter != null)
                        {
                            objVal = typeConverter.ConvertFromString(objVal.ToString());
                        }
                        infos[j].SetValue(it, objVal);
                    }

                }
                ret.Add(it);
            }
            return ret;
        }

    }


}
