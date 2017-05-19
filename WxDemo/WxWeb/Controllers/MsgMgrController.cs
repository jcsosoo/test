using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxBase.Comm;
using System.Web.Mvc;
using WxBase.Manager;
using WxBase.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WxWeb.Controllers
{
    public class MsgMgrController : Controller
    {
        // GET: MsgMgr
        public ActionResult MessageManager()
        {
            return View();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        public JsonResult getYh()
        {
            try
            {
                string Access_Token = WxToken.cur_wx_access_token.access_token;
                string url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token="+ Access_Token + "&next_openid=";             
                //JsonConvert.SerializeObject(WxBase.Utility.HttpUtility.GetData(url));
                return Json(WxBase.Utility.HttpUtility.GetData(url), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 发信息
        /// </summary>
        public JsonResult sendMsg(string text)
        {
            try
            {
                string Access_Token = WxToken.cur_wx_access_token.access_token;
                string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_Token;
                return Json(WxBase.Utility.HttpUtility.SendHttpRequest(url, text), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        public JsonResult getId(string file)
        {
            try
            {
                string Access_Token = WxToken.cur_wx_access_token.access_token;
                string url = "http://api.weixin.qq.com/cgi-bin/material/add_material?access_token=" + Access_Token+ "&type=image";
                return Json(WxBase.Utility.HttpUtility.SendHttpRequest(url, file), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 群发信息
        /// </summary>
        public JsonResult sendAllMsg(string text)
        {
            try
            {
                string Access_Token = WxToken.cur_wx_access_token.access_token;
                string url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + Access_Token;
                return Json(WxBase.Utility.HttpUtility.SendHttpRequest(url, text), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}