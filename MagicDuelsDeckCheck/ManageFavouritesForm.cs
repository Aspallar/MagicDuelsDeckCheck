using DeckCheckControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MagicDuelsDeckCheck
{
    public partial class ManageFavouritesForm : Form
    {
        private bool _isDirty = false;

        public ManageFavouritesForm(FavouritesList favourites)
        {
            InitializeComponent();
            PopulateFavourites(favourites);
        }

        private void ManageFavouritesForm_Load(object sender, EventArgs e)
        {
            UpdateUiState();
        }


        public FavouritesList Favourites { get; private set; }

        private void PopulateFavourites(List<MostRecentItem> favourites)
        {
            listBoxFavourites.Items.Clear();
            foreach (MostRecentItem item in favourites)
                listBoxFavourites.Items.Add(item);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = listBoxFavourites.SelectedIndex;
            if (index != -1)
            {
                listBoxFavourites.Items.RemoveAt(index);
                _isDirty = true;
            }
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            int index = listBoxFavourites.SelectedIndex;
            if (index > 0)
            {
                object item = listBoxFavourites.SelectedItem;
                listBoxFavourites.Items.Remove(item);
                listBoxFavourites.Items.Insert(index - 1, item);
                listBoxFavourites.SelectedItem = item;
                _isDirty = true;
            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            int index = listBoxFavourites.SelectedIndex;
            if (index != -1 && index != listBoxFavourites.Items.Count - 1)
            {
                object item = listBoxFavourites.SelectedItem;
                listBoxFavourites.Items.Remove(item);
                listBoxFavourites.Items.Insert(index + 1, item);
                listBoxFavourites.SelectedItem = item;
                _isDirty = true;
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            FavouritesList list = GetFavouritesList();
            PopulateFavourites(list.OrderBy(x => x.DisplayName).ToList());
            _isDirty = true;
        }

        private void buttonWebsite_Click(object sender, EventArgs e)
        {
            MostRecentItem item = (MostRecentItem)listBoxFavourites.SelectedItem;
            if (item != null && Utils.IsHttpPath(item.Path))
                Process.Start(item.Path);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!_isDirty)
            {
                DialogResult = DialogResult.Ignore;
                Close();
                return;
            }

            Favourites = GetFavouritesList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private FavouritesList GetFavouritesList()
        {
            FavouritesList list = new FavouritesList();
            foreach (object item in listBoxFavourites.Items)
                list.Add((MostRecentItem)item);
            return list;
        }

        private void listBoxFavourites_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUiState();
        }

        private void UpdateUiState()
        {
            int index = listBoxFavourites.SelectedIndex;
            buttonDelete.Enabled = index != -1;
            buttonMoveUp.Enabled = index > 0;
            buttonMoveDown.Enabled = index != -1 && index != listBoxFavourites.Items.Count - 1;
            if (index != -1)
            {
                MostRecentItem item = (MostRecentItem)listBoxFavourites.Items[index];
                buttonWebsite.Enabled = Utils.IsHttpPath(item.Path);
            }
            else
            {
                buttonWebsite.Enabled = false;
            }
        }
    }
}
