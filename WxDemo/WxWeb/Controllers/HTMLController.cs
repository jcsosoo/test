using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using WxBase.Comm;
using WxBase.Manager;

namespace WxWeb.Controllers
{
    public class HTMLController : Controller
    {
        // GET: Images
        public ActionResult test()
        {
            return View();
        }
        public ActionResult test01()
        {
            return View();
        }
        public ActionResult test02()
        {
            return View();
        }
        public ActionResult test03()
        {
            return View();
        }
        public ActionResult test04()
        {
            return View();
        }
        public ActionResult test05()
        {
            return View();
        }
        public ActionResult test06()
        {
            return View();
        }
        public ActionResult test07()
        {
            return View();
        }
        public ActionResult test08()
        {
            return View();
        }
        public ActionResult test10()
        {
            return View();
        }
        public static string ticket;
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="url">当前网址</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <returns></returns>
        public JsonResult GetMassages(string url,string nonceStr)
        {
            try
            {
                if (ticket==null || ticket == "")
                {
                    JsonResult str = jsapiTicket();
                    SDK jsapi_ticket = OtNaf.ComLib.OtCom.FromJson<SDK>(str.Data.ToString());
                    ticket = jsapi_ticket.ticket;
                }
                string timestamp = GetTimeStamp();
                string noncestr = nonceStr;
                string string1 = "jsapi_ticket=" + ticket +
                                 "&noncestr="+ noncestr +
                                 "&timestamp="+ timestamp +
                                 "&url="+url;
                string appid = Common.AppID;
                string signature = sha1(string1);
                CONFIG list = new CONFIG();
                list.ticket = ticket;
                list.appid = appid;
                list.signature = signature;
                list.timestamp = timestamp;
                list.noncestr = noncestr;
                //System.Web.Security.FormAuthentication.HashPasswordForStoringInConfigFile("", "SHA1");
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public class SDK
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }
          
        }
        public class CONFIG
        {
            public string ticket { get; set; }
            public string appid { get; set; }
            public string signature { get; set; }
            public string timestamp { get; set; }
            public string noncestr { get; set; }

        }
        /// <summary>
        /// 获取ticket
        /// </summary>
        public JsonResult jsapiTicket()
        {
            try
            {
                string Access_Token = WxToken.cur_wx_access_token.access_token;
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + Access_Token + "&type=jsapi";
                return Json(WxBase.Utility.HttpUtility.GetData(url), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <SUMMARY> 
        /// 下载保存多媒体文件
        /// </SUMMARY> 
        /// <PARAM name="MEDIA_ID">微信服务器端图片ID</PARAM> 
        /// <RETURNS></RETURNS> 
        public string GetMultimedia(string MEDIA_ID)
        {
            string str = "";
            string ACCESS_TOKEN = WxToken.cur_wx_access_token.access_token;
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string stUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                strpath = myResponse.ResponseUri.ToString();
                WebClient mywebclient = new WebClient();
                savepath = Server.MapPath("~/image") + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                try
                {
                    mywebclient.DownloadFile(strpath, savepath);
                    file = savepath;
                    str = "上传成功";
                }
                catch (Exception ex)
                {
                    //savepath = ex.ToString();
                    str = ex.Message.ToString();
                }

            }
            return str;
        }

        /// <summary>
        /// 签名加密
        /// </summary>
        public string sha1(string mystr)
        {
            try
            {
                //建立SHA1对象
                SHA1 sha = new SHA1CryptoServiceProvider();
                //将mystr转换成byte[]
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(mystr);
                //Hash运算
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                //将运算结果转换成string
                string hash = BitConverter.ToString(dataHashed).Replace("-", "");
                return hash;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}