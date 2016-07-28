namespace Graph
{
    partial class DistanceControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioGDT_TS = new System.Windows.Forms.RadioButton();
            this.radioEucl = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.radioMaxSub = new System.Windows.Forms.RadioButton();
            this.radioRmsd = new System.Windows.Forms.RadioButton();
            this.radio1DJury = new System.Windows.Forms.RadioButton();
            this.referenceBox = new System.Windows.Forms.CheckBox();
            this.jury1DSetup1 = new Graph.jury1DSetup();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radioGDT_TS);
            this.groupBox3.Controls.Add(this.radioEucl);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.radioMaxSub);
            this.groupBox3.Controls.Add(this.radioRmsd);
            this.groupBox3.Controls.Add(this.radio1DJury);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(541, 111);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Distance measures";
            // 
            // radioGDT_TS
            // 
            this.radioGDT_TS.AutoSize = true;
            this.radioGDT_TS.Location = new System.Drawing.Point(286, 19);
            this.radioGDT_TS.Name = "radioGDT_TS";
            this.radioGDT_TS.Size = new System.Drawing.Size(68, 17);
            this.radioGDT_TS.TabIndex = 25;
            this.radioGDT_TS.TabStop = true;
            this.radioGDT_TS.Text = "GDT_TS";
            this.radioGDT_TS.UseVisualStyleBackColor = true;
            this.radioGDT_TS.CheckedChanged += new System.EventHandler(this.radioMaxSub_CheckedChanged);
            // 
            // radioEucl
            // 
            this.radioEucl.AutoSize = true;
            this.radioEucl.Location = new System.Drawing.Point(388, 19);
            this.radioEucl.Name = "radioEucl";
            this.radioEucl.Size = new System.Drawing.Size(57, 17);
            this.radioEucl.TabIndex = 24;
            this.radioEucl.TabStop = true;
            this.radioEucl.Text = "Cosine";
            this.radioEucl.UseVisualStyleBackColor = true;
            this.radioEucl.CheckedChanged += new System.EventHandler(this.radio1DJury_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "File name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Location = new System.Drawing.Point(114, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 29);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Checked = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 10);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(63, 17);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Only CA";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(92, 10);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(68, 17);
            this.radioButton6.TabIndex = 6;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "All Atoms";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(10, 40);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Setup JuryMesaure profiles";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Active profiles: ";
            // 
            // radioMaxSub
            // 
            this.radioMaxSub.AutoSize = true;
            this.radioMaxSub.Location = new System.Drawing.Point(202, 19);
            this.radioMaxSub.Name = "radioMaxSub";
            this.radioMaxSub.Size = new System.Drawing.Size(64, 17);
            this.radioMaxSub.TabIndex = 3;
            this.radioMaxSub.TabStop = true;
            this.radioMaxSub.Text = "MaxSub";
            this.radioMaxSub.UseVisualStyleBackColor = true;
            this.radioMaxSub.CheckedChanged += new System.EventHandler(this.radioMaxSub_CheckedChanged);
            // 
            // radioRmsd
            // 
            this.radioRmsd.AutoSize = true;
            this.radioRmsd.Location = new System.Drawing.Point(114, 19);
            this.radioRmsd.Name = "radioRmsd";
            this.radioRmsd.Size = new System.Drawing.Size(52, 17);
            this.radioRmsd.TabIndex = 2;
            this.radioRmsd.TabStop = true;
            this.radioRmsd.Text = "Rmsd";
            this.radioRmsd.UseVisualStyleBackColor = true;
            this.radioRmsd.CheckedChanged += new System.EventHandler(this.radioRmsd_CheckedChanged);
            // 
            // radio1DJury
            // 
            this.radio1DJury.AutoSize = true;
            this.radio1DJury.Checked = true;
            this.radio1DJury.Location = new System.Drawing.Point(10, 19);
            this.radio1DJury.Name = "radio1DJury";
            this.radio1DJury.Size = new System.Drawing.Size(69, 17);
            this.radio1DJury.TabIndex = 1;
            this.radio1DJury.TabStop = true;
            this.radio1DJury.Text = "Hamming";
            this.radio1DJury.UseVisualStyleBackColor = true;
            this.radio1DJury.CheckedChanged += new System.EventHandler(this.radio1DJury_CheckedChanged);
            // 
            // referenceBox
            // 
            this.referenceBox.AutoSize = true;
            this.referenceBox.Location = new System.Drawing.Point(7, 140);
            this.referenceBox.Name = "referenceBox";
            this.referenceBox.Size = new System.Drawing.Size(115, 30);
            this.referenceBox.TabIndex = 7;
            this.referenceBox.Text = "Use 1DJury to find\r\nreference structure";
            this.referenceBox.UseVisualStyleBackColor = true;
            this.referenceBox.CheckedChanged += new System.EventHandler(this.referenceBox_CheckedChanged);
            // 
            // jury1DSetup1
            // 
            this.jury1DSetup1.Location = new System.Drawing.Point(138, 121);
            this.jury1DSetup1.Name = "jury1DSetup1";
            this.jury1DSetup1.profileName = null;
            this.jury1DSetup1.Size = new System.Drawing.Size(314, 75);
            this.jury1DSetup1.TabIndex = 8;
            this.jury1DSetup1.Load += new System.EventHandler(this.jury1DSetup1_Load);
            // 
            // DistanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.referenceBox);
            this.Controls.Add(this.jury1DSetup1);
            this.Controls.Add(this.groupBox3);
            this.Name = "DistanceControl";
            this.Size = new System.Drawing.Size(557, 194);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.Button button5;
        public System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton radioMaxSub;
        private System.Windows.Forms.RadioButton radioRmsd;
        private System.Windows.Forms.RadioButton radio1DJury;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private jury1DSetup jury1DSetup1;
        private System.Windows.Forms.CheckBox referenceBox;
        private System.Windows.Forms.RadioButton radioEucl;
        private System.Windows.Forms.RadioButton radioGDT_TS;
    }
}
