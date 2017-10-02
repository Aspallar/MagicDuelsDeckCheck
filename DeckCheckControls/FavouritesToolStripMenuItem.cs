using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DeckCheckControls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    [DefaultEvent("FavouriteClick")]
    public partial class FavouritesToolStripMenuItem : ToolStripMenuItem
    {
        FavouritesList _favourites = new FavouritesList();
        MostRecentlyUsedToolStripMenuItem _add;
        bool _favouritesEnabled = true;

        public FavouritesToolStripMenuItem()
        {
            InitializeComponent();
            AddFixedMenuItems();
        }

        public void SetAddableDecks(MostRecentList recentDecks)
        {
            _add.RecentItems = recentDecks.Except(_favourites);
        }

        public FavouritesList Favorites
        {
            set
            {
                _favourites = value;
                Update();
            }
        }

        public bool FavouritesEnabled
        {
            get
            {
                return _favouritesEnabled;
            }
            set
            {
                if (value != _favouritesEnabled)
                    SetFavouritesEnabled(value);
                _favouritesEnabled = value;
            }
        }

        private void SetFavouritesEnabled(bool enabled)
        {
            for (int k = favouritesStartIndex; k < DropDownItems.Count; k++)
                DropDownItems[k].Enabled = enabled;
        }

        private void Update()
        {
            foreach (MostRecentItem item in _favourites)
                DropDownItems.Add(CreateFavouriteItem(item));
        }

        private ToolStripMenuItem CreateFavouriteItem(MostRecentItem item)
        {
            ToolStripMenuItem favourite = new ToolStripMenuItem(item.DisplayName)
            {
                Tag = item,
                Enabled = _favouritesEnabled,
            };
            favourite.Click += Favourite_Click;

            return favourite;
        }

        const int favouritesStartIndex = 3;
        private void AddFixedMenuItems()
        {
            _add = new MostRecentlyUsedToolStripMenuItem
            {
                Name = "Add",
                Text = "&Add",
            };
            _add.RecentItemClick += add_RecentItemClick;
            ToolStripMenuItem manage = new ToolStripMenuItem("&Manage...");
            manage.Click += Manage_Click;

            DropDownItems.Add(_add);
            DropDownItems.Add(manage);
            DropDownItems.Add(new ToolStripSeparator());
        }


        private void add_RecentItemClick(object sender, RecentItemClickEventArgs e)
        {
            InsertNewItem(e.Item);
        }

        private void InsertNewItem(MostRecentItem item)
        {
            _favourites.Insert(0, item);
            DropDownItems.Insert(favouritesStartIndex, CreateFavouriteItem(item));
        }

        private void Favourite_Click(object sender, EventArgs e)
        {
            MostRecentItem item = (MostRecentItem)((ToolStripMenuItem)sender).Tag;
            OnRecentItemClicked(item);
        }

        public event EventHandler<RecentItemClickEventArgs> FavouriteClick;
        private void OnRecentItemClicked(MostRecentItem item) =>
            FavouriteClick?.Invoke(this, new RecentItemClickEventArgs(item));


        private void Manage_Click(object sender, EventArgs e)
        {
            OnManageClicked();
        }

        public event EventHandler<EventArgs> ManageClick;
        private void OnManageClicked() =>
            ManageClick?.Invoke(this, new EventArgs());
    }
}
