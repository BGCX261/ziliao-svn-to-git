using System;
using System.Collections.Generic;
using System.Text;
using ProjectManager.LogicGraphic;
using System.Drawing;
using System.Collections;

namespace TDK.Core.Logic.DAL
{
    public partial class Cld_FCBlock
    {
        #region Block 的位置与大小
        /// <summary>
        /// 获得FCBlock位置的x坐标,失败返回double.NaN
        /// </summary>
        public virtual float X
        {
            get
            {
                if (this.m_X_Y == null)
                {
                    return float.NaN;
                }

                try
                {
                    float result = float.Parse(this.m_X_Y.Substring(0, this.m_X_Y.IndexOf('_')));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCBlock表中id为" + this.ID + "的记录，Point部分的格式不正确");
                }
            }
        }

        /// <summary>
        /// 获得FCBlock位置的y坐标,失败返回double.NaN
        /// </summary>
        public virtual float Y
        {
            get
            {
                if (this.m_X_Y == null)
                {
                    return float.NaN;
                }

                try
                {
                    float result = float.Parse(this.m_X_Y.Substring(this.m_X_Y.IndexOf('_') + 1));
                    return result;
                }
                catch (Exception)
                {
                    throw new Exception("Cld_FCBlock表中id为" + this.ID + "的记录，Point部分的格式不正确");
                }
            }
        }

        /// <summary>
        /// 获取FCBlock左上角坐标
        /// </summary>
        public virtual PointF Location
        {
            get
            {
                return new PointF(this.X, this.Y);
            }
        }

        private float width;
        /// <summary>
        /// 宽
        /// </summary>
        public virtual float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private float height;
        /// <summary>
        /// 高
        /// </summary>
        public virtual float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public virtual SizeF Size
        {
            get
            {
                return new SizeF(this.width, this.height);
            }
            set
            {
                this.width = value.Width;
                this.height = value.Height;
            }
        }

        #endregion

        /// <summary>
        ///  块号
        /// </summary>
        public virtual int BlockID
        {
            get
            {
                int id;
                try
                {
                    id = Convert.ToInt32(this.AlgName.Substring(this.AlgName.LastIndexOf("-") + 1));
                }
                catch (Exception e)
                {
                    throw e;
                }

                return id;
            }
        }


        /// <summary>
        /// 创建一个 InputPin
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCInput New_FCInput()
        {
            Cld_FCInput result = new Cld_FCInput();
            result.Cld_FCBlock_ID = this.ID;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.Prj_Sheet_ID;
            return result;
        }
        /// <summary>
        /// 向该 Block 添加一个 InputPin
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_FCInput to_add)
        {
            this.Cld_FCInput_List.Add(to_add);
            to_add.Cld_FCBlock_ID = this.ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Sheet_ID = this.Prj_Sheet_ID;
        }
        /// <summary>
        /// 创建一个 OutputPin
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCOutput New_FCOutput()
        {
            Cld_FCOutput result = new Cld_FCOutput();
            result.Cld_FCBlock_ID = this.ID;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.Prj_Sheet_ID;
            return result;
        }
        /// <summary>
        /// 向该 Block 添加一个 OutputPin
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_FCOutput to_add)
        {
            this.Cld_FCOutput_List.Add(to_add);
            to_add.Cld_FCBlock_ID = this.ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Sheet_ID = this.Prj_Sheet_ID;
        }
        /// <summary>
        /// ArrayList
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCParameter New_FCParameter()
        {
            Cld_FCParameter result = new Cld_FCParameter();
            result.Cld_FCBlock_ID = this.ID;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Sheet_ID = this.Prj_Sheet_ID;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to_add"></param>
        public virtual void Add(Cld_FCParameter to_add)
        {
            this.Cld_FCParameter_List.Add(to_add);
            to_add.Cld_FCBlock_ID = this.ID;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Sheet_ID = this.Prj_Sheet_ID;
        }

        /// <summary>
        /// 附加在FCBlock上的信息
        /// </summary>
        private List<object> m_annotes;
        /// <summary>
        /// 附加在FCBlock上的信息
        /// </summary>
        public virtual List<object> Annotes
        {
            get
            {
                return this.m_annotes;
            }
            set
            {
                this.m_annotes = value;
            }
        }

        private ArrayList graphicsCollection = new ArrayList();
        /// <summary>
        /// 该 Block 包含的基本图形集合
        /// </summary>
        public virtual ArrayList GraphicsCollection
        {
            get
            {
                return graphicsCollection;
            }
            set
            {
                graphicsCollection = value;
            }
        }

        private symbol m_symbol;
        /// <summary>
        /// 与此Cld_FCBlock关联的symbol
        /// </summary>
        public virtual symbol Symbol
        {
            get
            {
                return m_symbol;
            }
            set
            {
                m_symbol = value;
            }
        }
    }

    public enum symbol_kinds
    {
        /// <summary>
        /// 规则的矩形
        /// </summary>
        Rectangle,
        /// <summary>
        /// 不规则的图形
        /// </summary>
        Irregular,
        /// <summary>
        /// 非法的symbol
        /// </summary>
        Error
    }

    /// <summary>
    /// 描述与Cld_FCBlock相关的symbol
    /// </summary>
    public class symbol
    {
        /// <summary>
        /// symbol的类型
        /// </summary>
        public symbol_kinds kind;
        /// <summary>
        /// 矩形的四条边
        /// </summary>
        public List<string> edges;
        /// <summary>
        /// 矩形的宽度
        /// </summary>
        public float width;
        /// <summary>
        /// 矩形的高度
        /// </summary>
        public float height;
        /// <summary>
        /// symbol的名字
        /// </summary>
        public string symbol_name;
        /// <summary>
        /// 构造函数
        /// </summary>
        public symbol()
        {
            kind = symbol_kinds.Error;
        }
    }
}
