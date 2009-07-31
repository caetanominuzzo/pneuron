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
                        DocumentManager.RegisterKnownDocumentType(type);
                }
                
            }
        }

        public static Type GetEditorTypeByFileVersionExtension(string extension)
        {
            DocumentDefinition t = DocumentManager.GetDocumentDefinitionByFileExtension(extension);

            if (t != null)
                return t.DefaultEditor;
            
            return null;
        }

        public static IEditorBase CreateEditorByFilename(string filename)
        {
            IEditorBase res = null;
            DocumentBase doc = null;

            FileInfo f = new FileInfo(filename);

            if (!f.Exists)
                f.Create();

            if (f.Length == 0)
            {
                string ext = Path.GetExtension(filename);

                Type tt = EditorManager.GetEditorTypeByFileVersionExtension(ext);

                if (tt == null)
                    return null;

                res = (IEditorBase)tt.GetConstructor(DefaultEditorCtor).Invoke(new object[2] { filename, doc });
            }
            else
            {
                doc = DocumentManager.ToObject(filename);

                if(doc != null)
                    res = (IEditorBase)doc.GetDefinition.DefaultEditor.GetConstructor(DefaultEditorCtor).Invoke(new object[2] { filename, doc });
            }

            return (IEditorBase)res;
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
                res = EditorManager.CreateEditorByFilename(FileName);

                if (res != null && (res.Document.GetDefinition.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
                    DocumentManager.AddDocument(res);
            }

            return res;
        }

    }
}
