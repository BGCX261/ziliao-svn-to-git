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
	public partial class Cld_IRef　: Base
	{

		
		#region 私有成员
		/// <summary>
		/// 
		/// </summary>
		private string m_ObjectID; 
		/// <summary>
		/// 
		/// </summary>
		private string m_ControllerAddress; 
		/// <summary>
		/// 
		/// </summary>
		private string m_DocumentName; 
		/// <summary>
		/// 
		/// </summary>
		private string m_SheetName; 
		/// <summary>
		/// 交叉引用名字
		/// </summary>
		private string m_RefName; 
		/// <summary>
		/// 源交叉引用的ID
		/// </summary>
		private string m_SrcRefID; 
		/// <summary>
		/// 如果不是IO则为空
		/// </summary>
		private string m_FunctionCode; 
		/// <summary>
		/// FC
		/// </summary>
		private string m_FunctionName; 
		/// <summary>
		/// 如果是IO Connector则是与该交叉引用连接的引脚名字
		/// </summary>
		private string m_PinName; 
		/// <summary>
		/// 如果是IO Connector该引脚的地址
		/// </summary>
		private string m_Address; 
		/// <summary>
		/// 位置
		/// </summary>
		private string m_X_Y; 
		/// <summary>
		/// 网络标号
		/// </summary>
		private string m_NetworkID; 
		
		
		
		#endregion 私有成员
		
		#region 构造函数
					/// <summary>
		/// 默认构造函数
		/// <summary>
		public Cld_IRef():base(){
			m_ObjectID = String.Empty;
			m_ControllerAddress = String.Empty;
			m_DocumentName = String.Empty;
			m_SheetName = String.Empty;
			m_RefName = String.Empty;
			m_SrcRefID = String.Empty;
			m_FunctionCode = String.Empty;
			m_FunctionName = String.Empty;
			m_PinName = String.Empty;
			m_Address = String.Empty;
			m_X_Y = String.Empty;
			m_NetworkID = String.Empty;
			
		}

		#endregion 构造函数
		
					

		/// <summary>
		/// 
		/// </summary>		
		public virtual string ObjectID
		{
			get { return m_ObjectID; }
			set	
			{
				if ( value != null)
				if( value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for ObjectID", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ObjectID", m_ObjectID);
				switch(this.State){
					case objstate.Loaded:
						if(m_ObjectID != value){
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
				m_ObjectID = value;
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual string ControllerAddress
		{
			get { return m_ControllerAddress; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for ControllerAddress", value, value.ToString());
				}
				//to store the undo info
                AddHistory("ControllerAddress", m_ControllerAddress);
				switch(this.State){
					case objstate.Loaded:
						if(m_ControllerAddress != value){
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
				m_ControllerAddress = value;
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual string DocumentName
		{
			get { return m_DocumentName; }
			set	
			{
				if ( value != null)
				if( value.Length > 100){
					throw new ArgumentOutOfRangeException("Invalid value for DocumentName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("DocumentName", m_DocumentName);
				switch(this.State){
					case objstate.Loaded:
						if(m_DocumentName != value){
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
				m_DocumentName = value;
			}

		}		
		/// <summary>
		/// 
		/// </summary>		
		public virtual string SheetName
		{
			get { return m_SheetName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SheetName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("SheetName", m_SheetName);
				switch(this.State){
					case objstate.Loaded:
						if(m_SheetName != value){
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
				m_SheetName = value;
			}

		}		
		/// <summary>
		/// 交叉引用名字
		/// </summary>		
		public virtual string RefName
		{
			get { return m_RefName; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for RefName", value, value.ToString());
				}
				//to store the undo info
                AddHistory("RefName", m_RefName);
				switch(this.State){
					case objstate.Loaded:
						if(m_RefName != value){
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
				m_RefName = value;
			}

		}		
		/// <summary>
		/// 源交叉引用的ID
		/// </summary>		
		public virtual string SrcRefID
		{
			get { return m_SrcRefID; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for SrcRefID", value, value.ToString());
				}
				//to store the undo info
                AddHistory("SrcRefID", m_SrcRefID);
				switch(this.State){
					case objstate.Loaded:
						if(m_SrcRefID != value){
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
				m_SrcRefID = value;
			}

		}		
		/// <summary>
		/// 如果不是IO则为空
		/// </summary>		
		public virtual string FunctionCode
		{
			get { return m_FunctionCode; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for FunctionCode", value, value.ToString());
				}
				//to store the undo info
                AddHistory("FunctionCode", m_FunctionCode);
				switch(this.State){
					case objstate.Loaded:
						if(m_FunctionCode != value){
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
				m_FunctionCode = value;
			}

		}		
		/// <summary>
		/// FC
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
		/// 如果是IO Connector则是与该交叉引用连接的引脚名字
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
		/// 如果是IO Connector该引脚的地址
		/// </summary>		
		public virtual string Address
		{
			get { return m_Address; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				}
				//to store the undo info
                AddHistory("Address", m_Address);
				switch(this.State){
					case objstate.Loaded:
						if(m_Address != value){
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
				m_Address = value;
			}

		}		
		/// <summary>
		/// 位置
		/// </summary>		
		public virtual string X_Y
		{
			get { return m_X_Y; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for X_Y", value, value.ToString());
				}
				//to store the undo info
                AddHistory("X_Y", m_X_Y);
				switch(this.State){
					case objstate.Loaded:
						if(m_X_Y != value){
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
				m_X_Y = value;
			}

		}		
		/// <summary>
		/// 网络标号
		/// </summary>		
		public virtual string NetworkID
		{
			get { return m_NetworkID; }
			set	
			{
				if ( value != null)
				if( value.Length > 50){
					throw new ArgumentOutOfRangeException("Invalid value for NetworkID", value, value.ToString());
				}
				//to store the undo info
                AddHistory("NetworkID", m_NetworkID);
				switch(this.State){
					case objstate.Loaded:
						if(m_NetworkID != value){
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
				m_NetworkID = value;
			}

		}		
	

	}
}

