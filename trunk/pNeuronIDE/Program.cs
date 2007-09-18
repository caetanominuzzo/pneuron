using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace primeira.pNeuron
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splasher.ShowSplashScreen();
            Application.Run(new pNeuronIDE());
        }
    }
}