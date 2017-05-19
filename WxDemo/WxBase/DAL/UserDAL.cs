using OtNaf.ComLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxBase.Models;

namespace WxBase.DAL
{
   public class UserDAL
    {
        /// <summary>
        /// 数据库服务代理
        /// </summary>
        private OtDB myDb = null;
        public UserDAL()
        {
            myDb= new OtDB("WXGL");
        }
       #region  用户管理
        /// <summary>
        ///注册用户
        /// </summary>
        /// <param name="jmspxx"></param>
        /// <returns></returns>
        public bool AddUser(WeChatUser WeChatUser)
        {

            try
            {
               
                myDb.Begin();
               int rtnInfo = myDb.ExecInsert(WeChatUser, "WeChatUser",
                                   new string[]
                                   {
                        "UserId",
                        "UserName",
                        "UserPassword",
                        "CardNum",
                        "Phone",
                        "Address"
                                   }
                                   );
                myDb.Commit();
                return rtnInfo > 0 ? true : false;
            }
            catch (Exception ex)
            {
                myDb.Rollback();
                throw new Exception(ex.Message, ex);
            }

        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="WeChatUser"></param>
        /// <returns></returns>
        public bool UpdateUser(WeChatUser WeChatUser)
        {

            try
            {
                myDb.Begin();
                int rtnInfo = myDb.ExecUpdate(WeChatUser, "WeChatUser",
                  new string[]
                  { "UserID",
                        "UserName",
                        "UserPassword",
                        "CardNum",
                        "Phone",
                        "Address"
                    }, "UserId=@UserId", WeChatUser.UserId
                  );
                myDb.Commit();
                return rtnInfo > 0 ? true : false;
             }
            catch (Exception)
            {
                myDb.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
     public bool DelUser(string UserId)
        {
            try
            {
                myDb.Begin();
                string strSql = "Delete From WeChatUser where UserId = @UserId ";
                int rtnInfo = myDb.Exec(strSql, UserId);
                myDb.Commit();
                return true;
            }
            catch (Exception)
            {
                myDb.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool selectUser(string UserId)
        {
            try
            {
                bool msg = false;
                if (string.IsNullOrEmpty(UserId)) {
                    return msg;
                }
                myDb.Begin();
                string strSql = "Select From WeChatUser where UserId = @UserId ";
                WXYH yhObj = myDb.QueryOneRow<WXYH>(strSql, UserId);
                if (yhObj != null) {
                    msg = true;
                    myDb.Commit();
                    return msg;
                }
                myDb.Commit();
                return msg;
            }
            catch (Exception)
            {
                myDb.Rollback();
                throw;
            }
        }

        #endregion

    }
}
