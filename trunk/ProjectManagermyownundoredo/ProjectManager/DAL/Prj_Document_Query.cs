using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Prj_DocumentCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Prj_DocumentCRUD(ISession session) {
            this.session = session;
			Prj_Document.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Prj_Document，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Document> GetPrj_Documents(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Document> result = this.session
						.CreateQuery("select from Prj_Document c").List<Prj_Document>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Prj_Document，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Document> GetPrj_Documents(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Document> result = this.session
						.CreateQuery("select from Prj_Document c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Document>();
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
		public int CountPrj_Documents(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Prj_Document c");
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
        /// 根据给定的条件字符串获得Prj_Document
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Prj_Document> Get_Prj_Document_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Document where " + wherestring;
					IList<Prj_Document> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Document)).List<Prj_Document>();
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
        /// 根据给定的条件字符串获得Prj_Document
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Prj_Document> Get_Prj_Document_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Document where " + wherestring;
					IList<Prj_Document> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Document))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Document>();
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
        public int Count_Prj_Document_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Prj_Document where " + wherestring;
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
		public Prj_Document GetPrj_Document_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Prj_Document result = this.session.Get<Prj_Document>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据DocumentName获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态文档名称 type:string</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_DocumentName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.DocumentName = '" + condition + "'").List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据DocumentName获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">DocumentName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_DocumentName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.DocumentName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
		public int CountPrj_Documents_By_DocumentName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.DocumentName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据DocumentCaption获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">保存文档名称 type:string</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_DocumentCaption(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.DocumentCaption = '" + condition + "'").List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据DocumentCaption获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">DocumentCaption type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_DocumentCaption(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.DocumentCaption = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
        /// <param name="condition">DocumentCaption type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Documents_By_DocumentCaption(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.DocumentCaption = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据CreateTime获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">创建时间 type:DateTime</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_CreateTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.CreateTime = " + condition ).List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据CreateTime获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">CreateTime type:DateTime</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_CreateTime(DateTime condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.CreateTime = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
		public int CountPrj_Documents_By_CreateTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.CreateTime = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ModifyTime获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">修改时间 type:DateTime</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_ModifyTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.ModifyTime = " + condition ).List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ModifyTime获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ModifyTime type:DateTime</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_ModifyTime(DateTime condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.ModifyTime = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
		public int CountPrj_Documents_By_ModifyTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.ModifyTime = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Sequence获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">算法块的执行顺序 type:int</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_Sequence(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.Sequence = " + condition ).List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Sequence获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sequence type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_Sequence(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.Sequence = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
		public int CountPrj_Documents_By_Sequence(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.Sequence = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Type获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">文档类型，用后缀表示，.cld .dwg等 type:string</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.Type = '" + condition + "'").List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Type获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Type type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_Type(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.Type = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
		public int CountPrj_Documents_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.Type = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据TranslatorResult获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">转换结果 type:string</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_TranslatorResult(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.TranslatorResult = '" + condition + "'").List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据TranslatorResult获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">TranslatorResult type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_TranslatorResult(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.TranslatorResult = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
		public int CountPrj_Documents_By_TranslatorResult(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.TranslatorResult = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据changed获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">是否修改 type:string</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_changed(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.changed = '" + condition + "'").List<Prj_Document>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据changed获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">changed type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_changed(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Document> result = this.session
					.CreateQuery("select from Prj_Document c where c.changed = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Document>();
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
        /// <param name="condition">changed type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Documents_By_changed(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Document c where c.changed = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Controller_ID获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器ID type:int</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Document where Prj_Controller_ID = " + condition;
					IList<Prj_Document> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Document").List<Prj_Document>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Prj_Document，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Document> GetPrj_Documents_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Prj_Document where Prj_Controller_ID = " + condition;
					IList<Prj_Document> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Prj_Document")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Document>();
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
		public int CountPrj_Documents_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Prj_Document where Prj_Controller_ID = " + condition;
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
