using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using pNeuronEditor.Business;

namespace pNeuronEditor.Components
{

    public abstract class EditorBase : UserControl, IEditorBase
    {

        #region Fields



        private bool _selected = false;

        private Timer _saveTimer;

        private bool _showCloseButton = true;

        #endregion

        #region Properties

        public string Filename { get; set; }

        public ITabButton TabButton { get; private set; }

        public bool ShowCloseButton
        {
            get { return _showCloseButton; }
            protected set { _showCloseButton = value; }
        }

        public DocumentBase Document { get; private set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value)
                    return;

                _selected = value;

                if (value)
                    BringToFront();

                if (TabButton != null)
                    ((Button)TabButton).BackgroundImage = value ? TabButton.SelectedImage : TabButton.UnselectedImage;

                if (_selected && OnSelected != null)
                    OnSelected(this);
            }
        }

        #endregion

        #region Ctor
      
        public EditorBase(string filename, DocumentBase data, Type documentType)
        {
            if (data == null)
                this.Document = (DocumentBase)documentType.GetConstructor(System.Type.EmptyTypes).Invoke(System.Type.EmptyTypes);
            else
                this.Document = data;

            this.Filename = filename;

            this.Dock = DockStyle.Fill;

            this.BorderStyle = BorderStyle.None;

            this.OnChanged += new ChangedDelegate(EditorBase_OnChanged);

            this.TimeOPen = DateTime.Now;

            InitializeComponent();

        }

        private void InitializeComponent()
        {
            if ((Document.GetDefinition.Options & DocumentDefinitionOptions.TimerSaver) == DocumentDefinitionOptions.TimerSaver)
            {
                _saveTimer = new Timer();
                _saveTimer.Interval = 1000;
                _saveTimer.Tick += new EventHandler(_saveTimer_Tick);
            }



            if (TabManager.GetInstance().TabControl != null)
            {
                TabButton = TabManager.GetInstance().CreateTabButton();

                ((Button)TabButton).Tag = this;

                ((Button)TabButton).Image = Document.GetDefinition.Icon;

                if ((this.Document.GetDefinition.Options & DocumentDefinitionOptions.DontShowLabel) != DocumentDefinitionOptions.DontShowLabel)
                {
                    ((Button)this.TabButton).Text = this.Filename;

                    FileManager.MeasureFromIDC((Button)this.TabButton);
                }
                else
                {
                    ((Button)this.TabButton).ImageAlign = ContentAlignment.MiddleCenter;
                }

                ToolTip tip = new ToolTip();

                tip.SetToolTip((Control)this.TabButton, this.Filename);

                ((Button)TabButton).Click += new EventHandler(TabButton_Click);
            }
        }

        #endregion

        #region Methods

        public void PrepareToClose()
        {

            //Since if !DocumentDefinitionOptions.Virtual there is not _timer set;
            if (_saveTimer != null)
                _saveTimer.Stop();

            DocumentManager.ToXml(this.Document, this.Filename);
        }

        #endregion

        #region Event Handlers

        private void _saveTimer_Tick(object sender, EventArgs e)
        {
            //Since if !DocumentDefinitionOptions.Virtual there is not _timer set;
            if (_saveTimer != null)
                _saveTimer.Stop();

            DocumentManager.ToXml(this.Document, this.Filename);
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            this.Selected = true;
        }

        private void EditorBase_OnChanged()
        {
            //Since if !DocumentDefinitionOptions.Virtual there is not _timer set;
            if (_saveTimer != null)
            {
                _saveTimer.Stop();
                _saveTimer.Start();
            }
        }

        #endregion

        #region Events

        public event SelectedDelegate OnSelected;

        public delegate void ChangedDelegate();
        public event ChangedDelegate OnChanged;

        protected virtual void Changed()
        {
            if (OnChanged != null)
                OnChanged();
        }

        #endregion

        public DateTime TimeOPen
        {
            get;
            private set;
        }
    }
}
