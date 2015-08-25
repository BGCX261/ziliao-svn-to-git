using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.ID;
            result.Prj_Sheet = this;
            this.Cld_Constant_List.Add(result);
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
            to_add.Prj_Sheet = this;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
        }



        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_Signal对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_Signal New_Cld_Signal()
        {
            Cld_Signal result = new Cld_Signal();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.ID;
            result.Prj_Sheet = this;
            this.Cld_Signal_List.Add(result);
            return result;
        }
        /// <summary>
        /// 将给定的Cld_Signal和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_Signal to_add)
        {
            this.Cld_Signal_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            //to_add.Prj_Sheet = this;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            //to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            //to_add.Prj_Controller = this.Prj_Controller;
        }

        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_FCBlock对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCBlock New_Cld_FCBlock()
        {
            Cld_FCBlock result = new Cld_FCBlock();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            //result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.Prj_Document_ID;
            //result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.ID;
            //result.Prj_Sheet = this;
            this.Cld_FCBlock_List.Add(result);
            return result;
        }
        /// <summary>
        /// 将给定的Cld_FCBlock和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_FCBlock to_add)
        {
            this.Cld_FCBlock_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            to_add.Prj_Sheet = this;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
        }

        /// <summary>
        /// 返回 此Sheet下面的一个新的Cld_Graphic对象
        /// </summary>
        /// <returns></returns>
        public virtual Cld_Graphic New_Cld_Graphic()
        {
            Cld_Graphic result = new Cld_Graphic();
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.ID;
            result.Prj_Sheet = this;
            this.Cld_Graphic_List.Add(result);
            return result;
        }
        /// <summary>
        /// 将给定的Cld_Graphic和当前的sheet关联起来
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_Graphic to_add)
        {
            this.Cld_Graphic_List.Add(to_add);
            to_add.Prj_Sheet_ID = this.ID;
            to_add.Prj_Sheet = this;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
        }

        /// <summary>
        /// true:is equal
        /// false:not equal
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public virtual bool Compare(Prj_Sheet sheet) {
            if (this.ID != sheet.ID) {
                throw new Exception("id should be equal");
            }
            if (this.Height != sheet.Height || this.Prj_Controller_ID != sheet.Prj_Controller_ID
                || this.Prj_Document_ID != sheet.Prj_Document_ID || this.Sequence != sheet.Sequence
                || this.SheetName != sheet.SheetName || this.SheetNum != sheet.SheetNum || this.Width != sheet.Width
                )
            {
                return false;
            }
            else {
                return true;
            }
        }

		public virtual SizeF Size
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
			set
			{
				this.Width = (int)value.Width;
				this.Height = (int)value.Height;
			}
		}

    }
}
