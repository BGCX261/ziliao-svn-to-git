using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TDK.Core.Logic.DAL;

namespace TDK.Core.Logic.URdoLib
{
    /// <summary>
    /// ��дIList����ط������Ա�ʵ��UndoRedo
    /// </summary>
    public class MyList : IList
    {
        private List<object> m_contents;
        private Base m_base;

        
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="b">ʹ��Mylist��Base����</param>
        public MyList(Base b){
            this.m_contents = new List<object>();
            this.m_base = b;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="temp"></param>
        public MyList(IList temp, Base b){
            this.m_base = b;
            foreach(object obj in temp){
                this.m_contents.Add(obj);
            }
        }

        //�ӿڷ�����ʵ��
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// setδʵ��
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                return this.m_contents[index];
            }
            set
            {
                throw new Exception("the 'set'method of this property is not implimented");
            }
        }

        /// <summary>
        /// ��Ӷ���list��ĩβ
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(object value)
        {
            this.m_contents.Add(value);
            Base temp = value as Base;
            switch (temp.State)
            {
                case objstate.Deleted:
                    //��deleted�б��еĶ����ٴ����
                    this.m_base.State_Manager.Deleted_List.Remove(value);
                    if (temp.IsModified)
                    {
                        temp.State = objstate.Modified;
                        this.m_base.State_Manager.Modified_List.Add(value);
                    }
                    else {
                        temp.State = objstate.Loaded;
                        this.m_base.State_Manager.Loaded_List.Add(value);
                    }
                    break;
                case objstate.Loaded:
                    if(this.m_base.IsModified){
                        throw new Exception("when a obj in loaded state,it should not have been modified");
                    }
                    break;
                case objstate.Modified:
                    break;
                case objstate.Newed:
                    if(!this.m_base.State_Manager.Newed_List.Contains(value)){
                        this.m_base.State_Manager.Newed_List.Add(value);
                    }
                    break;
            }
            return this.m_contents.Count;
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Clear()
        {
            foreach (object obj in this.m_contents)
            {
                Base temp = obj as Base;
                switch (temp.State) { 
                    case objstate.Deleted:
                        break;
                    case objstate.Loaded:
                        this.m_base.State_Manager.Loaded_List.Remove(obj);
                        this.m_base.State_Manager.Deleted_List.Add(obj);
                        break;
                    case objstate.Modified:
                        this.m_base.State_Manager.Modified_List.Remove(obj);
                        this.m_base.State_Manager.Deleted_List.Add(obj);
                        break;
                    case objstate.Newed:
                        this.m_base.State_Manager.Newed_List.Remove(obj);
                        break;
                }
                temp.State = objstate.Deleted;
            }
            this.m_contents.Clear();
        }

        public bool Contains(object value)
        {
            return this.m_contents.Contains(value);
        }

        public int IndexOf(object value)
        {
            return this.m_contents.IndexOf(value);
        }

        /// <summary>
        /// �˷���δʵ�� 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, object value)
        {
            throw new Exception("this method is not implemented");
        }

        /// <summary>
        /// ���б����Ƴ��ض�����ĵ�һ��ƥ����
        /// </summary>
        /// <param name="value"></param>
        public void Remove(object value)
        {
            Base temp = value as Base;
            switch(temp.State){
                case objstate.Deleted:
                    throw new Exception("object already deleted");
                    break;
                case objstate.Loaded:
                    this.m_base.State_Manager.Loaded_List.Remove(value);
                    this.m_base.State_Manager.Deleted_List.Add(value);
                    break;
                case objstate.Modified:
                    this.m_base.State_Manager.Modified_List.Remove(value);
                    this.m_base.State_Manager.Deleted_List.Add(value);
                    break;
                case objstate.Newed:
                    this.m_base.State_Manager.Newed_List.Remove(value);
                    break;
            }
            temp.State = objstate.Deleted;
            this.m_contents.Remove(value);
        }

        /// <summary>
        /// ���б���ɾ������
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            object obj = this.m_contents[index];
            this.Remove(obj);
        }

        /// <summary>
        /// �˷�����δʵ��
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            throw new Exception("this method is not implemented");
        }

        public int Count
        {
            get
            {
                return this.m_contents.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }
                
        public IEnumerator GetEnumerator()
        {
            return this.m_contents.GetEnumerator();
        }

    }


}
