namespace MagicDuelsDeckCheck
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.labelProfilePath = new System.Windows.Forms.Label();
            this.textBoxProfilePath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelProfileHelpText = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.numericUpDownMruSize = new System.Windows.Forms.NumericUpDown();
            this.labelMruSize = new System.Windows.Forms.Label();
            this.labelSeparator1 = new System.Windows.Forms.Label();
            this.labelUserAgent = new System.Windows.Forms.Label();
            this.textBoxUserAgent = new System.Windows.Forms.TextBox();
            this.labelUserAgentText = new System.Windows.Forms.Label();
            this.buttonDefaultUserAgent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMruSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProfilePath
            // 
            this.labelProfilePath.AutoSize = true;
            this.labelProfilePath.Location = new System.Drawing.Point(13, 13);
            this.labelProfilePath.Name = "labelProfilePath";
            this.labelProfilePath.Size = new System.Drawing.Size(156, 13);
            this.labelProfilePath.TabIndex = 0;
            this.labelProfilePath.Text = "Magic Duels Steam Profile Path";
            // 
            // textBoxProfilePath
            // 
            this.textBoxProfilePath.Location = new System.Drawing.Point(16, 30);
            this.textBoxProfilePath.Name = "textBoxProfilePath";
            this.textBoxProfilePath.Size = new System.Drawing.Size(553, 20);
            this.textBoxProfilePath.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(576, 26);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(31, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(445, 267);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(526, 267);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelProfileHelpText
            // 
            this.labelProfileHelpText.Location = new System.Drawing.Point(16, 57);
            this.labelProfileHelpText.Name = "labelProfileHelpText";
            this.labelProfileHelpText.Size = new System.Drawing.Size(591, 62);
            this.labelProfileHelpText.TabIndex = 5;
            this.labelProfileHelpText.Text = resources.GetString("labelProfileHelpText.Text");
            // 
            // labelSeparator
            // 
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.Location = new System.Drawing.Point(16, 119);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(568, 2);
            this.labelSeparator.TabIndex = 6;
            // 
            // numericUpDownMruSize
            // 
            this.numericUpDownMruSize.Location = new System.Drawing.Point(16, 134);
            this.numericUpDownMruSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMruSize.Name = "numericUpDownMruSize";
            this.numericUpDownMruSize.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownMruSize.TabIndex = 7;
            this.numericUpDownMruSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelMruSize
            // 
            this.labelMruSize.AutoSize = true;
            this.labelMruSize.Location = new System.Drawing.Point(60, 136);
            this.labelMruSize.Name = "labelMruSize";
            this.labelMruSize.Size = new System.Drawing.Size(263, 13);
            this.labelMruSize.TabIndex = 8;
            this.labelMruSize.Text = "Maximum number of decks to show in \'Recent Decks\'.";
            // 
            // labelSeparator1
            // 
            this.labelSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator1.Location = new System.Drawing.Point(16, 167);
            this.labelSeparator1.Name = "labelSeparator1";
            this.labelSeparator1.Size = new System.Drawing.Size(568, 2);
            this.labelSeparator1.TabIndex = 9;
            // 
            // labelUserAgent
            // 
            this.labelUserAgent.AutoSize = true;
            this.labelUserAgent.Location = new System.Drawing.Point(16, 175);
            this.labelUserAgent.Name = "labelUserAgent";
            this.labelUserAgent.Size = new System.Drawing.Size(92, 13);
            this.labelUserAgent.TabIndex = 10;
            this.labelUserAgent.Text = "HTTP User Agent";
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Location = new System.Drawing.Point(19, 192);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(492, 20);
            this.textBoxUserAgent.TabIndex = 11;
            // 
            // labelUserAgentText
            // 
            this.labelUserAgentText.Location = new System.Drawing.Point(19, 219);
            this.labelUserAgentText.Name = "labelUserAgentText";
            this.labelUserAgentText.Size = new System.Drawing.Size(565, 34);
            this.labelUserAgentText.TabIndex = 12;
            this.labelUserAgentText.Text = resources.GetString("labelUserAgentText.Text");
            // 
            // buttonDefaultUserAgent
            // 
            this.buttonDefaultUserAgent.Location = new System.Drawing.Point(518, 189);
            this.buttonDefaultUserAgent.Name = "buttonDefaultUserAgent";
            this.buttonDefaultUserAgent.Size = new System.Drawing.Size(75, 23);
            this.buttonDefaultUserAgent.TabIndex = 13;
            this.buttonDefaultUserAgent.Text = "Default";
            this.buttonDefaultUserAgent.UseVisualStyleBackColor = true;
            this.buttonDefaultUserAgent.Click += new System.EventHandler(this.buttonDefaultUserAgent_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 302);
            this.Controls.Add(this.buttonDefaultUserAgent);
            this.Controls.Add(this.labelUserAgentText);
            this.Controls.Add(this.textBoxUserAgent);
            this.Controls.Add(this.labelUserAgent);
            this.Controls.Add(this.labelSeparator1);
            this.Controls.Add(this.labelMruSize);
            this.Controls.Add(this.numericUpDownMruSize);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelProfileHelpText);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxProfilePath);
            this.Controls.Add(this.labelProfilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "MagicDuelsDeckCheck Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMruSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProfilePath;
        private System.Windows.Forms.TextBox textBoxProfilePath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelProfileHelpText;
        private System.Windows.Forms.Label labelSeparator;
        private System.Windows.Forms.NumericUpDown numericUpDownMruSize;
        private System.Windows.Forms.Label labelMruSize;
        private System.Windows.Forms.Label labelSeparator1;
        private System.Windows.Forms.Label labelUserAgent;
        private System.Windows.Forms.TextBox textBoxUserAgent;
        private System.Windows.Forms.Label labelUserAgentText;
        private System.Windows.Forms.Button buttonDefaultUserAgent;
    }
}