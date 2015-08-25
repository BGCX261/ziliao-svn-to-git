using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Meta_SymbolDetail
	{

		#region Private Members

		private MetaSymbolMaster m_symbolid; 
		private string m_elementtype; 
		private string m_elementdata; 
		private int m_dynamictype; 		
		#endregion

		#region Constuctor

		public Meta_SymbolDetail()
		{
			m_symbolid = new MetaSymbolMaster(); 
			m_elementtype = String.Empty; 
			m_elementdata = String.Empty; 
			m_dynamictype = 0; 
		}

		#region Public Properties
			
		public MetaSymbolMaster Symbolid
		{
			get { return m_symbolid; }
			set
			{
				m_symbolid = value;
			}

		}
			
		public string ElementType
		{
			get { return m_elementtype; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ElementType", value, value.ToString());
				
				m_elementtype = value;
			}
		}
			
		public string ElementData
		{
			get { return m_elementdata; }

			set	
			{	
				if(  value != null &&  value.Length > 0)
					throw new ArgumentOutOfRangeException("Invalid value for ElementData", value, value.ToString());
				
				m_elementdata = value;
			}
		}
			
		public int DynamicType
		{
			get { return m_dynamictype; }
			set
			{
				m_dynamictype = value;
			}

		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private MetaSymbolMaster m_SymbolID
		{
			get { return m_symbolid; }
			set { m_symbolid = value; }
		}

		
		private string m_ElementType
		{
			get { return m_elementtype; }
			set { m_elementtype = value; }
		}

		
		private string m_ElementData
		{
			get { return m_elementdata; }
			set { m_elementdata = value; }
		}

		
		private int m_DynamicType
		{
			get { return m_dynamictype; }
			set { m_dynamictype = value; }
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
			Meta_SymbolDetail castObj = (Meta_SymbolDetail)obj; 
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