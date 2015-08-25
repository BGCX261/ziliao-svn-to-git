using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace TDK.Core.Logic.Util
{
    public class ObjectCompare
    {
        public static bool IsEqual(object obj1,object obj2){
            Debug.Assert(obj1.GetType() == obj2.GetType());

            return false;
        }
    }
}
