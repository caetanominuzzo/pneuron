using System;
using System.Collections.Generic;
using System.Windows.Forms;
using primeira.pNeuron.Editor.Business;


namespace primeira.pNeuron.Editor
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



            //#if RELEASE
            //    Splasher.ShowSplashScreen();
            //#endif

            Application.Run(new pNeuronEditor());
        }
    }
}