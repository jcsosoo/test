using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WxBase.Comm;
using WxBase.Manager;

namespace WxWeb.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult test()
        {
            return View();
        }

        /// <summary>
        /// 换取openid
        /// </summary>
        public JsonResult getOpenId(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code)) {
                    return null;
                }           
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid="+ Common.AppID +"&secret="+ Common.AppSecret +"&code="+ code + "&grant_type=authorization_code";
                return Json(WxBase.Utility.HttpUtility.GetData(url), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}