using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProjectManager.LogicGraphic.Dynamic;

namespace ProjectManager.LogicGraphic
{
    /// <summary>
    /// 动态属性
    /// </summary>
    public class LogicObject
    {
        public LogicObject(params string[] dynamics)
        {
            if (dynamics.Length > 0)
            {
                foreach (string str in dynamics)
                {

                }
            }
        }

        private string type = LogicObjectType.Object;
        /// <summary>
        /// 获取图形的类型名称
        /// </summary>
        public virtual string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        private Color foreColor = Color.Black;
        /// <summary>
        /// 前景色
        /// </summary>
        public virtual Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
            }
        }

        private Color backColor = Color.White;
        /// <summary>
        /// 背景色
        /// </summary>
        public virtual Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                backColor = value;
            }
        }

        private IList<DynamicObject> dynamics = new List<DynamicObject>();
        /// <summary>
        /// 动态属性
        /// </summary>
        public IList<DynamicObject> Dynamics
        {
            get
            {
                return dynamics;
            }
            set
            {
                dynamics = value;
            }
        }

    }


    public class LogicObjectType
    {
        public const string Object = null;

        public const string Arc = "Arc";
        public const string Circle = "Ellipse";
        public const string DocShape = "DocShape";
        public const string Line = "Line";
        public const string LinkPoint = "LinkPoint";
        public const string Pin = "Pin";
        public const string Polygon = "Polygon";
        public const string Signalline = "Polyline";
        public const string Rectangle = "Rectangle";
        public const string RountRect = "RoundRect";
        public const string Text = "Text";
        public const string Poke = "Poke";


        public const string Symbol = "Group";
    }
}
