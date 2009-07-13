using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using primeira.pRandom;
using System.Linq;

namespace primeira.pNeuron.Core
{
    /// <summary>
    /// Represents a NeuralNetwork.
    /// </summary>
    [DataContract()]
    public class NeuralNetwork : INeuralNetwork, IDisposable
    {
        public void ToXml(string filepath)
        {
            Stream sm = File.Create(filepath);

            DataContractSerializer ser = new DataContractSerializer(typeof(NeuralNetwork),
                new Type[]{
                    typeof(NeuralNetwork),
                    typeof(Neuron),
                    typeof(NeuralValue)                
                }, 10000000, false, true, null);
            ser.WriteObject(sm, this);
            sm.Close();
        }

        public static NeuralNetwork ToObject(string filepath)
        {
            Stream sm = File.OpenRead(filepath);

            DataContractSerializer ser = new DataContractSerializer(typeof(NeuralNetwork),
                new Type[]{
                    typeof(NeuralNetwork),
                    typeof(Neuron),
                    typeof(NeuralValue)                
                }, 10000000, false, true, null);

            NeuralNetwork res = (NeuralNetwork)ser.ReadObject(sm);
            sm.Close();
            return res;
        }


        #region Constructors

        public NeuralNetwork()
        {
            dt = new System.Data.DataTable();
            dt.Columns.Add("si1", typeof(double));
            dt.Columns.Add("si2", typeof(double));
            dt.Columns.Add("si3", typeof(double));
            dt.Columns.Add("si4", typeof(double));
            dt.Columns.Add("si5", typeof(double));
            dt.Columns.Add("si6", typeof(double));
            dt.Columns.Add("i1", typeof(double));
            dt.Columns.Add("i2", typeof(double));
            dt.Columns.Add("o1", typeof(double));


            m_learningRate = DEFAULT_LEARNING_RATE;
            m_momentum = DEFAULT_MOMENTUM;
            m_innerTrainingTimes = DEFAULT_INNER_TRAINING_TIMES;
            m_neuron = new List<Neuron>();
        }

        #endregion

        #region Constants

        private int TRUE_RANDOM_GENERATOR_CACHE = 20;

        private double DEFAULT_LEARNING_RATE = 6;
        private double DEFAULT_MOMENTUM = .2;
        private int DEFAULT_INNER_TRAINING_TIMES = 10;
        

        public static int CMD_NOTHING = 0;
        public static int CMD_RESET_MEMORY_AND_NO_PULSE = 1;
        public static int CMD_NORMAL_STATE_BUT_DONT_USE_VALUE = 2;

        #endregion

        #region Fields

        private double m_learningRate;
        private double m_momentum;
        private int m_innerTrainingTimes;

        private List<Neuron> m_neuron;
        private pTrueRandomGenerator m_random;
        private int m_generatorID = 0;
        private bool m_stopOnNextCycle = false;
        private StringBuilder m_logger = new StringBuilder();

        private double m_lastCalculatedGlobalError;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the global LearningRate.
        /// </summary>
        [DataMember()]
        public double LearningRate
        {
            get { return m_learningRate; }
            set { m_learningRate = value; }

        }

        /// <summary>
        /// Gets the global Momentum.
        /// </summary>
        [DataMember()]
        public double Momentum
        {
            get { return m_momentum; }
            set { m_momentum = value; }
        }

        [DataMember()]
        public int InnerTrainingTimes
        {
            get { return m_innerTrainingTimes; }
            set { m_innerTrainingTimes = value; }
        }

        /// <summary>
        /// Gets all neurons.
        /// </summary>
        [DataMember()]
        public List<Neuron> Neuron
        {
            get { return m_neuron; }
            set { m_neuron = value; }
        }

        /// <summary>
        /// Gets the neural network global error.
        /// </summary>
        internal double GlobalError
        {
            get
            {
                double dGlobalTemp = 0d;

                foreach (Neuron n in Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Output)
                    {
                        dGlobalTemp += n.Error * n.Error;
                    }
                }

                try
                {
                    m_lastCalculatedGlobalError = Math.Min(Math.Max(dGlobalTemp / (double)OutputNeuronCount, double.MinValue), double.MaxValue);
                }
                catch
                {
                    m_lastCalculatedGlobalError = double.MinValue;
                }

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
        //[DataMember()]
        public int InputNeuronCount
        {
            get { return m_neuron == null ? 0 : m_neuron.Count(p => p.NeuronType == NeuronTypes.Input); }
        }

        /// <summary>
        /// Gets the total of neurons where NeuronType is Output.
        /// </summary>
        //[DataMember()]
        public int OutputNeuronCount
        {
            get { return m_neuron == null ? 0 : m_neuron.Count(p => p.NeuronType == NeuronTypes.Output); }
        }

        /// <summary>
        /// Gets the total of neurons where NeuronType is Output.
        /// </summary>
        //[DataMember()]
        public int MemoryNeuronCount
        {
            get { return m_neuron == null ? 0 : m_neuron.Count(p => p.NeuronType == NeuronTypes.Memory); }
        }

        /// <summary>
        /// Gets a pRandom true random generator object.
        /// </summary>
        public pTrueRandomGenerator Random
        {
            get { 
                if(m_random == null)
                    m_random = new pTrueRandomGenerator(TRUE_RANDOM_GENERATOR_CACHE);

                return m_random; }
        }

        /// <summary>
        /// Gets a new ID.
        /// </summary>
        public Guid GeneratorID
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        #endregion

        #region Methods

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
            return n;
        }

        /// <summary>
        /// Removes a specified neuron.
        /// </summary>
        /// <param name="neuron">A neuron to remove.</param>
        public void RemoveNeuron(Neuron neuron)
        {
            Neuron.Remove(neuron);

        }

        /// <summary>
        /// Pulses each input neuron.
        /// </summary>
        public void Pulse()
        {
            Log("Start Network Pulse.");

            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Input || n.NeuronType == NeuronTypes.Memory)
                        n.ThreadPulse();
                }


            }

            if (OnPulse != null)
                OnPulse();

            Log("End Network Pulse.");
        }

        /// <summary>
        /// Pulses each output neuron.
        /// </summary>
        /// <param name="desiredResults"></param>
        public void PulseBack(double[] desiredResults)
        {
            if (desiredResults.Length != OutputNeuronCount)
                throw new Exception("The number of desiredResults must be equal to number of output neurons.");

            Log("Start Network PulseBack.");

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

            Log("End Network PulseBack.");
        }

        /// <summary>
        /// Set zero on InputReady, OutputReady counter of each neuron. 
        /// </summary>
        public void ResetInputOutputReadyCount()
        {
            Log("ResetInputOutputReadyCount.");
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
            Log("Start Network ApplyLearning.");
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType != NeuronTypes.Input)
                        n.ApplyLearning(LearningRate);
                }
            }
            Log("End Network ApplyLearning.");
        }

        /// <summary>
        /// Calls CalculateDelta of each neuron.
        /// </summary>
        public void CalculateDelta()
        {
            Log("Network CalculateDelta.");
            foreach (Neuron n in this.Neuron)
                n.CalculateDelta(this.Momentum);
        }

        /// <summary>
        /// Calls ResetLearning of each non input neuron.
        /// Resets Memory too.
        /// </summary>
        public void ResetLearning()
        {
            if (OnResetLearning != null)
                OnResetLearning();

            Log("Network ResetLearning.");

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

            Log("Network ResetKnowledgement.");

            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    n.ResetKnowledgement();
                }
            }
        }

        /// <summary>
        /// Calls ResetMemory of each memory neuron.
        /// </summary>
        public void ResetMemory()
        {
            Log("Network ResetMemory.");
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
                    input[i][j] = SigmoidUtils.Sigmoid(input[i][j]);

            for (i = 0; i < output.Length; i++)
                for (j = 0; j < output[i].Length; j++)
                    output[i][j] = SigmoidUtils.Sigmoid(output[i][j]);

            int count = 0;
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
                    if (OnRefreshCyclesSec != null)
                        OnRefreshCyclesSec(count * m_innerTrainingTimes);

                    break;
                }

                count++;

                dGlobalError = TrainSession(sortedInputs, sortedOutputs, m_innerTrainingTimes);

                if (count % m_innerTrainingTimes == 0)
                    if (OnRefreshCyclesSec != null)
                        OnRefreshCyclesSec(count * m_innerTrainingTimes);

                if (dGlobalError < 0.0000000000001)
                    m_stopOnNextCycle = true;
            }

            if (OnStopTraing != null)
                OnStopTraing();

            if (OnMinimumReach != null)
                OnMinimumReach();

        }
        System.Data.DataTable dt;

        /// <summary>
        /// Trains the Neural Network.
        /// </summary>
        /// <param name="inputs">Array of input values.</param>
        /// <param name="outputs">Array of desired results.</param>
        /// <param name="iterations">Number of internal cycles.</param>
        public double TrainSession(double[][] inputs, double[][] outputs, int iterations)
        {

            



            lock (this)
            {

                double dErrorSum = 0d;

                for (int i = 0; i < iterations; i++)
                {

                    ResetLearning();

                    SetThreadPool();

                    for (int j = 0; j < inputs.Length; j++)
                    {
                        if (inputs[j][0] == 0) //Reset Memory Command
                        {
                            ResetMemory();
                        }
                        else
                        {
                            SetInputData(inputs[j]);
                            Pulse();
                            //dt.Rows.Add(new object[]
                            //    {
                            //        this.Neuron[0].GetSynapseTo(this.Neuron[2]).Weight,
                            //        this.Neuron[0].GetSynapseTo(this.Neuron[3]).Weight,
                            //        this.Neuron[1].GetSynapseTo(this.Neuron[2]).Weight,
                            //        this.Neuron[1].GetSynapseTo(this.Neuron[3]).Weight,
                            //        this.Neuron[2].GetSynapseTo(this.Neuron[4]).Weight,
                            //        this.Neuron[3].GetSynapseTo(this.Neuron[4]).Weight,
                            //        this.Neuron[0].Value,
                            //        this.Neuron[1].Value,
                            //        this.Neuron[4].Value
                            //    });
                            PulseBack(outputs[j]);
                            CalculateDelta();
                        }
                    }

                    //if (dt.Rows.Count > 100)
                    //    dErrorSum = 0;
                    //else
                        dErrorSum += GlobalError;

                    ApplyLearning();
                }

                return dErrorSum;

            }

        }

        private ManualResetEvent[] m_resetEvent;

        public void SetThreadPool()
        {
            int iMaxSynapse = 0;
            foreach (Neuron n in Neuron)
            {
                if (iMaxSynapse < n.GetInputNeurons().Length)
                    iMaxSynapse = n.GetInputNeurons().Length;

                if (iMaxSynapse < n.GetOutputNeurons().Length)
                    iMaxSynapse = n.GetOutputNeurons().Length;


            }

            m_resetEvent = new ManualResetEvent[iMaxSynapse];

            for (int i = 0; i < iMaxSynapse; i++)
                m_resetEvent[i] = new ManualResetEvent(false);

            foreach (Neuron n in Neuron)
                n.SetThreadPool(m_resetEvent);
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

        public void Log(string msg)
        {
            Log(0, msg);
        }

        public void Log(int Indent, string msg)
        {
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
        }

        public void Log(string format, object args)
        {
            Log(0, string.Format(format, args));
        }

        public void Log(int Indent, string format, object args)
        {
            Log(Indent, string.Format(format, args));
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

        public delegate void OnMinimumReachDelegate();
        public event OnMinimumReachDelegate OnMinimumReach;

        public delegate void OnResetLearningDelegate();
        public event OnResetLearningDelegate OnResetLearning;

        public delegate void OnResetKnowledgementDelegate();
        public event OnResetKnowledgementDelegate OnResetKnowledgement;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.m_neuron.Clear();
            this.m_neuron = null;
            //this.m_random.Dispose();
        }

        #endregion
    }
}
