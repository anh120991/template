using Excel;
using HDBH.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Lib
{
    public class Reader
    {
        public string FileName { get; private set; }
        public bool IsFirstRowAsColumnNames { get; private set; }
        public List<string> ColumnNames { get; private set; }
        private DataSet data = null;
        public Reader(string fileName, List<string> columnNames)
        {
            FileName = fileName;
            IsFirstRowAsColumnNames = false;
            ColumnNames = columnNames;
            Process();
        }
        public Reader(string fileName)
        {
            FileName = fileName;
            IsFirstRowAsColumnNames = true;
            Process();
        }
        private void Process()
        {
            using (FileStream stream = System.IO.File.Open(FileName, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader = null;
                try
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                catch (Exception)
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                if (IsFirstRowAsColumnNames)
                {
                    excelReader.IsFirstRowAsColumnNames = IsFirstRowAsColumnNames;
                }
                data = excelReader.AsDataSet();
            }
        }
        public DataTable GetData(int sheetNumber = 0)
        {
            return data.Tables[sheetNumber];
        }
        public List<T> GetData<T>(int sheetNumber = 0)
        {
            XmlParser parser = new XmlParser(GetXMLData(sheetNumber));
            return parser.GetList<T>("TABLE");
        }
        public string GetXMLData(int sheetNumber = 0)
        {
            var dt = data.Tables[sheetNumber];
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer);
            return writer.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transformer"></param>
        /// <param name="sheetNumber"></param>
        /// <param name="intRemoveRows">Số row cần bỏ qua (không tính dòng đầu tiên)</param>
        /// <returns></returns>
        public List<T> GetData<T>(IDataTransformer transformer, int sheetNumber = 0, int intRemoveRows = 0)
        {
            var dt = data.Tables[sheetNumber];
            DataTable tb = dt as DataTable;
            if (intRemoveRows > 0 && tb != null && tb.Rows.Count > intRemoveRows)
            {
                for (int i = 0; i < intRemoveRows; i++)
                {
                    tb.Rows.RemoveAt(0);
                }
            }

            return transformer.GetList<T>(tb);
        }
   
    }
}
