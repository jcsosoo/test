using OtNaf.ComLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxBase.Comm;
using WxBase.Models;
using WxBase.Utility;

namespace WxBase.Manager
{
    public class WxToken
    {
        private static DateTime GetAccessToken_Time;

        private static wx_access_token _cur_wx_access_token;
        /// <summary>
        /// 当前的全局唯一接口调用凭据
        /// </summary>
        public static wx_access_token cur_wx_access_token
        {
            get
            {
                if (IsEffective() != "")
                {
                    GetToken();
                }
                return _cur_wx_access_token;
            }
        }

        public static string GetToken()
        {
            string url = string.Format(Common.Cmd_GetToken, Common.AppID, Common.AppSecret);

            string result = HttpUtility.GetData(url);
            wx_apperror wx_err = null;
            if (Common.IsErrorMsg(result, out wx_err))
            {
                return result;
            }

            _cur_wx_access_token = OtCom.FromJson<wx_access_token>(result);

            if (_cur_wx_access_token == null)
            {
                return result;
            }

            if (_cur_wx_access_token.expires_in <= 0)
            {
                GetAccessToken_Time = DateTime.MinValue;
            }
            else
            {
                GetAccessToken_Time = DateTime.Now;
            }

            return result;
        }
        
        /// <summary>
        /// 判断Access_token是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired()
        {
            if (GetAccessToken_Time != null && _cur_wx_access_token!=null && _cur_wx_access_token.expires_in>0)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > GetAccessToken_Time.AddSeconds(_cur_wx_access_token.expires_in).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 验证票据是否有效
        /// </summary>
        /// <returns></returns>
        public static string IsEffective()
        {
            if (_cur_wx_access_token == null || string.IsNullOrEmpty(_cur_wx_access_token.access_token) || HasExpired())
            {
                return "全局操作凭证无效或已过期";
            }
            return "";
        }
    }
}
