using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using DejaVu;
using DejaVu.Collections.Generic;

namespace TDK.Core.Logic.DAL
{
	/// <summary>
	///	
	/// </summary>
	[Serializable]
	public partial class Prj_Project　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// Project ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Prj_Network_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// 项目名称
		/// </summary>
		private UndoRedo<string> m_ProjectName;// = new UndoRedo<string>();
		/// <summary>
		/// 创建时间
		/// </summary>
		private UndoRedo<DateTime> m_CreateTime;// = new UndoRedo<DateTime>();
		/// <summary>
		/// 修改时间
		/// </summary>
		private UndoRedo<DateTime> m_ModifyTime;// = new UndoRedo<DateTime>();
		/// <summary>
		/// 项目类型：ABB，WestWood
		/// </summary>
		private UndoRedo<string> m_Type;// = new UndoRedo<string>();
		/// <summary>
		/// 项目配置，当前项目分配的Block的个数
		/// </summary>
		private UndoRedo<string> m_BlockMaxNumber;// = new UndoRedo<string>();
		/// <summary>
		/// 项目配置，当前项目分配的Pin的个数
		/// </summary>
		private UndoRedo<string> m_PinMaxNumber;// = new UndoRedo<string>();
		/// <summary>
		/// 项目路径
		/// </summary>
		private UndoRedo<string> m_ProjectPath;// = new UndoRedo<string>();
		/// <summary>
		/// 系统点版本
		/// </summary>
		private UndoRedo<string> m_VarSystemVersion;// = new UndoRedo<string>();
		/// <summary>
		/// Alarm点版本
		/// </summary>
		private UndoRedo<string> m_AlarmVersion;// = new UndoRedo<string>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Project():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Prj_Network_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Prj_Network_List.Owner = this;
			this.m_ProjectName = new UndoRedo<string>(String.Empty);
			this.m_ProjectName.Owner = this;
			this.m_CreateTime = new UndoRedo<DateTime>(DateTime.MinValue);
			this.m_CreateTime.Owner = this;
			this.m_ModifyTime = new UndoRedo<DateTime>(DateTime.MinValue);
			this.m_ModifyTime.Owner = this;
			this.m_Type = new UndoRedo<string>(String.Empty);
			this.m_Type.Owner = this;
			this.m_BlockMaxNumber = new UndoRedo<string>(String.Empty);
			this.m_BlockMaxNumber.Owner = this;
			this.m_PinMaxNumber = new UndoRedo<string>(String.Empty);
			this.m_PinMaxNumber.Owner = this;
			this.m_ProjectPath = new UndoRedo<string>(String.Empty);
			this.m_ProjectPath.Owner = this;
			this.m_VarSystemVersion = new UndoRedo<string>(String.Empty);
			this.m_VarSystemVersion.Owner = this;
			this.m_AlarmVersion = new UndoRedo<string>(String.Empty);
			this.m_AlarmVersion.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Project ID
		/// </summary>
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		/// Project ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Prj_Network_List
		{
			get
            {
				if(this.Orin){
					return m_Prj_Network_List.Value;
				}
                if (Reload || this.m_Prj_Network_List.Value == null)
                {
                    if (Prj_Project.session != null)
                    {
						ITransaction transaction = Prj_Project.session.BeginTransaction();
						try{
							IList temp = Prj_Project.session
									.CreateQuery("from Prj_Network as i where i.Prj_Project_ID = " + this.ID)
									.List();
							this.m_Prj_Network_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Prj_Network_List.Value;
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
                return m_Prj_Network_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Prj_Network_List.Value = null;
				}else{
					m_Prj_Network_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// 项目名称
		/// </summary>		
		public virtual string ProjectName
		{
			get { return m_ProjectName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ProjectName", value, value.ToString());
				}
				m_ProjectName.Value = value;
			}

		}		
		/// <summary>
		/// 创建时间
		/// </summary>		
		public virtual DateTime CreateTime
		{
			get { return m_CreateTime.Value; }
			set { m_CreateTime.Value = value; }

		}		
		/// <summary>
		/// 修改时间
		/// </summary>		
		public virtual DateTime ModifyTime
		{
			get { return m_ModifyTime.Value; }
			set { m_ModifyTime.Value = value; }

		}		
		/// <summary>
		/// 项目类型：ABB，WestWood
		/// </summary>		
		public virtual string Type
		{
			get { return m_Type.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Type", value, value.ToString());
				}
				m_Type.Value = value;
			}

		}		
		/// <summary>
		/// 项目配置，当前项目分配的Block的个数
		/// </summary>		
		public virtual string BlockMaxNumber
		{
			get { return m_BlockMaxNumber.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for BlockMaxNumber", value, value.ToString());
				}
				m_BlockMaxNumber.Value = value;
			}

		}		
		/// <summary>
		/// 项目配置，当前项目分配的Pin的个数
		/// </summary>		
		public virtual string PinMaxNumber
		{
			get { return m_PinMaxNumber.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinMaxNumber", value, value.ToString());
				}
				m_PinMaxNumber.Value = value;
			}

		}		
		/// <summary>
		/// 项目路径
		/// </summary>		
		public virtual string ProjectPath
		{
			get { return m_ProjectPath.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ProjectPath", value, value.ToString());
				}
				m_ProjectPath.Value = value;
			}

		}		
		/// <summary>
		/// 系统点版本
		/// </summary>		
		public virtual string VarSystemVersion
		{
			get { return m_VarSystemVersion.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for VarSystemVersion", value, value.ToString());
				}
				m_VarSystemVersion.Value = value;
			}

		}		
		/// <summary>
		/// Alarm点版本
		/// </summary>		
		public virtual string AlarmVersion
		{
			get { return m_AlarmVersion.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for AlarmVersion", value, value.ToString());
				}
				m_AlarmVersion.Value = value;
			}

		}		
	

	}
}

