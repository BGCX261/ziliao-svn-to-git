// Entity.cs created with MonoDevelop
// User: root at 1:40 PM 8/21/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace TDK.Core.Logic.URdoLib
{
	public abstract class Entity 
	{
		private ModificationHistory mHistory;
		protected bool mBeingUndone; // flag to denote whether the undo method has been called
		public Entity()
		{
			mHistory = new ModificationHistory(this);
			mBeingUndone = false;
		}

		/// <summary>
		/// Add to the undo stack. Pass the Property name and the value
		/// from the Property Set method
		/// 
		/// This method delegates the call to the mHistory.Store method 
		/// IF the Undo flag is not set.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="Value"></param>
		protected virtual void AddHistory(string propertyName, object Value)
		{
			if (!mBeingUndone)
				mHistory.Store(propertyName, Value);
		}

		#region Modification History
		public virtual bool CanUndo
		{
			get
			{
				return mHistory.CanUndo;
			}
		}
		public virtual bool CanRedo
		{
			get
			{
				return mHistory.CanRedo;
			}
		}
		public virtual void Redo()
		{
			if (mHistory.CanRedo)
			{
				mBeingUndone =true;
				mHistory.Redo();
				mBeingUndone=false;
			}
		}

		/// <summary>
		/// Set the undo flag, call the undo operation of the 
		/// ModificationHistory object and then reset the undo flag
		/// </summary>
		public virtual void Undo()
		{
			if (mHistory.CanUndo)
			{
				mBeingUndone = true;
				mHistory.Undo();
				mBeingUndone = false;
			}
		}
		#endregion

	}
}

