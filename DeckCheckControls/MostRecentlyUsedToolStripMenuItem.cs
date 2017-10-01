﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DeckCheckControls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    [DefaultEvent("RecentItemClick")]
    public partial class MostRecentlyUsedToolStripMenuItem : ToolStripMenuItem
    {
        private MostRecentList _mostRecentItems = new MostRecentList();

        public MostRecentlyUsedToolStripMenuItem()
        {
            InitializeComponent();
            Update();
        }

        public void Add(MostRecentItem item)
        {
            _mostRecentItems.Add(item);
            Update();
        }

        private void Update()
        {
            DropDownItems.Clear();
            if (_mostRecentItems.Count == 0)
            {
                Enabled = false;
                return;
            }
            for (int k = 0; k < _mostRecentItems.Count; k++)
            {
                MostRecentItem item = _mostRecentItems[k];
                ToolStripMenuItem menuItem = new ToolStripMenuItem($"&{(k + 1)} {item.DisplayName}");
                menuItem.Tag = item;
                menuItem.Click += MenuItem_Click;
                DropDownItems.Add(menuItem);
            }
            Enabled = true;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            MostRecentItem item = (MostRecentItem)((ToolStripMenuItem)sender).Tag;
            OnRecentItemClicked(item.Path);
        }

        public event EventHandler<RecentItemClickEventArgs> RecentItemClick;
        private void OnRecentItemClicked(string path)
        {
            RecentItemClick?.Invoke(this, new RecentItemClickEventArgs(path));
        }
    }
}
