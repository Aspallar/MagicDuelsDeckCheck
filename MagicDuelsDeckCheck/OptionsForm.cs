using System;
using System.IO;
using System.Windows.Forms;

namespace MagicDuelsDeckCheck
{
    public partial class OptionsForm : Form
    {
        public string ProfilePath { get; internal set; }

        public OptionsForm()
        {
            InitializeComponent();
            textBoxProfilePath.Text = Properties.Settings.Default.MagicDuelsSteamProfile;
            labelProfileHelpText.Text = "Your profile will be located in in your steam folder usually 'C:\\Program Files (x86)\\Steam' in the sub-folder\r\n'userdata\\<userid>\\316010\\remote' where <userid> is your steam user id and will have a file name of '<userid>.profile'\r\n\r\ne.g. C:\\Program Files (x86)\\Steam\\userdata\\111849480\\316010\\remote\\111849480.profile";
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
                dlg.Title = "Select Magic Duels steam profile";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    textBoxProfilePath.Text = dlg.FileName;
            }
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ProfilePath = textBoxProfilePath.Text;
            Properties.Settings.Default.MagicDuelsSteamProfile = ProfilePath;
            Properties.Settings.Default.Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
