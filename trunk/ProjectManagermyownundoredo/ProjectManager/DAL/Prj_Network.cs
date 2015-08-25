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
	public partial class Prj_Network　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// Network ID
		/// </summary>
		private int m_ID;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Prj_Unit_List;
		/// <summary>
		/// 控制器地址
		/// </summary>
		private string m_NetworkAddress; 
		/// <summary>
		/// 控制器名字
		/// </summary>
		private string m_NetworkName; 
		/// <summary>
		/// 控制器描述
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 所在的Project ID
		/// </summary>
		private Prj_Project m_Prj_Project;
		private int m_Prj_Project_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Network():base(){
			m_ID = -1;
			m_Prj_Unit_List = null;
			m_NetworkAddress = String.Empty;
			m_NetworkName = String.Empty;
			m_Description = String.Empty;
			m_Prj_Project_ID = -1;
			m_Prj_Project = null;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Network ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// Network ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Prj_Unit_List
		{
			get
            {
                if (Reload || this.m_Prj_Unit_List == null)
                {
                    if (Prj_Network.session != null)
                    {
						ITransaction transaction = Prj_Network.session.BeginTransaction();
						try{
							IList temp = Prj_Network.session
									.CreateQuery("from Prj_Unit as i where i.Prj_Network_ID = " + this.ID)
									.List();
							this.m_Prj_Unit_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Prj_Unit_List;
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
                return m_Prj_Unit_List;
            }
			set 
			{ 
				if(this.m_Prj_Unit_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Prj_Unit_List = value; 
			}
		}
		/// <summary>
		/// 控制器地址
		/// </summary>		
		public virtual string NetworkAddress
		{
			get { return m_NetworkAddress; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for NetworkAddress", value, value.ToString());
				}
				//to store the undo info
                AddHistory("NetworkAddress", m_NetworkAddress);
				switch(this.State){
					case objstate.Loaded:
						if(m_NetworkAddress != value){
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
				m_NetworkAddress = value;
			}

		}		
		/// <summary>
		/// 控制器名字
		/// </summary>		
		public virtual string NetworkName
		{
			get { return m_NetworkName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for NetworkName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("NetworkName", m_NetworkName);
				switch(this.State){
					case objstate.Loaded:
						if(m_NetworkName != value){
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
				m_NetworkName = value;
			}

		}		
		/// <summary>
		/// 控制器描述
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
		/// 所在的Project ID
		/// </summary>
		public virtual int Prj_Project_ID
		{
			get { return m_Prj_Project_ID ;}
			set 
			{ 				
				m_Prj_Project_ID = value; 
			}
		}
		/// <summary>
		/// 所在的Project ID
		/// </summary>
		public virtual Prj_Project Prj_Project
		{
			get
            {
                if (Reload || this.m_Prj_Project == null)
                {
                    if (Prj_Network.session != null)
                    {
						ITransaction transaction = Prj_Network.session.BeginTransaction();
						try{
							this.m_Prj_Project = 
								Prj_Network.session.Get<Prj_Project>(this.Prj_Project_ID);
							transaction.Commit();
							return this.m_Prj_Project;
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
                return m_Prj_Project;
            }
			set { m_Prj_Project = value; }
		}
	

	}
}

