using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace xinhua
{
    public partial class Form1 : Form
    {
        private string file_path;
        private string strConn = "";
        private OleDbConnection myConn = null;
        private Meta_ModuleS meta_modules = null;
        private Meta_SymbolS meta_symbols = null;
        private List<string> controlladdr_list;

        public Form1()
        {
            InitializeComponent();
            this.toolStripStatusLabel1.Text = "Cuttent File is:NULL";
            this.toolStripStatusLabel2.Text = "";
            controlladdr_list = new List<string>();
        }

        /// <summary>
        /// symbol的生成
        /// </summary>
        public void Generate_Symbol()
        {
            if (this.myConn == null)
            {
                MessageBox.Show("首先请选择一个数据库文件");
                return;
            }
            this.toolStripStatusLabel2.Text = "正在生成图标... ...";
            Meta_SymbolS meta_symbols = new Meta_SymbolS(this.meta_modules);//转换为描述Meta_Symbol信息
            meta_symbols.InsertIntoTable(this.myConn);//插入到Meta_SymbolMaster和meta_symboldetail中
            this.toolStripStatusLabel2.Text = "图标生成结束.";
        }

        /// <summary>
        /// CLd_Signal表的数据填充
        /// </summary>
        public void Generate_Signal_By_CldTable(string ControllAddres,string DocumentsName,string SheetName)
        {
            Cld_ModuleS cld_modules = new Cld_ModuleS(this.myConn, ControllAddres, DocumentsName, SheetName, this.meta_modules);
            cld_modules.Generate_Signal_Info(ControllAddres, DocumentsName, SheetName);
            cld_modules.Generate_Signal_Data(this.meta_symbols);
            cld_modules.Insert_SignalData_Into_Table(this.myConn);
        }

        /// <summary>
        /// 文件的选择,Controlladdr的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {            
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            
            if (open_file_dialog.ShowDialog() == DialogResult.OK) 
            {
                this.file_path = open_file_dialog.FileName;
                this.toolStripStatusLabel1.Text = "Current File is:" + this.file_path;
                this.strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=" + this.file_path;
                this.myConn = new OleDbConnection(this.strConn);
                this.meta_modules = new Meta_ModuleS(this.myConn);
                this.meta_symbols = new Meta_SymbolS(this.meta_modules);

                string sqlStr = "SELECT DISTINCT ControllerAddress FROM Prj_Document";
                OleDbDataAdapter da = new OleDbDataAdapter(sqlStr, myConn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                this.dataGridView_ControlAddr.DataSource = ds.Tables[0].DefaultView;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    controlladdr_list.Add(row[0].ToString());
                }
            }
        }

        /// <summary>
        /// Document的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_ControlladdrSelectChanged(object sender, EventArgs e) {
            string controlladdr;
            controlladdr = this.dataGridView_ControlAddr.CurrentCell.Value.ToString();

            string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=" + file_path;
            OleDbConnection myConn = new OleDbConnection(strConn);       
            string sqlStr = "SELECT DocumentName,DocumentCaption,CreateTime,Order,Type,TranslatorResult " +
                    "FROM Prj_Document WHERE ControllerAddress = '" + controlladdr + "'";
            OleDbDataAdapter da = new OleDbDataAdapter(sqlStr, myConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DocumentName");
            this.dataGridView_DocumentName.DataSource = ds.Tables["DocumentName"].DefaultView;
        }

        /// <summary>
        /// signal数据的生成以及插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(new ThreadStart(this.Generate_Selected_Documents));
            td.Start();
            
        }

        public void Generate_Selected_Documents() { 
            List<string> documentnames = new List<string>();

            int rowcount = dataGridView_DocumentName.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (rowcount > 0)
            {
                for (int i = 0; i < rowcount; i++)
                {
                    int row_index = this.dataGridView_DocumentName.SelectedRows[i].Index;
                    string temp_name = this.dataGridView_DocumentName[0, row_index].Value.ToString();
                    documentnames.Add(temp_name);
                }
            }
            else {
                MessageBox.Show("请选择要生成signal数据的行");
            }

            foreach (string documentname in documentnames) {
                string controlladdr = this.dataGridView_ControlAddr.CurrentCell.Value.ToString();
                this.toolStripStatusLabel2.Text = "Generating " + controlladdr + ":" + documentname;
                this.Generate_Signal_By_CldTable(controlladdr, documentname, "1");
                this.toolStripStatusLabel2.Text = "Generating complete";
            }
        }

        /// <summary>
        /// 生成图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(new ThreadStart(this.Generate_Symbol));
            td.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(new ThreadStart(this.Generate_all));
            td.Start();
        }

        /// <summary>
        /// 生成所有的signal
        /// </summary>
        public void Generate_all() {
            if (this.myConn == null) {
                MessageBox.Show("请选择一个数据库文件");
            }

            List<List<string>> controller_docname = new List<List<string>>();
            string sql_string = "SELECT DISTINCT ControllerAddress,DocumentName FROM Cld_FCMaster";
            OleDbDataAdapter da = new OleDbDataAdapter(sql_string, myConn);
            DataSet temp_dataset = new DataSet();
            da.Fill(temp_dataset, "controller_docname");
            foreach (DataRow row in temp_dataset.Tables["controller_docname"].Rows) {
                List<string> temp = new List<string>();
                temp.Add(row[0].ToString());
                temp.Add(row[1].ToString());
                controller_docname.Add(temp);
            }

            foreach (List<string> list_string in controller_docname) {
                this.toolStripStatusLabel2.Text = "Generating " + list_string[0] + ":" + list_string[1];
                this.Generate_Signal_By_CldTable(list_string[0], list_string[1], "1");
                this.toolStripStatusLabel2.Text = "Generating complete";
            }
        }

    }
}
