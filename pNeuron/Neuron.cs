using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace primeira.pNeuron.Core
{
    /// <summary>
    /// Represents a neuron.
    /// </summary>
    [DataContract()]
    public class Neuron : INeuron
    {

        #region Constructors
    
        /// <summary>
        /// Represents a neuron.
        /// </summary>
        /// <param name="net">Parent Neural Network.</param>
        public Neuron(NeuralNetwork net)
            : this(net.Random.GetDouble(), net)
        {

        }

        /// <summary>
        /// Represents a neuron.
        /// </summary>
        /// <param name="biasValue">Value of neuron bias.</param>
        /// <param name="net">Parent Neural Network.</param>
        public Neuron(double biasValue, NeuralNetwork net) : this()
        {
            m_bias = new NeuralValue(this, biasValue);
            m_neuralNetwork = net;
            m_id = net.GeneratorID;
            net.Neuron.Add(this);
        }

               /// <summary>
        /// The Serializer parameterless ctor.
        /// </summary>
        public Neuron()
        {
            m_error = 0;
            m_input = new Dictionary<INeuron, NeuralValue>();
            m_output = new List<Neuron>();
        }


        #endregion

        #region Fields

        private int m_inputReady = 0;

        private int m_outputReady = 0;

        private double m_value, m_error, m_lastError;

        private NeuralValue m_bias;

        private NeuralNetwork m_neuralNetwork;

        private Dictionary<INeuron, NeuralValue> m_input;

        private List<Neuron> m_output;

        private NeuronTypes m_neuronType = NeuronTypes.Hidden;


        private int m_id = 0;

        private int m_command = NeuralNetwork.CMD_NOTHING;

        private ManualResetEvent[] m_resetEvent;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Parent Neural Network.
        /// </summary>
        [DataMember()]
        public NeuralNetwork NeuralNetwork
        {
            get { return m_neuralNetwork; }
            set { m_neuralNetwork = value; }
        }

        /// <summary>
        /// Gets or sets the neuron value .
        /// </summary>
        [DataMember()]
        public double Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// Gets a dictionary with all input synapses of this neuron.
        /// </summary>
        [DataMember()]
        private Dictionary<INeuron, NeuralValue> Input
        {
            get { return m_input; }
            set { m_input = value; }
        }

        /// <summary>
        /// Gets a dictionary with all output synapses of this neuron.
        /// </summary>
        [DataMember()]
        private List<Neuron> Output
        {
            get { return m_output; }
            set { m_output = value; }
        }

        /// <summary>
        /// How many inputs of this neuron are already calculated and are able to be read.
        /// When InputReady are equal to item in Input thus all Input are ready and the neuron can pulses.
        /// </summary>
        [XmlIgnore()]
        public int InputReady
        {
            get { return m_inputReady; }
            internal set { m_inputReady = value; }
        }

        /// <summary>
        /// How many outputs of this neuron are already calculated and are able to be read.
        /// When OututReady are equal to item in Output thus all Output are ready and the neuron can pulses back.
        /// </summary>
        [XmlIgnore()]
        public int OutputReady
        {
            get { return m_outputReady; }
            internal set { m_outputReady = value; }
        }

        /// <summary>
        /// Gets the neuron bias.
        /// </summary>
        [DataMember()]
        public NeuralValue Bias
        {
            get { return m_bias; }
            set { m_bias = value; }
        }

        /// <summary>
        /// Gets or sets the neuron error.
        /// </summary>
        [DataMember()]
        public double Error
        {
            get { return m_error; }
            set
            {
                m_lastError = m_error;
                m_error = value;
            }
        }

        /// <summary>
        /// Gets the last error of this neuron.
        /// </summary>
        [XmlIgnore()]
        public double LastError
        {
            get { return m_lastError; }
        }

        

        /// <summary>
        /// Gets the neuron ID.
        /// </summary>
        [DataMember()]
        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        /// <summary>
        /// Gets the current neuron command .
        /// </summary>
        [XmlIgnore()]
        public int Command
        {
            get { return m_command; }
        }

        /// <summary>
        /// Gets or sets the NeuronType.
        /// </summary>
        [DataMember()]
        public NeuronTypes NeuronType
        {
            get
            {
                return m_neuronType;
            }
            set
            {
                if (value != m_neuronType)
                    m_neuronType = value;
            }
        }

        ///// <summary>
        ///// Gets or sets the DataType.
        ///// </summary>
        //public DataTypes DataType
        //{
        //    get { return m_dataType; }
        //    set
        //    {

        //        if (value == m_dataType)
        //            return;

        //        if (NeuronType != NeuronTypes.Input && value != DataTypes.Not_Applicable)
        //            throw new Exception("Invalid operation on a non-input neuron.");

        //        if (NeuronType == NeuronTypes.Input)
        //        {
        //            if (value != DataTypes.Not_Applicable)
        //                m_dataType = value;
        //            else
        //                throw new Exception("Invalid operation on a input neuron.");
        //        }
        //        else
        //            m_dataType = DataTypes.Not_Applicable;
        //    }
        //}

        #endregion

        #region Methods

        public new string ToString()
        {
            if (NeuronType == NeuronTypes.Memory)
                return string.Format("#{0}{1}\tV:{2}\tB:{3}\tE:{4}\tC:{5}",
                    this.NeuronType, this.ID, this.Value.ToString("#0.0000000000"), this.Error.ToString("#0.0000000000"), this.Bias.Weight.ToString("#0.0000000000"), this.Command);
            else
                return string.Format("#{0}{1}\tV:{2}\tB:{3}\tE:{4}",
                        this.NeuronType, this.ID, this.Value.ToString("#0.0000000000"), this.Error.ToString("#0.0000000000"), this.Bias.Weight.ToString("#0.0000000000"));
        }

        public void SetThreadPool(ManualResetEvent[] resetEvent)
        {
            m_resetEvent = resetEvent;
        }

        public void ThreadPulse()
        {
            Pulse(null);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(Pulse));
        }

        /// <summary>
        /// Pulse this neuron propagating its value. Increase InputReady of all output synapses. Pulses output neurons ready to pulse.
        /// Raises OnNeuronPulse event on Neuron.
        /// </summary>
        public void Pulse(object state)
        {
            NeuralNetwork.Log(1, "Start Neuron {0} Pulse.", this.ToString());



            lock (this)
            {

                InputReady = 0;

                if (NeuronType != NeuronTypes.Input)
                {
                    if (NeuronType == NeuronTypes.Memory && m_command == NeuralNetwork.CMD_RESET_MEMORY_AND_NO_PULSE)
                    {
                        m_command = NeuralNetwork.CMD_NORMAL_STATE_BUT_DONT_USE_VALUE;
                        NeuralNetwork.Log(2, "Dont pulse, reseted memory.");

                    }
                    else
                    {
                        m_command = NeuralNetwork.CMD_NOTHING;
                        m_value = 0;

                        NeuralNetwork.Log(2, "Getting inputs for {0}.", this.ID);

                        foreach (KeyValuePair<INeuron, NeuralValue> item in m_input)
                        {
                            if (item.Key.NeuronType == NeuronTypes.Memory)
                            {
                                if (item.Key.Command == NeuralNetwork.CMD_NORMAL_STATE_BUT_DONT_USE_VALUE
                                    || item.Key.Command == NeuralNetwork.CMD_RESET_MEMORY_AND_NO_PULSE)
                                {
                                    NeuralNetwork.Log(3, "Input {0}, reseted memory, dont use value.", this.ToString());
                                    continue;
                                }
                            }

                            NeuralNetwork.Log(3, "Input {0}.", this.ToString());
                            m_value += item.Key.Value * item.Value.Weight;
                        }

                        m_value += m_bias.Weight;

                        m_value = SigmoidUtils.Sigmoid(m_value);
                    }
                }

                //    WaitHandle.WaitAll(m_resetEvent);

                Neuron n;
                for (int i = 0; i < m_output.Count; i++)
                {
                    n = m_output[i];

                    if (n.NeuronType == NeuronTypes.Memory)
                        continue;

                    n.InputReady++;
                    if (n.InputReady == n.Input.Count)
                        if (i == m_output.Count - 1)
                            n.ThreadPulse();
                        else
                            n.ThreadPulse();
                }


            }

            NeuralNetwork.Log(1, "End Neuron {0} Pulse.", this.ToString());
        }

        //= new ManualResetEvent[10];

        /// <summary>
        /// Pulse this neuron backpropagating its error. Increase OutputReady of all input synapses. Pulses back input neurons ready to pulse back.
        /// Raises OnNeuronPulseBack event on Neuron.
        /// </summary>
        /// <param name="desiredResult"></param>
        public void PulseBack(double desiredResult)
        {
            NeuralNetwork.Log(1, "Start Neuron {0} PulseBack.", this.ToString());

            this.OutputReady = 0;

            if (this.NeuronType == NeuronTypes.Output)
            {
                this.Error = (desiredResult - this.Value) * SigmoidUtils.DerivativeSigmoid(this.Value);
            }
            else
            {
                this.Error = (desiredResult) * SigmoidUtils.DerivativeSigmoid(this.Value);
            }


            foreach (Neuron n in this.Input.Keys)
            {
                //if (n.NeuronType == NeuronTypes.Input || n.NeuronType == NeuronTypes.Memory)
                //    continue;

                n.OutputReady++;
                if (n.OutputReady == n.Output.Count)
                {
                    double error = 0;

                    foreach (Neuron nn in n.Output)
                        error += (nn.Error * nn.Input[n].Weight);

                    if (n.NeuronType != NeuronTypes.Input && n.NeuronType != NeuronTypes.Memory)
                        n.PulseBack(error);
                }

            }

            NeuralNetwork.Log(1, "End Neuron {0} PulseBack.", this.ToString());
        }

        /// <summary>
        /// Call ApplyLearningRate of neuron bias and of each input synapses.
        /// </summary>
        /// <param name="learningRate"></param>
        public void ApplyLearning(double learningRate)
        {
            NeuralNetwork.Log(1, "Start Neuron {0} ApplyLearning.", this.ToString());

            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ApplyLearning(learningRate);

            m_bias.ApplyLearning(learningRate);

            NeuralNetwork.Log(1, "End Neuron {0} ApplyLearning.", this.ToString());
        }

        /// <summary>
        /// Call CalculateDelta of neuron bias and all input synapses.
        /// </summary>
        public void CalculateDelta(double Momentum)
        {
            NeuralNetwork.Log(1, "Start Neuron {0} CalculateDelta.", this.ToString());

            foreach (Neuron n in this.Input.Keys)
            {
                this.Input[n].CalculateDelta(this.Error, Momentum);
            }

            //this.Bias.Delta += this.Error * this.Bias.Weight;
            this.Bias.CalculateDelta(this.Error, Momentum);

            NeuralNetwork.Log(1, "End Neuron {0} CalculateDelta.", this.ToString());

        }

        /// <summary>
        /// Call ResetLearning of neuron bias and of each input synapses.
        /// </summary>
        public void ResetLearning()
        {
            NeuralNetwork.Log(1, "Start Neuron {0} ResetLearning.", this.ToString());

            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ResetLearning();

            m_bias.ResetLearning();

            NeuralNetwork.Log(1, "End Neuron {0} ResetLearning.", this.ToString());
        }

        /// <summary>
        /// Call ResetKnowledgement of each input synapses.
        /// </summary>
        public void ResetKnowledgement()
        {
            NeuralNetwork.Log(1, "Start Neuron {0} ResetKnowledgement.", this.ToString());

            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ResetKnowledgement();

            m_bias.ResetKnowledgement();

            NeuralNetwork.Log(1, "End Neuron {0} ResetKnowledgement.", this.ToString());
        }

        public void ResetMemory()
        {
            NeuralNetwork.Log(1, "Neuron {0} ResetMemory.", this.ToString());
            m_command = NeuralNetwork.CMD_RESET_MEMORY_AND_NO_PULSE;
        }

        /// <summary>
        /// Gets a input NeuralValue.
        /// </summary>
        /// <param name="neuron">The neuron starting the synapse.</param>
        /// <returns></returns>
        public NeuralValue GetSynapseFrom(INeuron neuron)
        {
            if (Input.ContainsKey(neuron))
                return Input[neuron];
            else
                return null;
        }

        /// <summary>
        /// Gets a output NeuralValue.
        /// </summary>
        /// <param name="neuron">The neuron ending the synapse.</param>
        /// <returns></returns>
        public NeuralValue GetSynapseTo(INeuron neuron)
        {
            if (Output.Contains((Neuron)neuron))
                return ((Neuron)neuron).Input[this];
            else
                return null;
        }

        /// <summary>
        /// Gets all input neurons. It's dinamically calculated so if you need to uso in a foreach prefer:
        /// <code>
        /// INeuron[] arInputNeuron = GetInputNeurons();
        /// foreach(INeuron neuron in arInputNeuron)
        /// {
        ///     //...
        /// }
        /// </code>
        /// insteed of
        /// <code>
        /// foreach(INeuron neuron in GetInputNeurons())
        /// {
        ///     //...
        /// }
        /// </code>
        /// </summary>
        /// <returns></returns>
        public INeuron[] GetInputNeurons()
        {
            List<INeuron> tmp = new List<INeuron>(Input.Keys.Count);
            foreach (INeuron n in Input.Keys)
                tmp.Add(n);

            return tmp.ToArray();
        }

        /// <summary>
        /// Adds a new synapse between this and a neuron.
        /// </summary>
        /// <param name="neuron">Target neuron.</param>
        public void AddSynapse(INeuron neuron)
        {
            AddSynapse(neuron, neuron.NeuralNetwork.Random.GetDouble());
        }

        /// <summary>
        /// Adds a new input synapse between this and a neuron and sets its value.
        /// Adds a new output synapse between a neuron and this.
        /// </summary>
        /// <param name="neuron">Target neuron.</param>
        /// <param name="value">Synapse value.</param>
        public void AddSynapse(INeuron neuron, double value)
        {
            Input.Add(neuron, new NeuralValue(neuron, value));
            ((Neuron)neuron).Output.Add(this);
        }

        /// <summary>
        /// Removes a input synapse between this and a neuron.
        /// Removes a output synapse between a neuron and this.
        /// </summary>
        /// <param name="neuron">Target neuron.</param>
        public void RemoveSynapse(INeuron neuron)
        {
            Input.Remove(neuron);
            ((Neuron)neuron).Output.Remove(this);
        }

        /// <summary>
        /// Gets all output neurons. It's dinamically calculated so if you need to uso in a foreach prefer:
        /// <code>
        /// INeuron[] arOutputNeuron = GetOutputNeurons();
        /// foreach(INeuron neuron in arOutputNeuron)
        /// {
        ///     //...
        /// }
        /// </code>
        /// insteed of
        /// <code>
        /// foreach(INeuron neuron in GetOutputNeurons())
        /// {
        ///     //...
        /// }
        /// </code>
        /// </summary>
        /// <returns></returns>
        public INeuron[] GetOutputNeurons()
        {
            return Output.ToArray();
        }

        #endregion

        #region Events

        //  internal delegate void OnNeuronPulseDelegate(Neuron sender);
        //  internal event OnNeuronPulseDelegate OnNeuronPulse;

        //   internal delegate void OnNeuronPulseBackDelegate(Neuron sender);
        //   internal event OnNeuronPulseBackDelegate OnNeuronPulseBack;

        #endregion
    }

    public enum NeuronTypes
    {
        Input,
        Hidden,
        Output,
        Memory,
    }


}
