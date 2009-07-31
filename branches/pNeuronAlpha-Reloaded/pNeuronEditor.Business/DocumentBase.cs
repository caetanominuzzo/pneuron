using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;

namespace pNeuronEditor.Business
{
    [DataContract()]
    public abstract class DocumentBase
    {
        public abstract DocumentDefinition GetDefinition { get; }

        public void ToXml(string filename)
        {
            Stream sm = File.Create(filename);

            Type[] knownTypes = DocumentManager.GetKnownDocumenTypes();

            Array.Resize(ref knownTypes, knownTypes.Length + 1);

            knownTypes[knownTypes.Length - 1] = this.GetType();

            DataContractSerializer ser = new DataContractSerializer(typeof(DocumentBase),
                knownTypes,
                10000000, false, true, null);
            ser.WriteObject(sm, this);

            sm.Close();
        }

        public static DocumentBase ToObject(string filename, Type type)
        {
            try
            {

                Stream sm = File.OpenRead(filename);

                Type[] knownTypes = DocumentManager.GetKnownDocumenTypes();

                Array.Resize(ref knownTypes, knownTypes.Length + 1);

                knownTypes[knownTypes.Length - 1] = type;

                DataContractSerializer ser = new DataContractSerializer(typeof(DocumentBase),
                    DocumentManager.GetKnownDocumenTypes(),
                    10000000, false, true, null);

                DocumentBase res = (DocumentBase)ser.ReadObject(sm);
                sm.Close();

                return res;
            }
            catch
            {
                MessageManager.Alert("File ", filename, " cannot be open.");
            }
            
            return null;
        }

    }
}
