using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_Network
    {
        /// <summary>
        /// 返回 此Prj_Network下的Unit
        /// </summary>
        /// <returns></returns>
        public virtual Prj_Unit New_Prj_Unit()
        {
            Prj_Unit result = new Prj_Unit();
            result.Prj_Network_ID = this.ID;
            return result;
        }
        /// <summary>
        /// 将给定的Prj_Unit和当前的Network关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Prj_Unit to_add)
        {
            this.Prj_Unit_List.Add(to_add);
            to_add.Prj_Network_ID = this.ID;
        }
    }
}
