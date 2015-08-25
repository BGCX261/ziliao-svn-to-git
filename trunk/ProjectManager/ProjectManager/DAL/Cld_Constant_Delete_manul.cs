using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_ConstantCRUD
    {
        /// <summary>
        /// 从数据库中删除所有的Cld_Constant
        /// </summary>
        /// <returns></returns>
        public void DeleteAll_Cld_Constant()
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_Constant";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        /// <summary>
        /// 根据特定的条件从数据库中删除所有的Cld_Constant
        /// </summary>
        /// <returns></returns>
        public void DeleteCld_Constant_By_ID(int condition)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_Constant where ID =" + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
        /// <summary>
        /// 根据给定的id List删除相关的Cld_Constant
        /// </summary>
        /// <param name="id_list"></param>
        public void DeleteCld_Constant(List<int> id_list)
        {
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string sql = "delete from Cld_Constant where ";
                for (int i = 0; i < id_list.Count; i++)
                {
                    if (i != id_list.Count - 1)
                    {
                        sql = sql + " ID = " + id_list[i] + " OR ";
                    }
                    else
                    {
                        sql = sql + " ID = " + id_list[i];
                    }
                    this.session.CreateSQLQuery(sql).ExecuteUpdate();
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        /// <summary>
        /// 根据给定的where 条件删除相关的Cld_Constant
        /// </summary>
        /// <param name="wherestring"></param>
        public void DeleteCld_Constant(string wherestring)
        {
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                string sql = "delete from Cld_Constant where " + wherestring;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }
}
