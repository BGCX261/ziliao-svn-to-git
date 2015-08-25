using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using TDK.Core.Logic.DAL;

namespace TDK.Core.Logic.BLL
{
    public partial class BllManager
    {

        /// <summary>
        /// 根据跟定的Function Name和Pin Name，Pin的Index,如果找不到，抛出异常
        /// 缓存查询过的
        /// </summary>
        /// <param name="function_name">Function Name</param>
        /// <param name="pinname">Pin Name</param>
        /// <returns>Pin 的Index，失败抛出异常</returns>
        public int get_pin_index(string function_name, string pinname)
        {
            Meta_FCMaster meta_master = get_BlockMeta(function_name);
            Meta_FCDetail meta_pin = get_PinMeta(meta_master, pinname);
            return meta_pin.PinIndex;
        }

        public string get_pin_dataType(string function_name, string pinname)
        {
            Meta_FCMaster meta_master = get_BlockMeta(function_name);
            Meta_FCDetail meta_pin = get_PinMeta(meta_master, pinname);
            return meta_pin.DataType;
        }

        /// <summary>
        /// 根据跟定的FunctionName得到元数据，如果找不到，抛出异常
        /// 缓存查询过的
        /// </summary>
        /// <param name="function_name">Function Name</param>
        /// <returns>Meta_FCMaster，元数据，失败抛出异常</returns>
        public Meta_FCMaster get_BlockMeta(string function_name)
        {
            if (funcname_to_MetaFCMaster.ContainsKey(function_name))
            {
                //缓存中存在此function的相关信息,查找缓存
                Meta_FCMaster master_temp = funcname_to_MetaFCMaster[function_name] as Meta_FCMaster;
                return master_temp;
            }
            //从数据库中加载
            IList<Meta_FCMaster> meta_masters = manager.MetaMasterCRUD.Load_FCMasters_By_FuncName(function_name);
            if (meta_masters.Count > 1)
            {
                throw new Exception("same function name occurs more than once");
            }
            if (meta_masters.Count < 1)
            {
                throw new Exception("no function: " + function_name + "metadata!");
            }
            Meta_FCMaster meta_master = meta_masters[0];
            //添加进缓存
            this.funcname_to_MetaFCMaster[function_name] = meta_master;
            return meta_master;
        }

        /// <summary>
        /// 根据跟定的Meta_FCMaster和PinName得到Pin的元数据，如果找不到，抛出异常
        /// </summary>
        /// <param name="function_name">Function Name</param>
        /// <returns>Meta_FCDetail，元数据，失败抛出异常</returns>
        public Meta_FCDetail get_PinMeta(Meta_FCMaster meta_master, string pinname)
        {
            foreach (Meta_FCDetail detail in meta_master.Meta_FCDetail_List)
            {
                if (detail.PinName.Equals(pinname))
                {
                    return detail;
                }
            }
            throw new Exception("no such " + pinname + " name in " + meta_master.FunctionName);
        }


        /// <summary>
        /// 根据跟定的Function Name和Pin Name，Pin的Index,如果找不到，抛出异常
        /// </summary>
        /// <param name="function_name"></param>
        /// <param name="pinname"></param>
        /// <param name="isload">是否使用一次加载（全部load进内存）方式</param>
        /// <returns></returns>
        public int get_pin_index(string function_name, string pinname, bool isload)
        {
            if (isload)
            {
                List<Meta_FCMaster> masters = this.manager.MetaMasterCRUD.Load_FCMasters();
                foreach (Meta_FCMaster master in masters)
                {
                    funcname_to_MetaFCMaster[master.FunctionName] = master;
                }
            }
            return get_pin_index(function_name, pinname);
        }
    }
}
