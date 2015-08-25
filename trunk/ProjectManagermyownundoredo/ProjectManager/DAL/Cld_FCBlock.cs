using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using TDK.Core.Logic.URdoLib;

namespace TDK.Core.Logic.DAL
{
	/// <summary>
	///	
	/// </summary>
	public partial class Cld_FCBlock　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private int m_ID;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_FCInput_List;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_FCOutput_List;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_FCParameter_List;
		/// <summary>
		/// 算法块名称
		/// </summary>
		private string m_AlgName; 
		/// <summary>
		/// 算法执行顺序
		/// </summary>
		private int m_Sequence; 
		/// <summary>
		/// 功能码名称
		/// </summary>
		private string m_FunctionName; 
		/// <summary>
		/// 坐标
		/// </summary>
		private string m_X_Y; 
		/// <summary>
		/// 图形符号名称
		/// </summary>
		private string m_SymbolName; 
		/// <summary>
		/// 描述
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 控制器ID
		/// </summary>
		private Prj_Controller m_Prj_Controller;
		private int m_Prj_Controller_ID; 
		/// <summary>
		/// 组态文档ID
		/// </summary>
		private Prj_Document m_Prj_Document;
		private int m_Prj_Document_ID; 
		/// <summary>
		/// 组态SheetID
		/// </summary>
		private Prj_Sheet m_Prj_Sheet;
		private int m_Prj_Sheet_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Cld_FCBlock():base(){
			m_ID = -1;
			m_Cld_FCInput_List = null;
			m_Cld_FCOutput_List = null;
			m_Cld_FCParameter_List = null;
			m_AlgName = String.Empty;
			m_Sequence = -1;
			m_FunctionName = String.Empty;
			m_X_Y = String.Empty;
			m_SymbolName = String.Empty;
			m_Description = String.Empty;
			m_Prj_Controller_ID = -1;
			m_Prj_Controller = null;
			m_Prj_Document_ID = -1;
			m_Prj_Document = null;
			m_Prj_Sheet_ID = -1;
			m_Prj_Sheet = null;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCInput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCInput_List == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							IList temp = Cld_FCBlock.session
									.CreateQuery("from Cld_FCInput as i where i.Cld_FCBlock_ID = " + this.ID)
									.List();
							this.m_Cld_FCInput_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_FCInput_List;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
					Reload=false;
                }
                return m_Cld_FCInput_List;
            }
			set 
			{ 
				if(this.m_Cld_FCInput_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_FCInput_List = value; 
			}
		}
		/// <summary>
		/// ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCOutput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCOutput_List == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							IList temp = Cld_FCBlock.session
									.CreateQuery("from Cld_FCOutput as i where i.Cld_FCBlock_ID = " + this.ID)
									.List();
							this.m_Cld_FCOutput_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_FCOutput_List;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
					Reload=false;
                }
                return m_Cld_FCOutput_List;
            }
			set 
			{ 
				if(this.m_Cld_FCOutput_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_FCOutput_List = value; 
			}
		}
		/// <summary>
		/// ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCParameter_List
		{
			get
            {
                if (Reload || this.m_Cld_FCParameter_List == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							IList temp = Cld_FCBlock.session
									.CreateQuery("from Cld_FCParameter as i where i.Cld_FCBlock_ID = " + this.ID)
									.List();
							this.m_Cld_FCParameter_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_FCParameter_List;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
					Reload=false;
                }
                return m_Cld_FCParameter_List;
            }
			set 
			{ 
				if(this.m_Cld_FCParameter_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_FCParameter_List = value; 
			}
		}
		/// <summary>
		/// 算法块名称
		/// </summary>		
		public virtual string AlgName
		{
			get { return m_AlgName; }
			set	
			{
				if ( value != null)
				if( value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for AlgName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("AlgName", m_AlgName);
				switch(this.State){
					case objstate.Loaded:
						if(m_AlgName != value){
							this.State = objstate.Modified;
							this.IsModified = true;
							this.State_Manager.Loaded_List.Remove(this);
                            this.State_Manager.Modified_List.Add(this);
						}
						break;
					case objstate.Newed:
						break;
					case objstate.Deleted:
						break;
					case objstate.Modified:
						break;
					case objstate.NAS:
						break;
				}
				m_AlgName = value;
			}

		}		
		/// <summary>
		/// 算法执行顺序
		/// </summary>		
		public virtual int Sequence
		{
			get { return m_Sequence; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Sequence != value){
							this.State = objstate.Modified;
							this.IsModified = true;
							this.State_Manager.Loaded_List.Remove(this);
                            this.State_Manager.Modified_List.Add(this);
						}
						break;
					case objstate.Newed:
						break;
					case objstate.Deleted:
						break;
					case objstate.Modified:
						break;
					case objstate.NAS:
						break;
				}
				m_Sequence = value; 
			}

		}		
		/// <summary>
		/// 功能码名称
		/// </summary>		
		public virtual string FunctionName
		{
			get { return m_FunctionName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for FunctionName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("FunctionName", m_FunctionName);
				switch(this.State){
					case objstate.Loaded:
						if(m_FunctionName != value){
							this.State = objstate.Modified;
							this.IsModified = true;
							this.State_Manager.Loaded_List.Remove(this);
                            this.State_Manager.Modified_List.Add(this);
						}
						break;
					case objstate.Newed:
						break;
					case objstate.Deleted:
						break;
					case objstate.Modified:
						break;
					case objstate.NAS:
						break;
				}
				m_FunctionName = value;
			}

		}		
		/// <summary>
		/// 坐标
		/// </summary>		
		public virtual string X_Y
		{
			get { return m_X_Y; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for X_Y", value, value.ToString());
				}
				//to store the undo info
                AddHistory("X_Y", m_X_Y);
				switch(this.State){
					case objstate.Loaded:
						if(m_X_Y != value){
							this.State = objstate.Modified;
							this.IsModified = true;
							this.State_Manager.Loaded_List.Remove(this);
                            this.State_Manager.Modified_List.Add(this);
						}
						break;
					case objstate.Newed:
						break;
					case objstate.Deleted:
						break;
					case objstate.Modified:
						break;
					case objstate.NAS:
						break;
				}
				m_X_Y = value;
			}

		}		
		/// <summary>
		/// 图形符号名称
		/// </summary>		
		public virtual string SymbolName
		{
			get { return m_SymbolName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SymbolName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("SymbolName", m_SymbolName);
				switch(this.State){
					case objstate.Loaded:
						if(m_SymbolName != value){
							this.State = objstate.Modified;
							this.IsModified = true;
							this.State_Manager.Loaded_List.Remove(this);
                            this.State_Manager.Modified_List.Add(this);
						}
						break;
					case objstate.Newed:
						break;
					case objstate.Deleted:
						break;
					case objstate.Modified:
						break;
					case objstate.NAS:
						break;
				}
				m_SymbolName = value;
			}

		}		
		/// <summary>
		/// 描述
		/// </summary>		
		public virtual string Description
		{
			get { return m_Description; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Description", m_Description);
				switch(this.State){
					case objstate.Loaded:
						if(m_Description != value){
							this.State = objstate.Modified;
							this.IsModified = true;
							this.State_Manager.Loaded_List.Remove(this);
                            this.State_Manager.Modified_List.Add(this);
						}
						break;
					case objstate.Newed:
						break;
					case objstate.Deleted:
						break;
					case objstate.Modified:
						break;
					case objstate.NAS:
						break;
				}
				m_Description = value;
			}

		}		
				
		/// <summary>
		/// 控制器ID
		/// </summary>
		public virtual int Prj_Controller_ID
		{
			get { return m_Prj_Controller_ID ;}
			set 
			{ 				
				m_Prj_Controller_ID = value; 
			}
		}
		/// <summary>
		/// 控制器ID
		/// </summary>
		public virtual Prj_Controller Prj_Controller
		{
			get
            {
                if (Reload || this.m_Prj_Controller == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							this.m_Prj_Controller = 
								Cld_FCBlock.session.Get<Prj_Controller>(this.Prj_Controller_ID);
							transaction.Commit();
							return this.m_Prj_Controller;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
					Reload=false;
                }
                return m_Prj_Controller;
            }
			set { m_Prj_Controller = value; }
		}
				
		/// <summary>
		/// 组态文档ID
		/// </summary>
		public virtual int Prj_Document_ID
		{
			get { return m_Prj_Document_ID ;}
			set 
			{ 				
				m_Prj_Document_ID = value; 
			}
		}
		/// <summary>
		/// 组态文档ID
		/// </summary>
		public virtual Prj_Document Prj_Document
		{
			get
            {
                if (Reload || this.m_Prj_Document == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							this.m_Prj_Document = 
								Cld_FCBlock.session.Get<Prj_Document>(this.Prj_Document_ID);
							transaction.Commit();
							return this.m_Prj_Document;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
					Reload=false;
                }
                return m_Prj_Document;
            }
			set { m_Prj_Document = value; }
		}
				
		/// <summary>
		/// 组态SheetID
		/// </summary>
		public virtual int Prj_Sheet_ID
		{
			get { return m_Prj_Sheet_ID ;}
			set 
			{ 				
				m_Prj_Sheet_ID = value; 
			}
		}
		/// <summary>
		/// 组态SheetID
		/// </summary>
		public virtual Prj_Sheet Prj_Sheet
		{
			get
            {
                if (Reload || this.m_Prj_Sheet == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							this.m_Prj_Sheet = 
								Cld_FCBlock.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
							transaction.Commit();
							return this.m_Prj_Sheet;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
					Reload=false;
                }
                return m_Prj_Sheet;
            }
			set { m_Prj_Sheet = value; }
		}
	

	}
}

