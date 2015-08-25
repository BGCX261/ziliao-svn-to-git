using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Meta_DataTypeDetail
	{

		#region Private Members

		private string m_typename; 
		private IList m_MetaDataTypeDetailList; 
		private string m_member; 
		private string m_systemtype; 
		private int m_index; 		
		#endregion

		#region Constuctor

		public Meta_DataTypeDetail()
		{
			m_typename = String.Empty; 
			m_MetaDataTypeDetailList = new ArrayList(); 
			m_member = String.Empty; 
			m_systemtype = String.Empty; 
			m_index = 0; 
		}

		#region Public Properties
			
		public string TypeName
		{
			get { return m_typename; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
				
				m_typename = value;
			}
		}
			
		public MetaDataTypeDetail[] MetaDataTypeDetail
		{
			get
			{

				ArrayList readonlyarray = ArrayList.ReadOnly(new ArrayList(m_MetaDataTypeDetailList));
				return (MetaDataTypeDetail[]) readonlyarray.ToArray(typeof(MetaDataTypeDetail));
			}
		}
						
		public string Member
		{
			get { return m_member; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Member", value, value.ToString());
				
				m_member = value;
			}
		}
			
		public string SystemType
		{
			get { return m_systemtype; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SystemType", value, value.ToString());
				
				m_systemtype = value;
			}
		}
			
		public int Index
		{
			get { return m_index; }
			set
			{
				m_index = value;
			}

		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private string m_TypeName
		{
			get { return m_typename; }
			set { m_typename = value; }
		}

		
		private string m_Member
		{
			get { return m_member; }
			set { m_member = value; }
		}

		
		private string m_SystemType
		{
			get { return m_systemtype; }
			set { m_systemtype = value; }
		}

		
		private int m_Index
		{
			get { return m_index; }
			set { m_index = value; }
		}

		#endregion // Internal Accessors for NHibernate 

		#region Public Functions

		public void AddMetaDataTypeDetail(MetaDataTypeDetail dados)
		{
			#region Sanity Check
			if (dados == null)
				throw new ArgumentNullException("dados", "Parâmetro não pode ser nulo");
			#endregion
			m_MetaDataTypeDetailList.Add(dados);
		}
		

		#endregion //Public Functions

		#region Equals And HashCode Overrides
		/// <summary>
		/// local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals( object obj )
		{
			if( this == obj ) return true;
			if( ( obj == null ) || ( obj.GetType() != this.GetType() ) ) return false;
			Meta_DataTypeDetail castObj = (Meta_DataTypeDetail)obj; 
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