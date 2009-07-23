using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace pNeuronEditor.Business
{
    public static class RevisionManager
    {
        public static void CreateRevision(string filename, IRevision document)
        {
            string revisionPath = Path.ChangeExtension(filename, "");

            if (!Directory.Exists(revisionPath))
                Directory.CreateDirectory(revisionPath);

            Guid g = Guid.NewGuid();

            string parent = Path.Combine(revisionPath, g.ToString());

            if (document.ParentRevision != null)
            {
                DocumentBase doc = DocumentManager.ToObject(document.ParentRevision);

                //todo:shit
                List<string> child = new List<string>();
                child.AddRange(doc.ChildRevision);
                child.Add(parent);

                doc.ChildRevision = child.ToArray();

                doc.ToXml(document.ParentRevision);
            }

            document.ToXml(parent);

            document.ParentRevision = parent;

            document.ToXml(filename);
        }

        public static IRevision Prior(IRevision document)
        {
            if (document.ParentRevision != null)
                return DocumentManager.ToObject(document.ParentRevision);
            else return document;
        }

        public static IRevision Next(IRevision document)
        {
            if (document.ChildRevision.Length > 0)
                return DocumentManager.ToObject(document.ChildRevision[0]);
            else return document;
        }

    }
}
