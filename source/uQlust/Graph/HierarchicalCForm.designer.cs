namespace Graph
{
    partial class HierarchicalCForm
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
            this.label19 = new System.Windows.Forms.Label();
            this.FastHClusterRadio = new System.Windows.Forms.RadioButton();
            this.maxRepeat = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.DBIndex = new System.Windows.Forms.NumericUpDown();
            this.maxK = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.AglomRadio = new System.Windows.Forms.RadioButton();
            this.KmeansRadio = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.jury1DSetup3 = new Graph.jury1DSetup();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.userHash1 = new Graph.UserHash();
            this.distanceControl1 = new Graph.DistanceControl();
            this.numericBox1 = new Graph.NumericBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.maxRepeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 79);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 13);
            this.label19.TabIndex = 38;
            this.label19.Text = "Number of initial nodes";
            this.label19.Visible = false;
            // 
            // FastHClusterRadio
            // 
            this.FastHClusterRadio.AutoSize = true;
            this.FastHClusterRadio.Location = new System.Drawing.Point(460, 12);
            this.FastHClusterRadio.Name = "FastHClusterRadio";
            this.FastHClusterRadio.Size = new System.Drawing.Size(138, 17);
            this.FastHClusterRadio.TabIndex = 2;
            this.FastHClusterRadio.Text = "Fast Hierarchical cluster";
            this.FastHClusterRadio.UseVisualStyleBackColor = true;
            this.FastHClusterRadio.CheckedChanged += new System.EventHandler(this.FastHClusterRadio_CheckedChanged);
            // 
            // maxRepeat
            // 
            this.maxRepeat.Location = new System.Drawing.Point(448, 182);
            this.maxRepeat.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxRepeat.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.maxRepeat.Name = "maxRepeat";
            this.maxRepeat.Size = new System.Drawing.Size(120, 20);
            this.maxRepeat.TabIndex = 9;
            this.maxRepeat.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.maxRepeat.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Repeat time for each level: ";
            // 
            // DBIndex
            // 
            this.DBIndex.DecimalPlaces = 1;
            this.DBIndex.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DBIndex.Location = new System.Drawing.Point(448, 95);
            this.DBIndex.Name = "DBIndex";
            this.DBIndex.Size = new System.Drawing.Size(120, 20);
            this.DBIndex.TabIndex = 7;
            this.DBIndex.Value = new decimal(new int[] {
            14,
            0,
            0,
            65536});
            this.DBIndex.Visible = false;
            // 
            // maxK
            // 
            this.maxK.Location = new System.Drawing.Point(448, 151);
            this.maxK.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxK.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxK.Name = "maxK";
            this.maxK.Size = new System.Drawing.Size(120, 20);
            this.maxK.TabIndex = 8;
            this.maxK.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.maxK.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(329, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Maximal number of k:  ";
            // 
            // threshold
            // 
            this.threshold.Location = new System.Drawing.Point(448, 123);
            this.threshold.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.threshold.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(120, 20);
            this.threshold.TabIndex = 8;
            this.threshold.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.threshold.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(253, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(189, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Minimal number of structures in cluster:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(270, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(172, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Threshold of Davies Bouldin Index:";
            // 
            // AglomRadio
            // 
            this.AglomRadio.AutoSize = true;
            this.AglomRadio.Location = new System.Drawing.Point(162, 12);
            this.AglomRadio.Name = "AglomRadio";
            this.AglomRadio.Size = new System.Drawing.Size(86, 17);
            this.AglomRadio.TabIndex = 1;
            this.AglomRadio.Text = "Aglomerative";
            this.AglomRadio.UseVisualStyleBackColor = true;
            this.AglomRadio.CheckedChanged += new System.EventHandler(this.AglomRadio_CheckedChanged);
            // 
            // KmeansRadio
            // 
            this.KmeansRadio.AutoSize = true;
            this.KmeansRadio.Location = new System.Drawing.Point(308, 12);
            this.KmeansRadio.Name = "KmeansRadio";
            this.KmeansRadio.Size = new System.Drawing.Size(125, 17);
            this.KmeansRadio.TabIndex = 3;
            this.KmeansRadio.Text = "Hierarchical K-means";
            this.KmeansRadio.UseVisualStyleBackColor = true;
            this.KmeansRadio.CheckedChanged += new System.EventHandler(this.KmeansRadio_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(33, 560);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(487, 560);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(21, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(80, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "uQlust:Tree";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(106, 317);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Linkage type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.jury1DSetup3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 115);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dividing options";
            this.groupBox1.Visible = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(282, 94);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(175, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of iterations";
            // 
            // jury1DSetup3
            // 
            this.jury1DSetup3.Location = new System.Drawing.Point(150, 13);
            this.jury1DSetup3.Name = "jury1DSetup3";
            this.jury1DSetup3.profileName = null;
            this.jury1DSetup3.Size = new System.Drawing.Size(220, 75);
            this.jury1DSetup3.TabIndex = 13;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(9, 92);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(132, 17);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Use 2-means to iterate";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(9, 48);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(123, 17);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Hamming consensus";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(55, 17);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1Djury";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // userHash1
            // 
            this.userHash1.distThres = 1;
            this.userHash1.Location = new System.Drawing.Point(12, 65);
            this.userHash1.Name = "userHash1";
            this.userHash1.nodesNumber = 1;
            this.userHash1.profRefFile = null;
            this.userHash1.profRegFile = null;
            this.userHash1.reg = false;
            this.userHash1.Size = new System.Drawing.Size(556, 245);
            this.userHash1.TabIndex = 50;
            this.userHash1.winSize = 7;
            // 
            // distanceControl1
            // 
            this.distanceControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.distanceControl1.CAtoms = uQlustCore.PDB.PDBMODE.ONLY_CA;
            this.distanceControl1.distDef = uQlustCore.DistanceMeasures.HAMMING;
            this.distanceControl1.HideAtoms = false;
            this.distanceControl1.HideCosine = false;
            this.distanceControl1.HideHamming = false;
            this.distanceControl1.hideReference = false;
            this.distanceControl1.HideRmsdLike = false;
            this.distanceControl1.hideSetup = false;
            this.distanceControl1.Location = new System.Drawing.Point(12, 340);
            this.distanceControl1.Name = "distanceControl1";
            this.distanceControl1.profileName = null;
            this.distanceControl1.reference = true;
            this.distanceControl1.referenceProfile = null;
            this.distanceControl1.Size = new System.Drawing.Size(550, 209);
            this.distanceControl1.TabIndex = 16;
            // 
            // numericBox1
            // 
            this.numericBox1.AllowSpace = false;
            this.numericBox1.Location = new System.Drawing.Point(138, 76);
            this.numericBox1.Name = "numericBox1";
            this.numericBox1.Size = new System.Drawing.Size(100, 20);
            this.numericBox1.TabIndex = 5;
            this.numericBox1.Text = "20";
            this.numericBox1.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(21, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(197, 23);
            this.button3.TabIndex = 64;
            this.button3.Text = "Use automatically generated profile";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // HierarchicalCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(598, 603);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.userHash1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.distanceControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericBox1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.FastHClusterRadio);
            this.Controls.Add(this.maxRepeat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DBIndex);
            this.Controls.Add(this.maxK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.threshold);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.AglomRadio);
            this.Controls.Add(this.KmeansRadio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HierarchicalCForm";
            this.Text = "HierarchicalCForm";
            ((System.ComponentModel.ISupportInitialize)(this.maxRepeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Graph.NumericBox numericBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton FastHClusterRadio;
        private System.Windows.Forms.NumericUpDown maxRepeat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown DBIndex;
        private System.Windows.Forms.NumericUpDown maxK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown threshold;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton AglomRadio;
        private System.Windows.Forms.RadioButton KmeansRadio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DistanceControl distanceControl1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private jury1DSetup jury1DSetup3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private UserHash userHash1;
        private System.Windows.Forms.Button button3;
    }
}