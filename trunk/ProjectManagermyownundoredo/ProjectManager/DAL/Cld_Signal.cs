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
	public partial class Cld_Signal　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private int m_ID; 
		/// <summary>
		/// 信号名称
		/// </summary>
		private string m_Name; 
		/// <summary>
		/// 信号类型
		/// </summary>
		private string m_SignalType; 
		/// <summary>
		/// 管脚与信号线绑定关系
		/// </summary>
		private string m_EntityBelongTo; 
		/// <summary>
		/// 数据
		/// </summary>
		private string m_Data; 
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
		/// <summary>
		/// 组态SheetID
		/// </summary>
		private Prj_Sheet m_Prj_Sheet;
		private int m_Prj_Sheet_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Cld_Signal():base(){
			m_ID = -1;
			m_Name = String.Empty;
			m_SignalType = String.Empty;
			m_EntityBelongTo = String.Empty;
			m_Data = String.Empty;
			m_Prj_Controller_ID = -1;
			m_Prj_Controller = null;
			m_Prj_Document_ID = -1;
			m_Prj_Document = null;
			m_Prj_Sheet_ID = -1;
			m_Prj_Sheet = null;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// ID
		/// </summary>		
		public virtual int ID
		{
			get { return m_ID; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_ID != value){
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
				m_ID = value; 
			}

		}		
		/// <summary>
		/// 信号名称
		/// </summary>		
		public virtual string Name
		{
			get { return m_Name; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Name", m_Name);
				switch(this.State){
					case objstate.Loaded:
						if(m_Name != value){
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
				m_Name = value;
			}

		}		
		/// <summary>
		/// 信号类型
		/// </summary>		
		public virtual string SignalType
		{
			get { return m_SignalType; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SignalType", value, value.ToString());
				}
				//to store the undo info
                AddHistory("SignalType", m_SignalType);
				switch(this.State){
					case objstate.Loaded:
						if(m_SignalType != value){
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
				m_SignalType = value;
			}

		}		
		/// <summary>
		/// 管脚与信号线绑定关系
		/// </summary>		
		public virtual string EntityBelongTo
		{
			get { return m_EntityBelongTo; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for EntityBelongTo", value, value.ToString());
				}
				//to store the undo info
                AddHistory("EntityBelongTo", m_EntityBelongTo);
				switch(this.State){
					case objstate.Loaded:
						if(m_EntityBelongTo != value){
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
				m_EntityBelongTo = value;
			}

		}		
		/// <summary>
		/// 数据
		/// </summary>		
		public virtual string Data
		{
			get { return m_Data; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Data", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Data", m_Data);
				switch(this.State){
					case objstate.Loaded:
						if(m_Data != value){
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
				m_Data = value;
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
                    if (Cld_Signal.session != null)
                    {
						ITransaction transaction = Cld_Signal.session.BeginTransaction();
						try{
							this.m_Prj_Controller = 
								Cld_Signal.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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
                    if (Cld_Signal.session != null)
                    {
						ITransaction transaction = Cld_Signal.session.BeginTransaction();
						try{
							this.m_Prj_Document = 
								Cld_Signal.session.Get<Prj_Document>(this.Prj_Document_ID);
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
				
		/// <summary>
		/// 组态SheetID
		/// </summary>
		public virtual int Prj_Sheet_ID
		{
			get { return m_Prj_Sheet_ID ;}
			set 
			{ 				
				m_Prj_Sheet_ID = value; 
			}
		}
		/// <summary>
		/// 组态SheetID
		/// </summary>
		public virtual Prj_Sheet Prj_Sheet
		{
			get
            {
                if (Reload || this.m_Prj_Sheet == null)
                {
                    if (Cld_Signal.session != null)
                    {
						ITransaction transaction = Cld_Signal.session.BeginTransaction();
						try{
							this.m_Prj_Sheet = 
								Cld_Signal.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
							transaction.Commit();
							return this.m_Prj_Sheet;
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
                return m_Prj_Sheet;
            }
			set { m_Prj_Sheet = value; }
		}
	

	}
}

