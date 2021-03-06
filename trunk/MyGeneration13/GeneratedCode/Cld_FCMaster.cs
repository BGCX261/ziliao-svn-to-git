using System;
using System.Collections;


namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class Cld_FCMaster
	{

		#region Private Members

		private string m_objectid; 
		private IList m_CldFcDetailList; 
		private string m_controlleraddress; 
		private string m_documentname; 
		private string m_sheetname; 
		private string m_algname; 
		private string m_algorder; 
		private string m_functionname; 
		private string m_x_y; 
		private string m_symbolname; 
		private string m_descrp; 
		private string m_period; 
		private string m_phase; 
		private string m_loopid; 		
		#endregion

		#region Constuctor

		public Cld_FCMaster()
		{
			m_objectid = String.Empty; 
			m_CldFcDetailList = new ArrayList(); 
			m_controlleraddress = String.Empty; 
			m_documentname = String.Empty; 
			m_sheetname = String.Empty; 
			m_algname = String.Empty; 
			m_algorder = String.Empty; 
			m_functionname = String.Empty; 
			m_x_y = String.Empty; 
			m_symbolname = String.Empty; 
			m_descrp = String.Empty; 
			m_period = String.Empty; 
			m_phase = String.Empty; 
			m_loopid = String.Empty; 
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
			
		public CldFcDetail[] CldFcDetail
		{
			get
			{

				ArrayList readonlyarray = ArrayList.ReadOnly(new ArrayList(m_CldFcDetailList));
				return (CldFcDetail[]) readonlyarray.ToArray(typeof(CldFcDetail));
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
			
		public string AlgName
		{
			get { return m_algname; }

			set	
			{	
				if(  value != null &&  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for AlgName", value, value.ToString());
				
				m_algname = value;
			}
		}
			
		public string AlgOrder
		{
			get { return m_algorder; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AlgOrder", value, value.ToString());
				
				m_algorder = value;
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
			
		public string SymbolName
		{
			get { return m_symbolname; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SymbolName", value, value.ToString());
				
				m_symbolname = value;
			}
		}
			
		public string Descrp
		{
			get { return m_descrp; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Descrp", value, value.ToString());
				
				m_descrp = value;
			}
		}
			
		public string Period
		{
			get { return m_period; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Period", value, value.ToString());
				
				m_period = value;
			}
		}
			
		public string Phase
		{
			get { return m_phase; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Phase", value, value.ToString());
				
				m_phase = value;
			}
		}
			
		public string Loopid
		{
			get { return m_loopid; }

			set	
			{	
				if(  value != null &&  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Loopid", value, value.ToString());
				
				m_loopid = value;
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

		
		private string m_AlgName
		{
			get { return m_algname; }
			set { m_algname = value; }
		}

		
		private string m_AlgOrder
		{
			get { return m_algorder; }
			set { m_algorder = value; }
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

		
		private string m_SymbolName
		{
			get { return m_symbolname; }
			set { m_symbolname = value; }
		}

		
		private string m_DESCRP
		{
			get { return m_descrp; }
			set { m_descrp = value; }
		}

		
		private string m_PERIOD
		{
			get { return m_period; }
			set { m_period = value; }
		}

		
		private string m_PHASE
		{
			get { return m_phase; }
			set { m_phase = value; }
		}

		
		private string m_LOOPID
		{
			get { return m_loopid; }
			set { m_loopid = value; }
		}

		#endregion // Internal Accessors for NHibernate 

		#region Public Functions

		public void AddCldFcDetail(CldFcDetail dados)
		{
			#region Sanity Check
			if (dados == null)
				throw new ArgumentNullException("dados", "Parâmetro não pode ser nulo");
			#endregion
			m_CldFcDetailList.Add(dados);
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
			Cld_FCMaster castObj = (Cld_FCMaster)obj; 
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
