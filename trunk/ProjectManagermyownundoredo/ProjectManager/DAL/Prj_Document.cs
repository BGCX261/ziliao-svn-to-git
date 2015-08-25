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
	public partial class Prj_Document　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// Document ID
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
		private MyList m_Prj_Sheet_List;
		/// <summary>
		/// 组态文档名称
		/// </summary>
		private string m_DocumentName; 
		/// <summary>
		/// 保存文档名称
		/// </summary>
		private string m_DocumentCaption; 
		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreateTime; 
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime m_ModifyTime; 
		/// <summary>
		/// 算法块的执行顺序
		/// </summary>
		private int m_Sequence; 
		/// <summary>
		/// 文档类型，用后缀表示，.cld .dwg等
		/// </summary>
		private string m_Type; 
		/// <summary>
		/// 转换结果
		/// </summary>
		private string m_TranslatorResult; 
		/// <summary>
		/// 是否修改
		/// </summary>
		private string m_changed; 
		/// <summary>
		/// 控制器ID
		/// </summary>
		private Prj_Controller m_Prj_Controller;
		private int m_Prj_Controller_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Document():base(){
			m_ID = -1;
			m_Cld_Constant_List = null;
			m_Cld_FCBlock_List = null;
			m_Cld_FCInput_List = null;
			m_Cld_FCOutput_List = null;
			m_Cld_FCParameter_List = null;
			m_Cld_Graphic_List = null;
			m_Cld_Signal_List = null;
			m_Prj_Sheet_List = null;
			m_DocumentName = String.Empty;
			m_DocumentCaption = String.Empty;
			m_CreateTime = DateTime.MinValue;
			m_ModifyTime = DateTime.MinValue;
			m_Sequence = -1;
			m_Type = String.Empty;
			m_TranslatorResult = String.Empty;
			m_changed = String.Empty;
			m_Prj_Controller_ID = -1;
			m_Prj_Controller = null;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Document ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Constant_List
		{
			get
            {
                if (Reload || this.m_Cld_Constant_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_Constant as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCBlock_List
		{
			get
            {
                if (Reload || this.m_Cld_FCBlock_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCBlock as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCInput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCInput_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCInput as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCOutput_List
		{
			get
            {
                if (Reload || this.m_Cld_FCOutput_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCOutput as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_FCParameter_List
		{
			get
            {
                if (Reload || this.m_Cld_FCParameter_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCParameter as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Graphic_List
		{
			get
            {
                if (Reload || this.m_Cld_Graphic_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_Graphic as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Cld_Signal_List
		{
			get
            {
                if (Reload || this.m_Cld_Signal_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_Signal as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Prj_Sheet_List
		{
			get
            {
                if (Reload || this.m_Prj_Sheet_List == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Prj_Sheet as i where i.Prj_Document_ID = " + this.ID)
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
		/// 组态文档名称
		/// </summary>		
		public virtual string DocumentName
		{
			get { return m_DocumentName; }
			set	
			{
				if ( value != null)
				if( value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for DocumentName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("DocumentName", m_DocumentName);
				switch(this.State){
					case objstate.Loaded:
						if(m_DocumentName != value){
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
				m_DocumentName = value;
			}

		}		
		/// <summary>
		/// 保存文档名称
		/// </summary>		
		public virtual string DocumentCaption
		{
			get { return m_DocumentCaption; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for DocumentCaption", value, value.ToString());
				}
				//to store the undo info
                AddHistory("DocumentCaption", m_DocumentCaption);
				switch(this.State){
					case objstate.Loaded:
						if(m_DocumentCaption != value){
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
				m_DocumentCaption = value;
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
		/// 算法块的执行顺序
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
		/// 文档类型，用后缀表示，.cld .dwg等
		/// </summary>		
		public virtual string Type
		{
			get { return m_Type; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Type", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Type", m_Type);
				switch(this.State){
					case objstate.Loaded:
						if(m_Type != value){
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
				m_Type = value;
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
		/// 是否修改
		/// </summary>		
		public virtual string changed
		{
			get { return m_changed; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for changed", value, value.ToString());
				}
				//to store the undo info
                AddHistory("changed", m_changed);
				switch(this.State){
					case objstate.Loaded:
						if(m_changed != value){
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
				m_changed = value;
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							this.m_Prj_Controller = 
								Prj_Document.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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
	

	}
}

