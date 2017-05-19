using OtNaf.ComLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WxBase.Comm;

namespace WxWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WxToken()
        {
            ViewBag.Message = "Your WxToken page.";

            return View();
        }
        /// <summary>
        /// 响应来自微信服务器的请求消息处理
        /// </summary>
        public void WxRequest()
        {
            try
            {
                HttpRequestBase request = HttpContext.Request;
                HttpResponseBase response = HttpContext.Response;
                WXService wxService = new WXService(request);
                string responseMsg = wxService.Response();
                response.Clear();
                response.Charset = "UTF-8";
                response.Write(responseMsg);
                response.End();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 获取操作的全局唯一凭据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetToken()
        {
            try
            {
                string msg = WxBase.Manager.WxToken.GetToken();
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}