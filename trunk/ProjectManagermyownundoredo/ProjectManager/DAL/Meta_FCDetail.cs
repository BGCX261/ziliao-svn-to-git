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
	public partial class Meta_FCDetail　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// ID
		/// </summary>
		private int m_ID; 
		/// <summary>
		/// 功能码名称
		/// </summary>
		private string m_FunctionName; 
		/// <summary>
		/// 管脚（或规格数、IO、Tag）名称
		/// </summary>
		private string m_PinName; 
		/// <summary>
		/// 顺序
		/// </summary>
		private int m_PinIndex; 
		/// <summary>
		/// 数据类型
		/// </summary>
		private string m_DataType; 
		/// <summary>
		/// 是否可调
		/// </summary>
		private bool m_Tune; 
		/// <summary>
		/// 1：Input，2：Constant，3：Output
		/// </summary>
		private string m_PinType; 
		/// <summary>
		/// 最大值
		/// </summary>
		private string m_MaxValue; 
		/// <summary>
		/// 最小值
		/// </summary>
		private string m_MinValue; 
		/// <summary>
		/// 有效值范围
		/// </summary>
		private string m_ValidValue; 
		/// <summary>
		/// 默认值
		/// </summary>
		private string m_DefaultValue; 
		/// <summary>
		/// 是否必要
		/// </summary>
		private bool m_Required; 
		/// <summary>
		/// 描述
		/// </summary>
		private string m_Description; 
		/// <summary>
		/// 地址是否绑定，不用分配
		/// </summary>
		private bool m_Fixed; 
		/// <summary>
		/// 信号类型
		/// </summary>
		private string m_PinSignalType; 
		/// <summary>
		/// Meta FCMaster ID
		/// </summary>
		private Meta_FCMaster m_Meta_FCMaster;
		private int m_Meta_FCMaster_ID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Meta_FCDetail():base(){
			m_ID = -1;
			m_FunctionName = String.Empty;
			m_PinName = String.Empty;
			m_PinIndex = -1;
			m_DataType = String.Empty;
			m_Tune = false;
			m_PinType = String.Empty;
			m_MaxValue = String.Empty;
			m_MinValue = String.Empty;
			m_ValidValue = String.Empty;
			m_DefaultValue = String.Empty;
			m_Required = false;
			m_Description = String.Empty;
			m_Fixed = false;
			m_PinSignalType = String.Empty;
			m_Meta_FCMaster_ID = -1;
			m_Meta_FCMaster = null;
			
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
		/// 功能码名称
		/// </summary>		
		public virtual string FunctionName
		{
			get { return m_FunctionName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for FunctionName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("FunctionName", m_FunctionName);
				switch(this.State){
					case objstate.Loaded:
						if(m_FunctionName != value){
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
				m_FunctionName = value;
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
		/// 顺序
		/// </summary>		
		public virtual int PinIndex
		{
			get { return m_PinIndex; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_PinIndex != value){
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
				m_PinIndex = value; 
			}

		}		
		/// <summary>
		/// 数据类型
		/// </summary>		
		public virtual string DataType
		{
			get { return m_DataType; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for DataType", value, value.ToString());
				}
				//to store the undo info
                AddHistory("DataType", m_DataType);
				switch(this.State){
					case objstate.Loaded:
						if(m_DataType != value){
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
				m_DataType = value;
			}

		}		
		/// <summary>
		/// 是否可调
		/// </summary>		
		public virtual bool Tune
		{
			get { return m_Tune; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Tune != value){
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
				m_Tune = value; 
			}

		}		
		/// <summary>
		/// 1：Input，2：Constant，3：Output
		/// </summary>		
		public virtual string PinType
		{
			get { return m_PinType; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinType", value, value.ToString());
				}
				//to store the undo info
                AddHistory("PinType", m_PinType);
				switch(this.State){
					case objstate.Loaded:
						if(m_PinType != value){
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
				m_PinType = value;
			}

		}		
		/// <summary>
		/// 最大值
		/// </summary>		
		public virtual string MaxValue
		{
			get { return m_MaxValue; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for MaxValue", value, value.ToString());
				}
				//to store the undo info
                AddHistory("MaxValue", m_MaxValue);
				switch(this.State){
					case objstate.Loaded:
						if(m_MaxValue != value){
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
				m_MaxValue = value;
			}

		}		
		/// <summary>
		/// 最小值
		/// </summary>		
		public virtual string MinValue
		{
			get { return m_MinValue; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for MinValue", value, value.ToString());
				}
				//to store the undo info
                AddHistory("MinValue", m_MinValue);
				switch(this.State){
					case objstate.Loaded:
						if(m_MinValue != value){
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
				m_MinValue = value;
			}

		}		
		/// <summary>
		/// 有效值范围
		/// </summary>		
		public virtual string ValidValue
		{
			get { return m_ValidValue; }
			set	
			{
				if ( value != null)
				if( value.Length > 200){
					throw new ArgumentOutOfRangeException("Invalid value for ValidValue", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ValidValue", m_ValidValue);
				switch(this.State){
					case objstate.Loaded:
						if(m_ValidValue != value){
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
				m_ValidValue = value;
			}

		}		
		/// <summary>
		/// 默认值
		/// </summary>		
		public virtual string DefaultValue
		{
			get { return m_DefaultValue; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for DefaultValue", value, value.ToString());
				}
				//to store the undo info
                AddHistory("DefaultValue", m_DefaultValue);
				switch(this.State){
					case objstate.Loaded:
						if(m_DefaultValue != value){
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
				m_DefaultValue = value;
			}

		}		
		/// <summary>
		/// 是否必要
		/// </summary>		
		public virtual bool Required
		{
			get { return m_Required; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Required != value){
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
				m_Required = value; 
			}

		}		
		/// <summary>
		/// 描述
		/// </summary>		
		public virtual string Description
		{
			get { return m_Description; }
			set	
			{
				if ( value != null)
				if( value.Length > 255){
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
		/// 地址是否绑定，不用分配
		/// </summary>		
		public virtual bool Fixed
		{
			get { return m_Fixed; }
			set 
			{	
				switch(this.State){
					case objstate.Loaded:
						if(m_Fixed != value){
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
				m_Fixed = value; 
			}

		}		
		/// <summary>
		/// 信号类型
		/// </summary>		
		public virtual string PinSignalType
		{
			get { return m_PinSignalType; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for PinSignalType", value, value.ToString());
				}
				//to store the undo info
                AddHistory("PinSignalType", m_PinSignalType);
				switch(this.State){
					case objstate.Loaded:
						if(m_PinSignalType != value){
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
				m_PinSignalType = value;
			}

		}		
				
		/// <summary>
		/// Meta FCMaster ID
		/// </summary>
		public virtual int Meta_FCMaster_ID
		{
			get { return m_Meta_FCMaster_ID ;}
			set 
			{ 				
				m_Meta_FCMaster_ID = value; 
			}
		}
		/// <summary>
		/// Meta FCMaster ID
		/// </summary>
		public virtual Meta_FCMaster Meta_FCMaster
		{
			get
            {
                if (Reload || this.m_Meta_FCMaster == null)
                {
                    if (Meta_FCDetail.session != null)
                    {
						ITransaction transaction = Meta_FCDetail.session.BeginTransaction();
						try{
							this.m_Meta_FCMaster = 
								Meta_FCDetail.session.Get<Meta_FCMaster>(this.Meta_FCMaster_ID);
							transaction.Commit();
							return this.m_Meta_FCMaster;
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
                return m_Meta_FCMaster;
            }
			set { m_Meta_FCMaster = value; }
		}
	

	}
}

