using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Cld_FCOutputCRUD : CRUDBase
	{	
		public Cld_FCOutputCRUD(ISession session):base(session){
		
		}
		
		/// <summary>
        /// 获得所有的Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_FCOutput> result = this.session
						.CreateQuery("select from Cld_FCOutput c").List<Cld_FCOutput>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Cld_FCOutput，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_FCOutput> result = this.session
						.CreateQuery("select from Cld_FCOutput c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Cld_FCOutput c");
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
        /// 根据给定的条件字符串获得Cld_FCOutput
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Cld_FCOutput> Get_Cld_FCOutput_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_FCOutput where " + wherestring;
					IList<Cld_FCOutput> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_FCOutput)).List<Cld_FCOutput>();
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
        /// 根据给定的条件字符串获得Cld_FCOutput
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Cld_FCOutput> Get_Cld_FCOutput_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_FCOutput where " + wherestring;
					IList<Cld_FCOutput> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_FCOutput))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCOutput>();
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
        public int Count_Cld_FCOutput_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Cld_FCOutput where " + wherestring;
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
		public Cld_FCOutput GetCld_FCOutput_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Cld_FCOutput result = this.session.Get<Cld_FCOutput>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据PinName获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">管脚（或规格数、IO、Tag）名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_PinName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.PinName = '" + condition + "'").List<Cld_FCOutput>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinName获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_PinName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.PinName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs_By_PinName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCOutput c where c.PinName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PointName获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">管脚（或规格数、IO、Tag）值 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_PointName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.PointName = '" + condition + "'").List<Cld_FCOutput>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PointName获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PointName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_PointName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.PointName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCOutput>();
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
        /// <param name="condition">PointName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCOutputs_By_PointName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCOutput c where c.PointName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据InitialValue获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">初始值 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_InitialValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.InitialValue = '" + condition + "'").List<Cld_FCOutput>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据InitialValue获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">InitialValue type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_InitialValue(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.InitialValue = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCOutput>();
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
        /// <param name="condition">InitialValue type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCOutputs_By_InitialValue(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCOutput c where c.InitialValue = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Point获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">管脚坐标 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Point(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.Point = '" + condition + "'").List<Cld_FCOutput>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Point获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Point type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Point(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.Point = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCOutput>();
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
        /// <param name="condition">Point type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCOutputs_By_Point(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCOutput c where c.Point = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Visible获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">是否可见 type:bool</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Visible(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.Visible = " + condition ).List<Cld_FCOutput>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Visible获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Visible type:bool</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Visible(bool condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.Visible = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCOutput>();
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
        /// <param name="condition">Visible type:bool</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCOutputs_By_Visible(bool condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCOutput c where c.Visible = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">注释 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.Description = '" + condition + "'").List<Cld_FCOutput>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCOutput> result = this.session
					.CreateQuery("select from Cld_FCOutput c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCOutput c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Cld_FCBlock_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">块的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Cld_FCBlock_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Cld_FCBlock_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput").List<Cld_FCOutput>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Cld_FCBlock_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Cld_FCBlock_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Cld_FCBlock_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Cld_FCBlock_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs_By_Cld_FCBlock_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCOutput where Cld_FCBlock_ID = " + condition;
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
        /// 根据Prj_Sheet_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sheet的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Prj_Sheet_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput").List<Cld_FCOutput>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Sheet_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Sheet_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Prj_Sheet_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Prj_Sheet_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCOutput where Prj_Sheet_ID = " + condition;
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
        /// 根据Prj_Document_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Document的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Prj_Document_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput").List<Cld_FCOutput>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Prj_Document_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Prj_Document_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCOutput where Prj_Document_ID = " + condition;
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
        /// 根据Prj_Controller_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Controller的ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Prj_Controller_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput").List<Cld_FCOutput>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_FCOutput，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCOutput> GetCld_FCOutputs_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCOutput where Prj_Controller_ID = " + condition;
					IList<Cld_FCOutput> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCOutput")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCOutput>();
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
		public int CountCld_FCOutputs_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCOutput where Prj_Controller_ID = " + condition;
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
