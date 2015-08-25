using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_SheetCRUD
    {
        /// <summary>
        /// 从数据库中删除所有的Prj_Sheet
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll_Prj_Sheet()
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Prj_Sheet";
                session.CreateSQLQuery(sql).ExecuteUpdate();

                //如果数据库设计中没有提供级联删除，则由以下代码负责
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
        /// 根据特定的条件从数据库中删除所有的Prj_Sheet
        /// </summary>
        /// <returns></returns>
        public bool DeletePrj_Sheet_By_ID(int condition)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                string sql = "delete  from Prj_Sheet where ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                //如果数据库设计中没有提供级联删除，则由一下代码负责
                sql = "delete  from Cld_FCParameter where Prj_Sheet_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCOutput where Prj_Sheet_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCInput where Prj_Sheet_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Signal where Prj_Sheet_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Constant where Prj_Sheet_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_FCBlock where Prj_Sheet_ID = " + condition;
                session.CreateSQLQuery(sql).ExecuteUpdate();

                sql = "delete  from Cld_Graphic where Prj_Sheet_ID = " + condition;
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

        /// <summary>
        /// 根据给定的where条件字符串删除
        /// </summary>
        /// <param name="wherestring"></param>
        /// <returns></returns>
        public bool DeletePrj_Sheet_By_Wherestring(string wherestring){
            ITransaction transaction = this.session.BeginTransaction();
            try{
                //如果数据库设计中有级联删除
                //string sql = "delete from Prj_Sheet where " + wherestring;
                //this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //如果没有级联删除
                string sql = "select * from Prj_Sheet where " + wherestring;
                IList<Prj_Sheet> sheets = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Sheet)).List<Prj_Sheet>();
                foreach(Prj_Sheet sheet in sheets){
                    this.DeletePrj_Sheet_By_ID(sheet.ID);
                }
                return true;
            }catch(Exception ex){
                transaction.Rollback();
                throw ex;//抛出异常
            }                                                           
        }
    }
}
