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
    /// 矩形
    /// </summary>
    public class LogicRectangle : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LogicRectangle()
        {
            this.Type = LogicObjectType.Rectangle;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="location">点 1 坐标</param>
        /// <param name="point2">点 2 坐标</param>
        /// <param name="color">颜色</param>
        /// <param name="dynamics"> 动态参数</param>
        public LogicRectangle(PointF point1, PointF point2, Color color, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.Rectangle;

            this.ForeColor = color;
            this.Location = point1;
            this.width = point2.X - point1.X;
            this.height = point2.Y - point1.Y;
        }

        /// <summary>
        /// Symbol 所在的矩形区域
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                return new RectangleF(this.Location, this.Size);
            }
            set
            {
                this.Location = value.Location;
                this.Size = value.Size;
            }
        }

        /// <summary>
        /// 块的位置坐标(左上角)
        /// </summary>
        public PointF Location
        {
            get
            {
                return new PointF(this.x, this.y);
            }
            set
            {
                this.x = value.X;
                this.y = value.Y;
            }
        }

        /// <summary>
        /// Symbol 的大小
        /// </summary>
        public SizeF Size
        {
            get
            {
                return new SizeF(this.width, this.height);
            }
            set
            {
                this.width = value.Width;
                this.height = value.Height;
            }
        }

        private float x;

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        private float y;

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        private float width = 60f;
        /// <summary>
        /// 块的宽度
        /// </summary>
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private float height = 28f;
        /// <summary>
        /// 块的高度
        /// </summary>
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        private int lineWidth = 0;

        public int LineWidth
        {
            get
            {
                return lineWidth;
            }
            set
            {
                lineWidth = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Rectangle);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.x * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.y * 10));
            xmlWriter.WriteAttributeString("X2", Convert.ToString((this.x + this.width) * 10));
            xmlWriter.WriteAttributeString("Y2", Convert.ToString((this.y + this.height) * 10));
            xmlWriter.WriteAttributeString("PenStyle", "0");
            xmlWriter.WriteAttributeString("PenWidth", Convert.ToString(this.lineWidth * 10));
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
