using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_Document
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Prj_Sheet New_Prj_Sheet()
        {
            Prj_Sheet result = new Prj_Sheet();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.ID;
            result.Prj_Document = this;
            this.Prj_Sheet_List.Add(result);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Prj_Sheet to_add)
        {
            this.Prj_Sheet_List.Add(to_add);
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
            to_add.Prj_Document_ID = this.ID;
            to_add.Prj_Document = this;
        }

        public virtual bool Compare(Prj_Document doc) {
            if (this.ID != doc.ID) {
                throw new Exception("id should be equal");
            }
            if (this.DocumentName != doc.DocumentName || this.DocumentCaption != doc.DocumentCaption
                || this.CreateTime != doc.CreateTime || this.ModifyTime != doc.ModifyTime
                || this.Sequence != doc.Sequence || this.Type != doc.Type || this.TranslatorResult != doc.TranslatorResult
                || this.changed != doc.changed || this.Prj_Controller_ID != doc.Prj_Controller_ID
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
