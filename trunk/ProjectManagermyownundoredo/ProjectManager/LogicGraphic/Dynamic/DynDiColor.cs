using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProjectManager.LogicGraphic.Dynamic
{
    public class DynDiColor : DynamicObject, ILogicGraphicFormat
    {
        public DynDiColor(string testPointName)
        {
            this.testPointName = testPointName;
        }

        private string testPointName;
	

        private Color color0 = Color.Green;
        /// <summary>
        /// 0值颜色
        /// </summary>
        public Color Color0
        {
            get
            {
                return color0;
            }
            set
            {
                color0 = value;
            }
        }

        private Color color1 = Color.Red;
        /// <summary>
        /// 1值颜色
        /// </summary>
        public Color Color1
        {
            get
            {
                return color1;
            }
            set
            {
                color1 = value;
            }
        }


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", DynamicObjectType.DynDiColor);
            xmlWriter.WriteAttributeString("TestPointName", this.testPointName);
            xmlWriter.WriteAttributeString("DomainName", "Actual Value");
            xmlWriter.WriteAttributeString("Color0", ColorHelper.ColorToString(this.color0));
            xmlWriter.WriteAttributeString("Color1", ColorHelper.ColorToString(this.color1));

            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
