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
	public partial class Prj_Network　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// Network ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Prj_Unit_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// 控制器地址
		/// </summary>
		private UndoRedo<string> m_NetworkAddress;// = new UndoRedo<string>();
		/// <summary>
		/// 控制器名字
		/// </summary>
		private UndoRedo<string> m_NetworkName;// = new UndoRedo<string>();
		/// <summary>
		/// 控制器描述
		/// </summary>
		private UndoRedo<string> m_Description;// = new UndoRedo<string>();
		/// <summary>
		/// 所在的Project ID
		/// </summary>
		private UndoRedo<Prj_Project> m_Prj_Project;// = new UndoRedo<Prj_Project>();
		private UndoRedo<int> m_Prj_Project_ID;// = new UndoRedo<int>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Network():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Prj_Unit_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Prj_Unit_List.Owner = this;
			this.m_NetworkAddress = new UndoRedo<string>(String.Empty);
			this.m_NetworkAddress.Owner = this;
			this.m_NetworkName = new UndoRedo<string>(String.Empty);
			this.m_NetworkName.Owner = this;
			this.m_Description = new UndoRedo<string>(String.Empty);
			this.m_Description.Owner = this;
			this.m_Prj_Project_ID = new UndoRedo<int>(-1);
			this.m_Prj_Project_ID.Owner = this;
			this.m_Prj_Project = new UndoRedo<Prj_Project>(null);
			this.m_Prj_Project.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Network ID
		/// </summary>
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		/// Network ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Prj_Unit_List
		{
			get
            {
				if(this.Orin){
					return m_Prj_Unit_List.Value;
				}
                if (Reload || this.m_Prj_Unit_List.Value == null)
                {
                    if (Prj_Network.session != null)
                    {
						ITransaction transaction = Prj_Network.session.BeginTransaction();
						try{
							IList temp = Prj_Network.session
									.CreateQuery("from Prj_Unit as i where i.Prj_Network_ID = " + this.ID)
									.List();
							this.m_Prj_Unit_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Prj_Unit_List.Value;
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
                return m_Prj_Unit_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Prj_Unit_List.Value = null;
				}else{
					m_Prj_Unit_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// 控制器地址
		/// </summary>		
		public virtual string NetworkAddress
		{
			get { return m_NetworkAddress.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for NetworkAddress", value, value.ToString());
				}
				m_NetworkAddress.Value = value;
			}

		}		
		/// <summary>
		/// 控制器名字
		/// </summary>		
		public virtual string NetworkName
		{
			get { return m_NetworkName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for NetworkName", value, value.ToString());
				}
				m_NetworkName.Value = value;
			}

		}		
		/// <summary>
		/// 控制器描述
		/// </summary>		
		public virtual string Description
		{
			get { return m_Description.Value; }
			set	
			{
				if(value != null && value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				}
				m_Description.Value = value;
			}

		}		
				
		/// <summary>
		/// 所在的Project ID
		/// </summary>
		public virtual int Prj_Project_ID
		{
			get 
			{ 
				if(m_Prj_Project_ID.Value == -1 && this.m_Prj_Project.Value != null){
					this.m_Prj_Project_ID.Value = this.m_Prj_Project.Value.ID;
					return m_Prj_Project_ID.Value ;
				}else{
					return m_Prj_Project_ID.Value ;
				}
			}
			set { m_Prj_Project_ID.Value = value;}
		}
		/// <summary>
		/// 所在的Project ID
		/// </summary>
		public virtual Prj_Project Prj_Project
		{
			get
            {
				if(this.Orin){
					return m_Prj_Project.Value;
				}
                if (Reload || this.m_Prj_Project.Value == null)
                {
                    if (Prj_Network.session != null)
                    {
						ITransaction transaction = Prj_Network.session.BeginTransaction();
						try{
							this.m_Prj_Project.Value = 
								Prj_Network.session.Get<Prj_Project>(this.Prj_Project_ID);
							transaction.Commit();
							return this.m_Prj_Project.Value;
						}catch(Exception e){
							transaction.Rollback();
							throw e;
						}
						Reload=false;
                    }
                    else
                    {
                        throw new Exception("the session is not open");
                    }
                }
                return m_Prj_Project.Value;
            }
			set { m_Prj_Project.Value = value; }
		}
	

	}
}

