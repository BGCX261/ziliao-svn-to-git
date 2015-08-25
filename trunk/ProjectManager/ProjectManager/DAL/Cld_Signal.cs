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
	public partial class Cld_Signal　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
		/// 信号名称
		/// </summary>
		private UndoRedo<string> m_Name;// = new UndoRedo<string>();
		/// <summary>
		/// 信号类型
		/// </summary>
		private UndoRedo<string> m_SignalType;// = new UndoRedo<string>();
		/// <summary>
		/// 管脚与信号线绑定关系
		/// </summary>
		private UndoRedo<string> m_EntityBelongTo;// = new UndoRedo<string>();
		/// <summary>
		/// 数据
		/// </summary>
		private UndoRedo<string> m_Data;// = new UndoRedo<string>();
		/// <summary>
		/// 控制器ID
		/// </summary>
		private UndoRedo<Prj_Controller> m_Prj_Controller;// = new UndoRedo<Prj_Controller>();
		private UndoRedo<int> m_Prj_Controller_ID;// = new UndoRedo<int>();
		/// <summary>
		/// 组态文档ID
		/// </summary>
		private UndoRedo<Prj_Document> m_Prj_Document;// = new UndoRedo<Prj_Document>();
		private UndoRedo<int> m_Prj_Document_ID;// = new UndoRedo<int>();
		/// <summary>
		/// 组态SheetID
		/// </summary>
		private UndoRedo<Prj_Sheet> m_Prj_Sheet;// = new UndoRedo<Prj_Sheet>();
		private UndoRedo<int> m_Prj_Sheet_ID;// = new UndoRedo<int>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Cld_Signal():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Name = new UndoRedo<string>(String.Empty);
			this.m_Name.Owner = this;
			this.m_SignalType = new UndoRedo<string>(String.Empty);
			this.m_SignalType.Owner = this;
			this.m_EntityBelongTo = new UndoRedo<string>(String.Empty);
			this.m_EntityBelongTo.Owner = this;
			this.m_Data = new UndoRedo<string>(String.Empty);
			this.m_Data.Owner = this;
			this.m_Prj_Controller_ID = new UndoRedo<int>(-1);
			this.m_Prj_Controller_ID.Owner = this;
			this.m_Prj_Controller = new UndoRedo<Prj_Controller>(null);
			this.m_Prj_Controller.Owner = this;
			this.m_Prj_Document_ID = new UndoRedo<int>(-1);
			this.m_Prj_Document_ID.Owner = this;
			this.m_Prj_Document = new UndoRedo<Prj_Document>(null);
			this.m_Prj_Document.Owner = this;
			this.m_Prj_Sheet_ID = new UndoRedo<int>(-1);
			this.m_Prj_Sheet_ID.Owner = this;
			this.m_Prj_Sheet = new UndoRedo<Prj_Sheet>(null);
			this.m_Prj_Sheet.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// ID
		/// </summary>		
		public virtual int ID
		{
			get { return m_ID.Value; }
			set { m_ID.Value = value; }

		}		
		/// <summary>
		/// 信号名称
		/// </summary>		
		public virtual string Name
		{
			get { return m_Name.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				}
				m_Name.Value = value;
			}

		}		
		/// <summary>
		/// 信号类型
		/// </summary>		
		public virtual string SignalType
		{
			get { return m_SignalType.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SignalType", value, value.ToString());
				}
				m_SignalType.Value = value;
			}

		}		
		/// <summary>
		/// 管脚与信号线绑定关系
		/// </summary>		
		public virtual string EntityBelongTo
		{
			get { return m_EntityBelongTo.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for EntityBelongTo", value, value.ToString());
				}
				m_EntityBelongTo.Value = value;
			}

		}		
		/// <summary>
		/// 数据
		/// </summary>		
		public virtual string Data
		{
			get { return m_Data.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Data", value, value.ToString());
				}
				m_Data.Value = value;
			}

		}		
				
		/// <summary>
		/// 控制器ID
		/// </summary>
		public virtual int Prj_Controller_ID
		{
			get 
			{ 
				if(m_Prj_Controller_ID.Value == -1 && this.m_Prj_Controller.Value != null){
					this.m_Prj_Controller_ID.Value = this.m_Prj_Controller.Value.ID;
					return m_Prj_Controller_ID.Value ;
				}else{
					return m_Prj_Controller_ID.Value ;
				}
			}
			set { m_Prj_Controller_ID.Value = value;}
		}
		/// <summary>
		/// 控制器ID
		/// </summary>
		public virtual Prj_Controller Prj_Controller
		{
			get
            {
				if(this.Orin){
					return m_Prj_Controller.Value;
				}
                if (Reload || this.m_Prj_Controller.Value == null)
                {
                    if (Cld_Signal.session != null)
                    {
						ITransaction transaction = Cld_Signal.session.BeginTransaction();
						try{
							this.m_Prj_Controller.Value = 
								Cld_Signal.session.Get<Prj_Controller>(this.Prj_Controller_ID);
							transaction.Commit();
							return this.m_Prj_Controller.Value;
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
                return m_Prj_Controller.Value;
            }
			set { m_Prj_Controller.Value = value; }
		}
				
		/// <summary>
		/// 组态文档ID
		/// </summary>
		public virtual int Prj_Document_ID
		{
			get 
			{ 
				if(m_Prj_Document_ID.Value == -1 && this.m_Prj_Document.Value != null){
					this.m_Prj_Document_ID.Value = this.m_Prj_Document.Value.ID;
					return m_Prj_Document_ID.Value ;
				}else{
					return m_Prj_Document_ID.Value ;
				}
			}
			set { m_Prj_Document_ID.Value = value;}
		}
		/// <summary>
		/// 组态文档ID
		/// </summary>
		public virtual Prj_Document Prj_Document
		{
			get
            {
				if(this.Orin){
					return m_Prj_Document.Value;
				}
                if (Reload || this.m_Prj_Document.Value == null)
                {
                    if (Cld_Signal.session != null)
                    {
						ITransaction transaction = Cld_Signal.session.BeginTransaction();
						try{
							this.m_Prj_Document.Value = 
								Cld_Signal.session.Get<Prj_Document>(this.Prj_Document_ID);
							transaction.Commit();
							return this.m_Prj_Document.Value;
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
                return m_Prj_Document.Value;
            }
			set { m_Prj_Document.Value = value; }
		}
				
		/// <summary>
		/// 组态SheetID
		/// </summary>
		public virtual int Prj_Sheet_ID
		{
			get 
			{ 
				if(m_Prj_Sheet_ID.Value == -1 && this.m_Prj_Sheet.Value != null){
					this.m_Prj_Sheet_ID.Value = this.m_Prj_Sheet.Value.ID;
					return m_Prj_Sheet_ID.Value ;
				}else{
					return m_Prj_Sheet_ID.Value ;
				}
			}
			set { m_Prj_Sheet_ID.Value = value;}
		}
		/// <summary>
		/// 组态SheetID
		/// </summary>
		public virtual Prj_Sheet Prj_Sheet
		{
			get
            {
				if(this.Orin){
					return m_Prj_Sheet.Value;
				}
                if (Reload || this.m_Prj_Sheet.Value == null)
                {
                    if (Cld_Signal.session != null)
                    {
						ITransaction transaction = Cld_Signal.session.BeginTransaction();
						try{
							this.m_Prj_Sheet.Value = 
								Cld_Signal.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
							transaction.Commit();
							return this.m_Prj_Sheet.Value;
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
                return m_Prj_Sheet.Value;
            }
			set { m_Prj_Sheet.Value = value; }
		}
	

	}
}

