using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 弧
    /// </summary>
    public class LogicArc : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogicArc()
        {
            this.Type = LogicObjectType.Arc;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="centrePoint">圆心坐标</param>
        /// <param name="point1">点 1 坐标</param>
        /// <param name="point2">点 2 坐标</param>
        /// <param name="color">颜色</param>
        /// <param name="dynamics">动态属性</param>
        public LogicArc(PointF centrePoint, PointF point1, PointF point2, Color color, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.Arc;

            this.ForeColor = color;
            this.centrePoint = centrePoint;
            this.point1 = point1;
            this.point2 = point2;
        }

        private PointF centrePoint;
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
                startAngle = (float)Math.Atan2(this.point1.Y - this.centrePoint.Y, this.point1.X - this.centrePoint.X);
                endAngle = (float)Math.Atan2(this.point2.Y - this.centrePoint.Y, this.point2.X - this.centrePoint.X);
            }
        }

        private float radius = 0;
        /// <summary>
        /// 半径
        /// </summary>
        public float Radius
        {
            get
            {
                if (radius == float.NaN)
                {
                    float radius0 = (float)Math.Sqrt(Math.Pow(this.point1.X - this.centrePoint.X, 2) + Math.Pow(this.point1.Y - this.centrePoint.Y, 2));
                    float radius1 = (float)Math.Sqrt(Math.Pow(this.point2.X - this.centrePoint.X, 2) + Math.Pow(this.point2.Y - this.centrePoint.Y, 2));
                    radius = radius0 > radius1 ? radius0 : radius1;
                }
                return radius;
            }
            set
            {
                radius = value;
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
                startAngle = (int)Math.Atan2(this.point1.Y - this.centrePoint.Y, this.point1.X - this.centrePoint.X);
                if (startAngle > 180)
                {
                    startAngle -= 360;
                }
                endAngle = (int)Math.Atan2(this.point2.Y - this.centrePoint.Y, this.point2.X - this.centrePoint.X);
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
                startAngle = (int)Math.Atan2(this.point1.Y - this.centrePoint.Y, this.point1.X - this.centrePoint.X);
                if (startAngle > 180)
                {
                    startAngle -= 360;
                }
                endAngle = (int)Math.Atan2(this.point2.Y - this.centrePoint.Y, this.point2.X - this.centrePoint.X);
            }
        }

        private float startAngle = 0;

        public float StartAngle
        {
            get
            {
                return startAngle;
            }
            set
            {
                startAngle = value;
            }
        }

        private float endAngle = 0;

        public float EndAngle
        {
            get
            {
                return endAngle;
            }
            set
            {
                endAngle = value;
            }
        }


        public float GetXinHuaAngle(float angle)
        {
            return Convert.ToSingle(angle * Math.PI / 180);
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Arc);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.centrePoint.X * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.centrePoint.Y * 10));
            xmlWriter.WriteAttributeString("Width", Convert.ToString(this.Radius * 10));
            xmlWriter.WriteAttributeString("Height", Convert.ToString(this.Radius * 10));
            xmlWriter.WriteAttributeString("StartArg", GetXinHuaAngle(this.StartAngle).ToString());
            xmlWriter.WriteAttributeString("EndArg", GetXinHuaAngle(this.EndAngle).ToString());
            xmlWriter.WriteAttributeString("PenColor", ColorHelper.ColorToString(this.ForeColor));
            xmlWriter.WriteAttributeString("PenStyle", "0");
            xmlWriter.WriteAttributeString("PenWidth", "0");
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
