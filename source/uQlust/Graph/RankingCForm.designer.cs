namespace Graph
{
    partial class RankingCForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankingCForm));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.jury1d = new System.Windows.Forms.RadioButton();
            this.sift = new System.Windows.Forms.RadioButton();
            this.jury3d = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.distanceControl1 = new Graph.DistanceControl();
            this.jury1DSetup1 = new Graph.jury1DSetup();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(518, 329);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(70, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // jury1d
            // 
            this.jury1d.AutoSize = true;
            this.jury1d.Checked = true;
            this.jury1d.Location = new System.Drawing.Point(12, 17);
            this.jury1d.Name = "jury1d";
            this.jury1d.Size = new System.Drawing.Size(58, 17);
            this.jury1d.TabIndex = 1;
            this.jury1d.TabStop = true;
            this.jury1d.Text = "1DJury";
            this.jury1d.UseVisualStyleBackColor = true;
            this.jury1d.Click += new System.EventHandler(this.jury1d_Click);
            // 
            // sift
            // 
            this.sift.AutoSize = true;
            this.sift.Location = new System.Drawing.Point(12, 106);
            this.sift.Name = "sift";
            this.sift.Size = new System.Drawing.Size(40, 17);
            this.sift.TabIndex = 4;
            this.sift.TabStop = true;
            this.sift.Text = "Sift";
            this.sift.UseVisualStyleBackColor = true;
            this.sift.CheckedChanged += new System.EventHandler(this.sift_CheckedChanged);
            // 
            // jury3d
            // 
            this.jury3d.AutoSize = true;
            this.jury3d.Location = new System.Drawing.Point(12, 60);
            this.jury3d.Name = "jury3d";
            this.jury3d.Size = new System.Drawing.Size(58, 17);
            this.jury3d.TabIndex = 3;
            this.jury3d.TabStop = true;
            this.jury3d.Text = "3DJury";
            this.jury3d.UseVisualStyleBackColor = true;
            this.jury3d.CheckedChanged += new System.EventHandler(this.jury3d_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(359, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(197, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Use automatically generated profile";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.distanceControl1.Location = new System.Drawing.Point(2, 129);
            this.distanceControl1.Name = "distanceControl1";
            this.distanceControl1.profileInfo = true;
            this.distanceControl1.profileName = null;
            this.distanceControl1.reference = true;
            this.distanceControl1.referenceProfile = null;
            this.distanceControl1.Size = new System.Drawing.Size(603, 194);
            this.distanceControl1.TabIndex = 5;
            // 
            // jury1DSetup1
            // 
            this.jury1DSetup1.Location = new System.Drawing.Point(133, 2);
            this.jury1DSetup1.Name = "jury1DSetup1";
            this.jury1DSetup1.profileName = null;
            this.jury1DSetup1.Size = new System.Drawing.Size(220, 75);
            this.jury1DSetup1.TabIndex = 2;
            // 
            // RankingCForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(626, 359);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.distanceControl1);
            this.Controls.Add(this.jury1DSetup1);
            this.Controls.Add(this.jury1d);
            this.Controls.Add(this.sift);
            this.Controls.Add(this.jury3d);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RankingCForm";
            this.Text = "Ranking algorithms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton jury1d;
        private System.Windows.Forms.RadioButton sift;
        private System.Windows.Forms.RadioButton jury3d;
        private jury1DSetup jury1DSetup1;
        private DistanceControl distanceControl1;
        private System.Windows.Forms.Button button3;
    }
}