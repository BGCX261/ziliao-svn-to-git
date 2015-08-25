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
	public partial class Cld_FCInput　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private int m_ID; 
		/// <summary>
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>
		private string m_PinName; 
		/// <summary>
		/// 管脚（或规格数、IO、Tag）值
		/// </summary>
		private string m_PointName; 
		/// <summary>
		/// 初始值
		/// </summary>
		private string m_InitialValue; 
		/// <summary>
		/// 管脚坐标
		/// </summary>
		private string m_Point; 
		/// <summary>
		/// 是否可见
		/// </summary>
		private bool m_Visible; 
		/// <summary>
		/// 注释
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 块的ID
		/// </summary>
		private Cld_FCBlock m_Cld_FCBlock;
		private int m_Cld_FCBlock_ID; 
		/// <summary>
		/// Sheet的ID
		/// </summary>
		private Prj_Sheet m_Prj_Sheet;
		private int m_Prj_Sheet_ID; 
		/// <summary>
		/// Document的ID
		/// </summary>
		private Prj_Document m_Prj_Document;
		private int m_Prj_Document_ID; 
		/// <summary>
		/// Controller的ID
		/// </summary>
		private Prj_Controller m_Prj_Controller;
		private int m_Prj_Controller_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Cld_FCInput():base(){
			m_ID = -1;
			m_PinName = String.Empty;
			m_PointName = String.Empty;
			m_InitialValue = String.Empty;
			m_Point = String.Empty;
			m_Visible = false;
			m_Description = String.Empty;
			m_Cld_FCBlock_ID = -1;
			m_Cld_FCBlock = null;
			m_Prj_Sheet_ID = -1;
			m_Prj_Sheet = null;
			m_Prj_Document_ID = -1;
			m_Prj_Document = null;
			m_Prj_Controller_ID = -1;
			m_Prj_Controller = null;
			
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
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>		
		public virtual string PinName
		{
			get { return m_PinName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("PinName", m_PinName);
				switch(this.State){
					case objstate.Loaded:
						if(m_PinName != value){
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
				m_PinName = value;
			}

		}		
		/// <summary>
		/// 管脚（或规格数、IO、Tag）值
		/// </summary>		
		public virtual string PointName
		{
			get { return m_PointName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PointName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("PointName", m_PointName);
				switch(this.State){
					case objstate.Loaded:
						if(m_PointName != value){
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
				m_PointName = value;
			}

		}		
		/// <summary>
		/// 初始值
		/// </summary>		
		public virtual string InitialValue
		{
			get { return m_InitialValue; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for InitialValue", value, value.ToString());
				}
				//to store the undo info
                AddHistory("InitialValue", m_InitialValue);
				switch(this.State){
					case objstate.Loaded:
						if(m_InitialValue != value){
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
				m_InitialValue = value;
			}

		}		
		/// <summary>
		/// 管脚坐标
		/// </summary>		
		public virtual string Point
		{
			get { return m_Point; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Point", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Point", m_Point);
				switch(this.State){
					case objstate.Loaded:
						if(m_Point != value){
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
				m_Point = value;
			}

		}		
		/// <summary>
		/// 是否可见
		/// </summary>		
		public virtual bool Visible
		{
			get { return m_Visible; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Visible != value){
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
				m_Visible = value; 
			}

		}		
		/// <summary>
		/// 注释
		/// </summary>		
		public virtual string Description
		{
			get { return m_Description; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
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
		/// 块的ID
		/// </summary>
		public virtual int Cld_FCBlock_ID
		{
			get { return m_Cld_FCBlock_ID ;}
			set 
			{ 				
				m_Cld_FCBlock_ID = value; 
			}
		}
		/// <summary>
		/// 块的ID
		/// </summary>
		public virtual Cld_FCBlock Cld_FCBlock
		{
			get
            {
                if (Reload || this.m_Cld_FCBlock == null)
                {
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Cld_FCBlock = 
								Cld_FCInput.session.Get<Cld_FCBlock>(this.Cld_FCBlock_ID);
							transaction.Commit();
							return this.m_Cld_FCBlock;
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
                return m_Cld_FCBlock;
            }
			set { m_Cld_FCBlock = value; }
		}
				
		/// <summary>
		/// Sheet的ID
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
		/// Sheet的ID
		/// </summary>
		public virtual Prj_Sheet Prj_Sheet
		{
			get
            {
                if (Reload || this.m_Prj_Sheet == null)
                {
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Prj_Sheet = 
								Cld_FCInput.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
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
				
		/// <summary>
		/// Document的ID
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
		/// Document的ID
		/// </summary>
		public virtual Prj_Document Prj_Document
		{
			get
            {
                if (Reload || this.m_Prj_Document == null)
                {
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Prj_Document = 
								Cld_FCInput.session.Get<Prj_Document>(this.Prj_Document_ID);
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
		/// Controller的ID
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
		/// Controller的ID
		/// </summary>
		public virtual Prj_Controller Prj_Controller
		{
			get
            {
                if (Reload || this.m_Prj_Controller == null)
                {
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Prj_Controller = 
								Cld_FCInput.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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

