using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using TDK.Core.Logic.BLL;
using TDK.Core.Logic.DAL;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectManager
{
    public partial class Form1 : Form
    {
        private ISession session = null;
        // 对象操作实例
        private PrjManager manager = null;
        // 业务逻辑层实例
        private BllManager bll = null;

        public Form1()
        {
            InitializeComponent();
            this.Select();

            this.dlgSetOutFolder.SelectedPath = Application.StartupPath;
        }

        /// <summary>
        /// 初始化 Session 对象
        /// </summary>
        private void InitializeSession()
        {
            if (session != null)
            {
                return;
            }

            if (!File.Exists(this.txtDatabaseFile.Text))
            {
                throw new FileNotFoundException("数据库文件不存在或已丢失！");
            }

            //string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.txtDatabase.Text.Trim();
            //IDbConnection conn = new OleDbConnection(connString);

            Configuration cfg = new Configuration().Configure(Application.StartupPath + "\\hibernate_config.xml");
            cfg.Properties[NHibernate.Cfg.Environment.ConnectionString] = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.txtDatabaseFile.Text.Trim();
            ISessionFactory factory = cfg.BuildSessionFactory();
            session = factory.OpenSession();
        }

        private void InitializeProjectManager()
        {
            if (manager != null)
            {
                return;
            }

            if (session == null)
            {
                InitializeSession();
            }

            manager = new PrjManager(session);
        }

        private void InitializeBllManager()
        {
            if (bll != null)
            {
                return;
            }

            bll = new BllManager(manager);
        }

        // 选择数据库文件
        private void btnSelectDatabaseFile_Click(object sender, EventArgs e)
        {
            if (this.dlgSelectDatabaseFile.ShowDialog() == DialogResult.OK)
            {
                this.txtDatabaseFile.Text = this.dlgSelectDatabaseFile.FileName;
            }
        }

        // 设置输出路径
        private void btnSelectOutPath_Click(object sender, EventArgs e)
        {
            if (this.dlgSetOutFolder.ShowDialog() == DialogResult.OK)
            {
                this.txtOutPath.Text = this.dlgSetOutFolder.SelectedPath;
            }
        }

        // 加载 Dpu 目录树
        private void btnLoadCatalog_Click(object sender, EventArgs e)
        {
            if (this.txtDatabaseFile.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择数据库文件！");
                return;
            }
            else if (!File.Exists(this.txtDatabaseFile.Text.Trim()))
            {
                MessageBox.Show("数据库文件不存在！");
                return;
            }

            this.treDpuCatalog.Nodes.Clear();

            InitializeProjectManager();

            #region 加载 Dpu 目录树
            // 加载 Project 列表
            IList<Prj_Project> projects = manager.ProjectCRUD.GetPrj_Projects();
            foreach (Prj_Project project in projects)
            {
                NodeTag tagProject = new NodeTag();
                tagProject.type = NodeType.Project;
                tagProject.id = project.ID;

                TreeNode nodeProject = new TreeNode();
                nodeProject.Tag = tagProject;
                nodeProject.Text = NodeType.Project.ToString() + ":" + project.ProjectName;
                this.treDpuCatalog.Nodes.Add(nodeProject);

                // 加载 Network 列表
                foreach (Prj_Network network in manager.NetworkCRUD.GetPrj_Networks_By_Prj_Project_ID(project.ID))
                {
                    NodeTag tagNetwork = new NodeTag();
                    tagNetwork.type = NodeType.Network;
                    tagNetwork.id = network.ID;

                    TreeNode nodeNetwork = new TreeNode();
                    nodeNetwork.Tag = tagNetwork;
                    nodeNetwork.Text = NodeType.Network.ToString() + ":" + network.NetworkName;
                    nodeProject.Nodes.Add(nodeNetwork);

                    // 加载 Unit 列表
                    foreach (Prj_Unit unit in manager.UnitCRUD.GetPrj_Units_By_Prj_Network_ID(network.ID))
                    {
                        NodeTag tagUnit = new NodeTag();
                        tagUnit.type = NodeType.Unit;
                        tagUnit.id = unit.ID;

                        TreeNode nodeUnit = new TreeNode();
                        nodeUnit.Tag = tagUnit;
                        nodeUnit.Text = NodeType.Unit.ToString() + ":" + unit.UnitName;
                        nodeNetwork.Nodes.Add(nodeUnit);

                        // 加载 Controller 列表
                        foreach (Prj_Controller controller in manager.ControllerCRUD.GetPrj_Controllers_By_Prj_Unit_ID(unit.ID))
                        {
                            NodeTag tagController = new NodeTag();
                            tagController.type = NodeType.Controller;
                            tagController.id = controller.ID;

                            TreeNode nodeController = new TreeNode();
                            nodeController.Tag = tagController;
                            nodeController.Text = NodeType.Controller.ToString() + ":" + controller.ControllerName;
                            nodeUnit.Nodes.Add(nodeController);
                        }
                    }
                }
            }
            #endregion

            this.btnGenerateCatalog.Enabled = true;
            this.gropGeneratePages.Enabled = true;
        }

        private TreeViewAction actionTemp = TreeViewAction.Unknown;
        // 选中或取消选中 Dpu 时执行的操作
        private void treDpuCatalog_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // 改变子节点的选中状态
            if ((e.Action == TreeViewAction.ByMouse || actionTemp == TreeViewAction.ByMouse) && e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    actionTemp = TreeViewAction.ByMouse;
                    node.Checked = e.Node.Checked;
                }
            }

            actionTemp = TreeViewAction.Unknown;

            // 改变父节点的选中状态
            if (e.Node.Parent != null)
            {
                if (!e.Node.Checked)
                {
                    bool hasChecked = false;

                    foreach (TreeNode node in e.Node.Parent.Nodes)
                    {
                        if (node.Checked)
                        {
                            hasChecked = true;
                            break;
                        }
                    }

                    e.Node.Parent.Checked = hasChecked;
                }
                else
                {
                    e.Node.Parent.Checked = true;
                }
            }

            if ((e.Node.Tag as NodeTag).type == NodeType.Controller)
            {
                if (e.Node.Checked)
                {
                    // 加载选中 Dpu 下的文档列表

                    BllManager bll = new BllManager(manager);

                    IList<Prj_Document> documents = bll.manager.DocumentCRUD.GetPrj_Documents_By_Prj_Controller_ID((e.Node.Tag as NodeTag).id);
                    foreach (Prj_Document doc in documents)
                    {
                        ListViewItem item = new ListViewItem(
                            new string[] { 
                                doc.DocumentName.Substring(0, doc.DocumentName.IndexOf('-')).PadLeft(3, '0') + "-"
                                + doc.DocumentName.Substring(doc.DocumentName.IndexOf('-') + 1).PadLeft(3, '0'),
                                doc.DocumentCaption });
                        item.Tag = doc;
                        item.ToolTipText = doc.DocumentCaption;
                        this.lstPages.Items.Add(item);
                    }
                }
                else
                {
                    // 移除对应 Dpu 的文档列表

                    IList<int> mastLeavedId = new List<int>();

                    for (int i = 0; i < this.lstPages.Items.Count; )
                    {
                        if (((Prj_Document)this.lstPages.Items[i].Tag).Prj_Controller_ID == (((NodeTag)e.Node.Tag).id))
                        {
                            this.lstPages.Items.Remove(this.lstPages.Items[i]);
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }
        }

        // 选中全部
        private void chkCheckedAll_CheckedChanged(object sender, EventArgs e)
        {
            this.lstPages.ItemChecked -= new ItemCheckedEventHandler(lstPages_ItemChecked);
            foreach (ListViewItem item in this.lstPages.Items)
            {
                item.Checked = this.chkCheckedAll.Checked;
            }
            this.lstPages.ItemChecked += new ItemCheckedEventHandler(lstPages_ItemChecked);
        }

        // 页面列表中的项选中状态改变时
        private void lstPages_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.chkCheckedAll.CheckedChanged -= new EventHandler(chkCheckedAll_CheckedChanged);
            if (this.lstPages.CheckedItems.Count == 0)
            {
                this.chkCheckedAll.CheckState = CheckState.Unchecked;
            }
            else if (this.lstPages.CheckedItems.Count == this.lstPages.Items.Count)
            {
                this.chkCheckedAll.CheckState = CheckState.Checked;
            }
            else
            {
                this.chkCheckedAll.CheckState = CheckState.Indeterminate;
            }
            this.chkCheckedAll.CheckedChanged += new EventHandler(chkCheckedAll_CheckedChanged);
        }

        // 生成目录文件
        private void btnGenerateCatalog_Click(object sender, EventArgs e)
        {
            if (!OutPathValidated())
            {
                return;
            }

            if (manager == null)
            {
                MessageBox.Show("请先加载数据！");
                return;
            }

            if (bll == null)
            {
                InitializeBllManager();
            }

            GraphicsDocument.GenerateXinHuaPageList(bll, this.txtOutPath.Text);

            MessageBox.Show("生成页面目录文件结束！", "完成");
        }

        // 生成页面文件
        private void btnGenerateSheets_Click(object sender, EventArgs e)
        {
            if (!OutPathValidated())
            {
                return;
            }

            if (manager == null)
            {
                MessageBox.Show("请先加载数据！");
                return;
            }

            if (bll == null)
            {
                InitializeBllManager();
            }

            IList<Prj_Document> documents = null;
            if (rbtnGenerateAllSheets.Checked == false)
            {
                documents = new List<Prj_Document>();
                foreach (ListViewItem item in this.lstPages.CheckedItems)
                {
                    if (item.Checked)
                    {
                        documents.Add(item.Tag as Prj_Document);
                    }
                }
            }

            backgroundWorkerGeneratePages.RunWorkerAsync(new GeneratePagesEventArgs(rbtnGenerateAllSheets.Checked, this.txtOutPath.Text.Trim(), documents));
        }

        // 验证输出目录
        private bool OutPathValidated()
        {
            if (this.txtOutPath.Text.Trim() == null)
            {
                MessageBox.Show("请设置输出路径！");
                return false;
            }
            else if (!Path.IsPathRooted(this.txtOutPath.Text.Trim()))
            {
                MessageBox.Show("输出路径格式错误！");
                return false;
            }
            else if (!Directory.Exists(this.txtOutPath.Text.Trim()))
            {
                if (MessageBox.Show("提示", "输出目录不存在，是否创建？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return false;
                }

                Directory.CreateDirectory(this.txtOutPath.Text.Trim());
            }

            return true;
        }

        private void backgroundWorkerGeneratePages_DoWork(object sender, DoWorkEventArgs e)
        {
            GeneratePagesEventArgs args = e.Argument as GeneratePagesEventArgs;

            if (args.GenerateAllDocuments)
            {
                GraphicsDocument.GenerateXinHuaProjects(bll, args.OutPath);
            }
            else
            {
                foreach (Prj_Document document in args.Documents)
                {
                    GraphicsDocument.GenerateXinHuaSheets(bll, document, args.OutPath);
                }
            }
        }

        private void backgroundWorkerGeneratePages_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("生成页面文件结束！", "完成");
        }

        class GeneratePagesEventArgs : EventArgs
        {
            public GeneratePagesEventArgs(bool generateAllDocuments, string outPath, IList<Prj_Document> documents)
            {
                this.generateAllDocuments = generateAllDocuments;
                this.outPath = outPath;
                this.documents = documents;
            }

            private bool generateAllDocuments;
            /// <summary>
            /// 生成全部文档
            /// </summary>
            public bool GenerateAllDocuments
            {
                get
                {
                    return generateAllDocuments;
                }
                set
                {
                    generateAllDocuments = value;
                }
            }

            private string outPath;
            /// <summary>
            /// 项目输出路径
            /// </summary>
            public string OutPath
            {
                get
                {
                    return outPath;
                }
                set
                {
                    outPath = value;
                }
            }

            private IList<Prj_Document> documents;
            /// <summary>
            /// 选中的文档项，仅在 GenerateAllDocuments 为 True 时有效
            /// </summary>
            public IList<Prj_Document> Documents
            {
                get
                {
                    return documents;
                }
                set
                {
                    documents = value;
                }
            }
        }

        private void txtOutPath_TextChanged(object sender, EventArgs e)
        {
            this.txtCatalogOutPath.Text = this.txtOutPath.Text.Trim() + "\\data\\xinhua";
            this.txtPagesOutPath.Text = this.txtOutPath.Text.Trim() + "\\data\\xinhua\\Pages";
        }
    }

    internal enum NodeType
    {
        /// <summary>
        /// 项目
        /// </summary>
        Project,
        Network,
        Unit,
        /// <summary>
        /// 控制器
        /// </summary>
        Controller,
        Document,
        /// <summary>
        /// 页面
        /// </summary>
        Sheet,
    }

    /// <summary>
    /// 描述一个treeview中的node
    /// </summary>
    internal class NodeTag
    {
        /// <summary>
        /// Node代表的对象的类型
        /// </summary>
        public NodeType type;
        /// <summary>
        /// Node代表的记录的ID
        /// </summary>
        public int id;
    }

}