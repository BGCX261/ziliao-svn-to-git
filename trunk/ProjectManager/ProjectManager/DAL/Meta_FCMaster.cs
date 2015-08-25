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
	public partial class Meta_FCMaster　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// 
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
        /// 与此对象相关联的IList
        /// </summary>
		private UndoRedo<UndoRedoList<EntityBase>> m_Meta_FCDetail_List;// = new UndoRedo<UndoRedoList<object>>();
		/// <summary>
		/// 功能码名称
		/// </summary>
		private UndoRedo<string> m_FunctionName;// = new UndoRedo<string>();
		/// <summary>
		/// 功能码序号
		/// </summary>
		private UndoRedo<int> m_FunctionCode;// = new UndoRedo<int>();
		/// <summary>
		/// 描述
		/// </summary>
		private UndoRedo<string> m_Description;// = new UndoRedo<string>();
		/// <summary>
		/// 功能函数
		/// </summary>
		private UndoRedo<string> m_Function;// = new UndoRedo<string>();
		/// <summary>
		/// 
		/// </summary>
		private UndoRedo<int> m_DIAG;// = new UndoRedo<int>();
		/// <summary>
		/// 输入管脚个数
		/// </summary>
		private UndoRedo<int> m_InputCount;// = new UndoRedo<int>();
		/// <summary>
		/// 规格数个数
		/// </summary>
		private UndoRedo<int> m_SpecCount;// = new UndoRedo<int>();
		/// <summary>
		/// 输出管脚个数
		/// </summary>
		private UndoRedo<int> m_OutPutCount;// = new UndoRedo<int>();
		/// <summary>
		/// 内部变量个数
		/// </summary>
		private UndoRedo<int> m_InternalCount;// = new UndoRedo<int>();
		/// <summary>
		/// FC的空间长度
		/// </summary>
		private UndoRedo<short> m_FCLength;// = new UndoRedo<short>();
		/// <summary>
		/// Function Code/IO Connector/Constant Block/Cross Reference
		/// </summary>
		private UndoRedo<string> m_Type;// = new UndoRedo<string>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Meta_FCMaster():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_Meta_FCDetail_List = new UndoRedo<UndoRedoList<EntityBase>>(null);
			this.m_Meta_FCDetail_List.Owner = this;
			this.m_FunctionName = new UndoRedo<string>(String.Empty);
			this.m_FunctionName.Owner = this;
			this.m_FunctionCode = new UndoRedo<int>(-1);
			this.m_FunctionCode.Owner = this;
			this.m_Description = new UndoRedo<string>(String.Empty);
			this.m_Description.Owner = this;
			this.m_Function = new UndoRedo<string>(String.Empty);
			this.m_Function.Owner = this;
			this.m_DIAG = new UndoRedo<int>(-1);
			this.m_DIAG.Owner = this;
			this.m_InputCount = new UndoRedo<int>(-1);
			this.m_InputCount.Owner = this;
			this.m_SpecCount = new UndoRedo<int>(-1);
			this.m_SpecCount.Owner = this;
			this.m_OutPutCount = new UndoRedo<int>(-1);
			this.m_OutPutCount.Owner = this;
			this.m_InternalCount = new UndoRedo<int>(-1);
			this.m_InternalCount.Owner = this;
			this.m_FCLength = new UndoRedo<short>(-1);
			this.m_FCLength.Owner = this;
			this.m_Type = new UndoRedo<string>(String.Empty);
			this.m_Type.Owner = this;
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// 
		/// </summary>
		public virtual int ID{
			get { return m_ID.Value; }
			set { m_ID.Value = value;}
		}
		/// <summary>
		///  ,当子元素很多的时候，由于没有分页功能，可能会影响程序性能，此时尽量不要使用此属性
		/// 可以采用相关类的查询类 *CRUD中的相关方法进行分页查询
		/// 此外如果单纯统计子元素的个数，也建议采用 相关查询类 *CRUD中的count方法，
		/// </summary>
		public virtual IList Meta_FCDetail_List
		{
			get
            {
				if(this.Orin){
					return m_Meta_FCDetail_List.Value;
				}
                if (Reload || this.m_Meta_FCDetail_List.Value == null)
                {
                    if (Meta_FCMaster.session != null)
                    {
						ITransaction transaction = Meta_FCMaster.session.BeginTransaction();
						try{
							IList temp = Meta_FCMaster.session
									.CreateQuery("from Meta_FCDetail as i where i.Meta_FCMaster_ID = " + this.ID)
									.List();
							this.m_Meta_FCDetail_List.Value = new UndoRedoList<EntityBase>(temp);
							transaction.Commit();
							return this.m_Meta_FCDetail_List.Value;
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
                return m_Meta_FCDetail_List.Value;
            }
			set 
			{ 
				if(value == null){
					m_Meta_FCDetail_List.Value = null;
				}else{
					m_Meta_FCDetail_List.Value = new UndoRedoList<EntityBase>(value); 
				}
			}
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
		/// 功能码序号
		/// </summary>		
		public virtual int FunctionCode
		{
			get { return m_FunctionCode.Value; }
			set { m_FunctionCode.Value = value; }

		}		
		/// <summary>
		/// 描述
		/// </summary>		
		public virtual string Description
		{
			get { return m_Description.Value; }
			set	
			{
				if(value != null && value.Length > 255){
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				}
				m_Description.Value = value;
			}

		}		
		/// <summary>
		/// 功能函数
		/// </summary>		
		public virtual string Function
		{
			get { return m_Function.Value; }
			set	
			{
				if(value != null && value.Length > 200){
					throw new ArgumentOutOfRangeException("Invalid value for Function", value, value.ToString());
				}
				m_Function.Value = value;
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual int DIAG
		{
			get { return m_DIAG.Value; }
			set { m_DIAG.Value = value; }

		}		
		/// <summary>
		/// 输入管脚个数
		/// </summary>		
		public virtual int InputCount
		{
			get { return m_InputCount.Value; }
			set { m_InputCount.Value = value; }

		}		
		/// <summary>
		/// 规格数个数
		/// </summary>		
		public virtual int SpecCount
		{
			get { return m_SpecCount.Value; }
			set { m_SpecCount.Value = value; }

		}		
		/// <summary>
		/// 输出管脚个数
		/// </summary>		
		public virtual int OutPutCount
		{
			get { return m_OutPutCount.Value; }
			set { m_OutPutCount.Value = value; }

		}		
		/// <summary>
		/// 内部变量个数
		/// </summary>		
		public virtual int InternalCount
		{
			get { return m_InternalCount.Value; }
			set { m_InternalCount.Value = value; }

		}		
		/// <summary>
		/// FC的空间长度
		/// </summary>		
		public virtual short FCLength
		{
			get { return m_FCLength.Value; }
			set { m_FCLength.Value = value; }

		}		
		/// <summary>
		/// Function Code/IO Connector/Constant Block/Cross Reference
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
	

	}
}

