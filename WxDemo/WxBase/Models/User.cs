using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxBase.Models
{
  public  class WeChatUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string CardNum { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class WXYH
    {
        public WXYH()
        {
            
        }

        private string _UserId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _UserPassword;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value; }
        }

        private string _CardNum;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string CardNum
        {
            get { return _CardNum; }
            set { _CardNum = value; }
        }

        private string _Phone;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Address;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }


    }
}
