using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
namespace MySelfEntityMvc.UtilityTools
{

    public class NetTool
    {        //�õ����ص�ַ
        public static string GetGateway()
        {
            //���ص�ַ
            string strGateway = "";
            //��ȡ��������
            System.Net.NetworkInformation.NetworkInterface[] nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //��������
            foreach (var netWork in nics)
            {
                //����������IP����
                System.Net.NetworkInformation.IPInterfaceProperties ip = netWork.GetIPProperties();
                //��ȡ��IP���������
                System.Net.NetworkInformation.GatewayIPAddressInformationCollection gateways = ip.GatewayAddresses;
                foreach (var gateWay in gateways)
                {
                    //����ܹ�Pingͨ����
                    if (IsPingIP(gateWay.Address.ToString()))
                    {
                        //�õ����ص�ַ
                        strGateway = gateWay.Address.ToString();
                        //����ѭ��
                        break;
                    }
                }
                //����Ѿ��õ����ص�ַ
                if (strGateway.Length > 0)
                {
                    //����ѭ��
                    break;
                }
            }
            //�������ص�ַ
            return strGateway;
        }/// <summary>
        /// ����Pingָ��IP�Ƿ��ܹ�Pingͨ
        /// </summary>
        /// <param name="strIP">ָ��IP</param>
        /// <returns>true �� false ��</returns>
        public static bool IsPingIP(string strIP)
        {
            try
            {
                //����Ping����
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                //����Ping����ֵ
                System.Net.NetworkInformation.PingReply reply = ping.Send(strIP, 1000);
                //Pingͨ
                return true;
            }
            catch
            {
                //Pingʧ��
                return false;
            }
        }
    }

    public class Tool
    {
        /// <summary>
        /// ��ȡConfiger���ݣ��Ȳ���KeyValue���ݣ��ٲ���Web.Config��
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfiger(string key)
        {
            string str= System.Configuration.ConfigurationManager.AppSettings[key];            
            return str;
        }
    }

    /// <summary>
    /// �����������
    /// </summary>
    public class Rand
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="length">���ɳ���</param>
        /// <returns></returns>
        public static string Number(int length)
        {
            return Number(length, false);
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
                result += random.Next(10).ToString();
            return result;
        }
        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="length">���ɳ���</param>
        /// <returns></returns>
        public static string Str(int length)
        {
            return Str(length, false);
        }
        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTools.GetNow().Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="length">���ɳ���</param>
        /// <returns></returns>
        public static string Str_char(int length)
        {
            return Str_char(length, false);
        }
        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTools.GetNow().Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
    }

    public class DateTools
    {
        private static int timezone = 8;
        public static DateTime GetNow()
        {        
            return DateTime.UtcNow.AddHours(timezone);// ��UTCʱ��Ϊ׼��ʱ���
        }
        public static Int64 GetValidityNum()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// ��UTCʱ��Ϊ׼��ʱ���
        }
        public static Int64 GetValidityNum(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// ��UTCʱ��Ϊ׼��ʱ���
        }

        /// <summary>
        /// ��nuix�е����ڸ�ʽת�����������ڸ�ʽ��ǰ�ᴫ��ĸ�ʽ��ȷ
        /// </summary>
        /// <param name="timestampString">�����ʱ���</param>
        /// <returns></returns>
        public static String ConvertToWin(String timestampString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestampString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// ��nuix�е����ڸ�ʽת��������ʱ��
        /// </summary>
        /// <param name="timestamp">�����ʱ���</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.Add(new TimeSpan(timestamp * 10000000));
        }
        /// <summary>
        /// ��nuix�е����ڸ�ʽת��������ʱ��
        /// </summary>
        /// <param name="timestampString">�����ʱ���</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(String timestampString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestampString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// ����ǰ����ʱ��ת����unix����ʱ�����ʽ
        /// </summary>
        /// <returns>unixʱ��</returns>
        public static string ConvertToUnix()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow =DateTime.Now.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        /// <summary>
        /// ����ǰ����ʱ��ת����unix����ʱ�����ʽ
        /// </summary>
        /// <returns>unixʱ��</returns>
        public static long ConvertToUnixofLong()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = DateTime.Now.Subtract(dtStart);
            return toNow.Ticks / 10000000;
        }
        /// <summary>
        /// ������������ת����unix����ʱ�����ʽ
        /// </summary>
        /// <param name="datetime">��������ת���ɵ��ַ�����ʽ�磺yyyy-MM-dd HH:mm:ss</param>
        /// <returns>unixʱ��</returns>
        public static string ConvertToUnix(DateTime datetime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = datetime.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static String GetDayOfWeek(DateTime now)
        {
            switch (Convert.ToInt32(now.DayOfWeek))
            {
                case 0: return "����";
                case 1: return "��һ";
                case 2: return "�ܶ�";
                case 3: return "����";
                case 4: return "����";
                case 5: return "����";
                case 6: return "����";
            }
            return string.Empty;
        }
    }
}
