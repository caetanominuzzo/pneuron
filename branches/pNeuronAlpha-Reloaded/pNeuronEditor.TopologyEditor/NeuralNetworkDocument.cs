﻿using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using primeira.pNeuron.Core;
using primeira.pNeuron.Editor.Business;

namespace pNeuronEditor.TopologyEditor
{
    [DataContract()]
    [KnownType(typeof(Neuron))]
    public class NeuralNetworkDocument : DocumentBase
    {
        private static DocumentDefinition _definition =
            new DocumentDefinition()
            {
                Name = "Topology File",
                DefaultName = "Topology {0:00}",
                Description = "pNeuron Topology File",
                Extension = ".pne",
                Id = new Guid("513ff96c-0d23-44f4-82ab-0dea5a62dcd3"),
                Icon = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\file_noborder.gif"),
                DefaultEditor = typeof(NeuralNetworkEditor)
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
    }
}