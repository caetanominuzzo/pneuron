using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace pNeuronEditor.Business
{
    public static class DocumentManager
    {

        private static List<Type> _knownDocumentType = new List<Type>();

        public static void RegisterKnownDocumentType(Type documentType)
        {
            _knownDocumentType.Add(documentType);
        }

        public static Type[] GetKnownDocumenTypes()
        {
            return _knownDocumentType.ToArray();
        }

        public static DocumentBase ToObject(string filename)
        {
            return DocumentBase.ToObject(filename);
        }

        public static void ToXml(DocumentBase document, string filename)
        {
            document.ToXml(filename);
        }


        #region New/Open/Save Document

        private static void AddDocument(IEditorBase editor)
        {
            if(TabManager.GetInstance().TabControl != null)
                TabManager.GetInstance().TabControl.AddTab(editor);

            //If not virtual add in recents list
            if ((editor.Document.GetDefinition.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
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


        public static void OpenDocument(DocumentDefinition FileVersion)
        {
            openOrNewDocument(false, FileVersion);
        }

        public static void NewDocument(DocumentDefinition FileVersion)
        {
            openOrNewDocument(true, FileVersion);
        }

        private static void openOrNewDocument(bool NewFile, DocumentDefinition FileVersion)
        {
            OpenFileDialog s = new OpenFileDialog();

            s.CheckFileExists = false;

            if (NewFile)
                s.FileName = FileManager.GetNewFile(FileVersion, BaseDir);

            s.Filter = EditorManager.GetDialogFilterString();

            s.DefaultExt = FileVersion.Extension;

            s.FilterIndex = EditorManager.GetDialogFilterIndex(FileVersion);

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

        public static IEditorBase LoadDocument(string FileName)
        {
            IEditorBase res = TabManager.GetInstance().GetDocumentByFilename(FileName);

            if (res == null)
            {
                res = EditorManager.GetEditorByFilename(FileName);
                
                if (res != null)
                    AddDocument(res);
            }

            res.Selected = true;

            return res;
        }

        #endregion


    }
}
