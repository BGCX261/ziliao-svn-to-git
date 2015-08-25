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
            result.Prj_Document_ID = this.ID;
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
            to_add.Prj_Document_ID = this.ID;
        }
    }
}
