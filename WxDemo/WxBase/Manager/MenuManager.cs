using OtNaf.ComLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxBase.Comm;
using WxBase.Utility;

namespace WxBase.Manager
{
    public class MenuManager
    {
        /// <summary>
        /// 菜单文件路径
        /// </summary>
        private static readonly string Menu_Data_Path = Path.Combine(OtCom.ConfigRoot, "menu.txt");
        /// <summary>
        /// 获取菜单
        /// </summary>
        public static string GetMenu()
        {
            string url = string.Format(Common.Cmd_GetMenu,WxToken.cur_wx_access_token.access_token);
            return HttpUtility.GetData(url);
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        public static string CreateMenu(string menutext)
        {
            string url = string.Format(Common.Cmd_CreateMenu, WxToken.cur_wx_access_token.access_token);
            return HttpUtility.SendHttpRequest(url, menutext);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public static string DeleteMenu()
        {
            string url = string.Format(Common.Cmd_DeleteMenu, WxToken.cur_wx_access_token.access_token);
            return HttpUtility.GetData(url);
        }
        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <returns></returns>
        public static string LoadMenu()
        {
            StreamReader sr = new StreamReader(Menu_Data_Path, Encoding.Default);
            String line;
            string str = "";
            while ((line = sr.ReadLine()) != null)
            {
                str += line.ToString();
            }

            return str;
        }
    }
}
