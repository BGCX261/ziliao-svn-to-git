using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_Unit
    {
        /// <summary>
        /// 返回 此Prj_Unit下的Controller
        /// </summary>
        /// <returns></returns>
        public virtual Prj_Controller New_Prj_Controller()
        {
            Prj_Controller result = new Prj_Controller();
            result.Prj_Unit_ID = this.ID;
            return result;
        }
        /// <summary>
        /// 将给定的Prj_COntroller和当前的Unit关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Prj_Controller to_add)
        {
            this.Prj_Controller_List.Add(to_add);
            to_add.Prj_Unit_ID = this.ID;
        }
    }
}
