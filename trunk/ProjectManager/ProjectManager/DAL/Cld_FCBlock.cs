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
	public partial class Cld_FCBlock　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_FCInput_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_FCOutput_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_FCParameter_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// 算法块名称
		/// </summary>
		private UndoRedo<string> m_AlgName;// = new UndoRedo<string>();
		/// <summary>
		/// 算法执行顺序
		/// </summary>
		private UndoRedo<int> m_Sequence;// = new UndoRedo<int>();
		/// <summary>
		/// 功能码名称
		/// </summary>
		private UndoRedo<string> m_FunctionName;// = new UndoRedo<string>();
		/// <summary>
		/// 坐标
		/// </summary>
		private UndoRedo<string> m_X_Y;// = new UndoRedo<string>();
		/// <summary>
		/// 图形符号名称
		/// </summary>
		private UndoRedo<string> m_SymbolName;// = new UndoRedo<string>();
		/// <summary>
		/// 描述
		/// </summary>
		private UndoRedo<string> m_Description;// = new UndoRedo<string>();
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
		public Cld_FCBlock():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Cld_FCInput_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCInput_List.Owner = this;
			this.m_Cld_FCOutput_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCOutput_List.Owner = this;
			this.m_Cld_FCParameter_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCParameter_List.Owner = this;
			this.m_AlgName = new UndoRedo<string>(String.Empty);
			this.m_AlgName.Owner = this;
			this.m_Sequence = new UndoRedo<int>(-1);
			this.m_Sequence.Owner = this;
			this.m_FunctionName = new UndoRedo<string>(String.Empty);
			this.m_FunctionName.Owner = this;
			this.m_X_Y = new UndoRedo<string>(String.Empty);
			this.m_X_Y.Owner = this;
			this.m_SymbolName = new UndoRedo<string>(String.Empty);
			this.m_SymbolName.Owner = this;
			this.m_Description = new UndoRedo<string>(String.Empty);
			this.m_Description.Owner = this;
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
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		/// ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_FCInput_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_FCInput_List.Value;
				}
                if (Reload || this.m_Cld_FCInput_List.Value == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							IList temp = Cld_FCBlock.session
									.CreateQuery("from Cld_FCInput as i where i.Cld_FCBlock_ID = " + this.ID)
									.List();
							this.m_Cld_FCInput_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_FCInput_List.Value;
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
                return m_Cld_FCInput_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_FCInput_List.Value = null;
				}else{
					m_Cld_FCInput_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_FCOutput_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_FCOutput_List.Value;
				}
                if (Reload || this.m_Cld_FCOutput_List.Value == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							IList temp = Cld_FCBlock.session
									.CreateQuery("from Cld_FCOutput as i where i.Cld_FCBlock_ID = " + this.ID)
									.List();
							this.m_Cld_FCOutput_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_FCOutput_List.Value;
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
                return m_Cld_FCOutput_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_FCOutput_List.Value = null;
				}else{
					m_Cld_FCOutput_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_FCParameter_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_FCParameter_List.Value;
				}
                if (Reload || this.m_Cld_FCParameter_List.Value == null)
                {
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							IList temp = Cld_FCBlock.session
									.CreateQuery("from Cld_FCParameter as i where i.Cld_FCBlock_ID = " + this.ID)
									.List();
							this.m_Cld_FCParameter_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_FCParameter_List.Value;
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
                return m_Cld_FCParameter_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_FCParameter_List.Value = null;
				}else{
					m_Cld_FCParameter_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// 算法块名称
		/// </summary>		
		public virtual string AlgName
		{
			get { return m_AlgName.Value; }
			set	
			{
				if(value != null && value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for AlgName", value, value.ToString());
				}
				m_AlgName.Value = value;
			}

		}		
		/// <summary>
		/// 算法执行顺序
		/// </summary>		
		public virtual int Sequence
		{
			get { return m_Sequence.Value; }
			set { m_Sequence.Value = value; }

		}		
		/// <summary>
		/// 功能码名称
		/// </summary>		
		public virtual string FunctionName
		{
			get { return m_FunctionName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for FunctionName", value, value.ToString());
				}
				m_FunctionName.Value = value;
			}

		}		
		/// <summary>
		/// 坐标
		/// </summary>		
		public virtual string X_Y
		{
			get { return m_X_Y.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for X_Y", value, value.ToString());
				}
				m_X_Y.Value = value;
			}

		}		
		/// <summary>
		/// 图形符号名称
		/// </summary>		
		public virtual string SymbolName
		{
			get { return m_SymbolName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SymbolName", value, value.ToString());
				}
				m_SymbolName.Value = value;
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
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				}
				m_Description.Value = value;
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
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							this.m_Prj_Controller.Value = 
								Cld_FCBlock.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							this.m_Prj_Document.Value = 
								Cld_FCBlock.session.Get<Prj_Document>(this.Prj_Document_ID);
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
                    if (Cld_FCBlock.session != null)
                    {
						ITransaction transaction = Cld_FCBlock.session.BeginTransaction();
						try{
							this.m_Prj_Sheet.Value = 
								Cld_FCBlock.session.Get<Prj_Sheet>(this.Prj_Sheet_ID);
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

