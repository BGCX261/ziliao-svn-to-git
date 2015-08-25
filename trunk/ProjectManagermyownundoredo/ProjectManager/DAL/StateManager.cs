using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    public class StateManager
    {
        private List<object> m_newed;
        private List<object> m_deleted;
        private List<object> m_modified;
        private List<object> m_loaded;

        /// <summary>
        /// 构造函数
        /// </summary>
        public StateManager(){
            this.m_newed = new List<object>();
            this.m_deleted = new List<object>();
            this.m_modified = new List<object>();
            this.m_loaded = new List<object>();
        }

        /// <summary>
        /// 新加入的块的列表
        /// </summary>
        public List<object> Newed_List{
            get{
                return this.m_newed;
            }
            set{
                this.m_newed = value;
            }
        }

        /// <summary>
        /// 删除的块的列表
        /// </summary>
        public List<object> Deleted_List{
            get{
                return this.m_deleted;
            }
            set{
                this.m_deleted = value;
            }
        }

        /// <summary>
        /// 修改的块的列表
        /// </summary>
        public List<object> Modified_List{
            get{
                return this.m_modified;
            }
            set{
                this.m_modified = value;
            }
        }

        /// <summary>
        /// 加载的块的列表
        /// </summary>
        public List<object> Loaded_List{
            get{
                return this.m_loaded;
            }
            set{
                this.m_loaded = value;
            }
        }

        
    }
}
