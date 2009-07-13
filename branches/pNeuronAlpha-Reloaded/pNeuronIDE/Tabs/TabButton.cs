using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace primeira.pNeuron
{
    public class EditorBaseTabButton : EditorBaseButton
    {
        public EditorBaseTabButton()
        {
            this.BackColor = Color.Red;
            base.ShowFocus = false;
            base.ShowLabel = true;
            this.Click += new EventHandler(EditorBaseTabButton_Click);
            base.Image = _unselectedimage;
            Invalidate();
        }

        void EditorBaseTabButton_Click(object sender, EventArgs e)
        {
            this.Selected = true;
        }

        private Image _selectedimage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_selected.png");
        private Image _unselectedimage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_unselected.png");

        private bool _selected = false;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                this.Image = value ? _selectedimage : _unselectedimage;
                this.Invalidate();
            }
        }
    }
}
