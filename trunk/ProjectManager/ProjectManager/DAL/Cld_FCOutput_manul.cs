using System;
using System.Collections.Generic;
using System.Text;
using DejaVu;
using System.Drawing;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCOutput
    {
        /// <summary>
        /// 获取输出引脚Pont的x坐标,失败返回double.NaN
        /// </summary>
        public virtual float X
        {
            get
            {
                try
                {
                    //if (this.m_Point == null) {
                    //    //如果为null，说明此字段未填充,数据库中此字段为空
                    //    return float.NaN;
                    //}
                    int index = this.Point.IndexOf('_');
                    float result = float.Parse(this.Point.Substring(0, index));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCOutput表中id为" + this.ID + "的记录，坐标信息的格式不正确");
                }
            }
        }
        /// <summary>
        /// 获取输出引脚Point的y坐标，失败返回double.NaN
        /// </summary>
        public virtual float Y
        {
            get
            {
                try
                {
                    //if (this.m_Point == null)
                    //{
                    //    //如果为null，说明此字段未填充,数据库中此字段为空
                    //    return float.NaN;
                    //}
                    int index = this.Point.IndexOf('_');
                    float result = float.Parse(this.Point.Substring(index + 1));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCOutput表中id为" + this.ID + "的记录，坐标信息的格式不正确");
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


        private UndoRedo<int> m_PinIndex = new UndoRedo<int>(-1);
        /// <summary>
        /// Pin的Index
        /// </summary>
        public virtual int PinIndex {
            get { return m_PinIndex.Value; }
            set { m_PinIndex.Value = value; }
        }

        public virtual bool Compare(Cld_FCOutput arg) {
            if (this.ID != arg.ID) {
                throw new Exception("id should be equal");
            }
            if (this.PinName != arg.PinName || this.PointName != arg.PointName
                || this.InitialValue != arg.InitialValue || this.Point != arg.Point
                || this.Visible != arg.Visible || this.Description != arg.Description
                || this.Cld_FCBlock_ID != arg.Cld_FCBlock_ID || this.Prj_Sheet_ID != arg.Prj_Sheet_ID
                || this.Prj_Document_ID != arg.Prj_Document_ID || this.Prj_Controller_ID != arg.Prj_Controller_ID
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
