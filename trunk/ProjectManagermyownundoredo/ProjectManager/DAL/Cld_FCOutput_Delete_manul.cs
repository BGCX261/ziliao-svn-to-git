using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCOutputCRUD
    {
        /// <summary>
        /// 从数据库中删除所有的Cld_FCOutput
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll_Cld_FCOutput()
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_FCOutput";
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
        /// 根据特定的条件从数据库中删除所有的Cld_FCOutput
        /// </summary>
        /// <returns></returns>
        public bool DeleteCld_FCOutput_By_ID(int condition)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_FCOutput where ID = " + condition;
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
