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
            this.labelProfilePath = new System.Windows.Forms.Label();
            this.textBoxProfilePath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelProfileHelpText = new System.Windows.Forms.Label();
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
            this.buttonOk.Location = new System.Drawing.Point(526, 129);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(445, 129);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelProfileHelpText
            // 
            this.labelProfileHelpText.AutoSize = true;
            this.labelProfileHelpText.Location = new System.Drawing.Point(16, 57);
            this.labelProfileHelpText.Name = "labelProfileHelpText";
            this.labelProfileHelpText.Size = new System.Drawing.Size(101, 13);
            this.labelProfileHelpText.TabIndex = 5;
            this.labelProfileHelpText.Text = "labelProfileHelpText";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 164);
            this.Controls.Add(this.labelProfileHelpText);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxProfilePath);
            this.Controls.Add(this.labelProfilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "MagicDuelsDeckCheck Options";
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
    }
}