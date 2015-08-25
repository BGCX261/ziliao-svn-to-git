using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TDK.Core.Logic.DAL;
using System.Xml;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// Symbol
    /// </summary>
    public class LogicSymbol : LogicObject, ILogicGraphicFormat
    {
        #region 构造方法

        /// <summary>
        /// 初始化 <seealso cref="LogicSymbol" /> 类的新实例
        /// </summary>
        protected LogicSymbol()
        {
            this.Type = LogicObjectType.Symbol;
        }

        /// <summary>
        /// 初始化 <seealso cref="LogicSymbol" /> 类的新实例
        /// </summary>
        /// <param name="symbolType"></param>
        public LogicSymbol(LogicSymbolType symbolType)
        {
            this.Type = LogicObjectType.Symbol;

            this.symbolType = symbolType;
        }

        //public LogicSymbol(Cld_FCBlock block)
        //{
        //    this.symbolName = block.FunctionName;
        //    this.symbolType = GetSymbolType(this.symbolName);

        //}

        #endregion

        private string symbolName = string.Empty;
        /// <summary>
        /// 块(算法)名称
        /// </summary>
        public string SymbolName
        {
            get
            {
                return symbolName;
            }
            set
            {
                symbolName = value;
            }
        }

        private LogicSymbolType symbolType = LogicSymbolType.GeneralSymbol;
        /// <summary>
        /// 块的类型
        /// </summary>
        public LogicSymbolType SymbolType
        {
            get
            {
                return symbolType;
            }
            set
            {
                symbolType = value;
            }
        }

        #region 位置与大小
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
        #endregion

        private int inputCount = 0;
        /// <summary>
        /// 块的输入引脚个数
        /// </summary>
        public int InputCount
        {
            get
            {
                return inputCount;
            }
            set
            {
                inputCount = value;

                if (value > this.outputCount)
                {
                    this.ResizeByPinCount(); 
                }
            }
        }

        private int outputCount = 0;
        /// <summary>
        /// 块的输出引脚个数
        /// </summary>
        public int OutputCount
        {
            get
            {
                return outputCount;
            }
            set
            {
                outputCount = value;

                if (value > this.inputCount)
                {
                    this.ResizeByPinCount(); 
                }
            }
        }


        private IList<LogicObject> graphics = new List<LogicObject>();
        /// <summary>
        /// 块包含的图形集合
        /// </summary>
        public IList<LogicObject> Graphics
        {
            get
            {
                return graphics;
            }
            set
            {
                graphics = value;
            }
        }

        /// <summary>
        /// 根据Pin的个数计算块的高度
        /// </summary>
        public void ResizeByPinCount()
        {
            if (this.symbolType == LogicSymbolType.GeneralSymbol)
            {
                int pinCount = this.inputCount >= this.outputCount ? this.inputCount : this.outputCount;

                this.height = LogicSymbol.HeadHeight + PinSpace * pinCount + LogicSymbol.FootHeight;
            }
        }

        /// <summary>
        /// 根据算法块的名称判断其类型
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static LogicSymbolType GetSymbolType(string symbolName)
        {
            if (symbolName.StartsWith("X") && symbolName != "Xor")
            {
                return LogicSymbolType.IOSymbol;
            }
            else
            {
                return LogicSymbolType.GeneralSymbol;
            }
        }

        #region 静态常量

        /// <summary>
        /// 块的名称文本高度
        /// </summary>
        public static readonly float HeadHeight = 15f;
        /// <summary>
        /// 块的序号文本高度
        /// </summary>
        public static readonly float FootHeight = 15f;
        /// <summary>
        /// 块的参数文本高度
        /// </summary>
        public static readonly float ParaHeight = 13f;
        /// <summary>
        /// 块的引脚间距
        /// </summary>
        public static readonly float PinSpace = 15f;
        /// <summary>
        /// 引脚默认长度
        /// </summary>
        public static readonly float PinLength = 15f;

        #endregion

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Symbol);
            xmlWriter.WriteAttributeString("X1", Convert.ToString(this.x * 10));
            xmlWriter.WriteAttributeString("Y1", Convert.ToString(this.y * 10));
            xmlWriter.WriteAttributeString("X2", Convert.ToString((this.x + this.width) * 10));
            xmlWriter.WriteAttributeString("Y2", Convert.ToString((this.y + this.height) * 10));
            xmlWriter.WriteAttributeString("DynamicOp", this.Dynamics.Count.ToString());

            foreach (LogicObject logobj in this.Graphics)
            {
                ILogicGraphicFormat graphicFormat = logobj as ILogicGraphicFormat;
                if (graphicFormat != null)
                {
                    graphicFormat.GetXinhuaGraphicXml(xmlWriter);
                }
            }

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


    public enum LogicSymbolType
    {
        /// <summary>
        /// 普通方形模块
        /// </summary>
        GeneralSymbol = 0,
        /// <summary>
        /// I/O模块
        /// </summary>
        IOSymbol = 1,
    }
}
