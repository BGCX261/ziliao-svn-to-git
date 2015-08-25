using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;



namespace xinhua
{
    /// <summary>
    /// 对象比较时使用的信息类
    /// </summary>
    public struct ReverserInfo
    {
        /// <summary>
        /// 比较的方向，如下：
        /// ASC：升序
        /// DESC：降序
        /// </summary>
        public enum Direction
        {
            ASC = 0,
            DESC,
        };

        public enum Target
        {
            CUSTOMER = 0,
            FORM,
            FIELD,
            SERVER,
        };

        public string name;
        public Direction direction;
        public Target target;
    }



    /// <summary>
    /// 实现IComparer<T>接口，实现同一自定义对象的比较
    /// </summary>
    /// <typeparam name="T">T为泛型</typeparam>
    class Reverser<T> : IComparer<T>
    {
        private Type type = null;
        private ReverserInfo info;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">进行比较的类类型</param>
        /// <param name="name">进行比较对象的属性名称</param>
        /// <param name="direction">比较方向(升序/降序)</param>
        public Reverser(Type type, string name, ReverserInfo.Direction direction)
        {
            this.type = type;
            this.info.name = name;
            if (direction != ReverserInfo.Direction.ASC)
                this.info.direction = direction;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="className">进行比较的类名称</param>
        /// <param name="name">进行比较对象的属性名称</param>
        /// <param name="direction">比较方向(升序/降序)</param>
        public Reverser(string className, string name, ReverserInfo.Direction direction)
        {
            try
            {
                this.type = Type.GetType(className, true);
                this.info.name = name;
                this.info.direction = direction;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t">进行比较的类型的实例</param>
        /// <param name="name">进行比较对象的属性名称</param>
        /// <param name="direction">比较方向(升序/降序)</param>
        public Reverser(T t, string name, ReverserInfo.Direction direction)
        {
            this.type = t.GetType();
            this.info.name = name;
            this.info.direction = direction;
        }

        //必须！实现IComparer<T>的比较方法。
        int IComparer<T>.Compare(T t1, T t2)
        {
            object x = this.type.InvokeMember(this.info.name, BindingFlags.Public
                | BindingFlags.Instance | BindingFlags.GetProperty, null, t1, null);

            object y = this.type.InvokeMember(this.info.name, BindingFlags.Public
                | BindingFlags.Instance | BindingFlags.GetProperty, null, t2, null);

            if (this.info.direction != ReverserInfo.Direction.ASC)
                Swap(ref x, ref y);
            return (new CaseInsensitiveComparer()).Compare(x, y);
        }

        //交换操作数
        private void Swap(ref object x, ref object y)
        {
            object temp = null;
            temp = x;
            x = y;
            y = temp;
        }

    }

   

    /// <summary>
    /// 表示一条signal，一个开始，0个或多个结束
    /// </summary>
    public class ASignal
    {
        /// <summary>
        /// 开始Pin连线的走向，向上连还是向下连？
        /// </summary>
        public enum DIR { UP, DOWN };
        /// <summary>
        /// Signal的开始CLd_Module
        /// </summary>
        public Cld_Module start_module;
        /// <summary>
        /// Signal的开始的Pin Name为Meta_FCDetail表中的PinName
        /// </summary>
        public string start_pin;
        /// <summary>
        /// 开始Pin连线的走线，向上连还是向下连
        /// </summary>
        public DIR start_direction;
        /// <summary>
        /// Pin为了连线向外延伸部分的长度
        /// </summary>
        public double start_length;
        /// <summary>
        /// 开始PinName对应的PinIndex
        /// </summary>
        public int start_pin_index;
        /// <summary>
        /// Signal结束的CLd_Module集合
        /// </summary>
        public List<Cld_Module> end_modules;
        /// <summary>
        /// Signal结束的PinName集合
        /// </summary>
        public List<string> end_pins;





        public Cld_Module end_module;
        public string end_pin;


        public object annote;


        /// <summary>
        /// 构造函数，进行最基本的初始化
        /// </summary>
        public ASignal()
        {
            end_modules = new List<Cld_Module>();
            end_pins = new List<string>();
        }
    }



    /// <summary>
    /// 表示Signal的集合
    /// </summary>
    public class Signals
    {
        /// <summary>
        /// start_module->Asignal
        /// </summary>
        public Hashtable data;

        /// <summary>
        /// 构造函数，进行最基本的初始化工作
        /// </summary>
        public Signals()
        {
            data = new Hashtable();
        }
    }




}
