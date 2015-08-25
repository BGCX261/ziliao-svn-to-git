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
	public partial class Prj_Document　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// Document ID
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
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Prj_Sheet_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// 组态文档名称
		/// </summary>
		private UndoRedo<string> m_DocumentName;// = new UndoRedo<string>();
		/// <summary>
		/// 保存文档名称
		/// </summary>
		private UndoRedo<string> m_DocumentCaption;// = new UndoRedo<string>();
		/// <summary>
		/// 创建时间
		/// </summary>
		private UndoRedo<DateTime> m_CreateTime;// = new UndoRedo<DateTime>();
		/// <summary>
		/// 修改时间
		/// </summary>
		private UndoRedo<DateTime> m_ModifyTime;// = new UndoRedo<DateTime>();
		/// <summary>
		/// 算法块的执行顺序
		/// </summary>
		private UndoRedo<int> m_Sequence;// = new UndoRedo<int>();
		/// <summary>
		/// 文档类型，用后缀表示，.cld .dwg等
		/// </summary>
		private UndoRedo<string> m_Type;// = new UndoRedo<string>();
		/// <summary>
		/// 转换结果
		/// </summary>
		private UndoRedo<string> m_TranslatorResult;// = new UndoRedo<string>();
		/// <summary>
		/// 是否修改
		/// </summary>
		private UndoRedo<string> m_changed;// = new UndoRedo<string>();
		/// <summary>
		/// 控制器ID
		/// </summary>
		private UndoRedo<Prj_Controller> m_Prj_Controller;// = new UndoRedo<Prj_Controller>();
		private UndoRedo<int> m_Prj_Controller_ID;// = new UndoRedo<int>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Prj_Document():base(){
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
			this.m_Prj_Sheet_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Prj_Sheet_List.Owner = this;
			this.m_DocumentName = new UndoRedo<string>(String.Empty);
			this.m_DocumentName.Owner = this;
			this.m_DocumentCaption = new UndoRedo<string>(String.Empty);
			this.m_DocumentCaption.Owner = this;
			this.m_CreateTime = new UndoRedo<DateTime>(DateTime.MinValue);
			this.m_CreateTime.Owner = this;
			this.m_ModifyTime = new UndoRedo<DateTime>(DateTime.MinValue);
			this.m_ModifyTime.Owner = this;
			this.m_Sequence = new UndoRedo<int>(-1);
			this.m_Sequence.Owner = this;
			this.m_Type = new UndoRedo<string>(String.Empty);
			this.m_Type.Owner = this;
			this.m_TranslatorResult = new UndoRedo<string>(String.Empty);
			this.m_TranslatorResult.Owner = this;
			this.m_changed = new UndoRedo<string>(String.Empty);
			this.m_changed.Owner = this;
			this.m_Prj_Controller_ID = new UndoRedo<int>(-1);
			this.m_Prj_Controller_ID.Owner = this;
			this.m_Prj_Controller = new UndoRedo<Prj_Controller>(null);
			this.m_Prj_Controller.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// Document ID
		/// </summary>
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_Constant as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCBlock as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCInput as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCOutput as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_FCParameter as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_Graphic as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Cld_Signal as i where i.Prj_Document_ID = " + this.ID)
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
		/// Document ID ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Prj_Sheet_List
		{
			get
            {
				if(this.Orin){
					return m_Prj_Sheet_List.Value;
				}
                if (Reload || this.m_Prj_Sheet_List.Value == null)
                {
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							IList temp = Prj_Document.session
									.CreateQuery("from Prj_Sheet as i where i.Prj_Document_ID = " + this.ID)
									.List();
							this.m_Prj_Sheet_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Prj_Sheet_List.Value;
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
                return m_Prj_Sheet_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Prj_Sheet_List.Value = null;
				}else{
					m_Prj_Sheet_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
		}
		/// <summary>
		/// 组态文档名称
		/// </summary>		
		public virtual string DocumentName
		{
			get { return m_DocumentName.Value; }
			set	
			{
				if(value != null && value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for DocumentName", value, value.ToString());
				}
				m_DocumentName.Value = value;
			}

		}		
		/// <summary>
		/// 保存文档名称
		/// </summary>		
		public virtual string DocumentCaption
		{
			get { return m_DocumentCaption.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for DocumentCaption", value, value.ToString());
				}
				m_DocumentCaption.Value = value;
			}

		}		
		/// <summary>
		/// 创建时间
		/// </summary>		
		public virtual DateTime CreateTime
		{
			get { return m_CreateTime.Value; }
			set { m_CreateTime.Value = value; }

		}		
		/// <summary>
		/// 修改时间
		/// </summary>		
		public virtual DateTime ModifyTime
		{
			get { return m_ModifyTime.Value; }
			set { m_ModifyTime.Value = value; }

		}		
		/// <summary>
		/// 算法块的执行顺序
		/// </summary>		
		public virtual int Sequence
		{
			get { return m_Sequence.Value; }
			set { m_Sequence.Value = value; }

		}		
		/// <summary>
		/// 文档类型，用后缀表示，.cld .dwg等
		/// </summary>		
		public virtual string Type
		{
			get { return m_Type.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Type", value, value.ToString());
				}
				m_Type.Value = value;
			}

		}		
		/// <summary>
		/// 转换结果
		/// </summary>		
		public virtual string TranslatorResult
		{
			get { return m_TranslatorResult.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for TranslatorResult", value, value.ToString());
				}
				m_TranslatorResult.Value = value;
			}

		}		
		/// <summary>
		/// 是否修改
		/// </summary>		
		public virtual string changed
		{
			get { return m_changed.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for changed", value, value.ToString());
				}
				m_changed.Value = value;
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
                    if (Prj_Document.session != null)
                    {
						ITransaction transaction = Prj_Document.session.BeginTransaction();
						try{
							this.m_Prj_Controller.Value = 
								Prj_Document.session.Get<Prj_Controller>(this.Prj_Controller_ID);
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

