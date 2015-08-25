using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 圆
    /// </summary>
    public class LogicCircle : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogicCircle()
        {
            this.Type = LogicObjectType.Circle;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="centrePoint">圆心坐标</param>
        /// <param name="radius">半径</param>
        /// <param name="color">颜色</param>
        /// <param name="dynamics">动态属性</param>
        public LogicCircle(PointF centrePoint, float radius, Color color, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.Circle;

            this.centrePoint = centrePoint;
            this.radius = radius;
            this.ForeColor = color;
        }

        private PointF centrePoint = new PointF();
        /// <summary>
        /// 圆心坐标
        /// </summary>
        public PointF CentrePoint
        {
            get
            {
                return centrePoint;
            }
            set
            {
                centrePoint = value;
            }
        }

        private float radius;
        /// <summary>
        /// 半径
        /// </summary>
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        /// <summary>
        /// 外接矩形左上角坐标
        /// </summary>
        public PointF Point1
        {
            get
            {
                return new PointF(this.centrePoint.X - this.radius, this.centrePoint.Y - this.radius);
            }
        }

        /// <summary>
        /// 外接矩形右下角坐标
        /// </summary>
        public PointF Point2
        {
            get
            {
                return new PointF(this.centrePoint.X + this.radius, this.centrePoint.Y + this.radius);
            }
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Circle);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.Point1.X * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.Point1.Y * 10));
            xmlWriter.WriteAttributeString("X2", Convert.ToString(this.Point2.X * 10));
            xmlWriter.WriteAttributeString("Y2", Convert.ToString(this.Point2.Y * 10));
            xmlWriter.WriteAttributeString("PenStyle", "0");
            xmlWriter.WriteAttributeString("PenWidth", "0");
            xmlWriter.WriteAttributeString("PenColor", ColorHelper.ColorToString(this.ForeColor));
            xmlWriter.WriteAttributeString("BrushStyle", "1");
            xmlWriter.WriteAttributeString("BrushColor", "FF FF FF FF");
            xmlWriter.WriteAttributeString("DynamicOp", this.Dynamics.Count.ToString());
            {
                // 添加动态属性
                xmlWriter.WriteStartElement("Element");
                xmlWriter.WriteAttributeString("type", "DynamicOp");
                foreach (DynamicObject dynamic in this.Dynamics)
                {
                    ILogicGraphicFormat dynamicFormat = dynamic as ILogicGraphicFormat;
                    if (dynamicFormat != null)
                    {
                        dynamicFormat.GetXinhuaGraphicXml(xmlWriter);
                    }
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
