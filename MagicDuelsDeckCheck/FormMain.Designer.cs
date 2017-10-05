namespace MagicDuelsDeckCheck
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.labelStatus = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadCradsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentDecks = new DeckCheckControls.MostRecentlyUsedToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favouritesMenuItem = new DeckCheckControls.FavouritesToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabelMagicDuelsHelper = new System.Windows.Forms.LinkLabel();
            this.linkLabelWiki = new System.Windows.Forms.LinkLabel();
            this.linkLabelTappedOut = new System.Windows.Forms.LinkLabel();
            this.linkLabelDeckStats = new System.Windows.Forms.LinkLabel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelStatus.Location = new System.Drawing.Point(37, 37);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(173, 24);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Drag deck link here";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.favouritesMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(282, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.reloadCradsToolStripMenuItem,
            this.recentDecks,
            this.alwaysOnTopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "&Options..";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // reloadCradsToolStripMenuItem
            // 
            this.reloadCradsToolStripMenuItem.Name = "reloadCradsToolStripMenuItem";
            this.reloadCradsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reloadCradsToolStripMenuItem.Text = "&Reload Cards";
            this.reloadCradsToolStripMenuItem.Click += new System.EventHandler(this.reloadCradsToolStripMenuItem_Click);
            // 
            // recentDecks
            // 
            this.recentDecks.Enabled = false;
            this.recentDecks.Name = "recentDecks";
            this.recentDecks.Size = new System.Drawing.Size(152, 22);
            this.recentDecks.Text = "Recent Decks";
            this.recentDecks.RecentItemClick += new System.EventHandler<DeckCheckControls.RecentItemClickEventArgs>(this.Deck_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on Top";
            this.alwaysOnTopToolStripMenuItem.CheckedChanged += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_CheckedChanged);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteClick);
            // 
            // favouritesMenuItem
            // 
            this.favouritesMenuItem.FavouritesEnabled = true;
            this.favouritesMenuItem.Name = "favouritesMenuItem";
            this.favouritesMenuItem.Size = new System.Drawing.Size(73, 20);
            this.favouritesMenuItem.Text = "Fa&vourites";
            this.favouritesMenuItem.FavouriteClick += new System.EventHandler<DeckCheckControls.RecentItemClickEventArgs>(this.Deck_Click);
            this.favouritesMenuItem.ManageClick += new System.EventHandler<System.EventArgs>(this.favouritesMenuItem_ManageClick);
            this.favouritesMenuItem.DropDownOpening += new System.EventHandler(this.favouritesMenuItem_DropDownOpening);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // linkLabelMagicDuelsHelper
            // 
            this.linkLabelMagicDuelsHelper.AutoSize = true;
            this.linkLabelMagicDuelsHelper.Location = new System.Drawing.Point(152, 90);
            this.linkLabelMagicDuelsHelper.Name = "linkLabelMagicDuelsHelper";
            this.linkLabelMagicDuelsHelper.Size = new System.Drawing.Size(100, 13);
            this.linkLabelMagicDuelsHelper.TabIndex = 2;
            this.linkLabelMagicDuelsHelper.TabStop = true;
            this.linkLabelMagicDuelsHelper.Text = "Magic Duels Helper";
            this.linkLabelMagicDuelsHelper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMagicDuelsHelper_LinkClicked);
            // 
            // linkLabelWiki
            // 
            this.linkLabelWiki.AutoSize = true;
            this.linkLabelWiki.Location = new System.Drawing.Point(152, 73);
            this.linkLabelWiki.Name = "linkLabelWiki";
            this.linkLabelWiki.Size = new System.Drawing.Size(90, 13);
            this.linkLabelWiki.TabIndex = 3;
            this.linkLabelWiki.TabStop = true;
            this.linkLabelWiki.Text = "Magic Duels Wiki";
            this.linkLabelWiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWiki_LinkClicked);
            // 
            // linkLabelTappedOut
            // 
            this.linkLabelTappedOut.AutoSize = true;
            this.linkLabelTappedOut.Location = new System.Drawing.Point(152, 107);
            this.linkLabelTappedOut.Name = "linkLabelTappedOut";
            this.linkLabelTappedOut.Size = new System.Drawing.Size(64, 13);
            this.linkLabelTappedOut.TabIndex = 4;
            this.linkLabelTappedOut.TabStop = true;
            this.linkLabelTappedOut.Text = "Tapped Out";
            this.linkLabelTappedOut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTappedOut_LinkClicked);
            // 
            // linkLabelDeckStats
            // 
            this.linkLabelDeckStats.AutoSize = true;
            this.linkLabelDeckStats.Location = new System.Drawing.Point(152, 124);
            this.linkLabelDeckStats.Name = "linkLabelDeckStats";
            this.linkLabelDeckStats.Size = new System.Drawing.Size(75, 13);
            this.linkLabelDeckStats.TabIndex = 5;
            this.linkLabelDeckStats.TabStop = true;
            this.linkLabelDeckStats.Text = "Deckstats.Net";
            this.linkLabelDeckStats.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeckStats_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 142);
            this.Controls.Add(this.linkLabelDeckStats);
            this.Controls.Add(this.linkLabelTappedOut);
            this.Controls.Add(this.linkLabelWiki);
            this.Controls.Add(this.linkLabelMagicDuelsHelper);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Magic Duels Deck Check";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadCradsToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabelMagicDuelsHelper;
        private System.Windows.Forms.LinkLabel linkLabelWiki;
        private System.Windows.Forms.LinkLabel linkLabelTappedOut;
        private System.Windows.Forms.LinkLabel linkLabelDeckStats;
        private DeckCheckControls.MostRecentlyUsedToolStripMenuItem recentDecks;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private DeckCheckControls.FavouritesToolStripMenuItem favouritesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
    }
}

