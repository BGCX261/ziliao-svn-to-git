using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using DejaVu;
using DejaVu.Collections.Generic;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_Signal
    {
        public virtual bool Compare(Cld_Signal s) {
            if (this.ID != s.ID) {
                throw new Exception("id should be equal");
            }
            if (this.Name != s.Name || this.SignalType != s.SignalType
                || this.EntityBelongTo != s.EntityBelongTo || this.Data != s.Data
                || this.Prj_Controller_ID != s.Prj_Controller_ID
                || this.Prj_Document_ID != s.Prj_Document_ID
                || this.Prj_Sheet_ID != s.Prj_Sheet_ID)
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}