//------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\CMS.Model\News\NewsClass.cs
//	运 行 库：2.0.50727.1882
//	代码功能：栏目分类Model类
//	最后修改：2011年12月7日 23:35:52
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace MySelfEntityMvc.UtilityTools.Data
{
    /// <summary>
    /// KeyValue
    /// </summary>
    public class KeyValue
    {
        private String _Key;
        private Object _Value;
        private String _Description;
        private DateTime _UpdateTime;
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get { return _Key; } set { _Key = value; } }
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get
            {
                if (_Value is Array)
                    return ConvertArrayToString(_Value as Array);
                else
                    return _Value.ToString();
            }
            set
            {
                this._Value = value;
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get { return _Description; } set { _Description = value; } }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get { return _UpdateTime; } set { _UpdateTime = value; } }
       /// <summary>
       /// 
       /// </summary>
        public KeyValue()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValue(string key, Object value)
        {
            Key = key;
            _Value = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}={1}", Key, Value);
        }
        /// <summary>
        /// 参数值
        /// </summary>
        public void SetValue(object val)
        {
            this._Value = val;
        }
        /// <summary>
        /// 创建参数对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static KeyValue Create(string key, string value)
        {
            return new KeyValue(key, value);
        }
        /// <summary>
        /// 创建参数对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static KeyValue Create(string key, object value)
        {
            return new KeyValue(key, value);
        }
        /// <summary>
        /// 根据Key排列
        /// </summary>
        /// <param name="kv1"></param>
        /// <param name="kv2"></param>
        /// <returns></returns>
        public static int CompareByKey(KeyValue kv1, KeyValue kv2)
        {
            return String.Compare(kv1.Key, kv2.Key);
        }
        /// <summary>
        /// 根据Key倒序排列
        /// </summary>
        /// <param name="kv1"></param>
        /// <param name="kv2"></param>
        /// <returns></returns>
        public static int CompareByKeyDesc(KeyValue kv1, KeyValue kv2)
        {
            return String.Compare(kv2.Key, kv1.Key);
        }
        /// <summary>
        /// 根据Value排列
        /// </summary>
        /// <param name="kv1"></param>
        /// <param name="kv2"></param>
        /// <returns></returns>
        public static int CompareByValue(KeyValue kv1, KeyValue kv2)
        {
            return String.Compare(kv1.Value, kv2.Value);
        }
        /// <summary>
        /// 根据Value倒序排列
        /// </summary>
        /// <param name="kv1"></param>
        /// <param name="kv2"></param>
        /// <returns></returns>
        public static int CompareByValueDesc(KeyValue kv1, KeyValue kv2)
        {
            return String.Compare(kv2.Value, kv1.Value);
        }
        /// <summary>
        /// 根据Key进行比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (!(obj is KeyValue))
                return -1;
            return this._Key.CompareTo((obj as KeyValue)._Key);
        }
        /// <summary>
        /// 将数组转为字符串
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static string ConvertArrayToString(Array a)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0)
                    builder.Append(",");
                builder.Append(a.GetValue(i).ToString());
            }
            return builder.ToString();
        }
        /// <summary>
        /// 获取参数值
        /// </summary>
        public string EncodedValue
        {
            get
            {
                if (_Value is Array)
                    return System.Web.HttpUtility.UrlEncode(ConvertArrayToString(_Value as Array));
                else
                    return System.Web.HttpUtility.UrlEncode(_Value.ToString());
            }
        }
        /// <summary>
        /// 生成encode字符串
        /// </summary>
        /// <returns></returns>
        public string ToEncodedString()
        {
            return string.Format("{0}={1}", Key, EncodedValue);
        }


    }   
}