using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Prj_ControllerCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Prj_ControllerCRUD(ISession session) {
            this.session = session;
			Prj_Controller.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Controller> GetPrj_Controllers(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Controller> result = this.session
						.CreateQuery("select from Prj_Controller c").List<Prj_Controller>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Prj_Controller，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Controller> GetPrj_Controllers(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Controller> result = this.session
						.CreateQuery("select from Prj_Controller c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Controller>();
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
		public int CountPrj_Controllers(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Prj_Controller c");
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
        /// 根据给定的条件字符串获得Prj_Controller
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Prj_Controller> Get_Prj_Controller_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Controller where " + wherestring;
					IList<Prj_Controller> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Controller)).List<Prj_Controller>();
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
        /// 根据给定的条件字符串获得Prj_Controller
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Prj_Controller> Get_Prj_Controller_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Controller where " + wherestring;
					IList<Prj_Controller> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Controller))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Controller>();
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
        public int Count_Prj_Controller_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Prj_Controller where " + wherestring;
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
		public Prj_Controller GetPrj_Controller_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Prj_Controller result = this.session.Get<Prj_Controller>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据ControllerAddress获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器地址 type:string</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_ControllerAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.ControllerAddress = '" + condition + "'").List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ControllerAddress获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ControllerAddress type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_ControllerAddress(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.ControllerAddress = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">ControllerAddress type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_ControllerAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.ControllerAddress = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ControllerName获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器名字 type:string</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_ControllerName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.ControllerName = '" + condition + "'").List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ControllerName获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ControllerName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_ControllerName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.ControllerName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">ControllerName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_ControllerName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.ControllerName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据CreateTime获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">创建时间 type:DateTime</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_CreateTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.CreateTime = " + condition ).List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据CreateTime获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">CreateTime type:DateTime</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_CreateTime(DateTime condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.CreateTime = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">CreateTime type:DateTime</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_CreateTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.CreateTime = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ModifyTime获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">修改时间 type:DateTime</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_ModifyTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.ModifyTime = " + condition ).List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ModifyTime获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ModifyTime type:DateTime</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_ModifyTime(DateTime condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.ModifyTime = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">ModifyTime type:DateTime</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_ModifyTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.ModifyTime = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">描述 type:string</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.Description = '" + condition + "'").List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
		public int CountPrj_Controllers_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Version获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">版本 type:string</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Version(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.Version = '" + condition + "'").List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Version获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Version type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Version(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.Version = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">Version type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_Version(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.Version = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据TranslatorResult获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">转换结果 type:string</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_TranslatorResult(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.TranslatorResult = '" + condition + "'").List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据TranslatorResult获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">TranslatorResult type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_TranslatorResult(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.TranslatorResult = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">TranslatorResult type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_TranslatorResult(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.TranslatorResult = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Unit_ID获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">所在的Unit ID type:int</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Prj_Unit_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Controller where Prj_Unit_ID = " + condition;
					IList<Prj_Controller> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Controller").List<Prj_Controller>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Unit_ID获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Unit_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Prj_Unit_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Controller where Prj_Unit_ID = " + condition;
					IList<Prj_Controller> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Controller")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Controller>();
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
        /// <param name="condition">Prj_Unit_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_Prj_Unit_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Prj_Controller where Prj_Unit_ID = " + condition;
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
        /// 根据Sequence获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">显示顺序 type:int</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Sequence(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.Sequence = " + condition ).List<Prj_Controller>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Sequence获得Prj_Controller，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sequence type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Controller> GetPrj_Controllers_By_Sequence(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Controller> result = this.session
					.CreateQuery("select from Prj_Controller c where c.Sequence = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Controller>();
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
        /// <param name="condition">Sequence type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Controllers_By_Sequence(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Controller c where c.Sequence = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		
		
	}
}
