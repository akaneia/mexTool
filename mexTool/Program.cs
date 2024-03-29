﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace mexTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool enableDpi = false;

            foreach (var v in args)
            {
                if (v == "-dpi")
                    enableDpi = true;
            }

            if (enableDpi && Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            ApplicationSettings.Init();
            Application.Run(new MxDtWindow());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
