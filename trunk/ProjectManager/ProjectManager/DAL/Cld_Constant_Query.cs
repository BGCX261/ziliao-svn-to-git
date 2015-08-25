using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Cld_ConstantCRUD : CRUDBase
	{	
		public Cld_ConstantCRUD(ISession session):base(session){
		
		}
		
		/// <summary>
        /// 获得所有的Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_Constant> GetCld_Constants(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_Constant> result = this.session
						.CreateQuery("select from Cld_Constant c").List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Cld_Constant，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_Constant> GetCld_Constants(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_Constant> result = this.session
						.CreateQuery("select from Cld_Constant c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <returns>对象的个数</returns>
		public int CountCld_Constants(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Cld_Constant c");
					IList<object> result = temp.List<object>();
					transaction.Commit();
					return Convert.ToInt32((Int64)result[0]);
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据给定的条件字符串获得Cld_Constant
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Cld_Constant> Get_Cld_Constant_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_Constant where " + wherestring;
					IList<Cld_Constant> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_Constant)).List<Cld_Constant>();
					transaction.Commit();
					return temps;
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw e;
				} 
			}
        }
		
		/// <summary>
        /// 根据给定的条件字符串获得Cld_Constant
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Cld_Constant> Get_Cld_Constant_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_Constant where " + wherestring;
					IList<Cld_Constant> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_Constant))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Constant>();
					transaction.Commit();
					return temps;
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw e;
				}
			}
        }
		
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public int Count_Cld_Constant_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Cld_Constant where " + wherestring;
					ISQLQuery query = session.CreateSQLQuery(sql).AddScalar("C", NHibernateUtil.Int32);
					int result = Convert.ToInt32(query.UniqueResult());
					transaction.Commit();
					return result;
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw e;
				}
			}
        }
		
		/// <summary>
        /// 根据ID获得Cld_FCBlock
        /// </summary>
        /// <param name="condition">ID type:int</param>
        /// <returns></returns>
		public Cld_Constant GetCld_Constant_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Cld_Constant result = this.session.Get<Cld_Constant>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据Name获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">常数块名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Name(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Constant> result = this.session
					.CreateQuery("select from Cld_Constant c where c.Name = '" + condition + "'").List<Cld_Constant>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Name获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Name type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Name(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Constant> result = this.session
					.CreateQuery("select from Cld_Constant c where c.Name = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_Constant>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <param name="condition">Name type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Constants_By_Name(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_Constant c where c.Name = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据X_Y获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">坐标 type:string</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_X_Y(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Constant> result = this.session
					.CreateQuery("select from Cld_Constant c where c.X_Y = '" + condition + "'").List<Cld_Constant>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据X_Y获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">X_Y type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_X_Y(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Constant> result = this.session
					.CreateQuery("select from Cld_Constant c where c.X_Y = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_Constant>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <param name="condition">X_Y type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Constants_By_X_Y(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_Constant c where c.X_Y = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Sheet_ID获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sheet的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Constant where Prj_Sheet_ID = " + condition;
					IList<Cld_Constant> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Constant").List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Sheet_ID获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Sheet_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Prj_Sheet_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Constant where Prj_Sheet_ID = " + condition;
					IList<Cld_Constant> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Constant")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}	
			}
		}
		
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <param name="condition">Prj_Sheet_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Constants_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_Constant where Prj_Sheet_ID = " + condition;
					ISQLQuery  query=session.CreateSQLQuery(sql).AddScalar("C", NHibernateUtil.Int32);
					int result=Convert.ToInt32(query.UniqueResult());
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Document的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Constant where Prj_Document_ID = " + condition;
					IList<Cld_Constant> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Constant").List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Prj_Document_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Constant where Prj_Document_ID = " + condition;
					IList<Cld_Constant> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Constant")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}	
			}
		}
		
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Constants_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_Constant where Prj_Document_ID = " + condition;
					ISQLQuery  query=session.CreateSQLQuery(sql).AddScalar("C", NHibernateUtil.Int32);
					int result=Convert.ToInt32(query.UniqueResult());
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Controller的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Constant where Prj_Controller_ID = " + condition;
					IList<Cld_Constant> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Constant").List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_Constant，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Constant> GetCld_Constants_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Constant where Prj_Controller_ID = " + condition;
					IList<Cld_Constant> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Constant")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Constant>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}	
			}
		}
		
		/// <summary>
        /// 返回符合条件的对象的个数
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Constants_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_Constant where Prj_Controller_ID = " + condition;
					ISQLQuery  query=session.CreateSQLQuery(sql).AddScalar("C", NHibernateUtil.Int32);
					int result=Convert.ToInt32(query.UniqueResult());
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		
		
	}
}
