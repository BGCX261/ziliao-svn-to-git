using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.LogicGraphic.Dynamic
{
    public class DynValueOut : DynamicObject, ILogicGraphicFormat
    {
        public DynValueOut(string testPointName, int align)
        {
            this.pointName = testPointName;
            this.alignment = align;
        }

        private string pointName = "";

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

        private int alignment = 1;
        /// <summary>
        /// 对齐方式：0：左；1：中；2：右
        /// </summary>
        public int Alignment
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


        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", DynamicObjectType.DynValueOut);
            xmlWriter.WriteAttributeString("TestPointName", this.pointName);
            xmlWriter.WriteAttributeString("DomainName", "Actual Value");
            xmlWriter.WriteAttributeString("Align", this.alignment.ToString());
            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
