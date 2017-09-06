﻿using System;
using System.Windows.Forms;
using MagicDuels;
using AngleSharp.Parser.Html;
using AngleSharp.Dom.Html;
using System.Net;
using System.Diagnostics;
using System.ComponentModel;

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
            _pageGenerator.Initialize();
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
                ShowError("Unexpected Error:\r\n" + ex.ToString());
                Application.Exit();
            }
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (_cardDataLoaded &&
                !_working &&
                e.Data.GetDataPresent("Text") &&
                e.Data.GetData("Text").ToString().StartsWith("https://www.magicduelshelper.com/decklist/details", StringComparison.InvariantCultureIgnoreCase))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
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
            try
            {
                MagicDuelsCardManager manager = new MagicDuelsCardManager();
                _cards = manager.GetCards(_profilePath);
                _cardDataLoaded = true;
                SetStatus();
            }
            catch (BadCardDataException)
            {
                ShowError("Unable to load Cards.xml\r\nCheck that it exists in the application folder.");
                Application.Exit();
            }
            catch (BadSteamProfileException)
            {
                ShowError("Unable to load steam profile. Use File->Options to set the correct profile path.\r\nDrag drop of decks is disabled until the correct profile path is set.");
                _cardDataLoaded = false;
                SetStatus();
            }
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
            DeckInfo deckInfo = GetDeckInfo(pageUrl);
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
                if (!entry.Unknown)
                {
                    entry.Owned = card.NumberOwned;
                    entry.Set = card.Set;
                }
            }
        }

        private DeckInfo GetDeckInfo(string pageUrl)
        {
            IHtmlDocument doc = GetDeckDocument(pageUrl);
            var deckTitle = doc.QuerySelector("h1").TextContent;
            var deckList = doc.QuerySelectorAll("a[href^='/cards?cardName=']");

            _worker.ReportProgress(50);

            const string titlePrefix = "Magic Duels Deck: ";
            if (deckTitle.StartsWith(titlePrefix))
                deckTitle = deckTitle.Substring(titlePrefix.Length);

            DeckInfo deckInfo = new DeckInfo(deckTitle);

            foreach (var entry in deckList)
            {
                string cardName = entry.TextContent;
                if (!MagicDuelsHelper.IsBasicLand(cardName))
                {
                    string numberOf = entry.PreviousSibling.TextContent.Trim();
                    int number = int.Parse(numberOf.Substring(0, numberOf.Length - 1));
                    deckInfo.Cards.Add(new DeckEntry
                    {
                        Required = number,
                        CardName = cardName,
                    });
                }
            }

            return deckInfo;
        }

        private static IHtmlDocument GetDeckDocument(string pageUrl)
        {
            string content;
            using (WebClient client = new WebClient())
                content = client.DownloadString(pageUrl);

            HtmlParser parser = new HtmlParser();
            return parser.Parse(content);
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

        private void ShowError(string msg)
        {
            System.Media.SystemSounds.Asterisk.Play();
            Activate();
            MessageBox.Show(this, msg, "Magic Duels Deck Checker");
        }
    }
}
