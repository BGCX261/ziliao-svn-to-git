// PropertyValue.cs created with MonoDevelop
// User: root at 1:30 PM 8/21/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace TDK.Core.Logic.URdoLib
{
	/// <summary>
	/// This class will hold the name and value of a property
	/// prior to the value being changed.
	/// </summary>
	[Serializable]
	public class PropertyValue
	{
		private string PropName;
		private object PropVal;

		public PropertyValue(string propName, object propVal)
		{
			PropName = propName;
			PropVal = propVal;
		}
		public string PropertyName
		{
			get
			{
				return PropName;
			}
			set
			{
				PropName = value;
			}
		}
		public object Value
		{
			get
			{
				return PropVal;
			}
			set
			{
				PropVal = value;
			}
		}
	}
}

