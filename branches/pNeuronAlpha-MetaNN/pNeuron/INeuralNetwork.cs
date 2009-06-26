using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron.Core
{

    public interface INeuralNetwork
    {

        void Pulse();
        void ApplyLearning();
        void ResetLearning();
        void PulseBack(double[] desiredResults);

        double LearningRate { get; }

        double Momentum { get; }

        List<Neuron> Neuron { get; }
    }
}
