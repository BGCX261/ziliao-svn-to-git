using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Prj_UnitCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Prj_UnitCRUD(ISession session) {
            this.session = session;
			Prj_Unit.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Unit> GetPrj_Units(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Unit> result = this.session
						.CreateQuery("select from Prj_Unit c").List<Prj_Unit>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Prj_Unit，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Unit> GetPrj_Units(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Unit> result = this.session
						.CreateQuery("select from Prj_Unit c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Unit>();
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
		public int CountPrj_Units(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Prj_Unit c");
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
        /// 根据给定的条件字符串获得Prj_Unit
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Prj_Unit> Get_Prj_Unit_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Unit where " + wherestring;
					IList<Prj_Unit> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Unit)).List<Prj_Unit>();
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
        /// 根据给定的条件字符串获得Prj_Unit
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Prj_Unit> Get_Prj_Unit_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Unit where " + wherestring;
					IList<Prj_Unit> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Unit))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Unit>();
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
        public int Count_Prj_Unit_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Prj_Unit where " + wherestring;
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
		public Prj_Unit GetPrj_Unit_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Prj_Unit result = this.session.Get<Prj_Unit>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据UnitAddress获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Unit地址 type:string</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_UnitAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.UnitAddress = '" + condition + "'").List<Prj_Unit>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据UnitAddress获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">UnitAddress type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_UnitAddress(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.UnitAddress = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Unit>();
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
        /// <param name="condition">UnitAddress type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Units_By_UnitAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Unit c where c.UnitAddress = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据UnitName获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Unit名字 type:string</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_UnitName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.UnitName = '" + condition + "'").List<Prj_Unit>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据UnitName获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">UnitName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_UnitName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.UnitName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Unit>();
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
        /// <param name="condition">UnitName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Units_By_UnitName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Unit c where c.UnitName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据NetworkAddress获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">NetworkAdress type:string</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_NetworkAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.NetworkAddress = '" + condition + "'").List<Prj_Unit>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据NetworkAddress获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">NetworkAddress type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_NetworkAddress(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.NetworkAddress = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Unit>();
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
        /// <param name="condition">NetworkAddress type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Units_By_NetworkAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Unit c where c.NetworkAddress = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">描述 type:string</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.Description = '" + condition + "'").List<Prj_Unit>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Unit> result = this.session
					.CreateQuery("select from Prj_Unit c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Unit>();
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
        /// <param name="condition">Description type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Units_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Unit c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Network_ID获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">所在的Network ID type:int</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_Prj_Network_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Unit where Prj_Network_ID = " + condition;
					IList<Prj_Unit> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Unit").List<Prj_Unit>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Network_ID获得Prj_Unit，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Network_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Unit> GetPrj_Units_By_Prj_Network_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Unit where Prj_Network_ID = " + condition;
					IList<Prj_Unit> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Unit")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Unit>();
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
        /// <param name="condition">Prj_Network_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Units_By_Prj_Network_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Prj_Unit where Prj_Network_ID = " + condition;
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
