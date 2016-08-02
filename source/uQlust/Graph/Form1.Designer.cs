namespace Graph
{
    partial class AdvancedVersion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedVersion));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clusteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hushClusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uQlustTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hierarchicalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kmeansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rankingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsComparisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clusterAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.best5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareBest5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pairwiseComparisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.best5RmsdCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fractionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAdvanedUQlustToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gotoProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentinTextModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadExternalPleiadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.finishProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label11 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cluster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Measure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progress = new Graph.ProgressColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.progressColumn1 = new Graph.ProgressColumn();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clusteringToolStripMenuItem,
            this.rankingToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(300, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clusteringToolStripMenuItem
            // 
            this.clusteringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hushClusterToolStripMenuItem,
            this.uQlustTreeToolStripMenuItem,
            this.hierarchicalToolStripMenuItem1,
            this.kmeansToolStripMenuItem});
            this.clusteringToolStripMenuItem.Name = "clusteringToolStripMenuItem";
            this.clusteringToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.clusteringToolStripMenuItem.Text = "Clustering";
            // 
            // hushClusterToolStripMenuItem
            // 
            this.hushClusterToolStripMenuItem.Enabled = false;
            this.hushClusterToolStripMenuItem.Name = "hushClusterToolStripMenuItem";
            this.hushClusterToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.hushClusterToolStripMenuItem.Text = "uQlust:Hash/Rpart";
            this.hushClusterToolStripMenuItem.Click += new System.EventHandler(this.hushClusterToolStripMenuItem_Click);
            // 
            // uQlustTreeToolStripMenuItem
            // 
            this.uQlustTreeToolStripMenuItem.Name = "uQlustTreeToolStripMenuItem";
            this.uQlustTreeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.uQlustTreeToolStripMenuItem.Text = "uQlust:Tree";
            this.uQlustTreeToolStripMenuItem.Click += new System.EventHandler(this.uQlustTreeToolStripMenuItem_Click);
            // 
            // hierarchicalToolStripMenuItem1
            // 
            this.hierarchicalToolStripMenuItem1.Name = "hierarchicalToolStripMenuItem1";
            this.hierarchicalToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.hierarchicalToolStripMenuItem1.Text = "Hierarchical Trad.";
            this.hierarchicalToolStripMenuItem1.Click += new System.EventHandler(this.hierarchicalToolStripMenuItem1_Click);
            // 
            // kmeansToolStripMenuItem
            // 
            this.kmeansToolStripMenuItem.Enabled = false;
            this.kmeansToolStripMenuItem.Name = "kmeansToolStripMenuItem";
            this.kmeansToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.kmeansToolStripMenuItem.Text = "K-means";
            this.kmeansToolStripMenuItem.Click += new System.EventHandler(this.kmeansToolStripMenuItem_Click);
            // 
            // rankingToolStripMenuItem
            // 
            this.rankingToolStripMenuItem.Name = "rankingToolStripMenuItem";
            this.rankingToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.rankingToolStripMenuItem.Text = "Ranking";
            this.rankingToolStripMenuItem.Click += new System.EventHandler(this.otherToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resultsComparisionToolStripMenuItem,
            this.clusterAnalysisToolStripMenuItem,
            this.classificationToolStripMenuItem,
            this.best5ToolStripMenuItem,
            this.compareBest5ToolStripMenuItem,
            this.pairwiseComparisionToolStripMenuItem,
            this.best5RmsdCenterToolStripMenuItem,
            this.fractionToolStripMenuItem,
            this.distancesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // resultsComparisionToolStripMenuItem
            // 
            this.resultsComparisionToolStripMenuItem.Name = "resultsComparisionToolStripMenuItem";
            this.resultsComparisionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.resultsComparisionToolStripMenuItem.Text = "Results comparision";
            this.resultsComparisionToolStripMenuItem.Click += new System.EventHandler(this.resultsComparisionToolStripMenuItem_Click);
            // 
            // clusterAnalysisToolStripMenuItem
            // 
            this.clusterAnalysisToolStripMenuItem.Name = "clusterAnalysisToolStripMenuItem";
            this.clusterAnalysisToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.clusterAnalysisToolStripMenuItem.Text = "ClusterAnalysis";
            this.clusterAnalysisToolStripMenuItem.Click += new System.EventHandler(this.clusterAnalysisToolStripMenuItem_Click);
            // 
            // classificationToolStripMenuItem
            // 
            this.classificationToolStripMenuItem.Name = "classificationToolStripMenuItem";
            this.classificationToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.classificationToolStripMenuItem.Text = "Classification";
            this.classificationToolStripMenuItem.Click += new System.EventHandler(this.classificationToolStripMenuItem_Click);
            // 
            // best5ToolStripMenuItem
            // 
            this.best5ToolStripMenuItem.Name = "best5ToolStripMenuItem";
            this.best5ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.best5ToolStripMenuItem.Text = "Best 5";
            this.best5ToolStripMenuItem.Visible = false;
            this.best5ToolStripMenuItem.Click += new System.EventHandler(this.best5ToolStripMenuItem_Click);
            // 
            // compareBest5ToolStripMenuItem
            // 
            this.compareBest5ToolStripMenuItem.Name = "compareBest5ToolStripMenuItem";
            this.compareBest5ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.compareBest5ToolStripMenuItem.Text = "CompareBest5";
            this.compareBest5ToolStripMenuItem.Visible = false;
            this.compareBest5ToolStripMenuItem.Click += new System.EventHandler(this.compareBest5ToolStripMenuItem_Click);
            // 
            // pairwiseComparisionToolStripMenuItem
            // 
            this.pairwiseComparisionToolStripMenuItem.Name = "pairwiseComparisionToolStripMenuItem";
            this.pairwiseComparisionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.pairwiseComparisionToolStripMenuItem.Text = "Pairwise comparision";
            this.pairwiseComparisionToolStripMenuItem.Click += new System.EventHandler(this.pairwiseComparisionToolStripMenuItem_Click);
            // 
            // best5RmsdCenterToolStripMenuItem
            // 
            this.best5RmsdCenterToolStripMenuItem.Name = "best5RmsdCenterToolStripMenuItem";
            this.best5RmsdCenterToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.best5RmsdCenterToolStripMenuItem.Text = "Best5 rmsd center";
            this.best5RmsdCenterToolStripMenuItem.Visible = false;
            this.best5RmsdCenterToolStripMenuItem.Click += new System.EventHandler(this.best5RmsdCenterToolStripMenuItem_Click);
            // 
            // fractionToolStripMenuItem
            // 
            this.fractionToolStripMenuItem.Name = "fractionToolStripMenuItem";
            this.fractionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.fractionToolStripMenuItem.Text = "Fraction";
            this.fractionToolStripMenuItem.Click += new System.EventHandler(this.fractionToolStripMenuItem_Click);
            // 
            // distancesToolStripMenuItem
            // 
            this.distancesToolStripMenuItem.Name = "distancesToolStripMenuItem";
            this.distancesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.distancesToolStripMenuItem.Text = "Calculate distances";
            this.distancesToolStripMenuItem.Click += new System.EventHandler(this.distancesToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // setingsToolStripMenuItem
            // 
            this.setingsToolStripMenuItem.Name = "setingsToolStripMenuItem";
            this.setingsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.setingsToolStripMenuItem.Text = "Setings";
            this.setingsToolStripMenuItem.Click += new System.EventHandler(this.setingsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAdvanedUQlustToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // closeAdvanedUQlustToolStripMenuItem
            // 
            this.closeAdvanedUQlustToolStripMenuItem.Name = "closeAdvanedUQlustToolStripMenuItem";
            this.closeAdvanedUQlustToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.closeAdvanedUQlustToolStripMenuItem.Text = "Close Advaned uQlust";
            this.closeAdvanedUQlustToolStripMenuItem.Click += new System.EventHandler(this.closeAdvanedUQlustToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current data directiory:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gotoProcessToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveCurrentinTextModeToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.loadExternalToolStripMenuItem,
            this.loadExternalPleiadesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.finishProcessToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(217, 202);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.contextMenuStrip1_MouseUp);
            // 
            // gotoProcessToolStripMenuItem
            // 
            this.gotoProcessToolStripMenuItem.Name = "gotoProcessToolStripMenuItem";
            this.gotoProcessToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.gotoProcessToolStripMenuItem.Text = "Show results";
            this.gotoProcessToolStripMenuItem.Click += new System.EventHandler(this.gotoProcessToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.saveToolStripMenuItem.Text = "Save all (binary)";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveCurrentinTextModeToolStripMenuItem
            // 
            this.saveCurrentinTextModeToolStripMenuItem.Name = "saveCurrentinTextModeToolStripMenuItem";
            this.saveCurrentinTextModeToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.saveCurrentinTextModeToolStripMenuItem.Text = "Save current (in text mode)";
            this.saveCurrentinTextModeToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentinTextModeToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadToolStripMenuItem.Text = "Load (binary)";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // loadExternalToolStripMenuItem
            // 
            this.loadExternalToolStripMenuItem.Name = "loadExternalToolStripMenuItem";
            this.loadExternalToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadExternalToolStripMenuItem.Text = "Load external";
            this.loadExternalToolStripMenuItem.Click += new System.EventHandler(this.loadExternalToolStripMenuItem_Click);
            // 
            // loadExternalPleiadesToolStripMenuItem
            // 
            this.loadExternalPleiadesToolStripMenuItem.Name = "loadExternalPleiadesToolStripMenuItem";
            this.loadExternalPleiadesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadExternalPleiadesToolStripMenuItem.Text = "Load external pleiades";
            this.loadExternalPleiadesToolStripMenuItem.Visible = false;
            this.loadExternalPleiadesToolStripMenuItem.Click += new System.EventHandler(this.loadExternalPleiadesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItem1.Text = "Load external PconsD";
            this.toolStripMenuItem1.Visible = false;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // finishProcessToolStripMenuItem
            // 
            this.finishProcessToolStripMenuItem.Name = "finishProcessToolStripMenuItem";
            this.finishProcessToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.finishProcessToolStripMenuItem.Text = "Close current";
            this.finishProcessToolStripMenuItem.Click += new System.EventHandler(this.finishProcessToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Clustering results";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.process,
            this.Cluster,
            this.Measure,
            this.Column1,
            this.time,
            this.Column2,
            this.Progress,
            this.Column3});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 254);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(617, 106);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // process
            // 
            this.process.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.process.HeaderText = "Name";
            this.process.Name = "process";
            this.process.ReadOnly = true;
            // 
            // Cluster
            // 
            this.Cluster.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cluster.HeaderText = "Cluster";
            this.Cluster.Name = "Cluster";
            this.Cluster.ReadOnly = true;
            // 
            // Measure
            // 
            this.Measure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Measure.HeaderText = "Measure";
            this.Measure.Name = "Measure";
            this.Measure.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Dir Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // time
            // 
            this.time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.time.HeaderText = "time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Peek Memory";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Progress
            // 
            this.Progress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Progress.HeaderText = "Progress [%]";
            this.Progress.Name = "Progress";
            this.Progress.ProgressBarColor = System.Drawing.Color.Gray;
            this.Progress.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Vis";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(4, 116);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(397, 108);
            this.listBox1.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(468, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(468, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(377, 78);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(69, 17);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.Text = "DCD files";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(18, 78);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(101, 17);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Data Directories";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(468, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Remove All";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(582, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "List of minor errors";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(188, 78);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(141, 17);
            this.radioButton3.TabIndex = 19;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "File with list of directories";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(453, 28);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(164, 17);
            this.radioButton4.TabIndex = 20;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "File with precomputed profiles";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "INPUT MODE: ";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(90, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 23;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // progressColumn1
            // 
            this.progressColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.progressColumn1.HeaderText = "Progress [%]";
            this.progressColumn1.Name = "progressColumn1";
            this.progressColumn1.ProgressBarColor = System.Drawing.Color.Gray;
            // 
            // AdvancedVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(617, 365);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdvancedVersion";
            this.Text = "uQlust";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdvancedVersion_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setingsToolStripMenuItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem finishProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gotoProcessToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsComparisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clusteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kmeansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clusterAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hushClusterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classificationToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ToolStripMenuItem best5ToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.ToolStripMenuItem compareBest5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pairwiseComparisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem best5RmsdCenterToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem fractionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadExternalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadExternalPleiadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem distancesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentinTextModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rankingToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn process;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cluster;
        private System.Windows.Forms.DataGridViewTextBoxColumn Measure;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private ProgressColumn Progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAdvanedUQlustToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uQlustTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hierarchicalToolStripMenuItem1;
        private ProgressColumn progressColumn1;


    }
}

