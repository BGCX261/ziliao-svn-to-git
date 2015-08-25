using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Meta_FCDetailCRUD : CRUDBase
	{	
		public Meta_FCDetailCRUD(ISession session):base(session){
		
		}
		
		/// <summary>
        /// 获得所有的Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Meta_FCDetail> result = this.session
						.CreateQuery("select from Meta_FCDetail c").List<Meta_FCDetail>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Meta_FCDetail，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Meta_FCDetail> result = this.session
						.CreateQuery("select from Meta_FCDetail c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Meta_FCDetail>();
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
		public int CountMeta_FCDetails(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Meta_FCDetail c");
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
        /// 根据给定的条件字符串获得Meta_FCDetail
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Meta_FCDetail> Get_Meta_FCDetail_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Meta_FCDetail where " + wherestring;
					IList<Meta_FCDetail> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Meta_FCDetail)).List<Meta_FCDetail>();
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
        /// 根据给定的条件字符串获得Meta_FCDetail
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Meta_FCDetail> Get_Meta_FCDetail_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Meta_FCDetail where " + wherestring;
					IList<Meta_FCDetail> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Meta_FCDetail))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Meta_FCDetail>();
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
        public int Count_Meta_FCDetail_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Meta_FCDetail where " + wherestring;
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
		public Meta_FCDetail GetMeta_FCDetail_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Meta_FCDetail result = this.session.Get<Meta_FCDetail>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据FunctionName获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">功能码名称 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.FunctionName = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FunctionName获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FunctionName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_FunctionName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.FunctionName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
		public int CountMeta_FCDetails_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.FunctionName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PinName获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">管脚（或规格数、IO、Tag）名称 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinName = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinName获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
		public int CountMeta_FCDetails_By_PinName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.PinName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PinIndex获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">顺序 type:int</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinIndex(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinIndex = " + condition ).List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinIndex获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinIndex type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinIndex(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinIndex = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">PinIndex type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_PinIndex(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.PinIndex = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据DataType获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">数据类型 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_DataType(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.DataType = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据DataType获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">DataType type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_DataType(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.DataType = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">DataType type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_DataType(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.DataType = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Tune获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">是否可调 type:bool</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Tune(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Tune = " + condition ).List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Tune获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Tune type:bool</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Tune(bool condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Tune = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">Tune type:bool</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_Tune(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.Tune = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PinType获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">1：Input，2：Constant，3：Output type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinType(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinType = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinType获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinType type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinType(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinType = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">PinType type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_PinType(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.PinType = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据MaxValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">最大值 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_MaxValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.MaxValue = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据MaxValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">MaxValue type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_MaxValue(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.MaxValue = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">MaxValue type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_MaxValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.MaxValue = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据MinValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">最小值 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_MinValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.MinValue = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据MinValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">MinValue type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_MinValue(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.MinValue = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">MinValue type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_MinValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.MinValue = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ValidValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">有效值范围 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_ValidValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.ValidValue = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ValidValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ValidValue type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_ValidValue(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.ValidValue = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">ValidValue type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_ValidValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.ValidValue = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据DefaultValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">默认值 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_DefaultValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.DefaultValue = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据DefaultValue获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">DefaultValue type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_DefaultValue(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.DefaultValue = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">DefaultValue type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_DefaultValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.DefaultValue = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Required获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">是否必要 type:bool</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Required(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Required = " + condition ).List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Required获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Required type:bool</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Required(bool condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Required = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">Required type:bool</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_Required(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.Required = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">描述 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Description = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
		public int CountMeta_FCDetails_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Fixed获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">地址是否绑定，不用分配 type:bool</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Fixed(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Fixed = " + condition ).List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Fixed获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Fixed type:bool</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Fixed(bool condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.Fixed = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">Fixed type:bool</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_Fixed(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.Fixed = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PinSignalType获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">信号类型 type:string</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinSignalType(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinSignalType = '" + condition + "'").List<Meta_FCDetail>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinSignalType获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinSignalType type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_PinSignalType(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Meta_FCDetail> result = this.session
					.CreateQuery("select from Meta_FCDetail c where c.PinSignalType = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Meta_FCDetail>();
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
        /// <param name="condition">PinSignalType type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_PinSignalType(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Meta_FCDetail c where c.PinSignalType = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Meta_FCMaster_ID获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Meta FCMaster ID type:int</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Meta_FCMaster_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Meta_FCDetail where Meta_FCMaster_ID = " + condition;
					IList<Meta_FCDetail> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Meta_FCDetail").List<Meta_FCDetail>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Meta_FCMaster_ID获得Meta_FCDetail，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Meta_FCMaster_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Meta_FCDetail> GetMeta_FCDetails_By_Meta_FCMaster_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Meta_FCDetail where Meta_FCMaster_ID = " + condition;
					IList<Meta_FCDetail> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Meta_FCDetail")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Meta_FCDetail>();
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
        /// <param name="condition">Meta_FCMaster_ID type:int</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountMeta_FCDetails_By_Meta_FCMaster_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Meta_FCDetail where Meta_FCMaster_ID = " + condition;
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
