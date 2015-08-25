using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using TDK.Core.Logic.URdoLib;
using System.Reflection;

namespace TDK.Core.Logic.DAL
{
    [Serializable]
    public class EntityBase
    {   
        private bool m_reload;
        private bool m_orin;
     
        /// <summary>
        /// ��˶��������ISession
        /// </summary>
        public static ISession session = null;


        /// <summary>
        /// ���캯��
        /// </summary>
        public EntityBase() {
            this.m_reload = false;
            this.m_orin = false;
        }

             
        /// <summary>
        /// �����������ʱ�Ƿ����¼���������ص�List��Parent
        /// </summary>
        public virtual bool Reload
        {
            get
            {
                return m_reload;
            }
            set
            {
                m_reload = value;
            }
        }
        public virtual EntityBase SetReload()
        {
            this.m_reload = true;
            return this;
        }
        public virtual EntityBase UnsetReload()
        {
            this.m_reload = false;
            return this;
        }

        public virtual bool Orin {
            get { return this.m_orin; }
            set { this.m_orin = value; }
        }
    }
}
