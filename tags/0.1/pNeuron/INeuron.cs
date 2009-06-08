using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron.Core
{
    public interface INeuron
    {
        void Pulse(object state);
        void ApplyLearning(double learningRate);
        void ResetLearning();
        void PulseBack(double desiredResult);

        NeuralValue Bias { get; }

        double Error { get; set; }
        double LastError { get; }

        NeuronTypes NeuronType { get; set; }

        double Value { get; set; }

        NeuralNetwork NeuralNetwork { get; }

        int ID { get; }

        int Command { get; }
    }
}
