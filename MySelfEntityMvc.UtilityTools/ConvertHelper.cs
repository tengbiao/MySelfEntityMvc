//------------------------------------------------------------------------------
//	�ļ����ƣ�System\cvt.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
namespace MySelfEntityMvc.UtilityTools
{
    /// <summary>
    /// ��ͬ����֮����ֵת��
    /// </summary>
    public partial class ConvertHelper
    {
        /// <summary>
        /// �ж��ַ����Ƿ���С��������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsDecimal( String str ) {
            if (strUtil.IsNullOrEmpty( str ))
                return false;
            if (str.StartsWith( "-" ))
                return isDecimal_private( str.TrimStart( '-' ) );
            else
                return isDecimal_private( str );
        }
        private static Boolean isDecimal_private( String str ) {
            foreach (char ch in str.ToCharArray()) {
                if (!(char.IsDigit( ch ) || (ch == '.')))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// �ж��ַ����Ƿ��Ƕ���������б�����֮�����ͨ��Ӣ�Ķ��ŷָ�
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Boolean IsIdListValid( String ids ) {
            if (strUtil.IsNullOrEmpty( ids )) {
                return false;
            }
            String[] strArray = ids.Split( new char[] { ',' } );
            foreach (String str in strArray) {
                if (!IsInt( str )) {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// �ж��ַ����Ƿ�������������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsInt( String str ) {
            if (strUtil.IsNullOrEmpty( str ))
                return false;
            if (str.StartsWith( "-" ))
                str = str.Substring( 1, str.Length - 1 );
            if (str.Length > 10)
                return false;
            char[] chArray = str.ToCharArray();
            foreach (char ch in chArray) {
                if (!char.IsDigit( ch )) 
                    return false;
            }
            if (chArray.Length == 10) {
                int charInt;
                Int32.TryParse( chArray[0].ToString(), out charInt );
                if (charInt > 2)
                    return false;
                int charInt2;
                Int32.TryParse( chArray[1].ToString(), out charInt2 );
                if ((charInt == 2) && (charInt2 > 0))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// �ж��ַ����Ƿ���"true"��"false"(�����ִ�Сд)
        /// </summary>
        /// <param name="str"></param>
        /// <returns>ֻ���ַ�����"true"��"false"(�����ִ�Сд)ʱ���ŷ���true</returns>
        public static Boolean IsBool( String str ) {
            if (str == null) return false;
            if (strUtil.EqualsIgnoreCase( str, "true" ) || strUtil.EqualsIgnoreCase( str, "false" )) return true;
            return false;
        }
        /// <summary>
        /// ������ת����Ŀ������
        /// </summary>
        /// <param name="val"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public static Object To( Object val, Type destinationType ) {
            return Convert.ChangeType( val, destinationType );
        }
        /// <summary>
        /// ������ת���� Boolean ���͡�ֻ�в�������1ʱ���ŷ���true
        /// </summary>
        /// <param name="integer"></param>
        /// <returns>ֻ�в�������1ʱ���ŷ���true</returns>
        public static Boolean ToBool( int integer ) {
            return (integer == 1);
        }
        /// <summary>
        /// ������ת���� Boolean ���͡�ֻ�ж�����ַ�����ʽ����1����true(�����ִ�Сд)ʱ���ŷ���true
        /// </summary>
        /// <param name="objBool"></param>
        /// <returns>ֻ�ж�����ַ�����ʽ����1����true(�����ִ�Сд)ʱ���ŷ���true</returns>
        public static Boolean ToBool( Object objBool ) {
            if (objBool == null) {
                return false;
            }
            String str = objBool.ToString();
            return (str.Equals( "1" ) || str.ToUpper().Equals( "TRUE" ));
        }
        /// <summary>
        /// ���ַ���(�����ִ�Сд)ת���� Boolean ���͡�ֻ���ַ�������1����trueʱ���ŷ���true
        /// </summary>
        /// <param name="str"></param>
        /// <returns>ֻ���ַ�������1����trueʱ���ŷ���true</returns>
        public static Boolean ToBool( String str ) {
            if (str == null)
                return false;
            if (str.ToUpper().Equals( "TRUE" ))
                return true;
            if (str.ToUpper().Equals( "FALSE" ))
                return false;
            return (str.Equals( "1" ) || str.ToUpper().Equals( "TRUE" ));
        }
        /// <summary>
        /// ���ַ���ת���� System.Decimal ���͡����str����������С��������0
        /// </summary>
        /// <param name="str"></param>
        /// <returns>���str����������С��������0</returns>
        public static decimal ToDecimal( String str ) {
            if (!IsDecimal( str ))
                return 0;
            return Convert.ToDecimal( str );
        }
        /// <summary>
        /// ���ַ���ת���� System.Double ���͡����str����������С��������0
        /// </summary>
        /// <param name="str"></param>
        /// <returns>���str����������С��������0</returns>
        public static Double ToDouble( String str ) {
            if (!IsDecimal( str ))
                return 0;
            return Convert.ToDouble( str );
        }
        /// <summary>
        /// ���ַ���ת���� System.Decimal ���͡����str����������С�������ز��� defaultValue ָ����ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal( String str, decimal defaultValue ) {
            if (!IsDecimal( str ))
                return defaultValue;
            return Convert.ToDecimal( str );
        }
        /// <summary>
        /// ������ת������������������������򷵻�0
        /// </summary>
        /// <param name="objInt"></param>
        /// <returns>��������������򷵻�0</returns>
        public static int ToInt( Object objInt ) {
            if ((objInt != null) && IsInt( objInt.ToString() )) {
                int result;
                Int32.TryParse( objInt.ToString(), out result );
                return result;
            }
            return 0;
        }
        /// <summary>
        /// �� decimal ת��������
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int ToInt( decimal number ) {
            return Convert.ToInt32( number );
        }
        /// <summary>
        /// ������ת���ɷ�Null��ʽ���������Ĳ����� null���򷵻ؿ��ַ���(��""��Ҳ��string.Empty)
        /// </summary>
        /// <param name="str"></param>
        /// <returns>���Ϊnull���򷵻ؿ��ַ���(��""��Ҳ��string.Empty)</returns>
        public static String ToNotNull( Object str ) {
            if (str == null)
                return "";
            return str.ToString();
        }
        /// <summary>
        /// ������ת���� DateTime ��ʽ����������ϸ�ʽ���򷵻ص�ǰʱ��
        /// </summary>
        /// <param name="objTime"></param>
        /// <returns>��������ϸ�ʽ���򷵻ص�ǰʱ��</returns>
        public static DateTime ToTime( Object objTime ) {
            return ToTime( objTime, DateTime.Now );
        }
        /// <summary>
        /// ������ת���� DateTime ��ʽ����������ϸ�ʽ���򷵻صڶ�������ָ����ʱ��
        /// </summary>
        /// <param name="objTime"></param>
        /// <param name="targetTime"></param>
        /// <returns></returns>
        public static DateTime ToTime( Object objTime, DateTime targetTime ) {
            if (objTime == null)
                return targetTime;
            try {
                return Convert.ToDateTime( objTime );
            }
            catch {
                return targetTime;
            }
        }
        /// <summary>
        /// �ж�����ʱ��������Ƿ���ͬ(Ҫ��ͬ��ͬ��ͬ��)
        /// </summary>
        /// <param name="day1"></param>
        /// <param name="day2"></param>
        /// <returns></returns>
        public static Boolean IsDayEqual( DateTime day1, DateTime day2 ) {
            return (day1.Year == day2.Year && day1.Month == day2.Month && day1.Day == day2.Day);
        }
       
        /// <summary>
        /// ��ȡʱ���Ӣ�ı����ʽ����ʽ�� {Monday, November 12, 2012}
        /// </summary>
        /// <param name="t"></param>
        /// <returns>��ʽ�� {Monday, November 12, 2012}</returns>
        public static String ToDateEnString(DateTime t)
        {
            System.Globalization.CultureInfo c = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            return t.ToString("D", c.DateTimeFormat);
        }
        /// <summary>
        /// ��ȡʱ���Ӣ�ı����ʽ����ʽ�� {Apr 07,2012}
        /// </summary>
        /// <param name="t"></param>
        /// <returns>��ʽ�� {Apr 07,2012}</returns>
        public static String ToDateEnShortString(DateTime t)
        {
            System.Globalization.CultureInfo c = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            string d = t.ToString("r", c.DateTimeFormat);
            d = d.Remove(d.IndexOf(':') - 2);
            return d;
        }
        /// <summary>
        /// ��ȡʱ���Ӣ�ı����ʽ����ʽ�� {Mon, 12 Nov 2012 00:00:00 GMT}
        /// </summary>
        /// <param name="t"></param>
        /// <returns>��ʽ�� {Mon, 12 Nov 2012 00:00:00 GMT}</returns>
        public static String ToDateEnLongString(DateTime t)
        {
            System.Globalization.CultureInfo c = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            return t.ToString("r", c.DateTimeFormat);
        }
        /// <summary>
        /// ������ת�����ַ�����ʽ���������֮����Ӣ�Ķ��ŷָ�
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static String ToString( int[] ids ) {
            if (ids == null || ids.Length == 0) return "";
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < ids.Length; i++) {
                builder.Append( ids[i] );
                if (i < ids.Length - 1) builder.Append( ',' );
            }
            return builder.ToString();
        }
        /// <summary>
        /// ���ַ�����ʽ�� id �б�ת������������
        /// </summary>
        /// <param name="myids"></param>
        /// <returns></returns>
        public static int[] ToIntArray( String myids ) {
            if (strUtil.IsNullOrEmpty( myids )) return new int[] { };
            String[] arrIds = myids.Split( ',' );
            int[] Ids = new int[arrIds.Length];
            for (int i = 0; i < arrIds.Length; i++) {
                int oneID = ToInt( arrIds[i].Trim() );
                Ids[i] = oneID;
            }
            return Ids;
        }
        /// <summary>
        /// ���ַ���ת�����Ծ��ſ�ͷ�ı����ʽ�����������Ч����ɫֵ���򷵻�null
        /// </summary>
        /// <param name="val"></param>
        /// <returns>���ַ���ת�����Ծ��ſ�ͷ�ı����ʽ�����������Ч����ɫֵ���򷵻�null</returns>
        public static String ToColorValue( String val ) {
            if (strUtil.IsColorValue( val ) == false) return null;
            if (val.StartsWith( "#" )) return val;
            return "#" + val;
        }
        /// <summary>
        /// ��10��������ת��Ϊ62����
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns>62������</returns>
        public static String ToBase62( Int64 inputNum ) {
            String chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return ToBase( inputNum, chars );
        }
        /// <summary>
        /// ��10��������ת��Ϊn����
        /// </summary>
        /// <param name="inputNum">10��������</param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static String ToBase( Int64 inputNum, String chars ) {
            int cbase = chars.Length;
            int imod;
            String result = "";
            while (inputNum >= cbase) {
                imod = (int)(inputNum % cbase);
                result = chars[imod] + result;
                inputNum = inputNum / cbase;
            }
            return chars[(int)inputNum] + result;
        }
        /// <summary>
        /// ��62����ת��Ϊ10��������
        /// </summary>
        /// <param name="str">62������</param>
        /// <returns>10��������</returns>
        public static Int64 DeBase62( String str ) {
            String chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return DeBase( str, chars );
        }
        /// <summary>
        /// ��n����ת��Ϊ10��������
        /// </summary>
        /// <param name="str">��Ҫת����n������</param>
        /// <param name="chars"></param>
        /// <returns>10��������</returns>
        public static Int64 DeBase( String str, String chars ) {
            int cbase = chars.Length;
            Int64 result = 0;
            for (int i = 0; i < str.Length; i++) {
                int index = chars.IndexOf( str[i] );
                result += index * (Int64)Math.Pow( cbase, (str.Length - i - 1) );
            }
            return result;
        }
        /// <summary>
        /// ���������л�Ϊ xml (�ڲ����� .net ����Դ��� XmlSerializer)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToXML(Object obj)
        {           
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            TextWriter textWriter = new StringWriter(sb);
            serializer.Serialize(textWriter, obj);
            textWriter.Close();
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="target"></param>
        public static void SaveToXml(String filePath, Object target)
        {
            XmlSerializer serializer = new XmlSerializer(target.GetType());
            TextWriter textWriter = new StreamWriter(filePath);
            serializer.Serialize(textWriter, target);
            textWriter.Close();
        }
    }
}