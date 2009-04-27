using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using pNeuron.Imagining;
using pNeuron.Imagining.Common;
using pNeuron.Imagining.ObjectDetection;

namespace pNeuron.Imagining.ObjectDetection.Test
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pNeuron.Imagining.Capture capture = new Capture();
            capture.OnProcessFrame += new Capture.ProcessFrameEventHandler(capture_OnProcessFrame);
            capture.OpenVideoSource();
        }

        void capture_OnProcessFrame(Bitmap image)
        {
            ObjectDetection.Face f = new Face();
            f.Detect(image);
        }
    }
}