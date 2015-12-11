//------------------------------------------------------------------------------
//	文件名称：System\strUtil.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Security;
using System.Text;
using System.IO;
namespace MySelfEntityMvc.UtilityTools
{
    /// <summary>
    /// 字符串工具类，封装了常见字符串操作
    /// </summary>
    public partial class strUtil
    {
        private static Regex htmlReg = new Regex("<[^>]*>");
        /// <summary>
        /// 检查字符串是否是 null 或者空白字符。不同于.net自带的string.IsNullOrEmpty，多个空格在这里也返回true。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(String target)
        {
            if (target != null)
            {
                return target.Trim().Length == 0;
            }
            return true;
        }
        /// <summary>
        /// 检查是否包含有效字符(空格等空白字符不算)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Boolean HasText(String target)
        {
            return !IsNullOrEmpty(target);
        }
        /// <summary>
        /// 比较两个字符串是否相等
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static Boolean Equals(String s1, String s2)
        {
            if (s1 == null && s2 == null) return true;
            if (s1 == null || s2 == null) return false;
            if (s2.Length != s1.Length) return false;
            return string.Compare(s1, 0, s2, 0, s2.Length) == 0;
        }
        /// <summary>
        /// 比较两个字符串是否相等(不区分大小写)
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static Boolean EqualsIgnoreCase(String s1, String s2)
        {
            if (s1 == null && s2 == null) return true;
            if (s1 == null || s2 == null) return false;
            if (s2.Length != s1.Length) return false;
            return string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
        }
        /// <summary>
        /// 将 endString 附加到 srcString末尾，如果 srcString 末尾已包含 endString，则不再附加。
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static String Append(String srcString, String endString)
        {
            if (strUtil.IsNullOrEmpty(srcString)) return endString;
            if (strUtil.IsNullOrEmpty(endString)) return srcString;
            if (srcString.EndsWith(endString)) return srcString;
            return srcString + endString;
        }
        /// <summary>
        /// 将对象转为字符串，如果对象为 null，则转为空字符串(string.Empty)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ConverToNotNull(Object str)
        {
            if (str == null) return "";
            return str.ToString();
        }
        /// <summary>
        /// 从字符串中截取指定长度的一段，如果源字符串被截取了，则结果末尾出现省略号...
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="length">需要截取的长度</param>
        /// <returns></returns>
        public static String CutString(Object str, int length)
        {
            return CutString(ConverToNotNull(str), length);
        }
        /// <summary>
        /// 从字符串中截取指定长度的一段，如果源字符串被截取了，则结果末尾出现省略号...
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="length">需要截取的长度</param>
        /// <returns></returns>
        public static String CutString(String str, int length)
        {
            if (str == null) return null;
            if (str.Length > length) return String.Format("{0}...", str.Substring(0, length));
            return str;
        }
        /// <summary>
        /// 将字符串转换为编辑器中可用的字符串(替换掉换行符号)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String Edit(String str)
        {
            return str.Replace("\n", "").Replace("\r", "").Replace("'", "&#39;");
        }
        /// <summary>
        /// 对双引号进行编码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static String EncodeQuote(String src)
        {
            return src.Replace("\"", "&quot;");
        }
        /// <summary>
        /// 让 html 在 textarea 中正常显示。替换尖括号和字符&amp;lt;与&amp;gt;
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static String EncodeTextarea(String html)
        {
            if (html == null) return null;
            return html.Replace("&lt;", "&amp;lt;").Replace("&gt;", "&amp;gt;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
        /// <summary>
        /// 对双引号进行编码，并替换掉换行符
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static String EncodeQuoteAndClearLine(String src)
        {
            return src.Replace("\"", "\\\"").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
        }
        /// <summary>
        /// 获取 html 文档的标题内容
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public String getHtmlTitle(String html)
        {
            Match match = Regex.Match(html, "<title>(.*)</title>");
            if (match.Groups.Count == 2) return match.Groups[1].Value;
            return "(unknown)";
        }

        /// <summary>
        /// 将整数按照指定的长度转换为字符串，比如33转换为6位就是"000033"
        /// </summary>
        /// <param name="intValue"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String GetIntString(int intValue, int length)
        {
            return String.Format("{0:D" + length + "}", intValue);
        }

        /// <summary>
        /// 得到字符串的 TitleCase 格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String GetTitleCase(String str)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str);
        }

        /// <summary>
        /// 得到字符串的 CamelCase 格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String GetCamelCase(String str)
        {
            if (IsNullOrEmpty(str)) return str;
            return str[0].ToString().ToLower() + str.Substring(1);
        }


        /// <summary>
        /// 从类型的全名中获取类型名称(不包括命名空间)
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static String GetTypeName(String typeFullName)
        {
            String[] strArray = typeFullName.Split(new char[] { '.' });
            return strArray[strArray.Length - 1];
        }

        /// <summary>
        /// 获取类型名称(主要针对泛型做特殊处理)。如果要获取内部元素信息，请使用t.GetGenericArguments
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static String GetTypeName(Type t)
        {
            if (t.IsGenericType == false) return t.Name;
            return t.Name.Split('`')[0];
        }

        /// <summary>
        /// 获取类型全名(主要针对泛型做特殊处理)，比如List&lt;String&gt;返回System.Collections.Generic.List。如果要获取内部元素信息，请使用t.GetGenericArguments
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static String GetTypeFullName(Type t)
        {
            if (t.IsGenericType == false) return t.FullName;
            return t.FullName.Split('`')[0];
        }

        /// <summary>
        /// 返回泛型的类型全名，包括元素名，比如System.Collections.Generic.List&lt;System.String&gt;
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static String GetGenericTypeWithArgs(Type t)
        {

            //System.Collections.Generic.Dictionary`2[System.Int32,System.String]
            String[] arr = t.ToString().Split('`');

            String[] arrArgs = arr[1].Split('[');
            String args = "<" + arrArgs[1].TrimEnd(']') + ">";

            return arr[0] + args;
        }

        /// <summary>
        /// 是否是英文字符和下划线
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static Boolean IsLetter(String rawString)
        {
            if (IsNullOrEmpty(rawString)) return false;

            char[] arrChar = rawString.ToCharArray();
            foreach (char c in arrChar)
            {

                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_".IndexOf(c) < 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 是否是英文、数字和下划线，但不能以下划线开头
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static Boolean IsUrlItem(String rawString)
        {
            if (IsNullOrEmpty(rawString)) return false;

            char[] arrChar = rawString.ToCharArray();
            if (arrChar[0] == '_') return false;

            foreach (char c in arrChar)
            {
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890".IndexOf(c) < 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 是否全部都是中文字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsChineseLetter(String str)
        {
            if (strUtil.IsNullOrEmpty(str)) return false;
            char[] arr = str.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsChineseLetter(str, i) == false) return false;
            }
            return true;
        }

        /// <summary>
        /// 只能以英文或中文开头，允许英文、数字、下划线和中文；
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsAbcNumberAndChineseLetter(String str)
        {

            if (strUtil.IsNullOrEmpty(str)) return false;

            char[] arr = str.ToCharArray();
            if (isAbcAndChinese(arr[0]) == false) return false;

            for (int i = 0; i < arr.Length; i++)
            {
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890".IndexOf(arr[i]) >= 0) continue;
                if (IsChineseLetter(str, i) == false) return false;
            }
            return true;
        }

        private static Boolean isAbcAndChinese(char c)
        {
            if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c) >= 0) return true;
            if (IsChineseLetter(c.ToString(), 0) == true) return true;
            return false;
        }

        private static Boolean IsChineseLetter(String input, int index)
        {

            int chineseCharBegin = Convert.ToInt32(0x4e00);
            int chineseCharEnd = Convert.ToInt32(0x9fff);
            int code = Char.ConvertToUtf32(input, index);
            return (code >= chineseCharBegin && code <= chineseCharEnd);
        }

        /// <summary>
        /// 是否是有效的颜色值(3位或6位，全部由英文字符或数字组成)
        /// </summary>
        /// <param name="aColor"></param>
        /// <returns></returns>
        public static Boolean IsColorValue(String aColor)
        {
            if (strUtil.IsNullOrEmpty(aColor)) return false;
            String color = aColor.Trim().TrimStart('#').Trim();
            if (color.Length != 3 && color.Length != 6) return false;

            char[] arr = color.ToCharArray();
            foreach (char c in arr)
            {
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".IndexOf(c) < 0) return false;
            }

            return true;
        }


        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(String str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']");
        }
        /// <summary>
        /// 获取安全的Sql参数值
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>安全的Sql参数值</returns>
        public static string GetSafeSqlString(String str)
        {
            return Regex.Replace(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']", "");
        }
        public static string GetSafeSqlString(String str, Boolean tolower)
        {
            if (tolower)
                return Regex.Replace(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']", "").ToLower();
            else
                return Regex.Replace(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']", "");
        }
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            return Regex.IsMatch(str, @"^\d+$");
        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return System.Web.HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码类型，如（gb2312,utf-8,gbk），默认为utf-8</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str, string encoding)
        {
            Encoding encode = Encoding.UTF8;
            if (!string.IsNullOrEmpty(encoding))
            {
                encode = System.Text.Encoding.GetEncoding(encoding);
            }
            return System.Web.HttpUtility.UrlEncode(str, encode);
        }
        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return System.Web.HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        /// <param name="encoding">编码类型，如（gb2312,utf-8,gbk），默认为utf-8</param>
        public static string UrlDecode(string str, string encoding)
        {
            Encoding encode = Encoding.UTF8;
            if (!string.IsNullOrEmpty(encoding))
            {
                encode = System.Text.Encoding.GetEncoding(encoding);
            }
            return System.Web.HttpUtility.UrlDecode(str, encode);
        }
        static public string CreateRndStr(int length)
        {
            string valid = "0123456789";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }
        static public string CreateRndStrE(int length)
        {
            string valid = "0123456789abcdefghijklmnopqrstuvwxyz";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }
        /// <summary>
        /// 获取当前的时间戳       
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);// 以UTC时间为准的时间戳
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获取当前的时间戳       
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);// 以UTC时间为准的时间戳
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 用“,”并联一个字符串数组
        /// </summary>
        /// <param name="strS"></param>
        /// <returns></returns>
        public static String Join(String[] strS)
        {
            return Join(",", strS);
        }
        /// <summary>
        /// 并联一个字符串数组
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="strS"></param>
        /// <returns></returns>
        public static String Join(String separator, String[] strS)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (string str in strS)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                } sb.Append(str);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 用斜杠/拼接两个字符串
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static String Join(String strA, String strB)
        {
            return Join(strA, strB, "/");
        }

        /// <summary>
        /// 根据制定的分隔符拼接两个字符串
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static String Join(String strA, String strB, String separator)
        {
            return (Append(strA, separator) + TrimStart(strB, separator));
        }

        /// <summary>
        /// 剔除 html 中的 tag
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static String ParseHtml(Object html)
        {
            if (html == null) return String.Empty;
            return htmlReg.Replace(html.ToString(), "").Replace(" ", " ");
        }

        /// <summary>
        /// 剔除 html 中的 tag，并返回指定长度的字符串
        /// </summary>
        /// <param name="html"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static String ParseHtml(Object html, int count)
        {
            return CutString(ParseHtml(html), count).Replace("　", "");
        }

        /// <summary>
        /// 从 html 中截取指定长度的一段，并关闭未结束的 html 标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="count">需要截取的长度(小于20个字符按20个字符计算)</param>
        /// <returns></returns>
        public static String CutHtmlAndColse(String html, int count)
        {
            if (html == null) return "";
            html = html.Trim();
            if (count <= 0) return "";
            if (count < 20) count = 20;
            String unclosedHtml = html.Length <= count ? html : html.Trim().Substring(0, count);
            return CloseHtml(unclosedHtml);
        }

        /// <summary>
        /// 关闭未结束的 html 标签
        /// (TODO 本方法临时使用，待重写)
        /// </summary>
        /// <param name="unClosedHtml"></param>
        /// <returns></returns>
        public static String CloseHtml(String unClosedHtml)
        {
            if (unClosedHtml == null) return "";
            String[] arrTags = new String[] { "strong", "b", "i", "u", "em", "font", "span", "label", "pre", "td", "th", "tr", "tbody", "table", "li", "ul", "ol", "h1", "h2", "h3", "h4", "h5", "h6", "p", "div" };

            for (int i = 0; i < arrTags.Length; i++)
            {

                Regex re = new Regex("<" + arrTags[i] + "[^>]*>", RegexOptions.IgnoreCase);
                int openCount = re.Matches(unClosedHtml).Count;
                if (openCount == 0) continue;

                re = new Regex("</" + arrTags[i] + ">", RegexOptions.IgnoreCase);
                int closeCount = re.Matches(unClosedHtml).Count;

                int unClosedCount = openCount - closeCount;

                for (int k = 0; k < unClosedCount; k++)
                {
                    unClosedHtml += "</" + arrTags[i] + ">";
                }
            }

            return unClosedHtml;
        }

        /// <summary>
        /// 将字符串分割成数组
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static String[] Split(String srcString, String separator)
        {
            if (srcString == null) return null;
            if (separator == null) throw new ArgumentNullException();
            return srcString.Split(new String[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// 过滤掉 sql 语句中的单引号，并返回指定长度的结果
        /// </summary>
        /// <param name="rawSql"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static String SqlClean(String rawSql, int number)
        {
            if (IsNullOrEmpty(rawSql)) return rawSql;
            return SubString(rawSql, number).Replace("'", "''");
        }

        /// <summary>
        /// 从字符串中截取指定长度的一段，结果末尾没有省略号
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String SubString(String str, int length)
        {
            if (str == null) return null;
            if (str.Length > length) return str.Substring(0, length);
            return str;
        }

        /// <summary>
        /// 将纯文本中的换行符转换成html中换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String Text2Html(String str)
        {
            return str.Replace("\n", "<br/>");
        }

        /// <summary>
        /// 将html中换行符转换成纯文本中的换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String Html2Text(string str)
        {
            return strUtil.RemoveHtmlTag(strUtil.HtmlDecode(str).Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("</p><p>", "\r\n")).Trim();
        }
        /// <summary>
        /// 从 srcString 的末尾剔除掉 trimString
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static String TrimEnd(String srcString, String trimString)
        {
            if (strUtil.IsNullOrEmpty(trimString)) return srcString;
            if (srcString.EndsWith(trimString) == false) return srcString;
            if (srcString.Equals(trimString)) return "";
            return srcString.Substring(0, srcString.Length - trimString.Length);
        }

        /// <summary>
        /// 从 srcString 的开头剔除掉 trimString
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static String TrimStart(String srcString, String trimString)
        {
            if (srcString == null) return null;
            if (trimString == null) return srcString;
            if (strUtil.IsNullOrEmpty(srcString)) return String.Empty;
            if (srcString.StartsWith(trimString) == false) return srcString;
            return srcString.Substring(trimString.Length);
        }

        /// <summary>
        /// 将 html 中的脚本从各个部位，全部挪到页脚，以提高网页加载速度
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static String ResetScript(String html)
        {

            Regex reg = new Regex("<script.*?</script>", RegexOptions.Singleline);

            MatchCollection mlist = reg.Matches(html);
            StringBuilder sb = new StringBuilder();
            sb.Append(reg.Replace(html, ""));

            for (int i = 0; i < mlist.Count; i++)
            {
                sb.Append(mlist[i].Value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串分割成平均的n等份，每份长度为count
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<String> SplitByNum(String str, int count)
        {

            List<String> list = new List<string>();

            if (str == null) return list;
            if (str.Length == 0)
            {
                list.Add(str);
                return list;
            }

            if (count <= 0)
            {
                list.Add(str);
                return list;
            }

            int k = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {

                if (k == count)
                {
                    list.Add(sb.ToString());
                    k = 0;
                    sb = new StringBuilder();
                }

                sb.Append(str[i]);

                k++;
            }

            if (sb.Length > 0) list.Add(sb.ToString());

            return list;
        }

        /// <summary>
        /// 将 html 中空白字符和空白标记(&amp;nbsp;)剔除掉
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static String TrimHtml(String val)
        {

            if (val == null) return null;
            val = val.Trim();

            String text = ParseHtml(val);
            text = trimHtmlBlank(text);
            if (strUtil.IsNullOrEmpty(text) && hasNotImg(val) && hasNotFlash(val)) return "";

            val = trimHtmlBlank(val);
            return val;
        }

        private static String trimHtmlBlank(String text)
        {

            if (text == null) return null;
            text = text.Trim();

            if (text.StartsWith(htmlBlank) || text.EndsWith(htmlBlank))
            {
                while (true)
                {
                    text = strUtil.TrimStart(text, htmlBlank).Trim();
                    text = strUtil.TrimEnd(text, htmlBlank).Trim();
                    if (!text.StartsWith(htmlBlank) && !text.EndsWith(htmlBlank)) break;
                }
            }

            return text;
        }

        private static Boolean hasNotImg(String val)
        {
            if (val.ToLower().IndexOf("<img ") >= 0) return false;
            return true;
        }

        private static Boolean hasNotFlash(String val)
        {
            if (val.ToLower().IndexOf("x-shockwave-flash") >= 0) return false;
            return true;
        }

        private static readonly String htmlBlank = "&nbsp;";


        /// <summary>
        /// 截取字符串末尾的整数
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static int GetEndNumber(String rawString)
        {
            if (IsNullOrEmpty(rawString)) return 0;
            char[] chArray = rawString.ToCharArray();
            int startIndex = -1;
            for (int i = chArray.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(chArray[i])) break;
                startIndex = i;
            }
            if (startIndex == -1) return 0;
            return ConvertHelper.ToInt(rawString.Substring(startIndex));
        }
       
        /// <summary>
        /// 将Text字符串转换成HTML格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToHTML(string str)
        {
            if (str == null || str == string.Empty)
                return string.Empty;
            return str.Replace("\r\n", "<br>").Replace("\n", "<br>").Replace(" ", "&nbsp;");
        }

        public static string HtmlDecode(string str)
        {
            if (str == null || str == string.Empty) return string.Empty;
            return System.Web.HttpUtility.HtmlDecode(str);
        }
        public static string HtmlEncode(string str)
        {
            if (str == null || str == string.Empty) return string.Empty;
            return System.Web.HttpUtility.HtmlEncode(str);
        }
        /// <summary>
        ///  将Text字符串转换成javascript格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToJSString(string str)
        {
            if (str == null || str == string.Empty) return string.Empty;
            return str.Replace(@"\", @"\\").Replace("'", @"\'").Replace("\"", "\\\"").Replace("\r\n", ""); ;
        }
        /// <summary>
        /// 将阿拉伯数字转换成中文数字
        /// </summary>
        /// <param name="number"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ConvertToCHNNumber(int number, bool type)
        {
            string[] strNum = new string[] { "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            if (type) strNum = new string[] { "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string strN = number.ToString();
            int bl = -1;
            bool ch = true;
            int len = strN.Length;
            if (len > 24)
                throw new Exception("输入的数字过大，无法转换");
            string strResult = "";
            string[] strSZ = new string[len];
            for (int i = 0; i < len; i++)
            {
                strSZ[i] = strN.Substring(i, 1);
                if (!Regex.IsMatch(strSZ[i], "^[0-9]$"))
                    throw new Exception("输入的数字含有非数字符号");
                if (strSZ[0] == "0" && ch)//检验首位出现零的情况
                {
                    if (i != len - 1 && strSZ[i] == "0" && strSZ[i + 1] != "0")
                        bl = i;
                    else
                        ch = false;
                }
            }
            for (int i = 0; i < len; i++)
            {
                int num = len - i;
                if (strSZ[i] != "0")
                {
                    strResult += strNum[Convert.ToInt32(strSZ[i]) - 1];//将阿拉伯数字转换成中文大写数字                    
                    if (num % 4 == 2)
                        strResult += type ? "十" : "拾";//加上单位
                    if (num % 4 == 3)
                        strResult += type ? "百" : "佰";
                    if (num % 4 == 0)
                        strResult += type ? "千" : "仟";
                    if (num % 4 == 1)
                    {
                        if (num / 4 == 1)
                            strResult += type ? "万" : "萬";
                        if (num / 4 == 2)
                            strResult += type ? "亿" : "亿";
                        if (num / 4 == 3)
                            strResult += type ? "万" : "萬";
                        if (num / 4 == 4)
                            strResult += type ? "亿" : "亿";
                        if (num / 4 == 5)
                            strResult += type ? "万" : "萬";
                    }
                }
                else
                {
                    if (i > bl)
                    {
                        if ((i != len - 1 && strSZ[i + 1] != "0" && (num - 1) % 4 != 0))
                        {
                            //此处判断“0”不是出现在末尾，且下一位也不是“0”；
                            //如 10012332 在此处读法应该为壹仟零壹萬贰仟叁佰叁拾贰,两个零只要读一个零
                            strResult += "零";
                        }
                        if (i != len - 1 && strSZ[i + 1] != "0")
                        {
                            switch (num)
                            {
                                //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“萬”读成壹仟萬零贰仟叁佰叁拾贰
                                case 5: strResult += type ? "万" : "萬";
                                    break;
                                case 9: strResult += type ? "亿" : "亿";
                                    break;
                                case 13: strResult += type ? "万" : "萬";
                                    break;
                            }
                        }
                        if (i != len - 1 && strSZ[i + 1] != "0" && (num - 1) % 4 == 0)
                        {
                            //此处出现的情况是如 10002332，“0”出现在万位上就应该加上一个“零”读成壹仟萬零贰仟叁佰叁拾贰
                            strResult += "零";
                        }
                    }
                }
            }
            if (type && strResult.IndexOf("一十") == 0)
                strResult = strResult.Substring(1);
            return strResult;
        }
        public static string ConvertToCHNNumber(int number)
        {
            return ConvertToCHNNumber(number, false);
        }
        /// <summary>
        /// 按指定的长度截取字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        public static string Ellipsis(string s, int l, string endStr)
        {
            s = s.Trim();
            string temp = s.Substring(0, (s.Length < l + 1) ? s.Length : l + 1);
            byte[] encodedBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(temp);
            string outputStr = "";
            int count = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if ((int)encodedBytes[i] == 63)
                    count += 2;
                else
                    count += 1;

                if (count <= l - endStr.Length)
                    outputStr += temp.Substring(i, 1);
                else if (count > l)
                    break;
            }
            if (count <= l)
            {
                outputStr = temp;
                endStr = "";
            }
            outputStr += endStr;
            return outputStr;
        }
        /// <summary>
        /// 移除Html标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string html)
        {
            if (html != null && html != "")
            {
                html = Regex.Replace(html, "<[^>]*>", "");
                html = html.Replace("&nbsp;", " ");
            }
            return html;
        }
        /// <summary>
        /// 移除Html标签(可保留部分)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="holdTags">保留的 tag </param>
        /// <returns></returns>
        public static string RemoveHtmlTagWithHold(string html,params string[] holdTags)
        {
            if (html != null && html != "")
            {
                if (holdTags == null||holdTags.Length==0)
                {
                    holdTags = new string[] { "a", "img", "br", "strong", "b", "span", "li" };//保留的 tag 
                }
                // <(?!((/?\s?li\b)|(/?\s?ul\b)|(/?\s?a\b)|(/?\s?img\b)|(/?\s?br\b)|(/?\s?span\b)|(/?\s?b\b)))[^>]+> 
                string regStr = string.Format(@"<(?!((/?\s?{0})))[^>]+>", string.Join(@"\b)|(/?\s?", holdTags));
                Regex reg = new Regex(regStr, RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

                return reg.Replace(html, ""); 
            }
            return html;
        }
        /// <summary>
        /// 按字节长度截取字符串(支持截取带HTML代码样式的字符串)
        /// </summary>
        /// <param name="param">将要截取的字符串参数</param>
        /// <param name="length">截取的字节长度</param>
        /// <param name="end">字符串末尾补上的字符串</param>
        /// <returns>返回截取后的字符串</returns>
        public static string SubstringToHTML(string param, int length, string end)
        {
            string Pattern = null;
            MatchCollection m = null;
            StringBuilder result = new StringBuilder();
            int n = 0;
            char temp;
            bool isCode = false; //是不是HTML代码
            bool isHTML = false; //是不是HTML特殊字符,如&nbsp;
            char[] pchar = param.ToCharArray();
            for (int i = 0; i < pchar.Length; i++)
            {
                temp = pchar[i];
                if (temp == '<')
                    isCode = true;
                else if (temp == '&')
                    isHTML = true;
                else if (temp == '>' && isCode)
                {
                    n--; isCode = false;
                }
                else if (temp == ';' && isHTML)
                    isHTML = false;
                if (!isCode && !isHTML)
                {
                    n = n + 1;
                    if (System.Text.Encoding.Default.GetBytes(temp + "").Length > 1)
                        n = n + 1;//UNICODE码字符占两个字节 
                }
                result.Append(temp);
                if (n >= length)
                {
                    result.Append(end);
                    break;
                }
            }
            //取出截取字符串中的HTML标记
            string temp_result = result.ToString().Replace("(>)[^<>]*(<?)", "$1$2");
            //去掉不需要结素标记的HTML标记
            temp_result = temp_result.Replace(@"</?(AREA|BASE|BASEFONT|BODY|BR|COL|COLGROUP|DD|DT|FRAME|HEAD|HR|HTML|IMG|INPUT|ISINDEX|LI|LINK|META|OPTION|P|PARAM|TBODY|TD|TFOOT|TH|THEAD|TR|area|base|basefont|body|br|col|colgroup|dd|dt|frame|head|hr|html|img|input|isindex|li|link|meta|option|p|param|tbody|td|tfoot|th|thead|tr)[^<>]*/?>", "");
            //去掉成对的HTML标记
            temp_result = temp_result.Replace(@"<([a-zA-Z]+)[^<>]*>(.*?)</\1>", "$2");
            //用正则表达式取出标记
            Pattern = ("<([a-zA-Z]+)[^<>]*>");
            m = Regex.Matches(temp_result, Pattern);
            System.Collections.ArrayList endHTML = new System.Collections.ArrayList();
            foreach (Match mt in m)
            {
                endHTML.Add(mt.Result("$1"));
            }
            //补全不成对的HTML标记
            for (int i = endHTML.Count - 1; i >= 0; i--)
            {
                result.Append("</");
                result.Append(endHTML[i]);
                result.Append(">");
            }
            return result.ToString();
        }
        /// <summary>
        /// Unicode转换为字符
        /// </summary>
        /// <param name="strUnicode">Unicode编码</param>
        /// <returns>字符</returns>
        public static string GetStringByUnicode(string strUnicode)
        {
            string dst = "";
            string[] src = strUnicode.Split(new string[] { "\\u" }, Int32.MaxValue, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < src.Length; i++)
            {
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(src[i].Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(src[i].Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }
        /// <summary>
        /// 字符转换为Unicode编码
        /// </summary>
        /// <param name="strInput">字符</param>
        /// <returns>Unicode编码</returns>
        public static string GetUnicodeByString(string strInput)
        {
            char[] src = strInput.ToCharArray();
            string dst = "";
            for (int i = 0; i < src.Length; i++)
            {
                byte[] utext = System.Text.Encoding.Unicode.GetBytes(src[i].ToString());
                dst += @"\u" + utext[1].ToString("x2") + utext[0].ToString("x2");
            }
            return dst;
        }
        /// <summary>
        /// 汉字转换为拼音
        /// </summary>
        private static int[] pyValue = new int[]
    {
    -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
    -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
    -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
    -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
    -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
    -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
    -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
    -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
    -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
    -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
    -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
    -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
    -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
    -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
    -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
    -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
    -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
    -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
    -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
    -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
    -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
    -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
    -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
    -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
    -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
    -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
    -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
    -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
    -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
    -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
    -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
    -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
    -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
    };
        private static string[] pyName = new string[]
    {
    "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
    "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
    "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
    "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
    "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
    "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
    "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
    "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
    "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
    "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
    "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
    "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
    "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
    "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
    "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
    "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
    "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
    "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
    "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
    "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
    "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
    "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
    "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
    "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
    "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
    "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
    "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
    "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
    "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
    "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
    "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
    "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
    "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
    };
        /// <summary>
        /// 汉字转换为拼音(每个字的首字母大写)
        /// </summary>
        /// <param name="chrstr">输入的汉字</param>
        /// <returns>输出的拼音</returns>
        public static string Chs2Pinyin(string chrstr)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = chrstr.ToCharArray();
            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                        pyString += noWChar[j];
                    else
                    {
                        // 修正部分文字
                        switch (chrAsc)
                        {
                            case -24728: // 修正“焗”字
                                pyString += "Ju";
                                break;
                            default:
                                bool handle = true;
                                for (int i = (pyValue.Length - 1); i >= 0; i--)
                                {
                                    if (pyValue[i] <= chrAsc)
                                    {
                                        pyString += pyName[i];
                                        handle = false;
                                        break;
                                    }
                                }
                                if (handle)
                                {
                                    pyString += noWChar[j];
                                }
                                break;
                        }
                    }
                }
                else // 非中文字符
                    pyString += noWChar[j].ToString();
            }
            return pyString;
        }
        /// <summary>
        /// 汉字转换为拼音
        /// </summary>
        /// <param name="chrstr">输入的汉字</param>
        /// <returns>输出的拼音</returns>
        public static string Chs2PinyinSplit(string chrstr)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = chrstr.ToCharArray();
            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    if (!strUtil.IsNullOrEmpty(pyString))
                    {
                        pyString += " ";
                    }
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                        pyString += noWChar[j];
                    else
                    {
                        // 修正部分文字
                        switch (chrAsc)
                        {
                            case -24728: // 修正“焗”字
                                pyString += "Ju";
                                break;
                            default:
                                bool handle = true;
                                for (int i = (pyValue.Length - 1); i >= 0; i--)
                                {
                                    if (pyValue[i] <= chrAsc)
                                    {
                                        pyString += pyName[i];
                                        handle = false;
                                        break;
                                    }
                                }
                                if (handle)
                                {
                                    pyString += noWChar[j];
                                }
                                break;
                        }
                    }
                }
                else // 非中文字符
                    pyString += noWChar[j].ToString();
            }
            return pyString;
        }
        /// <summary>
        /// 依次取得字符串中每个字符的拼音首字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        static public string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = null;
            for (int i = 0; i < len; i++)
            {
                myStr += Chs2Pinyin(strText.Substring(i, 1)).Substring(0, 1);
            }
            return myStr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static byte[] GetByteByStr(string strText)
        {
            return System.Text.Encoding.Default.GetBytes(strText);
        }

        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = Encoding.Default.GetBytes(c);
            int i = (array[0] * 0x100) + array[1];
            if (i >= 0xb0a1)
            {
                if (i < 0xb0c5)
                    return "a";
                if (i < 0xb2c1)
                    return "b";
                if (i < 0xb4ee)
                    return "c";
                if (i < 0xb6ea)
                    return "d";
                if (i < 0xb7a2)
                    return "e";
                if (i < 0xb8c1)
                    return "f";
                if (i < 0xb9fe)
                    return "g";
                if (i < 0xbbf7)
                    return "h";
                if (i < 0xbfa6)
                    return "g";
                if (i < 0xc0ac)
                    return "k";
                if (i < 0xc2e8)
                    return "l";
                if (i < 0xc4c3)
                    return "m";
                if (i < 0xc5b6)
                    return "n";
                if (i < 0xc5be)
                    return "o";
                if (i < 0xc6da)
                    return "p";
                if (i < 0xc8bb)
                    return "q";
                if (i < 0xc8f6)
                    return "r";
                if (i < 0xcbfa)
                    return "s";
                if (i < 0xcdda)
                    return "t";
                if (i < 0xcef4)
                    return "w";
                if (i < 0xd1b9)
                    return "x";
                if (i < 0xd4d1)
                    return "y";
                if (i < 0xd7fa)
                    return "z";
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPYString(string str)
        {
            string tempStr = "";
            foreach (char c in str)
            {
                if ((c >= '!') && (c <= '~'))
                    tempStr = tempStr + c.ToString();
                else
                    tempStr = tempStr + GetPYChar(c.ToString());
            }
            return tempStr;
        }
        /// <summary>
        /// 获取字符串中的大写字母
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string GetOnlyUpper(string src)
        {
            string temp = "";
            foreach (char c in src)
            {
                int t = (int)c;
                if (t > 65 && t <= 90)
                {
                    temp += (char)t;
                }

            }
            return temp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetTokenCharCount(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            Regex seperatorReg = new Regex("[,;.!?'，。？：；‘’！“”—……、《》<>{}【】]", RegexOptions.IgnorePatternWhitespace);
            return seperatorReg.Matches(str).Count;
        }


        /// <summary>
        /// gb2312utf-8 转换 gb2312
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns> 
        public static string UTF8ToGB2312(string str)
        {
            try
            {
                Encoding utf8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");//Encoding.Default ,936
                byte[] temp = utf8.GetBytes(str);
                byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
                string result = gb2312.GetString(temp1);
                return result;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// gb2312 转换 utf-8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding utf8 = Encoding.UTF8;
                Encoding gb2312 = Encoding.GetEncoding("GB2312");
                byte[] unicodeBytes = gb2312.GetBytes(str);
                byte[] asciiBytes = Encoding.Convert(gb2312, utf8, unicodeBytes);
                char[] asciiChars = new char[utf8.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                utf8.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                string result = new string(asciiChars);
                return result;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 检测敏感词（自动过滤符号，只支持中文）
        /// </summary>
        /// <param name="content">需要检测的内容，如：超G强抗t△干dst△扰敏■■感※◇词 kljb过＆滤jb一■＆№正■№则匹◎←配代crSBtr码（只……支{持^中#^文）</param>
        /// <param name="blackwords">需要检测的关键字，如：(超强|抗干扰|敏感词|过滤|正则匹配|代码|只支持|中文)</param>
        /// <returns>检测到敏感词时，Result.IsValid为false，且Result.Errors为命中关键字列表</returns>
        public static Result CheckSensitiveWordsZhcn(string content, string blackwords)
		{
			return CheckSensitiveWordsZhcn(content, blackwords, 0);
		}
		/// <summary>
		/// 检测敏感词（自动过滤符号，只支持中文）
		/// </summary>
		/// <param name="content">需要检测的内容，如：超G强抗t△干dst△扰敏■■感※◇词 kljb过＆滤jb一■＆№正■№则匹◎←配代crSBtr码（只……支{持^中#^文）</param>
		/// <param name="blackwords">需要检测的关键字，如：(超强|抗干扰|敏感词|过滤|正则匹配|代码|只支持|中文)</param>
		/// <param name="times">最低命中次数</param>
		/// <returns>检测到敏感词时，Result.IsValid为false，且Result.Errors为命中关键字列表</returns>
		public static Result CheckSensitiveWordsZhcn(string content, string blackwords, int times)
		{
			return CheckSensitiveWords(System.Text.RegularExpressions.Regex.Replace(content, "[^\u4e00-\u9fa5]", ""), blackwords, times);
		}
		/// <summary>
		/// 检测敏感词（自动过滤符号）
		/// </summary>
		/// <param name="content">需要检测的内容</param>
		/// <param name="blackwords">需要检测的关键字</param>
		/// <returns>检测到敏感词时，Result.IsValid为false，且Result.Errors为命中关键字列表</returns>
		public static Result CheckSensitiveWords(string content, string blackwords)
		{
			return CheckSensitiveWords(content,blackwords,0);
		}
        /// <summary>
        /// 检测敏感词（自动过滤符号）
        /// </summary>
        /// <param name="content">需要检测的内容</param>
        /// <param name="blackwords">需要检测的关键字</param>
        /// <param name="times">最低命中次数</param>
        /// <returns>检测到敏感词时，Result.IsValid为false，且Result.Errors为命中关键字列表</returns>
        public static Result CheckSensitiveWords(string content, string blackwords, int times)
        {
            Result result = new Result();
            if (!string.IsNullOrEmpty(blackwords))
            {
                if (!blackwords.StartsWith("("))
                {
                    blackwords = "(" + blackwords;
                }
                if (!blackwords.EndsWith(")"))
                {
                    blackwords += ")";
                }
                MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(content, blackwords);
                if (matches.Count > times)
                {
                    IEnumerator ie = matches.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        result.Add(ie.Current.ToString());
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 根据完整的URL获取主域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetMainDomain(string url)
        {
            string host;
            Uri uri;
            try
            {
                uri = new Uri(url);
                host = uri.Host + "";
            }
            catch
            {
                return "";
            }

            string[] BeReplacedStrs = new string[] { ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".co.jp", ".gov.cn", ".co.uk", ".ac.cn", ".edu", ".tv", ".info", ".com", ".ac", ".ag", ".am", ".at", ".be", ".biz", ".bz", ".cc", ".cn", ".com", ".de", ".es", ".eu", ".fm", ".gs", ".hk", ".in", ".info", ".io", ".it", ".jp", ".la", ".md", ".ms", ".name", ".net", ".nl", ".nu", ".org", ".pl", ".ru", ".sc", ".se", ".sg", ".sh", ".tc", ".tk", ".tv", ".tw", ".us", ".co", ".uk", ".vc", ".vg", ".ws", ".il", ".li", ".nz" };

            string temp = "";
            foreach (string oneBeReplacedStr in BeReplacedStrs)
            {
                string BeReplacedStr = oneBeReplacedStr + "";
                if (host.EndsWith(BeReplacedStr))
                {
                    host = host.Substring(0, host.Length - BeReplacedStr.Length);
                    temp = BeReplacedStr;
                    break;
                }
            }
            int dotIndex = host.LastIndexOf(".");
            host = host.Substring(dotIndex + 1);
            return host + temp;
        }
        /// <summary>
        /// 根据完整的URL获取主域名(无域名后缀)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetMainDomainNoSuffix(string url)
        {
            if (url.IndexOf('.') > 0)
            {
                string host;
                try
                {
                    host = url;
                }
                catch
                {
                    return "";
                }

                string[] BeReplacedStrs = new string[] { ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".co.jp", ".gov.cn", ".co.uk", ".ac.cn", ".edu", ".tv", ".info", ".com", ".ac", ".ag", ".am", ".at", ".be", ".biz", ".bz", ".cc", ".cn", ".com", ".de", ".es", ".eu", ".fm", ".gs", ".hk", ".in", ".info", ".io", ".it", ".jp", ".la", ".md", ".ms", ".name", ".net", ".nl", ".nu", ".org", ".pl", ".ru", ".sc", ".se", ".sg", ".sh", ".tc", ".tk", ".tv", ".tw", ".us", ".co", ".uk", ".vc", ".vg", ".ws", ".il", ".li", ".nz" };

                string temp = "";
                foreach (string oneBeReplacedStr in BeReplacedStrs)
                {
                    string BeReplacedStr = oneBeReplacedStr + "";
                    if (host.EndsWith(BeReplacedStr))
                    {
                        host = host.Substring(0, host.Length - BeReplacedStr.Length);
                        //host = host.Replace(BeReplacedStr, string.Empty);
                        temp = BeReplacedStr;
                        break;
                    }
                }

                int dotIndex = host.LastIndexOf(".");
                host = host.Substring(dotIndex + 1);
                return host;
            }
            else
            {
                return url;
            }
        }
        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static System.Collections.Specialized.NameValueCollection GetQueryString(string queryString)
        {
            return GetQueryString(queryString, null, true);
        }

        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="encoding"></param>
        /// <param name="isEncoded"></param>
        /// <returns></returns>
        public static System.Collections.Specialized.NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            queryString = queryString.Replace("?", "");
            System.Collections.Specialized.NameValueCollection result = new System.Collections.Specialized.NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {
                        result[UrlDecode(key)] = UrlDecode(value);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }
    }
}

