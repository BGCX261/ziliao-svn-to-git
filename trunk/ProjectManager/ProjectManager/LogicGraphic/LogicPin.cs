using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using TDK.Core.Logic.DAL;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 引脚
    /// </summary>
    public class LogicPin : LogicObject, ILogicGraphicFormat
    {
        /// <summary>
        /// 用来匹配类似"xxx-xxx-xxx-xxx"的PointName
        /// </summary>
        public static readonly Regex RegPointName = new Regex(@"^(\d+)((-(\d+)){3})$");

        /// <summary>
        /// 构造方法
        /// </summary>
        public LogicPin()
        {
            this.Type = LogicObjectType.Pin;
        }

        public LogicPin(Cld_FCInput pin, LogicSymbol symbol)
        {
            this.Type = LogicObjectType.Pin;

            this.pinType = LogicPinType.Input;
            this.pinName = pin.PinName;
            this.pinValue = pin.InitialValue;
            // 必须先给pinValue赋值
            this.PointName = pin.PointName;
            this.Visible = pin.Visible;
            this.point1 = this.point2 = new PointF(pin.Cld_FCBlock.X + pin.X, pin.Cld_FCBlock.Y + pin.Y);
            this.GenerateSubmitGraphicsByPin(symbol);
        }

        public LogicPin(Cld_FCOutput pin, LogicSymbol symbol)
        {
            this.Type = LogicObjectType.Pin;

            this.pinType = LogicPinType.Output;
            this.pinName = pin.PinName;
            this.pinValue = "0";
            // 必须先给pinValue赋值
            this.PointName = pin.PointName;
            this.Visible = pin.Visible;
            this.point1 = this.point2 = new PointF(pin.Cld_FCBlock.X + pin.X, pin.Cld_FCBlock.Y + pin.Y);
            this.GenerateSubmitGraphicsByPin(symbol);
        }

        public LogicPin(LogicPinType pinType, string name, PointF point1, PointF point2)
        {
            this.Type = LogicObjectType.Pin;

            this.pinName = name;
            this.pinType = pinType;
            this.point1 = point1;
            this.point2 = point2;
        }

        public LogicPin(LogicPinType pinType, string name, string value, PointF point1, PointF point2)
        {
            this.Type = LogicObjectType.Pin;

            this.pinType = pinType;
            this.pinName = name;
            this.PinValue = value;
            this.point1 = point1;
            this.point2 = point2;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">I/O 类型</param>
        /// <param name="point1">点 1 坐标</param>
        /// <param name="point2">点 2 坐标</param>
        /// <param name="dynamics">动态属性</param>
        public LogicPin(LogicPinType pinType, string name, PointF point1, PointF point2, params string[] dynamics)
            : base(dynamics)
        {
            this.Type = LogicObjectType.Pin;

            this.pinName = name;
            this.pinType = pinType;
            this.point1 = point1;
            this.point2 = point2;
        }

        private string pointName;
        /// <summary>
        /// 该引脚对应的点名
        /// </summary>
        public string PointName
        {
            get
            {
                return pointName;
            }
            set
            {
                pointName = value;
            }
        }

        private string pinName;
        /// <summary>
        /// 名称
        /// </summary>
        public string PinName
        {
            get
            {
                return pinName;
            }
            set
            {
                pinName = value;
            }
        }

        private string pinValue;

        public string PinValue
        {
            get
            {
                return pinValue;
            }
            set
            {
                pinValue = value;
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
            }
        }

        private bool m_Visible;

        public bool Visible
        {
            get
            {
                return m_Visible;
            }
            set
            {
                m_Visible = value;
            }
        }

        private LogicText pinNameText = null;
        /// <summary>
        /// 引脚名文本
        /// </summary>
        public LogicText PinNameText
        {
            get
            {
                return pinNameText;
            }
            set
            {
                pinNameText = value;
            }
        }

        private LogicText pinValueText = null;
        /// <summary>
        /// 引脚数值文本
        /// </summary>
        public LogicText PinValueText
        {
            get
            {
                return pinValueText;
            }
            set
            {
                pinValueText = value;
            }
        }

        private LogicLine pinLine = null;
        /// <summary>
        /// 从引脚伸出的线
        /// </summary>
        public LogicLine PinLine
        {
            get
            {
                return pinLine;
            }
            set
            {
                pinLine = value;
            }
        }

        /// <summary>
        /// 生成Pin的子图(PinName，PinValue，PinLine)
        /// </summary>
        public void GenerateSubmitGraphics()
        {
            // 生成引脚上伸出的线
            if (this.point1 != this.point2)
            {
                this.pinLine = new LogicLine(this.Point1, this.Point2, Color.Black, DashStyle.Solid);

            }

            // 生成引脚名称文本
            if (this.pinName != null && this.pinName.Length > 0)
            {
                if (this.pinType == LogicPinType.Input)
                {
                    this.pinNameText = new LogicText(this.pinName, 8f, new PointF(this.Point2.X + 2f, this.Point2.Y - 5f),
                                    new SizeF(28f, 10f), Align.MiddleLeft, Color.Black);
                }
                else
                {
                    this.pinNameText = new LogicText(this.pinName, 8f, new PointF(this.Point1.X - 30f, this.Point1.Y - 5f),
                                    new SizeF(28f, 10f), Align.MiddleRight, Color.Black);
                }
            }

            // 生成引脚值文本
            if (this.pinValue != null && this.pinValue.Length > 0)
            {
                if (this.pinType == LogicPinType.Input)
                {
                    this.pinValueText = new LogicText(this.pinValue, 8f, new PointF(this.Point1.X - 30f, this.Point2.Y - 5f),
                                    new SizeF(28f, 10f), Align.MiddleRight, Color.Black);
                }
                else
                {
                    this.pinValueText = new LogicText(this.pinValue, 8f, new PointF(this.Point2.X + 2f, this.Point2.Y - 12f),
                                    new SizeF(28f, 10f), Align.MiddleLeft, Color.Black);
                }

                // 动态属性(AiValueOut)
            }
        }

        public void GenerateSubmitGraphicsByPin(LogicSymbol symbol)
        {
            if (symbol.SymbolType == LogicSymbolType.GeneralSymbol)
            {
                // 生成引脚名称文本
                if (this.pinName != null && this.pinName.Length > 0)
                {
                    if (this.pinType == LogicPinType.Input)
                    {
                        this.pinNameText = new LogicText(this.pinName, 8f, new PointF(this.Point2.X + 3f, this.Point2.Y - 5f),
                            new SizeF(27f, 10f), Align.MiddleLeft, Color.Black);
                    }
                    else if (this.Visible)
                    {
                        this.pinNameText = new LogicText(this.pinName, 8f, new PointF(this.Point1.X - 30f, this.Point1.Y - 5f),
                            new SizeF(27f, 10f), Align.MiddleRight, Color.Black);
                    }
                }
            }

            // 生成引脚上伸出的线
            if (this.pinType == LogicPinType.Input)
            {
                if ((!this.Visible && this.pointName != null && RegPointName.IsMatch(this.pointName))
                    || (this.Visible && (this.pointName == null || this.pointName.Trim('?',' ').Length == 0)
                    && (this.pinValue == null || this.pinValue.Trim('?', ' ').Length == 0)))
                {
                    this.point1 = PointF.Subtract(this.point2, new SizeF(20f, 0f));
                    this.pinLine = new LogicLine(this.Point1, this.Point2, Color.Black, DashStyle.Solid);
                    if (symbol.X > this.point1.X)
                    {
                        symbol.Width += symbol.X - this.point1.X;
                        symbol.X = this.point1.X;
                    }

                    if (this.PointName != null && RegPointName.IsMatch(this.PointName))
                    {
                        string str = this.PointName.Substring(this.PointName.IndexOf('-') + 1);
                        this.pinValue = "<" + str.Substring(str.IndexOf('-') + 1) + ">";
                    }
                }
            }

            // 生成引脚值文本
            if (this.pinValue != null && this.pinValue.Length > 0)
            {
                if (this.pinType == LogicPinType.Input)
                {
                    // 输入值
                    this.pinValueText = new LogicText(this.pinValue, 8f, new PointF(this.Point1.X - 30f, this.Point1.Y - 5f),
                        new SizeF(27f, 10f), Align.MiddleRight, Color.Black);
                    if (symbol.X > this.point1.X - 30f)
                    {
                        symbol.Width += symbol.X - (this.point1.X - 30f);
                        symbol.X = this.point1.X - 30f;
                    }
                }
                else
                {
                    // 输出值
                    this.pinValueText = new LogicText(this.pinValue, 7f, new PointF(this.Point2.X + 3f, this.Point2.Y - 12f),
                                    new SizeF(27f, 10f), Align.MiddleLeft, Color.Black);

                    if (symbol.X + symbol.Width < this.point2.X + 30f)
                    {
                        symbol.Width += this.point2.X + 30f - symbol.X - symbol.Width;
                    }

                    if (symbol.SymbolType == LogicSymbolType.IOSymbol && symbol.Y > this.PinValueText.Y)
                    {
                        // 给 IO 块添加输出值文本后，需要调整块的高度和 Y 坐标
                        symbol.Height += symbol.Y - this.PinValueText.Y;
                        symbol.Y = this.PinValueText.Y;
                    }

                    // 动态输出值
                    DynValueOut value = new DynValueOut(this.pointName, 0);
                    this.PinValueText.Dynamics.Add(value);
                }
            }
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            if (this.pinNameText != null)
            {
                this.pinNameText.GetXinhuaGraphicXml(xmlWriter);
            }
            if (this.pinValueText != null)
            {
                this.pinValueText.GetXinhuaGraphicXml(xmlWriter);
            }
            if (this.pinLine != null)
            {
                this.pinLine.GetXinhuaGraphicXml(xmlWriter);
            }
        }

        #endregion
    }

    /// <summary>
    /// 引脚 I/O 类型
    /// </summary>
    public enum LogicPinType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Empty = 0,
        /// <summary>
        /// 输入
        /// </summary>
        Input = 1,
        /// <summary>
        /// 输出
        /// </summary>
        Output = 2,
    }
}
