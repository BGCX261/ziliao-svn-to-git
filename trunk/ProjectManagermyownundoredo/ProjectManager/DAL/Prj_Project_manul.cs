using System;
using System.Collections.Generic;
using System.Text;


namespace TDK.Core.Logic.DAL
{
    public partial class Prj_Project
    {
        /// <summary>
        /// 返回 此Prj_Project下的Network
        /// </summary>
        /// <returns></returns>
        public virtual Prj_Network New_Prj_Network()
        {
            Prj_Network result = new Prj_Network();
            result.Prj_Project_ID = this.ID;
            return result;
        }
        /// <summary>
        /// 将给定的Prj_Network和当前的Project关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Prj_Network to_add)
        {
            this.Prj_Network_List.Add(to_add);
            to_add.Prj_Project_ID = this.ID;
        }
    }
}
