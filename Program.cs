using System;
using System.Windows.Forms;
using ImapToImap_Transfer.Forms;

namespace ImapToImap_Transfer
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}