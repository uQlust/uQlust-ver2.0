namespace Graph
{
    partial class FormFraction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFraction));
            this.jury1D = new System.Windows.Forms.CheckBox();
            this.Rmsd = new System.Windows.Forms.RadioButton();
            this.MaxSub = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GDT_TS = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numClusters = new System.Windows.Forms.NumericUpDown();
            this.jury1DSetup1 = new Graph.jury1DSetup();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numClusters)).BeginInit();
            this.SuspendLayout();
            // 
            // jury1D
            // 
            this.jury1D.AutoSize = true;
            this.jury1D.Location = new System.Drawing.Point(14, 27);
            this.jury1D.Name = "jury1D";
            this.jury1D.Size = new System.Drawing.Size(188, 17);
            this.jury1D.TabIndex = 1;
            this.jury1D.Text = "Use 1DJury for reference structure";
            this.jury1D.UseVisualStyleBackColor = true;
            this.jury1D.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Rmsd
            // 
            this.Rmsd.AutoSize = true;
            this.Rmsd.Checked = true;
            this.Rmsd.Location = new System.Drawing.Point(15, 19);
            this.Rmsd.Name = "Rmsd";
            this.Rmsd.Size = new System.Drawing.Size(52, 17);
            this.Rmsd.TabIndex = 2;
            this.Rmsd.TabStop = true;
            this.Rmsd.Text = "Rmsd";
            this.Rmsd.UseVisualStyleBackColor = true;
            this.Rmsd.CheckedChanged += new System.EventHandler(this.Rmsd_CheckedChanged);
            // 
            // MaxSub
            // 
            this.MaxSub.AutoSize = true;
            this.MaxSub.Location = new System.Drawing.Point(130, 19);
            this.MaxSub.Name = "MaxSub";
            this.MaxSub.Size = new System.Drawing.Size(64, 17);
            this.MaxSub.TabIndex = 3;
            this.MaxSub.Text = "MaxSub";
            this.MaxSub.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GDT_TS);
            this.groupBox1.Controls.Add(this.MaxSub);
            this.groupBox1.Controls.Add(this.Rmsd);
            this.groupBox1.Location = new System.Drawing.Point(8, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distance for best model search";
            // 
            // GDT_TS
            // 
            this.GDT_TS.AutoSize = true;
            this.GDT_TS.Location = new System.Drawing.Point(235, 19);
            this.GDT_TS.Name = "GDT_TS";
            this.GDT_TS.Size = new System.Drawing.Size(68, 17);
            this.GDT_TS.TabIndex = 4;
            this.GDT_TS.TabStop = true;
            this.GDT_TS.Text = "GDT_TS";
            this.GDT_TS.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Distance threshold";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(191, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(135, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "2";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(23, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(346, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Considered number of best clusters";
            // 
            // numClusters
            // 
            this.numClusters.Location = new System.Drawing.Point(191, 212);
            this.numClusters.Name = "numClusters";
            this.numClusters.Size = new System.Drawing.Size(120, 20);
            this.numClusters.TabIndex = 10;
            this.numClusters.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // jury1DSetup1
            // 
            this.jury1DSetup1.Location = new System.Drawing.Point(208, 12);
            this.jury1DSetup1.Name = "jury1DSetup1";
            this.jury1DSetup1.profileName = null;
            this.jury1DSetup1.Size = new System.Drawing.Size(220, 75);
            this.jury1DSetup1.TabIndex = 0;
            this.jury1DSetup1.Visible = false;
            // 
            // FormFraction
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(444, 288);
            this.Controls.Add(this.numClusters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.jury1D);
            this.Controls.Add(this.jury1DSetup1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFraction";
            this.Text = "Fraction of good";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numClusters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::Graph.jury1DSetup jury1DSetup1;
        private System.Windows.Forms.CheckBox jury1D;
        private System.Windows.Forms.RadioButton Rmsd;
        private System.Windows.Forms.RadioButton MaxSub;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numClusters;
        private System.Windows.Forms.RadioButton GDT_TS;
    }
}