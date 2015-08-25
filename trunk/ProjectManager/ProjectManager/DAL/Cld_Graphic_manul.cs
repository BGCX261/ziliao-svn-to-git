using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using DejaVu;
using DejaVu.Collections.Generic;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_Graphic
    {
        public virtual bool Compare(Cld_Graphic g) {
            if (this.ID != g.ID) {
                throw new Exception("id should be equal");
            }
            if (this.Type != g.Type || this.Layer != g.Layer
                || this.Data != g.Data || this.Prj_Controller_ID != g.Prj_Controller_ID
                || this.Prj_Document_ID != g.Prj_Document_ID || this.Prj_Sheet_ID != g.Prj_Sheet_ID
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
