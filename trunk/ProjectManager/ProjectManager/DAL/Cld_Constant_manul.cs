using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using DejaVu;
using DejaVu.Collections.Generic;
using System.Diagnostics;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_Constant
    {
        /// <summary>
        /// 比较两个Cld_Constant，id须相等，否则抛出异常
        /// </summary>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public virtual bool Compare(Cld_Constant arg1) {
            if (this.ID != arg1.ID) {
                throw new Exception("the id should be equal");
            }
            if (this.Name != arg1.Name || this.Prj_Controller_ID != arg1.Prj_Controller_ID
                || this.Prj_Document_ID != arg1.Prj_Document_ID || this.Prj_Sheet_ID != arg1.Prj_Sheet_ID
                || this.X_Y != arg1.X_Y
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
