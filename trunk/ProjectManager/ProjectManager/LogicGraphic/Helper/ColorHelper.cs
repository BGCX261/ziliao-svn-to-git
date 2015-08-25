using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProjectManager.LogicGraphic
{
    public static class ColorHelper
    {
        /// <summary>
        /// 将<seealso cref="System.Drawing.Color"/>类型的颜色转换成用16进制字符串(AA RR GG BB)表示的形式
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToString(Color color)
        {
            string strColor = string.Empty;
            if (!color.IsEmpty)
            {
                // 新华系统目前使用的颜色字符串格式
                strColor = string.Format("{0} {1} {2} {3}",
                    color.R.ToString("X").PadLeft(2, '0'),
                    color.G.ToString("X").PadLeft(2, '0'),
                    color.B.ToString("X").PadLeft(2, '0'),
                    color.A.ToString("X")).PadLeft(2, '0');
            }

            return strColor;
        }
    }
}
