using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WxWeb.Controllers
{
    public class WxController : Controller
    {
        // GET: Wx
        public ActionResult index()
        {
            return View();
        }


        /// <summary>
        /// </summary>
        public string GetStr()
        {
            return "hello";
        }
    }
}