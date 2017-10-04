using System;
using System.IO;
using System.Windows.Forms;
using MagicDuelsDeckCheck.Properties;

namespace MagicDuelsDeckCheck
{
    public partial class OptionsForm : Form
    {
        public string ProfilePath { get; private set; }
        public int MruSize { get; private set; }
        public string UserAgent { get; set; }

        public OptionsForm()
        {
            InitializeComponent();
            textBoxProfilePath.Text = Settings.Default.MagicDuelsSteamProfile;
            numericUpDownMruSize.Value = (decimal)Settings.Default.MruSize;
            textBoxUserAgent.Text = Settings.Default.UserAgent;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            string initialFolder = GetInitialFolder();

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (!string.IsNullOrEmpty(initialFolder))
                    dlg.InitialDirectory = initialFolder;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                dlg.DefaultExt = "profile";
                dlg.Filter = "profile files (*.profile)|*.profile";
                dlg.Multiselect = false;
                dlg.DereferenceLinks = false;
                dlg.Title = "Select Magic Duels steam profile";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    textBoxProfilePath.Text = dlg.FileName;
            }
        }

        private void buttonDefaultUserAgent_Click(object sender, EventArgs e)
        {
            textBoxUserAgent.Text = (string)Settings.Default.Properties["UserAgent"].DefaultValue;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ProfilePath = textBoxProfilePath.Text;
            MruSize = (int)numericUpDownMruSize.Value;
            UserAgent = textBoxUserAgent.Text;
            Settings.Default.MagicDuelsSteamProfile = ProfilePath;
            Settings.Default.MruSize = MruSize;
            Settings.Default.UserAgent = UserAgent;
            Settings.Default.Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private string GetInitialFolder()
        {
            const string path = @":\Program Files (x86)\Steam\userdata";
            foreach (char driveLetter in new char[] { 'C', 'D', 'E', 'F' })
            {
                string fullPath = driveLetter + path;
                if (Directory.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }
    }
}
