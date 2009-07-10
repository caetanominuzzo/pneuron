#define USE_MOMENTUM

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace primeira.pNeuron.Core
{
    /// <summary>
    /// Internal value/delta of a synapse or bias.
    /// </summary>
    [DataContract()]
    public class NeuralValue
    {

        #region Constructors

        /// <summary>
        /// Internal value/delta of a synapse or bias.
        /// </summary>
        /// <param name="neuron">Neuron parent.</param>
        /// <param name="value">Initial value.</param>
        public NeuralValue(INeuron neuron, double value) : this()
        {
            m_neuron = (Neuron)neuron;
            m_weight = value;
            m_delta = 0;
        }

        /// <summary>
        /// The Serializer parameterless ctor.
        /// </summary>
        public NeuralValue()
        {
           
        }

        #endregion

        #region Fields

        private double m_weight, m_delta;

        private Neuron m_neuron;

        

        #endregion

        #region Properties

        /// <summary>
        /// Gets the weight of the synapse or bias.
        /// </summary>
        [DataMember()]
        public double Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        /// <summary>
        /// Gets the delta of the synapse or bias.
        /// </summary>
        [DataMember()]
        public double Delta
        {
            get { return m_delta; }
            set { m_delta = value; } //TODO : Remove
        }

        /// <summary>
        /// Gets the neuron parent.
        /// </summary>
        [DataMember()]
        public Neuron Neuron
        {
            get { return m_neuron; }
            set { m_neuron = value; } //for serializer
        }

        #endregion

        #region Methods

        /// <summary>
        /// After PulseBack calculates the error CalculateDelta appends it on this synapse or bias delta.
        /// </summary>
        /// <param name="Error"></param>
        public void CalculateDelta(double Error, double Momentum)
        {



            //Delta of the Bias
            if (this.Neuron.Bias == this)
            {
#if USE_MOMENTUM
                Delta = Momentum * Delta + (1.0 - Momentum) * Error;
#else
                Delta += Weight * Error;
#endif

            }
            else
            {
#if USE_MOMENTUM
                Delta = Momentum * Delta + (1.0 - Momentum) * Error * Neuron.Value;
#else
                Delta += Neuron.Value * Error;
#endif
            }
        }

        /// <summary>
        /// After CalculateDelta calculates the delta ApplyLearning applies it on this synapse or bias values.
        /// </summary>
        /// <param name="learningRate"></param>
        public void ApplyLearning(double learningRate)
        {
            m_weight += m_delta * learningRate;
        }

        /// <summary>
        /// Resets delta value of this synapse or bias.
        /// Sets delta to zero.
        /// </summary>
        public void ResetLearning()
        {
            m_delta = 0;
        }

        /// <summary>
        /// Reset value of this synapse or bias.
        /// Sets value to random.
        /// </summary>
        public void ResetKnowledgement()
        {
            m_weight = Neuron.NeuralNetwork.Random.GetDouble();
        }

        #endregion


    }
}
