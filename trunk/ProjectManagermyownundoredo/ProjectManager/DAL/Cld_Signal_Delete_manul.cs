using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_SignalCRUD
    {
        /// <summary>
        /// 从数据库中删除所有的Cld_Signal
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll_Cld_Signal()
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_Signal";
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
        /// 根据特定的条件从数据库中删除所有的Cld_Signal
        /// </summary>
        /// <returns></returns>
        public bool DeleteCld_Signal_By_ID(int condition)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Cld_Signal where ID = " + condition;
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
