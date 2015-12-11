using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySelfEntityMvc.UtilityTools.WechatMethod
{
    /// <summary>
    /// 微信所需的参数信息
    /// </summary>
    public class WeChatInfo
    {
        /// <summary>
        /// 微信公众平台AppId
        /// </summary>
        public readonly static string Appid = ConfigHelper.GetAppSettings("AppID");
        /// <summary>
        /// 微信公众平台Secret
        /// </summary>
        public readonly static string Secret = ConfigHelper.GetAppSettings("AppSecret");

        #region  微信支付用到的参数

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public static readonly string Mchid = ConfigHelper.GetAppSettings("Mchid");// "1252295901";
        /// <summary>
        /// 微信支付自主设置的ApiKey
        /// </summary>
        public readonly static string ApiKey = ConfigHelper.GetAppSettings("ApiKey");//"TH4YD2KIXCTQ6CQKD8VHOA3RSNUD9Z3B";

        /// <summary>
        /// 微信支付所需的证书路径
        /// </summary>
        public readonly static string CdaCertPath = ConfigHelper.GetAppSettings("CdaCertPath");

        #endregion


    }
}
