using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.LogicGraphic.Dynamic
{
    public class DynTagDef : DynamicObject, ILogicGraphicFormat
    {
        public DynTagDef(string pointName, int type)
        {
            this.pointName = pointName;
            this.tagType = type;
        }

        private string pointName;

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

        private int tagType;
        /// <summary>
        /// 描述类型  0：测点名；1：中文描述；2：单位
        /// </summary>
        public int TagType
        {
            get
            {
                return tagType;
            }
            set
            {
                tagType = value;
            }
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", DynamicObjectType.DynTagDef);
            xmlWriter.WriteAttributeString("TestPointName", this.pointName);
            xmlWriter.WriteAttributeString("TagDefType", this.tagType.ToString());

            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
