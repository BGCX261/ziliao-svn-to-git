using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using TDK.Core.Logic.DAL;

namespace TDK.Core.Logic.BLL
{
    public class PrjManager
    {
        /// <summary>
        /// 表Prj_Project相关的CRUD
        /// </summary>
        public Prj_ProjectCRUD ProjectCRUD;
        /// <summary>
        /// 表Prj_NetWork相关的CRUD
        /// </summary>
        public Prj_NetworkCRUD NetworkCRUD;
        public Prj_UnitCRUD UnitCRUD;
        public Prj_ControllerCRUD ControllerCRUD;
        public Prj_DocumentCRUD DocumentCRUD;
        public Prj_SheetCRUD SheetCRUD;
        public Cld_SignalCRUD SignalCRUD;
        public Cld_ConstantCRUD ConstantCRUD;
        public Cld_FCBlockCRUD BlockCRUD;
        public Cld_GraphicCRUD GraphicCRUD;
        public Cld_FCParameterCRUD ParameterCRUD;
        public Cld_FCOutputCRUD OutputCRUD;
        public Cld_FCInputCRUD InputCRUD;
        public Meta_FCMasterCRUD MetaMasterCRUD;
        public Meta_FCDetailCRUD MetaDetailCRUD;
        public ISession session;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session"></param>
        public PrjManager(ISession session)
        {
            this.session = session;
            this.ProjectCRUD = new Prj_ProjectCRUD(session);
            this.NetworkCRUD = new Prj_NetworkCRUD(session);
            this.UnitCRUD = new Prj_UnitCRUD(session);
            this.ControllerCRUD = new Prj_ControllerCRUD(session);
            this.DocumentCRUD = new Prj_DocumentCRUD(session);
            this.SheetCRUD = new Prj_SheetCRUD(session);
            this.SignalCRUD = new Cld_SignalCRUD(session);
            this.ConstantCRUD = new Cld_ConstantCRUD(session);
            this.BlockCRUD = new Cld_FCBlockCRUD(session);
            this.GraphicCRUD = new Cld_GraphicCRUD(session);
            this.ParameterCRUD = new Cld_FCParameterCRUD(session);
            this.OutputCRUD = new Cld_FCOutputCRUD(session);
            this.InputCRUD = new Cld_FCInputCRUD(session);
            this.MetaMasterCRUD = new Meta_FCMasterCRUD(session);
            this.MetaDetailCRUD = new Meta_FCDetailCRUD(session);
        }

        /************************************************************************/
        /* 操作                                                                  */
        /************************************************************************/

        //删除
        /// <summary>
        /// 从数据库中直接删除所给的数据
        /// </summary>
        /// <param name="objs"></param>
        public void Persistence_Delete(System.Collections.IList objs)
        {
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                foreach (object obj in objs)
                {
                    this.session.Delete(obj);
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
        /// 清空数据库中给定sheet的内容，sheet本身不删除
        /// </summary>
        /// <param name="sheetid"></param>
        public void ClearSheet(int sheetid) {
            ITransaction transaction = this.session.BeginTransaction();
            try
            {
                //signal
                string sql = "delete from Cld_Signal where Prj_Sheet_ID = " + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //const
                sql = "delete from Cld_Const where Prj_Sheet_ID = " + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //graphic
                sql = "delete from Cld_Graphic where Prj_Sheet_ID =" + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //input
                sql = "delete from Cld_FCInput where Prj_Sheet_ID = " + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //output
                sql = "delete from Cld_FCOutput where Prj_Sheet_ID = " + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //para
                sql = "delete from Cld_FCParameter where Prj_Sheet_ID = " + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                //block
                sql = "delete from Cld_FCBlock where Prj_Sheet_ID = " + sheetid;
                this.session.CreateSQLQuery(sql).ExecuteUpdate();
                
                transaction.Commit();
            }
            catch (Exception e) {
                if (transaction != null) {
                    transaction.Rollback();
                }
                throw e;
            }
        }


        /// <summary>
        /// 刷新session的更改到数据库
        /// </summary>
        public void Flush()
        {
            this.session.Flush();
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            this.session.Clear();
        }
        /// <summary>
        /// 关闭同数据库的连接
        /// </summary>
        public void Close()
        {
            this.session.Close();
        }
        /// <summary>
        /// 存储对象，但尚未到数据库中，必须Flush
        /// </summary>
        /// <param name="obj"></param>
        public void Save(object obj)
        {
            this.session.Save(obj);
        }
        /// <summary>
        /// 更新对象，但尚未到数据库中，必须Flush
        /// </summary>
        /// <param name="obj"></param>
        public void Update(object obj)
        {
            this.session.Update(obj);
        }
        /// <summary>
        /// 将对象存储至数据库中
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Exception persistence_save(object obj)
        {
            try
            {
                ITransaction transaction = this.session.BeginTransaction();
                this.session.Save(obj);
                transaction.Commit();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return null;
        }
        /// <summary>
        /// 将对象更新至数据库
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Exception persistence_update(object obj)
        {
            try
            {
                ITransaction transaction = this.session.BeginTransaction();
                this.session.Update(obj);
                transaction.Commit();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return null;
        }

        public Exception sql_persistence_update(object obj)
        {
            try
            {
                return null;
            }
            catch (Exception exp)
            {
                return exp;
            }
        }
        /// <summary>
        /// 将对象存储或者更新到数据库中
        /// </summary>
        /// <param name="obj">待处理的对象</param>
        /// <returns></returns>
        public Exception persistence_saveorupdate(object obj)
        {
            try
            {
                ITransaction transaction = this.session.BeginTransaction();
                this.session.SaveOrUpdate(obj);
                transaction.Commit();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return null;
        }
    }
}
