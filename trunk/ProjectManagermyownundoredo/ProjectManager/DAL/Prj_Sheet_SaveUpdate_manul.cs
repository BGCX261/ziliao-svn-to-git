using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using NHibernate;

namespace TDK.Core.Logic.DAL
{
    public partial class Prj_SheetCRUD
    {
        /// <summary>
        /// �����ݿ��ύһ��Sheet
        /// ԭ��
        /// �ȴ����ݿ���ɾ��
        /// Ȼ���ڲ���
        /// Ч�����⣿��������
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public bool Commit(Prj_Sheet sheet)
        {
            //����ɾ�����ݿ��е���ؼ�¼
            this.DeletePrj_Sheet_By_ID(sheet.ID);

            ITransaction transaction = this.session.BeginTransaction();
            int count = 0;
            try
            {
                //�ڽ��в���֮ǰ���л���Ĵ����������Ϊ�����״̬����һЩ����
                this.session.Flush();
                this.session.Clear();
                this.session.Save(sheet);
                this.session.Flush();
                this.session.Clear();

                //signal
                foreach (Cld_Signal signal in sheet.Cld_Signal_List)
                {
                    count++;
                    signal.Prj_Sheet_ID = sheet.ID;
                    this.session.Save(signal);
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //const
                foreach (Cld_Constant con in sheet.Cld_Constant_List)
                {
                    count++;
                    con.Prj_Sheet_ID = sheet.ID;
                    this.session.Save(con);
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //block,block���²���ʱ��ID�����·���,����д���
                Hashtable oldid_to_newid = new Hashtable();
                foreach (Cld_FCBlock block in sheet.Cld_FCBlock_List)
                {
                    count++;
                    block.Prj_Sheet_ID = sheet.ID;
                    int oldid = block.ID;
                    this.session.Save(block);
                    oldid_to_newid[oldid] = block.ID;
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //graphic
                foreach (Cld_Graphic graphic in sheet.Cld_Graphic_List)
                {
                    count++;
                    graphic.Prj_Sheet_ID = sheet.ID;
                    this.session.Save(graphic);
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }
                //flush to db and clean cache
                this.session.Flush();
                this.session.Clear();

                //parameter
                foreach (Cld_FCParameter para in sheet.Cld_FCParameter_List)
                {
                    count++;
                    para.Prj_Sheet_ID = sheet.ID;
                    para.Cld_FCBlock_ID = (int)oldid_to_newid[para.Cld_FCBlock_ID];
                    this.session.Save(para);
                    this.session.Flush();
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //out
                foreach (Cld_FCOutput output in sheet.Cld_FCOutput_List)
                {
                    count++;
                    output.Prj_Sheet_ID = sheet.ID;
                    output.Cld_FCBlock_ID = (int)oldid_to_newid[output.Cld_FCBlock_ID];
                    this.session.Save(output);
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }

                //input
                foreach (Cld_FCInput input in sheet.Cld_FCInput_List)
                {
                    count++;
                    input.Prj_Sheet_ID = sheet.ID;
                    input.Cld_FCBlock_ID = (int)oldid_to_newid[input.Cld_FCBlock_ID];
                    this.session.Save(input);
                    if (count % 50 == 0)
                    {
                        this.session.Flush();
                        this.session.Clear();
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}
