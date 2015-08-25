using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Cld_FCBlockCRUD
	{
		/// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public Cld_FCBlockCRUD(ISession session) {
            this.session = session;
			Cld_FCBlock.session = session;
        }
		
		
		/// <summary>
        /// 获得所有的Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_FCBlock> result = this.session
						.CreateQuery("select from Cld_FCBlock c").List<Cld_FCBlock>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Cld_FCBlock，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Cld_FCBlock> result = this.session
						.CreateQuery("select from Cld_FCBlock c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Cld_FCBlock c");
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
        /// 根据给定的条件字符串获得Cld_FCBlock
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Cld_FCBlock> Get_Cld_FCBlock_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_FCBlock where " + wherestring;
					IList<Cld_FCBlock> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_FCBlock)).List<Cld_FCBlock>();
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
        /// 根据给定的条件字符串获得Cld_FCBlock
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Cld_FCBlock> Get_Cld_FCBlock_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Cld_FCBlock where " + wherestring;
					IList<Cld_FCBlock> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Cld_FCBlock))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCBlock>();
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
        public int Count_Cld_FCBlock_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Cld_FCBlock where " + wherestring;
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
		public Cld_FCBlock GetCld_FCBlock_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Cld_FCBlock result = this.session.Get<Cld_FCBlock>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据AlgName获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">算法块名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_AlgName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.AlgName = '" + condition + "'").List<Cld_FCBlock>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据AlgName获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">AlgName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_AlgName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.AlgName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCBlock>();
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
        /// <param name="condition">AlgName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCBlocks_By_AlgName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCBlock c where c.AlgName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Sequence获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">算法执行顺序 type:int</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Sequence(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.Sequence = " + condition ).List<Cld_FCBlock>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Sequence获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Sequence type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Sequence(int condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.Sequence = " + condition )
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_Sequence(int condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCBlock c where c.Sequence = " + condition );
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据FunctionName获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">功能码名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.FunctionName = '" + condition + "'").List<Cld_FCBlock>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据FunctionName获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">FunctionName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_FunctionName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.FunctionName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_FunctionName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCBlock c where c.FunctionName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据X_Y获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">坐标 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_X_Y(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.X_Y = '" + condition + "'").List<Cld_FCBlock>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据X_Y获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">X_Y type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_X_Y(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.X_Y = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_X_Y(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCBlock c where c.X_Y = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据SymbolName获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">图形符号名称 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_SymbolName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.SymbolName = '" + condition + "'").List<Cld_FCBlock>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据SymbolName获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">SymbolName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_SymbolName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.SymbolName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCBlock>();
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
        /// <param name="condition">SymbolName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountCld_FCBlocks_By_SymbolName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCBlock c where c.SymbolName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Description获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">描述 type:string</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.Description = '" + condition + "'").List<Cld_FCBlock>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Description获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Description type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Description(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Cld_FCBlock> result = this.session
					.CreateQuery("select from Cld_FCBlock c where c.Description = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_Description(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Cld_FCBlock c where c.Description = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">控制器ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCBlock where Prj_Controller_ID = " + condition;
					IList<Cld_FCBlock> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCBlock").List<Cld_FCBlock>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Controller_ID获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Controller_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Prj_Controller_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCBlock where Prj_Controller_ID = " + condition;
					IList<Cld_FCBlock> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCBlock")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_Prj_Controller_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCBlock where Prj_Controller_ID = " + condition;
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
        /// 根据Prj_Document_ID获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态文档ID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCBlock where Prj_Document_ID = " + condition;
					IList<Cld_FCBlock> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCBlock").List<Cld_FCBlock>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Document_ID获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Document_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Prj_Document_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCBlock where Prj_Document_ID = " + condition;
					IList<Cld_FCBlock> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCBlock")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_Prj_Document_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCBlock where Prj_Document_ID = " + condition;
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
        /// 根据Prj_Sheet_ID获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">组态SheetID type:int</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCBlock where Prj_Sheet_ID = " + condition;
					IList<Cld_FCBlock> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCBlock").List<Cld_FCBlock>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		
		/// <summary>
        /// 根据Prj_Sheet_ID获得Cld_FCBlock，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Prj_Sheet_ID type:int</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Cld_FCBlock> GetCld_FCBlocks_By_Prj_Sheet_ID(int condition,int pagesize, int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select * from Cld_FCBlock where Prj_Sheet_ID = " + condition;
					IList<Cld_FCBlock> result = session.CreateSQLQuery(sql).AddEntity("TDK.Core.Logic.DAL.Cld_FCBlock")
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Cld_FCBlock>();
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
		public int CountCld_FCBlocks_By_Prj_Sheet_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					string sql="select count(*) as C from Cld_FCBlock where Prj_Sheet_ID = " + condition;
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
