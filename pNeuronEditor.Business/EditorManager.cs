using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace pNeuronEditor.Business
{
    public static class EditorManager
    {

        private static Type[] _defaultEditorCtor = new Type[2] { typeof(string), typeof(DocumentBase) };

        internal static Type[] DefaultEditorCtor
        {
            get { return _defaultEditorCtor; }
        }


        private static List<DocumentDefinition> _knownDocumentDefinition = new List<DocumentDefinition>();

        public static void RegisterEditors()
        {
            string[] dlls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");

            Assembly ass = null;
            foreach (string dll in dlls)
            {
                ass = Assembly.LoadFile(dll);
                Type[] types = ass.GetTypes();

                foreach (Type type in types)
                {
                    if (type.BaseType == typeof(DocumentBase))
                    {
                        //Register the Editor
                        PropertyInfo p = type.GetProperty("DocumentDefinition");
                        _knownDocumentDefinition.Add((DocumentDefinition)p.GetValue(null, null));

                        //Register the type document
                        DocumentManager.RegisterKnownDocumentType(type);
                    }
                }
                
            }
        }

        public static Type GetEditorByFileVersionExtension(string extension)
        {
            DocumentDefinition t = (from x in _knownDocumentDefinition where x.Extension == extension select (DocumentDefinition)x).FirstOrDefault();

            if (t == null)
                return null;
            else
                return t.DefaultEditor;
        }

        public static IEditorBase GetEditorByFilename(string filename)
        {
            IEditorBase res = null;

            DocumentBase d = DocumentManager.ToObject(filename);

            if (d != null)
            {
                res = (IEditorBase)d.GetDefinition.DefaultEditor.GetConstructor(DefaultEditorCtor).Invoke(new object[2] { filename, d });
            }
            else
            //new file
            {
                string ext = Path.GetExtension(filename);

                Type tt = EditorManager.GetEditorByFileVersionExtension(ext);

                if (tt == null)
                    return null;

                res = (IEditorBase)tt.GetConstructor(DefaultEditorCtor).Invoke(new object[2] { filename, d });
            }

            return (IEditorBase)res;
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

        public static DocumentDefinition[] GetAllDocumentDefinition()
        {
            return _knownDocumentDefinition.ToArray();
        }

        public static IEditorBase LoadEditor(string FileName)
        {
            IEditorBase res = TabManager.GetInstance().GetDocumentByFilename(FileName);

            if (res != null)
            {
                res.Selected = true;
                return res;
            }

            if (res == null)
            {
                res = EditorManager.GetEditorByFilename(FileName);

                if (res != null && (res.Document.GetDefinition.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
                    DocumentManager.AddDocument(res);

            }

            return res;
        }

    }
}
