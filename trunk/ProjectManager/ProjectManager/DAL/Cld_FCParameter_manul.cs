using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using DejaVu;
using DejaVu.Collections.Generic;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCParameter
    {
        public virtual bool Compare(Cld_FCParameter p) {
            if (this.ID != p.ID) {
                throw new Exception("id should be equal");
            }
            if (this.Name != p.Name || this.PValue != p.PValue
                || this.Cld_FCBlock_ID != p.Cld_FCBlock_ID || this.Prj_Controller_ID != p.Prj_Controller_ID
                || this.Prj_Document_ID != p.Prj_Document_ID || this.Prj_Sheet_ID != p.Prj_Sheet_ID
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
