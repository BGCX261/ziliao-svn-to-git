using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using TDK.Core.Logic.DAL;
//using ProjectManager.DAL;
using ProjectManager.LogicGraphic;
using ProjectManager.LogicGraphic.Dynamic;
using NHibernate;

namespace TDK.Core.Logic.BLL
{
    /// <summary>
    /// 根据对象模型生成图形文件
    /// </summary>
    public static class GraphicsDocument
    {
        public static ISession session = null;
        private static bool generatePageList = false;    // 输出控制器列表


        private static bool generateSheet = true;       // 输出页面数据
        private static int maxDpu = 0;                  // 输出的控制器数量(生成画面时)，0：不限制
        private static int maxPage = 0;                 // 输出的页面数量(生成画面时)，0：不限制

        /// <summary>
        /// 生成页面目录文件
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="outPath">项目输出路径，页面文件的输出路径为：outPath + "\\data\\xinhua"</param>
        public static void GenerateXinHuaPageList(BllManager bll, string outPath)
        {
            string filePath = outPath + "\\data\\xinhua\\";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            XmlTextWriter listWriter = new XmlTextWriter(filePath + "PageList.xml", Encoding.UTF8);
            listWriter.Indentation = 4;
            listWriter.Formatting = Formatting.Indented;

            IList<Prj_Project> projects = bll.manager.ProjectCRUD.GetPrj_Projects();
            listWriter.WriteStartElement("XDPS");
            listWriter.WriteAttributeString("Describe", projects[0].ProjectName);

            IList<Prj_Network> networks = bll.manager.NetworkCRUD.GetPrj_Networks_By_Prj_Project_ID(projects[0].ID);
            IList<Prj_Unit> units = bll.manager.UnitCRUD.GetPrj_Units_By_Prj_Network_ID(networks[0].ID);
            IList<Prj_Controller> controllers = bll.manager.ControllerCRUD.GetPrj_Controllers_By_Prj_Unit_ID(units[0].ID);

            foreach (Prj_Controller controller in controllers)
            {
                listWriter.WriteStartElement("DPU");
                listWriter.WriteAttributeString("Drop", controller.ControllerAddress);
                listWriter.WriteAttributeString("Name", controller.ControllerName);
                listWriter.WriteAttributeString("Describe", controller.Description);
                listWriter.WriteAttributeString("Version", controller.Version);

                IList<Prj_Document> documents = bll.manager.DocumentCRUD.GetPrj_Documents_By_Prj_Controller_ID(controller.ID);
                foreach (Prj_Document document in documents)
                {
                    listWriter.WriteStartElement("Page");
                    listWriter.WriteAttributeString("PageNum", document.DocumentName.Substring(document.DocumentName.LastIndexOf('-') + 1));
                    listWriter.WriteAttributeString("Describe", document.DocumentCaption);
                    listWriter.WriteEndElement();
                }

                listWriter.WriteEndElement();
            }

            listWriter.WriteEndElement();
            listWriter.Close();
        }

        /// <summary>
        /// 转换所有Project
        /// </summary>
        /// <param name="bll"></param>
        public static void GenerateXinHuaProjects(BllManager bll, string outPath)
        {
            session = bll.manager.session;
            IList<Prj_Project> projects = bll.manager.ProjectCRUD.GetPrj_Projects();    // 加载全部工程列表

            GenerateXinHuaControls(bll, projects[0], outPath);
        }

        /// <summary>
        /// 转换所有Control
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="project"></param>
        public static void GenerateXinHuaControls(BllManager bll, Prj_Project project, string outPath)
        {
            // 新华系统中，每个Project仅包含一个Network，每个Network仅包含一个Unit
            IList<Prj_Network> networks = bll.manager.NetworkCRUD.GetPrj_Networks_By_Prj_Project_ID(project.ID);
            IList<Prj_Unit> units = networks.Count > 0 ? bll.manager.UnitCRUD.GetPrj_Units_By_Prj_Network_ID(networks[0].ID) : new List<Prj_Unit>();
            IList<Prj_Controller> controllers = units.Count > 0 ? bll.manager.ControllerCRUD.GetPrj_Controllers_By_Prj_Unit_ID(units[0].ID) : new List<Prj_Controller>();

            for (int i = 0; i < controllers.Count; i++)
            {
                GeneralXinHuaDocuments(bll, controllers[i], outPath);
            }
        }

        /// <summary>
        /// 转换所有Document
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="path"></param>
        /// <param name="listWriter"></param>
        /// <param name="controller"></param>
        public static void GeneralXinHuaDocuments(BllManager bll, Prj_Controller controller, string outPath)
        {
            IList<Prj_Document> documents = bll.manager.DocumentCRUD.GetPrj_Documents_By_Prj_Controller_ID(controller.ID);

            for (int i = 0; i < documents.Count; i++)
            {
                Prj_Document document = documents[i];
                string pageNum = document.DocumentName.Substring(document.DocumentName.LastIndexOf('-') + 1);

                // 生成页面文件
                GenerateXinHuaSheets(bll, document, outPath);
            }
        }

        /// <summary>
        /// Prj_Sheet to Xml
        /// </summary>
        /// <param name="sheet"></param>
        public static void GenerateXinHuaSheets(BllManager bll, Prj_Document documet, string outPath)
        {
            string pagesPath = outPath + "\\data\\xinhua\\Pages\\";
            if (!Directory.Exists(pagesPath))
            {
                Directory.CreateDirectory(pagesPath);
            }

            if (session == null)
            {
                session = bll.manager.session;
            }

            try
            {
                // 每个Document只包含一个Sheet
                int sheetID = bll.manager.SheetCRUD.GetPrj_Sheets_By_Prj_Document_ID(documet.ID)[0].ID;
                Prj_Sheet sheet = bll.manager.SheetCRUD.Load_Sheet(sheetID);

                ArrayList symbols = new ArrayList();

                // 算法块

                foreach (Cld_FCBlock block in sheet.Cld_FCBlock_List)
                {

                    #region if(特殊块)
                    if (block.FunctionName == "XAI")
                    {
                        GenerateXAIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XAO")
                    {
                        GenerateXAOBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XDI")
                    {
                        GenerateXDIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XDO")
                    {
                        GenerateXDOBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XPI")
                    {
                        GenerateXPIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XPgAI")
                    {
                        GenerateXPgAIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XPgAO")
                    {
                        GenerateXPgAOBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XPgDI")
                    {
                        GenerateXPgDIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XPgDO")
                    {
                        GenerateXPgDOBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XNetAI")
                    {
                        GenerateXNetAIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XNetAO")
                    {
                        GenerateXNetAOBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XNetDI")
                    {
                        GenerateXNetDIBlock(block, symbols);
                    }
                    else if (block.FunctionName == "XNetDO")
                    {
                        GenerateXNetDOBlock(block, symbols);
                    }
                    #endregion
                    else
                    {
                        bll.generate_Rec_symbol(block, BllManager.rela_pos.UPLEFT);
                        GenerateCommonBlock(block, symbols);
                    }
                }

                // 文本块

                foreach (Cld_Graphic graphic in sheet.Cld_Graphic_List)
                {
                    if (graphic.Type == "Text")
                    {
                        GenerateText(graphic, symbols);
                    }
                }

                //IList constants = sheet.Cld_Constant_List;

                // 信号线

                IDictionary<string, Cld_Signal> signalTable = bll.GenerateSignalLines(sheet);
                foreach (Cld_Signal signal in signalTable.Values)
                {
                    string[] signalDataList = signal.Data.TrimEnd(' ', ';').Split(';');
                    foreach (string signalData in signalDataList)
                    {
                        LogicSignalLine signalline = new LogicSignalLine(signalData, signal.Name, signal.SignalType);
                        symbols.Add(signalline);
                    }
                }

                // 创建XmlTextWriter类的实例对象
                XmlTextWriter textWriter = new XmlTextWriter(pagesPath + documet.DocumentName.Replace('-', '_') + ".xfig", Encoding.UTF8);
                textWriter.Indentation = 4;
                textWriter.Formatting = Formatting.Indented;

                textWriter.WriteStartElement("document");
                textWriter.WriteAttributeString("Type", "BeginNode");
                textWriter.WriteAttributeString("GraphicName", documet.DocumentCaption);
                textWriter.WriteAttributeString("BackColor", "FF FF FF FF");
                textWriter.WriteAttributeString("width", Convert.ToString(sheet.Width * 10));
                textWriter.WriteAttributeString("heigth", Convert.ToString(sheet.Height * 10));

                LogicRectangle pageBounds0 = new LogicRectangle();
                pageBounds0.Location = new PointF(0, 0);
                pageBounds0.Size = new SizeF(sheet.Width, sheet.Height);
                pageBounds0.LineWidth = 1;
                pageBounds0.GetXinhuaGraphicXml(textWriter);

                LogicRectangle pageBounds1 = new LogicRectangle();
                pageBounds1.Location = new PointF(30, 30);
                pageBounds1.Size = new SizeF(sheet.Width - 60f, sheet.Height - 60f);
                pageBounds1.LineWidth = 2;
                pageBounds1.GetXinhuaGraphicXml(textWriter);

                foreach (object obj in symbols)
                {
                    ((ILogicGraphicFormat)obj).GetXinhuaGraphicXml(textWriter);
                }
                textWriter.WriteEndElement();
                textWriter.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public static string GetAddr(string addr)
        {
            int addrVal = Convert.ToInt32(addr);
            if (addrVal > 65535)
            {
                return "??-??-???";
            }

            int stationID = addrVal >> 12;
            int cardID = addrVal & 0x0F00 >> 8;
            int chanelID = addrVal & 0x00FF;

            return string.Format("{0}-{1}-{2}", stationID, cardID, chanelID);
        }


        /// <summary>
        /// XAI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXAIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：1 + 1； 输出：1； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = new SizeF(75f, 28f);

            LogicRectangle bodyRectangle = new LogicRectangle();
            bodyRectangle.Location = block.Location;
            bodyRectangle.Size = new SizeF(67.5f, 15f);

            // 右端的半圆


            LogicArc bodyArc = new LogicArc();
            bodyArc.CentrePoint = new PointF(block.X + 67.5f, block.Y + 7.5f);
            bodyArc.Radius = 7.5f;
            bodyArc.StartAngle = -90;
            bodyArc.EndAngle = 90;

            // GID，块中间显示的文本


            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(bodyRectangle.X + 2f, bodyRectangle.Y + 2f);
            bodyText.Width = bodyRectangle.Width - 4f;
            bodyText.Height = bodyRectangle.Height - 4f;

            // Addr，块下方显示的参数文本(数据存储位置不确定) //WangXiang
            LogicText parameterText = new LogicText();
            parameterText.Location = new PointF(bodyRectangle.X + 2f, bodyRectangle.Y + bodyRectangle.Height + 1f);
            parameterText.Width = bodyText.Width;
            parameterText.Height = 11f;
            parameterText.Alignment = Align.TopCenter;

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "X")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_7.5";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
                else if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            LogicPin outputPin = null;
            foreach (Cld_FCOutput pin in block.Cld_FCOutput_List)
            {
                if (pin.PinName == "Y")
                {
                    pin.Point = "75_7.5";
                    outputPin = new LogicPin(pin, symbolTemp);
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Addr")
                {
                    parameterText.Text = GetAddr(para.PValue);
                }
            }

            symbolTemp.Graphics.Add(bodyRectangle);
            symbolTemp.Graphics.Add(bodyArc);
            symbolTemp.Graphics.Add(bodyText);
            symbolTemp.Graphics.Add(parameterText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgAIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XAO
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXAOBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：1 + 1； 输出：0； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = new SizeF(75f, 28f);

            LogicRectangle bodyRectangle = new LogicRectangle();
            bodyRectangle.Location = block.Location;
            bodyRectangle.Size = new SizeF(67.5f, 15f);

            // 右端的半圆


            LogicArc bodyArc = new LogicArc();
            bodyArc.CentrePoint = new PointF(block.X + 67.5f, block.Y + 7.5f);
            bodyArc.Radius = 7.5f;
            bodyArc.StartAngle = -90;
            bodyArc.EndAngle = 90;

            // GID，块中间显示的文本


            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(bodyRectangle.X + 2f, bodyRectangle.Y + 2f);
            bodyText.Width = bodyRectangle.Width - 4f;
            bodyText.Height = bodyRectangle.Height - 4f;

            // Addr，块下方显示的参数文本(数据存储位置不确定) //WangXiang
            LogicText parameterText = new LogicText();
            parameterText.Location = new PointF(bodyRectangle.X + 2f, bodyRectangle.Y + bodyRectangle.Height + 1f);
            parameterText.Width = bodyText.Width;
            parameterText.Height = 11f;
            parameterText.Alignment = Align.TopCenter;

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "X")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_7.5";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
                else if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Addr")
                {
                    parameterText.Text = GetAddr(para.PValue);
                }
            }

            symbolTemp.Graphics.Add(bodyRectangle);
            symbolTemp.Graphics.Add(bodyArc);
            symbolTemp.Graphics.Add(bodyText);
            symbolTemp.Graphics.Add(parameterText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgAIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XDI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXDIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：0 + 1； 输出：1； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = new SizeF(75f, 28f);

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] {
                symbolTemp.Location,
                new PointF(block.X + block.Width - block.Height / 2, block.Y),
                new PointF(block.X + block.Width, block.Y + block.Height / 2),
                new PointF(block.X + block.Width - block.Height / 2, block.Y + block.Height),
                new PointF(block.X, block.Y + block.Height) };

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(block.X + 2f, block.Y + 2f);
            bodyText.Width = block.Width - block.Height / 2 - 4f;
            bodyText.Height = block.Height - 4f;

            LogicText parameterText = new LogicText();
            parameterText.Location = new PointF(block.X, block.Y + block.Height + 1f);
            parameterText.Width = block.Width;
            parameterText.Height = 11f;
            parameterText.Alignment = Align.TopCenter;

            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            LogicPin outputPin = null;
            foreach (Cld_FCOutput pin in block.Cld_FCOutput_List)
            {
                if (pin.PinName == "D")
                {
                    pin.Point = "75_7.5";
                    outputPin = new LogicPin(pin, symbolTemp);
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Addr")
                {
                    parameterText.Text = GetAddr(para.PValue);
                }
            }

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(bodyText);
            symbolTemp.Graphics.Add(parameterText);
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgDIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XDO
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXDOBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：1 + 1； 输出：0； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = new SizeF(75f, 28f);

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] {
                symbolTemp.Location,
                new PointF(block.X + block.Width - block.Height / 2, block.Y),
                new PointF(block.X + block.Width, block.Y + block.Height / 2),
                new PointF(block.X + block.Width - block.Height / 2, block.Y + block.Height),
                new PointF(block.X, block.Y + block.Height) };

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(block.X + 2f, block.Y + 2f);
            bodyText.Width = block.Width - block.Height / 2 - 4f;
            bodyText.Height = block.Height - 4f;

            LogicText parameterText = new LogicText();
            parameterText.Location = new PointF(block.X, block.Y + block.Height + 1f);
            parameterText.Width = block.Width;
            parameterText.Height = 11f;
            parameterText.Alignment = Align.TopCenter;

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "Z")
                {
                    pin.Point = "0_7.5";
                    inputPin = new LogicPin(pin, symbolTemp);
                }
                else if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Addr")
                {
                    parameterText.Text = GetAddr(para.PValue);
                }
            }

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(bodyText);
            symbolTemp.Graphics.Add(parameterText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgDIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XPI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXPIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：1 + 1； 输出：1； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = new SizeF(75f, 28f);

            LogicRectangle bodyRectangle = new LogicRectangle();
            bodyRectangle.Location = block.Location;
            bodyRectangle.Size = new SizeF(67.5f, 15f);

            // 右端的半圆


            LogicArc bodyArc = new LogicArc();
            bodyArc.CentrePoint = new PointF(block.X + 67.5f, block.Y + 7.5f);
            bodyArc.Radius = 7.5f;
            bodyArc.StartAngle = -90;
            bodyArc.EndAngle = 90;

            // GID，块中间显示的文本


            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(bodyRectangle.X + 2f, bodyRectangle.Y + 2f);
            bodyText.Width = bodyRectangle.Width - 4f;
            bodyText.Height = bodyRectangle.Height - 4f;

            // Addr，块下方显示的参数文本(数据存储位置不确定) //WangXiang
            LogicText parameterText = new LogicText();
            parameterText.Location = new PointF(bodyRectangle.X + 2f, bodyRectangle.Y + bodyRectangle.Height + 1f);
            parameterText.Width = bodyText.Width;
            parameterText.Height = 11f;
            parameterText.Alignment = Align.TopCenter;

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "Rst")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_7.5";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
                else if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            LogicPin outputPin = null;
            foreach (Cld_FCOutput pin in block.Cld_FCOutput_List)
            {
                if (pin.PinName == "Y")
                {
                    pin.Point = "75_7.5";
                    outputPin = new LogicPin(pin, symbolTemp);
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Addr")
                {
                    parameterText.Text = GetAddr(para.PValue);
                }
            }

            symbolTemp.Graphics.Add(bodyRectangle);
            symbolTemp.Graphics.Add(bodyArc);
            symbolTemp.Graphics.Add(bodyText);
            symbolTemp.Graphics.Add(parameterText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgAIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XNetAI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXNetAIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：0 + 1； 输出：1； 参数：2

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = block.Size;

            LogicRoundRectangle bodyRoundRectangle = new LogicRoundRectangle();
            bodyRoundRectangle.Location = block.Location;
            bodyRoundRectangle.Size = symbolTemp.Size;
            bodyRoundRectangle.XRadius = 7.5f;
            bodyRoundRectangle.YRadius = 7.5f;

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(symbolTemp.X + 8f, symbolTemp.Y + 2f);
            bodyText.Width = symbolTemp.Width - 16f;
            bodyText.Height = 11f;

            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            LogicPin outputPin = null;
            foreach (Cld_FCOutput pin in block.Cld_FCOutput_List)
            {
                if (pin.PinName == "Y")
                {
                    pin.Point = "75_7.5";
                    outputPin = new LogicPin(pin, symbolTemp);
                }
            }

            symbolTemp.Graphics.Add(bodyRoundRectangle);
            symbolTemp.Graphics.Add(bodyText);
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            LogicPoke poke = NewSymbolPoke(block, bodyText, null);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);
        }

        /// <summary>
        /// XNetAO
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXNetAOBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：1 + 1； 输出：0； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Size = block.Size;
            symbolTemp.Location = block.Location;

            LogicRoundRectangle bodyRoundRectangle = new LogicRoundRectangle();
            bodyRoundRectangle.Location = block.Location;
            bodyRoundRectangle.Size = symbolTemp.Size;
            bodyRoundRectangle.XRadius = 7.5f;
            bodyRoundRectangle.YRadius = 7.5f;

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(symbolTemp.X + 8f, symbolTemp.Y + 2f);
            bodyText.Width = symbolTemp.Width - 16f;
            bodyText.Height = symbolTemp.Height - 4f;

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "X")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_7.5";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
                else if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            symbolTemp.Graphics.Add(bodyRoundRectangle);
            symbolTemp.Graphics.Add(bodyText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgAIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XNetDI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXNetDIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：0 + 1； 输出：1； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = block.Size;

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] { 
                new PointF(block.X, block.Y + block.Height / 2),
                new PointF(block.X + block.Height / 2, block.Y),
                new PointF(block.X + block.Width - block.Height / 2, block.Y),
                new PointF(block.X + block.Width, block.Y + block.Height / 2),
                new PointF(block.X + block.Width - block.Height / 2, block.Y + block.Height),
                new PointF(block.X + block.Height / 2, block.Y + block.Height)};

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(block.X + 8f, block.Y + 2f);
            bodyText.Width = block.Width - 16f;
            bodyText.Height = block.Height - 4f;

            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            LogicPin outputPin = null;
            foreach (Cld_FCOutput pin in block.Cld_FCOutput_List)
            {
                if (pin.PinName == "D")
                {
                    pin.Point = "75_7.5";
                    outputPin = new LogicPin(pin, symbolTemp);
                }
            }

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(bodyText);
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            LogicPoke poke = NewSymbolPoke(block, bodyText, null);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);
        }

        /// <summary>
        /// XNetDO
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXNetDOBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：75 x 15
            // 输入：1； 输出：0 + 1； 参数：n

            block.Size = new SizeF(75f, 15f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = block.Size;

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] { 
                new PointF(block.X, block.Y + block.Height / 2),
                new PointF(block.X + block.Height / 2, block.Y),
                new PointF(block.X + block.Width - block.Height / 2, block.Y),
                new PointF(block.X + block.Width, block.Y + block.Height / 2),
                new PointF(block.X + block.Width - block.Height / 2, block.Y + block.Height),
                new PointF(block.X + block.Height / 2, block.Y + block.Height)};

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(block.X + 8f, block.Y + 2f);
            bodyText.Width = block.Width - 16f;
            bodyText.Height = block.Height - 4f;

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "Z")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_7.5";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
                else if (pin.PinName == "GID")
                {
                    if (pin.PointName != null && pin.PointName != "?")
                    {
                        bodyText.Text = pin.PointName;
                    }
                    else
                    {
                        bodyText.Text = "Null";
                    }
                }
            }

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(bodyText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);
            LogicPoke poke = NewSymbolPoke(block, bodyText, referenceBlocks);
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgDIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XPgAI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXPgAIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：30 x 30
            // 输入：0； 输出：1； 参数：2

            block.Size = new SizeF(30f, 30f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = PointF.Subtract(block.Location, new SizeF(60f, 0f));
            symbolTemp.Size = SizeF.Add(block.Size, new SizeF(60f, 0f));

            LogicCircle bodyCircle = new LogicCircle();
            bodyCircle.CentrePoint = new PointF(block.X + 15f, block.Y + 15f);
            bodyCircle.Radius = 15f;

            LogicLine bodyLine = new LogicLine();
            bodyLine.Point1 = new PointF(block.X, block.Y + 15f);
            bodyLine.Point2 = new PointF(block.X + 30f, block.Y + 15f);

            LogicText pageIndexText = new LogicText();
            pageIndexText.Location = new PointF(block.X + 5f, block.Y + 3f);
            pageIndexText.Width = 20f;
            pageIndexText.Height = 11f;
            pageIndexText.Text = "Null";

            LogicText blockIndexText = new LogicText();
            blockIndexText.Location = new PointF(block.X + 5f, block.Y + 16f);
            blockIndexText.Width = 20f;
            blockIndexText.Height = 11f;
            blockIndexText.Text = "Null";

            LogicText textPointName = new LogicText();
            textPointName.Location = new PointF(block.X - 60f, bodyCircle.Point1.Y + 2f);
            textPointName.Width = 58f;
            textPointName.Height = 11f;
            textPointName.Alignment = Align.BottomRight;

            LogicPin outputPin = null;
            foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
            {
                if (output.PinName == "Y")
                {
                    output.Point = "30_15";
                    outputPin = new LogicPin(output, symbolTemp);
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Page" && para.PValue != null)
                {
                    int pageIndex = Convert.ToInt32(para.PValue);
                    if (pageIndex > 0 && pageIndex < 65535)
                    {
                        pageIndexText.Text = para.PValue;
                    }
                    else
                    {
                        pageIndexText.Text = "?";
                    }
                }
                else if (para.Name == "Block" && para.PValue != null)
                {
                    int blockIndex = Convert.ToInt32(para.PValue);
                    if (blockIndex > 0 && blockIndex < 65535)
                    {
                        blockIndexText.Text = para.PValue;
                    }
                    else
                    {
                        blockIndexText.Text = "?";
                    }
                }
            }

            string[] functons = new string[] { "XAI", "XAO", "XPI", "XNetAO", "XPgAO" };
            IList<string> canReferenceFunctions = new List<string>(functons);
            Cld_FCBlock referenceBlock = CrossReference.GetInputReference(block);
            if (referenceBlock != null)
            {
                if (canReferenceFunctions.Contains(referenceBlock.FunctionName))
                {
                    if (referenceBlock.FunctionName != "XPgAO")
                    {
                        foreach (Cld_FCInput pin in referenceBlock.Cld_FCInput_List)
                        {
                            if (pin.PinName == "GID" && pin.PointName != null && pin.PointName != "?")
                            {
                                textPointName.Text = pin.PointName;
                            }
                        }

                        if (textPointName.Text.Length == 0)
                        {
                            textPointName.Text = "NoAxTag";     // 引用块名称为空(不存在)，GID == NULL
                        }
                    }
                }
                else
                {
                    textPointName.Text = "RefError";        // 引用错误
                    textPointName.ForeColor = Color.Red;
                }
            }
            else
            {
                textPointName.Text = "RefNoExist";          // 引用块不存在
                textPointName.ForeColor = Color.Red;
            }

            symbolTemp.Graphics.Add(bodyCircle);
            symbolTemp.Graphics.Add(bodyLine);
            symbolTemp.Graphics.Add(pageIndexText);
            symbolTemp.Graphics.Add(blockIndexText);
            symbolTemp.Graphics.Add(textPointName);
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            LogicPoke poke = new LogicPoke(block);
            if (referenceBlock != null)
            {
                DynCrossReference refenence = new DynCrossReference();
                refenence.AddReference(referenceBlock);
                poke.Dynamics.Add(refenence);
            }
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);
        }

        /// <summary>
        /// XPgAO
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXPgAOBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：30 x 30
            // 输入：1； 输出：0； 参数：0

            block.Size = new SizeF(30f, 30f);

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = block.Size;

            LogicCircle bodyCircle = new LogicCircle();
            bodyCircle.CentrePoint = new PointF(block.X + 15f, block.Y + 15f);
            bodyCircle.Radius = 15f;

            LogicText blockIndexText = new LogicText();
            blockIndexText.Location = new PointF(block.X + 5f, block.Y + 10f);
            blockIndexText.Width = 20f;
            blockIndexText.Height = 11f;
            blockIndexText.Text = block.AlgName.Substring(block.AlgName.LastIndexOf('-') + 1);

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "X")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_15";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
            }

            symbolTemp.Graphics.Add(bodyCircle);
            symbolTemp.Graphics.Add(blockIndexText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);

            LogicPoke poke = new LogicPoke(block);
            if (referenceBlocks.Count > 0)
            {
                DynCrossReference reference = new DynCrossReference();
                foreach (Cld_FCBlock refBlock in referenceBlocks)
                {
                    reference.AddReference(refBlock);
                }
                poke.Dynamics.Add(reference);
            }
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgAIReferer(block, i, referenceBlocks[i], symbols);
                }
            }
        }

        /// <summary>
        /// XPgDI
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXPgDIBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：35 x 30
            // 输入：0； 输出：1； 参数：2

            block.Size = new SizeF(35f, 30f);
            float edgeLength = 17.5f;             // 正六边形的边长


            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = PointF.Subtract(block.Location, new SizeF(60f, 0f));
            symbolTemp.Size = SizeF.Add(block.Size, new SizeF(60f, 0f));

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] { 
                new PointF(block.X, block.Y + 15f),
                new PointF(block.X + edgeLength / 2, block.Y),
                new PointF((float)(block.X + edgeLength * 1.5), block.Y),
                new PointF(block.X + 35f, block.Y + 15f),
                new PointF((float)(block.X + edgeLength * 1.5), block.Y + 30f),
                new PointF((float)(block.X + edgeLength / 2), block.Y + 30f)};

            LogicLine bodyLine = new LogicLine();
            bodyLine.Point1 = bodyPolygon.Points[0];
            bodyLine.Point2 = bodyPolygon.Points[3];

            LogicText pageIndexText = new LogicText();
            pageIndexText.Location = new PointF(block.X + 8f, block.Y + 3f);
            pageIndexText.Width = 19f;
            pageIndexText.Height = 11f;

            LogicText blockIndexText = new LogicText();
            blockIndexText.Location = new PointF(block.X + 8f, block.Y + 16f);
            blockIndexText.Width = 19f;
            blockIndexText.Height = 11f;

            string[] id = block.AlgName.Split('-');
            pageIndexText.Text = id[1];
            blockIndexText.Text = id[2];

            LogicText textPointName = new LogicText();
            textPointName.Location = new PointF(block.X - 60f, block.Y + 2f);
            textPointName.Width = 58f;
            textPointName.Height = 11f;
            textPointName.Alignment = Align.BottomRight;

            LogicPin outputPin = null;
            foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
            {
                if (output.PinName == "D")
                {
                    output.Point = "35_15";
                    outputPin = new LogicPin(output, symbolTemp);
                }
            }

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Page")
                {
                    if (para.PValue != null)
                    {
                        int pageIndex = Convert.ToInt32(para.PValue);
                        if (pageIndex > 0 && pageIndex < 65535)
                        {
                            pageIndexText.Text = para.PValue;
                        }
                        else
                        {
                            pageIndexText.Text = "?";
                        }
                    }
                    else
                    {
                        pageIndexText.Text = "?";
                    } 
                }
                else if (para.Name == "Block")
                {
                    if (para.PValue != null)
                    {
                        int blockIndex = Convert.ToInt32(para.PValue);
                        if (blockIndex > 0 && blockIndex < 65535)
                        {
                            blockIndexText.Text = para.PValue;
                        }
                        else
                        {
                            blockIndexText.Text = "?";
                        } 
                    }
                    else
                    {
                        blockIndexText.Text = "?";
                    }
                }
            }

            string[] functions = new string[] { "XDI", "XDO", "XNetDO", "XPgDO" };
            IList<string> canReferenceFunctions = new List<string>(functions);
            Cld_FCBlock referenceBlock = CrossReference.GetInputReference(block);
            if (referenceBlock != null)
            {
                if (canReferenceFunctions.Contains(referenceBlock.FunctionName))
                {
                    if (referenceBlock.FunctionName != "XPgDO")
                    {
                        foreach (Cld_FCInput pin in referenceBlock.Cld_FCInput_List)
                        {
                            if (pin.PinName == "GID" && pin.PointName != null && pin.PointName != "?")
                            {
                                textPointName.Text = pin.PointName;
                            }
                        }

                        if (textPointName.Text.Length == 0)
                        {
                            textPointName.Text = "NoDxTag";     // 引用块名称为空(不存在)，GID == NULL
                        }
                    }
                }
                else
                {
                    textPointName.Text = "RefError";        // 引用错误
                    textPointName.ForeColor = Color.Red;
                }
            }
            else
            {
                textPointName.Text = "RefNoExist";          // 引用块不存在
                textPointName.ForeColor = Color.Red;
            }

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(bodyLine);
            symbolTemp.Graphics.Add(pageIndexText);
            symbolTemp.Graphics.Add(blockIndexText);
            symbolTemp.Graphics.Add(textPointName);
            if (outputPin != null)
            {
                symbolTemp.Graphics.Add(outputPin);
            }

            LogicPoke poke = new LogicPoke(block);
            if (referenceBlock != null)
            {
                DynCrossReference refenence = new DynCrossReference();
                refenence.AddReference(referenceBlock);
                poke.Dynamics.Add(refenence);
            }
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);
        }

        /// <summary>
        /// XPgDO
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        /// <param name="bll"></param>
        private static void GenerateXPgDOBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // Body大小：35 x 30
            // 输入：1； 输出：0； 参数：0

            block.Size = new SizeF(35f, 30f);
            float edgeLength = 17.5f;

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = block.Size;

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] { 
                new PointF(block.X, block.Y + 15f),
                new PointF(block.X + edgeLength / 2, block.Y),
                new PointF((float)(block.X + edgeLength * 1.5), block.Y),
                new PointF(block.X + 35f, block.Y + 15f),
                new PointF((float)(block.X + edgeLength * 1.5), block.Y + 30f),
                new PointF((float)(block.X + edgeLength / 2), block.Y + 30f)};

            LogicText bodyText = new LogicText();
            bodyText.Location = new PointF(block.X + 8f, block.Y + 10f);
            bodyText.Width = 19f;
            bodyText.Height = 11f;
            bodyText.Text = block.AlgName.Substring(block.AlgName.LastIndexOf('-') + 1);

            LogicPin inputPin = null;
            foreach (Cld_FCInput pin in block.Cld_FCInput_List)
            {
                if (pin.PinName == "Z")
                {
                    if (pin.Visible || (pin.PointName != null && LogicPin.RegPointName.IsMatch(pin.PointName)))
                    {
                        pin.Point = "0_15";
                        inputPin = new LogicPin(pin, symbolTemp);
                    }
                }
            }

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(bodyText);
            if (inputPin != null)
            {
                symbolTemp.Graphics.Add(inputPin);
            }

            IList<Cld_FCBlock> referenceBlocks = CrossReference.GetOutputReference(block);

            LogicPoke poke = new LogicPoke(block);
            if (referenceBlocks.Count > 0)
            {
                DynCrossReference reference = new DynCrossReference();
                foreach (Cld_FCBlock refBlock in referenceBlocks)
                {
                    reference.AddReference(refBlock);
                }
                poke.Dynamics.Add(reference);
            }
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);

            if (referenceBlocks.Count > 0)
            {
                for (int i = 0; i < referenceBlocks.Count; i++)
                {
                    GenerateXPgDIReferer(block, i, referenceBlocks[i], symbols);
                }
            }

        }


        private static void GenerateXPgAIReferer(Cld_FCBlock block, int refererIndex, Cld_FCBlock refBlock, ArrayList symbols)
        {
            // 大小：30 x 30

            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = new PointF((float)(block.X + block.Width + refererIndex * 30), block.Y + block.Height / 2 - 15f);
            symbolTemp.Size = new SizeF(30f, 30f);

            LogicCircle bodyCircle = new LogicCircle();
            bodyCircle.CentrePoint = new PointF(symbolTemp.X + 15f, symbolTemp.Y + 15f);
            bodyCircle.Radius = 15f;

            LogicText pageIndexText = new LogicText();
            pageIndexText.Location = new PointF(symbolTemp.X + 5f, symbolTemp.Y + 3f);
            pageIndexText.Width = 20f;
            pageIndexText.Height = 11f;

            LogicText blockIndexText = new LogicText();
            blockIndexText.Location = new PointF(symbolTemp.X + 5f, symbolTemp.Y + 16f);
            blockIndexText.Width = 20f;
            blockIndexText.Height = 11f;

            string[] ids = refBlock.AlgName.Split('-');
            pageIndexText.Text = ids[1];
            blockIndexText.Text = ids[2];

            symbolTemp.Graphics.Add(bodyCircle);
            symbolTemp.Graphics.Add(pageIndexText);
            symbolTemp.Graphics.Add(blockIndexText);

            symbols.Add(symbolTemp);
        }

        private static void GenerateXPgDIReferer(Cld_FCBlock block, int refererIndex, Cld_FCBlock refBlock, ArrayList symbols)
        {
            // 大小：35 x 30

            float edgeLength = 17.5f;             // 正六边形的边长



            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.IOSymbol);
            symbolTemp.Location = new PointF((float)(block.X + block.Width + refererIndex * 35), block.Y + block.Height / 2 - 15f);
            symbolTemp.Size = new SizeF(35f, 30f);

            LogicPolygon bodyPolygon = new LogicPolygon();
            bodyPolygon.Points = new PointF[] { 
                new PointF(symbolTemp.X, symbolTemp.Y + 15f),
                new PointF(symbolTemp.X + edgeLength / 2, symbolTemp.Y),
                new PointF((float)(symbolTemp.X + edgeLength * 1.5), symbolTemp.Y),
                new PointF(symbolTemp.X + 35f, symbolTemp.Y + 15f),
                new PointF((float)(symbolTemp.X + edgeLength * 1.5), symbolTemp.Y + 30f),
                new PointF((float)(symbolTemp.X + edgeLength / 2), symbolTemp.Y + 30f)};


            LogicText pageIndexText = new LogicText();
            pageIndexText.Location = new PointF(symbolTemp.X + 8f, symbolTemp.Y + 3f);
            pageIndexText.Width = 19f;
            pageIndexText.Height = 11f;

            LogicText blockIndexText = new LogicText();
            blockIndexText.Location = new PointF(symbolTemp.X + 8f, symbolTemp.Y + 16f);
            blockIndexText.Width = 19f;
            blockIndexText.Height = 11f;

            string[] ids = refBlock.AlgName.Split('-');
            pageIndexText.Text = ids[1];
            blockIndexText.Text = ids[2];

            symbolTemp.Graphics.Add(bodyPolygon);
            symbolTemp.Graphics.Add(pageIndexText);
            symbolTemp.Graphics.Add(blockIndexText);

            symbols.Add(symbolTemp);
        }

        private static LogicPoke NewSymbolPoke(Cld_FCBlock block, LogicText bodyText, IList<Cld_FCBlock> referenceBlocks)
        {
            IList<Cld_FCBlock> loopReferenceBlocks = bodyText.Text != "Null" ? CrossReference.GetLoopReference(bodyText.Text, block.ID) : null;
            DynCrossReference reference = null;
            if (referenceBlocks != null && referenceBlocks.Count > 0)
            {
                reference = new DynCrossReference();
                foreach (Cld_FCBlock refBlock in referenceBlocks)
                {
                    reference.AddReference(refBlock);
                }
            }
            if (loopReferenceBlocks != null && loopReferenceBlocks.Count > 0)
            {
                if (reference == null)
                {
                    reference = new DynCrossReference();
                }
                foreach (Cld_FCBlock refBlock in loopReferenceBlocks)
                {
                    reference.AddLoopReference(refBlock);
                }
            }

            LogicPoke poke = new LogicPoke(block);
            if (reference != null)
            {
                poke.Dynamics.Add(reference);
            }
            return poke;
        }


        /// <summary>
        /// 根据FCBlock构造Symbol
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="fcblock"></param>
        private static void GenerateCommonBlock(Cld_FCBlock block, ArrayList symbols)
        {
            // 开始构造一个Symbol
            LogicSymbol symbolTemp = new LogicSymbol(LogicSymbolType.GeneralSymbol);
            symbolTemp.Location = block.Location;
            symbolTemp.Size = block.Size;

            // 主体矩形
            LogicRectangle symbolBody = new LogicRectangle();
            symbolBody.Location = block.Location;
            symbolBody.Size = block.Size;
            symbolTemp.Graphics.Add(symbolBody);

            #region 模块名称(内部，上中)

            // SymbolName
            LogicText symbolNameText = new LogicText();
            symbolNameText.Location = new PointF(block.X + 2f, block.Y + 1f);
            symbolNameText.Width = block.Width - 4f;
            symbolNameText.Height = LogicSymbol.HeadHeight - 3f;
            symbolNameText.Text = block.FunctionName;
            symbolNameText.Font = new Font(LogicText.DefaultFamily, 12, FontStyle.Bold);
            symbolNameText.Alignment = Align.TopCenter;
            symbolNameText.ForeColor = Color.Black;
            symbolTemp.Graphics.Add(symbolNameText);

            #endregion

            #region I/O引脚

            // 遍历输入引脚
            foreach (Cld_FCInput input in block.Cld_FCInput_List)
            {
                bool isConnected = input.PointName != null ? LogicPin.RegPointName.IsMatch(input.PointName) : false;
                if (input.Visible || isConnected)
                {
                    LogicPin pin = new LogicPin(input, symbolTemp);
                    symbolTemp.Graphics.Add(pin);

                    symbolTemp.InputCount++;
                }
            }

            // 遍历输出引脚
            foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
            {
                bool enable = false;
                if (!output.Visible)
                {
                    foreach (Cld_FCInput input in block.Cld_FCInput_List)
                    {
                        if (input.Visible && input.PointName == output.PointName)
                        {
                            enable = true;
                            break;
                        }
                    }
                }
                if (output.Visible || enable)
                {
                    LogicPin pin = new LogicPin(output, symbolTemp);
                    symbolTemp.Graphics.Add(pin);
                    symbolTemp.OutputCount++;
                }
            }
            #endregion

            #region 输出块号

            // 块号:序号
            LogicText symbolIndexText = new LogicText();
            symbolIndexText.Location = new PointF(block.X + 2f, block.Y + block.Height - LogicSymbol.FootHeight + 2f);
            symbolIndexText.Width = block.Width - 4f;
            symbolIndexText.Height = LogicSymbol.FootHeight - 3f;
            symbolIndexText.Text = block.AlgName.Substring(block.AlgName.LastIndexOf('-') + 1) + ":" + block.Sequence;
            symbolTemp.Graphics.Add(symbolIndexText);

            #endregion

            #region 输出块的参数信息

            IList fcParameters = block.Cld_FCParameter_List;
            if (fcParameters.Count > 0)
            {
                symbolTemp.Height += LogicSymbol.ParaHeight;
                LogicText symbolParaText = new LogicText();
                symbolParaText.Location = new PointF(block.X, block.Y + block.Height + 1f);
                symbolParaText.Width = block.Width;
                symbolParaText.Height = LogicSymbol.ParaHeight - 2f;
                symbolParaText.Alignment = Align.TopCenter;
                symbolParaText.Text = "";

                foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
                {
                    if (para.Name == "Num")
                    {
                        if (symbolParaText.Text.Length == 0)
                        {
                            symbolParaText.Text = "Num=" + para.PValue;
                        }
                        else
                        {
                            symbolParaText.Text = "Num=" + para.PValue + "," + symbolParaText.Text;
                        }
                    }
                    if (para.Name == "Mode")
                    {
                        if (symbolParaText.Text.Length == 0)
                        {
                            symbolParaText.Text = "Num=" + para.PValue;
                        }
                        else
                        {
                            symbolParaText.Text += ",Num=" + para.PValue;
                        }
                    }
                }

                symbolTemp.Graphics.Add(symbolParaText);
            }

            #endregion

            LogicPoke poke = new LogicPoke(block);
            // 操作方式
            symbolTemp.Graphics.Add(poke);

            symbols.Add(symbolTemp);
        }

        private static void GenerateText(Cld_Graphic graphic, ArrayList symbols)
        {
            string staticAttributeString;
            if (graphic.Data.Contains("$$"))
            {
                staticAttributeString = graphic.Data.Substring(graphic.Data.IndexOf("$$") + 2);
            }
            else
            {
                staticAttributeString = graphic.Data;
            }

            string[] staticAttributes = staticAttributeString.Split(new string[] { "||" }, StringSplitOptions.None);

            LogicText text = new LogicText();
            text.Text = staticAttributes[0];
            if (staticAttributes[0].StartsWith("&"))
            {
                DynTagDef tagDef = new DynTagDef(staticAttributes[0].Substring(1), 1);
                text.Dynamics.Add(tagDef);
            }
            string[] location = staticAttributes[2].Split('_');
            text.Location = new PointF(Convert.ToSingle(location[0]), Convert.ToSingle(location[1]));
            text.Angle = Convert.ToInt32(staticAttributes[3]);
            FontStyle style = FontStyle.Regular;
            if (staticAttributes.Length > 7)
            {
                if (staticAttributes[7] == "1")
                    style = FontStyle.Bold;
                if (staticAttributes[8] == "1")
                    style |= FontStyle.Italic;
                if (staticAttributes[9] == "1")
                    style |= FontStyle.Underline;
            }
            text.Font = new Font(staticAttributes[4], Convert.ToSingle(staticAttributes[5]), style);

            string[] color = staticAttributes[6].Split('_');
            text.ForeColor = Color.FromArgb(Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2]));


            //if (staticAttributes[1] == "左上")
            //{
            //    text.Alignment = Align.TopLeft;
            //}
            //else if (staticAttributes[1] == "中上")
            //{
            //    text.Alignment = Align.TopCenter;
            //}
            //else if (staticAttributes[1] == "右上")
            //{
            //    text.Alignment = Align.TopRight;
            //}
            //else if (staticAttributes[1] == "左中")
            //{
            //    text.Alignment = Align.MiddleLeft;
            //}
            //else if (staticAttributes[1] == "居中")
            //{
            //    text.Alignment = Align.MiddleCenter;
            //}
            //else if (staticAttributes[1] == "右中")
            //{
            //    text.Alignment = Align.MiddleRight;
            //}
            //else if (staticAttributes[1] == "左下")
            //{
            //    text.Alignment = Align.BottomLeft;
            //}
            //else if (staticAttributes[1] == "中下")
            //{
            //    text.Alignment = Align.BottomCenter;
            //}
            //else if (staticAttributes[1] == "右下")
            //{
            //    text.Alignment = Align.BottomRight;
            //}
            //else
            //{
            //    throw new Exception("对齐方式不确定！");
            //}

            symbols.Add(text);
        }
    }
}
