//------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\Core\API\PostAndGet.cs
//	运 行 库：2.0.50727.1882
//	代码功能：直接Post或Get参数辅助类
//	最后修改：2012年6月5日 23:35:52
//------------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using MySelfEntityMvc.UtilityTools.Serialization;
using System.Xml.Serialization;
using MySelfEntityMvc.UtilityTools.Data;
namespace MySelfEntityMvc.UtilityTools.Web
{
    public class PostAndGet
    {
        public static string PostWebRequest(string postUrl, string paramData, string encoding, bool jsonreq = false)
        {
            string ret = string.Empty;
            try
            {
                Encoding encode = Encoding.Default;
                if (!string.IsNullOrEmpty(encoding))
                {
                    encode = System.Text.Encoding.GetEncoding(encoding);
                }
                byte[] byteArray = encode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                //webReq.ContentType = "application/x-www-form-urlencoded";
                if (jsonreq)
                    webReq.ContentType = "application/json";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), encode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
            }
            return ret;
        }
        private const String LINE = "\r\n";
        private string api_key, secret, url;
        public PostAndGet(string api_key, string secret, string url)
        {
            this.api_key = api_key;
            this.secret = secret;
            this.url = url;
        }
        /// <summary>
        /// 应用程序的Secret密钥
        /// </summary>
        internal string Secret
        {
            get { return secret; }
            set { secret = value; }
        }
        /// <summary>
        /// 应用程序的ApiKey标识
        /// </summary>
        internal string ApiKey
        {
            get { return api_key; }
        }
        /// <summary>
        /// CloudDesk服务器地址
        /// </summary>
        internal string Url
        {
            get { return url; }
            set { url = value; }
        }
        public static T GetResponse<T>(string url, params KeyValue[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in parameters)
            {
                if (i > 0)
                    builder.Append("&");
                builder.Append(item.ToEncodedString());
                i++;
            }
            byte[] response_bytes = GetResponseBytes(url, builder.ToString(), "");
            string tem = System.Text.Encoding.UTF8.GetString(response_bytes);
            try
            {
                T response = JsonHelper.Deserialize<T>(tem);//反序列化为对象
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static String GetResponseString(string url, params KeyValue[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in parameters)
            {
                if (i > 0)
                    builder.Append("&");
                builder.Append(item.ToEncodedString());
                i++;
            }
            byte[] response_bytes = GetResponseBytes(url, builder.ToString(), "");
            return System.Text.Encoding.UTF8.GetString(response_bytes);
        }
        public static String GetResponseString(string url, string encoding, params KeyValue[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in parameters)
            {
                if (i > 0)
                    builder.Append("&");
                builder.Append(item.ToEncodedString());
                i++;
            }
            byte[] response_bytes = GetResponseBytes(url, builder.ToString(), encoding);
            return System.Text.Encoding.GetEncoding(encoding).GetString(response_bytes);
        }
        public T GetResponseObject<T>(string method_name, params KeyValue[] parameters)
        {
            KeyValue[] signed = Sign(method_name, parameters);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < signed.Length; i++)
            {
                if (i > 0)
                    builder.Append("&");
                builder.Append(signed[i].ToEncodedString());
            }
            byte[] response_bytes = GetResponseBytes(Url, builder.ToString(), "");
            string tem = System.Text.Encoding.UTF8.GetString(response_bytes);
            try
            {
                T response = JsonHelper.Deserialize<T>(tem);//反序列化为对象
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static byte[] GetResponseBytes(string apiUrl, string postData, string encoding)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Timeout = 20000;
            HttpWebResponse response = null;
            Encoding encode = Encoding.UTF8;
            if (!string.IsNullOrEmpty(encoding))
            {
                encode = System.Text.Encoding.GetEncoding(encoding);
            }
            try
            {
                StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postData);
                try
                {
                    if (swRequestWriter != null)
                        swRequestWriter.Close();
                }
                catch { }
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encode))
                {
                    return encode.GetBytes(reader.ReadToEnd());
                }
            }
            catch
            {
                return encode.GetBytes("对不起，Url地址无法访问！");
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        /// <summary>
        /// 根据参数列表生成请求的Url
        /// </summary>
        /// <param name="method_name">要调用的方法名</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>Url</returns>
        private string FormatGetUrl(string method_name, params KeyValue[] parameters)
        {
            KeyValue[] signed = Sign(method_name, parameters);
            StringBuilder builder = new StringBuilder(Url);
            for (int i = 0; i < signed.Length; i++)
            {
                if (i > 0)
                    builder.Append("&");
                builder.Append(signed[i].ToString());
            }
            return builder.ToString();
        }
        /// <summary>
        /// 生成包括Sig，Method，Api_key在内的全部的参数
        /// </summary>
        /// <param name="method_name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public KeyValue[] Sign(string method_name, KeyValue[] parameters)
        {
            List<KeyValue> list = new List<KeyValue>(parameters);
            list.Add(KeyValue.Create("method", method_name));   //指定调用的方法名
            list.Add(KeyValue.Create("api_key", api_key));      //api_key
            list.Add(KeyValue.Create("call_id", DateTools.GetNow().Ticks));//以时间戳作为call_id参数值
            list.Sort();
            StringBuilder values = new StringBuilder();
            foreach (KeyValue param in list)
            {
                if (!string.IsNullOrEmpty(param.Value))
                    values.Append(param.ToString());
            }
            values.Append(secret);          //指定Secret
            //生成sig
            byte[] md5_result = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(values.ToString()));
            StringBuilder sig_builder = new StringBuilder();
            foreach (byte b in md5_result)
            {
                sig_builder.Append(b.ToString("x2"));
            }
            list.Add(KeyValue.Create("sig", sig_builder.ToString()));
            return list.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string RemoveJsonNull(string json)
        {
            json = System.Text.RegularExpressions.Regex.Replace(json, @",""\w*"":null", string.Empty);
            json = System.Text.RegularExpressions.Regex.Replace(json, @"""\w*"":null,", string.Empty);
            json = System.Text.RegularExpressions.Regex.Replace(json, @"""\w*"":null", string.Empty);
            json = System.Text.RegularExpressions.Regex.Replace(json, @",""\w*"":0", string.Empty);
            json = System.Text.RegularExpressions.Regex.Replace(json, @"""\w*"":0,", string.Empty);
            return json;
        }
        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }
        /// <summary>
        /// php time()
        /// </summary>
        /// <returns></returns>
        public static long Time()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);
            return (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
        }
        /// <summary>
        /// 获取API提交的参数
        /// </summary>
        /// <param name="request">request对象</param>
        /// <returns>参数数组</returns>
        private KeyValue[] GetParamsFromRequest(HttpRequest request)
        {
            List<KeyValue> list = new List<KeyValue>();
            foreach (string key in request.QueryString.AllKeys)
            {
                list.Add(KeyValue.Create(key, request.QueryString[key]));
            }
            foreach (string key in request.Form.AllKeys)
            {
                list.Add(KeyValue.Create(key, request.Form[key]));
            }
            list.Sort();
            return list.ToArray();
        }
        /// <summary>
        /// 根据参数和密码生成签名字符串
        /// </summary>
        /// <param name="parameters">API参数</param>
        /// <param name="secret">密码</param>
        /// <returns>签名字符串</returns>
        private string GetSignature(KeyValue[] parameters, string secret)
        {
            StringBuilder values = new StringBuilder();
            foreach (KeyValue param in parameters)
            {
                if (param.Key == "sig" || string.IsNullOrEmpty(param.Value))
                    continue;
                values.Append(param.ToString());
            }
            values.Append(secret);
            byte[] md5_result = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(values.ToString()));
            StringBuilder sig_builder = new StringBuilder();
            foreach (byte b in md5_result)
            {
                sig_builder.Append(b.ToString("x2"));
            }
            return sig_builder.ToString();
        }
        /// <summary>
        /// 获取API数据同步传递的参数
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetQueryString()
        {
            KeyValue[] parameters = GetParamsFromRequest(HttpContext.Current.Request);
            string sig = GetSignature(parameters, this.secret);
            bool samesig = false;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (KeyValue dp in parameters)
            {
                if (dp.Key == "sig")
                {
                    if (dp.Value == sig)
                        samesig = true;
                    continue;
                }
                dict.Add(dp.Key, dp.Value);
            }
            if (samesig)
            {
                return dict;
            }
            return new Dictionary<string, string>();
        }
    }
}
