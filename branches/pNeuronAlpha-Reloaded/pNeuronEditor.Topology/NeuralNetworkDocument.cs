using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using primeira.pNeuron.Core;
using pNeuronEditor.Business;

namespace pNeuronEditor.Topology
{
    [DataContract()]
    [KnownType(typeof(Neuron))]
    public class NeuralNetworkDocument : DocumentBase
    {
        private static DocumentDefinition _definition =
            new DocumentDefinition()
            {
                Name = "Topology",
                DefaultName = "Topology {0:00}",
                Description = "pNeuron Topology Document",
                Extension = ".pne",
                Id = new Guid("513ff96c-0d23-44f4-82ab-0dea5a62dcd3"),
                Icon = Image.FromFile(@"D:\Media\Icons\24x24\full_page_noborder.png"),
                DefaultEditor = typeof(NeuralNetworkEditor),
                Options = DocumentDefinitionOptions.UserFile
            };

        public static DocumentDefinition DocumentDefinition
        {
            get { return _definition; }
        }

        public override DocumentDefinition GetDefinition
        {
            get { return _definition; }
        }

        [DataMember()]
        public NeuralNetwork NeuralNetwork { get; set; }

        public static DocumentBase ToObject(string filename)
        {
            return DocumentBase.ToObject(filename, typeof(NeuralNetworkDocument));
        }

    }
}
