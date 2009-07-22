using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using pNeuronEditor.Business;

namespace pNeuronEditor.EditorTemplate
{
    [DataContract()]
    public class EditorDocumentName : DocumentBase
    {
        private static DocumentDefinition _definition =
            new DocumentDefinition()
            {
                Name = "asdf",
                DefaultName = "sdaf",
                Description = "fff",
                Extension = ".xxx",
                Id = new Guid("513ff96c-0d23-44f4-82ab-0dea5a62dcd3"),
                //Icon = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\folder_noborder.gif"),
                DefaultEditor = typeof(EditorName)
            };

        public static DocumentDefinition DocumentDefinition
        {
            get { return _definition; }
        }

        public override DocumentDefinition GetDefinition
        {
            get { return _definition; }
        }
    }
}


