using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
	public partial class Prj_ProjectCRUD : CRUDBase
	{	
		public Prj_ProjectCRUD(ISession session):base(session){
		
		}
		
		/// <summary>
        /// 获得所有的Prj_Project，返回一个对象的IList
        /// </summary>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Project> GetPrj_Projects(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Project> result = this.session
						.CreateQuery("select from Prj_Project c").List<Prj_Project>();
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}
		/// <summary>
        /// 获得所有的Prj_Project，返回一个对象的IList
        /// </summary>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns>对象的IList的集合</returns>
		public IList<Prj_Project> GetPrj_Projects(int pagesize,int pageindex){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IList<Prj_Project> result = this.session
						.CreateQuery("select from Prj_Project c").SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Project>();
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
		public int CountPrj_Projects(){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					IQuery temp = session
						.CreateQuery("select count(c) from Prj_Project c");
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
        /// 根据给定的条件字符串获得Prj_Project
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <returns></returns>
        public IList<Prj_Project> Get_Prj_Project_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Project where " + wherestring;
					IList<Prj_Project> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Project)).List<Prj_Project>();
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
        /// 根据给定的条件字符串获得Prj_Project
        /// </summary>
        /// <param name="wherestring">条件字符串，sql语句中 where后边的部分</param>
        /// <param name="pagesize">页的大小</param>
        /// <param name="pageindex">页的Index从0开始</param>
        /// <returns></returns>
        public IList<Prj_Project> Get_Prj_Project_By_Wherestring(string wherestring, int pagesize, int pageindex)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select * from Prj_Project where " + wherestring;
					IList<Prj_Project> temps = this.session.CreateSQLQuery(sql).AddEntity(typeof(Prj_Project))
						.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
						.List<Prj_Project>();
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
        public int Count_Prj_Project_By_Wherestring(string wherestring)
        {
            using(ITransaction transaction = session.BeginTransaction()){
				try
				{
					string sql = "select count(*) as C from Prj_Project where " + wherestring;
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
		public Prj_Project GetPrj_Project_By_ID(int condition){
			using(ITransaction transaction = session.BeginTransaction()){
				try{
					Prj_Project result = this.session.Get<Prj_Project>(condition);
					transaction.Commit();
					return result;
				}catch(Exception e){
					transaction.Rollback();
					throw e;
				}
			}
		}

		/// <summary>
        /// 根据ProjectName获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">项目名称 type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_ProjectName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.ProjectName = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ProjectName获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ProjectName type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_ProjectName(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.ProjectName = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
        /// <param name="condition">ProjectName type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Projects_By_ProjectName(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.ProjectName = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据CreateTime获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">创建时间 type:DateTime</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_CreateTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.CreateTime = " + condition ).List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据CreateTime获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">CreateTime type:DateTime</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_CreateTime(DateTime condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.CreateTime = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
		public int CountPrj_Projects_By_CreateTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.CreateTime = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ModifyTime获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">修改时间 type:DateTime</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_ModifyTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.ModifyTime = " + condition ).List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ModifyTime获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ModifyTime type:DateTime</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_ModifyTime(DateTime condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.ModifyTime = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
		public int CountPrj_Projects_By_ModifyTime(DateTime condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.ModifyTime = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据Type获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">项目类型：ABB，WestWood type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.Type = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据Type获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Type type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_Type(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.Type = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
		public int CountPrj_Projects_By_Type(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.Type = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据BlockMaxNumber获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">项目配置，当前项目分配的Block的个数 type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_BlockMaxNumber(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.BlockMaxNumber = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据BlockMaxNumber获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">BlockMaxNumber type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_BlockMaxNumber(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.BlockMaxNumber = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
        /// <param name="condition">BlockMaxNumber type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Projects_By_BlockMaxNumber(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.BlockMaxNumber = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据PinMaxNumber获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">项目配置，当前项目分配的Pin的个数 type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_PinMaxNumber(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.PinMaxNumber = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据PinMaxNumber获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">PinMaxNumber type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_PinMaxNumber(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.PinMaxNumber = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
        /// <param name="condition">PinMaxNumber type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Projects_By_PinMaxNumber(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.PinMaxNumber = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据ProjectPath获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">项目路径 type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_ProjectPath(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.ProjectPath = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据ProjectPath获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">ProjectPath type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_ProjectPath(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.ProjectPath = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
        /// <param name="condition">ProjectPath type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Projects_By_ProjectPath(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.ProjectPath = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据VarSystemVersion获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">系统点版本 type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_VarSystemVersion(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.VarSystemVersion = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据VarSystemVersion获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">VarSystemVersion type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_VarSystemVersion(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.VarSystemVersion = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
        /// <param name="condition">VarSystemVersion type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Projects_By_VarSystemVersion(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.VarSystemVersion = '" + condition + "'");
				IList<object> result = temp.List<object>();
				transaction.Commit();
				return Convert.ToInt32((Int64)result[0]);
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}

		
		/// <summary>
        /// 根据AlarmVersion获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">Alarm点版本 type:string</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_AlarmVersion(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.AlarmVersion = '" + condition + "'").List<Prj_Project>();
				transaction.Commit();
				return result;
			}catch(Exception e){
				transaction.Rollback();
				throw e;
			}
		}
		
		/// <summary>
        /// 根据AlarmVersion获得Prj_Project，返回一个对象的IList
        /// </summary>
        /// <param name="condition">AlarmVersion type:string</param>
		/// <param name="pagesize">页的大小</param>
		/// <param name="pageindex">页的索引，从0开始</param>
        /// <returns></returns>
		public IList<Prj_Project> GetPrj_Projects_By_AlarmVersion(string condition, int pagesize, int pageindex){
			ITransaction transaction = session.BeginTransaction();
			try{
				IList<Prj_Project> result = this.session
					.CreateQuery("select from Prj_Project c where c.AlarmVersion = '" + condition + "'")
					.SetFirstResult((pageindex) * pagesize).SetMaxResults(pagesize)
					.List<Prj_Project>();
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
        /// <param name="condition">AlarmVersion type:string</param>
        /// <returns>符合条件的对象的个数</returns>
		public int CountPrj_Projects_By_AlarmVersion(string condition){
			ITransaction transaction = session.BeginTransaction();
			try{
				IQuery temp = session
					.CreateQuery("select count(c) from Prj_Project c where c.AlarmVersion = '" + condition + "'");
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
