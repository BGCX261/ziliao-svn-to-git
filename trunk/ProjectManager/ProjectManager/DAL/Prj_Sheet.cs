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
	public partial class Prj_Sheet　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// Sheet ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_Constant_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_FCBlock_List;// = new UndoRedo<UndoRedoList<object>>();
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
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_Graphic_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Cld_Signal_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// 组态Sheet名字
		/// </summary>
		private UndoRedo<string> m_SheetName;// = new UndoRedo<string>();
		/// <summary>
		/// 组态Sheet序号
		/// </summary>
		private UndoRedo<int> m_SheetNum;// = new UndoRedo<int>();
		/// <summary>
		/// 算法块的执行顺序，以Sheet为单位
		/// </summary>
		private UndoRedo<string> m_Sequence;// = new UndoRedo<string>();
		/// <summary>
		/// 
		/// </summary>
		private UndoRedo<int> m_Width;// = new UndoRedo<int>();
		/// <summary>
		/// 
		/// </summary>
		private UndoRedo<int> m_Height;// = new UndoRedo<int>();
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
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Sheet():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Cld_Constant_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_Constant_List.Owner = this;
			this.m_Cld_FCBlock_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCBlock_List.Owner = this;
			this.m_Cld_FCInput_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCInput_List.Owner = this;
			this.m_Cld_FCOutput_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCOutput_List.Owner = this;
			this.m_Cld_FCParameter_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_FCParameter_List.Owner = this;
			this.m_Cld_Graphic_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_Graphic_List.Owner = this;
			this.m_Cld_Signal_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Cld_Signal_List.Owner = this;
			this.m_SheetName = new UndoRedo<string>(String.Empty);
			this.m_SheetName.Owner = this;
			this.m_SheetNum = new UndoRedo<int>(-1);
			this.m_SheetNum.Owner = this;
			this.m_Sequence = new UndoRedo<string>(String.Empty);
			this.m_Sequence.Owner = this;
			this.m_Width = new UndoRedo<int>(-1);
			this.m_Width.Owner = this;
			this.m_Height = new UndoRedo<int>(-1);
			this.m_Height.Owner = this;
			this.m_Prj_Controller_ID = new UndoRedo<int>(-1);
			this.m_Prj_Controller_ID.Owner = this;
			this.m_Prj_Controller = new UndoRedo<Prj_Controller>(null);
			this.m_Prj_Controller.Owner = this;
			this.m_Prj_Document_ID = new UndoRedo<int>(-1);
			this.m_Prj_Document_ID.Owner = this;
			this.m_Prj_Document = new UndoRedo<Prj_Document>(null);
			this.m_Prj_Document.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Sheet ID
		/// </summary>
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_Constant_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_Constant_List.Value;
				}
                if (Reload || this.m_Cld_Constant_List.Value == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_Constant as i where i.Prj_Sheet_ID = " + this.ID)
									.List();
							this.m_Cld_Constant_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_Constant_List.Value;
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
                return m_Cld_Constant_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_Constant_List.Value = null;
				}else{
					m_Cld_Constant_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_FCBlock_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_FCBlock_List.Value;
				}
                if (Reload || this.m_Cld_FCBlock_List.Value == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCBlock as i where i.Prj_Sheet_ID = " + this.ID)
									.List();
							this.m_Cld_FCBlock_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_FCBlock_List.Value;
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
                return m_Cld_FCBlock_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_FCBlock_List.Value = null;
				}else{
					m_Cld_FCBlock_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCInput as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCOutput as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_FCParameter as i where i.Prj_Sheet_ID = " + this.ID)
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
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_Graphic_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_Graphic_List.Value;
				}
                if (Reload || this.m_Cld_Graphic_List.Value == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_Graphic as i where i.Prj_Sheet_ID = " + this.ID)
									.List();
							this.m_Cld_Graphic_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_Graphic_List.Value;
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
                return m_Cld_Graphic_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_Graphic_List.Value = null;
				}else{
					m_Cld_Graphic_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// Sheet ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Cld_Signal_List
		{
			get
            {
				if(this.Orin){
					return m_Cld_Signal_List.Value;
				}
                if (Reload || this.m_Cld_Signal_List.Value == null)
                {
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							IList temp = Prj_Sheet.session
									.CreateQuery("from Cld_Signal as i where i.Prj_Sheet_ID = " + this.ID)
									.List();
							this.m_Cld_Signal_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Cld_Signal_List.Value;
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
                return m_Cld_Signal_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Cld_Signal_List.Value = null;
				}else{
					m_Cld_Signal_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// 组态Sheet名字
		/// </summary>		
		public virtual string SheetName
		{
			get { return m_SheetName.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SheetName", value, value.ToString());
				}
				m_SheetName.Value = value;
			}

		}		
		/// <summary>
		/// 组态Sheet序号
		/// </summary>		
		public virtual int SheetNum
		{
			get { return m_SheetNum.Value; }
			set { m_SheetNum.Value = value; }

		}		
		/// <summary>
		/// 算法块的执行顺序，以Sheet为单位
		/// </summary>		
		public virtual string Sequence
		{
			get { return m_Sequence.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Sequence", value, value.ToString());
				}
				m_Sequence.Value = value;
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Width
		{
			get { return m_Width.Value; }
			set { m_Width.Value = value; }

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Height
		{
			get { return m_Height.Value; }
			set { m_Height.Value = value; }

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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							this.m_Prj_Controller.Value = 
								Prj_Sheet.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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
                    if (Prj_Sheet.session != null)
                    {
						ITransaction transaction = Prj_Sheet.session.BeginTransaction();
						try{
							this.m_Prj_Document.Value = 
								Prj_Sheet.session.Get<Prj_Document>(this.Prj_Document_ID);
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
	

	}
}

