using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 连接点
    /// </summary>
    public class LogicLinkPoint : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LogicLinkPoint()
        {
            this.Type = LogicObjectType.LinkPoint;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">I/O 类型</param>
        /// <param name="location">位置坐标</param>
        /// <param name="dynamics">动态属性</param>
        public LogicLinkPoint(string name, LogicPinType pinType, PointF location, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.LinkPoint;

            this.name = name;
            this.pinType = pinType;
            this.location = location;
        }

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private LogicPinType pinType;
        /// <summary>
        /// 类型
        /// </summary>
        public LogicPinType PinType
        {
            get
            {
                return pinType;
            }
            set
            {
                pinType = value;
            }
        }

        private PointF location;
        /// <summary>
        /// 点坐标
        /// </summary>
        public PointF Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.LinkPoint);

            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
