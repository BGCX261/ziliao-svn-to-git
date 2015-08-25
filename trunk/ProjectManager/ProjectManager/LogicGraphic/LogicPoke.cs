using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TDK.Core.Logic.DAL;
using ProjectManager.LogicGraphic.Dynamic;
using TDK.Core.Logic.DAL;

namespace ProjectManager.LogicGraphic
{
    class LogicPoke : LogicObject, ILogicGraphicFormat
    {
        public LogicPoke()
        {
            this.Type = LogicObjectType.Poke;
        }

        public LogicPoke(Cld_FCBlock block)
        {
            this.Type = LogicObjectType.Poke;
            this.Location = block.Location;
            this.Size = block.Size;
            this.BlockID = Convert.ToInt32(block.AlgName.Substring(block.AlgName.LastIndexOf('-') + 1));
        }

        public PointF Location
        {
            get
            {
                return new PointF(this.x, this.y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
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
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        private float width;

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

        private float height;

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

        private int blockID;

        public int BlockID
        {
            get
            {
                return blockID;
            }
            set
            {
                blockID = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Poke);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.X * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.Y * 10));
            xmlWriter.WriteAttributeString("X2", Convert.ToString((this.X + this.Width) * 10));
            xmlWriter.WriteAttributeString("Y2", Convert.ToString((this.Y + this.Height) * 10));
            xmlWriter.WriteAttributeString("BlockID", this.BlockID.ToString());
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
