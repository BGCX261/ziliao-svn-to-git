using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Prj_NetworkCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Prj_NetworkCRUD(ISession session) {
            this.session = session;
			Prj_Network.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Prj_Network，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Network> GetPrj_Networks(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Network> result = this.session
						.CreateQuery("select from Prj_Network c").List<Prj_Network>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Prj_Network，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Network> GetPrj_Networks(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Network> result = this.session
						.CreateQuery("select from Prj_Network c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Network>();
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
		public int CountPrj_Networks(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Prj_Network c");
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
        /// 根据给定的条件字符串获得Prj_Network
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Prj_Network> Get_Prj_Network_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Network where " + wherestring;
					IList<Prj_Network> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Network)).List<Prj_Network>();
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
        /// 根据给定的条件字符串获得Prj_Network
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Prj_Network> Get_Prj_Network_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Network where " + wherestring;
					IList<Prj_Network> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Network))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Network>();
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
        public int Count_Prj_Network_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Prj_Network where " + wherestring;
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
		public Prj_Network GetPrj_Network_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Prj_Network result = this.session.Get<Prj_Network>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据NetworkAddress获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器地址 type:string</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_NetworkAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Network> result = this.session
					.CreateQuery("select from Prj_Network c where c.NetworkAddress = '" + condition + "'").List<Prj_Network>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据NetworkAddress获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">NetworkAddress type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_NetworkAddress(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Network> result = this.session
					.CreateQuery("select from Prj_Network c where c.NetworkAddress = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Network>();
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
		public int CountPrj_Networks_By_NetworkAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Network c where c.NetworkAddress = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据NetworkName获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器名字 type:string</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_NetworkName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Network> result = this.session
					.CreateQuery("select from Prj_Network c where c.NetworkName = '" + condition + "'").List<Prj_Network>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据NetworkName获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">NetworkName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_NetworkName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Network> result = this.session
					.CreateQuery("select from Prj_Network c where c.NetworkName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Network>();
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
        /// <param name="condition">NetworkName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Networks_By_NetworkName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Network c where c.NetworkName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器描述 type:string</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Network> result = this.session
					.CreateQuery("select from Prj_Network c where c.Description = '" + condition + "'").List<Prj_Network>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Network> result = this.session
					.CreateQuery("select from Prj_Network c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Network>();
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
		public int CountPrj_Networks_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Network c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Project_ID获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">所在的Project ID type:int</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_Prj_Project_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Network where Prj_Project_ID = " + condition;
					IList<Prj_Network> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Network").List<Prj_Network>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Project_ID获得Prj_Network，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Project_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Network> GetPrj_Networks_By_Prj_Project_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Network where Prj_Project_ID = " + condition;
					IList<Prj_Network> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Network")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Network>();
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
        /// <param name="condition">Prj_Project_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Networks_By_Prj_Project_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Prj_Network where Prj_Project_ID = " + condition;
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
