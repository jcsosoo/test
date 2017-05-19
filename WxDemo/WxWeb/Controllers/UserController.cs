using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WxBase.DAL;
using WxBase.Models;

namespace WxWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: YuanDian
        public ActionResult Index()
        {
            return View();
        }
        public string getXml(string requestXml)
        {
            return requestXml;
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        public bool RegisterUser(string UserName,string UserPassword,string Phone,string Address,string CardNum,string UserId)
        {
              WeChatUser WeChatUser = new WeChatUser {
                UserId = UserId,
                UserName = UserName,
                UserPassword= UserPassword,
                Phone= Phone,
                Address= Address,
                CardNum= CardNum
            };
            UserDAL  userDAL= new UserDAL();
             userDAL.AddUser(WeChatUser);
            return true;

        }
    }
}