using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using primeira.pNeuron.Core;
using primeira.Editor;

namespace pNeuronEditor.Topology
{
    [DataContract()]
    [KnownType(typeof(Neuron))]
    [DocumentDefinition(Name = "Topology",
            DefaultFileName = "Topology {0:00}",
            Description = "pNeuron Topology Document.",
            DefaultFileExtension = ".pne",
            DefaultEditor = typeof(NeuralNetworkEditor),
            Options = DocumentDefinitionOptions.UserFile)]
    public class NeuralNetworkDocument : DocumentBase
    {
        [DataMember()]
        public NeuralNetwork NeuralNetwork { get; set; }

    }
}
