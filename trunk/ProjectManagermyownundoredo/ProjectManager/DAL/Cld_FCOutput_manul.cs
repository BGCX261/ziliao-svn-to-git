using System;
using System.Collections.Generic;
using System.Text;

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
                    int index = this.m_Point.IndexOf('_');
                    float result = float.Parse(this.m_Point.Substring(0, index));
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
                    //if (this.m_Point == null) {
                    //    //如果为null，说明此字段未填充,数据库中此字段为空
                    //    return float.NaN;
                    //}
                    int index = this.m_Point.IndexOf('_');
                    float result = float.Parse(this.m_Point.Substring(index + 1));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCOutput表中id为" + this.ID + "的记录，坐标信息的格式不正确");
                }
            }
        }

        private int m_PinIndex = -1;
        /// <summary>
        /// Pin的Index
        /// </summary>
        public virtual int PinIndex {
            get { return m_PinIndex; }
            set { m_PinIndex = value; }
        }
    }
}
