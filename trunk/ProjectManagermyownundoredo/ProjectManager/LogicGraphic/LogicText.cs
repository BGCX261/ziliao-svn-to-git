using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 文本
    /// </summary>
    public class LogicText:LogicObject, ILogicGraphicFormat
    {
        public static readonly string DefaultFamily = "Arial";

        /// <summary>
        /// 构造方法
        /// </summary>
        public LogicText()
        {
        }

        public LogicText(string text, float fontSize, PointF location, SizeF size, Align alignment, Color color)
        {
            this.text = text;
            this.font = new Font(DefaultFamily, fontSize);
            this.Location = location;
            this.Size = size;
            this.alignment = alignment;
            this.ForeColor = color;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="fontSize">字号</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="location">位置坐标</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="color">颜色</param>
        /// <param name="dynamics">动态属性</param>
        public LogicText(string text, float fontSize, FontFamily fontFamily, PointF location, SizeF size,
            Align alignment, float angle, Color color, params string[] dynamics)
            : base(dynamics)
        {
            this.text = text;
            this.font = new Font(fontFamily, fontSize);
            this.Location = location;
            this.Size = size;
            this.alignment = alignment;
            this.angle = angle;
            this.ForeColor = color;
        }

        private string text = "";
        /// <summary>
        /// 文字内容
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        private Align alignment = Align.MiddleCenter;
        /// <summary>
        /// 对齐方式
        /// </summary>
        public Align Alignment
        {
            get
            {
                
                return alignment;
            }
            set
            {
                alignment = value;
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

        /// <summary>
        /// 位置坐标
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

        private float angle = 0f;
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

        private Font font = new Font(DefaultFamily, 8f, FontStyle.Regular);
        /// <summary>
        /// 字体
        /// </summary>
        /// <remarks>包括 FontFamily、FontStyle、FontSize等字体信息</remarks>
        public Font Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            if (xmlWriter != null)
            {
                xmlWriter.WriteStartElement("Element");
                xmlWriter.WriteAttributeString("type", LogicObjectType.Text);
                xmlWriter.WriteAttributeString("X1", Convert.ToString(this.X * 10));
                xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.Y * 10));
                xmlWriter.WriteAttributeString("X2", Convert.ToString((this.X + this.width)* 10));
                xmlWriter.WriteAttributeString("Y2", Convert.ToString((this.Y + this.height)* 10));
                xmlWriter.WriteAttributeString("Alignment", ((int)this.alignment).ToString());
                xmlWriter.WriteAttributeString("Font", this.font.FontFamily.Name);
                xmlWriter.WriteAttributeString("FontSize", this.font.Size.ToString());
                xmlWriter.WriteAttributeString("FontStyle", ((int)this.font.Style).ToString());
                xmlWriter.WriteAttributeString("FontColor", ColorHelper.ColorToString(this.ForeColor));
                xmlWriter.WriteAttributeString("BrushType", "1");
                xmlWriter.WriteAttributeString("BackColor", ColorHelper.ColorToString(this.BackColor));
                xmlWriter.WriteAttributeString("StringContent", this.text);
                xmlWriter.WriteAttributeString("Angle", this.angle.ToString());
                xmlWriter.WriteAttributeString("DynamicOp", this.Dynamics.Count.ToString());

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

                xmlWriter.WriteEndElement();
            }
        }

        #endregion
    }
}
