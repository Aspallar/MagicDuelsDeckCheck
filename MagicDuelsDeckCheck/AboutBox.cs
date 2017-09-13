using System;
using System.Reflection;
using System.Windows.Forms;

namespace MagicDuelsDeckCheck
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            InitializeText();
        }

        private void InitializeText()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Text = "About " + GetTitle(assembly);
            labelProductName.Text = GetProduct(assembly);
            labelVersion.Text = "Version " + GetVersion(assembly);
            labelCopyright.Text = GetCopyright(assembly);
        }

        private string GetCopyright(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
                return "";
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }

        private string GetVersion(Assembly assembly)
        {
            Version version = assembly.GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }

        private string GetTitle(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(titleAttribute.Title))
                {
                    return titleAttribute.Title;
                }
            }
            return System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase);
        }

        private string GetProduct(Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
                return "";
            return ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }
}
