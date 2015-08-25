using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public class CRUDBase
    {
        /// <summary>
        /// 访问数据库的接口
        /// </summary>
		public ISession session;
		
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">nhibernate session</param>
        public CRUDBase(ISession session)
        {
            this.session = session;
			Cld_Constant.session = session;
        }
        
    }
}
