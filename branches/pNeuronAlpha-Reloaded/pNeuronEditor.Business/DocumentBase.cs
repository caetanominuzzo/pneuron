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

            DataContractSerializer ser = new DataContractSerializer(typeof(DocumentBase),
                 DocumentManager.GetKnownDocumenTypes(),
                10000000, false, true, null);
            ser.WriteObject(sm, this);

            sm.Close();
        }

        public static DocumentBase ToObject(string filename)
        {
            if (!File.Exists(filename))
                File.Create(filename).Close();

            //todo:shit
            string s = File.ReadAllText(filename);

            if (s.Length > 0)
            {

                Stream sm = File.OpenRead(filename);

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

    }
}
