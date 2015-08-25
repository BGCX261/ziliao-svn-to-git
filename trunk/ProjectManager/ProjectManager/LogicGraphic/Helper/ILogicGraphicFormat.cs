using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ProjectManager.LogicGraphic
{
    interface ILogicGraphicFormat
    {
        /// <summary>
        /// 将图形信息写入XML件
        /// </summary>
        /// <param name="xmlWriter">XML文件编写器</param>
        void GetXinhuaGraphicXml(XmlTextWriter xmlWriter);
    }
}
