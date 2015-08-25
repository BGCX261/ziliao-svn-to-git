using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// DocShape
    /// </summary>
    public class LogicDocShape : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogicDocShape()
        {
            this.Type = LogicObjectType.DocShape;
            this.name = string.Empty;
            this.location = PointF.Empty;
            this.angle = 0f;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="location">位置坐标</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="dynamics">动态属性</param>
        public LogicDocShape(string name, PointF location, float angle, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.DocShape;
            this.name = name;
            this.location = location;
            this.angle = angle;
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

        private PointF location;
        /// <summary>
        /// 坐标位置
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

        private float angle;
        /// <summary>
        /// 旋转角度
        /// </summary>
        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.DocShape);

            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
