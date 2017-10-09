using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MagicDuelsDeckCheck
{
    public partial class SelectTemplateForm : Form
    {
        string[] _templates;

        public string TemplatePath { get; set; }

        public SelectTemplateForm(string templatePath)
        {
            InitializeComponent();
            GetTemplates();
            PopulateTemplateDropdown();
            SetInitalSelection(templatePath);
        }

        private void SetInitalSelection(string templatePath)
        {
            comboBoxTemplate.SelectedIndex = 0;
            for (int k = 0; k < _templates.Length; k++)
            {
                if (_templates[k] == templatePath)
                {
                    comboBoxTemplate.SelectedIndex = k + 1;
                    break; // for
                }
            }
        }

        private void PopulateTemplateDropdown()
        {
            comboBoxTemplate.Items.Add("Basic");
            foreach (string template in _templates)
                comboBoxTemplate.Items.Add(Path.GetFileNameWithoutExtension(template));
        }

        private void GetTemplates()
        {
            _templates = Directory.GetDirectories(AppPaths.UserTemplatesFolder)
                .Where(x => Path.GetFileNameWithoutExtension(x)[0] != '-')
                .ToArray();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            int index = comboBoxTemplate.SelectedIndex;
            if (index == 0)
                TemplatePath = "";
            else
                TemplatePath = _templates[index - 1];
        }
    }


}
