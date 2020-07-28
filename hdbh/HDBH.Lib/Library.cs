using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace HDBH.Lib
{
    public static class Library
    {
        private static Random random = new Random();
        
        /// <summary>
        ///  Transfer Data Object
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void TransferData<TSource, TTarget>(TSource source, ref TTarget target)
        {
            TransferData(source, ref target, null, new string[] { });
        }

        /// <summary>
        /// TransferList Data object
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void TransferData<TSource, TTarget>(List<TSource> source, ref List<TTarget> target)
        {
            target = new List<TTarget>();
            foreach (var _source in source)
            {
                var _target = (TTarget)Activator.CreateInstance(typeof(TTarget));
                TransferData(_source, ref _target, null, new string[] { });
                target.Add(_target);
            }
        }

        private static void TransferData<TSource, TTarget>(TSource source, ref TTarget target, Type _targetType, object include)
        {
            if (source == null)
                return;
            string[] includeProperty = include?.GetType().GetProperties().Select(a => a.Name).ToArray() ?? new string[] { };
            PropertyInfo[] __propSource = source.GetType().GetProperties().ToArray();

            Type targetObject = typeof(TTarget);
            if (_targetType != null)
            {
                targetObject = _targetType;
                target = (dynamic)Activator.CreateInstance(_targetType);
            }

            foreach (var propSource in __propSource)
            {
                var propTarget = targetObject.GetProperty(propSource.Name);
                if (propTarget != null)
                {
                    var sourceType = propSource.PropertyType;
                    var targetType = propTarget.PropertyType;

                    if (sourceType.GetInterfaces().Any(a => a.IsGenericType &&
                            a.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        && includeProperty.Contains(propSource.Name))
                    {
                        //Transfer child as list
                        Type _type2 = targetType.GetGenericArguments()[0];
                        var _data1 = propSource.GetValue(source);
                        var listType = typeof(List<>);
                        var constructedListType = listType.MakeGenericType(_type2);
                        var _data2 = Activator.CreateInstance(constructedListType);

                        //Get child object include
                        var includeChild = include.GetType().GetProperty(propSource.Name)?.GetValue(include);

                        foreach (var item1 in (dynamic)_data1)
                        {
                            dynamic item2 = (dynamic)Activator.CreateInstance(_type2);
                            TransferData(item1, ref item2, _type2, includeChild);
                            _data2.GetType().GetMethod("Add").Invoke(_data2, new[] { item2 });
                        }
                        propTarget.SetValue(target, _data2);
                    }
                    else if (sourceType.Namespace.StartsWith("HDBH")
                        && targetType.Namespace.StartsWith("HDBH")
                        && sourceType.IsClass
                        && targetType.IsClass
                        && includeProperty.Contains(propSource.Name))
                    {
                        //Transfer child as class object
                        var _data1 = propSource.GetValue(source);
                        dynamic item2 = (dynamic)Activator.CreateInstance(targetType);

                        //Get child object include
                        var includeChild = include.GetType().GetProperty(propSource.Name)?.GetValue(include);

                        TransferData(_data1, ref item2, targetType, includeChild);
                        propTarget.SetValue(target, item2);
                    }
                    else if (sourceType == targetType
                        || (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            && Nullable.GetUnderlyingType(sourceType) == targetType)
                        || (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            && Nullable.GetUnderlyingType(targetType) == sourceType))
                    {
                        var _data = propSource.GetValue(source);
                        propTarget.SetValue(target, _data);
                    }
                }
            }
        }

        public static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        public static string encode(string text)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
            string returntext = System.Convert.ToBase64String(mybyte);
            return returntext;
        }

        public static string decode(string text)
        {
            byte[] mybyte = System.Convert.FromBase64String(text);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        public static string getExtentionFile(string file)
        {
            try
            {
                string ext = Path.GetExtension(file);
                return ext;
            }
            catch
            {
                return "atm";
            }
        }

        public static bool checkAction(string area, string controller, string action, string method = "POST")
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath(@"~\App_Data\actionNoncheck.xml");
                using (XmlTextReader reader = new XmlTextReader(path))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);
                    XmlNode root = xmlDoc.DocumentElement;
                    XmlNode myNode = root.SelectSingleNode("/actions/area[@name='" + area.Trim().ToUpper() + "']/controller[@name='" + controller.Trim().ToUpper() + "']/action[@name='" + action.Trim().ToUpper() + "'][@method='" + method.Trim().ToUpper() + "']");
                    return myNode != null;

                }
            }
            catch
            {
                return false;
            }
        }

        public static void ShowMessage(string Message, string Title)
        {
            System.Web.HttpContext.Current.Response.Write(@"<SCRIPT LANGUAGE=""JavaScript"">alert(""Hello this is an Alert"")</SCRIPT>");
        }

        public static string checkStatus(string processStatusCode)
        {
            string result = string.Empty;
            switch (processStatusCode.Trim().ToUpper())
            {
                case "REJCLOSED": result = "Từ chối đóng HĐ"; break;
                case "INAU": result = "Chờ duyệt"; break;
                case "A": result = "Đã duyệt"; break;
                case "EDIT": result = "Cần điều chỉnh"; break;
                case "RV": result = "Hủy"; break;
                case "DEL": result = "Xóa"; break;
                case "CLOSED": result = "Đã đóng HĐ"; break;
                case "REJ": result = "Từ chối"; break;
                case "CHK": result = "Đã duyệt cấp 1"; break;
                case "OVERCOME": result = "Sắp quá hạn"; break;
                case "OOD": result = "Đã quá hạn"; break;
                case "INAUCLOSED": result = "Chờ duyệt đóng HĐ"; break;
                case "CHKCLOSED": result = "Đã duyệt đóng HĐ cấp 1"; break;
                default: result = "Không tìm thấy trạng thái"; break;
            }
            return result;
        }


    }
}