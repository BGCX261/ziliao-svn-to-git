using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace TDK.Core.Logic.URdoLib
{
    /// <summary>
    /// 多个对象Undo Redo的管理器
    /// </summary>
    [Serializable]
    public class URdoManager
    {
        private Stack<object> undo;
        private Stack<object> redo;
        protected bool m_BeingUndone;

        /// <summary>
        /// 构造函数
        /// </summary>
        public URdoManager() {
            undo = new Stack<object>();
            redo = new Stack<object>();
            m_BeingUndone = false;
        }

        public void add_history(Entity obj) {
            if (!m_BeingUndone)
            {
                undo.Push(obj);
                redo.Clear();
            }
        }

        public bool CanUndo(){
            if(undo.Count>0){
                return true;
            }else{
                return false;
            }
        }

        public bool CanRedo(){
            if(redo.Count>0){
                return true;
            }else{
                return false;
            }
        }

        public void Undo() {
            if (undo.Count > 0)
            {
                m_BeingUndone = true;
                Entity temp = undo.Pop() as Entity;
                temp.Undo();
                redo.Push(temp);
                m_BeingUndone = false;
            }
        }

        public void Redo() {
            if (redo.Count > 0)
            {
                m_BeingUndone = true;
                Entity temp = redo.Pop() as Entity;
                temp.Redo();
                undo.Push(temp);
                m_BeingUndone = false;
            }
        }
    }
}
