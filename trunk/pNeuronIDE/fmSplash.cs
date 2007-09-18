using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace primeira.pNeuron
{
    

    public partial class fmSplash : Form
    {
        public fmSplash()
        {
            InitializeComponent();
        }
    }

    static public class Splasher
    {
        static fmSplash m_splash = null;

        static private void ShowSplash()
        {
            m_splash = new fmSplash();
            Application.Run(m_splash);
        }

        

        static public void CloseSplash()
        {
            m_splash.Close();
            
        }

        static Thread m_thread = null;

        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (m_splash != null)
                return;
            m_thread = new Thread(new ThreadStart(ShowSplash));
            m_thread.IsBackground = true;
            m_thread.ApartmentState = ApartmentState.STA;
            m_thread.Start();
        }
    }
}