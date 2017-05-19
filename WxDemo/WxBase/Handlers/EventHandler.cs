using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WxBase.Comm;
using WxBase.Messages;

namespace WxBase.Handlers
{
    class EventHandler : IHandler
    {
        /// <summary>
        /// 请求的xml
        /// </summary>
        private string RequestXml { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestXml"></param>
        public EventHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            string response = string.Empty;
            XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(RequestXml);
            XmlNode ScanCodeInfoNode = doc.SelectSingleNode("/xml/ScanCodeInfo/ScanResult");
            EventMessage em = EventMessage.LoadFromXml(RequestXml);
            XElement element = XElement.Parse(RequestXml);
            if (em.Event.Equals("subscribe", StringComparison.OrdinalIgnoreCase))
            {
                //回复欢迎消息
                TextMessage tm = new TextMessage();
                tm.ToUserName = em.FromUserName;
                tm.FromUserName = em.ToUserName;
                tm.CreateTime = Common.GetNowTime();
                tm.Content = "欢迎您关注青羊政务服务中心公众号！\n\n";
                response = tm.GenerateContent();
            }
            else if (em.Event.Equals("click", StringComparison.OrdinalIgnoreCase)) {               
                //回复点击事件
                TextMessage tm = new TextMessage();
                tm.ToUserName = em.FromUserName;
                tm.FromUserName = em.ToUserName;
                tm.CreateTime = Common.GetNowTime();
                tm.Content = "您点击的是" + em.EventKey;
                response = tm.GenerateContent();
                
            }
            else if (em.Event.Equals("view", StringComparison.OrdinalIgnoreCase)) {
                TextMessage tm = new TextMessage();
                tm.ToUserName = em.FromUserName;
                tm.FromUserName = em.ToUserName;
                tm.CreateTime = Common.GetNowTime();
                tm.Content = "页面跳转";
                response = tm.GenerateContent();

            }
            else if (em.Event.Equals("scancode_push", StringComparison.OrdinalIgnoreCase))
            {
                TextMessage tm = new TextMessage();
                tm.ToUserName = em.FromUserName;
                tm.FromUserName = em.ToUserName;
                tm.CreateTime = Common.GetNowTime();
                tm.Content = "您扫描的信息为：";
                response = tm.GenerateContent();

            }
            else if (em.Event.Equals("scancode_waitmsg", StringComparison.OrdinalIgnoreCase))
            {
                TextMessage tm = new TextMessage();
                tm.ToUserName = em.FromUserName;
                tm.FromUserName = em.ToUserName;
                tm.CreateTime = Common.GetNowTime();
                if (ScanCodeInfoNode != null)
                {
                    XmlCDataSection section = ScanCodeInfoNode.FirstChild as XmlCDataSection;
                    if (section != null)
                    {
                        tm.Content = "您扫描的信息为：" + section.Value;
                    }
                }
                response = tm.GenerateContent();
            }

            return response;
        }
    }
}
