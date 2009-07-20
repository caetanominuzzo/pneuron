//#define DESIGNER

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
using primeira.pNeuron.Editor.Business;

namespace primeira.pNeuron.Editor
{

//#if DESIGNER
    //public class EditorBase : UserControl
    //{
//#else
        public abstract class EditorBase : UserControl
    {
//#endif

        #region Fields

        private bool _selected = false;

        private bool _showCloseButton = true;

        private static Type[] _defaultEditorCtor = new Type[2] { typeof(string), typeof(DocumentBase) };

        private Timer _saveTimer;

        #endregion

        #region Properties

        public static Type[] DefaultEditorCtor
        {
            get { return _defaultEditorCtor; }
        }

        public string Filename { get; set; }

        public TabButton TabButton { get; internal set; }

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

                TabButton.BackgroundImage = value ? TabButton.Selectedimage : TabButton.Unselectedimage;

                if (_selected && OnSelected != null)
                    OnSelected(this);
            }
        }

        public bool ShowCloseButton
        {
            get { return _showCloseButton; }
            protected set { _showCloseButton = value; }
        }

        public string TabCaption { get; private set; }

        public DocumentBase Data { get; private set; }

        #endregion

        #region Ctor

        public EditorBase()
        {
        }

        public EditorBase(string filename, DocumentBase data, Type documentType)
        {
            if (data == null)
                this.Data = (DocumentBase)documentType.GetConstructor(System.Type.EmptyTypes).Invoke(System.Type.EmptyTypes);
            else
                this.Data = data;

            this.Filename = filename;

            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.None;

            this.OnChanged += new ChangedDelegate(EditorBase_OnChanged);

            InitializeComponent();

        }

        private void InitializeComponent()
        {
            _saveTimer = new Timer();
            _saveTimer.Interval = 5000;
            _saveTimer.Tick += new EventHandler(_saveTimer_Tick);

            TabButton = new TabButton();
            TabButton.Tag = this;
            TabButton.Font = new Font(SystemFonts.CaptionFont.FontFamily, 12);
            TabButton.ForeColor = Color.DarkGray;

            if ((this.Data.GetDefinition.Options & DocumentDefinitionOptions.DontShowLabel) != DocumentDefinitionOptions.DontShowLabel)
            {
                this.TabButton.Text = this.Filename;
                this.TabButton.ImageAlign = ContentAlignment.TopLeft;
            }
            else
            {
                this.TabButton.ImageAlign = ContentAlignment.MiddleCenter;
            }

            TabButton.Click += new EventHandler(TabButton_Click);

            TabButton.Image = Data.GetDefinition.Icon;
        }

        #endregion

        #region Methods

        public void PrepareToClose()
        {
            _saveTimer.Stop();
            ToXml();
        }

        #endregion

        #region Event Handlers

        private void _saveTimer_Tick(object sender, EventArgs e)
        {
            _saveTimer.Stop();
            ToXml();
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            this.Selected = true;
        }

        private void EditorBase_OnChanged()
        {
            _saveTimer.Stop();
            _saveTimer.Start();
        }

        #endregion

        #region Serialization

        protected void ToXml()
        {
            Stream sm = File.Create(Filename);

            DataContractSerializer ser = new DataContractSerializer(typeof(DocumentBase),
                 DocumentManager.GetKnownDocumenTypes(),
                10000000, false, true, null);
            ser.WriteObject(sm, this.Data);

            sm.Close();
        }

        public static DocumentBase ToObject(string Filename)
        {
            if (!File.Exists(Filename))
                File.Create(Filename).Close();

            //todo:shit
            string s = File.ReadAllText(Filename);

            if (s.Length > 0)
            {

                Stream sm = File.OpenRead(Filename);

                DataContractSerializer ser = new DataContractSerializer(typeof(DocumentBase),
                    DocumentManager.GetKnownDocumenTypes(),
                    10000000, false, true, null);

                DocumentBase res = (DocumentBase)ser.ReadObject(sm);
                sm.Close();

                return res;
            }
            else
                return null;
        }

        #endregion

        #region Events

        public delegate void ChangedDelegate();
        public event ChangedDelegate OnChanged;

        public delegate void SelectedDelegate(EditorBase sender);
        public event SelectedDelegate OnSelected;

        protected virtual void Changed()
        {
            if (OnChanged != null)
                OnChanged();
        }

        #endregion

        

    }
}
