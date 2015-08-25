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
	public partial class Cld_FCParameter　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private int m_ID; 
		/// <summary>
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>
		private string m_Name; 
		/// <summary>
		/// 管脚（或规格数、IO、Tag）值
		/// </summary>
		private string m_PValue; 
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
		public Cld_FCParameter():base(){
			m_ID = -1;
			m_Name = String.Empty;
			m_PValue = String.Empty;
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
		/// 管脚（或规格数、IO、Tag）值
		/// </summary>		
		public virtual string PValue
		{
			get { return m_PValue; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PValue", value, value.ToString());
				}
				//to store the undo info
                AddHistory("PValue", m_PValue);
				switch(this.State){
					case objstate.Loaded:
						if(m_PValue != value){
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
				m_PValue = value;
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
                    if (Cld_FCParameter.session != null)
                    {
						ITransaction transaction = Cld_FCParameter.session.BeginTransaction();
						try{
							this.m_Cld_FCBlock = 
								Cld_FCParameter.session.Get<Cld_FCBlock>(this.Cld_FCBlock_ID);
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
                    if (Cld_FCParameter.session != null)
                    {
						ITransaction transaction = Cld_FCParameter.session.BeginTransaction();
						try{
							this.m_Prj_Sheet = 
								Cld_FCParameter.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
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
                    if (Cld_FCParameter.session != null)
                    {
						ITransaction transaction = Cld_FCParameter.session.BeginTransaction();
						try{
							this.m_Prj_Document = 
								Cld_FCParameter.session.Get<Prj_Document>(this.Prj_Document_ID);
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
                    if (Cld_FCParameter.session != null)
                    {
						ITransaction transaction = Cld_FCParameter.session.BeginTransaction();
						try{
							this.m_Prj_Controller = 
								Cld_FCParameter.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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

