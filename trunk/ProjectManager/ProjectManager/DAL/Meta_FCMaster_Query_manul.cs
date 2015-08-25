using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using TDK.Core.Logic.URdoLib;

namespace TDK.Core.Logic.DAL
{
    public partial class Meta_FCMasterCRUD
    {
        /// <summary>
        /// 加载表中所有的Meta_FCMaster以及与其相关的Meta_FCDetail
        /// </summary>
        /// <returns></returns>
        public List<Meta_FCMaster> Load_FCMasters()
        {
            IList<Meta_FCMaster> masters = this.session.CreateQuery("from Meta_FCMaster").List<Meta_FCMaster>();
            IList<Meta_FCDetail> details = this.session.CreateQuery("from Meta_FCDetail").List<Meta_FCDetail>();
            Hashtable table = new Hashtable();
            foreach (Meta_FCMaster master in masters)
            {
                table[master.ID] = master;
                master.Meta_FCDetail_List = new ArrayList();
            }

            //双向关联detail和master
            foreach (Meta_FCDetail detail in details)
            {
                if (table.ContainsKey(detail.Meta_FCMaster_ID))
                {
                    Meta_FCMaster temp = (Meta_FCMaster)table[detail.Meta_FCMaster_ID];
                    temp.Meta_FCDetail_List.Add(detail);
                    detail.Meta_FCMaster = temp;
                }
                else
                {
                    continue;
                }
            }

            List<Meta_FCMaster> result = new List<Meta_FCMaster>();
            foreach (Meta_FCMaster m in table.Values)
            {
                result.Add(m);
            }
            return result;
        }

        /// <summary>
        /// 加载给定Name的meta_fcmaster以及与其相关的Meta_FCDetail
        /// </summary>
        /// <param name="funcname"></param>
        /// <returns></returns>
        public Meta_FCMaster Load_FCMaster_By_FuncName(string funcname)
        {
            //获得所有符合条件的master
            IList<Meta_FCMaster> masters = this.session.CreateQuery("from Meta_FCMaster as m where m.FunctionName = '" + funcname + "'")
                .List<Meta_FCMaster>();
            if (masters.Count == 0)
            {
                throw new Exception("no such Function Name");
            }

            Hashtable table = new Hashtable();           

            string querystring = "from Meta_FCDetail as d where ";
            //构造查询sql语句
            foreach (Meta_FCMaster master in masters)
            {
                table[master.ID] = master;
                querystring = querystring + "d.Meta_FCMaster_ID=" + master.ID + " OR ";
                //分配空间
                master.Meta_FCDetail_List = new ArrayList();
            }

            querystring = querystring.Trim();
            if (querystring.EndsWith("OR"))
            {
                querystring = querystring.Substring(0, querystring.LastIndexOf("OR"));
            }
            IList<Meta_FCDetail> details = this.session.CreateQuery(querystring).List<Meta_FCDetail>();
            //关联detail
            foreach (Meta_FCDetail d in details)
            {
                if (table.ContainsKey(d.Meta_FCMaster_ID))
                {
                    Meta_FCMaster temp = (Meta_FCMaster)table[d.Meta_FCMaster_ID];
                    temp.Meta_FCDetail_List.Add(d);
                    d.Meta_FCMaster = temp;
                }
                
            }

			if (masters.Count > 1)
			{
				throw new Exception("same function name accures more than one time");
			}

            return masters[0];
        }
    }
}
