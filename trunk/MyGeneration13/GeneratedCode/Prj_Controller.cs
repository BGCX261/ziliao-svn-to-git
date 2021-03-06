using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Prj_Controller
	{

		#region Private Members

		private string m_controlleraddress; 
		private IList m_PrjDocumentList; 
		private string m_controllername; 
		private DateTime m_createtime; 
		private DateTime m_modifytime; 
		private string m_networkaddress; 
		private PrjUnit m_unitaddress; 
		private string m_description; 
		private string m_version; 
		private string m_translatorresult; 		
		#endregion

		#region Constuctor

		public Prj_Controller()
		{
			m_controlleraddress = String.Empty; 
			m_PrjDocumentList = new ArrayList(); 
			m_controllername = String.Empty; 
			m_createtime = DateTime.MinValue; 
			m_modifytime = DateTime.MinValue; 
			m_networkaddress = String.Empty; 
			m_unitaddress = new PrjUnit(); 
			m_description = String.Empty; 
			m_version = String.Empty; 
			m_translatorresult = String.Empty; 
		}

		#region Public Properties
			
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
			
		public PrjDocument[] PrjDocument
		{
			get
			{

				ArrayList readonlyarray = ArrayList.ReadOnly(new ArrayList(m_PrjDocumentList));
				return (PrjDocument[]) readonlyarray.ToArray(typeof(PrjDocument));
			}
		}
						
		public string ControllerName
		{
			get { return m_controllername; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ControllerName", value, value.ToString());
				
				m_controllername = value;
			}
		}
			
		public DateTime CreateTime
		{
			get { return m_createtime; }
			set
			{
				m_createtime = value;
			}

		}
			
		public DateTime ModifyTime
		{
			get { return m_modifytime; }
			set
			{
				m_modifytime = value;
			}

		}
			
		public string NetworkAddress
		{
			get { return m_networkaddress; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NetworkAddress", value, value.ToString());
				
				m_networkaddress = value;
			}
		}
			
		public PrjUnit UnitAddress
		{
			get { return m_unitaddress; }
			set
			{
				m_unitaddress = value;
			}

		}
			
		public string Description
		{
			get { return m_description; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
				
				m_description = value;
			}
		}
			
		public string Version
		{
			get { return m_version; }

			set	
			{	
				if(  value != null &&  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Version", value, value.ToString());
				
				m_version = value;
			}
		}
			
		public string TranslatorResult
		{
			get { return m_translatorresult; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TranslatorResult", value, value.ToString());
				
				m_translatorresult = value;
			}
		}
			
				
		#endregion 

		#region Private Accessors for NHibernate
		
		
		private string m_ControllerAddress
		{
			get { return m_controlleraddress; }
			set { m_controlleraddress = value; }
		}

		
		private string m_ControllerName
		{
			get { return m_controllername; }
			set { m_controllername = value; }
		}

		
		private DateTime m_CreateTime
		{
			get { return m_createtime; }
			set { m_createtime = value; }
		}

		
		private DateTime m_ModifyTime
		{
			get { return m_modifytime; }
			set { m_modifytime = value; }
		}

		
		private string m_NetworkAddress
		{
			get { return m_networkaddress; }
			set { m_networkaddress = value; }
		}

		
		private PrjUnit m_UnitAddress
		{
			get { return m_unitaddress; }
			set { m_unitaddress = value; }
		}

		
		private string m_Description
		{
			get { return m_description; }
			set { m_description = value; }
		}

		
		private string m_Version
		{
			get { return m_version; }
			set { m_version = value; }
		}

		
		private string m_TranslatorResult
		{
			get { return m_translatorresult; }
			set { m_translatorresult = value; }
		}

		#endregion // Internal Accessors for NHibernate 

		#region Public Functions

		public void AddPrjDocument(PrjDocument dados)
		{
			#region Sanity Check
			if (dados == null)
				throw new ArgumentNullException("dados", "Parâmetro não pode ser nulo");
			#endregion
			m_PrjDocumentList.Add(dados);
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
			Prj_Controller castObj = (Prj_Controller)obj; 
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
