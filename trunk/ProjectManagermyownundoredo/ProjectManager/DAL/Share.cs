using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.DAL
{
    /// <summary>
    /// 描述对象所处的状态
    /// </summary>
    public enum objstate{
        /// <summary>
        /// 对象从数据库中加载
        /// </summary>
        Loaded,
        /// <summary>
        /// 新建的对象
        /// </summary>
        Newed,
        /// <summary>
        /// 对象被删除
        /// </summary>
        Deleted,
        /// <summary>
        /// 对象被修改
        /// </summary>
        Modified,
        /// <summary>
        /// 此状态属性不可用
        /// </summary>
        NAS
    }
}
