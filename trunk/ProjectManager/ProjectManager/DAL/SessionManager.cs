using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace TDK.Core.Logic.DAL
{
    /// <summary>
    /// 用于管理NHibernate中的ISessionFactory,每一个SessionManager对象管理一个Isession
    /// </summary>
    public class SessionManager
    {
        private ISessionFactory m_session_factory;
        private string m_config_file;

        private ISessionFactory GetSessionFactory()
        {
            return (new Configuration()).Configure(m_config_file).BuildSessionFactory();
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config_file">NHibernate的数据库连接配置文件</param>
        public SessionManager(string config_file)
        {
            m_config_file = config_file;
            m_session_factory = GetSessionFactory();
        }
        /// <summary>
        /// 构造函数，默认采用名为“hibernate_config.xml”的数据库连接文件
        /// </summary>
        public SessionManager()
        {
            m_config_file = "hibernate_config.xml";
            m_session_factory = GetSessionFactory();
        }

        /// <summary>
        /// 获得一个Isession
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return m_session_factory.OpenSession();
        }
    }
}
