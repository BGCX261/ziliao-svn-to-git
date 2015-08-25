using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using NHibernate;
using TDK.Core.Logic.URdoLib;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCBlockCRUD
    {
        /// <summary>
        /// 获得给定id的Block，以及与其相关的所有后代对象
        /// </summary>
        /// <param name="blockid"></param>
        /// <returns></returns>
        public Cld_FCBlock Load_Block(int blockid)
        {
            ITransaction transation = this.session.BeginTransaction();
            IList<Cld_FCInput> inputs;
            IList<Cld_FCOutput> outputs;
            IList<Cld_FCParameter> parameters;
            Cld_FCBlock result;
            try
            {
                inputs = this.session.CreateQuery("from Cld_FCInput as i where i.Cld_FCBlock_ID = " + blockid).List<Cld_FCInput>();
                outputs = this.session.CreateQuery("from Cld_FCOutput as o where o.Cld_FCBlock_ID = " + blockid).List<Cld_FCOutput>();
                parameters = this.session.CreateQuery("from Cld_FCParameter as p where p.Cld_FCBlock_ID = " + blockid).List<Cld_FCParameter>();
                //获得唯一的Block
                result = this.session.Get<Cld_FCBlock>(blockid);
                transation.Commit();
            }
            catch (Exception e)
            {
                transation.Rollback();
                throw e;
            }
            //分配空间
            result.Cld_FCInput_List = new MyList(result);
            result.Cld_FCOutput_List = new MyList(result);
            result.Cld_FCParameter_List = new MyList(result);

            //双向关联input
            foreach (Cld_FCInput input in inputs)
            {
                result.Cld_FCInput_List.Add(input);
                input.Cld_FCBlock = result;
            }
            //关联output
            foreach (Cld_FCOutput output in outputs)
            {
                result.Cld_FCOutput_List.Add(output);
                output.Cld_FCBlock = result;
            }
            //关联para
            foreach (Cld_FCParameter p in parameters)
            {
                result.Cld_FCParameter_List.Add(p);
                p.Cld_FCBlock = result;
            }
            return result;
        }

        /// <summary>
        /// 获得给定条件所有的Blocks，以及与其相关的所有后代对象
        /// </summary>
        /// <param name="wherestring"></param>
        /// <returns></returns>
        public IList<Cld_FCBlock> Load_Blocks(string wherestring)
        {
            string sql = "select * from Cld_FCBlock where " + wherestring;
            IList<Cld_FCBlock> blocks;
            ITransaction transacton = this.session.BeginTransaction();
            try
            {
                blocks = this.session.CreateSQLQuery(sql).AddEntity("ProjectManager.DAL.Cld_FCBlock").List<Cld_FCBlock>();
                transacton.Commit();
            }
            catch (Exception e)
            {
                transacton.Rollback();
                throw e;
            }
            
            Hashtable table = new Hashtable();
            IList<Cld_FCInput> inputs = Get_FCInput_List(blocks);
            IList<Cld_FCOutput> outputs = Get_FCOutput_List(blocks);
            IList<Cld_FCParameter> parameters = Get_FCParameter_List(blocks);
            foreach (Cld_FCBlock block in blocks)
            {
                table[block.ID] = block;
                block.Cld_FCInput_List = new MyList(block);
                block.Cld_FCOutput_List = new MyList(block);
                block.Cld_FCParameter_List = new MyList(block);
            }

            //双向关联Input
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
            //双向关联Parameter
            foreach (Cld_FCParameter p in parameters)
            {
                Cld_FCBlock block = (Cld_FCBlock)table[p.Cld_FCBlock_ID];
                block.Cld_FCParameter_List.Add(p);
                p.Cld_FCBlock = block;
            }
            return null;   
        }
        /// <summary>
        /// 获得与给定blocks相关的所有FCInput
        /// </summary>
        /// <param name="cld_fcblock">FCBlock List</param>
        /// <returns></returns>
        public IList<Cld_FCInput> Get_FCInput_List(IList<Cld_FCBlock> cld_fcblocks)
        {
            string sql = "from Cld_FCInput as i where ";
            foreach (Cld_FCBlock block in cld_fcblocks)
            {
                sql = sql + " i.Cld_FCBlock_ID = " + block.ID + " OR ";
            }
            sql = sql.Trim();
            sql = sql.Substring(0, sql.LastIndexOf("OR"));
            ITransaction transacton = this.session.BeginTransaction();
            IList<Cld_FCInput> inputlist;
            try
            {
                inputlist = this.session.CreateQuery(sql).List<Cld_FCInput>();
                transacton.Commit();
            }
            catch (Exception e)
            {
                transacton.Rollback();
                throw e;
            }

            return inputlist;
        }
        /// <summary>
        /// 获得与给定blocks相关的所有FCOutput
        /// </summary>
        /// <param name="cld_fcblocks"></param>
        /// <returns></returns>
        public IList<Cld_FCOutput> Get_FCOutput_List(IList<Cld_FCBlock> cld_fcblocks)
        {
            string sql = "from Cld_FCOutput as o where ";
            foreach (Cld_FCBlock block in cld_fcblocks)
            {
                sql = sql + " o.Cld_FCBlock_ID = " + block.ID + " OR ";
            }
            sql = sql.Trim();
            sql = sql.Substring(0, sql.LastIndexOf("OR"));
            ITransaction transaction = this.session.BeginTransaction();
            IList<Cld_FCOutput> outputs;
            try
            {
                outputs = this.session.CreateQuery(sql).List<Cld_FCOutput>();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }

            return outputs;
        }
        /// <summary>
        /// 获得与给定blocks相关的所有FCParameter
        /// </summary>
        /// <param name="cld_fcblocks"></param>
        /// <returns></returns>
        public IList<Cld_FCParameter> Get_FCParameter_List(IList<Cld_FCBlock> cld_fcblocks)
        {
            string sql = "from Cld_FCParameter as p where p ";
            foreach (Cld_FCBlock block in cld_fcblocks)
            {
                sql = sql + " p.Cld_FCBlock_ID = " + block.ID + " OR ";
            }
            sql = sql.Trim();
            sql = sql.Substring(0, sql.LastIndexOf("OR"));
            ITransaction transacton = this.session.BeginTransaction();
            IList<Cld_FCParameter> parameters;
            try
            {
                parameters = this.session.CreateQuery(sql).List<Cld_FCParameter>();
                transacton.Commit();
            }
            catch (Exception e)
            {
                transacton.Rollback();
                throw e;
            }

            return parameters;
        }
    }
}
