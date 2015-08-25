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
	public partial class Cld_FCInput　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>
		private UndoRedo<string> m_PinName;// = new UndoRedo<string>();
		/// <summary>
		/// 管脚（或规格数、IO、Tag）值
		/// </summary>
		private UndoRedo<string> m_PointName;// = new UndoRedo<string>();
		/// <summary>
		/// 初始值
		/// </summary>
		private UndoRedo<string> m_InitialValue;// = new UndoRedo<string>();
		/// <summary>
		/// 管脚坐标
		/// </summary>
		private UndoRedo<string> m_Point;// = new UndoRedo<string>();
		/// <summary>
		/// 是否可见
		/// </summary>
		private UndoRedo<bool> m_Visible;// = new UndoRedo<bool>();
		/// <summary>
		/// 注释
		/// </summary>
		private UndoRedo<string> m_Description;// = new UndoRedo<string>();
		/// <summary>
		/// 块的ID
		/// </summary>
		private UndoRedo<Cld_FCBlock> m_Cld_FCBlock;// = new UndoRedo<Cld_FCBlock>();
		private UndoRedo<int> m_Cld_FCBlock_ID;// = new UndoRedo<int>();
		/// <summary>
		/// Sheet的ID
		/// </summary>
		private UndoRedo<Prj_Sheet> m_Prj_Sheet;// = new UndoRedo<Prj_Sheet>();
		private UndoRedo<int> m_Prj_Sheet_ID;// = new UndoRedo<int>();
		/// <summary>
		/// Document的ID
		/// </summary>
		private UndoRedo<Prj_Document> m_Prj_Document;// = new UndoRedo<Prj_Document>();
		private UndoRedo<int> m_Prj_Document_ID;// = new UndoRedo<int>();
		/// <summary>
		/// Controller的ID
		/// </summary>
		private UndoRedo<Prj_Controller> m_Prj_Controller;// = new UndoRedo<Prj_Controller>();
		private UndoRedo<int> m_Prj_Controller_ID;// = new UndoRedo<int>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Cld_FCInput():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_PinName = new UndoRedo<string>(String.Empty);
			this.m_PinName.Owner = this;
			this.m_PointName = new UndoRedo<string>(String.Empty);
			this.m_PointName.Owner = this;
			this.m_InitialValue = new UndoRedo<string>(String.Empty);
			this.m_InitialValue.Owner = this;
			this.m_Point = new UndoRedo<string>(String.Empty);
			this.m_Point.Owner = this;
			this.m_Visible = new UndoRedo<bool>(false);
			this.m_Visible.Owner = this;
			this.m_Description = new UndoRedo<string>(String.Empty);
			this.m_Description.Owner = this;
			this.m_Cld_FCBlock_ID = new UndoRedo<int>(-1);
			this.m_Cld_FCBlock_ID.Owner = this;
			this.m_Cld_FCBlock = new UndoRedo<Cld_FCBlock>(null);
			this.m_Cld_FCBlock.Owner = this;
			this.m_Prj_Sheet_ID = new UndoRedo<int>(-1);
			this.m_Prj_Sheet_ID.Owner = this;
			this.m_Prj_Sheet = new UndoRedo<Prj_Sheet>(null);
			this.m_Prj_Sheet.Owner = this;
			this.m_Prj_Document_ID = new UndoRedo<int>(-1);
			this.m_Prj_Document_ID.Owner = this;
			this.m_Prj_Document = new UndoRedo<Prj_Document>(null);
			this.m_Prj_Document.Owner = this;
			this.m_Prj_Controller_ID = new UndoRedo<int>(-1);
			this.m_Prj_Controller_ID.Owner = this;
			this.m_Prj_Controller = new UndoRedo<Prj_Controller>(null);
			this.m_Prj_Controller.Owner = this;
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
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>		
		public virtual string PinName
		{
			get { return m_PinName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinName", value, value.ToString());
				}
				m_PinName.Value = value;
			}

		}		
		/// <summary>
		/// 管脚（或规格数、IO、Tag）值
		/// </summary>		
		public virtual string PointName
		{
			get { return m_PointName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PointName", value, value.ToString());
				}
				m_PointName.Value = value;
			}

		}		
		/// <summary>
		/// 初始值
		/// </summary>		
		public virtual string InitialValue
		{
			get { return m_InitialValue.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for InitialValue", value, value.ToString());
				}
				m_InitialValue.Value = value;
			}

		}		
		/// <summary>
		/// 管脚坐标
		/// </summary>		
		public virtual string Point
		{
			get { return m_Point.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Point", value, value.ToString());
				}
				m_Point.Value = value;
			}

		}		
		/// <summary>
		/// 是否可见
		/// </summary>		
		public virtual bool Visible
		{
			get { return m_Visible.Value; }
			set { m_Visible.Value = value; }

		}		
		/// <summary>
		/// 注释
		/// </summary>		
		public virtual string Description
		{
			get { return m_Description.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				}
				m_Description.Value = value;
			}

		}		
				
		/// <summary>
		/// 块的ID
		/// </summary>
		public virtual int Cld_FCBlock_ID
		{
			get 
			{ 
				if(m_Cld_FCBlock_ID.Value == -1 && this.m_Cld_FCBlock.Value != null){
					this.m_Cld_FCBlock_ID.Value = this.m_Cld_FCBlock.Value.ID;
					return m_Cld_FCBlock_ID.Value ;
				}else{
					return m_Cld_FCBlock_ID.Value ;
				}
			}
			set { m_Cld_FCBlock_ID.Value = value;}
		}
		/// <summary>
		/// 块的ID
		/// </summary>
		public virtual Cld_FCBlock Cld_FCBlock
		{
			get
            {
				if(this.Orin){
					return m_Cld_FCBlock.Value;
				}
                if (Reload || this.m_Cld_FCBlock.Value == null)
                {
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Cld_FCBlock.Value = 
								Cld_FCInput.session.Get<Cld_FCBlock>(this.Cld_FCBlock_ID);
							transaction.Commit();
							return this.m_Cld_FCBlock.Value;
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
                return m_Cld_FCBlock.Value;
            }
			set { m_Cld_FCBlock.Value = value; }
		}
				
		/// <summary>
		/// Sheet的ID
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
		/// Sheet的ID
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
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Prj_Sheet.Value = 
								Cld_FCInput.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
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
				
		/// <summary>
		/// Document的ID
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
		/// Document的ID
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
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Prj_Document.Value = 
								Cld_FCInput.session.Get<Prj_Document>(this.Prj_Document_ID);
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
		/// Controller的ID
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
		/// Controller的ID
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
                    if (Cld_FCInput.session != null)
                    {
						ITransaction transaction = Cld_FCInput.session.BeginTransaction();
						try{
							this.m_Prj_Controller.Value = 
								Cld_FCInput.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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
	

	}
}

