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
	public partial class Meta_FCDetail　: EntityBase
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private UndoRedo<int> m_ID;// = new UndoRedo<int>();
		/// <summary>
		/// 功能码名称
		/// </summary>
		private UndoRedo<string> m_FunctionName;// = new UndoRedo<string>();
		/// <summary>
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>
		private UndoRedo<string> m_PinName;// = new UndoRedo<string>();
		/// <summary>
		/// 顺序
		/// </summary>
		private UndoRedo<int> m_PinIndex;// = new UndoRedo<int>();
		/// <summary>
		/// 数据类型
		/// </summary>
		private UndoRedo<string> m_DataType;// = new UndoRedo<string>();
		/// <summary>
		/// 是否可调
		/// </summary>
		private UndoRedo<bool> m_Tune;// = new UndoRedo<bool>();
		/// <summary>
		/// 1：Input，2：Constant，3：Output
		/// </summary>
		private UndoRedo<string> m_PinType;// = new UndoRedo<string>();
		/// <summary>
		/// 最大值
		/// </summary>
		private UndoRedo<string> m_MaxValue;// = new UndoRedo<string>();
		/// <summary>
		/// 最小值
		/// </summary>
		private UndoRedo<string> m_MinValue;// = new UndoRedo<string>();
		/// <summary>
		/// 有效值范围
		/// </summary>
		private UndoRedo<string> m_ValidValue;// = new UndoRedo<string>();
		/// <summary>
		/// 默认值
		/// </summary>
		private UndoRedo<string> m_DefaultValue;// = new UndoRedo<string>();
		/// <summary>
		/// 是否必要
		/// </summary>
		private UndoRedo<bool> m_Required;// = new UndoRedo<bool>();
		/// <summary>
		/// 描述
		/// </summary>
		private UndoRedo<string> m_Description;// = new UndoRedo<string>();
		/// <summary>
		/// 地址是否绑定，不用分配
		/// </summary>
		private UndoRedo<bool> m_Fixed;// = new UndoRedo<bool>();
		/// <summary>
		/// 信号类型
		/// </summary>
		private UndoRedo<string> m_PinSignalType;// = new UndoRedo<string>();
		/// <summary>
		/// Meta FCMaster ID
		/// </summary>
		private UndoRedo<Meta_FCMaster> m_Meta_FCMaster;// = new UndoRedo<Meta_FCMaster>();
		private UndoRedo<int> m_Meta_FCMaster_ID;// = new UndoRedo<int>();
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					
		/// <summary>
		/// 默认构造函数
		/// <summary>
		public Meta_FCDetail():base(){
			this.m_ID = new UndoRedo<int>(-1);
			this.m_ID.Owner = this;
			this.m_FunctionName = new UndoRedo<string>(String.Empty);
			this.m_FunctionName.Owner = this;
			this.m_PinName = new UndoRedo<string>(String.Empty);
			this.m_PinName.Owner = this;
			this.m_PinIndex = new UndoRedo<int>(-1);
			this.m_PinIndex.Owner = this;
			this.m_DataType = new UndoRedo<string>(String.Empty);
			this.m_DataType.Owner = this;
			this.m_Tune = new UndoRedo<bool>(false);
			this.m_Tune.Owner = this;
			this.m_PinType = new UndoRedo<string>(String.Empty);
			this.m_PinType.Owner = this;
			this.m_MaxValue = new UndoRedo<string>(String.Empty);
			this.m_MaxValue.Owner = this;
			this.m_MinValue = new UndoRedo<string>(String.Empty);
			this.m_MinValue.Owner = this;
			this.m_ValidValue = new UndoRedo<string>(String.Empty);
			this.m_ValidValue.Owner = this;
			this.m_DefaultValue = new UndoRedo<string>(String.Empty);
			this.m_DefaultValue.Owner = this;
			this.m_Required = new UndoRedo<bool>(false);
			this.m_Required.Owner = this;
			this.m_Description = new UndoRedo<string>(String.Empty);
			this.m_Description.Owner = this;
			this.m_Fixed = new UndoRedo<bool>(false);
			this.m_Fixed.Owner = this;
			this.m_PinSignalType = new UndoRedo<string>(String.Empty);
			this.m_PinSignalType.Owner = this;
			this.m_Meta_FCMaster_ID = new UndoRedo<int>(-1);
			this.m_Meta_FCMaster_ID.Owner = this;
			this.m_Meta_FCMaster = new UndoRedo<Meta_FCMaster>(null);
			this.m_Meta_FCMaster.Owner = this;
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
		/// 顺序
		/// </summary>		
		public virtual int PinIndex
		{
			get { return m_PinIndex.Value; }
			set { m_PinIndex.Value = value; }

		}		
		/// <summary>
		/// 数据类型
		/// </summary>		
		public virtual string DataType
		{
			get { return m_DataType.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for DataType", value, value.ToString());
				}
				m_DataType.Value = value;
			}

		}		
		/// <summary>
		/// 是否可调
		/// </summary>		
		public virtual bool Tune
		{
			get { return m_Tune.Value; }
			set { m_Tune.Value = value; }

		}		
		/// <summary>
		/// 1：Input，2：Constant，3：Output
		/// </summary>		
		public virtual string PinType
		{
			get { return m_PinType.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinType", value, value.ToString());
				}
				m_PinType.Value = value;
			}

		}		
		/// <summary>
		/// 最大值
		/// </summary>		
		public virtual string MaxValue
		{
			get { return m_MaxValue.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for MaxValue", value, value.ToString());
				}
				m_MaxValue.Value = value;
			}

		}		
		/// <summary>
		/// 最小值
		/// </summary>		
		public virtual string MinValue
		{
			get { return m_MinValue.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for MinValue", value, value.ToString());
				}
				m_MinValue.Value = value;
			}

		}		
		/// <summary>
		/// 有效值范围
		/// </summary>		
		public virtual string ValidValue
		{
			get { return m_ValidValue.Value; }
			set	
			{
				if(value != null && value.Length > 200){
					throw new ArgumentOutOfRangeException("Invalid value for ValidValue", value, value.ToString());
				}
				m_ValidValue.Value = value;
			}

		}		
		/// <summary>
		/// 默认值
		/// </summary>		
		public virtual string DefaultValue
		{
			get { return m_DefaultValue.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for DefaultValue", value, value.ToString());
				}
				m_DefaultValue.Value = value;
			}

		}		
		/// <summary>
		/// 是否必要
		/// </summary>		
		public virtual bool Required
		{
			get { return m_Required.Value; }
			set { m_Required.Value = value; }

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
		/// 地址是否绑定，不用分配
		/// </summary>		
		public virtual bool Fixed
		{
			get { return m_Fixed.Value; }
			set { m_Fixed.Value = value; }

		}		
		/// <summary>
		/// 信号类型
		/// </summary>		
		public virtual string PinSignalType
		{
			get { return m_PinSignalType.Value; }
			set	
			{
				if(value != null && value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinSignalType", value, value.ToString());
				}
				m_PinSignalType.Value = value;
			}

		}		
				
		/// <summary>
		/// Meta FCMaster ID
		/// </summary>
		public virtual int Meta_FCMaster_ID
		{
			get 
			{ 
				if(m_Meta_FCMaster_ID.Value == -1 && this.m_Meta_FCMaster.Value != null){
					this.m_Meta_FCMaster_ID.Value = this.m_Meta_FCMaster.Value.ID;
					return m_Meta_FCMaster_ID.Value ;
				}else{
					return m_Meta_FCMaster_ID.Value ;
				}
			}
			set { m_Meta_FCMaster_ID.Value = value;}
		}
		/// <summary>
		/// Meta FCMaster ID
		/// </summary>
		public virtual Meta_FCMaster Meta_FCMaster
		{
			get
            {
				if(this.Orin){
					return m_Meta_FCMaster.Value;
				}
                if (Reload || this.m_Meta_FCMaster.Value == null)
                {
                    if (Meta_FCDetail.session != null)
                    {
						ITransaction transaction = Meta_FCDetail.session.BeginTransaction();
						try{
							this.m_Meta_FCMaster.Value = 
								Meta_FCDetail.session.Get<Meta_FCMaster>(this.Meta_FCMaster_ID);
							transaction.Commit();
							return this.m_Meta_FCMaster.Value;
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
                return m_Meta_FCMaster.Value;
            }
			set { m_Meta_FCMaster.Value = value; }
		}
	

	}
}

