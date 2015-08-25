using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using TDK.Core.Logic.URdoLib;

namespace TDK.Core.Logic.DAL
{
    public class Base : Entity
    {
        private objstate m_state;
        private bool m_ismodified;
        private StateManager m_state_manager;
        private bool m_reload;
        private URdoManager m_manager;

        /// <summary>
        /// ��˶��������ISession
        /// </summary>
        public static ISession session = null;


        /// <summary>
        /// ���캯��
        /// </summary>
        public Base() {
            this.m_state = objstate.Newed;
            this.m_state_manager = null;
            this.m_ismodified = false;
            this.m_reload = false;
            this.m_manager = null;
        }

        /// <summary>
        /// ��¼������������Ƿ��޸�
        /// </summary>
        public virtual bool IsModified {
            get {
                return this.m_ismodified;
            }
            set {
                this.m_ismodified = value;
            }
        }


        /// <summary>
        /// ��¼�����״̬
        /// </summary>
        public virtual objstate State
        {
            get
            {
                return this.m_state;
            }
            set
            {
                this.m_state = value;
            }
        }

        /// <summary>
        /// ����״̬������
        /// </summary>
        public virtual StateManager State_Manager {
            get {
                return this.m_state_manager;
            }
            set {
                this.m_state_manager = value;
            }
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
        public virtual Base SetReload()
        {
            this.m_reload = true;
            return this;
        }
        public virtual Base UnsetReload()
        {
            this.m_reload = false;
            return this;
        }

        public virtual URdoManager URManager
        {
            get
            {
                return m_manager;
            }
            set
            {
                m_manager = value;
            }
        }

        //method
        

    }
}
