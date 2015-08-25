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
	public partial class Prj_Project　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// Project ID
		/// </summary>
		private int m_ID;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Prj_Network_List;
		/// <summary>
		/// 项目名称
		/// </summary>
		private string m_ProjectName; 
		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreateTime; 
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime m_ModifyTime; 
		/// <summary>
		/// 项目类型：ABB，WestWood
		/// </summary>
		private string m_Type; 
		/// <summary>
		/// 项目配置，当前项目分配的Block的个数
		/// </summary>
		private string m_BlockMaxNumber; 
		/// <summary>
		/// 项目配置，当前项目分配的Pin的个数
		/// </summary>
		private string m_PinMaxNumber; 
		/// <summary>
		/// 项目路径
		/// </summary>
		private string m_ProjectPath; 
		/// <summary>
		/// 系统点版本
		/// </summary>
		private string m_VarSystemVersion; 
		/// <summary>
		/// Alarm点版本
		/// </summary>
		private string m_AlarmVersion; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Project():base(){
			m_ID = -1;
			m_Prj_Network_List = null;
			m_ProjectName = String.Empty;
			m_CreateTime = DateTime.MinValue;
			m_ModifyTime = DateTime.MinValue;
			m_Type = String.Empty;
			m_BlockMaxNumber = String.Empty;
			m_PinMaxNumber = String.Empty;
			m_ProjectPath = String.Empty;
			m_VarSystemVersion = String.Empty;
			m_AlarmVersion = String.Empty;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Project ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// Project ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Prj_Network_List
		{
			get
            {
                if (Reload || this.m_Prj_Network_List == null)
                {
                    if (Prj_Project.session != null)
                    {
						ITransaction transaction = Prj_Project.session.BeginTransaction();
						try{
							IList temp = Prj_Project.session
									.CreateQuery("from Prj_Network as i where i.Prj_Project_ID = " + this.ID)
									.List();
							this.m_Prj_Network_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Prj_Network_List;
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
                return m_Prj_Network_List;
            }
			set 
			{ 
				if(this.m_Prj_Network_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Prj_Network_List = value; 
			}
		}
		/// <summary>
		/// 项目名称
		/// </summary>		
		public virtual string ProjectName
		{
			get { return m_ProjectName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ProjectName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ProjectName", m_ProjectName);
				switch(this.State){
					case objstate.Loaded:
						if(m_ProjectName != value){
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
				m_ProjectName = value;
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
		/// 项目类型：ABB，WestWood
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
		/// 项目配置，当前项目分配的Block的个数
		/// </summary>		
		public virtual string BlockMaxNumber
		{
			get { return m_BlockMaxNumber; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for BlockMaxNumber", value, value.ToString());
				}
				//to store the undo info
                AddHistory("BlockMaxNumber", m_BlockMaxNumber);
				switch(this.State){
					case objstate.Loaded:
						if(m_BlockMaxNumber != value){
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
				m_BlockMaxNumber = value;
			}

		}		
		/// <summary>
		/// 项目配置，当前项目分配的Pin的个数
		/// </summary>		
		public virtual string PinMaxNumber
		{
			get { return m_PinMaxNumber; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinMaxNumber", value, value.ToString());
				}
				//to store the undo info
                AddHistory("PinMaxNumber", m_PinMaxNumber);
				switch(this.State){
					case objstate.Loaded:
						if(m_PinMaxNumber != value){
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
				m_PinMaxNumber = value;
			}

		}		
		/// <summary>
		/// 项目路径
		/// </summary>		
		public virtual string ProjectPath
		{
			get { return m_ProjectPath; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ProjectPath", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ProjectPath", m_ProjectPath);
				switch(this.State){
					case objstate.Loaded:
						if(m_ProjectPath != value){
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
				m_ProjectPath = value;
			}

		}		
		/// <summary>
		/// 系统点版本
		/// </summary>		
		public virtual string VarSystemVersion
		{
			get { return m_VarSystemVersion; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for VarSystemVersion", value, value.ToString());
				}
				//to store the undo info
                AddHistory("VarSystemVersion", m_VarSystemVersion);
				switch(this.State){
					case objstate.Loaded:
						if(m_VarSystemVersion != value){
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
				m_VarSystemVersion = value;
			}

		}		
		/// <summary>
		/// Alarm点版本
		/// </summary>		
		public virtual string AlarmVersion
		{
			get { return m_AlarmVersion; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for AlarmVersion", value, value.ToString());
				}
				//to store the undo info
                AddHistory("AlarmVersion", m_AlarmVersion);
				switch(this.State){
					case objstate.Loaded:
						if(m_AlarmVersion != value){
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
				m_AlarmVersion = value;
			}

		}		
	

	}
}

