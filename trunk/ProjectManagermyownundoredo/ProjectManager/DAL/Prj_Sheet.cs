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
	public partial class Prj_Sheet　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// Sheet ID
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
		/// 组态Sheet名字
		/// </summary>
		private string m_SheetName; 
		/// <summary>
		/// 组态Sheet序号
		/// </summary>
		private int m_SheetNum; 
		/// <summary>
		/// 算法块的执行顺序，以Sheet为单位
		/// </summary>
		private string m_Sequence; 
		/// <summary>
		/// 
		/// </summary>
		private int m_Width; 
		/// <summary>
		/// 
		/// </summary>
		private int m_Height; 
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
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Sheet():base(){
			m_ID = -1;
			m_Cld_Constant_List = null;
			m_Cld_FCBlock_List = null;
			m_Cld_FCInput_List = null;
			m_Cld_FCOutput_List = null;
			m_Cld_FCParameter_List = null;
			m_Cld_Graphic_List = null;
			m_Cld_Signal_List = null;
			m_SheetName = String.Empty;
			m_SheetNum = -1;
			m_Sequence = String.Empty;
			m_Width = -1;
			m_Height = -1;
			m_Prj_Controller_ID = -1;
			m_Prj_Controller = null;
			m_Prj_Document_ID = -1;
			m_Prj_Document = null;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Sheet ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Constant_List
		{
			get
            {
                if (Reload || this.m_Cld_Constant_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_Constant as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCBlock_List
		{
			get
            {
                if (Reload || this.m_Cld_FCBlock_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCBlock as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCInput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCInput_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCInput as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCOutput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCOutput_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCOutput as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCParameter_List
		{
			get
            {
                if (Reload || this.m_Cld_FCParameter_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCParameter as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Graphic_List
		{
			get
            {
                if (Reload || this.m_Cld_Graphic_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_Graphic as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Signal_List
		{
			get
            {
                if (Reload || this.m_Cld_Signal_List == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_Signal as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// 组态Sheet名字
		/// </summary>		
		public virtual string SheetName
		{
			get { return m_SheetName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SheetName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("SheetName", m_SheetName);
				switch(this.State){
					case objstate.Loaded:
						if(m_SheetName != value){
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
				m_SheetName = value;
			}

		}		
		/// <summary>
		/// 组态Sheet序号
		/// </summary>		
		public virtual int SheetNum
		{
			get { return m_SheetNum; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_SheetNum != value){
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
				m_SheetNum = value; 
			}

		}		
		/// <summary>
		/// 算法块的执行顺序，以Sheet为单位
		/// </summary>		
		public virtual string Sequence
		{
			get { return m_Sequence; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Sequence", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Sequence", m_Sequence);
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
		/// 
		/// </summary>		
		public virtual int Width
		{
			get { return m_Width; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Width != value){
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
				m_Width = value; 
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Height
		{
			get { return m_Height; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Height != value){
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
				m_Height = value; 
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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							this.m_Prj_Controller = 
								Prj_Sheet.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							this.m_Prj_Document = 
								Prj_Sheet.session.Get<Prj_Document>(this.Prj_Document_ID);
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
	

	}
}

