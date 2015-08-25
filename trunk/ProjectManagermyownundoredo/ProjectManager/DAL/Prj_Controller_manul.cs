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
        }
    }
}
