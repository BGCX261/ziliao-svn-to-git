using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_Sheet
    {
        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_Const对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_Constant New_Cld_Const()
        {
            Cld_Constant result = new Cld_Constant();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.ID;
            return result;
        }

        /// <summary>
        /// 将给定的Cld_Const和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_Constant to_add)
        {
            this.Cld_Constant_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
        }



        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_Signal对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_Signal New_Cld_Signal()
        {
            Cld_Signal result = new Cld_Signal();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.ID;
            return result;
        }
        /// <summary>
        /// 将给定的Cld_Signal和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_Signal to_add)
        {
            this.Cld_Constant_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
        }

        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_FCBlock对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCBlock New_Cld_FCBlock()
        {
            Cld_FCBlock result = new Cld_FCBlock();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.ID;
            return result;
        }
        /// <summary>
        /// 将给定的Cld_FCBlock和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_FCBlock to_add)
        {
            this.Cld_Constant_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
        }

        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_Graphic对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_Graphic New_Cld_Graphic()
        {
            Cld_Graphic result = new Cld_Graphic();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.ID;
            return result;
        }
        /// <summary>
        /// 将给定的Cld_Graphic和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_Graphic to_add)
        {
            this.Cld_Constant_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
        }
    }
}
