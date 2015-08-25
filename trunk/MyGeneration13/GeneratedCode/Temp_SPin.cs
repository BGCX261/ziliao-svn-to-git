using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Temp_SPin
	{

		#region Private Members

		private string m_objectid; 
		private string m_controlleraddress; 
		private string m_functionname; 
		private string m_pinname; 		
		#endregion

		#region Constuctor

		public Temp_SPin()
		{
			m_objectid = String.Empty; 
			m_controlleraddress = String.Empty; 
			m_functionname = String.Empty; 
			m_pinname = String.Empty; 
		}

		#region Public Properties
			
		public string Objectid
		{
			get { return m_objectid; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
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
			
		public string PinName
		{
			get { return m_pinname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinName", value, value.ToString());
				
				m_pinname = value;
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

		
		private string m_FunctionName
		{
			get { return m_functionname; }
			set { m_functionname = value; }
		}

		
		private string m_PinName
		{
			get { return m_pinname; }
			set { m_pinname = value; }
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
			Temp_SPin castObj = (Temp_SPin)obj; 
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