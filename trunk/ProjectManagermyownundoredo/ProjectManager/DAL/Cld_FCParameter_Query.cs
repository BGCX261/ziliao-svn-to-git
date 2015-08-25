using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Cld_FCParameterCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Cld_FCParameterCRUD(ISession session) {
            this.session = session;
			Cld_FCParameter.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_FCParameter> GetCld_FCParameters(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_FCParameter> result = this.session
						.CreateQuery("select from Cld_FCParameter c").List<Cld_FCParameter>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Cld_FCParameter，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_FCParameter> GetCld_FCParameters(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_FCParameter> result = this.session
						.CreateQuery("select from Cld_FCParameter c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCParameter>();
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
		public int CountCld_FCParameters(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Cld_FCParameter c");
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
        /// 根据给定的条件字符串获得Cld_FCParameter
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Cld_FCParameter> Get_Cld_FCParameter_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_FCParameter where " + wherestring;
					IList<Cld_FCParameter> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_FCParameter)).List<Cld_FCParameter>();
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
        /// 根据给定的条件字符串获得Cld_FCParameter
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Cld_FCParameter> Get_Cld_FCParameter_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_FCParameter where " + wherestring;
					IList<Cld_FCParameter> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_FCParameter))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCParameter>();
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
        public int Count_Cld_FCParameter_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Cld_FCParameter where " + wherestring;
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
		public Cld_FCParameter GetCld_FCParameter_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Cld_FCParameter result = this.session.Get<Cld_FCParameter>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据Name获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">管脚（或规格数、IO、Tag）名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Name(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCParameter> result = this.session
					.CreateQuery("select from Cld_FCParameter c where c.Name = '" + condition + "'").List<Cld_FCParameter>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Name获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Name type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Name(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCParameter> result = this.session
					.CreateQuery("select from Cld_FCParameter c where c.Name = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCParameter>();
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
		public int CountCld_FCParameters_By_Name(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCParameter c where c.Name = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PValue获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">管脚（或规格数、IO、Tag）值 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_PValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCParameter> result = this.session
					.CreateQuery("select from Cld_FCParameter c where c.PValue = '" + condition + "'").List<Cld_FCParameter>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PValue获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PValue type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_PValue(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCParameter> result = this.session
					.CreateQuery("select from Cld_FCParameter c where c.PValue = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCParameter>();
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
        /// <param name="condition">PValue type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCParameters_By_PValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCParameter c where c.PValue = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Cld_FCBlock_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">块的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Cld_FCBlock_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Cld_FCBlock_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter").List<Cld_FCParameter>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Cld_FCBlock_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Cld_FCBlock_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Cld_FCBlock_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Cld_FCBlock_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCParameter>();
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
        /// <param name="condition">Cld_FCBlock_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCParameters_By_Cld_FCBlock_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCParameter where Cld_FCBlock_ID = " + condition;
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
        /// 根据Prj_Sheet_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sheet的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Prj_Sheet_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter").List<Cld_FCParameter>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Sheet_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Sheet_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Prj_Sheet_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Prj_Sheet_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCParameter>();
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
		public int CountCld_FCParameters_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCParameter where Prj_Sheet_ID = " + condition;
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
        /// 根据Prj_Document_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Document的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Prj_Document_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter").List<Cld_FCParameter>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Prj_Document_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Prj_Document_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCParameter>();
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
		public int CountCld_FCParameters_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCParameter where Prj_Document_ID = " + condition;
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
        /// 根据Prj_Controller_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Controller的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Prj_Controller_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter").List<Cld_FCParameter>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_FCParameter，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCParameter> GetCld_FCParameters_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCParameter where Prj_Controller_ID = " + condition;
					IList<Cld_FCParameter> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCParameter")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCParameter>();
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
		public int CountCld_FCParameters_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCParameter where Prj_Controller_ID = " + condition;
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
