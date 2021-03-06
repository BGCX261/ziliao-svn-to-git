using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Temp_AlgPoint
	{

		#region Private Members

		private string m_algname; 
		private string m_algpointx; 
		private string m_algpointy; 		
		#endregion

		#region Constuctor

		public Temp_AlgPoint()
		{
			m_algname = String.Empty; 
			m_algpointx = String.Empty; 
			m_algpointy = String.Empty; 
		}

		#region Public Properties
			
		public string AlgName
		{
			get { return m_algname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AlgName", value, value.ToString());
				
				m_algname = value;
			}
		}
			
		public string AlgPointx
		{
			get { return m_algpointx; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AlgPointx", value, value.ToString());
				
				m_algpointx = value;
			}
		}
			
		public string AlgPointy
		{
			get { return m_algpointy; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AlgPointy", value, value.ToString());
				
				m_algpointy = value;
			}
		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private string m_AlgName
		{
			get { return m_algname; }
			set { m_algname = value; }
		}

		
		private string m_AlgPointX
		{
			get { return m_algpointx; }
			set { m_algpointx = value; }
		}

		
		private string m_AlgPointY
		{
			get { return m_algpointy; }
			set { m_algpointy = value; }
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
			Temp_AlgPoint castObj = (Temp_AlgPoint)obj; 
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
