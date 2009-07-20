using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace primeira.pNeuron.Editor.Business
{
    public static class EditorManager
    {
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

        public static EditorBase GetEditorByFilename(string filename)
        {
            EditorBase res = null;

            DocumentBase d = EditorBase.ToObject(filename);

            if (d != null)
            {
                res = (EditorBase)d.GetDefinition.DefaultEditor.GetConstructor(EditorBase.DefaultEditorCtor).Invoke(new object[2] { filename, d });
            }
            else
            //new file
            {
                string ext = Path.GetExtension(filename);

                Type tt = EditorManager.GetEditorByFileVersionExtension(ext);

                if (tt == null)
                    return null;

                res = (EditorBase)tt.GetConstructor(EditorBase.DefaultEditorCtor).Invoke(new object[2] { filename, d });
            }



            return (EditorBase)res;
        }

        public static string GetDialogFilterString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DocumentDefinition d in _knownDocumentDefinition)
            {
                if ((d.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
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
                if ((d.Options & DocumentDefinitionOptions.Virtual) == DocumentDefinitionOptions.Virtual)
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

    }
}
