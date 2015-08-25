using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Prj_Document New_Prj_Document()
        {
            Prj_Document result = new Prj_Document();
            result.Prj_Controller_ID = this.ID;
            result.Prj_Controller = this;
            this.Prj_Document_List.Add(result);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Prj_Document to_add)
        {
            this.Prj_Document_List.Add(to_add);
            to_add.Prj_Controller_ID = this.ID;
            to_add.Prj_Controller = this;
        }

        public virtual bool Compare(Prj_Controller con) {
            if (this.ID != con.ID) {
                throw new Exception("id should be equal");
            }
            if (this.ControllerAddress != con.ControllerAddress || this.ControllerName != con.ControllerName
                || this.CreateTime != con.CreateTime || this.ModifyTime != con.ModifyTime
                || this.Description != con.Description || this.Version != con.Version
                || this.TranslatorResult != con.TranslatorResult || this.Prj_Unit_ID != con.Prj_Unit_ID
                || this.Sequence != con.Sequence
                )
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
