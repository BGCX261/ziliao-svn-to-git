using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using DejaVu;

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
                if (this.X_Y == null)
                {
                    return float.NaN;
                }

                try
                {
                    float result = float.Parse(this.X_Y.Substring(0, this.X_Y.IndexOf('_')));
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
                if (this.X_Y == null)
                {
                    return float.NaN;
                }

                try
                {
                    float result = float.Parse(this.X_Y.Substring(this.X_Y.IndexOf('_') + 1));
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
			set
			{
				this.X_Y = string.Format("{0}_{1}", value.X, value.Y);
			}
        }

        private UndoRedo<float> m_width = new UndoRedo<float>();
        /// <summary>
        /// 宽
        /// </summary>
        public virtual float Width
        {
            get
            {
                return this.m_width.Value; ;
            }
            set
            {
                this.m_width.Value = value;
            }
        }

        private UndoRedo<float> m_height = new UndoRedo<float>();
        /// <summary>
        /// 高
        /// </summary>
        public virtual float Height
        {
            get
            {
                return this.m_height.Value;
            }
            set
            {
                this.m_height.Value = value;
            }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public virtual SizeF Size
        {
            get
            {
                return new SizeF(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
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
			set
			{
				this.AlgName = this.AlgName.Substring(0, this.AlgName.LastIndexOf("-") + 1) + value.ToString();
			}
        }

		public virtual int BlockIndex
		{
			get
			{
				return this.Sequence;
			}
			set
			{
				this.Sequence = value;
			}
		}


		private Cld_FCBlock inputReferenceBlock;

		public virtual Cld_FCBlock InputReferenceBlock
		{
			get
			{
				return inputReferenceBlock;
			}
			set
			{
				inputReferenceBlock = value;
			}
		}

		private IList<Cld_FCBlock> outputReferenceBlocks = new List<Cld_FCBlock>();

		public virtual IList<Cld_FCBlock> OutputReferenceBlocks
		{
			get
			{
				return outputReferenceBlocks;
			}
			set
			{
				outputReferenceBlocks = value;
			}
		}

		private IList<Cld_FCBlock> loopReferenceBlocks = new List<Cld_FCBlock>();

		public virtual IList<Cld_FCBlock> LoopReferenceBlocks
		{
			get
			{
				return loopReferenceBlocks;
			}
			set
			{
				loopReferenceBlocks = value;
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
            result.Cld_FCBlock = this;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.Prj_Sheet_ID;
            result.Prj_Sheet = this.Prj_Sheet;
            this.Cld_FCInput_List.Add(result);
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
            to_add.Cld_FCBlock = this;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Sheet_ID = this.Prj_Sheet_ID;
            to_add.Prj_Sheet = this.Prj_Sheet;
        }
        /// <summary>
        /// 创建一个 OutputPin
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCOutput New_FCOutput()
        {
            Cld_FCOutput result = new Cld_FCOutput();
            result.Cld_FCBlock_ID = this.ID;
            result.Cld_FCBlock = this;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.Prj_Sheet_ID;
            result.Prj_Sheet = this.Prj_Sheet;
            this.Cld_FCOutput_List.Add(result);
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
            to_add.Cld_FCBlock = this;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Sheet_ID = this.Prj_Sheet_ID;
            to_add.Prj_Sheet = this.Prj_Sheet;
        }
        /// <summary>
        /// ArrayList
        /// </summary>
        /// <returns></returns>
        public virtual Cld_FCParameter New_FCParameter()
        {
            Cld_FCParameter result = new Cld_FCParameter();
            result.Cld_FCBlock_ID = this.ID;
            result.Cld_FCBlock = this;
            result.Prj_Controller_ID = this.Prj_Controller_ID;
            result.Prj_Controller = this.Prj_Controller;
            result.Prj_Document_ID = this.Prj_Document_ID;
            result.Prj_Document = this.Prj_Document;
            result.Prj_Sheet_ID = this.Prj_Sheet_ID;
            result.Prj_Sheet = this.Prj_Sheet;
            this.Cld_FCParameter_List.Add(result);
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
            to_add.Cld_FCBlock = this;
            to_add.Prj_Controller_ID = this.Prj_Controller_ID;
            to_add.Prj_Controller = this.Prj_Controller;
            to_add.Prj_Document_ID = this.Prj_Document_ID;
            to_add.Prj_Document = this.Prj_Document;
            to_add.Prj_Sheet_ID = this.Prj_Sheet_ID;
            to_add.Prj_Sheet = this.Prj_Sheet;
        }

        /// <summary>
        /// 附加在FCBlock上的信息
        /// </summary>
        private UndoRedo<List<object>> m_annotes = new UndoRedo<List<object>>();
        /// <summary>
        /// 附加在FCBlock上的信息
        /// </summary>
        public virtual List<object> Annotes
        {
            get
            {
                return this.m_annotes.Value;
            }
            set
            {
                this.m_annotes.Value = value;
            }
        }

        private UndoRedo<ArrayList> graphicsCollection = new UndoRedo<ArrayList>();
        /// <summary>
        /// 该 Block 包含的基本图形集合
        /// </summary>
        public virtual ArrayList GraphicsCollection
        {
            get
            {
                return graphicsCollection.Value;
            }
            set
            {
                graphicsCollection.Value = value;
            }
        }

        private UndoRedo<symbol> m_symbol = new UndoRedo<symbol>();
        /// <summary>
        /// 与此Cld_FCBlock关联的symbol
        /// </summary>
        public virtual symbol Symbol
        {
            get
            {
                return m_symbol.Value;
            }
            set
            {
                m_symbol.Value = value;
            }
        }

        public virtual bool Compare(Cld_FCBlock block) {
            if (this.ID != block.ID) {
                throw new Exception("id should be equal");
            }
            if (this.AlgName != block.AlgName || this.Sequence != block.Sequence
                || this.FunctionName != block.FunctionName || this.X_Y != block.X_Y
                || this.SymbolName != block.SymbolName || this.Description != block.Description
                || this.Prj_Controller_ID != block.Prj_Controller_ID || this.Prj_Document_ID != block.Prj_Document_ID
                || this.Prj_Sheet_ID != block.Prj_Sheet_ID
                )
            {
                return false;
            }
            else {
                return true;
            }
        }

    }

    [Serializable]
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
    [Serializable]
    public class symbol
    {
        private UndoRedo<symbol_kinds> m_kind = new UndoRedo<symbol_kinds>();
        private UndoRedo<List<string>> m_edges = new UndoRedo<List<string>>();
        private UndoRedo<float> m_width = new UndoRedo<float>();
        private UndoRedo<float> m_height = new UndoRedo<float>();
        private UndoRedo<string> m_symbol_name = new UndoRedo<string>();

        /// <summary>
        /// symbol的类型
        /// </summary>
        public symbol_kinds kind{
            get { return this.m_kind.Value; }
            set { this.m_kind.Value = value; }
        }
        /// <summary>
        /// 矩形的四条边
        /// </summary>
        public List<string> edges{
            get { return this.m_edges.Value; }
            set { this.m_edges.Value = value; }
        }
        /// <summary>
        /// 矩形的宽度
        /// </summary>
        public float width{
            get { return this.m_width.Value; }
            set { this.m_width.Value = value; }
        }
        /// <summary>
        /// 矩形的高度
        /// </summary>
        public float height {
            get { return this.m_height.Value; }
            set { this.m_height.Value = value; }
        }
        /// <summary>
        /// symbol的名字
        /// </summary>
        public string symbol_name{
            get { return this.m_symbol_name.Value; }
            set { this.m_symbol_name.Value = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public symbol()
        {
            kind = symbol_kinds.Error;
        }
    }
}
