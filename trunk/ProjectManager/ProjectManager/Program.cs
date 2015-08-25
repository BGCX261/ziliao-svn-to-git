using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using System.Xml;
using System.Reflection;
using ProjectManager;
using TDK.Core.Logic.BLL;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using TDK.Core.Logic.DAL;
using TDK.Core.Logic.Util;
using DejaVu;

namespace TDK.Core.Logic
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SessionManager sessionmanager = new SessionManager("hibernate_config.xml");
            ISession session = sessionmanager.GetSession();
            // 相关操作的接口
            PrjManager manager = new PrjManager(session);
            //add your code bellow

            Prj_Sheet sheet = manager.SheetCRUD.GetPrj_Sheet_By_ID(202);

            Cld_FCBlock temp = sheet.New_Cld_FCBlock();
            temp.AlgName = "weifdajflkjdlkajfkldjlkajfkdjkaljfkdjajfldjalfjlkajfldjlak";
            manager.persistence_save(temp);




            //释放相关资源
            manager.Close();
            Console.WriteLine("\nPress Enter to Exit !");
            Console.ReadKey();


            //以下为GUI运行
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Form1());


        }


        public static void nhibernate_enumerable_test(PrjManager manager){
            IEnumerable<Cld_FCParameter> paras = manager.session.CreateQuery("select from Cld_FCParameter c")
                .Enumerable<Cld_FCParameter>();
            
            foreach(Cld_FCParameter p in paras){
                Console.WriteLine(p.Name);
            }
        }

        private static void load_commit_test(PrjManager manager)
        {
            Prj_Sheet sheet = manager.SheetCRUD.Load_Sheet(2316);
            Prj_Sheet backup = ObjectCopier.Clone(sheet);


            Cld_FCBlock block = sheet.New_Cld_FCBlock();
            block.AlgName = "added";
            block.Sequence = 12;

            Cld_FCInput inpt = block.New_FCInput();
            inpt.Description = "added desc";
            inpt.PinName = "for test";

            Cld_FCOutput output = block.New_FCOutput();
            output.PinName = "for test output";


            SheetDiffer diff = manager.SheetCRUD.CompareSheet(backup, sheet);
            manager.SheetCRUD.Commit_Sheet(diff);
        }
        /// <summary>
        /// Prj_Sheet To Xml
        /// </summary>
        /// <param name="sheet"></param>
        public static void Generate_Sheet_Xml(Prj_Sheet sheet, BllManager bll)
        {
            //因为此方法采用反射来获取类中的所有共有属性进行输出，因此这里屏蔽对象中的某些不需要输出的属性
            List<string> myclass = new List<string>();
            myclass.Add("Prj_Project");
            myclass.Add("Prj_Network");
            myclass.Add("Prj_Unit");
            myclass.Add("Prj_Controller");
            myclass.Add("Prj_Document");
            myclass.Add("Prj_Sheet");
            myclass.Add("Cld_FCBlock");
            myclass.Add("Cld_Signal");
            myclass.Add("Cld_Constant");
            myclass.Add("Cld_Graphic");
            myclass.Add("X");
            myclass.Add("Y");
            myclass.Add("PinIndex");

            try
            {
                // 创建XmlTextWriter类的实例对象
                XmlTextWriter textWriter = new XmlTextWriter(sheet.ID.ToString() + ".xml", null);
                textWriter.Formatting = Formatting.Indented;

                // 开始写过程，调用WriteStartDocument方法
                textWriter.WriteStartDocument();
                // 写入说明
                textWriter.WriteComment("First Comment XmlTextWriter Sample Example");
                textWriter.WriteComment("w3sky.xml in root dir");

                Type sheet_type = typeof(Prj_Sheet);
                Type fcblock_type = typeof(Cld_FCBlock);
                Type fcconst_type = typeof(Cld_Constant);
                Type fcgraphic_type = typeof(Cld_Graphic);
                Type fcsignal_type = typeof(Cld_Signal);
                Type fcinput_type = typeof(Cld_FCInput);
                Type fcoutput_type = typeof(Cld_FCOutput);
                Type fcpara_type = typeof(Cld_FCParameter);



                //创建sheet结点
                textWriter.WriteStartElement("sheet");
                foreach (PropertyInfo pi in sheet_type.GetProperties())
                {
                    object value_object = pi.GetValue(sheet, null);
                    string value_string = "";

                    if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                    {
                        continue;
                    }
                    if (value_object != null)
                    {
                        value_string = value_object.ToString();
                    }
                    textWriter.WriteAttributeString(pi.Name, value_string);
                }


                //fcblock
                foreach (Cld_FCBlock fcblock in sheet.Cld_FCBlock_List)
                {
                    textWriter.WriteStartElement("element");
                    textWriter.WriteAttributeString("type", "Cld_FCBlock");

                    bll.generate_Rec_symbol(fcblock, TDK.Core.Logic.BLL.BllManager.rela_pos.UPLEFT);

                    foreach (PropertyInfo pi in fcblock_type.GetProperties())
                    {
                        object value_object = pi.GetValue(fcblock, null);
                        string value_string = "";

                        if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                        {
                            continue;
                        }
                        if (value_object != null)
                        {
                            value_string = value_object.ToString();
                        }
                        textWriter.WriteAttributeString(pi.Name, value_string);
                    }

                    foreach (Cld_FCInput input in fcblock.Cld_FCInput_List)
                    {
                        textWriter.WriteStartElement("element");
                        textWriter.WriteAttributeString("type", "Cld_FCInput");
                        foreach (PropertyInfo pi in fcinput_type.GetProperties())
                        {
                            object value_object = pi.GetValue(input, null);
                            string value_string = "";

                            if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                            {
                                continue;
                            }
                            if (value_object != null)
                            {
                                value_string = value_object.ToString();
                            }
                            textWriter.WriteAttributeString(pi.Name, value_string);
                        }
                        textWriter.WriteEndElement();
                    }

                    //foreach (Cld_FCOutput output in fcblock.Cld_FCOutput_List)
                    //{
                    //    textWriter.WriteStartElement("element");
                    //    textWriter.WriteAttributeString("type", "FCOutput");
                    //    foreach (PropertyInfo pi in fcoutput_type.GetProperties())
                    //    {
                    //        object value_object = pi.GetValue(output, null);
                    //        string value_string = "";

                    //        if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                    //        {
                    //            continue;
                    //        }
                    //        if (value_object != null)
                    //        {
                    //            value_string = value_object.ToString();
                    //        }
                    //        textWriter.WriteAttributeString(pi.Name, value_string);
                    //    }
                    //    textWriter.WriteEndElement();
                    //}
                    //foreach (Cld_FCParameter para in fcblock.Cld_FCParameter_List)
                    //{
                    //    textWriter.WriteStartElement("element");
                    //    textWriter.WriteAttributeString("type", "Cld_FCParameter");
                    //    foreach (PropertyInfo pi in fcpara_type.GetProperties())
                    //    {
                    //        object value_object = pi.GetValue(para, null);
                    //        string value_string = "";

                    //        if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                    //        {
                    //            continue;
                    //        }
                    //        if (value_object != null)
                    //        {
                    //            value_string = value_object.ToString();
                    //        }
                    //        textWriter.WriteAttributeString(pi.Name, value_string);
                    //    }
                    //    textWriter.WriteEndElement();
                    //}
                    textWriter.WriteEndElement();
                }

                ////const
                //foreach (Cld_Constant cldconst in sheet.Cld_Constant_List)
                //{
                //    textWriter.WriteStartElement("element");
                //    textWriter.WriteAttributeString("type", "Cld_Constant");
                //    foreach (PropertyInfo pi in fcconst_type.GetProperties())
                //    {
                //        object value_object = pi.GetValue(cldconst, null);
                //        string value_string = "";

                //        if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                //        {
                //            continue;
                //        }
                //        if (value_object == null)
                //        {
                //            value_string = value_object.ToString();
                //        }
                //        textWriter.WriteAttributeString(pi.Name, value_string);
                //    }
                //    textWriter.WriteEndElement();
                //}

                ////graphic
                //foreach (Cld_Graphic graphic in sheet.Cld_Graphic_List)
                //{
                //    textWriter.WriteStartElement("element");
                //    textWriter.WriteAttributeString("type", "Cld_Graphic");
                //    foreach (PropertyInfo pi in fcgraphic_type.GetProperties())
                //    {
                //        object value_object = pi.GetValue(graphic, null);
                //        string value_string = "";

                //        if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                //        {
                //            continue;
                //        }
                //        if (value_object == null)
                //        {
                //            value_string = value_object.ToString();
                //        }
                //        textWriter.WriteAttributeString(pi.Name, value_string);
                //    }
                //    textWriter.WriteEndElement();
                //}

                ////signal
                //foreach (Cld_Signal signal in sheet.Cld_Signal_List)
                //{
                //    textWriter.WriteStartElement("element");
                //    textWriter.WriteAttributeString("type", "Cld_Signal");
                //    foreach (PropertyInfo pi in fcsignal_type.GetProperties())
                //    {
                //        object value_object = pi.GetValue(signal, null);
                //        string value_string = "";

                //        if (pi.PropertyType.Name.Equals("IList") || myclass.Contains(pi.PropertyType.Name))
                //        {
                //            continue;
                //        }
                //        if (value_object == null)
                //        {
                //            value_string = value_object.ToString();
                //        }
                //        textWriter.WriteAttributeString(pi.Name, value_string);
                //    }
                //    textWriter.WriteEndElement();
                //}

                textWriter.WriteEndElement();

                // 关闭textWriter
                textWriter.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}