using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Meta_DataType
	{

		#region Private Members

		private string m_datatypename; 
		private IList m_MetaDataTypeDetailList; 
		private IList m_MetaFcDetailList; 
		private int m_index; 
		private string m_class; 
		private string m_systemtype; 
		private short m_length; 
		private string m_function; 		
		#endregion

		#region Constuctor

		public Meta_DataType()
		{
			m_datatypename = String.Empty; 
			m_MetaDataTypeDetailList = new ArrayList(); 
			m_MetaFcDetailList = new ArrayList(); 
			m_index = 0; 
			m_class = String.Empty; 
			m_systemtype = String.Empty; 
			m_length = 0; 
			m_function = String.Empty; 
		}

		#region Public Properties
			
		public string DataTypeName
		{
			get { return m_datatypename; }

			set	
			{	
				if(  value != null &&  value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for DataTypeName", value, value.ToString());
				
				m_datatypename = value;
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
						
		public MetaFcDetail[] MetaFcDetail
		{
			get
			{

				ArrayList readonlyarray = ArrayList.ReadOnly(new ArrayList(m_MetaFcDetailList));
				return (MetaFcDetail[]) readonlyarray.ToArray(typeof(MetaFcDetail));
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
			
		public string Class
		{
			get { return m_class; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Class", value, value.ToString());
				
				m_class = value;
			}
		}
			
		public string SystemType
		{
			get { return m_systemtype; }

			set	
			{	
				if(  value != null &&  value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for SystemType", value, value.ToString());
				
				m_systemtype = value;
			}
		}
			
		public short Length
		{
			get { return m_length; }
			set
			{
				m_length = value;
			}

		}
			
		public string Function
		{
			get { return m_function; }

			set	
			{	
				if(  value != null &&  value.Length > 0)
					throw new ArgumentOutOfRangeException("Invalid value for Function", value, value.ToString());
				
				m_function = value;
			}
		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private string m_DataTypeName
		{
			get { return m_datatypename; }
			set { m_datatypename = value; }
		}

		
		private int m_Index
		{
			get { return m_index; }
			set { m_index = value; }
		}

		
		private string m_Class
		{
			get { return m_class; }
			set { m_class = value; }
		}

		
		private string m_SystemType
		{
			get { return m_systemtype; }
			set { m_systemtype = value; }
		}

		
		private short m_Length
		{
			get { return m_length; }
			set { m_length = value; }
		}

		
		private string m_Function
		{
			get { return m_function; }
			set { m_function = value; }
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
		

		public void AddMetaFcDetail(MetaFcDetail dados)
		{
			#region Sanity Check
			if (dados == null)
				throw new ArgumentNullException("dados", "Parâmetro não pode ser nulo");
			#endregion
			m_MetaFcDetailList.Add(dados);
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
			Meta_DataType castObj = (Meta_DataType)obj; 
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
