using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    class LogicRoundRectangle : LogicObject, ILogicGraphicFormat
    {
        public LogicRoundRectangle()
        {
            this.Type = LogicObjectType.RountRect;
        }

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

        private float width = 75f;

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

        private float height = 15f;

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


        private float xRadius = 15f;
        /// <summary>
        /// X 半径
        /// </summary>
        public float XRadius
        {
            get
            {
                return xRadius;
            }
            set
            {
                xRadius = value;
            }
        }

        private float yRadius = 15f;
        /// <summary>
        /// Y 半径
        /// </summary>
        public float YRadius
        {
            get
            {
                return yRadius;
            }
            set
            {
                yRadius = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.RountRect);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.x * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.y * 10));
            xmlWriter.WriteAttributeString("X2", Convert.ToString((this.x + this.width) * 10));
            xmlWriter.WriteAttributeString("Y2", Convert.ToString((this.y + this.height) * 10));
            xmlWriter.WriteAttributeString("XRadius", Convert.ToString(this.xRadius * 10));
            xmlWriter.WriteAttributeString("YRadius", Convert.ToString(this.yRadius * 10));
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
