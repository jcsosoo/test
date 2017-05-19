using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WxBase;

namespace WxWeb.Controllers
{
    public class MenuMgrController : Controller
    {
        public ActionResult MenuManager()
        {
            ViewBag.Message = "Your MenuManager page.";

            return View();
        }


        /// <summary>
        /// 获取菜单
        /// </summary>
        public JsonResult GetMenu()
        {
            try
            {
                string str = WxBase.Manager.MenuManager.GetMenu();
                return Json(str, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        public JsonResult CreateMenu(string menutext)
        {
            try
            {
                string str = WxBase.Manager.MenuManager.CreateMenu(menutext);
                return Json(str, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public JsonResult DeleteMenu()
        {
            try
            {
                string str = WxBase.Manager.MenuManager.DeleteMenu();
                return Json(str, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
