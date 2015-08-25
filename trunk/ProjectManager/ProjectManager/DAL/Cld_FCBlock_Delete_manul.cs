using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCBlockCRUD
    {
        /// <summary>
        /// 从数据库中删除所有的Cld_FCBlock
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll_Cld_FCBlock()
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_FCBlock";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCParameter";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCInput";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCOutput";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        /// <summary>
        /// 根据特定的条件从数据库中删除所有的Cld_FCBlock
        /// </summary>
        /// <returns></returns>
        public bool DeleteCld_FCBlock_By_ID(int condition)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_FCBlock where ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCParameter where Cld_FCBlock_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCInput where Cld_FCBlock_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCOutput where Cld_FCBlock_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;//抛出异常
            }
        }
    }
}
