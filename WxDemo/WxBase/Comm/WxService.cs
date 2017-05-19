using OtNaf.ComLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxBase.Handlers;
using WxBase.Utility;

namespace WxBase.Comm
{
    public class WXService
    {
        /// <summary>
        /// 
        /// </summary>
        private HttpRequestBase Request { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request"></param>
        public WXService(HttpRequestBase request)
        {
            this.Request = request;
        }
        /// <summary>
        /// 处理请求，产生响应
        /// </summary>
        /// <returns></returns>
        public string Response()
        {
            string method = Request.HttpMethod.ToUpper();
            //验证签名
            if (method == "GET")
            {
                if (CheckSignature())
                {
                    return Request.QueryString[Common.ECHOSTR];
                }
                else
                {
                    OtCom.XLogErr("error:" + Request.Url.AbsoluteUri);
                    return "签名验证失败";
                }
            }

            //处理消息
            if (method == "POST")
            {
                return ResponseMsg();
            }
            else
            {
                OtCom.XLogErr("无法处理:" + Request.Url.AbsoluteUri);
                return "无法处理";
            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        private string ResponseMsg()
        {
            string requestXml = Common.ReadRequest(this.Request);
            OtCom.XLogInfo(requestXml);
            IHandler handler = HandlerFactory.CreateHandler(requestXml);
            if (handler != null)
            {
                return handler.HandleRequest();
            }

            return string.Empty;
        }
        /// <summary>
        /// 检查签名
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString[Common.SIGNATURE];
            string timestamp = Request.QueryString[Common.TIMESTAMP];
            string nonce = Request.QueryString[Common.NONCE];

            List<string> list = new List<string>();
            list.Add(Common.TOKEN);
            list.Add(timestamp);
            list.Add(nonce);
            //排序
            list.Sort();
            //拼串
            string input = string.Empty;
            foreach (var item in list)
            {
                input += item;
            }
            //加密
            string new_signature = SecurityUtility.SHA1Encrypt(input);
            string msg = string.Format("token is {0}|| signature is {1}||timestamp is {2}||nonce is {3}||input is {4}", Common.TOKEN, signature, timestamp, nonce, input);
            //验证
            if (new_signature == signature)
            {
                OtCom.XLogInfo("验证签名成功：{0}", msg);
                return true;
            }
            else
            {
                OtCom.XLogErr("验证签名失败：{0}", msg);
                return false;
            }
        }
    }
}