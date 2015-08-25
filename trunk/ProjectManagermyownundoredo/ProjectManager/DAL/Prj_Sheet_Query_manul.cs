using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NHibernate;
using TDK.Core.Logic.URdoLib;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_SheetCRUD
    {
        /// <summary>
        /// 加载满足给定条件的sheet，以及与其相关的所有后代对象
        /// </summary>
        /// <returns></returns>
        public List<Prj_Sheet> Load_Sheet(string wherestring){
            IList<Prj_Sheet> sheets = this.Get_Prj_Sheet_By_Wherestring(wherestring);
            List<Prj_Sheet> result = new List<Prj_Sheet>();

            string sql_string = "";
            foreach(Prj_Sheet sheet in sheets){
                sql_string = sql_string + " Prj_Sheet_ID = " + sheet.ID + " or";
            }
            if(sql_string.EndsWith("or")){
                sql_string = sql_string.Substring(0, sql_string.Length - 2);
            }

            throw new Exception("not completely implemented");

            return result;
        }


        /// <summary>
        /// 根据给定的ID加载sheet，以及与其相关的所有后代对象,
        /// 如果不存在，则返回null
        /// </summary>
        /// <param name="sheetid"></param>
        /// <returns></returns>
        public Prj_Sheet Load_Sheet(int sheetid)
        {
            Hashtable table = new Hashtable();
            Prj_Sheet result;

            IList<Cld_FCBlock> blocks;
            IList<Cld_Signal> signals;
            IList<Cld_Constant> consts;
            IList<Cld_Graphic> graphics;
            IList<Cld_FCInput> inputs;
            IList<Cld_FCOutput> outputs;
            IList<Cld_FCParameter> parameters;

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    result = (Prj_Sheet)this.session.Get(typeof(Prj_Sheet), sheetid);
                    //如果没有找到对应的Prj_Sheet,返回null
                    if (result == null)
                    {
                        return null;
                    }
                    result.State = objstate.Loaded;
                    blocks = this.session.CreateQuery("from Cld_FCBlock as b where b.Prj_Sheet_ID = " + sheetid).List<Cld_FCBlock>();
                    signals = this.session.CreateQuery("from Cld_Signal as s where s.Prj_Sheet_ID = " + sheetid).List<Cld_Signal>();
                    consts = this.session.CreateQuery("from Cld_Constant as c where c.Prj_Sheet_ID = " + sheetid).List<Cld_Constant>();
                    graphics = this.session.CreateQuery("from Cld_Graphic as g where g.Prj_Sheet_ID = " + sheetid).List<Cld_Graphic>();
                    inputs = this.session.CreateQuery("from Cld_FCInput as i where i.Prj_Sheet_ID = " + sheetid).List<Cld_FCInput>();
                    outputs = this.session.CreateQuery("from Cld_FCOutput as o where o.Prj_Sheet_ID = " + sheetid).List<Cld_FCOutput>();
                    parameters = this.session.CreateQuery("from Cld_FCParameter as p where p.Prj_Sheet_ID = " + sheetid).List<Cld_FCParameter>();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }

            //List to Hahstable
            foreach (Cld_FCBlock b in blocks)
            {
                b.State = objstate.Loaded;
                table[b.ID] = b;
                b.Cld_FCInput_List = new MyList(b);
                b.Cld_FCOutput_List = new MyList(b);
                b.Cld_FCParameter_List = new MyList(b);
            }


            //关联input
            result.Cld_FCInput_List = new MyList(result);
            foreach (Cld_FCInput input in inputs)
            {
                input.State = objstate.Loaded;
                //关联到Block
                if (table.ContainsKey(input.Cld_FCBlock_ID))
                {
                    Cld_FCBlock block = (Cld_FCBlock)table[input.Cld_FCBlock_ID];
                    block.Cld_FCInput_List.Add(input);
                    input.Cld_FCBlock = block;
                }
                else
                {
                    continue;
                }
                //关联到sheet
                result.Cld_FCInput_List.Add(input);
                input.Prj_Sheet = result;
            }
            //关联output
            result.Cld_FCOutput_List = new MyList(result);
            foreach (Cld_FCOutput output in outputs)
            {
                output.State = objstate.Loaded;
                //关联到Block
                if (table.ContainsKey(output.Cld_FCBlock_ID))
                {
                    Cld_FCBlock block = (Cld_FCBlock)table[output.Cld_FCBlock_ID];
                    block.Cld_FCOutput_List.Add(output);
                    output.Cld_FCBlock = block;
                }
                else
                {
                    continue;
                }
                //关联到sheet
                result.Cld_FCOutput_List.Add(output);
                output.Prj_Sheet = result;
            }

            //关联parameter
            result.Cld_FCParameter_List = new MyList(result);
            foreach (Cld_FCParameter p in parameters)
            {
                p.State = objstate.Loaded;
                //关联到Block
                if (table.ContainsKey(p.Cld_FCBlock_ID))
                {
                    Cld_FCBlock block = (Cld_FCBlock)table[p.Cld_FCBlock_ID];
                    block.Cld_FCParameter_List.Add(p);
                    p.Cld_FCBlock = block;
                }
                else
                {
                    continue;
                }
                //关联到sheet
                result.Cld_FCParameter_List.Add(p);
                p.Prj_Sheet = result;
            }

            //关联signal
            result.Cld_Signal_List = new MyList(result);
            foreach (Cld_Signal signal in signals)
            {
                signal.State = objstate.Loaded;
                result.Cld_Signal_List.Add(signal);
                signal.Prj_Sheet = result;
            }
            //关联constant
            result.Cld_Constant_List = new MyList(result);
            foreach (Cld_Constant c in consts)
            {
                c.State = objstate.Loaded;
                result.Cld_Constant_List.Add(c);
                c.Prj_Sheet = result;
            }
            //关联graphic
            result.Cld_Graphic_List = new MyList(result);
            foreach (Cld_Graphic g in graphics)
            {
                g.State = objstate.Loaded;
                result.Cld_Graphic_List.Add(g);
                g.Prj_Sheet = result;
            }
            //关联block
            result.Cld_FCBlock_List = new MyList(result);
            foreach (Cld_FCBlock b in blocks)
            {
                b.State = objstate.Loaded;
                result.Cld_FCBlock_List.Add(b);
                b.Prj_Sheet = result;
            }


            Console.WriteLine("Load Prj_Sheet OK!");

            return result;
        }


        /// <summary>
        /// 根据给定的ID加载sheet，以及与其相关的所有后代对象,
        /// 如果不存在，则返回null
        /// 并采用给定的StateManager对对象的状态进行管理
        /// </summary>
        /// <param name="sheetid"></param>
        /// <returns></returns>
        public Prj_Sheet Load_Sheet(int sheetid, StateManager sm)
        {
            Hashtable table = new Hashtable();
            Prj_Sheet result;

            IList<Cld_FCBlock> blocks;
            IList<Cld_Signal> signals;
            IList<Cld_Constant> consts;
            IList<Cld_Graphic> graphics;
            IList<Cld_FCInput> inputs;
            IList<Cld_FCOutput> outputs;
            IList<Cld_FCParameter> parameters;

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                try
                {
                    result = (Prj_Sheet)this.session.Get(typeof(Prj_Sheet), sheetid);
                    //如果没有找到对应的Prj_Sheet,返回null
                    if (result == null)
                    {
                        return null;
                    }
                    result.State = objstate.Loaded;
                    result.State_Manager = sm;
                    sm.Loaded_List.Add(result);
                    blocks = this.session.CreateQuery("from Cld_FCBlock as b where b.Prj_Sheet_ID = " + sheetid).List<Cld_FCBlock>();
                    signals = this.session.CreateQuery("from Cld_Signal as s where s.Prj_Sheet_ID = " + sheetid).List<Cld_Signal>();
                    consts = this.session.CreateQuery("from Cld_Constant as c where c.Prj_Sheet_ID = " + sheetid).List<Cld_Constant>();
                    graphics = this.session.CreateQuery("from Cld_Graphic as g where g.Prj_Sheet_ID = " + sheetid).List<Cld_Graphic>();
                    inputs = this.session.CreateQuery("from Cld_FCInput as i where i.Prj_Sheet_ID = " + sheetid).List<Cld_FCInput>();
                    outputs = this.session.CreateQuery("from Cld_FCOutput as o where o.Prj_Sheet_ID = " + sheetid).List<Cld_FCOutput>();
                    parameters = this.session.CreateQuery("from Cld_FCParameter as p where p.Prj_Sheet_ID = " + sheetid).List<Cld_FCParameter>();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }

            //List to Hahstable
            foreach (Cld_FCBlock b in blocks)
            {
                b.State = objstate.Loaded;
                b.State_Manager = sm;
                table[b.ID] = b;
                b.Cld_FCInput_List = new MyList(b);
                b.Cld_FCOutput_List = new MyList(b);
                b.Cld_FCParameter_List = new MyList(b);
            }


            //关联input
            result.Cld_FCInput_List = new MyList(result);
            foreach (Cld_FCInput input in inputs)
            {
                input.State = objstate.Loaded;
                input.State_Manager = sm;
                sm.Loaded_List.Add(input);
                //关联到Block
                if (table.ContainsKey(input.Cld_FCBlock_ID))
                {
                    Cld_FCBlock block = (Cld_FCBlock)table[input.Cld_FCBlock_ID];
                    block.Cld_FCInput_List.Add(input);
                    input.Cld_FCBlock = block;
                }
                else
                {
                    continue;
                }
                //关联到sheet
                result.Cld_FCInput_List.Add(input);
                input.Prj_Sheet = result;
            }
            //关联output
            result.Cld_FCOutput_List = new MyList(result);
            foreach (Cld_FCOutput output in outputs)
            {
                output.State = objstate.Loaded;
                output.State_Manager = sm;
                sm.Loaded_List.Add(output);
                //关联到Block
                if (table.ContainsKey(output.Cld_FCBlock_ID))
                {
                    Cld_FCBlock block = (Cld_FCBlock)table[output.Cld_FCBlock_ID];
                    block.Cld_FCOutput_List.Add(output);
                    output.Cld_FCBlock = block;
                }
                else
                {
                    continue;
                }
                //关联到sheet
                result.Cld_FCOutput_List.Add(output);
                output.Prj_Sheet = result;
            }

            //关联parameter
            result.Cld_FCParameter_List = new MyList(result);
            foreach (Cld_FCParameter p in parameters)
            {
                p.State = objstate.Loaded;
                p.State_Manager = sm;
                sm.Loaded_List.Add(p);
                //关联到Block
                if (table.ContainsKey(p.Cld_FCBlock_ID))
                {
                    Cld_FCBlock block = (Cld_FCBlock)table[p.Cld_FCBlock_ID];
                    block.Cld_FCParameter_List.Add(p);
                    p.Cld_FCBlock = block;
                }
                else
                {
                    continue;
                }
                //关联到sheet
                result.Cld_FCParameter_List.Add(p);
                p.Prj_Sheet = result;
            }

            //关联signal
            result.Cld_Signal_List = new MyList(result);
            foreach (Cld_Signal signal in signals)
            {
                signal.State = objstate.Loaded;
                signal.State_Manager = sm;
                sm.Loaded_List.Add(signal);
                result.Cld_Signal_List.Add(signal);
                signal.Prj_Sheet = result;
            }
            //关联constant
            result.Cld_Constant_List = new MyList(result);
            foreach (Cld_Constant c in consts)
            {
                c.State = objstate.Loaded;
                c.State_Manager = sm;
                sm.Loaded_List.Add(c);
                result.Cld_Constant_List.Add(c);
                c.Prj_Sheet = result;
            }
            //关联graphic
            result.Cld_Graphic_List = new MyList(result);
            foreach (Cld_Graphic g in graphics)
            {
                g.State = objstate.Loaded;
                g.State_Manager = sm;
                sm.Loaded_List.Add(g);
                result.Cld_Graphic_List.Add(g);
                g.Prj_Sheet = result;
            }
            //关联block
            result.Cld_FCBlock_List = new MyList(result);
            foreach (Cld_FCBlock b in blocks)
            {
                b.State = objstate.Loaded;
                b.State_Manager = sm;
                sm.Loaded_List.Add(b);
                result.Cld_FCBlock_List.Add(b);
                b.Prj_Sheet = result;
            }


            Console.WriteLine("Load Prj_Sheet OK!");

            return result;
        }


        /// <summary>
        /// 获得与给定prj_sheet相关的Cld_Signal
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        public IList<Cld_Signal> Get_Signal_List(IList<Prj_Sheet> sheets)
        {
            //构造HQL查询字符串
            string hql = "from Cld_Signal as s where ";
            foreach (Prj_Sheet sheet in sheets)
            {
                hql = hql + "s.Prj_Sheet_ID = " + sheet.ID + " OR ";
            }
            hql = hql.Trim();
            hql = hql.Substring(0, hql.LastIndexOf("OR"));

            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                IList<Cld_Signal> signals = this.session.CreateQuery(hql).List<Cld_Signal>();
                transaction.Commit();
                return signals;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        /// <summary>
        /// 获得与给定Prj_Sheet相关的CLd_Constant,一条sql语句
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        public IList<Cld_Constant> Get_Constant_List(IList<Prj_Sheet> sheets)
        {
            //构造查询字符换
            string hql = "from Cld_Constant as c where ";
            foreach (Prj_Sheet sheet in sheets)
            {
                hql = hql + "c.Prj_Sheet_ID = " + sheet.ID + " OR ";
            }
            hql = hql.Trim();
            hql = hql.Substring(0, hql.LastIndexOf("OR"));

            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                IList<Cld_Constant> constants = this.session.CreateQuery(hql).List<Cld_Constant>();
                transaction.Commit();
                return constants;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        /// <summary>
        /// 获得与给定Prj_Sheet相关的Cld_FCBlocks，底层一条sql语句
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        public IList<Cld_FCBlock> Get_FCBlock_List(IList<Prj_Sheet> sheets)
        {
            //构造查询字符串
            string hql = "from Cld_FCBlock as b where ";
            foreach (Prj_Sheet sheet in sheets)
            {
                hql = hql + " b.Prj_Sheet_ID = " + sheet.ID + " OR ";
            }
            hql = hql.Trim();
            hql = hql.Substring(0, hql.LastIndexOf("OR"));

            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                IList<Cld_FCBlock> blcoks = this.session.CreateQuery(hql).List<Cld_FCBlock>();
                transaction.Commit();
                return blcoks;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        /// <summary>
        /// 获得与给定prj_sheet相关的cld_fcblock以及所有相关的后代对象
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        public IList<Cld_FCBlock> Load_FCBlock_List(IList<Prj_Sheet> sheets)
        {
            IList<Cld_FCBlock> blocks = Get_FCBlock_List(sheets);
            IList<Cld_FCInput> inputs = (new Cld_FCBlockCRUD(this.session)).Get_FCInput_List(blocks);
            IList<Cld_FCOutput> outputs = (new Cld_FCBlockCRUD(this.session)).Get_FCOutput_List(blocks);
            IList<Cld_FCParameter> parameters = (new Cld_FCBlockCRUD(this.session)).Get_FCParameter_List(blocks);

            Hashtable table = new Hashtable();
            foreach (Cld_FCBlock block in blocks)
            {
                table[block.ID] = block;
                block.Cld_FCInput_List = new MyList(block);
                block.Cld_FCOutput_List = new MyList(block);
                block.Cld_FCParameter_List = new MyList(block);
            }

            //双向关联input
            foreach (Cld_FCInput input in inputs)
            {
                Cld_FCBlock block = (Cld_FCBlock)table[input.Cld_FCBlock_ID];
                block.Cld_FCInput_List.Add(input);
                input.Cld_FCBlock = block;
            }
            //双向关联output
            foreach (Cld_FCOutput output in outputs)
            {
                Cld_FCBlock block = (Cld_FCBlock)table[output.Cld_FCBlock_ID];
                block.Cld_FCOutput_List.Add(output);
                output.Cld_FCBlock = block;
            }
            //双向关联parameter
            foreach (Cld_FCParameter p in parameters)
            {
                Cld_FCBlock block = (Cld_FCBlock)table[p.Cld_FCBlock_ID];
                block.Cld_FCParameter_List.Add(p);
                p.Cld_FCBlock = block;
            }

            return blocks;
        }

        /// <summary>
        /// 或得与给定prj_sheets相关的cld_graphic
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        public IList<Cld_Graphic> Get_Graphic_List(IList<Prj_Sheet> sheets)
        {
            string hql = "from Cld_Graphic as g where ";
            foreach (Prj_Sheet sheet in sheets)
            {
                hql = hql + " g.Prj_Sheet_ID  = " + sheet.ID + " OR ";
            }
            hql = hql.Trim();
            hql = hql.Substring(0, hql.LastIndexOf("OR"));

            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                IList<Cld_Graphic> graphics = this.session.CreateQuery(hql).List<Cld_Graphic>();
                transaction.Commit();
                return graphics;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }
}
