namespace ProjectManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.treDpuCatalog = new System.Windows.Forms.TreeView();
            this.btnLoadCatalog = new System.Windows.Forms.Button();
            this.lstPages = new System.Windows.Forms.ListView();
            this.colDocumentName = new System.Windows.Forms.ColumnHeader();
            this.colDocumentCaption = new System.Windows.Forms.ColumnHeader();
            this.lblDatabaseFileTag = new System.Windows.Forms.Label();
            this.lblOutPathTag = new System.Windows.Forms.Label();
            this.txtDatabaseFile = new System.Windows.Forms.TextBox();
            this.txtOutPath = new System.Windows.Forms.TextBox();
            this.btnSelectDatabaseFile = new System.Windows.Forms.Button();
            this.btnSelectOutPath = new System.Windows.Forms.Button();
            this.btnGenerateSheets = new System.Windows.Forms.Button();
            this.btnGenerateCatalog = new System.Windows.Forms.Button();
            this.gropGeneratePages = new System.Windows.Forms.GroupBox();
            this.rbtnGenerateAllSheets = new System.Windows.Forms.RadioButton();
            this.rbtnGenerateCheckedSheets = new System.Windows.Forms.RadioButton();
            this.dlgSetOutFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgSelectDatabaseFile = new System.Windows.Forms.OpenFileDialog();
            this.chkCheckedAll = new System.Windows.Forms.CheckBox();
            this.txtCatalogOutPath = new System.Windows.Forms.TextBox();
            this.lblCatalogOutPathTag = new System.Windows.Forms.Label();
            this.txtPagesOutPath = new System.Windows.Forms.TextBox();
            this.lblPagesOutPathTag = new System.Windows.Forms.Label();
            this.backgroundWorkerGeneratePages = new System.ComponentModel.BackgroundWorker();
            this.gropGeneratePages.SuspendLayout();
            this.SuspendLayout();
            // 
            // treDpuCatalog
            // 
            this.treDpuCatalog.CheckBoxes = true;
            this.treDpuCatalog.HideSelection = false;
            this.treDpuCatalog.HotTracking = true;
            this.treDpuCatalog.Location = new System.Drawing.Point(20, 170);
            this.treDpuCatalog.Name = "treDpuCatalog";
            this.treDpuCatalog.Size = new System.Drawing.Size(260, 460);
            this.treDpuCatalog.TabIndex = 0;
            this.treDpuCatalog.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treDpuCatalog_AfterCheck);
            // 
            // btnLoadCatalog
            // 
            this.btnLoadCatalog.Location = new System.Drawing.Point(20, 140);
            this.btnLoadCatalog.Name = "btnLoadCatalog";
            this.btnLoadCatalog.Size = new System.Drawing.Size(100, 21);
            this.btnLoadCatalog.TabIndex = 1;
            this.btnLoadCatalog.Text = "加载Dpu列表";
            this.btnLoadCatalog.UseVisualStyleBackColor = true;
            this.btnLoadCatalog.Click += new System.EventHandler(this.btnLoadCatalog_Click);
            // 
            // lstPages
            // 
            this.lstPages.CheckBoxes = true;
            this.lstPages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDocumentName,
            this.colDocumentCaption});
            this.lstPages.FullRowSelect = true;
            this.lstPages.GridLines = true;
            this.lstPages.Location = new System.Drawing.Point(290, 170);
            this.lstPages.Name = "lstPages";
            this.lstPages.ShowItemToolTips = true;
            this.lstPages.Size = new System.Drawing.Size(320, 460);
            this.lstPages.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstPages.TabIndex = 2;
            this.lstPages.UseCompatibleStateImageBehavior = false;
            this.lstPages.View = System.Windows.Forms.View.Details;
            this.lstPages.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstPages_ItemChecked);
            // 
            // colDocumentName
            // 
            this.colDocumentName.Text = "DocumentName";
            this.colDocumentName.Width = 90;
            // 
            // colDocumentCaption
            // 
            this.colDocumentCaption.Text = "DocumentCaption";
            this.colDocumentCaption.Width = 206;
            // 
            // lblDatabaseFileTag
            // 
            this.lblDatabaseFileTag.AutoSize = true;
            this.lblDatabaseFileTag.Location = new System.Drawing.Point(44, 24);
            this.lblDatabaseFileTag.Name = "lblDatabaseFileTag";
            this.lblDatabaseFileTag.Size = new System.Drawing.Size(65, 12);
            this.lblDatabaseFileTag.TabIndex = 3;
            this.lblDatabaseFileTag.Text = "数 据 库：";
            // 
            // lblOutPathTag
            // 
            this.lblOutPathTag.AutoSize = true;
            this.lblOutPathTag.Location = new System.Drawing.Point(44, 51);
            this.lblOutPathTag.Name = "lblOutPathTag";
            this.lblOutPathTag.Size = new System.Drawing.Size(65, 12);
            this.lblOutPathTag.TabIndex = 4;
            this.lblOutPathTag.Text = "输出路径：";
            // 
            // txtDatabaseFile
            // 
            this.txtDatabaseFile.Location = new System.Drawing.Point(110, 20);
            this.txtDatabaseFile.Name = "txtDatabaseFile";
            this.txtDatabaseFile.Size = new System.Drawing.Size(500, 21);
            this.txtDatabaseFile.TabIndex = 5;
            // 
            // txtOutPath
            // 
            this.txtOutPath.Location = new System.Drawing.Point(110, 47);
            this.txtOutPath.Name = "txtOutPath";
            this.txtOutPath.Size = new System.Drawing.Size(500, 21);
            this.txtOutPath.TabIndex = 6;
            this.txtOutPath.TextChanged += new System.EventHandler(this.txtOutPath_TextChanged);
            // 
            // btnSelectDatabaseFile
            // 
            this.btnSelectDatabaseFile.Location = new System.Drawing.Point(636, 20);
            this.btnSelectDatabaseFile.Name = "btnSelectDatabaseFile";
            this.btnSelectDatabaseFile.Size = new System.Drawing.Size(100, 21);
            this.btnSelectDatabaseFile.TabIndex = 7;
            this.btnSelectDatabaseFile.Text = "选择数据库";
            this.btnSelectDatabaseFile.UseVisualStyleBackColor = true;
            this.btnSelectDatabaseFile.Click += new System.EventHandler(this.btnSelectDatabaseFile_Click);
            // 
            // btnSelectOutPath
            // 
            this.btnSelectOutPath.Location = new System.Drawing.Point(636, 47);
            this.btnSelectOutPath.Name = "btnSelectOutPath";
            this.btnSelectOutPath.Size = new System.Drawing.Size(100, 21);
            this.btnSelectOutPath.TabIndex = 8;
            this.btnSelectOutPath.Text = "设置输出路径";
            this.btnSelectOutPath.UseVisualStyleBackColor = true;
            this.btnSelectOutPath.Click += new System.EventHandler(this.btnSelectOutPath_Click);
            // 
            // btnGenerateSheets
            // 
            this.btnGenerateSheets.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGenerateSheets.Location = new System.Drawing.Point(15, 80);
            this.btnGenerateSheets.Name = "btnGenerateSheets";
            this.btnGenerateSheets.Size = new System.Drawing.Size(100, 21);
            this.btnGenerateSheets.TabIndex = 9;
            this.btnGenerateSheets.Text = "生成页面文件";
            this.btnGenerateSheets.UseVisualStyleBackColor = true;
            this.btnGenerateSheets.Click += new System.EventHandler(this.btnGenerateSheets_Click);
            // 
            // btnGenerateCatalog
            // 
            this.btnGenerateCatalog.Enabled = false;
            this.btnGenerateCatalog.Location = new System.Drawing.Point(636, 170);
            this.btnGenerateCatalog.Name = "btnGenerateCatalog";
            this.btnGenerateCatalog.Size = new System.Drawing.Size(100, 21);
            this.btnGenerateCatalog.TabIndex = 10;
            this.btnGenerateCatalog.Text = "生成目录文件";
            this.btnGenerateCatalog.UseVisualStyleBackColor = true;
            this.btnGenerateCatalog.Click += new System.EventHandler(this.btnGenerateCatalog_Click);
            // 
            // gropGeneratePages
            // 
            this.gropGeneratePages.Controls.Add(this.rbtnGenerateAllSheets);
            this.gropGeneratePages.Controls.Add(this.rbtnGenerateCheckedSheets);
            this.gropGeneratePages.Controls.Add(this.btnGenerateSheets);
            this.gropGeneratePages.Enabled = false;
            this.gropGeneratePages.Location = new System.Drawing.Point(621, 220);
            this.gropGeneratePages.Name = "gropGeneratePages";
            this.gropGeneratePages.Size = new System.Drawing.Size(130, 120);
            this.gropGeneratePages.TabIndex = 11;
            this.gropGeneratePages.TabStop = false;
            this.gropGeneratePages.Text = "生成页面文件";
            // 
            // rbtnGenerateAllSheets
            // 
            this.rbtnGenerateAllSheets.AutoSize = true;
            this.rbtnGenerateAllSheets.Checked = true;
            this.rbtnGenerateAllSheets.Location = new System.Drawing.Point(12, 52);
            this.rbtnGenerateAllSheets.Name = "rbtnGenerateAllSheets";
            this.rbtnGenerateAllSheets.Size = new System.Drawing.Size(95, 16);
            this.rbtnGenerateAllSheets.TabIndex = 11;
            this.rbtnGenerateAllSheets.TabStop = true;
            this.rbtnGenerateAllSheets.Text = "生成全部页面";
            this.rbtnGenerateAllSheets.UseVisualStyleBackColor = true;
            // 
            // rbtnGenerateCheckedSheets
            // 
            this.rbtnGenerateCheckedSheets.AutoSize = true;
            this.rbtnGenerateCheckedSheets.Location = new System.Drawing.Point(12, 30);
            this.rbtnGenerateCheckedSheets.Name = "rbtnGenerateCheckedSheets";
            this.rbtnGenerateCheckedSheets.Size = new System.Drawing.Size(107, 16);
            this.rbtnGenerateCheckedSheets.TabIndex = 10;
            this.rbtnGenerateCheckedSheets.Text = "仅生成选中部分";
            this.rbtnGenerateCheckedSheets.UseVisualStyleBackColor = true;
            // 
            // dlgSetOutFolder
            // 
            this.dlgSetOutFolder.Description = "请选择输出目录";
            // 
            // dlgSelectDatabaseFile
            // 
            this.dlgSelectDatabaseFile.Filter = "MIcrosoft Office Access 数据库|*.mdb";
            this.dlgSelectDatabaseFile.ValidateNames = false;
            // 
            // chkCheckedAll
            // 
            this.chkCheckedAll.AutoSize = true;
            this.chkCheckedAll.Location = new System.Drawing.Point(291, 143);
            this.chkCheckedAll.Name = "chkCheckedAll";
            this.chkCheckedAll.Size = new System.Drawing.Size(72, 16);
            this.chkCheckedAll.TabIndex = 12;
            this.chkCheckedAll.Text = "选中全部";
            this.chkCheckedAll.UseVisualStyleBackColor = true;
            this.chkCheckedAll.CheckedChanged += new System.EventHandler(this.chkCheckedAll_CheckedChanged);
            // 
            // txtCatalogOutPath
            // 
            this.txtCatalogOutPath.Location = new System.Drawing.Point(110, 74);
            this.txtCatalogOutPath.Name = "txtCatalogOutPath";
            this.txtCatalogOutPath.ReadOnly = true;
            this.txtCatalogOutPath.Size = new System.Drawing.Size(500, 21);
            this.txtCatalogOutPath.TabIndex = 14;
            // 
            // lblCatalogOutPathTag
            // 
            this.lblCatalogOutPathTag.AutoSize = true;
            this.lblCatalogOutPathTag.Location = new System.Drawing.Point(20, 78);
            this.lblCatalogOutPathTag.Name = "lblCatalogOutPathTag";
            this.lblCatalogOutPathTag.Size = new System.Drawing.Size(89, 12);
            this.lblCatalogOutPathTag.TabIndex = 13;
            this.lblCatalogOutPathTag.Text = "目录输出路径：";
            // 
            // txtPagesOutPath
            // 
            this.txtPagesOutPath.Location = new System.Drawing.Point(110, 101);
            this.txtPagesOutPath.Name = "txtPagesOutPath";
            this.txtPagesOutPath.ReadOnly = true;
            this.txtPagesOutPath.Size = new System.Drawing.Size(500, 21);
            this.txtPagesOutPath.TabIndex = 16;
            // 
            // lblPagesOutPathTag
            // 
            this.lblPagesOutPathTag.AutoSize = true;
            this.lblPagesOutPathTag.Location = new System.Drawing.Point(20, 105);
            this.lblPagesOutPathTag.Name = "lblPagesOutPathTag";
            this.lblPagesOutPathTag.Size = new System.Drawing.Size(89, 12);
            this.lblPagesOutPathTag.TabIndex = 15;
            this.lblPagesOutPathTag.Text = "页面输出路径：";
            // 
            // backgroundWorkerGeneratePages
            // 
            this.backgroundWorkerGeneratePages.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGeneratePages_DoWork);
            this.backgroundWorkerGeneratePages.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGeneratePages_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 648);
            this.Controls.Add(this.txtPagesOutPath);
            this.Controls.Add(this.lblPagesOutPathTag);
            this.Controls.Add(this.txtCatalogOutPath);
            this.Controls.Add(this.lblCatalogOutPathTag);
            this.Controls.Add(this.chkCheckedAll);
            this.Controls.Add(this.gropGeneratePages);
            this.Controls.Add(this.btnGenerateCatalog);
            this.Controls.Add(this.btnSelectOutPath);
            this.Controls.Add(this.btnSelectDatabaseFile);
            this.Controls.Add(this.txtOutPath);
            this.Controls.Add(this.txtDatabaseFile);
            this.Controls.Add(this.lblOutPathTag);
            this.Controls.Add(this.lblDatabaseFileTag);
            this.Controls.Add(this.lstPages);
            this.Controls.Add(this.btnLoadCatalog);
            this.Controls.Add(this.treDpuCatalog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.gropGeneratePages.ResumeLayout(false);
            this.gropGeneratePages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treDpuCatalog;
        private System.Windows.Forms.Button btnLoadCatalog;
        private System.Windows.Forms.ListView lstPages;
        private System.Windows.Forms.Label lblDatabaseFileTag;
        private System.Windows.Forms.Label lblOutPathTag;
        private System.Windows.Forms.TextBox txtDatabaseFile;
        private System.Windows.Forms.TextBox txtOutPath;
        private System.Windows.Forms.Button btnSelectDatabaseFile;
        private System.Windows.Forms.Button btnSelectOutPath;
        private System.Windows.Forms.Button btnGenerateSheets;
        private System.Windows.Forms.Button btnGenerateCatalog;
        private System.Windows.Forms.GroupBox gropGeneratePages;
        private System.Windows.Forms.RadioButton rbtnGenerateCheckedSheets;
        private System.Windows.Forms.RadioButton rbtnGenerateAllSheets;
        private System.Windows.Forms.FolderBrowserDialog dlgSetOutFolder;
        private System.Windows.Forms.OpenFileDialog dlgSelectDatabaseFile;
        private System.Windows.Forms.ColumnHeader colDocumentName;
        private System.Windows.Forms.ColumnHeader colDocumentCaption;
        private System.Windows.Forms.CheckBox chkCheckedAll;
        private System.Windows.Forms.TextBox txtCatalogOutPath;
        private System.Windows.Forms.Label lblCatalogOutPathTag;
        private System.Windows.Forms.TextBox txtPagesOutPath;
        private System.Windows.Forms.Label lblPagesOutPathTag;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGeneratePages;
    }
}

