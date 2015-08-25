using System;
using System.Collections.Generic;
using System.Text;
using TDK.Core.Logic.DAL;
using System.Text.RegularExpressions;

namespace ProjectManager.LogicGraphic.Dynamic
{
    public class DynCrossReference : DynamicObject, ILogicGraphicFormat
    {
        private static Regex regAlgName = new Regex(@"^(\d+)((-(\d+)){2})$");
        private IList<string> references;
        private int loopRefCount;

        public DynCrossReference()
        {
            references = new List<string>();
            loopRefCount = 0;
        }

        public void AddReference(Cld_FCBlock block)
        {
            if (regAlgName.IsMatch(block.AlgName))
            {
                this.references.Add(block.AlgName);
            }
            else
            {
                throw new Exception("AlgName格式错误！");
            }
        }

        public void AddLoopReference(Cld_FCBlock block)
        {
            if (regAlgName.IsMatch(block.AlgName))
            {
                string algName;
                if (block.FunctionName != "XNetAI" && block.FunctionName != "XNetDI")
                {
                    algName = "net:*" + block.AlgName;
                }
                else
                {
                    algName = "net:" + block.AlgName;
                }

                this.references.Add(algName);
                loopRefCount++;
            }
            else
            {
                throw new Exception("AlgName格式错误！");
            }
        }

        #region ILogicGraphicFormat 成员

        public void GetXinhuaGraphicXml(System.Xml.XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Element");
            xmlWriter.WriteAttributeString("type", "MMIOP");

            StringBuilder referenceString = new StringBuilder("CrossReference");
            foreach (string str in this.references)
            {
                referenceString.Append(' ' + str);
            }
            xmlWriter.WriteAttributeString("CmdString", referenceString.ToString());


            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
