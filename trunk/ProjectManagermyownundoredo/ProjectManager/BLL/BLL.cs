using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using NHibernate;
using System.Drawing;
using TDK.Core.Logic.DAL;
using TDK.Core.Logic.DAL;

namespace TDK.Core.Logic.BLL
{
    /// <summary>
    /// 业务逻辑层操作接口
    /// </summary>
    public partial class BllManager
    {
        /// <summary>
        /// 缓存了Meta_FCMaster,以及Meta_FCDetail的元数据
        /// 是function name 到 meta_Fcmaster的哈希表
        /// </summary>
        private Hashtable funcname_to_MetaFCMaster;

        /// <summary>
        /// 对象访问接口
        /// </summary>
        public PrjManager manager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prjmanager"></param>
        public BllManager(PrjManager prjmanager)
        {
            this.manager = prjmanager;
            this.funcname_to_MetaFCMaster = new Hashtable();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session"></param>
        public BllManager(ISession session)
        {
            this.manager = new PrjManager(session);
            this.funcname_to_MetaFCMaster = new Hashtable();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="start_pin"></param>
        /// <param name="end"></param>
        /// <param name="end_pin"></param>
        /// <returns></returns>
        public Exception Add_Persis_Signal(Cld_FCBlock start, string start_pin, Cld_FCBlock end, string end_pin)
        {
            //检查 从输出到输入的规则是否满足
            bool has = false;
            foreach (Cld_FCOutput outpin in manager.OutputCRUD.GetCld_FCOutputs_By_Cld_FCBlock_ID(start.ID))
            {
                if (outpin.PinName.Equals(start_pin))
                {
                    has = true;
                    break;
                }
            }
            if (!has)
            {
                return new Exception("cld_fcblock " + start.AlgName + " does not have the pin " + start_pin);
            }
            has = false;
            foreach (Cld_FCInput inpin in manager.InputCRUD.GetCld_FCInputs_By_Cld_FCBlock_ID(end.ID))
            {
                if (inpin.PinName.Equals(end_pin))
                {
                    has = true;
                    break;
                }
            }
            if (!has)
            {
                return new Exception("cld_fcblocl " + end.AlgName + " does not have the pin " + end_pin);
            }
            //检查 一个输入引脚只能有一个连线的规则是否满足


            return null;
        }

        public enum range
        {
            /// <summary>
            /// 一个sheet 范围
            /// </summary>
            Sheet,
            /// <summary>
            /// 一个Document的范围
            /// </summary>
            Document,
            /// <summary>
            /// 一个Cotroller的范围
            /// </summary>
            Cotroller
        }

        /// <summary>
        /// 判断给定的Cld_FCBlock的algname，在给定的范围内是否可用,,,,,,
        /// </summary>
        /// <param name="rangename">范围</param>
        /// <param name="id">具体范围的ID</param>
        /// <param name="name">给定的FCBlock的algname</param>
        /// <returns></returns>
        public bool can_use_as_fcblock_algname(range rangename, int id, string name)
        {
            IList<Cld_FCBlock> fcblocks;
            switch (rangename)
            {
                case range.Sheet:
                    fcblocks = manager.BlockCRUD.GetCld_FCBlocks_By_Prj_Sheet_ID(id);
                    foreach (Cld_FCBlock block in fcblocks)
                    {
                        if (block.AlgName.Equals(name))
                        {
                            return false;
                        }
                    }
                    break;
                case range.Document:
                    fcblocks = manager.BlockCRUD.GetCld_FCBlocks_By_Prj_Document_ID(id);
                    foreach (Cld_FCBlock block in fcblocks)
                    {
                        if (block.AlgName.Equals(name))
                        {
                            return false;
                        }
                    }
                    break;
                case range.Cotroller:
                    fcblocks = manager.BlockCRUD.GetCld_FCBlocks_By_Prj_Controller_ID(id);
                    foreach (Cld_FCBlock block in fcblocks)
                    {
                        if (block.AlgName.Equals(name))
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 产生Cld_FCBlock的大小及引脚坐标
        /// </summary>
        /// <param name="block"></param>
        /// <param name="pos"></param>
        public void generate_Rec_symbol(Cld_FCBlock block, rela_pos pos)
        {
            Regex regPointName = new Regex(@"^(\d+)((-(\d+)){3})$");
            symbol sym = new symbol();
            float width = 60f;              // 初始宽度
            float height = 0f;              // 初始高度
            float headHeight = 15f;         // 上边距
            float footHeight = 15f;         // 下边距
            float pinSpace = 15f;           // 引脚间隔
            //List<string> edges = null;    // 各条边的信息

            List<Cld_FCInput> VisibleInputs = new List<Cld_FCInput>();
            List<Cld_FCOutput> VisibleOutputs = new List<Cld_FCOutput>();
            List<string> VisiblePointNames = new List<string>();

            foreach (Cld_FCInput input in block.Cld_FCInput_List)
            {
                // InputPin 的索引也从 0 开始  ___WangXiang
                input.PinIndex = get_pin_index(block.FunctionName, input.PinName) - block.Cld_FCOutput_List.Count;

                // input.PointName!=null的时候不管是否Visible也要占一个Pin位置  ___WangXiang
                bool display = input.Visible || (input.PointName != null && regPointName.IsMatch(input.PointName));
                if (display)
                {
                    VisibleInputs.Add(input);
                }
            }

            foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
            {
                output.PinIndex = get_pin_index(block.FunctionName, output.PinName);

                // output的Visible只控制PointName的显示。 ___WangXiang
                // 当Visible==true或outputPin有连线时，始终占一个Pin的位置（未处理）
                if (output.Visible)
                {
                    VisibleOutputs.Add(output);
                }
                else
                {
                    foreach (Cld_FCBlock blockTemp in block.Prj_Sheet.Cld_FCBlock_List)
                    {
                        foreach (Cld_FCInput input in blockTemp.Cld_FCInput_List)
                        {
                            if (input.Visible && input.PointName == output.PointName)
                            {
                                VisibleOutputs.Add(output);
                                break;
                            }
                        } 
                    }
                }
            }

            int max = (VisibleInputs.Count > VisibleOutputs.Count) ? VisibleInputs.Count : VisibleOutputs.Count;
            if (max == 0)
            {
                //当模块引脚的个数为0
                height = (float)(headHeight + footHeight);
            }
            else if (max > 0)
            {
                height = (float)(headHeight + footHeight + max * pinSpace);
            }
            else
            {
                throw new Exception("the pin number should not be negative");
            }
            //产生矩形的四条边
            //edges = generate_edge(width, height, Color.Black, pos);


            //根据Pin的Index对输入引脚和输出引脚进行排序
            VisibleInputs.Sort(new Cld_FCInput_Compare());
            VisibleOutputs.Sort(new Cld_FCOutput_Compare());

            switch (pos)
            {
                case rela_pos.UPLEFT:
                    for (int i = 0; i < VisibleInputs.Count; i++)
                    {
                        VisibleInputs[i].Point = "0_" + (headHeight + i * pinSpace + pinSpace / 2);
                    }
                    for (int i = 0; i < VisibleOutputs.Count; i++)
                    {
                        VisibleOutputs[i].Point = (width) + "_" + (headHeight + i * pinSpace + pinSpace / 2);
                    }
                    break;
                case rela_pos.UPRIGHT:
                    for (int i = 0; i < VisibleInputs.Count; i++)
                    {
                        VisibleInputs[i].Point = (-width) + "_" + (headHeight + i * pinSpace);
                    }
                    for (int i = 0; i < VisibleOutputs.Count; i++)
                    {
                        VisibleOutputs[i].Point = "0_" + (headHeight + i * pinSpace);
                    }
                    break;
                case rela_pos.DOWNLEFT:
                    for (int i = 0; i < VisibleInputs.Count; i++)
                    {
                        VisibleInputs[i].Point = "0_" + (headHeight + i * pinSpace - height);
                    }
                    for (int i = 0; i < VisibleOutputs.Count; i++)
                    {
                        VisibleOutputs[i].Point = (width) + "_" + (headHeight + i * pinSpace - height);
                    }
                    break;
                case rela_pos.DOWNRIGHT:
                    for (int i = 0; i < VisibleInputs.Count; i++)
                    {
                        VisibleInputs[i].Point = (-width) + "_" + (headHeight + i * pinSpace - height);
                    }
                    for (int i = 0; i < VisibleOutputs.Count; i++)
                    {
                        VisibleOutputs[i].Point = "0_" + (headHeight + i * pinSpace - height);
                    }
                    break;
                case rela_pos.CENTER:
                    for (int i = 0; i < VisibleInputs.Count; i++)
                    {
                        VisibleInputs[i].Point = (-width) + "_" + (headHeight + i * pinSpace - height / 2);
                    }
                    for (int i = 0; i < VisibleOutputs.Count; i++)
                    {
                        VisibleOutputs[i].Point = (width / 2) + "_" + (headHeight + i * pinSpace - height / 2);
                    }
                    break;
                default:
                    break;

            }

            //填充symbol对象
            sym.kind = symbol_kinds.Rectangle;
            //sym.edges = edges;
            sym.height = height;
            sym.symbol_name = block.FunctionName;
            sym.width = width;
            block.Symbol = sym;
            block.Size = new SizeF(width, height);

        }

        /// <summary>
        /// CLd_Input类的比较器,按照Index进行排序
        /// </summary>
        public class Cld_FCInput_Compare : IComparer<Cld_FCInput>
        {
            public int Compare(Cld_FCInput t1, Cld_FCInput t2)
            {
                return t1.PinIndex - t2.PinIndex;
            }
        }

        /// <summary>
        /// CLd_Output类的比较器,按照Index进行排序
        /// </summary>
        public class Cld_FCOutput_Compare : IComparer<Cld_FCOutput>
        {
            public int Compare(Cld_FCOutput t1, Cld_FCOutput t2)
            {
                return t1.PinIndex - t2.PinIndex;
            }
        }

        /// <summary>
        /// 图标相对点的选取
        /// </summary>
        public enum rela_pos
        {
            /// <summary>
            /// 矩形的中心
            /// </summary>
            CENTER,
            /// <summary>
            /// 矩形的左上
            /// </summary>
            UPLEFT,
            /// <summary>
            /// 矩形的右上
            /// </summary>
            UPRIGHT,
            /// <summary>
            /// 矩形的左下
            /// </summary>
            DOWNLEFT,
            /// <summary>
            /// 矩形的右下
            /// </summary>
            DOWNRIGHT
        }

        /// <summary>
        /// 生成矩形的四条边
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color_string"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private List<string> generate_edge(double width, double height, Color color, rela_pos pos)
        {
            List<string> result = new List<string>();
            string color_string = "255_255_255";
            if (color != null)
            {
                color_string = color.R + "_" + color.G + "_" + color.B;
            }

            switch (pos)
            {
                case rela_pos.UPLEFT:
                    result.Add("0||" + color_string + "||0_0||" + width + "_0");
                    result.Add("0||" + color_string + "||" + width + "_0" + "||" + width + "_" + height);
                    result.Add("0||" + color_string + "||" + width + "_" + height + "||0_" + height);
                    result.Add("0||" + color_string + "||0_" + height + "||0_0");
                    break;
                case rela_pos.UPRIGHT:
                    result.Add("0||" + color_string + "||" + (-width) + "_0" + "||0_0");
                    result.Add("0||" + color_string + "||0_0" + "||0_" + height);
                    result.Add("0||" + color_string + "||0_" + height + "||" + (-width) + "_" + height);
                    result.Add("0||" + color_string + "||" + (-width) + "_" + height + "||" + (-width) + "_0");
                    break;
                case rela_pos.DOWNRIGHT:

                    break;
                case rela_pos.DOWNLEFT:

                    break;
                case rela_pos.CENTER:

                    break;
                default:
                    break;

            }
            return result;
        }

        
        /// <summary>
        /// 关闭与BBL相关的所有资源
        /// </summary>
        public void Close()
        {
            manager.Close();
        }

        /// <summary>
        /// 清空对象缓存
        /// </summary>
        public void Clear()
        {
            manager.Clear();
        }
    }
}
