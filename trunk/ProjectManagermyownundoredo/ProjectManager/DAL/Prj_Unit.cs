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
	public partial class Prj_Unit　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// Unite ID
		/// </summary>
		private int m_ID;
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private MyList m_Prj_Controller_List;
		/// <summary>
		/// Unit地址
		/// </summary>
		private string m_UnitAddress; 
		/// <summary>
		/// Unit名字
		/// </summary>
		private string m_UnitName; 
		/// <summary>
		/// NetworkAdress
		/// </summary>
		private string m_NetworkAddress; 
		/// <summary>
		/// 描述
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 所在的Network ID
		/// </summary>
		private Prj_Network m_Prj_Network;
		private int m_Prj_Network_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Unit():base(){
			m_ID = -1;
			m_Prj_Controller_List = null;
			m_UnitAddress = String.Empty;
			m_UnitName = String.Empty;
			m_NetworkAddress = String.Empty;
			m_Description = String.Empty;
			m_Prj_Network_ID = -1;
			m_Prj_Network = null;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Unite ID
		/// </summary>
		public virtual int ID{
			get { return m_ID; }
			set { m_ID = value;}
		}
		/// <summary>
		/// Unite ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual MyList Prj_Controller_List
		{
			get
            {
                if (Reload || this.m_Prj_Controller_List == null)
                {
                    if (Prj_Unit.session != null)
                    {
						ITransaction transaction = Prj_Unit.session.BeginTransaction();
						try{
							IList temp = Prj_Unit.session
									.CreateQuery("from Prj_Controller as i where i.Prj_Unit_ID = " + this.ID)
									.List();
							this.m_Prj_Controller_List = new MyList(temp,this);
							transaction.Commit();
							return this.m_Prj_Controller_List;
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
                return m_Prj_Controller_List;
            }
			set 
			{ 
				if(this.m_Prj_Controller_List != null){
                    throw new Exception("not implemented");                                    
                }
				m_Prj_Controller_List = value; 
			}
		}
		/// <summary>
		/// Unit地址
		/// </summary>		
		public virtual string UnitAddress
		{
			get { return m_UnitAddress; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for UnitAddress", value, value.ToString());
				}
				//to store the undo info
                AddHistory("UnitAddress", m_UnitAddress);
				switch(this.State){
					case objstate.Loaded:
						if(m_UnitAddress != value){
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
				m_UnitAddress = value;
			}

		}		
		/// <summary>
		/// Unit名字
		/// </summary>		
		public virtual string UnitName
		{
			get { return m_UnitName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for UnitName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("UnitName", m_UnitName);
				switch(this.State){
					case objstate.Loaded:
						if(m_UnitName != value){
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
				m_UnitName = value;
			}

		}		
		/// <summary>
		/// NetworkAdress
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
		/// 所在的Network ID
		/// </summary>
		public virtual int Prj_Network_ID
		{
			get { return m_Prj_Network_ID ;}
			set 
			{ 				
				m_Prj_Network_ID = value; 
			}
		}
		/// <summary>
		/// 所在的Network ID
		/// </summary>
		public virtual Prj_Network Prj_Network
		{
			get
            {
                if (Reload || this.m_Prj_Network == null)
                {
                    if (Prj_Unit.session != null)
                    {
						ITransaction transaction = Prj_Unit.session.BeginTransaction();
						try{
							this.m_Prj_Network = 
								Prj_Unit.session.Get<Prj_Network>(this.Prj_Network_ID);
							transaction.Commit();
							return this.m_Prj_Network;
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
                return m_Prj_Network;
            }
			set { m_Prj_Network = value; }
		}
	

	}
}

