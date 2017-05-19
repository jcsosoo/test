using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WxBase.Comm;

namespace WxBase.Messages
{
    // <summary>
    // 
    // </summary>
    public class WaitMsg : Message
    {
        /// <summary>
        /// 模板静态字段
        /// </summary>
        private static string m_Template;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WaitMsg()
        {
            this.MsgType = "event";
        }
        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static WaitMsg LoadFromXml(string xml)
        {
            WaitMsg tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new WaitMsg();
                    tm.FromUserName = element.Element(Common.FROM_USERNAME).Value;
                    tm.ToUserName = element.Element(Common.TO_USERNAME).Value;
                    tm.CreateTime = element.Element(Common.CREATE_TIME).Value;
                    tm.Event = element.Element(Common.EVENT).Value;
                    tm.EventKey = element.Element(Common.EVENT_KEY).Value;
                    tm.ScanType = element.Element(Common.SCAN_TYPE).Value;
                    tm.ScanResult = element.Element(Common.SCAN_RESULT).Value;
                }
            }

            return tm;
        }

        internal static WaitMsg LoadFromXml(object requestXml)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 模板
        /// </summary>
        public override string Template
        {
            get
            {
                if (string.IsNullOrEmpty(m_Template))
                {
                    LoadTemplate();
                }

                return m_Template;
            }
        }
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public override string GenerateContent()
        {
            this.CreateTime = Common.GetNowTime();
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.MsgType, this.Event, this.EventKey, this.ScanType, this.ScanResult);
        }
        /// <summary>
        /// 加载模板
        /// </summary>
        private static void LoadTemplate()
        {
            m_Template = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[{3}]]></MsgType>
                                <Event><![CDATA[{4}]]></Event>
                                <EventKey><![CDATA[{5}]]></EventKey>
                                <ScanCodeInfo>
                                    <ScanType><![CDATA[{6}]]></ScanType>
                                    <ScanResult><![CDATA[{7}]]></ScanResult>
                                </ScanCodeInfo>
                            </xml>";
        }
    }
}

