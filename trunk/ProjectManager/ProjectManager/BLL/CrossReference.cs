using System;
using System.Collections.Generic;
using System.Text;
using TDK.Core.Logic.DAL;
using NHibernate;

namespace TDK.Core.Logic.BLL
{
    public static class CrossReference
    {
        /// <summary>
        /// 获取 XPgAI/XPgDI 的输入引用块
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public static Cld_FCBlock GetInputReference(Cld_FCBlock block)
        {
            string dpuNum = block.AlgName.Substring(0, block.AlgName.IndexOf('-'));
            string pageNum = "";
            string blockNum = "";

            foreach (Cld_FCParameter para in block.Cld_FCParameter_List)
            {
                if (para.Name == "Page")
                {
                    pageNum = para.PValue;
                }
                else if (para.Name == "Block")
                {
                    blockNum = para.PValue;
                }
            }

            string sql = "SELECT * FROM Cld_FCBlock WHERE AlgName='" + dpuNum + "-" + pageNum + "-" + blockNum + "'";
            IList<Cld_FCBlock> blocks;
            try
            {
                blocks = GraphicsDocument.session.CreateSQLQuery(sql).AddEntity("ProjectManager.DAL.Cld_FCBlock").List<Cld_FCBlock>();
            }
            catch (Exception e)
            {
                throw e;
            }

            if (blocks.Count > 0)
            {
                return blocks[0];
            }

            return null;
        }

        /// <summary>
        /// 获取指定 Block 的输出引用块列表
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public static IList<Cld_FCBlock> GetOutputReference(Cld_FCBlock block)
        {
            string[] Numbers = block.AlgName.Split('-');
            string dpuNum = Numbers[0];
            string pageNum = Numbers[1];
            string blockNum = Numbers[2];

            string sql = "SELECT * FROM Cld_FCBlock WHERE ID IN ("
                + " SELECT blocks.Cld_FCBlock_ID"
                + " FROM Cld_FCParameter AS pages INNER JOIN Cld_FCParameter AS blocks"
                + " ON pages.Cld_FCBlock_ID = blocks.Cld_FCBlock_ID"
                + " WHERE pages.Prj_Controller_ID=" + block.Prj_Controller_ID
                + " AND blocks.Prj_Controller_ID=" + block.Prj_Controller_ID
                + " AND pages.Name='Page' AND pages.PValue='" + pageNum + "'"
                + " AND blocks.Name='Block' AND blocks.PValue='" + blockNum + "')";

            IList<Cld_FCBlock> blocks = null;
            try
            {
                blocks = GraphicsDocument.session.CreateSQLQuery(sql).AddEntity("ProjectManager.DAL.Cld_FCBlock").List<Cld_FCBlock>();
            }
            catch (Exception e)
            {
                throw e;
            }

            if (blocks != null)
            {
                return blocks;
            }

            return new List<Cld_FCBlock>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public static IList<Cld_FCBlock> GetLoopReference(string gid, int blockID)
        {
            string sql = "SELECT * FROM Cld_FCBlock WHERE ID IN"
                + " (SELECT Cld_FCBlock_ID FROM Cld_FCInput WHERE PinName='GID' AND PointName='" + gid + "')";

            IList<Cld_FCBlock> blocks = null;
            try
            {
                blocks = GraphicsDocument.session.CreateSQLQuery(sql).AddEntity("ProjectManager.DAL.Cld_FCBlock").List<Cld_FCBlock>();
            }
            catch (Exception e)
            {
                throw e;
            }

            if (blocks != null && blocks.Count > 1)
            {
                //int index = -1;
                //for (int i = 0; i < blocks.Count; i++)
                //{
                //    if (blocks[i].FunctionName != "XNetAI" && blocks[i].FunctionName != "XNetDI")
                //    {
                //        if (index < 0)
                //        {
                //            index = i;
                //        }
                //        else
                //        {
                //            throw new Exception("存在两个上环块！");
                //        }
                //    }
                //}

                //if (index > 0)
                //{
                //    Cld_FCBlock block = blocks[index];
                //    blocks.RemoveAt(index);
                //    blocks.Insert(0, block);
                //}

                return blocks;
            }

            return new List<Cld_FCBlock>();
        }
    }
}
