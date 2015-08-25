using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using NHibernate;
using TDK.Core.Logic.BLL;
using System.Diagnostics;
using System.Reflection;


namespace TDK.Core.Logic.DAL
{
    /// <summary>
    /// 描述Prj_Sheet之间的差异
    /// </summary>
    public class SheetDiffer
    {
        public List<Cld_Signal> Signal_Newed;
        public List<Cld_Constant> Const_Newed;
        public List<Cld_FCBlock> Block_Newed;
        public List<Cld_Graphic> Graphic_Newed;
        public List<Cld_FCParameter> Para_Newed;
        public List<Cld_FCInput> Input_Newed;
        public List<Cld_FCOutput> Output_Newed;

        public List<Cld_Signal> Signal_Deleted;
        public List<Cld_Constant> Const_Deleted;
        public List<Cld_FCBlock> Block_Deleted;
        public List<Cld_Graphic> Graphic_Deleted;
        public List<Cld_FCParameter> Para_Deleted;
        public List<Cld_FCInput> Input_Deleted;
        public List<Cld_FCOutput> Output_Deleted;

        public List<Cld_Signal> Signal_Modified;
        public List<Cld_Constant> Const_Modified;
        public List<Cld_FCBlock> Block_Modified;
        public List<Cld_Graphic> Graphic_Modified;
        public List<Cld_FCParameter> Para_Modified;
        public List<Cld_FCInput> Input_Modified;
        public List<Cld_FCOutput> Output_Modified;
        public List<Prj_Sheet> Sheet_Modified;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SheetDiffer()
        {
            Signal_Newed = new List<Cld_Signal>();
            Const_Newed = new List<Cld_Constant>();
            Block_Newed = new List<Cld_FCBlock>();
            Graphic_Newed = new List<Cld_Graphic>();
            Para_Newed = new List<Cld_FCParameter>();
            Input_Newed = new List<Cld_FCInput>();
            Output_Newed = new List<Cld_FCOutput>();
            Signal_Deleted = new List<Cld_Signal>();
            Const_Deleted = new List<Cld_Constant>();
            Block_Deleted = new List<Cld_FCBlock>();
            Graphic_Deleted = new List<Cld_Graphic>();
            Para_Deleted = new List<Cld_FCParameter>();
            Input_Deleted = new List<Cld_FCInput>();
            Output_Deleted = new List<Cld_FCOutput>();
            Signal_Modified = new List<Cld_Signal>();
            Const_Modified = new List<Cld_Constant>();
            Block_Modified = new List<Cld_FCBlock>();
            Graphic_Modified = new List<Cld_Graphic>();
            Para_Modified = new List<Cld_FCParameter>();
            Input_Modified = new List<Cld_FCInput>();
            Output_Modified = new List<Cld_FCOutput>();
            Sheet_Modified = new List<Prj_Sheet>();
        }
    }

    public partial class Prj_SheetCRUD
    {
        /// <summary>
        /// 比较两个sheet的不同
        /// </summary>
        /// <param name="pre">操作前</param>
        /// <param name="after">操作后</param>
        /// <returns></returns>
        public SheetDiffer CompareSheet(Prj_Sheet pre, Prj_Sheet after)
        {
            SheetDiffer result = new SheetDiffer();
            if (pre.ID != after.ID) {
                throw new Exception("sheet id should be equal");
            }
            if (!pre.Compare(after))
            {
                result.Sheet_Modified.Add(after);
            }

            {//signal
                Hashtable id_to_signal1 = new Hashtable();
                foreach (Cld_Signal signal in pre.Cld_Signal_List)
                {
                    id_to_signal1[signal.ID] = signal;
                }

                foreach (Cld_Signal signal in after.Cld_Signal_List)
                {
                    if (id_to_signal1.Contains(signal.ID))
                    {
                        if (!((Cld_Signal)id_to_signal1[signal.ID]).Compare(signal))
                        {
                            result.Signal_Modified.Add(signal);
                        }
                        id_to_signal1.Remove(signal.ID);
                    }
                    else
                    {
                        result.Signal_Newed.Add(signal);
                    }
                }
                foreach (Cld_Signal signal in id_to_signal1.Values)
                {
                    result.Signal_Deleted.Add(signal);
                }
            }
            {//cld_fcblcok
                Hashtable id_to_block = new Hashtable();
                foreach (Cld_FCBlock block in pre.Cld_FCBlock_List)
                {
                    id_to_block[block.ID] = block;
                }

                foreach (Cld_FCBlock block in after.Cld_FCBlock_List)
                {
                    if (id_to_block.Contains(block.ID))
                    {
                        if (!((Cld_FCBlock)id_to_block[block.ID]).Compare(block))
                        {
                            result.Block_Modified.Add(block);
                        }

                        //input 
                        Hashtable id_to_input = new Hashtable();
                        foreach (Cld_FCInput input in ((Cld_FCBlock)id_to_block[block.ID]).Cld_FCInput_List)
                        {
                            id_to_input[input.ID] = input;
                        }
                        foreach (Cld_FCInput input in block.Cld_FCInput_List)
                        {
                            if (id_to_input.Contains(input.ID))
                            {
                                if (!((Cld_FCInput)id_to_input[input.ID]).Compare(input))
                                {
                                    result.Input_Modified.Add(input);
                                }
                                id_to_input.Remove(input.ID);
                            }
                            else
                            {
                                result.Input_Newed.Add(input);
                            }
                        }
                        foreach (Cld_FCInput input in id_to_input.Values)
                        {
                            result.Input_Deleted.Add(input);
                        }
                        //output
                        Hashtable id_to_output = new Hashtable();
                        foreach (Cld_FCOutput output in ((Cld_FCBlock)id_to_block[block.ID]).Cld_FCOutput_List)
                        {
                            id_to_output[output.ID] = output;
                        }
                        foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
                        {
                            if (id_to_output.Contains(output.ID))
                            {
                                if (!((Cld_FCOutput)id_to_output[output.ID]).Compare(output))
                                {
                                    result.Output_Modified.Add(output);
                                }
                                id_to_output.Remove(output.ID);
                            }
                            else
                            {
                                result.Output_Newed.Add(output);
                            }
                        }
                        foreach (Cld_FCOutput output in id_to_output.Values)
                        {
                            result.Output_Deleted.Add(output);
                        }
                        //para
                        Hashtable id_to_para = new Hashtable();
                        foreach (Cld_FCParameter p in ((Cld_FCBlock)id_to_block[block.ID]).Cld_FCParameter_List)
                        {
                            id_to_para[p.ID] = p;
                        }
                        foreach (Cld_FCParameter p in block.Cld_FCParameter_List)
                        {
                            if (id_to_para.Contains(p.ID))
                            {
                                if (!((Cld_FCParameter)id_to_para[p.ID]).Compare(p))
                                {
                                    result.Para_Modified.Add(p);
                                }
                                id_to_para.Remove(p.ID);
                            }
                            else
                            {
                                result.Para_Newed.Add(p);
                            }
                        }
                        foreach (Cld_FCParameter p in id_to_para.Values)
                        {
                            result.Para_Deleted.Add(p);
                        }

                        id_to_block.Remove(block.ID);
                    }
                    else
                    {
                        result.Block_Newed.Add(block);
                        block.Orin = true;
                        if (block.Cld_FCInput_List != null)
                        {
                            foreach (Cld_FCInput input in block.Cld_FCInput_List)
                            {
                                result.Input_Newed.Add(input);
                            }
                        }
                        if (block.Cld_FCOutput_List != null)
                        {
                            foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
                            {
                                result.Output_Newed.Add(output);
                            }
                        }
                        if (block.Cld_FCParameter_List != null)
                        {
                            foreach (Cld_FCParameter p in block.Cld_FCParameter_List)
                            {
                                result.Para_Newed.Add(p);
                            }
                        }
                        block.Orin = false;
                    }
                }
                foreach (Cld_FCBlock block in id_to_block.Values)
                {
                    result.Block_Deleted.Add(block);
                    foreach (Cld_FCInput input in block.Cld_FCInput_List)
                    {
                        result.Input_Deleted.Add(input);
                    }
                    foreach (Cld_FCOutput output in block.Cld_FCOutput_List)
                    {
                        result.Output_Deleted.Add(output);
                    }
                    foreach (Cld_FCParameter p in block.Cld_FCParameter_List)
                    {
                        result.Para_Deleted.Add(p);
                    }
                }
            }
            { //cld_const
                Hashtable id_to_const = new Hashtable();
                foreach (Cld_Constant con in pre.Cld_Constant_List)
                {
                    id_to_const[con.ID] = con;
                }

                foreach (Cld_Constant con in after.Cld_Constant_List)
                {
                    if (id_to_const.Contains(con.ID))
                    {
                        if (!((Cld_Constant)id_to_const[con.ID]).Compare(con))
                        {
                            result.Const_Modified.Add(con);
                        }
                        id_to_const.Remove(con.ID);
                    }
                    else
                    {
                        result.Const_Newed.Add(con);
                    }
                }
                foreach (Cld_Constant con in id_to_const.Values)
                {
                    result.Const_Deleted.Add(con);
                }
            }
            { //cld_graphic

                Hashtable id_to_grap = new Hashtable();
                foreach (Cld_Graphic g in pre.Cld_Graphic_List)
                {
                    id_to_grap[g.ID] = g;
                }
                foreach (Cld_Graphic g in after.Cld_Graphic_List)
                {
                    if (id_to_grap.Contains(g))
                    {
                        if (!((Cld_Graphic)id_to_grap[g.ID]).Compare(g))
                        {
                            result.Graphic_Modified.Add(g);
                        }
                        id_to_grap.Remove(g.ID);
                    }
                    else
                    {
                        result.Graphic_Newed.Add(g);
                    }
                }
                foreach (Cld_Graphic g in id_to_grap.Values)
                {
                    result.Graphic_Deleted.Add(g);
                }
            }


            return result;
        }


        /// <summary>
        /// 向数据库提交sheet
        /// </summary>
        /// <param name="diff"></param>
        /// <returns></returns>
        public void Commit_Sheet(SheetDiffer diff)
        {
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                //deleted注意删除的顺序，有可能发生“unexpected row count”异常
                foreach (Cld_Signal signal in diff.Signal_Deleted)
                {
                    this.session.Delete(signal);
                }
                foreach (Cld_Constant con in diff.Const_Deleted)
                {
                    this.session.Delete(con);
                }
                foreach (Cld_Graphic g in diff.Graphic_Deleted)
                {
                    this.session.Delete(g);
                }
                foreach (Cld_FCParameter p in diff.Para_Deleted)
                {
                    this.session.Delete(p);
                }
                foreach (Cld_FCInput input in diff.Input_Deleted)
                {
                    this.session.Delete(input);
                }
                foreach (Cld_FCOutput output in diff.Output_Deleted)
                {
                    this.session.Delete(output);
                }
                foreach (Cld_FCBlock block in diff.Block_Deleted)
                {
                    this.session.Delete(block);
                }

                //modified
                foreach (Cld_Signal signal in diff.Signal_Modified)
                {
                    this.session.SaveOrUpdate(signal);
                }
                foreach (Cld_Constant con in diff.Const_Modified)
                {
                    this.session.SaveOrUpdate(con);
                }
                foreach (Cld_Graphic g in diff.Graphic_Modified)
                {
                    this.session.SaveOrUpdate(g);
                }
                foreach (Cld_FCOutput output in diff.Output_Modified)
                {
                    this.session.SaveOrUpdate(output);
                }
                foreach (Cld_FCInput input in diff.Input_Modified)
                {
                    this.session.SaveOrUpdate(input);
                }
                foreach (Cld_FCParameter p in diff.Para_Modified)
                {
                    this.session.SaveOrUpdate(p);
                }
                foreach (Cld_FCBlock b in diff.Block_Modified)
                {
                    this.session.SaveOrUpdate(b);
                }

                //added
                foreach (Cld_Signal signal in diff.Signal_Newed)
                {
                    this.session.Save(signal);
                }
                foreach (Cld_Constant con in diff.Const_Newed)
                {
                    this.session.Save(con);
                }
                foreach (Cld_Graphic g in diff.Graphic_Newed)
                {
                    this.session.Save(g);
                }
                foreach (Cld_FCBlock block in diff.Block_Newed)
                {
                    this.session.Save(block);
                }
                foreach (Cld_FCParameter p in diff.Para_Newed)
                {
                    this.session.Save(p);
                }
                foreach (Cld_FCInput input in diff.Input_Newed)
                {
                    this.session.Save(input);
                }
                foreach (Cld_FCOutput output in diff.Output_Newed)
                {
                    this.session.Save(output);
                }
                transaction.Commit();
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }




        /// <summary>
        /// 向数据库提交一个Sheet
        /// 原理：
        /// 先从数据库中删除
        /// 然后在插入
        /// 效率问题？？？？！
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public bool Commit(Prj_Sheet sheet)
        {
            //首先删除数据库中的相关记录
            this.DeletePrj_Sheet_By_ID(sheet.ID);

            ITransaction transaction = this.session.BeginTransaction();
            int count = 0;
            try
            {
                //在进行操作之前进行缓存的处理，否则会因为对象的状态出现一些问题
                this.session.Flush();
                this.session.Clear();
                this.session.Save(sheet);
                this.session.Flush();
                this.session.Clear();

                //signal
                foreach (Cld_Signal signal in sheet.Cld_Signal_List)
                {
                    count++;
                    signal.Prj_Sheet_ID = sheet.ID;
                    this.session.Save(signal);
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //const
                foreach (Cld_Constant con in sheet.Cld_Constant_List)
                {
                    count++;
                    con.Prj_Sheet_ID = sheet.ID;
                    this.session.Save(con);
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //block,block重新插入时，ID会重新分配,需进行处理
                Hashtable oldid_to_newid = new Hashtable();
                foreach (Cld_FCBlock block in sheet.Cld_FCBlock_List)
                {
                    count++;
                    block.Prj_Sheet_ID = sheet.ID;
                    int oldid = block.ID;
                    this.session.Save(block);
                    oldid_to_newid[oldid] = block.ID;
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //graphic
                foreach (Cld_Graphic graphic in sheet.Cld_Graphic_List)
                {
                    count++;
                    graphic.Prj_Sheet_ID = sheet.ID;
                    this.session.Save(graphic);
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }
                //flush to db and clean cache
                this.session.Flush();
                this.session.Clear();

                //parameter
                foreach (Cld_FCParameter para in sheet.Cld_FCParameter_List)
                {
                    count++;
                    para.Prj_Sheet_ID = sheet.ID;
                    para.Cld_FCBlock_ID = (int)oldid_to_newid[para.Cld_FCBlock_ID];
                    this.session.Save(para);
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //out
                foreach (Cld_FCOutput output in sheet.Cld_FCOutput_List)
                {
                    count++;
                    output.Prj_Sheet_ID = sheet.ID;
                    output.Cld_FCBlock_ID = (int)oldid_to_newid[output.Cld_FCBlock_ID];
                    this.session.Save(output);
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //input
                foreach (Cld_FCInput input in sheet.Cld_FCInput_List)
                {
                    count++;
                    input.Prj_Sheet_ID = sheet.ID;
                    input.Cld_FCBlock_ID = (int)oldid_to_newid[input.Cld_FCBlock_ID];
                    this.session.Save(input);
                    if (count % 100 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }



    }
}
