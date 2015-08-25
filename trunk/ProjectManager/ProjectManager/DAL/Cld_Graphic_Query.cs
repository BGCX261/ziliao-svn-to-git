using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Cld_GraphicCRUD : CRUDBase
	{	
		public Cld_GraphicCRUD(ISession session):base(session){
		
		}
		
		/// <summary>
        /// 获得所有的Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_Graphic> GetCld_Graphics(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_Graphic> result = this.session
						.CreateQuery("select from Cld_Graphic c").List<Cld_Graphic>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Cld_Graphic，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_Graphic> GetCld_Graphics(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_Graphic> result = this.session
						.CreateQuery("select from Cld_Graphic c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Graphic>();
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
		public int CountCld_Graphics(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Cld_Graphic c");
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
        /// 根据给定的条件字符串获得Cld_Graphic
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Cld_Graphic> Get_Cld_Graphic_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_Graphic where " + wherestring;
					IList<Cld_Graphic> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_Graphic)).List<Cld_Graphic>();
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
        /// 根据给定的条件字符串获得Cld_Graphic
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Cld_Graphic> Get_Cld_Graphic_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_Graphic where " + wherestring;
					IList<Cld_Graphic> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_Graphic))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Graphic>();
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
        public int Count_Cld_Graphic_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Cld_Graphic where " + wherestring;
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
		public Cld_Graphic GetCld_Graphic_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Cld_Graphic result = this.session.Get<Cld_Graphic>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据Type获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">图形类型 type:string</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Graphic> result = this.session
					.CreateQuery("select from Cld_Graphic c where c.Type = '" + condition + "'").List<Cld_Graphic>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Type获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Type type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Type(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Graphic> result = this.session
					.CreateQuery("select from Cld_Graphic c where c.Type = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_Graphic>();
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
		public int CountCld_Graphics_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_Graphic c where c.Type = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Layer获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">层名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Layer(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Graphic> result = this.session
					.CreateQuery("select from Cld_Graphic c where c.Layer = '" + condition + "'").List<Cld_Graphic>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Layer获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Layer type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Layer(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Graphic> result = this.session
					.CreateQuery("select from Cld_Graphic c where c.Layer = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_Graphic>();
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
        /// <param name="condition">Layer type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Graphics_By_Layer(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_Graphic c where c.Layer = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Data获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">数据 type:string</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Data(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Graphic> result = this.session
					.CreateQuery("select from Cld_Graphic c where c.Data = '" + condition + "'").List<Cld_Graphic>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Data获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Data type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Data(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_Graphic> result = this.session
					.CreateQuery("select from Cld_Graphic c where c.Data = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_Graphic>();
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
        /// <param name="condition">Data type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_Graphics_By_Data(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_Graphic c where c.Data = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器ID type:int</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Graphic where Prj_Controller_ID = " + condition;
					IList<Cld_Graphic> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Graphic").List<Cld_Graphic>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Graphic where Prj_Controller_ID = " + condition;
					IList<Cld_Graphic> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Graphic")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Graphic>();
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
		public int CountCld_Graphics_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_Graphic where Prj_Controller_ID = " + condition;
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
        /// 根据Prj_Document_ID获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态文档ID type:int</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Graphic where Prj_Document_ID = " + condition;
					IList<Cld_Graphic> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Graphic").List<Cld_Graphic>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Prj_Document_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Graphic where Prj_Document_ID = " + condition;
					IList<Cld_Graphic> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Graphic")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Graphic>();
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
		public int CountCld_Graphics_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_Graphic where Prj_Document_ID = " + condition;
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
        /// 根据Prj_Sheet_ID获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态SheetID type:int</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Graphic where Prj_Sheet_ID = " + condition;
					IList<Cld_Graphic> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Graphic").List<Cld_Graphic>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Sheet_ID获得Cld_Graphic，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Sheet_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_Graphic> GetCld_Graphics_By_Prj_Sheet_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_Graphic where Prj_Sheet_ID = " + condition;
					IList<Cld_Graphic> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_Graphic")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_Graphic>();
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
		public int CountCld_Graphics_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_Graphic where Prj_Sheet_ID = " + condition;
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
