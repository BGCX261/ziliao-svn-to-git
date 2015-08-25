using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 直线(非信号线)
    /// </summary>
    public class LogicLine : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogicLine()
        {
            this.Type = LogicObjectType.Line;
        }

        public LogicLine(PointF point1, PointF point2, Color color, DashStyle lineType)
        {
            this.Type = LogicObjectType.Line;

            this.point1 = point1;
            this.point2 = point2;
            this.ForeColor = color;
            this.lineType = lineType;
            this.length = this.GetLength();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="point1">点1坐标</param>
        /// <param name="point2">点2坐标</param>
        /// <param name="color">颜色</param>
        /// <param name="lineType">线型</param>
        /// <param name="dynamics">动态属性</param>
        public LogicLine(PointF point1, PointF point2, Color color, DashStyle lineType, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.Line;

            this.point1 = point1;
            this.point2 = point2;
            this.ForeColor = color;
            this.lineType = lineType;
            this.length = this.GetLength();
        }

        private DashStyle lineType;
        /// <summary>
        /// 线型
        /// </summary>
        public DashStyle LineType
        {
            get
            {
                return lineType;
            }
            set
            {
                lineType = value;
            }
        }

        private PointF point1;
        /// <summary>
        /// 点1
        /// </summary>
        public PointF Point1
        {
            get
            {
                return point1;
            }
            set
            {
                point1 = value;
                this.length = this.GetLength();
            }
        }

        private PointF point2;
        /// <summary>
        /// 点2
        /// </summary>
        public PointF Point2
        {
            get
            {
                return point2;
            }
            set
            {
                point2 = value;
                this.length = this.GetLength();
            }
        }

        private float length;
        /// <summary>
        /// 线的长度
        /// </summary>
        public float Length
        {
            get
            {
                return length;
            }
        }

        public float GetLength()
        {
            float lineLength = (float)Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
            return lineLength;
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Line);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.point1.X * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.point1.Y * 10));
            xmlWriter.WriteAttributeString("X2", Convert.ToString(this.point2.X * 10));
            xmlWriter.WriteAttributeString("Y2", Convert.ToString(this.point2.Y * 10));
            xmlWriter.WriteAttributeString("PenStyle", ((int)this.lineType).ToString());
            xmlWriter.WriteAttributeString("PenWidth", "0");
            xmlWriter.WriteAttributeString("PenColor", ColorHelper.ColorToString(this.ForeColor));
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
