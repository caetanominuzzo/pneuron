using System;
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

        NeuralNetwork NeuralNetwork{ get;}

        int ID { get; }
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
        public Neuron(NeuralNetwork net) : this(net.Random.GetDouble(), net)
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

        public int ID
        {
            get { return m_id; }
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

        /// <summary>
        /// Pulse this neuron propagating its value. Increase InputReady of all output synapses. Pulses output neurons ready to pulse.
        /// Raises OnNeuronPulse event on Neuron.
        /// </summary>
        public void Pulse()
        {
            lock (this)
            {

                InputReady = 0;

                if (NeuronType != NeuronTypes.Input)
                {

                    m_value = 0;



                    foreach (KeyValuePair<INeuron, NeuralValue> item in m_input)
                    {
                        if (item.Key.NeuronType == NeuronTypes.Memory && item.Key.Value == double.PositiveInfinity)
                            continue;

                        m_value += item.Key.Value * item.Value.Weight;
                    }

                    m_value += m_bias.Weight;

                        m_value = Util.Sigmoid(m_value);

                }

                foreach (Neuron n in m_output)
                {
                    if (n.NeuronType == NeuronTypes.Memory)
                        continue;

                    n.InputReady++;
                    if (n.InputReady == n.Input.Count)
                        n.Pulse();
                }
            }
        }

        /// <summary>
        /// Pulse this neuron backpropagating its error. Increase OutputReady of all input synapses. Pulses back input neurons ready to pulse back.
        /// Raises OnNeuronPulseBack event on Neuron.
        /// </summary>
        /// <param name="desiredResult"></param>
        public void PulseBack(double desiredResult)
        {

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
        }

        /// <summary>
        /// Call ApplyLearningRate of neuron bias and of each input synapses.
        /// </summary>
        /// <param name="learningRate"></param>
        public void ApplyLearning(double learningRate)
        {
            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ApplyLearning(learningRate);

            m_bias.ApplyLearning(learningRate);
        }

        /// <summary>
        /// Call CalculateDelta of neuron bias and all input synapses.
        /// </summary>
        public void CalculateDelta()
        {

            foreach (Neuron n in this.Input.Keys)
            {
                this.Input[n].CalculateDelta(this.Error);
            }

            this.Bias.Delta += this.Error * this.Bias.Weight;

        }

        /// <summary>
        /// Call ResetLearning of neuron bias and of each input synapses.
        /// </summary>
        public void ResetLearning()
        {
            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ResetLearning();

            m_bias.ResetLearning();
        }

        /// <summary>
        /// Call ResetKnowledgement of each input synapses.
        /// </summary>
        public void ResetKnowledgement()
        {
            foreach (KeyValuePair<INeuron, NeuralValue> m in m_input)
                m.Value.ResetKnowledgement();

            m_bias.ResetKnowledgement();
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
        
        int INNER_TRAINING_TIMES = 100;

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

        private double m_lastCalculatedGlobalError;

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
        /// Gets the neural network global error.
        /// </summary>
        internal double GlobalError
        {
            get
            {
                double dGlobalTemp = 0;
                int iNoPerception = Neuron.Count - InputNeuronCount;

                foreach (Neuron n in Neuron)
                {
                    if (n.NeuronType != NeuronTypes.Input)
                    {
                        dGlobalTemp += Math.Abs(n.Error * n.Error);
                    }
                }

                m_lastCalculatedGlobalError = dGlobalTemp / (double)iNoPerception;

                return m_lastCalculatedGlobalError;
            }
        }

        public double LastCalculatedGlobalError
        {
            get { return m_lastCalculatedGlobalError; }
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

        #endregion

        #region Methods

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

            switch(neuron.NeuronType)
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
        }

        /// <summary>
        /// Pulses each output neuron.
        /// </summary>
        /// <param name="desiredResults"></param>
        public void PulseBack(double[] desiredResults)
        {
            if (desiredResults.Length != OutputNeuronCount)
                throw new Exception("The number of desiredResults must be equal to number of output neurons.");

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
        }

        /// <summary>
        /// Set zero on InputReady, OutputReady counter of each neuron. 
        /// </summary>
        public void ResetInputOutputReadyCount()
        {
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
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType != NeuronTypes.Input)
                        n.ApplyLearning(LearningRate);
                }
            }
        }

        /// <summary>
        /// Calls CalculateDelta of each neuron.
        /// </summary>
        public void CalculateDelta()
        {
            foreach (Neuron n in this.Neuron)
                n.CalculateDelta();
        }

        /// <summary>
        /// Calls ResetLearning of each non input neuron.
        /// Resets Memory too.
        /// </summary>
        public void ResetLearning()
        {
            if(OnResetLearning != null)
               OnResetLearning();

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

            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    n.ResetKnowledgement();
                }
            }
        }

        /// <summary>
        /// Sets initial value on Memories' Inputs and pulses it.
        /// </summary>
        public void ResetMemory()
        {
            lock (this)
            {
                List<INeuron> MemoriesInput = new List<INeuron>();

                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Memory)
                    {
                        INeuron[] inputs = n.GetInputNeurons();

                        foreach (INeuron nn in inputs)
                            if (!MemoriesInput.Contains(nn))
                                MemoriesInput.Add(nn);

                        foreach (INeuron nn in MemoriesInput)
                        {
                            nn.Value = Util.Sigmoid(1);
                        }

                    }
                }

                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Memory)
                    {
                        n.Pulse();
                    }
                }

                ResetInputOutputReadyCount();

            }
        }


        public void Train(double[][] input, double[][] output)
        {
            if (OnStartTraing != null)
                OnStartTraing();

            //for (int i = 0; i < input.Length; i++)
            //    for (int j = 0; j < input[i].Length; j++)
            //        input[i][j] = Util.Sigmoid(input[i][j]);

            //for (int i = 0; i < output.Length; i++)
            //    for (int j = 0; j < output[i].Length; j++)
            //        output[i][j] = Util.Sigmoid(output[i][j]);
          



            int count = 0;
            double dGlobalError = 1;


            while (true)
            {
                if (m_stopOnNextCycle)
                {
                    m_stopOnNextCycle = false;
                    if (OnRefreshCyclesSec != null)
                        OnRefreshCyclesSec(count * INNER_TRAINING_TIMES);

                    break;
                }

                count++;

                TrainSession(input, output, INNER_TRAINING_TIMES);

                if (count % INNER_TRAINING_TIMES == 0)
                    if (OnRefreshCyclesSec != null)
                        OnRefreshCyclesSec(count * INNER_TRAINING_TIMES);

                     dGlobalError = GlobalError;
                if (dGlobalError < 0.00000000000000000001)
                    m_stopOnNextCycle = true;
            }

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
            int i, j;

            double[][] sortedInputs = new double[inputs.Length][];
            double[][] sortedOutputs = new double[outputs.Length][];

            List<int> lOrder = new List<int>(inputs.Length);
            for (int k = 0; k < inputs.Length; k++)
                lOrder.Add(k);

            if (MemoryNeuronCount > 0)
                lOrder.Sort(new SortCompare());

                for (int k = 0; k < inputs.Length; k++)
                {
                    sortedInputs[k] = inputs[lOrder[k]];
                    sortedOutputs[k] = outputs[lOrder[k]];
                }


            lock (this)
            {

                for (i = 0; i < iterations; i++)
                {

                    ResetLearning();

                    for (j = 0; j < inputs.Length; j++)
                    {
                        SetInputData(sortedInputs[j]);
                        Pulse();
                        PulseBack(sortedOutputs[j]);
                        CalculateDelta();
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

            //TODO:Moves it to Train()
            if (input.Length != InputNeuronCount)
                throw new Exception("The number of input must be equal to number of perception neurons.");

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

        #endregion

        #region Events

        public delegate void OnPulseDelegate();
        public event OnPulseDelegate OnPulse;

        public delegate void OnPulseBackDelegate();
        public event OnPulseBackDelegate OnPulseBack;

        public delegate void OnRefreshCyclesSecDelegate(int Times);
        public event OnRefreshCyclesSecDelegate OnRefreshCyclesSec;

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
            return -Math.Log((1.0 / value + 1.0));
            
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