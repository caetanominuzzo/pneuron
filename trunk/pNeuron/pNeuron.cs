using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace primeira.pNeuron.Core
{


    public class Sinapse
    {
        private NeuralFactor _neuralFactor;

        public NeuralFactor NeuralFactor
        {
            get { return _neuralFactor; }
            set { _neuralFactor = value; }
        }
        private Neuron _neuron;

        public Neuron Neuron
        {
            get { return _neuron; }
            set { _neuron = value; }
        }

        public Sinapse(NeuralFactor aNeuralFactor, Neuron aNeuron)
        {
            NeuralFactor = aNeuralFactor;
            Neuron = aNeuron;
        }
    }


    public class NeuralFactor
    {
        #region Constructors

        public NeuralFactor(double weight)
        {
            m_weight = weight;
            m_lastDelta = m_delta = 0;
            
        }

        #endregion

        #region Member Variables

        private double m_weight, m_lastDelta, m_delta;

        #endregion

        #region Properties

        public double Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        public double H_Vector
        {
            get { return m_delta; }
            set { m_delta = value; }
        }

        public double Last_H_Vector
        {
            get { return m_lastDelta; }
            //set { m_lastDelta = value; }
        }

        #endregion

        #region Methods

        public void ApplyWeightChange(double learningRate)
        {
            m_lastDelta = m_delta;
            m_weight += m_delta * learningRate;
        }

        public void ResetWeightChange()
        {
            m_lastDelta = m_delta = 0;
        }

        public void ResetValue()
        {
            this.Weight = new Random(1).NextDouble();
        }

        #endregion
    }



    public interface INeuronSignal
    {
        double Value { get; set; }
    }

    public interface INeuron : INeuronSignal, INeuronReceptor
    {
    
        void Pulse();
        void ApplyLearning(double learningRate);
        void InitializeLearning();

        void CalculateErrors(double desiredResult);

        NeuralFactor Bias { get; set; }

        double Error { get; set; }
        double LastError { get; set; }

        NeuronTypes NeuronType
        {
            get;
            set;
        }
    }

    #region
    /*
    public interface INeuralLayer : IList<INeuron>
    {
        LayerType LayerType
        {
            get;
        }
    
        void Pulse(INeuralNet net);
        void ApplyLearning(INeuralNet net);
        void InitializeLearning(INeuralNet net);
    }
     * */
    #endregion

    public interface INeuralNet
    {

        double LearningRate { get; set; }

        List<Neuron> Neuron
        {
            get;
            set;
        }

        void Pulse();
        void ApplyLearning();
        void InitializeLearning();
    }

    public class Neuron : INeuron
    {
        #region Constructors

        public Neuron() : this(new Random().NextDouble())
        {
         
        }

        public Neuron(double Weight)
        {
            m_bias = new NeuralFactor(Weight);
            m_error = 0;
            m_input = new Dictionary<INeuron, NeuralFactor>();
            m_output = new List<Neuron>();
        }

        #endregion

        #region Member Variables

        private Dictionary<INeuron, NeuralFactor> m_input;

        private int m_inputReady = 0;

        double m_value, m_error, m_lastError;
        NeuralFactor m_bias;

        #endregion

        #region INeuronSignal Members

        public double Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        #endregion

        #region INeuronReceptor Members

        public Dictionary<INeuron, NeuralFactor> Input
        {
            get { return m_input; }
        }

        #endregion

        #region INeuron Members

        public void Pulse()
        {
            lock (this)
            {
                this.InputReady = 0;

                if (NeuronType != NeuronTypes.Input)
                {

                    m_value = 0;

                    foreach (KeyValuePair<INeuron, NeuralFactor> item in m_input)
                        m_value += item.Key.Value * item.Value.Weight;

                    m_value += m_bias.Weight;

                    m_value = Sigmoid(m_value);
                }

                foreach (Neuron n in m_output)
                {
                    n.InputReady++;
                    if (n.InputReady == n.Input.Count)
                        n.Pulse();
                }
            }
        }

        public int InputReady
        {
            get { return m_inputReady; }
            set { m_inputReady = value; }
        }

        public NeuralFactor Bias
        {
            get { return m_bias; }
            set { m_bias = value; }
        }

        public double Error
        {
            get { return m_error; }
            set
            {
                m_lastError = m_error;
                m_error = value;
            }
        }

        public void ApplyLearning(double learningRate)
        {
            foreach (KeyValuePair<INeuron, NeuralFactor> m in m_input)
                m.Value.ApplyWeightChange(learningRate);

            m_bias.ApplyWeightChange(learningRate);
        }

        public void CalculateErrors(double desiredResult)
        {
            this.OutputReady = 0;

            if (this.NeuronType == NeuronTypes.Output)
            {
                this.Error = (desiredResult - this.Value) * NeuralNet.SigmoidDerivative(this.Value); //* temp * (1.0F - temp);
            }
            else
            {
                this.Error = (desiredResult) * NeuralNet.SigmoidDerivative(this.Value); //* temp * (1.0F - temp);
            }


            foreach (Neuron n in this.Input.Keys)
            {
                if (n.NeuronType == NeuronTypes.Input)
                    continue;

                n.OutputReady++;
                if (n.OutputReady == n.Output.Count)
                {
                    double error = 0;

                    foreach (Neuron nn in n.Output)
                        error += (nn.Error * nn.Input[n].Weight);

                    n.CalculateErrors(error);
                }
                
            }
        }

        public void CalculateAndAppendTransformation()
        {
            foreach (Neuron n in this.Input.Keys)
            {
                this.Input[n].H_Vector += this.Error * n.Value;
            }

            this.Bias.H_Vector += this.Error * this.Bias.Weight;
        }

        public void InitializeLearning()
        {
            foreach (KeyValuePair<INeuron, NeuralFactor> m in m_input)
                m.Value.ResetWeightChange();

            m_bias.ResetWeightChange();
        }

        public void ResetLearning()
        {
            foreach (KeyValuePair<INeuron, NeuralFactor> m in m_input)
                m.Value.ResetValue();
        }

        public double LastError
        {
            get { return m_lastError; }
            set { m_lastError = value; }
        }

        #endregion

        #region Private Static Utility Methods

        public static double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        #endregion


        private  List<Neuron> m_output;

        

        public List<Neuron> Output
        {
            get { return m_output; }
            set { m_output = value; }
        }

        private int m_outputReady = 0;

        public int OutputReady
        {
            get { return m_outputReady; }
            set { m_outputReady = value; }
        }

        private NeuronTypes m_neuronType;

        public NeuronTypes NeuronType
        {
            get
            {
                return m_neuronType;
            }
            set
            {
                m_neuronType = value;
            }
        }
    }

    #region
    /* Deprecated 
    public class NeuralLayer : INeuralLayer
    {
        #region Constructor

        public NeuralLayer(LayerType aLayerType)
        {
            m_layerType = aLayerType;
            m_neurons = new List<INeuron>();
        }

        #endregion

        #region Member Variables

        private List<INeuron> m_neurons;

        #endregion

        #region IList<INeuron> Members

        public int IndexOf(INeuron item)
        {
            return m_neurons.IndexOf(item);
        }

        public void Insert(int index, INeuron item)
        {
            m_neurons.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            m_neurons.RemoveAt(index);
        }

        public INeuron this[int index]
        {
            get { return m_neurons[index]; }
            set { m_neurons[index] = value; }
        }

        #endregion

        #region ICollection<INeuron> Members

        public void Add(INeuron item)
        {
            m_neurons.Add(item);
        }

        public void Clear()
        {
            m_neurons.Clear();
        }

        public bool Contains(INeuron item)
        {
            return m_neurons.Contains(item);
        }

        public void CopyTo(INeuron[] array, int arrayIndex)
        {
            m_neurons.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_neurons.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(INeuron item)
        {
            return m_neurons.Remove(item);
        }

        #endregion

        #region IEnumerable<INeuron> Members

        public IEnumerator<INeuron> GetEnumerator()
        {
            return m_neurons.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region INeuralLayer Members

        public void Pulse(INeuralNet net)
        {
            foreach (INeuron n in m_neurons)
                n.Pulse(this);
        }

        public void ApplyLearning(INeuralNet net)
        {
            double learningRate = net.LearningRate;

            foreach (INeuron n in m_neurons)
                n.ApplyLearning(this, ref learningRate);
        }

        public void InitializeLearning(INeuralNet net)
        {
            foreach (INeuron n in m_neurons)
                n.InitializeLearning(this);
        }

        #endregion

        private LayerType m_layerType;

        public LayerType LayerType
        {
            get { return m_layerType; }
            set { m_layerType = value; }
        }
    }
    */
    #endregion

    public class NeuralNet : INeuralNet
    {
        #region Constructors

        public NeuralNet()
        {
            m_learningRate = 0.5;
        }

        #endregion

        #region Member Variables

        private double m_learningRate;
        private List<Neuron> m_neuron;
        private int m_inputNeuronCount = 0;
        private int m_outputNeuronCount = 0;

        #endregion

        #region INeuralNet Members

        public double LearningRate
        {
            get { return m_learningRate; }
            set { m_learningRate = value; }
        }

        public List<Neuron> Neuron
        {
            get { return m_neuron; }
            set { m_neuron = value; }
        }

        public double GlobalError
        {
            get
            {
                double dGlobalTemp = 0;
                int iNoPerception = Neuron.Count - InputNeuronCount;
                return dGlobalTemp / (double)iNoPerception;

            }
        }

        public int InputNeuronCount
        {
            get { return m_inputNeuronCount; }
        }

        public int OutputNeuronCount
        {
            get { return m_outputNeuronCount; }
        }

        #endregion

        #region Methods

        public void AddNeuron(Neuron n)
        {
            Neuron.Add(n);

            switch(n.NeuronType)
            {
                case NeuronTypes.Input: m_inputNeuronCount++; 
                    break;
                case NeuronTypes.Output: m_outputNeuronCount++;
                    break;
            }
        }

        public void RemoveNeuron(Neuron n)
        {
            Neuron.Remove(n);

            switch (n.NeuronType)
            {
                case NeuronTypes.Input: m_inputNeuronCount--;
                    break;
                case NeuronTypes.Output: m_outputNeuronCount--;
                    break;
            }
        }


        public void Initialize(int randomSeed)
        {
            Initialize(this, randomSeed);
        }

        public void PreparePerceptionLayerForPulse(double[] input)
        {
            PreparePerceptionLayerForPulse(this, input);
        }

        public void Pulse()
        {
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Input)
                        n.Pulse();
                }
            }
        }

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

        public void InitializeLearning()
        {
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType != NeuronTypes.Input)
                        n.InitializeLearning();
                }
            }
        }

        public void ResetLearning()
        {
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    n.ResetLearning();
                }
            }
        }

        private class SortCompare : IComparer<int>
        {

            #region IComparer<int> Members

            public int Compare(int x, int y)
            {
                return new Random().Next(1);
            }

            #endregion
        }

        public void Train(double[][] inputs, double[][] outputs, int iterations)
        {
            int i, j;

            //Randomize input order

            double[][] sortedInputs = new double[inputs.Length][];
            double[][] sortedOutputs = new double[outputs.Length][];

            int iAlreadyAdded = 0;

            List<int> lOrder = new List<int>(inputs.Length);
            for (int k = 0; k < inputs.Length; k++)
                lOrder.Add(k);

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

                    // set all weight changes to zero
                    InitializeLearning();

                    for (j = 0; j < inputs.Length; j++)
                        BackPropogation_TrainingSession(this, sortedInputs[j], sortedOutputs[j]);

                    ApplyLearning(); // apply batch of cumulative weight changes
                }



            }

        }

        #region Private Static Utility Methods -----------------------------------------------

        private static void Initialize(NeuralNet net, int randomSeed)
        {

            #region Declarations

            Random rand;

            #endregion

            #region Initialization

            rand = new Random(randomSeed);
            net.Neuron = new List<Neuron>();

            #endregion

            #region Execution

            //for (int i = 0; i < neuronCount; i++)
            //    net.m_neuron.Add(new Neuron(rand.NextDouble()));


            /*
            net.Layer.Add(new NeuralLayer(LayerType.Input));
            net.Layer.Add(new NeuralLayer(LayerType.Hidden));
            net.Layer.Add(new NeuralLayer(LayerType.Output));
            
            foreach (NeuralLayer nl in net.Layer)
            {
                nl.Add(new Neuron(nl.LayerType == LayerType.Input ? 0 : rand.NextDouble()));
            }

/*
            // wire-up input layer to hidden layer
            for (i = 0; i < net.m_hiddenLayer.Count; i++)
                for (j = 0; j < net.m_inputLayer.Count; j++)
                    net.m_hiddenLayer[i].Input.Add(net.m_inputLayer[j], new Sinapse(new NeuralFactor(rand.NextDouble()), (Neuron) net.m_inputLayer[j] ));

            // wire-up output layer to hidden layer
            for (i = 0; i < net.m_outputLayer.Count; i++)
                for (j = 0; j < net.m_hiddenLayer.Count; j++)
                    net.m_outputLayer[i].Input.Add(net.m_hiddenLayer[j], new Sinapse(new NeuralFactor(rand.NextDouble()), (Neuron)net.m_hiddenLayer[j]));

 */
        //    fmNNDesigner fm = new fmNNDesigner(net);
        //    fm.Show();

            #endregion
        }

        private void CalculateErrors(NeuralNet net, double[] desiredResults)
        {
            #region Declarations

            int i, j;
            double temp, error;

            #endregion

            #region Execution

            if (desiredResults.Length != OutputNeuronCount)
                throw new Exception("The number of desiredResults must be equal to number of output neurons.");

            int ni = 0;
            foreach (Neuron n in net.Neuron)
            {
                if (n.NeuronType == NeuronTypes.Output)
                {
                    n.CalculateErrors(desiredResults[ni]);
                    ni++;
                }
            }

        
            #endregion
        }

        public static double SigmoidDerivative(double value)
        {
            return value * (1.0F - value);
        }

        public void PreparePerceptionLayerForPulse(NeuralNet net, double[] input)
        {

            #region Execution


            if (input.Length != InputNeuronCount)
                throw new Exception("The number of input must be equal to number of perception neurons.");

            // initialize data
            int i = 0;
            foreach (Neuron n in net.Neuron)
            {
                if (n.NeuronType == NeuronTypes.Input)
                {
                    n.Value = input[i];
                    i++;
                }
            }

            #endregion

        }

        public static void CalculateAndAppendTransformation(NeuralNet net)
        {
            #region Declarations


            int i, j;
            INeuron outputNode, inputNode, hiddenNode;


            #endregion

            #region Execution

            foreach (Neuron n in net.Neuron)
                n.CalculateAndAppendTransformation();
            

            //// adjust output layer weight change
            //for (j = 0; j < net.m_outputLayer.Count; j++)
            //{
            //    outputNode = net.m_outputLayer[j];

            //    for (i = 0; i < net.m_hiddenLayer.Count; i++)
            //    {
            //        hiddenNode = net.m_hiddenLayer[i];
            //        outputNode.Input[hiddenNode].NeuralFactor.H_Vector += outputNode.Error * hiddenNode.Value;
            //    }

            //    outputNode.Bias.H_Vector += outputNode.Error * outputNode.Bias.Weight;
            //}

            //// adjust hidden layer weight change
            //for (j = 0; j < net.m_hiddenLayer.Count; j++)
            //{
            //    hiddenNode = net.m_hiddenLayer[j];

            //    for (i = 0; i < net.m_inputLayer.Count; i++)
            //    {
            //        inputNode = net.m_inputLayer[i];
            //        hiddenNode.Input[inputNode].NeuralFactor.H_Vector += hiddenNode.Error * inputNode.Value;
            //    }

            //    hiddenNode.Bias.H_Vector += hiddenNode.Error * hiddenNode.Bias.Weight;
            //}

            #endregion
        }


        #region Backprop

        public void BackPropogation_TrainingSession(NeuralNet net, double[] input, double[] desiredResult)
        {
            PreparePerceptionLayerForPulse(net, input);
            net.Pulse();
            CalculateErrors(net, desiredResult);
            CalculateAndAppendTransformation(net);
        }

        #endregion


        #endregion Private Static Utility Methods -------------------------------------------


        #endregion


    }


    public interface INeuronReceptor
    {
        Dictionary<INeuron, NeuralFactor> Input { get; }

        List<Neuron> Output
        {
            get;
            set;
        }

        
    }

    public interface ISinapse
    {
        Neuron Input
        {
            get;
            set;
        }

        Neuron Output
        {
            get;
            set;
        }
    }

    public enum NeuronTypes
    {
        Input,
        Hidden,
        Output
    }
}