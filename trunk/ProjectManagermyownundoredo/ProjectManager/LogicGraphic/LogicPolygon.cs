using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Drawing.Drawing2D;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 多边形
    /// </summary>
    public class LogicPolygon : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LogicPolygon()
        {
            this.Type = LogicObjectType.Polygon;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="filled">是否填充</param>
        /// <param name="points">点集合</param>
        /// <param name="dynamics">动态参数</param>
        public LogicPolygon(Color color, bool filled, PointF[] points, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.Polygon;

            this.ForeColor = color;
            this.filled = filled;
            this.points = points;
        }

        private bool filled = false;
        /// <summary>
        /// 是否填充
        /// </summary>
        public bool Filled
        {
            get
            {
                return filled;
            }
            set
            {
                filled = value;
            }
        }

        private PointF[] points;
        /// <summary>
        /// 点集合
        /// </summary>
        public PointF[] Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }



        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Polygon);
            xmlWriter.WriteAttributeString("PointNum", this.points.Length.ToString());
            for (int i = 0; i < points.Length; i++)
            {
                xmlWriter.WriteAttributeString("X" + i.ToString(), Convert.ToString(this.points[i].X * 10));
                xmlWriter.WriteAttributeString("Y" + i.ToString(), Convert.ToString(this.points[i].Y * 10));
            }
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
