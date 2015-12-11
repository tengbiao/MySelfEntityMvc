using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MySelfEntityMvc.UtilityTools.Serialization
{
    public class JsonHelper
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            string str = "";
            try
            {
                str = JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                
            }
            return str;
        }

        /// <summary>
        /// 将json字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string str)
        {
            try
            {
                T t = (T)JsonConvert.DeserializeObject(str, typeof(T));
                return t;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 获取键值对中某一字段的值
        /// </summary>
        /// <param name="map">键值对</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public static Object GetField(Dictionary<String, object> map, String field)
        {
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取键值对中某一字段的值
        /// </summary>
        /// <param name="map">键值对</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public static String GetFieldStr(Dictionary<String, object> map, String field)
        {
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value.ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// 获取 json 字符串中某一字段的值
        /// </summary>
        /// <param name="oneJsonString">json 字符串</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public static Object GetField(String oneJsonString, String field)
        {

            Dictionary<String, object> map = JsonHelper.Deserialize<Dictionary<String, object>>(oneJsonString);
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取 json 字符串中某一字段的值
        /// </summary>
        /// <param name="oneJsonString">json 字符串</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public static String GetFieldStr(String oneJsonString, String field)
        {

            Dictionary<String, object> map = JsonHelper.Deserialize<Dictionary<String, object>>(oneJsonString);
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value.ToString();
                }
            }
            return null;
        }
    }
}
