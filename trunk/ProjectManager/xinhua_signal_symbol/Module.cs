using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;

namespace xinhua
{
    //描述平面上的一个点
    public class Point
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public double x;
        /// <summary>
        /// Y坐标
        /// </summary>
        public double y;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool is_valid;
        /// <summary>
        /// 对应的Cld_Module
        /// </summary>
        public Cld_Module_Pin cld_module_pin;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Point()
        {
            cld_module_pin = null;
            is_valid = false;
        }


        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point operator +(Point p1, Point p2)
        {
            Point result = new Point();
            result.x = p1.x + p2.x;
            result.y = p1.y + p2.y;
            result.is_valid = (p1.is_valid && p2.is_valid) ? true : false;
            return result;
        }
    }


    /// <summary>
    /// 对应Meta_FCDetail表
    /// </summary>
    public class Meta_Module_Pin
    {
        /// <summary>
        /// 功能码名称
        /// </summary>
        public string FunctionName;
        /// <summary>
        /// 管脚（或规格数、IO、Tag）名称
        /// </summary>
        public string PinName;
        /// <summary>
        /// 顺序
        /// </summary>
        public string PinIndex;
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType;
        /// <summary>
        /// 是否可调
        /// </summary>
        public bool Tune;
        /// <summary>
        /// 1：Input，2：Constant，3：Output 4:Internal
        /// </summary>
        public string PinType;
        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue;
        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue;
        /// <summary>
        /// 有效值范围
        /// </summary>
        public string ValidValue;
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue;
        /// <summary>
        /// 是否必要
        /// </summary>
        public bool Required;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 地址是否绑定，不用分配
        /// </summary>
        public bool Fixed;
        /// <summary>
        /// 信号类型
        /// </summary>
        public string PinSignalType;
    }//End of Class Meta_Module_Pin



    /// <summary>
    /// 对应Meta_FCMaster表
    /// </summary>
    public class Meta_Module
    {
        /// <summary>
        /// 功能码名称,主键
        /// </summary>
        public string FunctionName;
        /// <summary>
        /// 功能码序号
        /// </summary>
        public string FunctionCode;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 功能函数
        /// </summary>
        public string Function;
        /// <summary>
        /// 
        /// </summary>
        public string DIAG;
        /// <summary>
        /// 输入引脚的个数
        /// </summary>
        public string InputCount;
        /// <summary>
        /// 规格数个数
        /// </summary>
        public string SpecCount;
        /// <summary>
        /// 输出管脚个数
        /// </summary>
        public string OutputCount;
        /// <summary>
        /// 内部变量个数
        /// </summary>
        public string InternalCount;
        /// <summary>
        /// FC的空间长度
        /// </summary>
        public string FCLength;
        /// <summary>
        /// Function Code/IO Connector/Constant Block/Cross Reference
        /// </summary>
        public string Type;

        /// <summary>
        /// PinName->Meta_Module_Pin对象
        /// </summary>
        public Hashtable Input_Pin;
        /// <summary>
        /// PinName->Meta_Module_Pin对象
        /// </summary>
        public Hashtable Out_Pin;
        public Hashtable Constant_Pin;///PinName->Meta_Module_Pin对象
        public Hashtable Internal_Pin;///PinName->Meta_Module_Pin对象
        public Hashtable Pins;///PinName->Meta_Module_Pin对象
        public Hashtable PinType;//PinName->PinType 1：Input，2：Constant，3：Output 4:Internal

        /// <summary>
        /// 构造函数,
        /// </summary>
        public Meta_Module()
        {
            Input_Pin = new Hashtable();
            Out_Pin = new Hashtable();
            Constant_Pin = new Hashtable();
            Internal_Pin = new Hashtable();
            Pins = new Hashtable();
            PinType = new Hashtable();
        }

        /// <summary>
        /// 根据Pin的Index获得对应的PinName
        /// </summary>
        /// <param name="index">Pin Index</param>
        /// <returns>PinName</returns>
        public string PinName_By_Index(int index)
        {
            foreach (Meta_Module_Pin meta_module_pin in Pins.Values)
            {
                if (Int32.Parse(meta_module_pin.PinIndex) == index)
                {
                    return meta_module_pin.PinName;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据PinName 返回meta_module_pin对象
        /// </summary>
        /// <param name="pinname"></param>
        /// <returns></returns>
        public Meta_Module_Pin Pin_Obj_By_Pin_Name(string pinname)
        {
            Meta_Module_Pin result = (Meta_Module_Pin)Pins[pinname];
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }

    }//end of class Meta_Module



    /// <summary>
    /// Meta_Module的集合
    /// </summary>
    public class Meta_ModuleS
    {
        /// <summary>
        /// FuncName->Meta_Module
        /// </summary>
        public Hashtable meta_modules;

        /// <summary>
        /// 从数据库中读取所有的MetaModule
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public Meta_ModuleS(OleDbConnection conn)
        {
            //测试连接状态，并进行相应的处理
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            meta_modules = new Hashtable();//FuncName->Meta_Module


            string sql_string = "SELECT * FROM Meta_FCMaster";
            OleDbDataAdapter Meta_FCMaster_Adapter = new OleDbDataAdapter(sql_string, conn);
            DataSet myDataSet = new DataSet();
            Meta_FCMaster_Adapter.Fill(myDataSet, "Meta_FCMaster");

            for (int i = 0; i < myDataSet.Tables["Meta_FCMaster"].Rows.Count; i++)
            {//对每一个Meta FCMaster 
                #region Meta Module部分
                Meta_Module meta_module_temp = new Meta_Module();
                meta_module_temp.FunctionName = myDataSet.Tables["Meta_FCMaster"].Rows[i]["FunctionName"].ToString();
                meta_module_temp.FunctionCode = myDataSet.Tables["Meta_FCMaster"].Rows[i]["FunctionCode"].ToString();
                meta_module_temp.Description = myDataSet.Tables["Meta_FCMaster"].Rows[i]["Description"].ToString();
                meta_module_temp.Function = myDataSet.Tables["Meta_FCMaster"].Rows[i]["Function"].ToString();
                meta_module_temp.DIAG = myDataSet.Tables["Meta_FCMaster"].Rows[i]["DIAG"].ToString();
                meta_module_temp.InputCount = myDataSet.Tables["Meta_FCMaster"].Rows[i]["InputCount"].ToString();
                meta_module_temp.SpecCount = myDataSet.Tables["Meta_FCMaster"].Rows[i]["SpecCount"].ToString();
                meta_module_temp.OutputCount = myDataSet.Tables["Meta_FCMaster"].Rows[i]["OutPutCount"].ToString();
                meta_module_temp.InternalCount = myDataSet.Tables["Meta_FCMaster"].Rows[i]["InternalCount"].ToString();
                meta_module_temp.FCLength = myDataSet.Tables["Meta_FCMaster"].Rows[i]["FCLength"].ToString();
                meta_module_temp.Type = myDataSet.Tables["Meta_FCMaster"].Rows[i]["Type"].ToString();
                #endregion

                //对每一个Meta Module寻找对应的Meta Module Pin
                sql_string = "SELECT * FROM Meta_FCDetail WHERE FunctionName='" + meta_module_temp.FunctionName + "'";
                OleDbDataAdapter Meta_FCDetail_Adapter = new OleDbDataAdapter(sql_string, conn);
                Meta_FCDetail_Adapter.Fill(myDataSet, "Meta_FCDetail");
                for (int m = 0; m < myDataSet.Tables["Meta_FCDetail"].Rows.Count; m++)
                {
                    Meta_Module_Pin meta_module_pin_temp = new Meta_Module_Pin();
                    meta_module_pin_temp.FunctionName = myDataSet.Tables["Meta_FCDetail"].Rows[m]["FunctionName"].ToString();
                    meta_module_pin_temp.PinName = myDataSet.Tables["Meta_FCDetail"].Rows[m]["PinName"].ToString();
                    meta_module_pin_temp.PinIndex = myDataSet.Tables["Meta_FCDetail"].Rows[m]["PinIndex"].ToString();
                    meta_module_pin_temp.DataType = myDataSet.Tables["Meta_FCDetail"].Rows[m]["DataType"].ToString();
                    meta_module_pin_temp.Tune = (bool)myDataSet.Tables["Meta_FCDetail"].Rows[m]["Tune"];
                    meta_module_pin_temp.PinType = myDataSet.Tables["Meta_FCDetail"].Rows[m]["PinType"].ToString();
                    meta_module_pin_temp.MaxValue = myDataSet.Tables["Meta_FCDetail"].Rows[m]["MaxValue"].ToString();
                    meta_module_pin_temp.MinValue = myDataSet.Tables["Meta_FCDetail"].Rows[m]["MinValue"].ToString();
                    meta_module_pin_temp.ValidValue = myDataSet.Tables["Meta_FCDetail"].Rows[m]["ValidValue"].ToString();
                    meta_module_pin_temp.DefaultValue = myDataSet.Tables["Meta_FCDetail"].Rows[m]["DefaultValue"].ToString();
                    meta_module_pin_temp.Required = (bool)myDataSet.Tables["Meta_FCDetail"].Rows[m]["Required"];
                    meta_module_pin_temp.Description = myDataSet.Tables["Meta_FCDetail"].Rows[m]["Description"].ToString();
                    meta_module_pin_temp.Fixed = (bool)myDataSet.Tables["Meta_FCDetail"].Rows[m]["Fixed"];
                    meta_module_pin_temp.PinSignalType = myDataSet.Tables["Meta_FCDetail"].Rows[m]["PinSignalType"].ToString();
                    if (meta_module_pin_temp.PinType.Equals("Input"))
                    {
                        meta_module_temp.Input_Pin[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.Pins[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.PinType[meta_module_pin_temp.PinName] = "Input";
                    }
                    else if (meta_module_pin_temp.PinType.Equals("Output"))
                    {
                        meta_module_temp.Out_Pin[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.Pins[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.PinType[meta_module_pin_temp.PinName] = "Output";
                    }
                    else if (meta_module_pin_temp.PinType.Equals("Constant"))
                    {
                        meta_module_temp.Constant_Pin[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.Pins[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.PinType[meta_module_pin_temp.PinName] = "Constant";
                    }
                    else if (meta_module_pin_temp.PinType.Equals("Internal"))
                    {
                        meta_module_temp.Internal_Pin[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.Pins[meta_module_pin_temp.PinName] = meta_module_pin_temp;
                        meta_module_temp.PinType[meta_module_pin_temp.PinName] = "Internal";
                    }
                    else
                    {
                        throw (new Exception("No such Pin type"));
                    }
                }
                myDataSet.Tables["Meta_FCDetail"].Clear();//清空已备下次使用
                meta_modules[meta_module_temp.FunctionName] = meta_module_temp;
            }
        }


        /// <summary>
        /// 根据FuncName和Pin Index获得对应的Pin Name
        /// </summary>
        /// <param name="FuncName">FuncName</param>
        /// <param name="index">Pin Index</param>
        /// <returns>Pin Name</returns>
        public string PinName_By_FuncName_And_Index(string FuncName, int index)
        {
            Meta_Module meta_module = (Meta_Module)meta_modules[FuncName];
            if (meta_module == null)
            {
                throw new Exception("In Table Meta_FCMaster,No " + FuncName + "Module");
            }
            else
            {
                return meta_module.PinName_By_Index(index);
            }
            return null;
        }


        /// <summary>
        /// 获得给定FuncName 中PinName的Type
        /// </summary>
        /// <param name="FuncName">FuncName的Meta Module</param>
        /// <param name="PinName">pin的名字</param>
        /// <returns></returns>
        public string Pin_Type(string FuncName, string PinName)
        {
            Meta_Module meta_module = (Meta_Module)meta_modules[FuncName];

            if (meta_module == null)
            {
                return "";
            }
            else
            {
                if (((string)meta_module.PinType[PinName]) == null)
                {
                    return "";
                }
                else
                {
                    return (string)meta_module.PinType[PinName];
                }
            }
        }
    }//end of Meta_ModuleS



    /// <summary>
    /// 对Meta_SymbolMaster描述的类
    /// </summary>
    public class Meta_SymbolMaster
    {
        /// <summary>
        /// SysmbolID
        /// </summary>
        public string SymbolID;
        /// <summary>
        /// SymbolName
        /// </summary>
        public string SymbolName;
        /// <summary>
        /// Symbol Type:FunctionCode,DocumentShape,
        /// </summary>
        public string SymbolType;
        /// <summary>
        /// OringinPoint
        /// </summary>
        public string OringinPoint;
        /// <summary>
        /// ReadOnly
        /// </summary>
        public bool ReadOnly;
        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName;
        /// <summary>
        /// code
        /// </summary>
        public int Code;
        /// <summary>
        /// Meta_Symbol包含的具体内容
        /// </summary>
        public List<Meta_SymbolDetail> Details;
        /// <summary>
        /// 从Pin Index 到Point的哈希表，描述Pin相对于symbol的相对位置
        /// </summary>
        public Hashtable Pin_Relative_Pos;
        /// <summary>
        /// 模块的高度
        /// </summary>
        public double Height;
        /// <summary>
        /// 模块的宽度
        /// </summary>
        public double Width;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Meta_SymbolMaster()
        {
            Details = new List<Meta_SymbolDetail>();
            Pin_Relative_Pos = new Hashtable();
        }
    }


    /// <summary>
    /// 对Meta_SymbolDetail描述的类
    /// </summary>
    public class Meta_SymbolDetail
    {
        /// <summary>
        /// Symbol ID
        /// </summary>
        public string SymbolID;
        /// <summary>
        /// Element type:DocShape,Pin,Line,Text
        /// </summary>
        public string ElementType;
        /// <summary>
        /// element data
        /// </summary>
        public string ElementData;
        /// <summary>
        /// Dynamic type
        /// </summary>
        public int DynamicType;
    }


    /// <summary>
    /// Meta_Symbol的集合
    /// </summary>
    public class Meta_SymbolS
    {
        /// <summary>
        /// Meta_Symbol的哈希集合，从SymbolID到Meta_SymbolMaster对象的哈希
        /// </summary>
        public Hashtable meta_symbols;


        /// <summary>
        /// 构造函数,只进行基本的初始化工作
        /// </summary>
        public Meta_SymbolS()
        {
            //数据成员的初始化
            meta_symbols = new Hashtable();
        }


        /// <summary>
        /// 构造函数，根据给定的Meta_ModuleS，构造Meta_symbols
        /// </summary>
        /// <param name="meta_modules">给定的Meta_Modules</param>
        public Meta_SymbolS(Meta_ModuleS meta_modules)
        {
            // 数据成员的初始化
            meta_symbols = new Hashtable();

            //对每一个Meta_Module构造相应的Meta_Symbol
            foreach (Meta_Module meta_module in meta_modules.meta_modules.Values)
            {
                #region 特殊模块图标的构造
                if (meta_module.FunctionName.Equals("XPgDO"))
                {
                    List<Meta_SymbolMaster> temp = Create_XPgDO(17.5, "XPgDO", "XPgDO", "60", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XPgAO"))
                {
                    List<Meta_SymbolMaster> temp = Create_XPgAO("XPgAO", 15, "te", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XPgDI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XPgDI("XPgDI", 17.5, "w", "w", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XPgAI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XPgAI("XPgAI", 15, "w", "w", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XPI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XPI("XPI", 75, 15, "null", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XDO"))
                {
                    List<Meta_SymbolMaster> temp = Create_XDO("XDO", 75, 15, "null", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XAI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XAI("XAI", 75, 15, "null", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XAO"))
                {
                    List<Meta_SymbolMaster> temp = Create_XAO("XAO", 75, 15, "null", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XDI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XDI("XDI", 75, 15, "null", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XAI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XAI("XAI", 75, 15, "null", meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XNetDO"))
                {
                    List<Meta_SymbolMaster> temp = Create_XNetDOI("XNetDO", 75, 15, "null", true, meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XNetDI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XNetDOI("XNetDI", 75, 15, "null", false, meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XNetAO"))
                {
                    List<Meta_SymbolMaster> temp = Create_XNetAOI("XNetAO", 75, 15, "null", true, meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                else if (meta_module.FunctionName.Equals("XNetAI"))
                {
                    List<Meta_SymbolMaster> temp = Create_XNetAOI("XNetAI", 75, 15, "null", false, meta_module);
                    foreach (Meta_SymbolMaster meta_sym_master in temp)
                    {
                        meta_symbols[meta_sym_master.SymbolID] = meta_sym_master;
                    }
                    continue;
                }
                #endregion

                //一般模块图标的构造
                //构造Master的整体
                Meta_SymbolMaster meta_symbolmaster = new Meta_SymbolMaster();
                meta_symbolmaster.SymbolID = meta_module.FunctionName;
                string symbolid = meta_module.FunctionName;
                meta_symbolmaster.SymbolName = meta_module.FunctionName;
                meta_symbolmaster.SymbolType = "FunctionCode";
                meta_symbolmaster.OringinPoint = "0.00_0.00";
                meta_symbolmaster.ReadOnly = false;
                meta_symbolmaster.FunctionName = meta_module.FunctionName;
                string functionname = meta_symbolmaster.FunctionName;
                meta_symbolmaster.Code = 0;

                List<int> width_height = GetWidthHeight(meta_module);
                //模块中输入引脚的个数
                int module_input_count = Int32.Parse(meta_module.InputCount);
                //模块中输出引脚的个数
                int module_output_count = Int32.Parse(meta_module.OutputCount);

                //记录模块图标的高度和宽度，以方便后边的使用
                meta_symbolmaster.Height = width_height[1];
                meta_symbolmaster.Width = width_height[0];
                meta_symbols[meta_symbolmaster.SymbolID] = meta_symbolmaster;


                //构造Details部分

                //对Input_Pin按Index的大小进行排序，其实可以采用实现比较接口的办法来实现
                List<Meta_Module_Pin> in_mmp_temp = new List<Meta_Module_Pin>();
                Hashtable index_2_Pin = new Hashtable();
                foreach (Meta_Module_Pin mmp in meta_module.Input_Pin.Values)
                {
                    index_2_Pin[Int32.Parse(mmp.PinIndex)] = mmp;
                }
                List<int> pin_index_list = new List<int>();
                foreach (int pinindex in index_2_Pin.Keys)
                {
                    pin_index_list.Add(pinindex);
                }
                pin_index_list.Sort();
                foreach (int i in pin_index_list)
                {
                    in_mmp_temp.Add((Meta_Module_Pin)index_2_Pin[i]);
                }
                //排序结束

                //对Out_Pin按Index的大小进行排序，其实可以采用实现比较接口的办法来实现
                List<Meta_Module_Pin> out_mmp_temp = new List<Meta_Module_Pin>();
                index_2_Pin = new Hashtable();
                foreach (Meta_Module_Pin mmp in meta_module.Out_Pin.Values)
                {
                    index_2_Pin[Int32.Parse(mmp.PinIndex)] = mmp;
                }
                pin_index_list = new List<int>();
                foreach (int pinindex in index_2_Pin.Keys)
                {
                    pin_index_list.Add(pinindex);
                }
                pin_index_list.Sort();
                foreach (int i in pin_index_list)
                {
                    out_mmp_temp.Add((Meta_Module_Pin)index_2_Pin[i]);
                }
                //排序结束

                #region 输入引脚的插入
                List<double> Pin_y_pos = Get_Pin_Y_Pos(width_height[1], module_input_count);
                int index = 1;
                foreach (Meta_Module_Pin inputpin in in_mmp_temp)
                {
                    //输入引脚
                    Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
                    Point point_temp = new Point();//存储Pin的相对位置
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Pin";
                    meta_symboldetail.ElementData = Get_Pin_Data(width_height[0], 5, true, index, ref point_temp, inputpin.PinName, Pin_y_pos);
                    meta_symboldetail.DynamicType = 1;
                    meta_symbolmaster.Details.Add(meta_symboldetail);
                    index++;

                    //增加Pin的相对位置
                    meta_symbolmaster.Pin_Relative_Pos[inputpin] = point_temp;

                    //增加引脚数据值的显示
                    meta_symboldetail = new Meta_SymbolDetail();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Text";
                    string PinName = "";
                    string text_pos = "";
                    PinName = inputpin.PinName;
                    text_pos = (point_temp.x + 16) + "_" + (point_temp.y - 2);
                    meta_symboldetail.ElementData = "TextDispFun(AbbPinValueDisplayRule," + PinName + ".Value$$w||右下||" + text_pos +
                        "||0||\"Courier New\"||8||255_255_255";
                    meta_symboldetail.DynamicType = 1;
                    meta_symbolmaster.Details.Add(meta_symboldetail);


                }
                #endregion
                #region 输出引脚的插入
                Pin_y_pos = Get_Pin_Y_Pos(width_height[1], module_output_count);
                index = 1;
                foreach (Meta_Module_Pin outputpin in out_mmp_temp)
                {
                    //输出引脚
                    Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
                    Point point_temp = new Point();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Pin";
                    meta_symboldetail.ElementData = Get_Pin_Data(width_height[0], 5, false, index, ref point_temp, outputpin.PinName, Pin_y_pos);
                    meta_symboldetail.DynamicType = 1;
                    meta_symbolmaster.Details.Add(meta_symboldetail);
                    index++;

                    //增加Pin的相对位置
                    meta_symbolmaster.Pin_Relative_Pos[outputpin] = point_temp;

                    //增加引脚数据值的显示
                    meta_symboldetail = new Meta_SymbolDetail();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Text";
                    string PinName = "";
                    string text_pos = "";
                    PinName = outputpin.PinName;
                    text_pos = (point_temp.x - 13) + "_" + (point_temp.y - 2);
                    meta_symboldetail.ElementData = "TextDispFun(AbbPinValueDisplayRule," + PinName + ".Value$$w||左下||" + text_pos +
                        "||0||\"\"Courier New\"\"||8||255_255_255";
                    meta_symboldetail.DynamicType = 1;
                    meta_symbolmaster.Details.Add(meta_symboldetail);
                }
                #endregion
                {//DocShape
                    Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "DocShape";
                    meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
                    meta_symboldetail.DynamicType = 0;
                    meta_symbolmaster.Details.Add(meta_symboldetail);
                }
                {//动态文字的添加
                    //功能名字
                    Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Text";//TextDispFun(OwtBlankRule,BlockMe.FUNC_NAME)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
                    //meta_symboldetail.ElementData = Get_Text_Data(meta_symbolmaster.FunctionName, width_height[0], width_height[1]);
                    meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,BlockMe.FUNC_NAME)$$  ||正中||" +
                        (0.5 * meta_symbolmaster.Width) + "_" + (0.2 * meta_symbolmaster.Height) + "||0||\"Courier New\"||8||255_255_255";
                    meta_symboldetail.DynamicType = 1;
                    meta_symbolmaster.Details.Add(meta_symboldetail);

                    //alg_name
                    meta_symboldetail = new Meta_SymbolDetail();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Text";//TextDispFun(OwtBlankRule,BlockMe.ALG_NAME)$$  ||左下||62.37_188.61||0||"Courier New"||12||255_255_255
                    meta_symboldetail.ElementData = "TextDispFun(XinhuaBlockNameAndOrderRule,BlockMe.ALG_NAME,BlockMe.Order)$$  ||正中||" +
                        (0.5 * meta_symbolmaster.Width) + "_" + (0.8 * meta_symbolmaster.Height) + "||0||\"Courier New\"||7||255_255_255";
                    meta_symboldetail.DynamicType = 2;
                    meta_symbolmaster.Details.Add(meta_symboldetail);
                }

                Meta_SymbolMaster master_symbol = meta_symbolmaster;

                //body部分（里边的内容都为静态）
                {
                    #region 构造Body部分
                    meta_symbolmaster = new Meta_SymbolMaster();
                    meta_symbolmaster.SymbolID = "BODY_" + symbolid;
                    meta_symbolmaster.SymbolName = "BODY_" + symbolid;
                    meta_symbolmaster.SymbolType = "DocumentShape";
                    meta_symbolmaster.OringinPoint = "0.00_0.00";
                    meta_symbolmaster.ReadOnly = false;
                    meta_symbolmaster.FunctionName = functionname;
                    meta_symbolmaster.Code = 0;
                    meta_symbolmaster.Height = width_height[1];//模块的高度
                    meta_symbolmaster.Width = width_height[0];//模块的宽度
                    meta_symbols["BODY" + symbolid] = meta_symbolmaster;

                    //构造Body的Detail部分
                    {
                        Meta_SymbolDetail meta_symboldetail;
                        //输入Pin Name的显示
                        foreach (Meta_Module_Pin inpin in in_mmp_temp)
                        {
                            meta_symboldetail = new Meta_SymbolDetail();
                            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                            meta_symboldetail.ElementType = "Text";
                            Point body_pin = (Point)master_symbol.Pin_Relative_Pos[inpin];
                            meta_symboldetail.ElementData = inpin.PinName + "||左下||" + 0 + "_" + (body_pin.y + 4) +
                                "||0||MONOTEXT||6||255_255_255";
                            meta_symboldetail.DynamicType = 0;
                            meta_symbolmaster.Details.Add(meta_symboldetail);
                        }
                        //输出Pin Name的显示
                        foreach (Meta_Module_Pin outpin in out_mmp_temp)
                        {
                            meta_symboldetail = new Meta_SymbolDetail();
                            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                            meta_symboldetail.ElementType = "Text";
                            Point body_pin = (Point)master_symbol.Pin_Relative_Pos[outpin];
                            meta_symboldetail.ElementData = outpin.PinName + "||右下||" + (master_symbol.Width) + "_" + (body_pin.y + 4) +
                                "||0||MONOTEXT||6||255_255_255";
                            meta_symboldetail.DynamicType = 0;
                            meta_symbolmaster.Details.Add(meta_symboldetail);
                        }
                        //Line
                        for (int i = 0; i < 4; i++)
                        { //four lines
                            meta_symboldetail = new Meta_SymbolDetail();
                            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                            meta_symboldetail.ElementType = "Line";
                            meta_symboldetail.ElementData = Get_Line_Data(width_height[0], width_height[1], i);
                            meta_symboldetail.DynamicType = 0;
                            meta_symbolmaster.Details.Add(meta_symboldetail);
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 将Meta_FCMaster和Meta_FCDetail表中的内容转换到Meta_SymbolMaster和Meta_SymbolDetail表中
        /// </summary>
        /// <param name="conn">数据库连接:OleDbConnection</param>
        static public void Meta_Module_2_Meta_Symbol(OleDbConnection conn)
        {
            Meta_ModuleS meta_modules = new Meta_ModuleS(conn);//读取Meta_Module信息
            Meta_SymbolS meta_symbols = new Meta_SymbolS(meta_modules);//转换为描述Meta_Symbol信息
            meta_symbols.InsertIntoTable(conn);//插入到Meta_SymbolMaster和meta_symboldetail中
        }

        /// <summary>
        /// 生成XNetAO和XNetAI的图标信息
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="width">图标的宽度</param>
        /// <param name="height">图标的高度</param>
        /// <param name="text">图标显示的文本</param>
        /// <param name="isAO">是：XNetAO，否：XNetAI</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XNetAOI(string id, double width, double height, string text, bool isAO, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            //BODY
            Meta_SymbolMaster meta_symbolmaster;
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XNetAO";
            meta_symbolmaster.Code = 0;

            //body detail
            Meta_SymbolDetail meta_symboldetail;
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Arc";
            meta_symboldetail.ElementData = "255_255_255||" + (height / 2) + "_" + (height / 2) + "||" + (height / 2) + "_" + (height) +
                "||" + (height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Arc";
            meta_symboldetail.ElementData = "255_255_255||" + (width - height / 2) + "_" + (height / 2) + "||" + (width - height / 2) + "_" + (0) +
                "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);


            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = id;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            if (isAO)
            {
                meta_symbolmaster.FunctionName = "XNetAO";
            }
            else
            {
                meta_symbolmaster.FunctionName = "XNetAI";
            }
            meta_symbolmaster.Code = 0;

            //主要部分的Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态Text
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,GID.Name)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,GID.Address)$$  ||左下||" +
                    (0.4 * width) + "_" + (0.8 * height) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            if (isAO)
            {
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Pin";
                meta_symboldetail.ElementData = (-12) + "_" + (height / 2) + "," + (0) + "_" + (height / 2) + "||X||Input";
                meta_symboldetail.DynamicType = 1;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["X"];


                Point point_temp = new Point();
                point_temp.x = -12;
                point_temp.y = height / 2;
                meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

                ////增加引脚数据值的显示
                meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "X");

            }
            else
            {
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Pin";
                meta_symboldetail.ElementData = (width + 12) + "_" + (height / 2) + "," + (width) + "_" + (height / 2) + "||Y||Output";

                Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["Y"];

                Point point_temp = new Point();
                point_temp.x = width + 12;
                point_temp.y = height / 2;
                point_temp.is_valid = true;
                meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

                //增加引脚数据值的显示
                meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Y");
            }
            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XNetDO和XNetDI的图标信息
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="width">图标的宽度</param>
        /// <param name="height">图标的高度</param>
        /// <param name="text">图标显示的文本</param>
        /// <param name="isDO">是：XNetDO，否：XNetDI</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XNetDOI(string id, double width, double height, string text, bool isDO, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            //BODY
            Meta_SymbolMaster meta_symbolmaster;
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XNetDO";
            meta_symbolmaster.Code = 0;

            //body detail
            Meta_SymbolDetail meta_symboldetail;
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (0);//0||255_255_255||0_0||60_0
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (0) + "||" + (width) + "_" + (height / 2);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width) + "_" + (height / 2) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (height / 2) + "_" + (height) + "||" + (0) + "_" + (height / 2);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (height / 2) + "||" + (height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            if (isDO)
            {
                meta_symbolmaster.FunctionName = "XNetDO";
            }
            else
            {
                meta_symbolmaster.FunctionName = "XNetDI";
            }
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态Text
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,GID.Name)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,GID.Address)$$  ||左下||" +
                    (0.4 * width) + "_" + (0.8 * height) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            if (isDO)
            {
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Pin";
                meta_symboldetail.ElementData = (-12) + "_" + (height / 2) + "," + (0) + "_" + (height / 2) + "||Z||Input";
                meta_symboldetail.DynamicType = 1;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["Z"];

                Point point_temp = new Point();
                point_temp.x = -12;
                point_temp.y = height / 2;
                point_temp.is_valid = true;
                meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

                ////增加引脚数据值的显示
                meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Z");
            }
            else
            {
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Pin";
                meta_symboldetail.ElementData = (width + 12) + "_" + (height / 2) + "," + (width) + "_" + (height / 2) + "||D||Output";
                meta_symboldetail.DynamicType = 1;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["D"];

                Point point_temp = new Point();
                point_temp.x = width + 12;
                point_temp.y = height / 2;
                point_temp.is_valid = true;
                meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

                ////增加引脚数据值的显示
                meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "D");
            }

            result.Add(meta_symbolmaster);


            return result;
        }


        private static Meta_SymbolDetail Inpin_Add_Dynamic_Text(Meta_SymbolMaster meta_symbolmaster, Meta_SymbolDetail meta_symboldetail,
            Point point_temp, string pinname)
        {
            //增加引脚数据值的显示
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Text";
            string PinName = "";
            string text_pos = "";
            PinName = pinname;
            text_pos = (point_temp.x + 16) + "_" + (point_temp.y - 2);
            meta_symboldetail.ElementData = "TextDispFun(AbbPinValueDisplayRule," + PinName + ".Value$$w||右下||" + text_pos +
                "||0||\"Courier New\"||8||255_255_255";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);
            return meta_symboldetail;
        }

        private static Meta_SymbolDetail Outpin_Add_Dynamic_Text(Meta_SymbolMaster meta_symbolmaster, Meta_SymbolDetail meta_symboldetail,
            Point point_temp, string pinname)
        {
            //增加引脚数据值的显示
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Text";
            string PinName = "";
            string text_pos = "";
            PinName = pinname;
            text_pos = (point_temp.x - 13) + "_" + (point_temp.y - 2);
            meta_symboldetail.ElementData = "TextDispFun(AbbPinValueDisplayRule," + PinName + ".Value$$w||左下||" + text_pos +
                "||0||\"\"Courier New\"\"||8||255_255_255";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);
            return meta_symboldetail;
        }


        /// <summary>
        /// 生成XAI图标
        /// </summary>
        /// <param name="id">图标ID</param>
        /// <param name="width">图标宽度</param>
        /// <param name="height">图标高度</param>
        /// <param name="text">图标显示的文本</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XAI(string id, double width, double height, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            Meta_SymbolMaster meta_symbolmaster;

            //BODY
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail of Body
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||0_0||" + (width - height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (0) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (height) + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Arc";
            meta_symboldetail.ElementData = "255_255_255||" + (width - height / 2) + "_" + (height / 2) + "||" +
                (width - height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态Text
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,GID.Name)$$  ||左下||62.37_188.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,GID.Address)$$  ||左下||" +
                    (0.2 * width) + "_" + (height) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                //address
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,Addr.Name)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaIOAddrRule,Addr.Address)$$  ||左下||" +
                    (0.2 * width) + "_" + (height * 2.2) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = "-12_" + (height / 2) + "," + (0) + "_" + (height / 2) + "||X||Input";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["X"];

            Point point_temp = new Point();
            point_temp.x = -12;
            point_temp.y = height / 2;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "X");

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (width + 12) + "_" + (height / 2) + "," + (width) + "_" + (height / 2) + "||Y||Output";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["Y"];

            point_temp = new Point();
            point_temp.x = width + 12;
            point_temp.y = height / 2;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Y");


            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XDI图标
        /// </summary>
        /// <param name="id">图标id</param>
        /// <param name="width">图标宽度</param>
        /// <param name="height">图标高度</param>
        /// <param name="text">图标显示的文字</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XDI(string id, double width, double height, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            Meta_SymbolMaster meta_symbolmaster;

            //BODY
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail of Body
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||0_0||" + (width - height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (0) + "||" + (width) + "_" + (height / 2);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width) + "_" + (height / 2) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (0) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (height) + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_00||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            //动态Text
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,GID.Name)$$  ||左下||62.37_188.61||0||"Courier New"||12||255_255_255
            meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,GID.Address)$$  ||左下||" +
                (0.2 * width) + "_" + (height) + "||0||\"Courier New\"||12||255_255_255";
            meta_symboldetail.DynamicType = 2;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            //address
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,Addr.Name)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
            meta_symboldetail.ElementData = "TextDispFun(XinhuaIOAddrRule,Addr.Address)$$  ||左下||" +
                (0.2 * width) + "_" + (height * 2.2) + "||0||\"Courier New\"||12||255_255_255";
            meta_symboldetail.DynamicType = 2;
            meta_symbolmaster.Details.Add(meta_symboldetail);


            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (width + 12) + "_" + (height / 2) + "," + (width) + "_" + (height / 2) + "||D||Output";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["D"];

            Point point_temp = new Point();
            point_temp.x = width + 12;
            point_temp.y = height / 2;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "D");


            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XDO图标
        /// </summary>
        /// <param name="id">图标ID</param>
        /// <param name="width">图标宽度</param>
        /// <param name="height">图标高度</param>
        /// <param name="text">图标显示的文本</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XDO(string id, double width, double height, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            Meta_SymbolMaster meta_symbolmaster;

            //BODY
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail of Body
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||0_0||" + (width - height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (0) + "||" + (width) + "_" + (height / 2);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width) + "_" + (height / 2) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (0) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (height) + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_00||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态Text
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,GID.Name)$$  ||左下||62.37_188.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,GID.Address)$$  ||左下||" +
                    (0.2 * width) + "_" + (height) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                //address
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,Addr.Name)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaIOAddrRule,Addr.Address)$$  ||左下||" +
                    (0.2 * width) + "_" + (height * 2.2) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (-12) + "_" + (height / 2) + "," + (0) + "_" + (height / 2) + "||Z||Input";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["Z"];

            Point point_temp = new Point();
            point_temp.x = -12;
            point_temp.y = height / 2;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Z");

            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XAO图标
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="width">图标的宽度</param>
        /// <param name="height">图标的高度</param>
        /// <param name="text">图标显示的文本</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XAO(string id, double width, double height, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            Meta_SymbolMaster meta_symbolmaster;

            //BODY
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail of Body
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||0_0||" + (width - height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (0) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (height) + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Arc";
            meta_symboldetail.ElementData = "255_255_255||" + (width - height / 2) + "_" + (height / 2) + "||" +
                (width - height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XAO";
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态Text
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,GID.Name)$$  ||左下||62.37_188.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,GID.Address)$$  ||左下||" +
                    (0.2 * width) + "_" + (height) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                //address
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";//TextDispFun(AbbBlankRule,Addr.Name)$$  ||左下||62.37_203.61||0||"Courier New"||12||255_255_255
                meta_symboldetail.ElementData = "TextDispFun(XinhuaIOAddrRule,Addr.Address)$$  ||左下||" +
                    (0.2 * width) + "_" + (height * 2.2) + "||0||\"Courier New\"||8||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (-12) + "_" + (height / 2) + "," + (0) + "_" + (height / 2) + "||X||Input";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["X"];
            if (meta_module_pin == null)
            {
                throw (new Exception(meta_module.FunctionName + ",No such Pin: X"));
            }

            Point point_temp = new Point();
            point_temp.x = -12;
            point_temp.y = height / 2;
            point_temp.is_valid = true; ; ; ;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "X");

            result.Add(meta_symbolmaster);

            return result;
        }


        /// <summary>
        /// 生成XPI图标
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="width">图标的宽度</param>
        /// <param name="height">图标的高度</param>
        /// <param name="text">图标显示的文本</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XPI(string id, double width, double height, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            Meta_SymbolMaster meta_symbolmaster;

            //BODY
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail of Body
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||0_0||" + (width - height / 2) + "_" + (0);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (width - height / 2) + "_" + (height) + "||" + (0) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (height) + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Arc";
            meta_symboldetail.ElementData = "255_255_255||" + (width - height / 2) + "_" + (height / 2) + "||" +
                (width - height / 2) + "_" + (0) + "||" + (width - height / 2) + "_" + (height);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Text";
            meta_symboldetail.ElementData = text + "||左下||" + (0.4 * width) + "_" + (height * 0.65) + "||0||MONOTEXT||8||255_255_255";
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPI";
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (width + 12) + "_" + (height / 2) + "," + (width) + "_" + (height / 2) + "||Y||Output";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["Y"];

            Point point_temp = new Point();
            point_temp.x = width + 12;
            point_temp.y = height / 2;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Y");

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (-12) + "_" + (height / 2) + "," + (0) + "_" + (height / 2) + "||Rst||Input";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["Rst"];

            point_temp = new Point();
            point_temp.x = -12;
            point_temp.y = height / 2;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Rst");


            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XPgDI图标，正六边形
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="edge_length">正六边形的边长</param>
        /// <param name="text1">显示在上方的文字</param>
        /// <param name="text2">显示在下方的文字</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XPgDI(string id, double edge_length, string text1, string text2, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            //body部分
            Meta_SymbolMaster meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = "BODY_" + id;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPgDI";
            meta_symbolmaster.Code = 0;

            //body的Detail部分
            Meta_SymbolDetail meta_symboldetail;
            for (int i = 0; i < 6; i++)
            {//六条边的添加
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Line";
                meta_symboldetail.ElementData = Get_Hexagon_Line_Data(edge_length, i);
                meta_symboldetail.DynamicType = 0;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (0) + "_" + (0.87 * edge_length) + "||" + (2 * edge_length) + "_" + (0.87 * edge_length);
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);



            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPgDI";
            meta_symbolmaster.Code = 0;

            //Detail部分
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态text(2 个)
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,Page.Value)$$  " + "||左下||" + (0.4 * edge_length) + "_" + (0.8 * edge_length) +
                    "||0||MONOTEXT||5||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,Block.Value)$$  " + "||左下||" + (0.4 * edge_length) + "_" + (1.5 * edge_length) +
                    "||0||MONOTEXT||5||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }


            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (12 + 2 * edge_length) + "_" + 0.87 * edge_length + "," + (2 * edge_length) + "_" + (0.87 * edge_length) +
                "||D||Output";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["D"];

            Point point_temp = new Point();
            point_temp.x = 12 + 2 * edge_length;
            point_temp.y = 0.87 * edge_length;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "D");


            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XPgAO图标，圆形
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="radius">圆的半径</param>
        /// <param name="text">图表显示的文字</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XPgAO(string id, double radius, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            //body部分
            Meta_SymbolMaster meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = "BODY_" + id;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPgAO";
            meta_symbolmaster.Code = 0;

            //body的Detail部分
            //circle
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Circle";
            meta_symboldetail.ElementData = "255_255_255||" + radius + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);
            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPgAO";
            meta_symbolmaster.Code = 0;

            //Detail部分
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态text
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,BlockMe.ALG_NAME)$$  " + "||左下||" + (-0.25 * radius) + "_" + (0) +
                    "||0||MONOTEXT||5||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (-12 - radius) + "_" + (0) + "," + (-radius) + "_" + (0) + "||X||Input";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["X"];

            Point point_temp = new Point();
            point_temp.x = -12 - radius;
            point_temp.y = -radius;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "X");

            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XPgAI，圆形
        /// </summary>
        /// <param name="id">图标的ID</param>
        /// <param name="radius">圆的半径</param>
        /// <param name="text1">显示在上方的文字</param>
        /// <param name="text2">显示在下方的文字</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XPgAI(string id, double radius, string text1, string text2, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            Meta_SymbolMaster meta_symbolmaster;
            //body部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPgAI";
            meta_symbolmaster.Code = 0;

            //Detail
            //circle
            Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Circle";
            meta_symboldetail.ElementData = "255_255_255||" + radius + "||0_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);
            //line
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Line";
            meta_symboldetail.ElementData = "0||255_255_255||" + (-radius) + "_0" + "||" + (radius) + "_0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            result.Add(meta_symbolmaster);

            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = meta_symbolmaster.SymbolID;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = "XPgAI";
            meta_symbolmaster.Code = 0;

            //Detail
            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "DocShape";
            meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
            meta_symboldetail.DynamicType = 0;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            {
                //动态text1
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,Page.Value)$$  " + "||左下||" + (-0.25 * radius) + "_" + (-0.25 * radius) + "||0||MONOTEXT||5||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
                //动态text2
                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Text";
                meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,Block.Value)$$  " + "||左下||" + (-0.25 * radius) + "_" + (0.5 * radius) + "||0||MONOTEXT||5||255_255_255";
                meta_symboldetail.DynamicType = 2;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            meta_symboldetail = new Meta_SymbolDetail();
            meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
            meta_symboldetail.ElementType = "Pin";
            meta_symboldetail.ElementData = (12 + radius) + "_" + (0) + "," + (radius) + "_" + (0) + "||Y||Output";
            meta_symboldetail.DynamicType = 1;
            meta_symbolmaster.Details.Add(meta_symboldetail);

            Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Out_Pin["Y"];

            Point point_temp = new Point();
            point_temp.x = 12 + radius;
            point_temp.y = 0;
            point_temp.is_valid = true;
            meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

            ////增加引脚数据值的显示
            meta_symboldetail = Outpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Y");


            result.Add(meta_symbolmaster);

            return result;
        }

        /// <summary>
        /// 生成XPgDO，正六边形
        /// </summary>
        /// <param name="edge_length">边长</param>
        /// <param name="id">图标ID</param>
        /// <param name="FunctionName">Function Name</param>
        /// <param name="text">图标显示的文字</param>
        /// <returns></returns>
        public List<Meta_SymbolMaster> Create_XPgDO(double edge_length, string id, string FunctionName, string text, Meta_Module meta_module)
        {
            List<Meta_SymbolMaster> result = new List<Meta_SymbolMaster>();

            //body部分
            Meta_SymbolMaster meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = "BODY_" + id;
            meta_symbolmaster.SymbolName = "BODY_" + id;
            meta_symbolmaster.SymbolType = "DocumentShape";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = FunctionName;
            meta_symbolmaster.Code = 0;

            //body的Detail
            for (int i = 0; i < 6; i++)
            {//六条边的添加
                Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Line";
                meta_symboldetail.ElementData = Get_Hexagon_Line_Data(edge_length, i);
                meta_symboldetail.DynamicType = 0;
                meta_symbolmaster.Details.Add(meta_symboldetail);
            }

            result.Add(meta_symbolmaster);


            //主要部分
            meta_symbolmaster = new Meta_SymbolMaster();
            meta_symbolmaster.SymbolID = id;
            meta_symbolmaster.SymbolName = id;
            meta_symbolmaster.SymbolType = "FunctionCode";
            meta_symbolmaster.OringinPoint = "0.00_0.00";
            meta_symbolmaster.ReadOnly = false;
            meta_symbolmaster.FunctionName = FunctionName;
            meta_symbolmaster.Code = 0;

            {//Detail部分
                Meta_SymbolDetail meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "DocShape";
                meta_symboldetail.ElementData = "BODY_" + meta_symbolmaster.SymbolID + "||0_0||0";
                meta_symboldetail.DynamicType = 0;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                {
                    //中心动态文字的添加 
                    meta_symboldetail = new Meta_SymbolDetail();
                    meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                    meta_symboldetail.ElementType = "Text";
                    meta_symboldetail.ElementData = "TextDispFun(XinhuaBlankRule,BlockMe.ALG_NAME)$$  " + "||" + "左下||" + (edge_length * 0.5) + "_" + (edge_length * 1.0) +
                        "||0||MONOTEXT||5||255_255_255";
                    meta_symboldetail.DynamicType = 2;
                    meta_symbolmaster.Details.Add(meta_symboldetail);

                }

                meta_symboldetail = new Meta_SymbolDetail();
                meta_symboldetail.SymbolID = meta_symbolmaster.SymbolID;
                meta_symboldetail.ElementType = "Pin";
                meta_symboldetail.ElementData = (-12) + "_" + 0.87 * edge_length + "," + (0) + "_" + (0.87 * edge_length) +
                    "||Z||Input";
                meta_symboldetail.DynamicType = 1;
                meta_symbolmaster.Details.Add(meta_symboldetail);

                Meta_Module_Pin meta_module_pin = (Meta_Module_Pin)meta_module.Input_Pin["Z"];

                Point point_temp = new Point();
                point_temp.x = -12;
                point_temp.y = 0.87 * edge_length;
                point_temp.is_valid = true;
                meta_symbolmaster.Pin_Relative_Pos[meta_module_pin] = point_temp;

                ////增加引脚数据值的显示
                meta_symboldetail = Inpin_Add_Dynamic_Text(meta_symbolmaster, meta_symboldetail, point_temp, "Z");
            }

            result.Add(meta_symbolmaster);

            return result;
        }




        /// <summary>
        /// 返回构造六边形所需的边的数据，index从最上边的边开始（0），顺时针依次增加
        /// </summary>
        /// <param name="edge_length">六边形的边长</param>
        /// <param name="index">边的编号</param>
        /// <returns>数据</returns>
        public string Get_Hexagon_Line_Data(double edge_length, int index)
        {
            string result = "";
            result = result + "0||255_255_255||";

            switch (index)
            {
                case 0:
                    result = result + (edge_length / 2) + "_" + (0) + "||";
                    result = result + (edge_length * 1.5) + "_" + (0);
                    break;
                case 1:
                    result = result + (edge_length * 1.5) + "_" + (0) + "||";
                    result = result + (edge_length * 2) + "_" + (0.87 * edge_length);
                    break;
                case 2:
                    result = result + (edge_length * 2) + "_" + (0.87 * edge_length) + "||";
                    result = result + (edge_length * 1.5) + "_" + (edge_length * 1.73);
                    break;
                case 3:
                    result = result + (edge_length * 1.5) + "_" + (edge_length * 1.73) + "||";
                    result = result + (edge_length / 2) + "_" + (edge_length * 1.73);
                    break;
                case 4:
                    result = result + (edge_length / 2) + "_" + (edge_length * 1.73) + "||";
                    result = result + (0) + "_" + (edge_length * 0.87);
                    break;
                case 5:
                    result = result + (0) + "_" + (edge_length * 0.87) + "||";
                    result = result + (edge_length / 2) + "_" + (0);
                    break;
                default:
                    break;
            }
            return result;
        }


        /// <summary>
        /// 给定模块的高度和Pin的数量，返回Pin的Y坐标
        /// </summary>
        /// <param name="Height">模块的高度</param>
        /// <param name="pinNum">Pin的数量</param>
        /// <returns>Pin的Y坐标</returns>
        private List<double> Get_Pin_Y_Pos(double Height, int pinNum)
        {
            List<double> result = new List<double>();
            double shangbianju = 22.5;
            double xiabianju = 22.5;
            //Height = Height - shangbianju - xiabianju;
            //double jiange=0;
            //if (pinNum == 1)
            //{
            //    jiange = 0;
            //}
            //else { 
            //    jiange = Height / (pinNum - 1);
            //}

            ////计算每个Pin的Y坐标
            //for (int i = 0; i < pinNum; i++) {
            //    result.Add(jiange * i + (shangbianju));
            //}

            double temp = (Height - (pinNum - 1) * 15) / 2;
            for (int i = 0; i < pinNum; i++)
            {
                result.Add(temp);
                temp = temp + 15;
            }
            return result;
        }




        /// <summary>
        /// 获得描述Line的数据
        /// </summary>
        /// <param name="width">模块的宽度</param>
        /// <param name="height">模块的高度</param>
        /// <param name="index">Line的索引，0：上；1：右；2：下；3：左</param>
        /// <returns>描述Line的数据</returns>
        private string Get_Line_Data(double width, double height, int index)
        {
            string result = "";
            if (index == 0)
            {//上
                result = result + "0||255_255_255||";//Line的颜色等
                result = result + (0) + "_" + (0) + "||";//上边的Line的开始点
                result = result + (width) + "_" + (0);//上边的Line的结束点
            }
            else if (index == 1)
            { //右
                result = result + "0||255_255_255||";
                result = result + (width) + "_" + (0) + "||";
                result = result + (width) + "_" + (height);
            }
            else if (index == 2)
            { //下
                result = result + "0||255_255_255||";
                result = result + (width) + "_" + (height) + "||";
                result = result + (0) + "_" + (height);
            }
            else if (index == 3)
            { //左
                result = result + "0||255_255_255||";
                result = result + (0) + "_" + (height) + "||";
                result = result + (0) + "_" + (0);
            }

            return result;
        }




        /// <summary>
        /// 获得描述Text的数据
        /// </summary>
        /// <param name="Text">文字</param>
        /// <param name="width">模块的宽度</param>
        /// <param name="height">模块的高度</param>
        /// <returns>描述文字的数据</returns>
        private string Get_Text_Data(string Text, double width, double height)
        {
            string result = "";

            result = result + Text + "||左下||";
            result = result + "" + (width * 0.2) + "_" + (0.4 * height) + "||";//文字的位置
            result = result + "0" + "||";
            result = result + "MONOTEXT||";
            result = result + "8||";//文字的大小
            result = result + "255_255_255";//文字的颜色

            return result;
        }




        /// <summary>
        /// 获得描述Pin的数据
        /// </summary>
        /// <param name="Width">模块宽度</param>
        /// <param name="PinLength">引脚延伸的长度</param>
        /// <param name="is_input">是否时输入引脚</param>
        /// <param name="index">引脚的index 从1 开始</param>
        /// <param name="pos">引脚接入点的相对位置</param>
        /// <param name="pinname">Pin Name</param>
        /// <param name="Pin_Y_Pos">Pin Y position list</param>
        /// <returns>描述Pin的数据</returns>
        private string Get_Pin_Data(double Width, double PinLength, bool is_input, int index, ref Point pos,
            string pinname, List<double> Pin_Y_Pos)
        {
            string result = "";
            //List<double> Pin_Y_Pos = Get_Pin_Y_Pos(Height, PinNum);

            if (is_input)
            {//输入引脚
                //start end
                result = result + "" + (0 - PinLength);//X pos
                result = result + "_";
                result = result + "" + (Pin_Y_Pos[index - 1]);//Y pos

                //填充Pin连接点的相对位置
                pos.x = (0 - PinLength);
                pos.y = Pin_Y_Pos[index - 1];
                pos.is_valid = true;

                result = result + ",";
                //ending end
                result = result + "" + (0);//X pos
                result = result + "_";
                result = result + "" + (Pin_Y_Pos[index - 1]);//Y pos

                result = result + "||";

                result = result + pinname + "||Input";
            }
            else
            { //输出引脚
                //start end
                result = result + "" + (Width + PinLength) + "_" + (Pin_Y_Pos[index - 1]);

                //填充Pin连接点的相对位置
                pos.x = Width + PinLength;
                pos.y = Pin_Y_Pos[index - 1];
                pos.is_valid = true;

                result = result + ",";
                result = result + "" + (Width) + "_" + (Pin_Y_Pos[index - 1]);

                result = result + "||";

                result = result + pinname + "||Output";
            }
            return result;
        }


        /// <summary>
        /// 将数据插入到Master和Details表中
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public void InsertIntoTable(OleDbConnection conn)
        {
            //建立数据库连接
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //清空表
            string sql_string = "SELECT * FROM Meta_SymbolMaster";
            OleDbCommand comm = new OleDbCommand(sql_string, conn);
            comm.CommandText = "DELETE FROM Meta_SymbolMaster";
            comm.ExecuteScalar();
            //清空表
            sql_string = "SELECT * FROM Meta_SymbolDetail";
            comm = new OleDbCommand(sql_string, conn);
            comm.CommandText = "DELETE FROM Meta_SymbolDetail";
            comm.ExecuteScalar();

            DataSet myDataSet = new DataSet();
            sql_string = "SELECT * FROM Meta_SymbolMaster";
            OleDbDataAdapter meta_symbolmaster_adapter = new OleDbDataAdapter(sql_string, conn);
            meta_symbolmaster_adapter.Fill(myDataSet, "Meta_SymbolMaster");
            sql_string = "SELECT * FROM Meta_SymbolDetail";
            OleDbDataAdapter meta_symboldetail_adapter = new OleDbDataAdapter(sql_string, conn);
            meta_symboldetail_adapter.Fill(myDataSet, "Meta_SymbolDetail");


            foreach (Meta_SymbolMaster meta_symbolmaster in meta_symbols.Values)
            {
                //向Master中添加数据
                DataRow datarow = myDataSet.Tables["Meta_SymbolMaster"].NewRow();
                datarow[0] = meta_symbolmaster.SymbolID;
                datarow[1] = meta_symbolmaster.SymbolName;
                datarow[2] = meta_symbolmaster.SymbolType;
                datarow[3] = meta_symbolmaster.OringinPoint;
                datarow[4] = meta_symbolmaster.ReadOnly;
                datarow[5] = meta_symbolmaster.FunctionName;
                datarow[6] = meta_symbolmaster.Code;
                myDataSet.Tables["Meta_SymbolMaster"].Rows.Add(datarow);

                //向Detail中添加数据
                foreach (Meta_SymbolDetail meta_symboldetail in meta_symbolmaster.Details)
                {
                    datarow = myDataSet.Tables["Meta_SymbolDetail"].NewRow();
                    datarow[0] = meta_symboldetail.SymbolID;
                    datarow[1] = meta_symboldetail.ElementType;
                    datarow[2] = meta_symboldetail.ElementData;
                    datarow[3] = meta_symboldetail.DynamicType;
                    myDataSet.Tables["Meta_SymbolDetail"].Rows.Add(datarow);
                }

            }

            //更新数据库
            new OleDbCommandBuilder(meta_symbolmaster_adapter);
            meta_symbolmaster_adapter.Update(myDataSet, "Meta_SymbolMaster");
            new OleDbCommandBuilder(meta_symboldetail_adapter);
            meta_symboldetail_adapter.Update(myDataSet, "Meta_SymbolDetail");
            myDataSet.AcceptChanges();
        }//end of method::InsertIntoTable


        /// <summary>
        /// 给定一个Meta_Module，返回其相应图标的宽度和高度的List，第一个为宽度，第二个为高度
        /// </summary>
        /// <param name="meta_module">Meta_Module</param>
        /// <returns>包含宽度和高度的List，第一个为宽度，第二个为高度</returns>
        public static List<int> GetWidthHeight(Meta_Module meta_module)
        {
            List<int> result = new List<int>();

            //设置模块的宽度,根据模块上显示的文字的大小以及模块的最小宽度
            //Font font = null;
            //string text = meta_module.FunctionName;//模块上显示的文字
            //int expected_width;
            //try
            //{
            //    //expected_width = (int)(text.Length * font.Size);
            //    expected_width = (int)(text.Length * 15);
            //}
            //catch (Exception) {
            //    expected_width = 0;
            //}
            //if (expected_width > 50){//最小宽度设置为50
            //    result.Add(expected_width);
            //}
            //else {
            //    result.Add(50);
            //}
            result.Add(60);//模块的宽度固定为60

            //设置模块的高度
            int minimumHeight = 45;//最小高度
            int pin_interval = 15;//Pin之间的间隔

            int module_input_count = Int32.Parse(meta_module.InputCount);//模块输入Pin的个数
            int module_output_count = Int32.Parse(meta_module.OutputCount);//模块输出Pin的个数

            int height = ((module_input_count > module_output_count) ? module_input_count : module_output_count) * pin_interval - pin_interval;
            height = height + 45;//加上 上下边距（分别22.5）
            if (height < minimumHeight)
            {
                height = minimumHeight;
            }
            result.Add(height);//模块的高度


            return result;
        }//end of method:: GetWidthHeight



    }//end of Class "Meta_SymbolS"

    /// <summary>
    /// CLd_Signal的描述类
    /// </summary>
    public class Cld_Signal
    {
        /// <summary>
        /// 唯一标志
        /// </summary>
        public string ObjectID;
        /// <summary>
        /// 控制器地址
        /// </summary>
        public string ControllerAddress;
        /// <summary>
        /// 组态文档名称
        /// </summary>
        public string DocumentName;
        /// <summary>
        /// 组态Sheet名称
        /// </summary>
        public string SheetName;
        /// <summary>
        /// 信号名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 信号类型
        /// </summary>
        public string SignalType;
        /// <summary>
        /// 管脚与信号线绑定关系
        /// </summary>
        public string EntityBelongTo;
        /// <summary>
        /// 数据
        /// </summary>
        public string Data;
        /// <summary>
        /// 独立线的集合，给出了线上的点
        /// </summary>
        public List<List<Point>> lines;
        /// <summary>
        /// 构造函数，进行最小的初始化
        /// </summary>
        public Cld_Signal()
        {
            lines = new List<List<Point>>();
        }
    }

    /// <summary>
    /// 描述CLd_Signal集合的类,包含了从objectid到 Cld_Signal对象的哈希表
    /// </summary>
    public class Cld_SignalS
    {
        /// <summary>
        /// 从objectid到 Cld_Signal对象的哈希表
        /// </summary>
        public Hashtable data;

        /// <summary>
        /// 构造函数，只进行初始化，不做任何实质性工作
        /// </summary>
        public Cld_SignalS()
        {
            data = new Hashtable();
        }
    }

    /// <summary>
    /// Cld_Module的描述类
    /// </summary>
    public class Cld_Module
    {
        /// <summary>
        /// 模块的唯一标志
        /// </summary>
        public string ObjectID;
        /// <summary>
        /// 控制器地址
        /// </summary>
        public string ControllerAddress;
        /// <summary>
        /// 组态文档名称
        /// </summary>
        public string DocumentName;
        /// <summary>
        /// 组态Sheet名称
        /// </summary>
        public string SheetName;
        /// <summary>
        /// 算法块名称
        /// </summary>
        public string AlgName;
        /// <summary>
        /// 算法执行顺序
        /// </summary>
        public string AlgOrder;
        /// <summary>
        /// 功能码名称
        /// </summary>
        public string FunctionName;
        /// <summary>
        /// 模块的坐标
        /// </summary>
        public string X_Y;
        /// <summary>
        /// 模块的图标
        /// </summary>
        public string SymbolName;
        /// <summary>
        /// 模块的描述信息
        /// </summary>
        public string DESCRP;
        /// <summary>
        /// 模块周期
        /// </summary>
        public string PERIOD;
        /// <summary>
        /// 相位
        /// </summary>
        public string PHASE;
        /// <summary>
        /// 
        /// </summary>
        public string LOOPID;

        /// <summary>
        /// 列位置
        /// </summary>
        public int col;
        /// <summary>
        /// 行位置
        /// </summary>
        public int row;
        /// <summary>
        /// 模块的X坐标
        /// </summary>
        public double x_position;
        /// <summary>
        /// 模块的Y坐标
        /// </summary>
        public double y_position;
        /// <summary>
        /// 获得X_Y中的X坐标
        /// </summary>
        public double X
        {
            get
            {
                int index = X_Y.IndexOf('_');
                return Int32.Parse(X_Y.Substring(0, index));
            }
        }
        /// <summary>
        /// 获得X_Y中的Y坐标
        /// </summary>
        public double Y
        {
            get
            {
                int index = X_Y.IndexOf('_');
                return Int32.Parse(X_Y.Substring(index + 1));
            }
        }

        /// <summary>
        /// 描述模块每个引脚的类型信息，从引脚名称到引脚类型的哈希，1：Input，2：Constant，3：Output 4:Internal
        /// </summary>
        public Hashtable PinType;
        /// <summary>
        /// 描述模块的输入引脚，从引脚名称到相应Cld_Module_Pin的哈希
        /// </summary>
        public Hashtable Input_Pin;
        /// <summary>
        /// 描述模块的输出引脚，从引脚名称到相应Cld_Module_Pin的哈希
        /// </summary>
        public Hashtable Out_Pin;
        /// <summary>
        /// 描述模块的Const引脚，引脚名称到相应Cld_Module_Pin的哈希
        /// </summary>
        public Hashtable Constant_Pin;
        /// <summary>
        /// 描述模块的Internal引脚，引脚名称到相应Cld_Module_Pin的哈希
        /// </summary>
        public Hashtable Internal_Pin;
        /// <summary>
        /// 描述模块的所有引脚，引脚名称到相应Cld_Module_Pin的哈希
        /// </summary>
        public Hashtable Pins;

        /// <summary>
        /// 此模块的输出所到达的模块列表，从object_id到Cld_Module的哈希
        /// </summary>
        public Hashtable post;
        /// <summary>
        /// 此模块的输入所来自的模块列表，从Object_id到Cld_Module的哈希
        /// </summary>
        public Hashtable pre;



        /// <summary>
        /// 构造函数
        /// </summary>
        public Cld_Module()
        {
            PinType = new Hashtable();
            Input_Pin = new Hashtable();
            Out_Pin = new Hashtable();
            Constant_Pin = new Hashtable();
            Internal_Pin = new Hashtable();
            Pins = new Hashtable();
            post = new Hashtable();
            pre = new Hashtable();
        }

        /// <summary>
        /// 获得模块的位置坐标，Point对象
        /// </summary>
        /// <returns></returns>
        public Point Get_Pos()
        {
            Point result = new Point();
            if (X_Y.Equals("") || !X_Y.Contains("_"))
            {
                throw new Exception("Cld_Module's position is not set");
            }
            result.x = X;
            result.y = Y;
            result.is_valid = true;
            return result;
        }


    }


    /// <summary>
    /// Cld_Module_Pin的描述类
    /// </summary>
    public class Cld_Module_Pin
    {
        /// <summary>
        /// 包含此引脚的模块的id
        /// </summary>
        public string ObjectID;
        /// <summary>
        /// 管脚（或规格数、IO、Tag）名称
        /// </summary>
        public string PinName;
        /// <summary>
        /// 管脚（或规格数、IO、Tag）值
        /// </summary>
        public string PinValue;
        /// <summary>
        /// 网络标号（所连接的连接线的唯一编号）
        /// </summary>
        public string NetworkID;
        /// <summary>
        /// 是否加入历史
        /// </summary>
        public bool IsHistory;
        /// <summary>
        /// 管脚坐标
        /// </summary>
        public string Point;
        /// <summary>
        /// 1：Input，2：Constant，3：Output 4:Internal
        /// </summary>
        public string PinType;
    }

    /// <summary>
    /// 描述Cld_Module集合的类
    /// </summary>
    public class Cld_ModuleS
    {
        /// <summary>
        /// Cld_Module的集合，从Object_id到Cld_Module对象的一个哈希
        /// </summary>
        public Hashtable cld_modules;
        /// <summary>
        /// Meta_ModuleS的对象，其中包含了所有Meta_Module的信息
        /// </summary>
        public readonly Meta_ModuleS meta_modules;
        /// <summary>
        /// 包含了模块的连线信息，从模块的object_id+"_"+pinname到Asignal
        /// </summary>
        public Signals signals;
        /// <summary>
        /// CLd_Signal的集合，包含了所有的cld_signal信息，从objectid到 Cld_Signal对象的哈希表
        /// </summary>
        public Cld_SignalS cld_signals;
        /// <summary>
        /// ControllerAddr
        /// </summary>
        public string ControllerAddr;
        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName;
        /// <summary>
        /// SheetName
        /// </summary>
        public string SheetName;

        /// <summary>
        /// 构造函数,从数据库中读取包含给定ControllerAddr,DocumentName,SheetName的Cld_Module的集合
        /// </summary>
        /// <param name="conn">到数据库的连接</param>
        /// <param name="ControllerAddr">Controller Address</param>
        /// <param name="DocumentName">Document Name</param>
        /// <param name="SheetName">Sheet Name</param>
        /// <param name="metamodules">Meta_Modules</param>
        public Cld_ModuleS(OleDbConnection conn, string ControllerAddr, string DocumentName, string SheetName, Meta_ModuleS metamodules)
        {
            //进行必要的初始化
            cld_modules = new Hashtable();
            signals = new Signals();
            cld_signals = new Cld_SignalS();
            meta_modules = metamodules;
            this.ControllerAddr = ControllerAddr;
            this.DocumentName = DocumentName;
            this.SheetName = SheetName;

            //建立数据库连接
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //数据库操作字符串
            string sql_string = "SELECT * FROM Cld_FCMaster WHERE ControllerAddress='" + ControllerAddr +
                "' AND DocumentName='" + DocumentName + "' AND SheetName='" + SheetName + "'";
            OleDbDataAdapter Cld_FCMaster_Adapter = new OleDbDataAdapter(sql_string, conn);
            DataSet myDataSet = new DataSet();
            Cld_FCMaster_Adapter.Fill(myDataSet, "Cld_FCMaster");


            //处理CLD FCMaster表的每一行
            for (int i = 0; i < myDataSet.Tables["Cld_FCMaster"].Rows.Count; i++)
            {
                Cld_Module cld_module_temp = new Cld_Module();

                //处理相应的Cld_FCMaster表
                cld_module_temp.ObjectID = myDataSet.Tables["Cld_FCMaster"].Rows[i]["ObjectID"].ToString();
                cld_module_temp.ControllerAddress = myDataSet.Tables["Cld_FCMaster"].Rows[i]["ControllerAddress"].ToString();
                cld_module_temp.DocumentName = myDataSet.Tables["Cld_FCMaster"].Rows[i]["DocumentName"].ToString();
                cld_module_temp.SheetName = myDataSet.Tables["Cld_FCMaster"].Rows[i]["SheetName"].ToString();
                cld_module_temp.AlgName = myDataSet.Tables["Cld_FCMaster"].Rows[i]["AlgName"].ToString();
                cld_module_temp.AlgOrder = myDataSet.Tables["Cld_FCMaster"].Rows[i]["AlgOrder"].ToString();
                cld_module_temp.FunctionName = myDataSet.Tables["Cld_FCMaster"].Rows[i]["FunctionName"].ToString();
                cld_module_temp.X_Y = myDataSet.Tables["Cld_FCMaster"].Rows[i]["X_Y"].ToString();
                cld_module_temp.SymbolName = myDataSet.Tables["Cld_FCMaster"].Rows[i]["SymbolName"].ToString();
                cld_module_temp.DESCRP = myDataSet.Tables["Cld_FCMaster"].Rows[i]["DESCRP"].ToString();
                cld_module_temp.PERIOD = myDataSet.Tables["Cld_FCMaster"].Rows[i]["PERIOD"].ToString();
                cld_module_temp.PHASE = myDataSet.Tables["Cld_FCMaster"].Rows[i]["PHASE"].ToString();
                cld_module_temp.LOOPID = myDataSet.Tables["Cld_FCMaster"].Rows[i]["LOOPID"].ToString();


                //处理相应的Cld_FCDetail表
                sql_string = "SELECT * FROM Cld_FCDetail WHERE ObjectID='" + cld_module_temp.ObjectID + "'";
                OleDbDataAdapter Cld_FCDetail_Adapter = new OleDbDataAdapter(sql_string, conn);
                Cld_FCDetail_Adapter.Fill(myDataSet, "Cld_FCDetail");

                for (int m = 0; m < myDataSet.Tables["Cld_FCDetail"].Rows.Count; m++)
                {
                    Cld_Module_Pin cld_module_pin_temp = new Cld_Module_Pin();

                    //对Cld_Module_Pin对象进行数据的填充
                    cld_module_pin_temp.ObjectID = myDataSet.Tables["Cld_FCDetail"].Rows[m]["ObjectID"].ToString();
                    cld_module_pin_temp.PinName = myDataSet.Tables["Cld_FCDetail"].Rows[m]["PinName"].ToString();
                    cld_module_pin_temp.PinValue = myDataSet.Tables["Cld_FCDetail"].Rows[m]["PinValue"].ToString();
                    cld_module_pin_temp.NetworkID = myDataSet.Tables["Cld_FCDetail"].Rows[m]["NetworkID"].ToString();
                    cld_module_pin_temp.IsHistory = (bool)(myDataSet.Tables["Cld_FCDetail"].Rows[m]["IsHistory"]);
                    cld_module_pin_temp.Point = myDataSet.Tables["Cld_FCDetail"].Rows[m]["Point"].ToString();

                    cld_module_temp.Pins[cld_module_pin_temp.PinName] = cld_module_pin_temp;
                    if (metamodules.Pin_Type(cld_module_temp.FunctionName, cld_module_pin_temp.PinName).Equals("Input"))
                    {
                        cld_module_pin_temp.PinType = "Input";
                        cld_module_temp.PinType[cld_module_pin_temp.PinName] = "Input";
                        cld_module_temp.Input_Pin[cld_module_pin_temp.PinName] = cld_module_pin_temp;
                    }
                    else if (metamodules.Pin_Type(cld_module_temp.FunctionName, cld_module_pin_temp.PinName).Equals("Output"))
                    {
                        cld_module_pin_temp.PinType = "Output";
                        cld_module_temp.PinType[cld_module_pin_temp.PinName] = "Output";
                        cld_module_temp.Out_Pin[cld_module_pin_temp.PinName] = cld_module_pin_temp;
                    }
                    else if (metamodules.Pin_Type(cld_module_temp.FunctionName, cld_module_pin_temp.PinName).Equals("Constant"))
                    {
                        cld_module_temp.PinType[cld_module_pin_temp.PinName] = "Constant";
                        cld_module_pin_temp.PinType = "Constant";
                        //cld_module_temp.Constant_Pin.Add(cld_module_pin_temp);
                        cld_module_temp.Constant_Pin[cld_module_pin_temp.PinName] = cld_module_pin_temp;
                    }
                    else if (metamodules.Pin_Type(cld_module_temp.FunctionName, cld_module_pin_temp.PinName).Equals("Internal"))
                    {
                        cld_module_pin_temp.PinType = "Internal";
                        cld_module_temp.PinType[cld_module_pin_temp.PinName] = "Internal";
                        //cld_module_temp.Internal_Pin.Add(cld_module_pin_temp);
                        cld_module_temp.Internal_Pin[cld_module_pin_temp.PinName] = cld_module_pin_temp;
                    }
                    else
                    {
                        //没有这种类型的引脚，因该是数据库有问题
                        cld_module_temp.PinType[cld_module_pin_temp.PinName] = "";
                        cld_module_pin_temp.PinType = "";

                    }
                }
                myDataSet.Tables["Cld_FCDetail"].Clear();
                cld_modules[cld_module_temp.ObjectID] = cld_module_temp;
            }

            //读取数据完毕
            myDataSet.Dispose();
            Cld_FCMaster_Adapter.Dispose();
            conn.Close();
        }

        /// <summary>
        /// 根据模块的Pin Value生成模块之间的连接关系
        /// </summary>
        /// <param name="ControllerAddr">ControllerAddr</param>
        /// <param name="DocumentName">DocumentName</param>
        /// <param name="SheetName">SheetName</param>
        public void Generate_Signal_Info(string ControllerAddr, string DocumentName, string SheetName)
        {
            List<ASignal> one_to_one_list = new List<ASignal>();//临时，存储一对一的Signal的集合
            ASignal temp_asignal;

            //对于每一个CLd_Module检查它的输入引脚的Value，寻找源端
            foreach (Cld_Module cld_module in cld_modules.Values)
            {
                foreach (Cld_Module_Pin cld_module_pin in cld_module.Input_Pin.Values)
                {//遍历每一个输入引脚
                    string pinvalue = cld_module_pin.PinValue;//输入引脚的值
                    if (!pinvalue.StartsWith(DocumentName))
                    {//检查合法性
                        continue;
                    }
                    int last_index_ = pinvalue.LastIndexOf("-");
                    int pinindex = Int32.Parse(pinvalue.Substring(last_index_ + 1));//源PinIndex
                    string objID = pinvalue.Substring(0, last_index_);//源Cld_Module的ID

                    Cld_Module src = (Cld_Module)cld_modules[objID];//源CLd_MOdule
                    if (src == null)
                    {
                        throw new Exception("In Table Cld_FCMaster,NO such objectid as " + objID);
                    }
                    string src_pin_name = this.meta_modules.PinName_By_FuncName_And_Index(src.FunctionName, pinindex);//源PinName

                    temp_asignal = new ASignal();
                    temp_asignal.start_module = src;
                    temp_asignal.start_pin = src_pin_name;
                    temp_asignal.end_modules.Add(cld_module);
                    temp_asignal.end_pins.Add(cld_module_pin.PinName);
                    one_to_one_list.Add(temp_asignal);
                }
            }
            //Functions.show_ASignal_List(one_to_one_list);//显示

            foreach (ASignal asignal in one_to_one_list)
            {
                Cld_Module start = asignal.start_module;
                string id_pinname = start.ObjectID + "_" + asignal.start_pin;
                if (!signals.data.Contains(id_pinname))
                {
                    temp_asignal = new ASignal();
                    temp_asignal.start_module = start;
                    temp_asignal.start_pin = asignal.start_pin;
                    temp_asignal.end_modules.Add(asignal.end_modules[0]);
                    temp_asignal.end_pins.Add(asignal.end_pins[0]);
                    signals.data[id_pinname] = temp_asignal;
                }
                else
                {
                    ((ASignal)signals.data[id_pinname]).end_modules.Add(asignal.end_modules[0]);
                    ((ASignal)signals.data[id_pinname]).end_pins.Add(asignal.end_pins[0]);
                }
            }
            //Functions.show_Signals(signals);//显示
        }

        /// <summary>
        /// 将连接关系转换为输入到输出的模式,返回一个哈希表，从＜objectid pinname＞到Asignal的哈希
        /// </summary>
        /// <returns></returns>
        public Hashtable Transform_Signal()
        {
            Hashtable result = new Hashtable();

            foreach (ASignal asignal in signals.data.Values)
            {
                for (int i = 0; i < asignal.end_modules.Count; i++)
                {
                    List<string> key = new List<string>();
                    key.Add(asignal.end_modules[i].ObjectID);
                    key.Add(asignal.end_pins[i]);
                    if (result.Contains(key))
                    {
                        ((ASignal)result[key]).end_modules.Add(asignal.start_module);
                        ((ASignal)result[key]).end_pins.Add(asignal.start_pin);
                    }
                    else
                    {
                        ASignal temp = new ASignal();
                        temp.start_module = asignal.end_modules[i];
                        temp.start_pin = asignal.end_pins[i];
                        temp.end_modules.Add(asignal.start_module);
                        temp.end_pins.Add(asignal.start_pin);
                        key = new List<string>();
                        key.Add(temp.start_module.ObjectID);
                        key.Add(temp.start_pin);
                        result[key] = temp;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据模块的连线关系，生成Cld_Signal数据
        /// 输入为：signals(从Cld_Module（开始模块）到ASignal的哈希)
        /// 输出：cld_signals(包含了所有的cld_signal信息，从objectid到 Cld_Signal对象的哈希表)
        /// </summary>
        /// <param name="meta_symbols">Meta_Symbols</param>
        public void Generate_Signal_Data(Meta_SymbolS meta_symbols)
        {
            #region 计算模块输入引脚延伸的长度
            //Cld_FCMaster id, PinName 到Asignal的哈希
            Hashtable id_name_to_asignal = Transform_Signal();

            //Cld_FCMaster id到ASignal List的哈希
            Hashtable id_to_asignal_list = new Hashtable();
            foreach (List<string> id_name_list in id_name_to_asignal.Keys)
            {
                if (id_to_asignal_list.Contains(id_name_list[0]))
                {
                    ((List<ASignal>)id_to_asignal_list[id_name_list[0]]).Add((ASignal)id_name_to_asignal[id_name_list]);
                }
                else
                {
                    List<ASignal> t = new List<ASignal>();
                    t.Add((ASignal)id_name_to_asignal[id_name_list]);
                    id_to_asignal_list[id_name_list[0]] = t;
                }
            }



            //对每一个Cld模块
            foreach (string id in id_to_asignal_list.Keys)
            {
                List<ASignal> asignal_list = (List<ASignal>)id_to_asignal_list[id];

                //表示一个cld_module_pin连线的方向 ，true means "up"  false means "down or h"

                foreach (ASignal asignal in asignal_list)
                {
                    //对每一个CLd模块的每一个输入引脚的连线进行处理


                    Cld_Module start_module = asignal.start_module;//开始模块
                    string start_pin = asignal.start_pin;//开始PinName
                    Cld_Module_Pin start_cld_pin_obj = (Cld_Module_Pin)start_module.Pins[start_pin];
                    Meta_Module meta_module = (Meta_Module)meta_modules.meta_modules[start_module.FunctionName];
                    Meta_Module_Pin start_pin_obj = meta_module.Pin_Obj_By_Pin_Name(start_pin);//开始Pin对象
                    Point start_module_point = start_module.Get_Pos();//模块的绝对位置坐标
                    Meta_SymbolMaster meta_symbol_master = (Meta_SymbolMaster)meta_symbols.meta_symbols[start_module.SymbolName];
                    Point start_pin_relative_point = (Point)meta_symbol_master.Pin_Relative_Pos[start_pin_obj];//开始点的相对坐标
                    Point start_pin_abs_point = start_module_point + start_pin_relative_point;



                    Cld_Module end_module = asignal.end_modules[0];//结束模块
                    string end_pin = asignal.end_pins[0];//结束PinName
                    Cld_Module_Pin end_cld_pin_obj = (Cld_Module_Pin)end_module.Pins[end_pin];
                    Meta_Module meta_module_end = (Meta_Module)meta_modules.meta_modules[end_module.FunctionName];
                    Meta_Module_Pin end_pin_obj = meta_module_end.Pin_Obj_By_Pin_Name(end_pin);//结束Pin对象
                    Point end_module_point = end_module.Get_Pos();//结束模块的绝对坐标
                    Meta_SymbolMaster meta_symbol_master_end = (Meta_SymbolMaster)meta_symbols.meta_symbols[end_module.SymbolName];
                    Point end_pin_relative_point = (Point)meta_symbol_master_end.Pin_Relative_Pos[end_pin_obj];//结束点的相对坐标
                    Point end_pin_abs_point = end_module_point + end_pin_relative_point;

                    if (start_pin_abs_point.y <= end_pin_abs_point.y)
                    {
                        //此Pin的连线向下
                        List<int> updown_and_pinindex = new List<int>();
                        updown_and_pinindex.Add(0);
                        updown_and_pinindex.Add(Int32.Parse(start_pin_obj.PinIndex));
                        asignal.annote = updown_and_pinindex;
                        asignal.start_pin_index = Int32.Parse(start_pin_obj.PinIndex);

                        asignal.start_direction = ASignal.DIR.DOWN;
                    }
                    else
                    {
                        //此Pin的连线向下
                        List<int> updown_and_pinindex = new List<int>();
                        updown_and_pinindex.Add(1);
                        updown_and_pinindex.Add(Int32.Parse(start_pin_obj.PinIndex));
                        asignal.annote = updown_and_pinindex;
                        asignal.start_pin_index = Int32.Parse(start_pin_obj.PinIndex);

                        asignal.start_direction = ASignal.DIR.UP;
                    }

                }
            }

            //Cld_FCMaster id到ASignal List的哈希
            Pin_length pin_length = new Pin_length(id_to_asignal_list);
            #endregion

            //最终数据库中要插入的signal的对象
            Cld_Signal temp;
            //处理每一个连线关系
            foreach (ASignal asignal in signals.data.Values)
            {
                Cld_Module start_module = asignal.start_module;//开始模块
                string start_pin = asignal.start_pin;//开始PinName
                Cld_Module_Pin start_cld_pin_obj = (Cld_Module_Pin)start_module.Pins[start_pin];
                Meta_Module meta_module = (Meta_Module)meta_modules.meta_modules[start_module.FunctionName];
                Meta_Module_Pin start_pin_obj = meta_module.Pin_Obj_By_Pin_Name(start_pin);//开始Pin对象
                Point start_module_point = start_module.Get_Pos();//模块的绝对位置坐标
                Meta_SymbolMaster meta_symbol_master = (Meta_SymbolMaster)meta_symbols.meta_symbols[start_module.SymbolName];
                Point start_pin_relative_point = (Point)meta_symbol_master.Pin_Relative_Pos[start_pin_obj];//开始点的相对坐标
                Point start_pin_abs_point = start_module_point + start_pin_relative_point;

                temp = new Cld_Signal();
                temp.ObjectID = start_module.ObjectID + "_" + start_pin;
                temp.ControllerAddress = ControllerAddr;
                temp.DocumentName = DocumentName;
                temp.SheetName = SheetName;
                temp.Name = "";
                temp.SignalType = "";
                temp.EntityBelongTo = "";

                //开始点的坐标 + { + 模块Id.pinName +} +，
                for (int i = 0; i < asignal.end_modules.Count; i++)
                {
                    Cld_Module end_module = asignal.end_modules[i];//结束模块
                    string end_pin = asignal.end_pins[i];//结束PinName
                    Cld_Module_Pin end_cld_pin_obj = (Cld_Module_Pin)end_module.Pins[end_pin];
                    Meta_Module meta_module_end = (Meta_Module)meta_modules.meta_modules[end_module.FunctionName];
                    Meta_Module_Pin end_pin_obj = meta_module_end.Pin_Obj_By_Pin_Name(end_pin);//结束Pin对象
                    Point end_module_point = end_module.Get_Pos();//结束模块的绝对坐标
                    Meta_SymbolMaster meta_symbol_master_end = (Meta_SymbolMaster)meta_symbols.meta_symbols[end_module.SymbolName];
                    Point end_pin_relative_point = (Point)meta_symbol_master_end.Pin_Relative_Pos[end_pin_obj];//结束点的相对坐标
                    Point end_pin_abs_point = end_module_point + end_pin_relative_point;

                    List<Point> line = new List<Point>();//一条线的数据
                    if (start_pin_abs_point.x - end_pin_abs_point.x < 0)
                    {
                        #region 从前到后
                        //首先判断源和目的是否在一条直线上
                        if (start_pin_abs_point.y == end_pin_abs_point.y)
                        {
                            //在一条直线上
                            start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                            line.Add(start_pin_abs_point);
                            end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                            line.Add(end_pin_abs_point);
                        }
                        else
                        {
                            //不在一条直线上，那么通过两个折点进行连接
                            Point temp_point;
                            //double layout_before_temp = end_module.line_layout_before.Next();
                            double layout_before_temp = pin_length.Get_Length(end_module.ObjectID, end_pin);
                            start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                            line.Add(start_pin_abs_point);//start
                            //第一个折点
                            temp_point = new Point();
                            temp_point.y = start_pin_abs_point.y;
                            temp_point.x = end_pin_abs_point.x - layout_before_temp;
                            temp_point.is_valid = true;
                            line.Add(temp_point);
                            //第二个折点
                            temp_point = new Point();
                            temp_point.y = end_pin_abs_point.y;
                            temp_point.x = end_pin_abs_point.x - layout_before_temp;
                            temp_point.is_valid = true;
                            line.Add(temp_point);
                            //最后的end
                            end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                            line.Add(end_pin_abs_point);
                        }
                        #endregion
                    }
                    else if (start_pin_abs_point.x - end_pin_abs_point.x == 0)
                    {
                        #region 在同一垂直方向上
                        if (start_pin_abs_point.y == end_pin_abs_point.y)
                        {
                            //在一条直线上
                            start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                            line.Add(start_pin_abs_point);
                            end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                            line.Add(end_pin_abs_point);
                        }
                        else
                        {
                            //不在一条直线上，直接相连x坐标相同
                            start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                            line.Add(start_pin_abs_point);//start

                            //最后的end
                            end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                            line.Add(end_pin_abs_point);
                        }
                        #endregion
                    }
                    else
                    {
                        #region 从后到前
                        //首先判断源和目的是否在一条水平线上
                        if (start_pin_abs_point.y == end_pin_abs_point.y)
                        {
                            //在一条水平线上
                            start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                            line.Add(start_pin_abs_point);
                            end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                            line.Add(end_pin_abs_point);
                        }
                        else
                        {
                            //不在一条水平线上,通过四个折点连接
                            if (start_module.ObjectID.Equals(end_module.ObjectID))
                            {
                                #region 开始和结束为同一个模块
                                start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                                line.Add(start_pin_abs_point);//start
                                //第一个折点
                                Point temp_point1 = new Point();
                                temp_point1.x = start_pin_abs_point.x + 30;
                                temp_point1.y = start_pin_abs_point.y;
                                temp_point1.is_valid = true;
                                line.Add(temp_point1);
                                //第二个折点
                                Point temp_point2 = new Point();
                                double upper_y = start_module_point.y - 15;
                                double lower_y = ((Meta_SymbolMaster)meta_symbols.meta_symbols[start_module.SymbolName]).Height + start_module_point.y + 15;
                                double upper_sum = start_pin_abs_point.y - upper_y + end_pin_abs_point.y - upper_y;
                                double lower_sum = lower_y - start_pin_abs_point.y + lower_y - end_pin_abs_point.y;
                                if (upper_sum > lower_sum)
                                {
                                    //第二个折点的y为lower
                                    temp_point2.x = temp_point1.x;
                                    temp_point2.y = lower_y;
                                }
                                else
                                {
                                    //第二个折点的y为upper
                                    temp_point2.x = temp_point1.x;
                                    temp_point2.y = upper_y;
                                }
                                temp_point2.is_valid = true;
                                line.Add(temp_point2);
                                //第三个折点
                                Point temp_point3 = new Point();
                                temp_point3.x = end_pin_abs_point.x - pin_length.Get_Length(end_module.ObjectID, end_pin);
                                temp_point3.y = temp_point2.y;
                                temp_point3.is_valid = true;
                                line.Add(temp_point3);
                                //第四个折点
                                Point temp_point4 = new Point();
                                temp_point4.x = temp_point3.x;
                                temp_point4.y = end_pin_abs_point.y;
                                temp_point4.is_valid = true;
                                line.Add(temp_point4);
                                //最后的end
                                end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                                line.Add(end_pin_abs_point);
                                #endregion
                            }
                            else
                            {
                                #region signal的开始和结束不为同一个模块
                                start_pin_abs_point.cld_module_pin = start_cld_pin_obj;
                                line.Add(start_pin_abs_point);//start
                                //第一个折点
                                Point temp_point1 = new Point();
                                temp_point1.x = start_pin_abs_point.x + 30;
                                temp_point1.y = start_pin_abs_point.y;
                                temp_point1.is_valid = true;
                                line.Add(temp_point1);
                                //第二个折点
                                Point temp_point2 = new Point();
                                temp_point2.x = temp_point1.x;
                                temp_point2.y = (end_pin_abs_point.y + start_pin_abs_point.y) / 2;
                                temp_point2.is_valid = true;
                                line.Add(temp_point2);
                                //第三个折点
                                Point temp_point3 = new Point();
                                temp_point3.x = end_pin_abs_point.x - pin_length.Get_Length(end_module.ObjectID, end_pin);
                                temp_point3.y = temp_point2.y;
                                temp_point3.is_valid = true;
                                line.Add(temp_point3);
                                //第四个折点
                                Point temp_point4 = new Point();
                                temp_point4.x = temp_point3.x;
                                temp_point4.y = end_pin_abs_point.y;
                                temp_point4.is_valid = true;
                                line.Add(temp_point4);
                                //最后的end
                                end_pin_abs_point.cld_module_pin = end_cld_pin_obj;
                                line.Add(end_pin_abs_point);
                                #endregion
                            }

                        }
                        #endregion
                    }

                    temp.lines.Add(line);

                }//end of foreach end_module

                //将数据进行合并，构成一个统一的数据
                temp.Data = Combine_Lines(temp.lines);

                cld_signals.data[temp.ObjectID] = temp;
            }
        }

        /// <summary>
        /// 将从同一模块开始的多个Asignal进行合并
        /// </summary>
        /// <param name="lines">同一模块开始的多个Asignal</param>
        /// <returns>相应的Asignal数据</returns>
        private string Combine_Lines(List<List<Point>> lines)
        {
            string result = "";
            foreach (List<Point> line in lines)
            {
                result = result + Combine_Point(line);
            }

            return result;
        }

        /// <summary>
        /// 将一个Point List转化为相应的signals数据
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string Combine_Point(List<Point> line)
        {
            string result = "";
            for (int i = 0; i < line.Count; i++)
            {
                if (i == 0)
                {
                    Debug.Assert(line[i].cld_module_pin != null);
                    result = result + line[i].x + "_" + line[i].y + "{" + line[i].cld_module_pin.ObjectID + "." + line[i].cld_module_pin.PinName + "},";
                }
                if (i == line.Count - 1)
                {
                    Debug.Assert(line[i].cld_module_pin != null);
                    result = result + line[i].x + "_" + line[i].y + "{" + line[i].cld_module_pin.ObjectID + "." + line[i].cld_module_pin.PinName + "};";
                }
                else
                {
                    //Debug.Assert(line[i].cld_module_pin == null);
                    result = result + line[i].x + "_" + line[i].y + ",";
                }
            }
            return result;
        }

        /// <summary>
        /// 将CLd_Signals数据插入到Cld_Signal表中
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public void Insert_SignalData_Into_Table(OleDbConnection conn)
        {
            //建立数据库连接
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //数据库的插入
            //清空表
            string sql_string = "SELECT * FROM Cld_Signal";
            OleDbCommand comm = new OleDbCommand(sql_string, conn);
            comm.CommandText = "DELETE * FROM Cld_Signal WHERE ControllerAddress ='" + ControllerAddr + "'AND DocumentName = '" + DocumentName +
                "' AND SheetName = '" + SheetName + "'";
            comm.ExecuteScalar();

            DataSet myDataSet = new DataSet();
            sql_string = "SELECT * FROM Cld_Signal";
            OleDbDataAdapter cld_signal_adapter = new OleDbDataAdapter(sql_string, conn);
            cld_signal_adapter.Fill(myDataSet, "Cld_Signal");

            foreach (Cld_Signal cld_signal in cld_signals.data.Values)
            {
                //向cld_signal表中添加数据
                DataRow datarow = myDataSet.Tables["Cld_Signal"].NewRow();

                datarow["ObjectID"] = cld_signal.ObjectID;
                datarow["ControllerAddress"] = cld_signal.ControllerAddress;
                datarow["DocumentName"] = cld_signal.DocumentName;
                datarow["SheetName"] = cld_signal.SheetName;
                datarow["Name"] = cld_signal.Name;
                datarow["SignalType"] = cld_signal.SignalType;
                datarow["EntityBelongTo"] = cld_signal.EntityBelongTo;
                datarow["Data"] = cld_signal.Data;

                myDataSet.Tables["Cld_Signal"].Rows.Add(datarow);
            }

            //更新数据
            new OleDbCommandBuilder(cld_signal_adapter);
            cld_signal_adapter.Update(myDataSet, "Cld_Signal");
            myDataSet.AcceptChanges();
        }

    }
    /// <summary>
    /// 计算Pin连线向外延伸部分的长度的类
    /// </summary>
    public class Pin_length
    {
        /// <summary>
        /// Cld_FCMaster id到ASignal List的哈希
        /// ASignal List表示此FCMaster中的每一个输入Pin到相应输出Pin的Asignal的集合
        /// </summary>
        private Hashtable id_to_asignalList;
        /// <summary>
        /// Pin延伸的最小长度
        /// </summary>
        private double min_length = 5;
        /// <summary>
        /// Pin延伸每次的增量
        /// </summary>
        private double incr_unit = 5;


        /// <summary>
        /// 构造函数，计算每一个模块每一个引脚向外延伸部分的长度，并将其写入到Asignal中
        /// </summary>
        /// <param name="input">
        /// Cld_FCMaster id到ASignal List的哈希,
        /// ASignal List表示此FCMaster中的每一个输入Pin到相应输出Pin的Asignal的集合
        /// </param>
        public Pin_length(Hashtable input)
        {
            id_to_asignalList = input;
            //根据PinIndex排序后的哈希表
            Hashtable new_hash = new Hashtable();

            foreach (string id in id_to_asignalList.Keys)
            {
                //对应一个id的所有asignal_list
                List<ASignal> asignal_list = (List<ASignal>)id_to_asignalList[id];

                //pinindex到Asignal的哈希表
                Hashtable temp = new Hashtable();
                //pinindex的list
                List<int> pinindex_list = new List<int>();

                foreach (ASignal signal in asignal_list)
                {
                    temp[signal.start_pin_index] = signal;
                    pinindex_list.Add(signal.start_pin_index);
                }
                //进行排序
                pinindex_list.Sort();
                List<ASignal> adjust = new List<ASignal>();
                for (int i = 0; i < pinindex_list.Count; i++)
                {
                    adjust.Add((ASignal)temp[pinindex_list[i]]);
                }
                new_hash[id] = adjust;
            }
            //更新为排序后的哈希表
            id_to_asignalList = new_hash;

            //排序完成,进行延伸长度的计算
            foreach (string id in id_to_asignalList.Keys)
            {
                //分别对每一个模块的引脚进行计算
                List<ASignal> asignal_list = (List<ASignal>)id_to_asignalList[id];
                Process(asignal_list);
            }


        }

        /// <summary>
        /// 构造函数，计算每一个模块每一个引脚向外延伸部分的长度，并将其写入到Asignal中
        /// </summary>
        /// <param name="input">
        /// Cld_FCMaster id到ASignal List的哈希,
        /// ASignal List表示此FCMaster中的每一个输入Pin到相应输出Pin的Asignal的集合
        /// </param>
        /// <param name="min_length">延伸部分最小的长度</param>
        /// <param name="incr">延伸部分的增量</param>
        public Pin_length(Hashtable input, double min_length, double incr)
            : this(input)
        {
            this.min_length = min_length;
            this.incr_unit = incr;
        }

        /// <summary>
        /// 计算一个模块Pin延伸部分的长度
        /// </summary>
        /// <param name="asignal_list">与一个模块相关的输入Asignal的List</param>
        public void Process(List<ASignal> asignal_list)
        {
            //所有的Pin都向上连
            bool all_up = true;
            //所有的Pin都向下连
            bool all_down = true;

            foreach (ASignal asignal in asignal_list)
            {
                if (asignal.start_direction != ASignal.DIR.UP)
                {
                    all_up = false;
                }
                if (asignal.start_direction != ASignal.DIR.DOWN)
                {
                    all_down = false;
                }
            }

            if (all_up)
            {
                //所有的都向上连
                double temp = this.min_length;
                for (int i = 0; i < asignal_list.Count; i++)
                {
                    asignal_list[i].start_length = temp;
                    temp = temp + this.incr_unit;
                }
                return;
            }

            if (all_down)
            {
                //所有的都向下连
                //int start = 5;
                double temp = this.min_length;
                for (int i = asignal_list.Count - 1; i >= 0; i--)
                {
                    asignal_list[i].start_length = temp;
                    temp = temp + this.incr_unit;
                }
                return;
            }


            //一部分向上连，一部分向下连
            bool is_part_up_part_down = true;
            //向上向下的分界点,sep_index -1 到 0 都向上连
            int sep_index = 0;

            for (int i = 0; i < asignal_list.Count; i++)
            {
                if (asignal_list[i].start_direction != ASignal.DIR.UP)
                {
                    sep_index = i;
                    break;
                }
            }
            for (int i = sep_index; i < asignal_list.Count; i++)
            {
                if (asignal_list[i].start_direction != ASignal.DIR.DOWN)
                {
                    is_part_up_part_down = false;
                }
            }

            if (is_part_up_part_down)
            {
                //一部分向上连，一部分向下连，且有明确的分界点
                //int start = 5;
                double temp = this.min_length;
                for (int i = 0; i < sep_index; i++)
                {
                    asignal_list[i].start_length = temp;
                    temp = temp + this.incr_unit;
                }
                //start = 5;
                temp = this.min_length;
                for (int i = asignal_list.Count - 1; i >= sep_index; i--)
                {
                    asignal_list[i].start_length = temp;
                    temp = temp + this.incr_unit;
                }
            }
            else
            {
                //一般的布局,不应该存在这种情况
                //int start = 5;
                double temp = this.min_length;
                for (int i = 0; i < asignal_list.Count; i++)
                {
                    asignal_list[i].start_length = temp;
                    temp = temp + this.incr_unit;
                }
            }
        }

        /// <summary>
        /// 取得相应模块相应Pinname的延伸长度
        /// </summary>
        /// <param name="id">cld_module_pin的ID</param>
        /// <param name="pinname">PinName</param>
        /// <returns>延伸长度</returns>
        public double Get_Length(string id, string pinname)
        {
            if (!id_to_asignalList.Contains(id))
            {
                throw (new Exception("In Pin_Length:Get_Length(string id,string pinname),no such id"));
            }
            List<ASignal> asignal_list = (List<ASignal>)id_to_asignalList[id];
            if (asignal_list == null)
            {
                return 0.0;
            }
            foreach (ASignal asignal in asignal_list)
            {
                if (asignal.start_pin.Equals(pinname) && asignal.start_module.ObjectID.Equals(id))
                {
                    return asignal.start_length;
                }
            }
            return 0;
        }
    }



}
