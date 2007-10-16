#define NNDEBUGNEURON

using System;
using System.Text;
using System.Collections.Generic;
using primeira.pRandom;


namespace primeira.pNeuron.Core
{
    public delegate void Assinc();
    public delegate void AssincP(object o);

    #region Interfaces INeuron, INeuralNetwork

    public interface INeuron
    {
        void Pulse();
        void ApplyLearning(double learningRate);
        void ResetLearning();
        void PulseBack(double desiredResult);

        NeuralValue Bias { get; }

        double Error { get; set; }
        double LastError { get; }

        NeuronTypes NeuronType { get; set;}

        double Value { get;  set; }

        NeuralNetwork NeuralNetwork { get;}

        int ID { get; }

        int Command { get; }
    }

    public interface INeuralNetwork
    {

        void Pulse();
        void ApplyLearning();
        void ResetLearning();
        void PulseBack(double[] desiredResults);

        double LearningRate { get; }

        List<Neuron> Neuron { get; }
    }

    public interface ILogger
    {
        void Log(String msg);
        void Flush();
    }


    #endregion

    #region Classes NeuralValue, Neuron, NeuralNetwork, Util

    /// <summary>
    /// Internal value/delta of a synapse or bias.
    /// </summary>
    public class NeuralValue
    {

        #region Constructors

        /// <summary>
        /// Internal value/delta of a synapse or bias.
        /// </summary>
        /// <param name="neuron">Neuron parent.</param>
        /// <param name="value">Initial value.</param>
        public NeuralValue(INeuron neuron, double value)
        {
            m_neuron = (Neuron)neuron;
            m_weight = value;
            m_delta = 0;
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
        public double Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        /// <summary>
        /// Gets the delta of the synapse or bias.
        /// </summary>
        public double Delta
        {
            get { return m_delta; }
            set { m_delta = value; } //TODO : Remove
        }

        /// <summary>
        /// Gets the neuron parent.
        /// </summary>
        public Neuron Neuron
        {
            get { return m_neuron; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// After PulseBack calculates the error CalculateDelta appends it on this synapse or bias delta.
        /// </summary>
        /// <param name="Error"></param>
        public void CalculateDelta(double Error)
        {
            Delta += Neuron.Value * Error;
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

    /// <summary>
    /// Represents a neuron.
    /// </summary>
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
        public Neuron(double biasValue, NeuralNetwork net)
        {
            m_bias = new NeuralValue(this, biasValue);
            m_error = 0;
            m_input = new Dictionary<INeuron, NeuralValue>();
            m_output = new List<Neuron>();
            m_neuralNetwork = net;
            m_id = net.GeneratorID;
            net.Neuron.Add(this);
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

        private DataTypes m_dataType = DataTypes.Not_Applicable;

        private int m_id = 0;

        private int m_command = NeuralNetwork.CMD_NOTHING;



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the neuron value .
        /// </summary>
        public double Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// Gets a dictionary with all input synapses of this neuron.
        /// </summary>
        private Dictionary<INeuron, NeuralValue> Input
        {
            get { return m_input; }
        }

        /// <summary>
        /// Gets a dictionary with all output synapses of this neuron.
        /// </summary>
        private List<Neuron> Output
        {
            get { return m_output; }
        }

        /// <summary>
        /// How many inputs of this neuron are already calculated and are able to be read.
        /// When InputReady are equal to item in Input thus all Input are ready and the neuron can pulses.
        /// </summary>
        public int InputReady
        {
            get { return m_inputReady; }
            internal set { m_inputReady = value; }
        }

        /// <summary>
        /// How many outputs of this neuron are already calculated and are able to be read.
        /// When OututReady are equal to item in Output thus all Output are ready and the neuron can pulses back.
        /// </summary>
        public int OutputReady
        {
            get { return m_outputReady; }
            internal set { m_outputReady = value; }
        }

        /// <summary>
        /// Gets the neuron bias.
        /// </summary>
        public NeuralValue Bias
        {
            get { return m_bias; }
        }

        /// <summary>
        /// Gets or sets the neuron error.
        /// </summary>
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
        public double LastError
        {
            get { return m_lastError; }
        }

        /// <summary>
        /// Gets the Parent Neural Network.
        /// </summary>
        public NeuralNetwork NeuralNetwork
        {
            get { return m_neuralNetwork; }
        }

        /// <summary>
        /// Gets the neuron ID.
        /// </summary>
        public int ID
        {
            get { return m_id; }
        }

        /// <summary>
        /// Gets the current neuron command .
        /// </summary>
        public int Command
        {
            get { return m_command; }
        }

        /// <summary>
        /// Gets or sets the NeuronType.
        /// </summary>
        public NeuronTypes NeuronType
        {
            get
            {
                return m_neuronType;
            }
            set
            {

                if (value != m_neuronType)
                {
                    if (value == NeuronTypes.Input)
                        NeuralNetwork.InputNeuronCount++;
                    if (value == NeuronTypes.Output)
                        NeuralNetwork.OutputNeuronCount++;
                    if (value == NeuronTypes.Memory)
                        NeuralNetwork.MemoryNeuronCount++;

                    if (m_neuronType == NeuronTypes.Input)
                        NeuralNetwork.InputNeuronCount--;
                    if (m_neuronType == NeuronTypes.Output)
                        NeuralNetwork.OutputNeuronCount--;
                    if (m_neuronType == NeuronTypes.Memory)
                        NeuralNetwork.MemoryNeuronCount--;

                }
                if (value == NeuronTypes.Input && m_neuronType != NeuronTypes.Input)
                {
                    m_neuronType = value;
                    DataType = DataTypes.Integer;
                }

                if (value != NeuronTypes.Input)
                {
                    //To avoid validation
                    m_dataType = DataTypes.Not_Applicable;
                    m_neuronType = value;
                }

            }
        }

        /// <summary>
        /// Gets or sets the DataType.
        /// </summary>
        public DataTypes DataType
        {
            get { return m_dataType; }
            set
            {

                if (value == m_dataType)
                    return;

                if (NeuronType != NeuronTypes.Input && value != DataTypes.Not_Applicable)
                    throw new Exception("Invalid operation on a non-input neuron.");

                if (NeuronType == NeuronTypes.Input)
                {
                    if (value != DataTypes.Not_Applicable)
                        m_dataType = value;
                    else
                        throw new Exception("Invalid operation on a input neuron.");
                }
                else
                    m_dataType = DataTypes.Not_Applicable;
            }
        }

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

        /// <summary>
        /// Pulse this neuron propagating its value. Increase InputReady of all output synapses. Pulses output neurons ready to pulse.
        /// Raises OnNeuronPulse event on Neuron.
        /// </summary>
        public void Pulse()
        {
#if NNDEBUGNEURON || NNDEBUG
            NeuralNetwork.Log(this.GetHashCode());
            //NeuralNetwork.Log(1, "Start Neuron {0} Pulse.", this.ToString());
#endif

            lock (this)
            {

                InputReady = 0;

                if (NeuronType != NeuronTypes.Input)
                {
                    if (NeuronType == NeuronTypes.Memory && m_command == NeuralNetwork.CMD_RESET_MEMORY_AND_NO_PULSE)
                    {
                        m_command = NeuralNetwork.CMD_NORMAL_STATE_BUT_DONT_USE_VALUE;
#if NNDEBUG
                        NeuralNetwork.Log(2, "Dont pulse, reseted memory.");
#endif

                    }
                    else
                    {
                        m_command = NeuralNetwork.CMD_NOTHING;

                        m_value = 0;
#if NNDEBUG
                        NeuralNetwork.Log(2, "Getting inputs for {0}.", this.ID);
#endif
                        foreach (KeyValuePair<INeuron, NeuralValue> item in m_input)
                        {
                            if (item.Key.NeuronType == NeuronTypes.Memory)
                            {
                                if (item.Key.Command == NeuralNetwork.CMD_NORMAL_STATE_BUT_DONT_USE_VALUE
                                    || item.Key.Command == NeuralNetwork.CMD_RESET_MEMORY_AND_NO_PULSE)
                                {
#if NNDEBUG
                                    NeuralNetwork.Log(3, "Input {0}, reseted memory, dont use value.", this.ToString());
#endif
                                    continue;
                                }
                            }
#if NNDEBUG
                            NeuralNetwork.Log(3, "Input {0}.", this.ToString());
#endif
                            m_value += item.Key.Value * item.Value.Weight;
                        }

                        m_value += m_bias.Weight;

                        m_value = Util.Sigmoid(m_value);
                    }
                }

                Neuron n;
                for (int i = 0; i < m_output.Count; i++)
                {
                    n = m_output[i];

                    if (n.NeuronType == NeuronTypes.Memory)
                        continue;

                    n.InputReady++;
                    if (n.InputReady == n.Input.Count)
                        n.Pulse();
                }
            }
#if NNDEBUG
            NeuralNetwork.Log(1, "End Neuron {0} Pulse.", this.ToString());
#endif
        }

        /// <summary>
        /// Pulse this neuron backpropagating its error. Increase OutputReady of all input synapses. Pulses back input neurons ready to pulse back.
        /// Raises OnNeuronPulseBack event on Neuron.
        /// </summary>
        /// <param name="desiredResult"></param>
        public void PulseBack(double desiredResult)
        {
#if NNDEBUGNEURON || NNDEBUG
            NeuralNetwork.Log(this.GetHashCode());
            //NeuralNetwork.Log(1, "Start Neuron {0} PulseBack.", this.ToString());
#endif
            this.OutputReady = 0;

            if (this.NeuronType == NeuronTypes.Output)
            {
                this.Error = (desiredResult - this.Value) * Util.DerivativeSigmoid(this.Value);
            }
            else
            {
                this.Error = (desiredResult) * Util.DerivativeSigmoid(this.Value);
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
#if NNDEBUG
            NeuralNetwork.Log(1, "End Neuron {0} PulseBack.", this.ToString());
#endif
        }

        /// <summary>
        /// Call ApplyLearningRate of neuron bias and of each input synapses.
        /// </summary>
        /// <param name="learningRate"></param>
        public void ApplyLearning(double learningRate)
        {
#if NNDEBUG
            NeuralNetwork.Log(1, "Start Neuron {0} ApplyLearning.", this.ToString());
#endif

            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ApplyLearning(learningRate);

           //m_bias.ApplyLearning(learningRate);

            NeuralNetwork.Log(1, "End Neuron {0} ApplyLearning.", this.ToString());
        }

        /// <summary>
        /// Call CalculateDelta of neuron bias and all input synapses.
        /// </summary>
        public void CalculateDelta()
        {
#if NNDEBUG
            NeuralNetwork.Log(1, "Start Neuron {0} CalculateDelta.", this.ToString());
#endif
            foreach (Neuron n in this.Input.Keys)
            {
                this.Input[n].CalculateDelta(this.Error);
            }

          //  this.Bias.Delta += this.Error * this.Bias.Weight;
#if NNDEBUG
            NeuralNetwork.Log(1, "End Neuron {0} CalculateDelta.", this.ToString());
#endif
        }

        /// <summary>
        /// Call ResetLearning of neuron bias and of each input synapses.
        /// </summary>
        public void ResetLearning()
        {
#if NNDEBUG
            NeuralNetwork.Log(1, "Start Neuron {0} ResetLearning.", this.ToString());
#endif
            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ResetLearning();

            m_bias.ResetLearning();
#if NNDEBUG
            NeuralNetwork.Log(1, "End Neuron {0} ResetLearning.", this.ToString());
#endif
        }

        /// <summary>
        /// Call ResetKnowledgement of each input synapses.
        /// </summary>
        public void ResetKnowledgement()
        {
#if NNDEBUG
            NeuralNetwork.Log(1, "Start Neuron {0} ResetKnowledgement.", this.ToString());
#endif
            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ResetKnowledgement();

            m_bias.ResetKnowledgement();

            NeuralNetwork.Log(1, "End Neuron {0} ResetKnowledgement.", this.ToString());
        }

        public void ResetMemory()
        {
#if NNDEBUG
            NeuralNetwork.Log(1, "Neuron {0} ResetMemory.", this.ToString());
#endif
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

    /// <summary>
    /// Represents a NeuralNetwork.
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        #region Constructors

        public NeuralNetwork()
        {
            m_learningRate = DEFAULT_LEARNING_RATE;
            m_neuron = new List<Neuron>();
        }

        #endregion

        #region Constants

        double DEFAULT_LEARNING_RATE = 5;

        int INNER_TRAINING_TIMES = 200;

        public static int CMD_NOTHING = 0;
        public static int CMD_RESET_MEMORY_AND_NO_PULSE = 1;
        public static int CMD_NORMAL_STATE_BUT_DONT_USE_VALUE = 2;

        #endregion

        #region Fields

        private double m_learningRate;
        private List<Neuron> m_neuron;
        private int m_inputNeuronCount = 0;
        private int m_outputNeuronCount = 0;
        private int m_memoryNeuronCount = 0;
        private pTrueRandomGenerator m_random;
        private int m_generatorID = 0;
        private bool m_stopOnNextCycle = false;
        private StringBuilder m_logger = new StringBuilder();
        private int m_cycles = 0;

        private double m_lastCalculatedGlobalError;

        private List<double> m_logGlobalError = new List<double>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the global LearningRate.
        /// </summary>
        public double LearningRate
        {
            get { return m_learningRate; }
            set { m_learningRate = value; }

        }

        /// <summary>
        /// Gets all neurons.
        /// </summary>
        public List<Neuron> Neuron
        {
            get { return m_neuron; }
        }

        /// <summary>
        /// Gets the last calculated neural network global error.
        /// </summary>
        public double LastCalculatedGlobalError
        {
            get { return m_logGlobalError.Count > 0 ? m_logGlobalError[m_logGlobalError.Count - 1] : 0; }
        }

        /// <summary>
        /// Gets the total of neurons where NeuronType is Input.
        /// </summary>
        public int InputNeuronCount
        {
            get { return m_inputNeuronCount; }
            internal set { m_inputNeuronCount = value; }

        }

        /// <summary>
        /// Gets the total of neurons where NeuronType is Output.
        /// </summary>
        public int OutputNeuronCount
        {
            get { return m_outputNeuronCount; }
            internal set { m_outputNeuronCount = value; }
        }

        /// <summary>
        /// Gets the total of neurons where NeuronType is Output.
        /// </summary>
        public int MemoryNeuronCount
        {
            get { return m_memoryNeuronCount; }
            internal set { m_memoryNeuronCount = value; }
        }

        /// <summary>
        /// Gets a pRandom true random generator object.
        /// </summary>
        public pTrueRandomGenerator Random
        {
            get { return m_random; }
        }

        /// <summary>
        /// Gets a new ID.
        /// </summary>
        public int GeneratorID
        {
            get
            {
                m_generatorID++;
                return m_generatorID;
            }
        }

        public int Cycles
        {
            get
            {
                return m_cycles * INNER_TRAINING_TIMES;
            }
        }

        #endregion

        #region Methods

        public void AdjustGeneratorID(string Id)
        {
            try
            {
                int i = int.Parse(Id);
                if (i >= m_generatorID)
                    m_generatorID = i++;
            }
            catch
            {
            }
        }

        private double CalculateGlobalError()
        {
            double dGlobalTemp = 0;
            int iNoPerception = Neuron.Count - InputNeuronCount;

            foreach (Neuron n in Neuron)
            {
                if (n.NeuronType != NeuronTypes.Input)
                {
                    dGlobalTemp += Math.Abs(n.Error);
                }
            }

            double v = dGlobalTemp / (double)iNoPerception;

            m_logGlobalError.Add(v);

            return v;
        }

        /// <summary>
        /// Gets a range of Global Errors
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public double[] GetGlobalErrors(int Index, int Count)
        {
            return m_logGlobalError.GetRange(Index, Count).ToArray();
        }

        /// <summary>
        /// Sets the internal random generator.
        /// </summary>
        /// <param name="random"></param>
        public void SetRandomGenerator(pTrueRandomGenerator random)
        {
            m_random = random;
        }

        /// <summary>
        /// Tells to stop trainig on next train cycle.
        /// </summary>
        public void StopOnNextCycle()
        {
            m_stopOnNextCycle = true;
        }

        /// <summary>
        /// Adds a new neuron and returns it.
        /// </summary>
        /// <returns></returns>
        public Neuron AddNeuron()
        {
            Neuron n = new Neuron(this);
            AddNeuron(n);
            return n;
        }

        /// <summary>
        /// Adds a specified neuron.
        /// </summary>
        /// <param name="neuron">A neuron to add.</param>
        private void AddNeuron(Neuron neuron)
        {

            switch (neuron.NeuronType)
            {
                case NeuronTypes.Input: m_inputNeuronCount++;
                    break;
                case NeuronTypes.Output: m_outputNeuronCount++;
                    break;
                case NeuronTypes.Memory: m_memoryNeuronCount++;
                    break;
            }
        }

        /// <summary>
        /// Removes a specified neuron.
        /// </summary>
        /// <param name="neuron">A neuron to remove.</param>
        public void RemoveNeuron(Neuron neuron)
        {
            Neuron.Remove(neuron);

            switch (neuron.NeuronType)
            {
                case NeuronTypes.Input: m_inputNeuronCount--;
                    break;
                case NeuronTypes.Output: m_outputNeuronCount--;
                    break;
                case NeuronTypes.Memory: m_memoryNeuronCount--;
                    break;
            }

        }

        /// <summary>
        /// Pulses each input neuron.
        /// </summary>
        public void Pulse()
        {
#if NNDEBUG
            Log("Start Network Pulse.");
#endif

            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Input || n.NeuronType == NeuronTypes.Memory)
                        n.Pulse();
                }


            }

            if (OnPulse != null)
                OnPulse();

#if NNDEBUG
            Log("End Network Pulse.");
#endif
        }

        /// <summary>
        /// Pulses each output neuron.
        /// </summary>
        /// <param name="desiredResults"></param>
        public void PulseBack(double[] desiredResults)
        {
#if NNDEBUG
            Log("Start Network PulseBack.");
#endif

            int ni = 0;
            foreach (Neuron n in this.Neuron)
            {
                if (n.NeuronType == NeuronTypes.Output)
                {
                    n.PulseBack(desiredResults[ni]);
                    ni++;
                }
            }

            if (OnPulseBack != null)
                OnPulseBack();

#if NNDEBUG
            Log("End Network PulseBack.");
#endif
        }

        /// <summary>
        /// Set zero on InputReady, OutputReady counter of each neuron. 
        /// </summary>
        public void ResetInputOutputReadyCount()
        {
#if NNDEBUG
            Log("ResetInputOutputReadyCount.");
#endif
            foreach (Neuron n in Neuron)
            {
                n.InputReady = 0;
                n.OutputReady = 0;
            }
        }

        /// <summary>
        /// Calls ApplyLearning of each neuron.
        /// </summary>
        public void ApplyLearning()
        {
#if NNDEBUG
            Log("Start Network ApplyLearning.");
#endif
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType != NeuronTypes.Input)
                        n.ApplyLearning(LearningRate);
                }
            }
#if NNDEBUG
            Log("End Network ApplyLearning.");
#endif
        }

        /// <summary>
        /// Calls CalculateDelta of each neuron.
        /// </summary>
        public void CalculateDelta()
        {
#if NNDEBUG
            Log("Network CalculateDelta.");
#endif
            foreach (Neuron n in this.Neuron)
                n.CalculateDelta();
        }

        /// <summary>
        /// Calls ResetLearning of each non input neuron.
        /// Resets Memory too.
        /// </summary>
        public void ResetLearning()
        {
            if (OnResetLearning != null)
                OnResetLearning();

#if NNDEBUG
            Log("Network ResetLearning.");
#endif

            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType != NeuronTypes.Input)
                        n.ResetLearning();
                }
            }

            ResetMemory();
        }

        /// <summary>
        /// Calls ResetKnowledgement of each neuron.
        /// </summary>
        public void ResetKnowledgement()
        {
            if (OnResetKnowledgement != null)
                OnResetKnowledgement();

#if NNDEBUG
            Log("Network ResetKnowledgement.");
#endif

            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    n.ResetKnowledgement();
                }
            }

            m_logGlobalError.Clear();
        }

        /// <summary>
        /// Calls ResetMemory of each memory neuron.
        /// </summary>
        public void ResetMemory()
        {
#if NNDEBUG
            Log("Network ResetMemory.");
#endif
            lock (this)
            {

                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Memory)
                        n.ResetMemory();
                }

            }
        }


        public void Train(double[][] input, double[][] output)
        {
            if (OnStartTraing != null)
                OnStartTraing();
            int i, j;

            for (i = 0; i < input.Length; i++)
                for (j = 0; j < input[i].Length; j++)
                    input[i][j] = Util.Sigmoid(input[i][j]);

            for (i = 0; i < output.Length; i++)
                for (j = 0; j < output[i].Length; j++)
                    output[i][j] = Util.Sigmoid(output[i][j]);




            double dGlobalError = 1;



            double[][] sortedInputs = new double[input.Length][];
            double[][] sortedOutputs = new double[output.Length][];

            List<int> lOrder = new List<int>(input.Length);
            for (int k = 0; k < input.Length; k++)
                lOrder.Add(k);

            if (MemoryNeuronCount > 0)
                lOrder.Sort(new SortCompare());

            for (int k = 0; k < input.Length; k++)
            {
                sortedInputs[k] = input[lOrder[k]];
                sortedOutputs[k] = output[lOrder[k]];
            }

            while (true)
            {
                if (m_stopOnNextCycle)
                {
                    m_stopOnNextCycle = false;
                    break;
                }

                m_cycles++;

                TrainSession(sortedInputs, sortedOutputs, INNER_TRAINING_TIMES);

                dGlobalError = CalculateGlobalError();

                if (dGlobalError < 0.00000000000000000001)
                    m_stopOnNextCycle = true;
            }

            m_cycles = 0;

            if (OnStopTraing != null)
                OnStopTraing();
        }

        /// <summary>
        /// Trains the Neural Network.
        /// </summary>
        /// <param name="inputs">Array of input values.</param>
        /// <param name="outputs">Array of desired results.</param>
        /// <param name="iterations">Number of internal cycles.</param>
        public void TrainSession(double[][] inputs, double[][] outputs, int iterations)
        {



            lock (this)
            {

                for (int i = 0; i < iterations; i++)
                {

                    ResetLearning();

                    for (int j = 0; j < inputs.Length; j++)
                    {
                        //Reset Memory Command
                        if (inputs[j][0] == 1) 
                        {
                            ResetMemory();
                        }
                        else
                        {
                            SetInputData(inputs[j]);
                            Pulse();
                            PulseBack(outputs[j]);
                            CalculateDelta();
                        }
                    }

                    ApplyLearning();
                }

            }

        }

        /// <summary>
        /// Set specified input data on input neurons.
        /// </summary>
        /// <param name="input">Input data.</param>
        public void SetInputData(double[] input)
        {
            int i = 0;
            foreach (Neuron n in this.Neuron)
            {
                if (n.NeuronType == NeuronTypes.Input)
                {
                    n.Value = input[i];
                    i++;
                }
            }
        }

        /// <summary>
        /// Used internaly to randomize input data order.
        /// </summary>
        private class SortCompare : IComparer<int>
        {
            static Random m_random = new Random(DateTime.Now.Second);
            public int Compare(int x, int y)
            {
                return m_random.Next(1);
            }
        }

        private List<int> logcore = new List<int>();

        public void Log(string msg)
        {
            Log(0, msg);
        }

        public void Log(int Indent, string msg)
        {
#if NNDEBUG
            return;
            for (int i = 0; i < Indent; i++)
                m_logger.Append("\t");

            m_logger.Append(msg);

            m_logger.Append(Environment.NewLine);

            if (m_logger.Length > 10000)
            {
                System.IO.File.AppendAllText("c:\\logger.txt", m_logger.ToString());
                m_logger.Length = 0;
            }
#endif
        }

        public void Log(string format, object args)
        {
            Log(0, string.Format(format, args));
        }

        public void Log(int Indent, string format, object args)
        {
            Log(Indent, string.Format(format, args));
        }

        public void Log(int Handle)
        {
            logcore.Add(Handle);
            if (logcore.Count > 10000)
            {
                logcore.Clear();
            }
        }


        #endregion

        #region Events

        public delegate void OnPulseDelegate();
        public event OnPulseDelegate OnPulse;

        public delegate void OnPulseBackDelegate();
        public event OnPulseBackDelegate OnPulseBack;

        public delegate void OnStartTraingDelegate();
        public event OnStartTraingDelegate OnStartTraing;

        public delegate void OnStopTraingDelegate();
        public event OnStopTraingDelegate OnStopTraing;

        public delegate void OnResetLearningDelegate();
        public event OnResetLearningDelegate OnResetLearning;

        public delegate void OnResetKnowledgementDelegate();
        public event OnResetKnowledgementDelegate OnResetKnowledgement;

        #endregion

    }

    /// <summary>
    /// Math utility methods.
    /// </summary>
    public static class Util
    {
        public static double DerivativeSigmoid(double value)
        {
            return value * (1.0F - value);
        }

        public static double Sigmoid(double value)
        {
            return (1 / (1 + Math.Exp(-value)));
        }

        public static double UnSigmoid(double value)
        {
            return -Math.Log((1.0 / value - 1.0));

        }

        /// <summary>
        /// Low precision, high performance magic.
        /// See http://martin.ankerl.com/2007/10/04/optimized-pow-approximation-for-java-and-c-c/
        /// and http://citeseer.ist.psu.edu/lazzaro99jpeg.html
        /// and http://citeseer.ist.psu.edu/schraudolph98fast.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Pow(double a, double b)
        {
            int x = (int)(BitConverter.DoubleToInt64Bits(a) >> 32);
            int y = (int)(b * (x - 1072632447) + 1072632447);
            return BitConverter.Int64BitsToDouble(((long)y) << 32);
        }

        /// <summary>
        /// More magic.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double Exp(double val)
        {
            long tmp = (long)(1512775 * val + (1072693248 - 60801));
            return BitConverter.Int64BitsToDouble(tmp << 32);
        }

    }

    #endregion

    #region Enums NeuronTypes, DataTypes

    public enum NeuronTypes
    {

        Input,
        Hidden,
        Output,
        Memory,
    }

    public enum DataTypes
    {
        Not_Applicable,
        Integer,
        Continuos,
        List
    }

    #endregion
}
