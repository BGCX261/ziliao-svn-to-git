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
	public partial class Prj_Unit　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// Unite ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Prj_Controller_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// Unit地址
		/// </summary>
		private UndoRedo<string> m_UnitAddress;// = new UndoRedo<string>();
		/// <summary>
		/// Unit名字
		/// </summary>
		private UndoRedo<string> m_UnitName;// = new UndoRedo<string>();
		/// <summary>
		/// NetworkAdress
		/// </summary>
		private UndoRedo<string> m_NetworkAddress;// = new UndoRedo<string>();
		/// <summary>
		/// 描述
		/// </summary>
		private UndoRedo<string> m_Description;// = new UndoRedo<string>();
		/// <summary>
		/// 所在的Network ID
		/// </summary>
		private UndoRedo<Prj_Network> m_Prj_Network;// = new UndoRedo<Prj_Network>();
		private UndoRedo<int> m_Prj_Network_ID;// = new UndoRedo<int>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Unit():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Prj_Controller_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Prj_Controller_List.Owner = this;
			this.m_UnitAddress = new UndoRedo<string>(String.Empty);
			this.m_UnitAddress.Owner = this;
			this.m_UnitName = new UndoRedo<string>(String.Empty);
			this.m_UnitName.Owner = this;
			this.m_NetworkAddress = new UndoRedo<string>(String.Empty);
			this.m_NetworkAddress.Owner = this;
			this.m_Description = new UndoRedo<string>(String.Empty);
			this.m_Description.Owner = this;
			this.m_Prj_Network_ID = new UndoRedo<int>(-1);
			this.m_Prj_Network_ID.Owner = this;
			this.m_Prj_Network = new UndoRedo<Prj_Network>(null);
			this.m_Prj_Network.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Unite ID
		/// </summary>
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		/// Unite ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Prj_Controller_List
		{
			get
            {
				if(this.Orin){
					return m_Prj_Controller_List.Value;
				}
                if (Reload || this.m_Prj_Controller_List.Value == null)
                {
                    if (Prj_Unit.session != null)
                    {
						ITransaction transaction = Prj_Unit.session.BeginTransaction();
						try{
							IList temp = Prj_Unit.session
									.CreateQuery("from Prj_Controller as i where i.Prj_Unit_ID = " + this.ID)
									.List();
							this.m_Prj_Controller_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Prj_Controller_List.Value;
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
                return m_Prj_Controller_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Prj_Controller_List.Value = null;
				}else{
					m_Prj_Controller_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// Unit地址
		/// </summary>		
		public virtual string UnitAddress
		{
			get { return m_UnitAddress.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for UnitAddress", value, value.ToString());
				}
				m_UnitAddress.Value = value;
			}

		}		
		/// <summary>
		/// Unit名字
		/// </summary>		
		public virtual string UnitName
		{
			get { return m_UnitName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for UnitName", value, value.ToString());
				}
				m_UnitName.Value = value;
			}

		}		
		/// <summary>
		/// NetworkAdress
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
		/// 描述
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
		/// 所在的Network ID
		/// </summary>
		public virtual int Prj_Network_ID
		{
			get 
			{ 
				if(m_Prj_Network_ID.Value == -1 && this.m_Prj_Network.Value != null){
					this.m_Prj_Network_ID.Value = this.m_Prj_Network.Value.ID;
					return m_Prj_Network_ID.Value ;
				}else{
					return m_Prj_Network_ID.Value ;
				}
			}
			set { m_Prj_Network_ID.Value = value;}
		}
		/// <summary>
		/// 所在的Network ID
		/// </summary>
		public virtual Prj_Network Prj_Network
		{
			get
            {
				if(this.Orin){
					return m_Prj_Network.Value;
				}
                if (Reload || this.m_Prj_Network.Value == null)
                {
                    if (Prj_Unit.session != null)
                    {
						ITransaction transaction = Prj_Unit.session.BeginTransaction();
						try{
							this.m_Prj_Network.Value = 
								Prj_Unit.session.Get<Prj_Network>(this.Prj_Network_ID);
							transaction.Commit();
							return this.m_Prj_Network.Value;
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
                return m_Prj_Network.Value;
            }
			set { m_Prj_Network.Value = value; }
		}
	

	}
}

