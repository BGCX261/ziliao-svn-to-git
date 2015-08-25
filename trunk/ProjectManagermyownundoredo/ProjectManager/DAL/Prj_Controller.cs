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
	public partial class Prj_Controller　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// Controller ID
		/// </summary>
		private int m_ID;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_Constant_List;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_FCBlock_List;
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
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_Graphic_List;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Cld_Signal_List;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Prj_Document_List;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Prj_Sheet_List;
		/// <summary>
		/// 控制器地址
		/// </summary>
		private string m_ControllerAddress; 
		/// <summary>
		/// 控制器名字
		/// </summary>
		private string m_ControllerName; 
		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreateTime; 
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime m_ModifyTime; 
		/// <summary>
		/// 描述
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 版本
		/// </summary>
		private string m_Version; 
		/// <summary>
		/// 转换结果
		/// </summary>
		private string m_TranslatorResult; 
		/// <summary>
		/// 所在的Unit ID
		/// </summary>
		private Prj_Unit m_Prj_Unit;
		private int m_Prj_Unit_ID; 
		/// <summary>
		/// 显示顺序
		/// </summary>
		private int m_Sequence; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Controller():base(){
			m_ID = -1;
			m_Cld_Constant_List = null;
			m_Cld_FCBlock_List = null;
			m_Cld_FCInput_List = null;
			m_Cld_FCOutput_List = null;
			m_Cld_FCParameter_List = null;
			m_Cld_Graphic_List = null;
			m_Cld_Signal_List = null;
			m_Prj_Document_List = null;
			m_Prj_Sheet_List = null;
			m_ControllerAddress = String.Empty;
			m_ControllerName = String.Empty;
			m_CreateTime = DateTime.MinValue;
			m_ModifyTime = DateTime.MinValue;
			m_Description = String.Empty;
			m_Version = String.Empty;
			m_TranslatorResult = String.Empty;
			m_Prj_Unit_ID = -1;
			m_Prj_Unit = null;
			m_Sequence = -1;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Controller ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Constant_List
		{
			get
            {
                if (Reload || this.m_Cld_Constant_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_Constant as i where i.Prj_Controller_ID = " + this.ID)
									.List();
							this.m_Cld_Constant_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_Constant_List;
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
                return m_Cld_Constant_List;
            }
			set 
			{ 
				if(this.m_Cld_Constant_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_Constant_List = value; 
			}
		}
		/// <summary>
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCBlock_List
		{
			get
            {
                if (Reload || this.m_Cld_FCBlock_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_FCBlock as i where i.Prj_Controller_ID = " + this.ID)
									.List();
							this.m_Cld_FCBlock_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_FCBlock_List;
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
                return m_Cld_FCBlock_List;
            }
			set 
			{ 
				if(this.m_Cld_FCBlock_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_FCBlock_List = value; 
			}
		}
		/// <summary>
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCInput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCInput_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_FCInput as i where i.Prj_Controller_ID = " + this.ID)
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
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCOutput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCOutput_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_FCOutput as i where i.Prj_Controller_ID = " + this.ID)
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
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCParameter_List
		{
			get
            {
                if (Reload || this.m_Cld_FCParameter_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_FCParameter as i where i.Prj_Controller_ID = " + this.ID)
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
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Graphic_List
		{
			get
            {
                if (Reload || this.m_Cld_Graphic_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_Graphic as i where i.Prj_Controller_ID = " + this.ID)
									.List();
							this.m_Cld_Graphic_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_Graphic_List;
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
                return m_Cld_Graphic_List;
            }
			set 
			{ 
				if(this.m_Cld_Graphic_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_Graphic_List = value; 
			}
		}
		/// <summary>
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Signal_List
		{
			get
            {
                if (Reload || this.m_Cld_Signal_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Cld_Signal as i where i.Prj_Controller_ID = " + this.ID)
									.List();
							this.m_Cld_Signal_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Cld_Signal_List;
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
                return m_Cld_Signal_List;
            }
			set 
			{ 
				if(this.m_Cld_Signal_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Cld_Signal_List = value; 
			}
		}
		/// <summary>
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Prj_Document_List
		{
			get
            {
                if (Reload || this.m_Prj_Document_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Prj_Document as i where i.Prj_Controller_ID = " + this.ID)
									.List();
							this.m_Prj_Document_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Prj_Document_List;
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
                return m_Prj_Document_List;
            }
			set 
			{ 
				if(this.m_Prj_Document_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Prj_Document_List = value; 
			}
		}
		/// <summary>
		/// Controller ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Prj_Sheet_List
		{
			get
            {
                if (Reload || this.m_Prj_Sheet_List == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							IList temp = Prj_Controller.session
									.CreateQuery("from Prj_Sheet as i where i.Prj_Controller_ID = " + this.ID)
									.List();
							this.m_Prj_Sheet_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Prj_Sheet_List;
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
                return m_Prj_Sheet_List;
            }
			set 
			{ 
				if(this.m_Prj_Sheet_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Prj_Sheet_List = value; 
			}
		}
		/// <summary>
		/// 控制器地址
		/// </summary>		
		public virtual string ControllerAddress
		{
			get { return m_ControllerAddress; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ControllerAddress", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ControllerAddress", m_ControllerAddress);
				switch(this.State){
					case objstate.Loaded:
						if(m_ControllerAddress != value){
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
				m_ControllerAddress = value;
			}

		}		
		/// <summary>
		/// 控制器名字
		/// </summary>		
		public virtual string ControllerName
		{
			get { return m_ControllerName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ControllerName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ControllerName", m_ControllerName);
				switch(this.State){
					case objstate.Loaded:
						if(m_ControllerName != value){
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
				m_ControllerName = value;
			}

		}		
		/// <summary>
		/// 创建时间
		/// </summary>		
		public virtual DateTime CreateTime
		{
			get { return m_CreateTime; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_CreateTime != value){
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
				m_CreateTime = value; 
			}

		}		
		/// <summary>
		/// 修改时间
		/// </summary>		
		public virtual DateTime ModifyTime
		{
			get { return m_ModifyTime; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_ModifyTime != value){
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
				m_ModifyTime = value; 
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
				if( value.Length > 100){
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
		/// 版本
		/// </summary>		
		public virtual string Version
		{
			get { return m_Version; }
			set	
			{
				if ( value != null)
				if( value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for Version", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Version", m_Version);
				switch(this.State){
					case objstate.Loaded:
						if(m_Version != value){
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
				m_Version = value;
			}

		}		
		/// <summary>
		/// 转换结果
		/// </summary>		
		public virtual string TranslatorResult
		{
			get { return m_TranslatorResult; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for TranslatorResult", value, value.ToString());
				}
				//to store the undo info
                AddHistory("TranslatorResult", m_TranslatorResult);
				switch(this.State){
					case objstate.Loaded:
						if(m_TranslatorResult != value){
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
				m_TranslatorResult = value;
			}

		}		
				
		/// <summary>
		/// 所在的Unit ID
		/// </summary>
		public virtual int Prj_Unit_ID
		{
			get { return m_Prj_Unit_ID ;}
			set 
			{ 				
				m_Prj_Unit_ID = value; 
			}
		}
		/// <summary>
		/// 所在的Unit ID
		/// </summary>
		public virtual Prj_Unit Prj_Unit
		{
			get
            {
                if (Reload || this.m_Prj_Unit == null)
                {
                    if (Prj_Controller.session != null)
                    {
						ITransaction transaction = Prj_Controller.session.BeginTransaction();
						try{
							this.m_Prj_Unit = 
								Prj_Controller.session.Get<Prj_Unit>(this.Prj_Unit_ID);
							transaction.Commit();
							return this.m_Prj_Unit;
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
                return m_Prj_Unit;
            }
			set { m_Prj_Unit = value; }
		}
		/// <summary>
		/// 显示顺序
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
	

	}
}

