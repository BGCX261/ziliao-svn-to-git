using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Meta_FCMasterCRUD : CRUDBase
	{	
		public Meta_FCMasterCRUD(ISession session):base(session){
		
		}
		
		/// <summary>
        /// 获得所有的Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Meta_FCMaster> result = this.session
						.CreateQuery("select from Meta_FCMaster c").List<Meta_FCMaster>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Meta_FCMaster，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Meta_FCMaster> result = this.session
						.CreateQuery("select from Meta_FCMaster c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Meta_FCMaster>();
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
		public int CountMeta_FCMasters(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Meta_FCMaster c");
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
        /// 根据给定的条件字符串获得Meta_FCMaster
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Meta_FCMaster> Get_Meta_FCMaster_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Meta_FCMaster where " + wherestring;
					IList<Meta_FCMaster> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Meta_FCMaster)).List<Meta_FCMaster>();
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
        /// 根据给定的条件字符串获得Meta_FCMaster
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Meta_FCMaster> Get_Meta_FCMaster_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Meta_FCMaster where " + wherestring;
					IList<Meta_FCMaster> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Meta_FCMaster))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Meta_FCMaster>();
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
        public int Count_Meta_FCMaster_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Meta_FCMaster where " + wherestring;
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
		public Meta_FCMaster GetMeta_FCMaster_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Meta_FCMaster result = this.session.Get<Meta_FCMaster>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据FunctionName获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">功能码名称 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.FunctionName = '" + condition + "'").List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FunctionName获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FunctionName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_FunctionName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.FunctionName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
		public int CountMeta_FCMasters_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.FunctionName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据FunctionCode获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">功能码序号 type:int</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_FunctionCode(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.FunctionCode = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FunctionCode获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FunctionCode type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_FunctionCode(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.FunctionCode = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">FunctionCode type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_FunctionCode(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.FunctionCode = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">描述 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.Description = '" + condition + "'").List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
		public int CountMeta_FCMasters_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Function获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">功能函数 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_Function(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.Function = '" + condition + "'").List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Function获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Function type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_Function(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.Function = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">Function type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_Function(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.Function = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据DIAG获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition"> type:int</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_DIAG(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.DIAG = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据DIAG获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">DIAG type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_DIAG(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.DIAG = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">DIAG type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_DIAG(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.DIAG = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据InputCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">输入管脚个数 type:int</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_InputCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.InputCount = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据InputCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">InputCount type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_InputCount(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.InputCount = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">InputCount type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_InputCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.InputCount = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据SpecCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">规格数个数 type:int</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_SpecCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.SpecCount = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据SpecCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">SpecCount type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_SpecCount(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.SpecCount = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">SpecCount type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_SpecCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.SpecCount = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据OutPutCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">输出管脚个数 type:int</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_OutPutCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.OutPutCount = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据OutPutCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">OutPutCount type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_OutPutCount(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.OutPutCount = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">OutPutCount type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_OutPutCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.OutPutCount = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据InternalCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">内部变量个数 type:int</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_InternalCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.InternalCount = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据InternalCount获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">InternalCount type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_InternalCount(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.InternalCount = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">InternalCount type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_InternalCount(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.InternalCount = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据FCLength获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FC的空间长度 type:short</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_FCLength(short condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.FCLength = " + condition ).List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FCLength获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FCLength type:short</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_FCLength(short condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.FCLength = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">FCLength type:short</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_FCLength(short condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.FCLength = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Type获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Function Code/IO Connector/Constant Block/Cross Reference type:string</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.Type = '" + condition + "'").List<Meta_FCMaster>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Type获得Meta_FCMaster，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Type type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCMaster> GetMeta_FCMasters_By_Type(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCMaster> result = this.session
					.CreateQuery("select from Meta_FCMaster c where c.Type = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCMaster>();
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
        /// <param name="condition">Type type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCMasters_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCMaster c where c.Type = '" + condition + "'");
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
