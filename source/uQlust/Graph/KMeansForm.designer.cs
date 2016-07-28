namespace Graph
{
    partial class KMeansForm
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
            this.juryRadio = new System.Windows.Forms.RadioButton();
            this.randomRadio = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.kmeansMaxK = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.distanceControl1 = new Graph.DistanceControl();
            this.jury1DSetup1 = new Graph.jury1DSetup();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.kmeansMaxK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // juryRadio
            // 
            this.juryRadio.AutoSize = true;
            this.juryRadio.Checked = true;
            this.juryRadio.Location = new System.Drawing.Point(53, 149);
            this.juryRadio.Name = "juryRadio";
            this.juryRadio.Size = new System.Drawing.Size(55, 17);
            this.juryRadio.TabIndex = 24;
            this.juryRadio.TabStop = true;
            this.juryRadio.Text = "1Djury";
            this.juryRadio.UseVisualStyleBackColor = true;
            this.juryRadio.CheckedChanged += new System.EventHandler(this.juryRadio_CheckedChanged);
            // 
            // randomRadio
            // 
            this.randomRadio.AutoSize = true;
            this.randomRadio.Location = new System.Drawing.Point(53, 208);
            this.randomRadio.Name = "randomRadio";
            this.randomRadio.Size = new System.Drawing.Size(65, 17);
            this.randomRadio.TabIndex = 23;
            this.randomRadio.Text = "Random";
            this.randomRadio.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 127);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Initializatial centroids";
            // 
            // kmeansMaxK
            // 
            this.kmeansMaxK.Location = new System.Drawing.Point(152, 16);
            this.kmeansMaxK.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.kmeansMaxK.Name = "kmeansMaxK";
            this.kmeansMaxK.Size = new System.Drawing.Size(120, 20);
            this.kmeansMaxK.TabIndex = 21;
            this.kmeansMaxK.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Number of clusters K";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(441, 434);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 44;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(94, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 43;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Max numer of iterations";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(152, 59);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 48;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // distanceControl1
            // 
            this.distanceControl1.CAtoms = uQlustCore.PDB.PDBMODE.ONLY_CA;
            this.distanceControl1.distDef = uQlustCore.DistanceMeasures.HAMMING;
            this.distanceControl1.HideAtoms = false;
            this.distanceControl1.HideCosine = false;
            this.distanceControl1.HideHamming = false;
            this.distanceControl1.hideReference = false;
            this.distanceControl1.HideRmsdLike = false;
            this.distanceControl1.hideSetup = false;
            this.distanceControl1.Location = new System.Drawing.Point(12, 226);
            this.distanceControl1.Name = "distanceControl1";
            this.distanceControl1.profileName = null;
            this.distanceControl1.reference = true;
            this.distanceControl1.referenceProfile = null;
            this.distanceControl1.Size = new System.Drawing.Size(533, 196);
            this.distanceControl1.TabIndex = 49;
            // 
            // jury1DSetup1
            // 
            this.jury1DSetup1.Location = new System.Drawing.Point(152, 136);
            this.jury1DSetup1.Name = "jury1DSetup1";
            this.jury1DSetup1.profileName = null;
            this.jury1DSetup1.Size = new System.Drawing.Size(220, 75);
            this.jury1DSetup1.TabIndex = 46;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 101);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(197, 23);
            this.button3.TabIndex = 64;
            this.button3.Text = "Use automatically generated profile";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // KMeansForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(557, 454);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.distanceControl1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.jury1DSetup1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.juryRadio);
            this.Controls.Add(this.randomRadio);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.kmeansMaxK);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KMeansForm";
            this.Text = "KMeansForm";
            ((System.ComponentModel.ISupportInitialize)(this.kmeansMaxK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton juryRadio;
        private System.Windows.Forms.RadioButton randomRadio;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown kmeansMaxK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private Graph.jury1DSetup jury1DSetup1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Graph.DistanceControl distanceControl1;
        private System.Windows.Forms.Button button3;
    }
}