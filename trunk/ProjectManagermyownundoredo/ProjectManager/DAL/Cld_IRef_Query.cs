using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Cld_IRefCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Cld_IRefCRUD(ISession session) {
            this.session = session;
			Cld_IRef.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_IRef> GetCld_IRefs(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_IRef> result = this.session
						.CreateQuery("select from Cld_IRef c").List<Cld_IRef>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Cld_IRef，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_IRef> GetCld_IRefs(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_IRef> result = this.session
						.CreateQuery("select from Cld_IRef c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_IRef>();
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
		public int CountCld_IRefs(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Cld_IRef c");
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
        /// 根据给定的条件字符串获得Cld_IRef
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Cld_IRef> Get_Cld_IRef_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_IRef where " + wherestring;
					IList<Cld_IRef> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_IRef)).List<Cld_IRef>();
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
        /// 根据给定的条件字符串获得Cld_IRef
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Cld_IRef> Get_Cld_IRef_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_IRef where " + wherestring;
					IList<Cld_IRef> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_IRef))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_IRef>();
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
        public int Count_Cld_IRef_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Cld_IRef where " + wherestring;
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
        /// 根据ObjectID获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_ObjectID(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.ObjectID = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ObjectID获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ObjectID type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_ObjectID(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.ObjectID = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">ObjectID type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_ObjectID(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.ObjectID = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ControllerAddress获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_ControllerAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.ControllerAddress = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ControllerAddress获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ControllerAddress type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_ControllerAddress(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.ControllerAddress = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
		public int CountCld_IRefs_By_ControllerAddress(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.ControllerAddress = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据DocumentName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_DocumentName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.DocumentName = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据DocumentName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">DocumentName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_DocumentName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.DocumentName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">DocumentName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_DocumentName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.DocumentName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据SheetName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_SheetName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.SheetName = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据SheetName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">SheetName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_SheetName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.SheetName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
		public int CountCld_IRefs_By_SheetName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.SheetName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据RefName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">交叉引用名字 type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_RefName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.RefName = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据RefName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">RefName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_RefName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.RefName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">RefName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_RefName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.RefName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据SrcRefID获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">源交叉引用的ID type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_SrcRefID(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.SrcRefID = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据SrcRefID获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">SrcRefID type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_SrcRefID(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.SrcRefID = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">SrcRefID type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_SrcRefID(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.SrcRefID = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据FunctionCode获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">如果不是IO则为空 type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_FunctionCode(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.FunctionCode = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FunctionCode获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FunctionCode type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_FunctionCode(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.FunctionCode = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">FunctionCode type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_FunctionCode(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.FunctionCode = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据FunctionName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FC type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.FunctionName = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FunctionName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FunctionName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_FunctionName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.FunctionName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">FunctionName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.FunctionName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PinName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">如果是IO Connector则是与该交叉引用连接的引脚名字 type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_PinName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.PinName = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinName获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_PinName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.PinName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">PinName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_PinName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.PinName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Address获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">如果是IO Connector该引脚的地址 type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_Address(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.Address = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Address获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Address type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_Address(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.Address = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">Address type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_Address(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.Address = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据X_Y获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">位置 type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_X_Y(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.X_Y = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据X_Y获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">X_Y type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_X_Y(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.X_Y = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
		public int CountCld_IRefs_By_X_Y(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.X_Y = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据NetworkID获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">网络标号 type:string</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_NetworkID(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.NetworkID = '" + condition + "'").List<Cld_IRef>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据NetworkID获得Cld_IRef，返回一个对象的IList
        /// </summary>
        /// <param name="condition">NetworkID type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_IRef> GetCld_IRefs_By_NetworkID(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_IRef> result = this.session
					.CreateQuery("select from Cld_IRef c where c.NetworkID = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_IRef>();
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
        /// <param name="condition">NetworkID type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_IRefs_By_NetworkID(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_IRef c where c.NetworkID = '" + condition + "'");
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
