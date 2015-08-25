using System;
using System.Collections.Generic;
using System.Text;
using DejaVu;
using System.Drawing;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCInput
    {
        /// <summary>
        /// 获取输入引脚Point的x坐标，失败返回double.NaN
        /// </summary>
        public virtual float X
        {
            get
            {
                try
                {
                    //if (this.m_Point == null)
                    //{
                    //    return float.NaN;
                    //}
                    int index = this.Point.IndexOf('_');
                    float result = float.Parse(this.Point.Substring(0, index));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCInput表中id为" + this.ID + "的记录，坐标信息的格式不正确");
                }
            }
        }
        /// <summary>
        /// 获取输入引脚Point的y坐标
        /// </summary>
        public virtual float Y
        {
            get
            {
                try
                {
                    //if (this.m_Point == null)
                    //{
                    //    return float.NaN;
                    //}
                    int index = this.Point.IndexOf('_');
                    float result = float.Parse(this.Point.Substring(index + 1));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCInput表中id为" + this.ID + "的记录，坐标信息的格式不正确");
                }
            }
        }

		public virtual PointF Location
		{
			get
			{
				return new PointF(X, Y);
			}
			set
			{
				this.Point = string.Format("{0}_{1}", value.X, value.Y);
			}
		}


		private bool isDigital;

		public virtual bool IsDigital
		{
			get
			{
				return isDigital;
			}
			set
			{
				isDigital = value;
			}
		}


        private UndoRedo<int> m_pinIndex = new UndoRedo<int>(-1);
        /// <summary>
        /// Pin的Index
        /// </summary>
        public virtual int PinIndex
        {
            get { return m_pinIndex.Value; }
            set { m_pinIndex.Value = value; }
        }

        public virtual bool Compare(Cld_FCInput arg) {
            if (this.ID != arg.ID) {
                throw new Exception("id should be equal");
            }
            if (this.PinName != arg.PinName || this.PointName != arg.PointName
                || this.InitialValue != arg.InitialValue || this.Point != arg.Point
                || this.Visible != arg.Visible || this.Description != arg.Description
                || this.Cld_FCBlock_ID != arg.Cld_FCBlock_ID || this.Prj_Controller_ID != arg.Prj_Controller_ID
                || this.Prj_Document_ID != arg.Prj_Document_ID || this.Prj_Sheet_ID != arg.Prj_Sheet_ID
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
