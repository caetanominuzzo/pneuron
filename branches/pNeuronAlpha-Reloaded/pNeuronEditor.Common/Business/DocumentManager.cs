using System;
using System.Collections.Generic;

namespace primeira.pNeuron.Editor.Business
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
    }
}
