using System;
using System.Security.Permissions;
using System.Windows.Forms;
using MagicDuelsDeckCheck.Properties;
using System.IO;

namespace MagicDuelsDeckCheck
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            CreateTemplatesFolder();
            Application.Run(new FormMain(
                Settings.Default.MagicDuelsSteamProfile,
                Settings.Default.MruSize,
                Settings.Default.UserAgent
            ));
        }

        private static void CreateTemplatesFolder()
        {
            if (Directory.Exists(AppPaths.UserTemplatesFolder))
                return;

            string templatePath = Application.StartupPath + "\\Templates";
            var source = new DirectoryInfo(templatePath);
            source.CopyTo(AppPaths.UserTemplatesFolder, true);
            Settings.Default.TemplatePath = AppPaths.UserTemplatesFolder + "\\Panels - Cyborg";
            Settings.Default.Save();
        }

        //private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    Exception ex = (Exception)e.ExceptionObject;
        //    MessageBox.Show(ex.ToString());
        //}

        //private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        //{
        //    MessageBox.Show(e.Exception.ToString());
        //}
    }
}
