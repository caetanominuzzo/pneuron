using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron.Core
{
    public enum TrainingType
    {
        BackPropogation
    }

    /*
    public enum LayerType
    {
        Input,
        Hidden,
        Output
    }
     * */

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

        public void ApplyWeightChange(ref double learningRate)
        {
            m_lastDelta = m_delta;
            m_weight += m_delta * learningRate;
        }

        public void ResetWeightChange()
        {
            m_lastDelta = m_delta = 0;
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
        void ApplyLearning(ref double learningRate);
        void InitializeLearning();

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

        public Neuron(double bias)
        {
            m_bias = new NeuralFactor(bias);
            m_error = 0;
            m_input = new Dictionary<INeuron, NeuralFactor>();
            m_output = new List<Neuron>();
        }

        #endregion

        #region Member Variables

        private Dictionary<INeuron, NeuralFactor> m_input;
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
                m_value = 0;

                foreach (KeyValuePair<INeuron, NeuralFactor> item in m_input)
                    m_value += item.Key.Value * item.Value.Weight;

                m_value += m_bias.Weight;

                m_value = Sigmoid(m_value);
            }
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

        public void ApplyLearning(ref double learningRate)
        {
            foreach (KeyValuePair<INeuron, NeuralFactor> m in m_input)
                m.Value.ApplyWeightChange(ref learningRate);

            m_bias.ApplyWeightChange(ref learningRate);
        }

        public void InitializeLearning()
        {
            foreach (KeyValuePair<INeuron, NeuralFactor> m in m_input)
                m.Value.ResetWeightChange();

            m_bias.ResetWeightChange();
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

        private int m_join;

        public int Join
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        private  List<Neuron> m_output;

        public List<Neuron> Output
        {
            get { return m_output; }
            set { m_output = value; }
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

        #endregion

        private List<Neuron> m_neuron;

        public List<Neuron> Neuron
        {
            get { return m_neuron; }
            set { m_neuron = value; }
        }

        #region INeuralNet Members


        public double LearningRate
        {
            get { return m_learningRate; }
            set { m_learningRate = value; }
        }

        public void Pulse()
        {
            lock (this)
            {
                foreach (Neuron n in this.Neuron)
                {
                    if(n.NeuronType == NeuronTypes.Perception)
                        n.Pulse();
                }
            }
        }

        public void ApplyLearning()
        {
            lock (this)
            {
                //TODO:DEFINE A START LAYER
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Output)
                        n.ApplyLearning();
                }
            }
        }

        public void InitializeLearning()
        {
            lock (this)
            {
                //TODO:DEFINE A START LAYER
                foreach (Neuron n in this.Neuron)
                {
                    if (n.NeuronType == NeuronTypes.Hidden)
                        n.InitializeLearning();
                }
            }
        }

        public void Train(double[][] inputs, double[][] expected, TrainingType type, int iterations)
        {
            int i, j;

            switch (type)
            {
                case TrainingType.BackPropogation:

                    lock (this)
                    {

                        for (i = 0; i < iterations; i++)
                        {

                            InitializeLearning(); // set all weight changes to zero

                            for (j = 0; j < inputs.Length; j++)
                                BackPropogation_TrainingSession(this, inputs[j], expected[j]);

                            ApplyLearning(); // apply batch of cumlutive weight changes
                        }

                    }
                    break;
                default:
                    throw new ArgumentException("Unexpected TrainingType");
            }
        }

        #endregion

        #region Methods

        public void Initialize(int randomSeed,
            int neuronCount)
        {
            Initialize(this, randomSeed, neuronCount);
        }

        public void PreparePerceptionLayerForPulse(double[] input)
        {
            PreparePerceptionLayerForPulse(this, input);
        }

        #region Private Static Utility Methods -----------------------------------------------

        private static void Initialize(NeuralNet net, int randomSeed,
            int neuronCount)
        {

            #region Declarations

            Random rand;

            #endregion

            #region Initialization

            rand = new Random(randomSeed);
            net.Neuron = new List<Neuron>(neuronCount);

            #endregion

            #region Execution

            for (int i = 0; i < neuronCount; i++)
                net.m_neuron.Add(new Neuron(rand.NextDouble()));


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

        private static void CalculateErrors(NeuralNet net, double[] desiredResults)
        {
            #region Declarations

            int i, j;
            double temp, error;

            #endregion

            #region Execution


            //Errors must be calculated from output to perception layer
            //so the layer order will be inverted.
            net.Layer.Reverse();

            NeuralLayer lastLayer = null;
            INeuron n;


            /*
            foreach (NeuralLayer nl in net.Layer)
            {
                if (nl.LayerType == LayerType.Input)
                    continue;

                if (lastLayer == null)
                {
                    for (i = 0; i < nl.Count; i++)
                    {
                        n = nl[i];
                        temp = n.Output;

                        n.Error = (desiredResults[i] - temp) * SigmoidDerivative(temp); //* temp * (1.0F - temp);
                    }
                }
                else
                {

                    for (i = 0; i < nl.Count; i++)
                    {
                        n = nl[i];
                        temp = n.Output;

                        error = 0;

                        n

                        for (j = 0; j < lastLayer.Count; j++)
                        {
                            error += (lastLayer[j].Error * lastLayer[j].Input[n].NeuralFactor.Weight) * SigmoidDerivative(temp);
                        }

                        n.Error = error;
                    }
                }

                lastLayer = nl;
            }
             */


            //undo reverse
            net.Layer.Reverse();

            

            /*
            // Calcualte output error values 
             for (i = 0; i < net.m_outputLayer.Count; i++)
            {
                outputNode = net.m_outputLayer[i];
                temp = outputNode.Output;
                outputNode.Error = (desiredResults[i] - temp) * SigmoidDerivative(temp); //* temp * (1.0F - temp);
            }


            // calculate hidden layer error values
            for (i = 0; i < net.m_hiddenLayer.Count; i++)
            {
                hiddenNode = net.m_hiddenLayer[i];
                temp = hiddenNode.Output;

                error = 0;
                for (j = 0; j < net.m_outputLayer.Count; j++)
                {
                    outputNode = net.m_outputLayer[j];
                    error += (outputNode.Error * outputNode.Input[hiddenNode].NeuralFactor.Weight) * SigmoidDerivative(temp);// *(1.0F - temp);                   
                }

                hiddenNode.Error = error;

            }
            */
            #endregion
        }

        private static double SigmoidDerivative(double value)
        {
            return value * (1.0F - value);
        }

        public static void PreparePerceptionLayerForPulse(NeuralNet net, double[] input)
        {
            #region Declarations

            int i;

            #endregion

            #region Execution

            if (input.Length != net.Layer[0].Count)
                throw new ArgumentException(string.Format("Expecting {0} inputs for this net", net.Layer[0].Count));

            // initialize data
            for (i = 0; i < net.Layer[0].Count; i++)
                net.Layer[0][i].Value = input[i];

            #endregion

        }

        public static void CalculateAndAppendTransformation(NeuralNet net)
        {
            #region Declarations


            int i, j;
            INeuron outputNode, inputNode, hiddenNode;


            #endregion

            #region Execution

            
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

        public static void BackPropogation_TrainingSession(NeuralNet net, double[] input, double[] desiredResult)
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

    public class pSinapse
    {
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
        Perception,
        Hidden,
        Output,
    }
}