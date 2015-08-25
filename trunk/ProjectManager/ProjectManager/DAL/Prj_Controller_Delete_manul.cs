using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_ControllerCRUD
    {
        /// <summary>
        /// 从数据库中删除所有的Prj_Controller，以及相关的儿子
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll_Prj_Controller()
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Prj_Controller";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                //如果数据库设计中没有提供级联删除，则由一下代码负责
                sql = "delete  from Cld_FCParameter";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCOutput";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCInput";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Signal";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Constant";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCBlock";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Graphic";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Prj_Sheet";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Prj_Document";
                session.CreateSQLQuery(sql).ExecuteUpdate();


                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
        /// <summary>
        /// 根据特定的条件从数据库中删除所有的Prj_Controller
        /// </summary>
        /// <returns></returns>
        public bool DeletePrj_Controller_By_ID(int condition)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Prj_Controller where ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                //如果数据库设计中没有提供级联删除，则由一下代码负责
                sql = "delete  from Cld_FCParameter where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCOutput where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCInput where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Signal where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Constant where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCBlock where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Graphic where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Prj_Sheet where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Prj_Document where Prj_Controller_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();


                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }
}
