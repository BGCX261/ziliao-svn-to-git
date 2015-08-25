using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ProjectManager.LogicGraphic.Dynamic;
using TDK.Core.Logic.DAL;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 多线(信号线)
    /// </summary>
    public class LogicSignalLine : LogicObject, ILogicGraphicFormat
    {
        #region 构造函数

        /// <summary>
        /// 初始化<see cref="LogicSignal"/>类的新实例
        /// </summary>
        public LogicSignalLine()
        {
            this.Type = LogicObjectType.Signalline;
        }

        public LogicSignalLine(string signalData)
        {
            this.Type = LogicObjectType.Signalline;

            this.InitializePoints(signalData);
        }

        public LogicSignalLine(string signalData, string pointName, string signalType)
        {
            this.Type = LogicObjectType.Signalline;

            this.InitializePoints(signalData);
            this.pointName = pointName;
            if (signalType == "LD")
            {
                this.signalType = DataType.Digital;
            }
            else
            {
                this.signalType = DataType.Analog;
            }
        }

        private void InitializePoints(string signalData)
        {
            string[] pointList = signalData.Trim(' ', ';').Split(',');
            this.points = new PointF[pointList.Length];

            // 添加DiColor动态属性

            for (int i = 0; i < pointList.Length; i++)
            {
                if (pointList[i].Contains("{"))
                {
                    if (i == 0)
                    {
                        this.startPinName = pointList[i].Substring(pointList[i].IndexOf('{')).Trim('{', '}');
                    }
                    else
                    {
                        this.endPinName = pointList[i].Substring(pointList[i].IndexOf('{')).Trim('{', '}');
                    }

                    pointList[i] = pointList[i].Substring(0, pointList[i].IndexOf('{'));
                }
                string[] point = pointList[i].Split('_');
                this.points[i] = new PointF(Convert.ToSingle(point[0]), Convert.ToSingle(point[1]));
            }
        }

        #endregion

        private PointF[] points;
        /// <summary>
        /// 构成Polyline的点集合
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

        private string pointName;
        /// <summary>
        /// 测点名
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

        private string startPinName;
        /// <summary>
        /// 起始引脚名称
        /// </summary>
        public string StartPinName
        {
            get
            {
                return startPinName;
            }
            set
            {
                startPinName = value;
            }
        }

        private string endPinName;
        /// <summary>
        /// 结束引脚名称
        /// </summary>
        public string EndPinName
        {
            get
            {
                return endPinName;
            }
            set
            {
                endPinName = value;
            }
        }

        private DataType signalType = DataType.Analog;
        /// <summary>
        /// 信号数据类型
        /// </summary>
        public DataType SignalType
        {
            get
            {
                return signalType;
            }
            set
            {
                signalType = value;
            }
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            if (this.signalType == DataType.Digital)
            {
                // 信号类型为数字量时，添加开关量颜色动态属性
                DynDiColor diColor = new DynDiColor(this.pointName);
                this.Dynamics.Add(diColor);
            }

            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", LogicObjectType.Signalline);
            xmlWriter.WriteAttributeString("PointNum", this.points.Length.ToString());
            for (int i = 0; i < points.Length; i++)
            {
                xmlWriter.WriteAttributeString("X" + i.ToString(), Convert.ToString(this.points[i].X * 10));
                xmlWriter.WriteAttributeString("Y" + i.ToString(), Convert.ToString(this.points[i].Y * 10));
            }
            xmlWriter.WriteAttributeString("PenStyle", ((int)this.signalType).ToString());
            xmlWriter.WriteAttributeString("PenWidth", "1");
            xmlWriter.WriteAttributeString("PenColor", ColorHelper.ColorToString(this.ForeColor));
            xmlWriter.WriteAttributeString("IsLinkLine", "1");
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
