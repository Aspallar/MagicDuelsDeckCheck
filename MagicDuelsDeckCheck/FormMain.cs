using System;
using System.Windows.Forms;
using MagicDuels;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using DeckCheckControls;

namespace MagicDuelsDeckCheck
{
    public partial class FormMain : Form
    {
        private const string pasteContextMenu = "Paste";
        private const string recentDecksFileName = "RecentDecks.xml";

        private MagicDuelsCards _cards;
        private PageGenerator _pageGenerator;
        private string _profilePath;
        private bool _cardDataLoaded;
        private BackgroundWorker _worker;
        private bool _working;
        private CorrectCardNames _correctCardNames;
        private MostRecentList _recentDecks;
        private string _appDataFolder;

        public FormMain(string profilePath)
        {
            _profilePath = profilePath;
            _appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MagicDuelsDeckCheck\";
            InitializeComponent();
            CreateContextMenu();
        }

        private void CreateContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem paste = new ToolStripMenuItem(pasteContextMenu);
            paste.Name = pasteContextMenu;
            paste.Click += PasteClick;
            menu.Opening += ContextMenu_Opening;
            menu.Items.Add(paste);
            this.ContextMenuStrip = menu;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            AllowDrop = true;
            LoadCardData();
            _pageGenerator = new PageGenerator();
            _correctCardNames = new CorrectCardNames();
            CreateWorker();
            Initialize();
            _recentDecks = MostRecentList.Read(_appDataFolder + recentDecksFileName);
            mostRecentlyUsed.RecentItems = _recentDecks;
            if (!_cardDataLoaded)
                mostRecentlyUsed.Enabled = false;
        }

        private void CreateWorker()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                StartJob(e.Data);
            }
            catch (Exception ex)
            {
                // we catch all exceptions here, as exceptions that occur in a drop event handler just vanish, because...
                // <quote>
                // Even with drag-and-drop within the same application, the drag-and-drop is handled through the standard OLE drag-drop mechanism.
                // From OLE's point of view it's dealing with two applications, the source and the target and decouples them appropriately.
                // Since OLE has been around far longer than .NET, OLE has no concept of a .NET exception and therefore can't communicate an exception from
                // the target back to the source. Even if it could, why should the source care that the target couldn't perform the drop?
                // If you want to handle an exception during a DragDrop event you must handle it within your DragDrop event handler,
                // it won't propagate beyond that event handler because there is a managed to unmanaged to managed code transition between the source and the target.
                // </quote>
                FatalError("Unexpected Error:\r\n" + ex.ToString());
            }
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            ((ContextMenuStrip)sender).Items[pasteContextMenu].Enabled = ShouldAccept(Clipboard.GetDataObject());
        }

        private void PasteClick(object sender, EventArgs e)
        {
            StartJob(Clipboard.GetDataObject());
        }

        private void StartJob(IDataObject data)
        {
            string deckPath;
            if (data.GetDataPresent("Text"))
                deckPath = data.GetData("Text").ToString();
            else
                deckPath = GetFileName(data);
            StartJob(deckPath);
        }

        private void StartJob(string deckPath)
        {
            labelStatus.Text = IsHttpPath(deckPath) ? "Reading web page..." : "Reading file...";
            _working = true;
            mostRecentlyUsed.Enabled = false;
            _worker.RunWorkerAsync(deckPath);
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = ShouldAccept(e.Data) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string deckPath = (string)e.Argument;
            string deckName = ShowMissingCards(deckPath);
            if (IsHttpPath(deckPath))
                e.Result = new MostRecentItem(deckName, deckPath);
            else
                e.Result = new MostRecentItem(Path.GetFileName(new FileInfo(deckPath).FullName), deckPath);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _working = false;
            SetStatus();
            if (e.Error != null)
            {
                ShowError("Error processing deck list:\r\n" + e.Error.Message);
                return;
            }
            mostRecentlyUsed.Add((MostRecentItem)e.Result);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelStatus.Text = "Working...";
        }

        private void LoadCardData()
        {
            _cardDataLoaded = false;
            try
            {
                MagicDuelsCardManager manager = new MagicDuelsCardManager();
                _cards = manager.GetCards(_profilePath);
                _cardDataLoaded = true;
                SetStatus();
            }
            catch (BadCardDataException ex)
            {
                FatalError(ex.Message);
            }
            catch (SteamProfileNotFoundException)
            {
                SteamProfileError("Unable to find steam profile. Use File->Options to set the correct profile path.");
            }
            catch (BadSteamProfileDataException)
            {
                SteamProfileError("Steam profile contained bad data. Use File->Options to check profile path is correct.");
            }
            catch (IOException)
            {
                FatalError("IO error while reading steam profile.");
            }
        }

        private void SteamProfileError(string msg)
        {
            ShowError(msg + "\r\n\r\nDrag drop of decks is disabled until the correct profile path is set.");
            _cardDataLoaded = false;
            SetStatus();
        }

        private void SetStatus()
        {
            if (_cardDataLoaded)
                labelStatus.Text = "Drag deck link here";
            else
                labelStatus.Text = "Error: No steam profile";
        }

        private string ShowMissingCards(string deckPath)
        {
            string content = GetDeckDocument(deckPath);
            _worker.ReportProgress(50);
            DeckInfo deckInfo = DeckReaders.GetReader(deckPath).ReadDeck(content);
            deckInfo.GetOwned(_cards, _correctCardNames);
            DisplayMissingPage(deckInfo);
            return deckInfo.DeckName;
        }

        private void DisplayMissingPage(DeckInfo deckInfo)
        {
            string fileName = _pageGenerator.MakePage(deckInfo);
            Process.Start(fileName);
        }

        private void Initialize()
        {
            try
            {
                _correctCardNames.Initialize();
            }
            catch (IOException)
            {
                FatalError($"IO Error reading {CorrectCardNames.FileName}");
            }
            catch (InvalidOperationException)
            {
                FatalError($"{CorrectCardNames.FileName} contains invalid entries.");
            }
            try
            {
                _pageGenerator.Initialize();
            }
            catch (IOException)
            {
                FatalError($"IO Error reading html templates");
            }
        }

        private bool ShouldAccept(IDataObject data)
        {
            bool accept = _cardDataLoaded && !_working;
            if (accept)
            {
                if (data.GetDataPresent("Text"))
                    accept = DeckReaders.HasReaderFor(data.GetData("Text").ToString());
                else if (data.GetDataPresent("FileName"))
                    accept = DeckReaders.HasReaderFor(GetFileName(data));
                else
                    accept = false;
            }
            return accept;
        }

        private static string GetDeckDocument(string deckPath)
        {
            if (IsHttpPath(deckPath))
            {
                string content;
                using (WebClient client = new WebClient())
                    content = client.DownloadString(deckPath);
                return content;
            }
            else
            {
                return File.ReadAllText(deckPath);
            }
        }

        private void linkLabelMagicDuelsHelper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.magicduelshelper.com/decklist/alldecks");
        }

        private void linkLabelWiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://magicduels.wikia.com/wiki/Decklists");
        }

        private void linkLabelTappedOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://tappedout.net/mtg-decks/search/?q=&format=magic-duels&cards=&general=&price_0=&price_1=&o=-date_updated&submit=Filter+results");
        }

        private void linkLabelDeckStats_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://deckstats.net/decks/search/?lng=en&search_title=&search_format=0&search_price_min=&search_price_max=&search_number_cards_main=&search_number_cards_sideboard=&search_cards%5B%5D=&search_tags=Magic+Duels&search_order=updated%2Cdesc");
        }

        private void mostRecentlyUsed_RecentItemClick(object sender, RecentItemClickEventArgs e)
        {
            StartJob(e.Path);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox dlg = new AboutBox();
            dlg.ShowDialog(this);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm dlg = new OptionsForm();
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.ProfilePath != _profilePath)
            {
                _profilePath = dlg.ProfilePath;
                LoadCardData();
                if (_cardDataLoaded)
                    mostRecentlyUsed.SetEnabled();
                else
                    mostRecentlyUsed.Enabled = false;
            }
        }

        private void reloadCradsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCardData();
            if (_cardDataLoaded)
                ShowMessage("Cards counts have beed reloaded from steam profile.");
        }

        private void ShowMessage(string msg)
        {
            MessageBox.Show(this, msg, "Magic Duels Deck Checker");
        }

        private void ShowError(string msg)
        {
            System.Media.SystemSounds.Asterisk.Play();
            Activate();
            ShowMessage(msg);
        }

        private void FatalError(string msg)
        {
            ShowError(msg);
            Application.Exit();
        }

        private static string GetFileName(IDataObject data)
        {
            string[] names = (string[])data.GetData("FileName");
            return names[0];
        }

        private static bool IsHttpPath(string deckPath)
        {
            return deckPath.StartsWith("http", StringComparison.InvariantCultureIgnoreCase);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            MostRecentList.Save(_appDataFolder + recentDecksFileName, _recentDecks);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("here");
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            const int pasteIndex = 0;
            ((ToolStripMenuItem)sender).DropDownItems[pasteIndex].Enabled = ShouldAccept(Clipboard.GetDataObject());
        }
    }
}
