using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Meta_FCMaster
	{

		#region Private Members

		private string m_functionname; 
		private IList m_MetaFcDetailList; 
		private int m_functioncode; 
		private string m_description; 
		private string m_function; 
		private byte m_diag; 
		private byte m_inputcount; 
		private byte m_speccount; 
		private byte m_outputcount; 
		private int m_internalcount; 
		private short m_fclength; 
		private string m_type; 		
		#endregion

		#region Constuctor

		public Meta_FCMaster()
		{
			m_functionname = String.Empty; 
			m_MetaFcDetailList = new ArrayList(); 
			m_functioncode = 0; 
			m_description = String.Empty; 
			m_function = String.Empty; 
			m_diag = new byte(); 
			m_inputcount = new byte(); 
			m_speccount = new byte(); 
			m_outputcount = new byte(); 
			m_internalcount = 0; 
			m_fclength = 0; 
			m_type = String.Empty; 
		}

		#region Public Properties
			
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
			
		public MetaFcDetail[] MetaFcDetail
		{
			get
			{

				ArrayList readonlyarray = ArrayList.ReadOnly(new ArrayList(m_MetaFcDetailList));
				return (MetaFcDetail[]) readonlyarray.ToArray(typeof(MetaFcDetail));
			}
		}
						
		public int FunctionCode
		{
			get { return m_functioncode; }
			set
			{
				m_functioncode = value;
			}

		}
			
		public string Description
		{
			get { return m_description; }

			set	
			{	
				if(  value != null &&  value.Length > 255)
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				
				m_description = value;
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
			
		public byte Diag
		{
			get { return m_diag; }

			set	
			{	
				if(  value != null &&  value.Length > 0)
					throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
				
				m_diag = value;
			}

		}
			
		public byte InputCount
		{
			get { return m_inputcount; }

			set	
			{	
				if(  value != null &&  value.Length > 0)
					throw new ArgumentOutOfRangeException("Invalid value for InputCount", value, value.ToString());
				
				m_inputcount = value;
			}

		}
			
		public byte SpecCount
		{
			get { return m_speccount; }

			set	
			{	
				if(  value != null &&  value.Length > 0)
					throw new ArgumentOutOfRangeException("Invalid value for SpecCount", value, value.ToString());
				
				m_speccount = value;
			}

		}
			
		public byte OutPutCount
		{
			get { return m_outputcount; }

			set	
			{	
				if(  value != null &&  value.Length > 0)
					throw new ArgumentOutOfRangeException("Invalid value for OutPutCount", value, value.ToString());
				
				m_outputcount = value;
			}

		}
			
		public int InternalCount
		{
			get { return m_internalcount; }
			set
			{
				m_internalcount = value;
			}

		}
			
		public short FcLength
		{
			get { return m_fclength; }
			set
			{
				m_fclength = value;
			}

		}
			
		public string Type
		{
			get { return m_type; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Type", value, value.ToString());
				
				m_type = value;
			}
		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private string m_FunctionName
		{
			get { return m_functionname; }
			set { m_functionname = value; }
		}

		
		private int m_FunctionCode
		{
			get { return m_functioncode; }
			set { m_functioncode = value; }
		}

		
		private string m_Description
		{
			get { return m_description; }
			set { m_description = value; }
		}

		
		private string m_Function
		{
			get { return m_function; }
			set { m_function = value; }
		}

		
		private byte m_DIAG
		{
			get { return m_diag; }
			set { m_diag = value; }
		}

		
		private byte m_InputCount
		{
			get { return m_inputcount; }
			set { m_inputcount = value; }
		}

		
		private byte m_SpecCount
		{
			get { return m_speccount; }
			set { m_speccount = value; }
		}

		
		private byte m_OutPutCount
		{
			get { return m_outputcount; }
			set { m_outputcount = value; }
		}

		
		private int m_InternalCount
		{
			get { return m_internalcount; }
			set { m_internalcount = value; }
		}

		
		private short m_FCLength
		{
			get { return m_fclength; }
			set { m_fclength = value; }
		}

		
		private string m_Type
		{
			get { return m_type; }
			set { m_type = value; }
		}

		#endregion // Internal Accessors for NHibernate 

		#region Public Functions

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
			Meta_FCMaster castObj = (Meta_FCMaster)obj; 
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