using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace pNeuronEditor.Business
{

    public static class DocumentManager
    {
        #region Fields

        private static List<Type> _knownDocumentType = new List<Type>();
        
        private static List<DocumentDefinition> _knownDocumentDefinition = new List<DocumentDefinition>();

        #endregion

        #region Get DocumentDefinition & DocumentType

        private static DocumentDefinition GetDocumentDefinitionByDocumentType(Type documentType)
        {
            PropertyInfo pDef = documentType.GetProperty("DocumentDefinition");

            if (pDef == null)
            {
                MessageManager.Alert("Missing DocumentDefinition Property in ", documentType.Name, " Class.");
                return null;
            }

            return (DocumentDefinition)pDef.GetValue(null, null);
        }

        private static DocumentDefinition GetDocumentDefinitionByEditorType(Type editorType)
        {
            try
            {
                return (from x in _knownDocumentDefinition where x.DefaultEditor == editorType select (DocumentDefinition)x).FirstOrDefault();
            }
            catch
            {
                MessageManager.Alert("No DocumentDefinition found for ", editorType.Name, " Class");
                return null;
            }
        }

        public static DocumentDefinition GetDocumentDefinitionByFileExtension(string extension)
        {
            try
            {
                return (from x in _knownDocumentDefinition where x.Extension == extension select (DocumentDefinition)x).FirstOrDefault();
            }
            catch
            {
                MessageManager.Alert("No Editor Class found for *", extension, " files.");
                return null;
            }
        }

        public static DocumentDefinition GetDocumentDefinitionByFilename(string filename)
        {
            string ext = Path.GetExtension(filename);

            Type type = EditorManager.GetEditorTypeByFileVersionExtension(ext);

            return GetDocumentDefinitionByEditorType(type);
        }

        public static Type[] GetKnownDocumenTypes()
        {
            return _knownDocumentType.ToArray();
        }

        public static DocumentDefinition[] GetKnowDocumentDefinition()
        {
            return _knownDocumentDefinition.ToArray();
        }

        #endregion

        public static void RegisterKnownDocumentType(Type documentType)
        {
            DocumentDefinition def = GetDocumentDefinitionByDocumentType(documentType);

            if (def != null)
            {
                _knownDocumentDefinition.Add(def);
                _knownDocumentType.Add(documentType);
            }
        }

        public static string GetDialogFilterString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DocumentDefinition d in _knownDocumentDefinition)
            {
                if ((d.Options & DocumentDefinitionOptions.ShowInOpen) == DocumentDefinitionOptions.ShowInOpen)
                    sb.Append(string.Format("{0} (*{1})|*{1}|", d.Name, d.Extension));
            }

            sb.Append("All files (*.*)|*.*");

            return sb.ToString();
        }

        public static int GetDialogFilterIndex(DocumentDefinition FileVersion)
        {
            int i = 0;
            foreach (DocumentDefinition d in _knownDocumentDefinition)
            {
                if ((d.Options & DocumentDefinitionOptions.ShowInOpen) == DocumentDefinitionOptions.ShowInOpen)
                    continue;
                else i++;

                if (d == FileVersion)
                    return i;
            }

            return 1;
        }

        #region Serialization

        public static DocumentBase ToObject(string filename)
        {
            return DocumentBase.ToObject(filename, null);
        }

        public static void ToXml(DocumentBase document, string filename)
        {
            document.ToXml(filename);
        }

        #endregion

        #region New/Open/Save Document

        internal static void AddDocument(IEditorBase editor)
        {
            if(TabManager.GetInstance().TabControl != null)
                TabManager.GetInstance().TabControl.AddTab(editor);

            if ((editor.Document.GetDefinition.Options & DocumentDefinitionOptions.ShowInRecent) == DocumentDefinitionOptions.ShowInRecent)
            {
                    if(FileManager.Recent != null)
                        FileManager.Recent.AddRecent(editor.Filename);
            }

            editor.OnSelected += new SelectedDelegate(TabControl_OnSelected);

            editor.Selected = true;
        }

        static void TabControl_OnSelected(IEditorBase sender)
        {
            TabManager.GetInstance().ActiveEditor = (IEditorBase)sender;
        }


        public static void OpenOrCreateDocument(bool NewFile, DocumentDefinition FileVersion)
        {
            OpenFileDialog s = new OpenFileDialog();

            s.CheckFileExists = false;

            if (NewFile)
                s.FileName = FileManager.GetNewFile(FileVersion, BaseDir);

            s.Filter = DocumentManager.GetDialogFilterString();

            s.DefaultExt = FileVersion.Extension;

            s.FilterIndex = DocumentManager.GetDialogFilterIndex(FileVersion);

            s.InitialDirectory = BaseDir;

            if (s.ShowDialog() == DialogResult.OK)
            {
                BaseDir = s.InitialDirectory;

                string ss = Path.Combine(BaseDir, s.FileName);

                if (!File.Exists(ss))
                    File.Create(ss).Close();

                LoadDocument(ss);
            }
        }

        private static string _baseDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string BaseDir
        {
            get { return _baseDir; }
            private set { _baseDir = value; }
        }

        internal static DocumentBase LoadDocument(string filename)
        {
            return EditorManager.LoadEditor(filename).Document;
        }

        #endregion


    }
}
