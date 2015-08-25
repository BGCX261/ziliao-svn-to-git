using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Prj_SheetCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Prj_SheetCRUD(ISession session) {
            this.session = session;
			Prj_Sheet.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Sheet> GetPrj_Sheets(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Sheet> result = this.session
						.CreateQuery("select from Prj_Sheet c").List<Prj_Sheet>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Prj_Sheet，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Sheet> GetPrj_Sheets(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Sheet> result = this.session
						.CreateQuery("select from Prj_Sheet c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Sheet>();
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
		public int CountPrj_Sheets(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Prj_Sheet c");
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
        /// 根据给定的条件字符串获得Prj_Sheet
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Prj_Sheet> Get_Prj_Sheet_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Sheet where " + wherestring;
					IList<Prj_Sheet> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Sheet)).List<Prj_Sheet>();
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
        /// 根据给定的条件字符串获得Prj_Sheet
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Prj_Sheet> Get_Prj_Sheet_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Sheet where " + wherestring;
					IList<Prj_Sheet> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Sheet))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Sheet>();
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
        public int Count_Prj_Sheet_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Prj_Sheet where " + wherestring;
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
		public Prj_Sheet GetPrj_Sheet_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Prj_Sheet result = this.session.Get<Prj_Sheet>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据SheetName获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态Sheet名字 type:string</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_SheetName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.SheetName = '" + condition + "'").List<Prj_Sheet>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据SheetName获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">SheetName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_SheetName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.SheetName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Sheet>();
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
        /// <param name="condition">SheetName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Sheets_By_SheetName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Sheet c where c.SheetName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据SheetNum获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态Sheet序号 type:int</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_SheetNum(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.SheetNum = " + condition ).List<Prj_Sheet>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据SheetNum获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">SheetNum type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_SheetNum(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.SheetNum = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Sheet>();
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
        /// <param name="condition">SheetNum type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Sheets_By_SheetNum(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Sheet c where c.SheetNum = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Sequence获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">算法块的执行顺序，以Sheet为单位 type:string</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Sequence(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.Sequence = '" + condition + "'").List<Prj_Sheet>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Sequence获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sequence type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Sequence(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.Sequence = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Sheet>();
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
        /// <param name="condition">Sequence type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Sheets_By_Sequence(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Sheet c where c.Sequence = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Width获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:int</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Width(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.Width = " + condition ).List<Prj_Sheet>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Width获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Width type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Width(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.Width = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Sheet>();
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
        /// <param name="condition">Width type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Sheets_By_Width(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Sheet c where c.Width = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Height获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:int</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Height(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.Height = " + condition ).List<Prj_Sheet>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Height获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Height type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Height(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Sheet> result = this.session
					.CreateQuery("select from Prj_Sheet c where c.Height = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Sheet>();
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
        /// <param name="condition">Height type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Sheets_By_Height(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Sheet c where c.Height = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Controller_ID获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器ID type:int</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Sheet where Prj_Controller_ID = " + condition;
					IList<Prj_Sheet> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Sheet").List<Prj_Sheet>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Sheet where Prj_Controller_ID = " + condition;
					IList<Prj_Sheet> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Sheet")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Sheet>();
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
		public int CountPrj_Sheets_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Prj_Sheet where Prj_Controller_ID = " + condition;
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
        /// 根据Prj_Document_ID获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态文档ID type:int</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Sheet where Prj_Document_ID = " + condition;
					IList<Prj_Sheet> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Sheet").List<Prj_Sheet>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Prj_Sheet，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Sheet> GetPrj_Sheets_By_Prj_Document_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Sheet where Prj_Document_ID = " + condition;
					IList<Prj_Sheet> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Sheet")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Sheet>();
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
		public int CountPrj_Sheets_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Prj_Sheet where Prj_Document_ID = " + condition;
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
