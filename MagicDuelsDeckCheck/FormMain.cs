using System;
using System.Windows.Forms;
using MagicDuels;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace MagicDuelsDeckCheck
{
    public partial class FormMain : Form
    {
        private MagicDuelsCards _cards;
        private PageGenerator _pageGenerator;
        private string _profilePath;
        private bool _cardDataLoaded;
        private BackgroundWorker _worker;
        private bool _working;
        private CorrectCardNames _correctCardNames;

        public FormMain(string profilePath)
        {
            _profilePath = profilePath;
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            AllowDrop = true;
            LoadCardData();
            _pageGenerator = new PageGenerator();
            _correctCardNames = new CorrectCardNames();
            CreateWorker();
            Initialize();
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
                string pageUrl = e.Data.GetData("Text").ToString();
                labelStatus.Text = "Reading web page...";
                _working = true;
                _worker.RunWorkerAsync(pageUrl);
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

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            bool accept = _cardDataLoaded
                && !_working
                && e.Data.GetDataPresent("Text")
                && DeckReaders.HasReaderFor(e.Data.GetData("Text").ToString());

            e.Effect = accept ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ShowMissingCards((string)e.Argument);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _working = false;
            SetStatus();
            if (e.Error != null)
                ShowError("Error processing deck list:\r\n" + e.Error.Message);
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

        private void ShowMissingCards(string pageUrl)
        {
            string content = GetDeckDocument(pageUrl);
            _worker.ReportProgress(50);
            DeckInfo deckInfo = DeckReaders.GetReader(pageUrl).ReadDeck(content);
            GetOwned(deckInfo);
            DisplayMissingPage(deckInfo);
        }

        private void DisplayMissingPage(DeckInfo deckInfo)
        {
            string fileName = _pageGenerator.MakePage(deckInfo);
            Process.Start(fileName);
        }

        private void GetOwned(DeckInfo deckInfo)
        {
            foreach (var entry in deckInfo.Cards)
            {
                CardInfo card;
                entry.Unknown = !_cards.TryGetValue(entry.CardName, out card);
                if (entry.Unknown)
                {
                    string correctName = _correctCardNames.GetCorrectName(entry.CardName);
                    if (!string.IsNullOrEmpty(correctName))
                    {
                        entry.CorrectName = correctName;
                        entry.Unknown = false;
                        card = _cards[correctName];
                    }
                }
                if (!entry.Unknown)
                {
                    entry.Owned = card.NumberOwned;
                    entry.Set = card.Set;
                }
            }
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

        private static string GetDeckDocument(string pageUrl)
        {
            string content;
            using (WebClient client = new WebClient())
                content = client.DownloadString(pageUrl);
            return content;
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

    }
}
