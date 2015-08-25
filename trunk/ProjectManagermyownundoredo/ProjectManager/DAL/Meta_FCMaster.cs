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
	public partial class Meta_FCMaster　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// 
		/// </summary>
		private int m_ID;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Meta_FCDetail_List;
		/// <summary>
		/// 功能码名称
		/// </summary>
		private string m_FunctionName; 
		/// <summary>
		/// 功能码序号
		/// </summary>
		private int m_FunctionCode; 
		/// <summary>
		/// 描述
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 功能函数
		/// </summary>
		private string m_Function; 
		/// <summary>
		/// 
		/// </summary>
		private int m_DIAG; 
		/// <summary>
		/// 输入管脚个数
		/// </summary>
		private int m_InputCount; 
		/// <summary>
		/// 规格数个数
		/// </summary>
		private int m_SpecCount; 
		/// <summary>
		/// 输出管脚个数
		/// </summary>
		private int m_OutPutCount; 
		/// <summary>
		/// 内部变量个数
		/// </summary>
		private int m_InternalCount; 
		/// <summary>
		/// FC的空间长度
		/// </summary>
		private short m_FCLength; 
		/// <summary>
		/// Function Code/IO Connector/Constant Block/Cross Reference
		/// </summary>
		private string m_Type; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Meta_FCMaster():base(){
			m_ID = -1;
			m_Meta_FCDetail_List = null;
			m_FunctionName = String.Empty;
			m_FunctionCode = -1;
			m_Description = String.Empty;
			m_Function = String.Empty;
			m_DIAG = -1;
			m_InputCount = -1;
			m_SpecCount = -1;
			m_OutPutCount = -1;
			m_InternalCount = -1;
			m_FCLength = -1;
			m_Type = String.Empty;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// 
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		///  ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Meta_FCDetail_List
		{
			get
            {
                if (Reload || this.m_Meta_FCDetail_List == null)
                {
                    if (Meta_FCMaster.session != null)
                    {
						ITransaction transaction = Meta_FCMaster.session.BeginTransaction();
						try{
							IList temp = Meta_FCMaster.session
									.CreateQuery("from Meta_FCDetail as i where i.Meta_FCMaster_ID = " + this.ID)
									.List();
							this.m_Meta_FCDetail_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Meta_FCDetail_List;
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
                return m_Meta_FCDetail_List;
            }
			set 
			{ 
				if(this.m_Meta_FCDetail_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Meta_FCDetail_List = value; 
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
		/// 功能码序号
		/// </summary>		
		public virtual int FunctionCode
		{
			get { return m_FunctionCode; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_FunctionCode != value){
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
				m_FunctionCode = value; 
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
				if( value.Length > 255){
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
		/// 功能函数
		/// </summary>		
		public virtual string Function
		{
			get { return m_Function; }
			set	
			{
				if ( value != null)
				if( value.Length > 200){
					throw new ArgumentOutOfRangeException("Invalid value for Function", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Function", m_Function);
				switch(this.State){
					case objstate.Loaded:
						if(m_Function != value){
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
				m_Function = value;
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual int DIAG
		{
			get { return m_DIAG; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_DIAG != value){
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
				m_DIAG = value; 
			}

		}		
		/// <summary>
		/// 输入管脚个数
		/// </summary>		
		public virtual int InputCount
		{
			get { return m_InputCount; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_InputCount != value){
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
				m_InputCount = value; 
			}

		}		
		/// <summary>
		/// 规格数个数
		/// </summary>		
		public virtual int SpecCount
		{
			get { return m_SpecCount; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_SpecCount != value){
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
				m_SpecCount = value; 
			}

		}		
		/// <summary>
		/// 输出管脚个数
		/// </summary>		
		public virtual int OutPutCount
		{
			get { return m_OutPutCount; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_OutPutCount != value){
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
				m_OutPutCount = value; 
			}

		}		
		/// <summary>
		/// 内部变量个数
		/// </summary>		
		public virtual int InternalCount
		{
			get { return m_InternalCount; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_InternalCount != value){
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
				m_InternalCount = value; 
			}

		}		
		/// <summary>
		/// FC的空间长度
		/// </summary>		
		public virtual short FCLength
		{
			get { return m_FCLength; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_FCLength != value){
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
				m_FCLength = value; 
			}

		}		
		/// <summary>
		/// Function Code/IO Connector/Constant Block/Cross Reference
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
	

	}
}

