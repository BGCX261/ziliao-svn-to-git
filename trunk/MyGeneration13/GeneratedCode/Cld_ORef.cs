using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Cld_ORef
	{

		#region Private Members

		private string m_objectid; 
		private string m_controlleraddress; 
		private string m_documentname; 
		private string m_sheetname; 
		private string m_refname; 
		private string m_srcblockid; 
		private string m_srcpinname; 
		private string m_srcaddress; 
		private string m_functionname; 
		private string m_x_y; 
		private string m_networkid; 		
		#endregion

		#region Constuctor

		public Cld_ORef()
		{
			m_objectid = String.Empty; 
			m_controlleraddress = String.Empty; 
			m_documentname = String.Empty; 
			m_sheetname = String.Empty; 
			m_refname = String.Empty; 
			m_srcblockid = String.Empty; 
			m_srcpinname = String.Empty; 
			m_srcaddress = String.Empty; 
			m_functionname = String.Empty; 
			m_x_y = String.Empty; 
			m_networkid = String.Empty; 
		}

		#region Public Properties
			
		public string Objectid
		{
			get { return m_objectid; }

			set	
			{	
				if(  value != null &&  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Objectid", value, value.ToString());
				
				m_objectid = value;
			}
		}
			
		public string ControllerAddress
		{
			get { return m_controlleraddress; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ControllerAddress", value, value.ToString());
				
				m_controlleraddress = value;
			}
		}
			
		public string DocumentName
		{
			get { return m_documentname; }

			set	
			{	
				if(  value != null &&  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for DocumentName", value, value.ToString());
				
				m_documentname = value;
			}
		}
			
		public string SheetName
		{
			get { return m_sheetname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SheetName", value, value.ToString());
				
				m_sheetname = value;
			}
		}
			
		public string RefName
		{
			get { return m_refname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefName", value, value.ToString());
				
				m_refname = value;
			}
		}
			
		public string SrcBlockid
		{
			get { return m_srcblockid; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SrcBlockid", value, value.ToString());
				
				m_srcblockid = value;
			}
		}
			
		public string SrcPinName
		{
			get { return m_srcpinname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SrcPinName", value, value.ToString());
				
				m_srcpinname = value;
			}
		}
			
		public string SrcAddress
		{
			get { return m_srcaddress; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SrcAddress", value, value.ToString());
				
				m_srcaddress = value;
			}
		}
			
		public string FunctionName
		{
			get { return m_functionname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for FunctionName", value, value.ToString());
				
				m_functionname = value;
			}
		}
			
		public string XY
		{
			get { return m_x_y; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for XY", value, value.ToString());
				
				m_x_y = value;
			}
		}
			
		public string Networkid
		{
			get { return m_networkid; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Networkid", value, value.ToString());
				
				m_networkid = value;
			}
		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private string m_ObjectID
		{
			get { return m_objectid; }
			set { m_objectid = value; }
		}

		
		private string m_ControllerAddress
		{
			get { return m_controlleraddress; }
			set { m_controlleraddress = value; }
		}

		
		private string m_DocumentName
		{
			get { return m_documentname; }
			set { m_documentname = value; }
		}

		
		private string m_SheetName
		{
			get { return m_sheetname; }
			set { m_sheetname = value; }
		}

		
		private string m_RefName
		{
			get { return m_refname; }
			set { m_refname = value; }
		}

		
		private string m_SrcBlockID
		{
			get { return m_srcblockid; }
			set { m_srcblockid = value; }
		}

		
		private string m_SrcPinName
		{
			get { return m_srcpinname; }
			set { m_srcpinname = value; }
		}

		
		private string m_SrcAddress
		{
			get { return m_srcaddress; }
			set { m_srcaddress = value; }
		}

		
		private string m_FunctionName
		{
			get { return m_functionname; }
			set { m_functionname = value; }
		}

		
		private string m_X_Y
		{
			get { return m_x_y; }
			set { m_x_y = value; }
		}

		
		private string m_NetworkID
		{
			get { return m_networkid; }
			set { m_networkid = value; }
		}

		#endregion // Internal Accessors for NHibernate 

		#region Public Functions

		#endregion //Public Functions

		#region Equals And HashCode Overrides
		/// <summary>
		/// local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals( object obj )
		{
			if( this == obj ) return true;
			if( ( obj == null ) || ( obj.GetType() != this.GetType() ) ) return false;
			Cld_ORef castObj = (Cld_ORef)obj; 
			return castObj.GetHashCode() == this.GetHashCode();
		}
		
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return this.GetType().FullName.GetHashCode();
				
		}
		#endregion
			}
}
