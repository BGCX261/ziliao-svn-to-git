using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace xinhua
{
    class Functions
    {

        public static int Get_Max_ColNum(Hashtable input, List<List<Cld_Module>> target)
        {
            int max = -1;
            foreach (Cld_Module cld_module in input.Values)
            {
                int temp = Get_ColNum(cld_module, target);
                if (temp > max)
                {
                    max = temp;
                }
            }
            return max;
        }

        public static int Get_ColNum(Cld_Module input, List<List<Cld_Module>> target)
        {
            for (int i = 0; i < target.Count; i++)
            {
                if (target[i].Contains(input))
                {
                    return i;
                }
            }
            return -1;
        }




       

        /// <summary>
        /// 根据模块图标的名字得到其相应的高度
        /// </summary>
        /// <param name="Module_Symbol_Name">模块图标的名字</param>
        /// <returns></returns>
        public double Get_Module_Symbol_Height(string Module_Symbol_Name)
        {
            //not implented
            return 100;
        }


        public static void show_ASignal_List(List<ASignal> asignal_list)
        {
            foreach (ASignal asignal in asignal_list)
            {
                Console.WriteLine(asignal.start_module.ObjectID + "::" + asignal.start_pin +
                    "-->>>> type:" + (string)asignal.start_module.PinType[asignal.start_pin]);//开始
                //结束
                for (int i = 0; i < asignal.end_modules.Count; i++)
                {
                    Console.WriteLine(asignal.end_modules[i].ObjectID + "::" + asignal.end_pins[i] +
                        "   type:" + (string)asignal.end_modules[i].PinType[asignal.end_pins[i]]);
                }
            }
        }

        public static void show_Signals(Signals signals)
        {
            List<ASignal> temp = new List<ASignal>();
            foreach (ASignal asignal in signals.data.Values)
            {
                temp.Add(asignal);
            }
            show_ASignal_List(temp);
        }

    }
}
