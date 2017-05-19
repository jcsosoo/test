using OtNaf.ComLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WxBase.Manager;
using WxBase.Models;

namespace WxBase.Comm
{
    /// <summary>
    /// 公共功能
    /// </summary>
    public  class Common
    {
        /// <summary>
        /// 加密签名
        /// </summary>
        public const string SIGNATURE = "signature";
        /// <summary>
        /// 时间戳
        /// </summary>
        public const string TIMESTAMP = "timestamp";
        /// <summary>
        /// 随机数
        /// </summary>
        public const string NONCE = "nonce";
        /// <summary>
        /// 随机字符串
        /// </summary>
        public const string ECHOSTR = "echostr";

        /// <summary>
        /// 发送人
        /// </summary>
        public const string FROM_USERNAME = "FromUserName";
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public const string TO_USERNAME = "ToUserName";
        /// <summary>
        /// 消息内容
        /// </summary>
        public const string CONTENT = "Content";
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public const string CREATE_TIME = "CreateTime";

        public const string EVENT = "Event";

        public const string EVENT_KEY = "EventKey";

        public const string SCAN_TYPE = "ScanType";

        public const string SCAN_RESULT = "ScanResult";
        /// <summary>
        /// 消息类型
        /// </summary>
        public const string MSG_TYPE = "MsgType"; 
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public const string MSG_ID = "MsgId";
        /// <summary>
        /// 令牌 ACCESS_TOKEN
        /// </summary>
        public const string ACCESS_TOKEN= "access_token";
        /// <summary>
        /// 令牌 EXPIRES_IN
        /// </summary>
        public const string EXPIRES_IN = "expires_in";
        /// <summary>
        /// 令牌获取的时间
        /// </summary>
        public const string ACCESS_DATETIME = "access_datetime";

        private static string _TOKEN;
        /// <summary>
        /// 令牌
        /// </summary>
        public static string TOKEN
        {
            get
            {
                if(string.IsNullOrEmpty(_TOKEN))
                {
                    _TOKEN = OtCom.GetAppCfg("Token", "");
                }
                return _TOKEN;
            }

            set
            {
                _TOKEN = value;
            }
        }
        private static string _AppID;
        /// <summary>
        /// APP ID
        /// </summary>
        public static string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(_AppID))
                {
                    _AppID = OtCom.GetAppCfg("AppID", "");
                }
                return _AppID;
            }

            set
            {
                _AppID = value;
            }
        }
        private static string _AppSecret;
        /// <summary>
        /// 应用密钥
        /// </summary>
        public static string AppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(_AppSecret))
                {
                    _AppSecret = OtCom.GetAppCfg("AppSecret", "");
                }
                return _AppSecret;
            }

            set
            {
                _AppSecret = value;
            }
        }
        private static string _EncondingAESKey;
        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public static string EncondingAESKey
        {
            get
            {
                if (string.IsNullOrEmpty(_EncondingAESKey))
                {
                    _EncondingAESKey = OtCom.GetAppCfg("EncondingAESKey", "");
                }
                return _EncondingAESKey;
            }

            set
            {
                _EncondingAESKey = value;
            }
        }

        private static string _Cmd_GetToken;
        /// <summary>
        /// 获取Token命令
        /// </summary>
        public static string Cmd_GetToken
        {
            get
            {
                if (string.IsNullOrEmpty(_Cmd_GetToken))
                {
                    _Cmd_GetToken = OtCom.GetAppCfg("Cmd_GetToken", "");
                }
                return _Cmd_GetToken;
            }

            set
            {
                _Cmd_GetToken = value;
            }
        }


        private static string _Cmd_GetMenu;
        /// <summary>
        /// 获取菜单命令
        /// </summary>
        public static string Cmd_GetMenu
        {
            get
            {
                if (string.IsNullOrEmpty(_Cmd_GetMenu))
                {
                    _Cmd_GetMenu = OtCom.GetAppCfg("Cmd_GetMenu", "");
                }
                return _Cmd_GetMenu;
            }

            set
            {
                _Cmd_GetMenu = value;
            }
        }

        private static string _Cmd_CreateMenu;
        /// <summary>
        /// 创建菜单命令
        /// </summary>
        public static string Cmd_CreateMenu
        {
            get
            {
                if (string.IsNullOrEmpty(_Cmd_CreateMenu))
                {
                    _Cmd_CreateMenu = OtCom.GetAppCfg("Cmd_CreateMenu", "");
                }
                return _Cmd_CreateMenu;
            }

            set
            {
                _Cmd_CreateMenu = value;
            }
        }

        private static string _Cmd_DeleteMenu;
        /// <summary>
        /// 删除菜单命令
        /// </summary>
        public static string Cmd_DeleteMenu
        {
            get
            {
                if (string.IsNullOrEmpty(_Cmd_CreateMenu))
                {
                    _Cmd_DeleteMenu = OtCom.GetAppCfg("Cmd_DeleteMenu", "");
                }
                return _Cmd_DeleteMenu;
            }

            set
            {
                _Cmd_DeleteMenu = value;
            }
        }


        /// <summary>
        /// 得到当前时间（整型）（考虑时区）
        /// </summary>
        /// <returns></returns>
        public static string GetNowTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
            return a.ToString();
        }
        /// <summary>
        /// 读取请求对象的内容
        /// 只能读一次
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <returns>string</returns>
        public static string ReadRequest(HttpRequestBase request)
        {
            string reqStr = string.Empty;
            using (Stream s = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                {
                    reqStr = reader.ReadToEnd();
                }
            }

            return reqStr;
        }

        /// <summary>
        /// 判断POST消息是否为错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool IsErrorMsg(string msg,out wx_apperror wx_err)
        {
            wx_err = null;
            if (msg.IndexOf("errcode") != -1)
            {
                wx_err = OtCom.FromJson<wx_apperror>(msg);
                return true;
            }
            return false;
        }
    }
}
