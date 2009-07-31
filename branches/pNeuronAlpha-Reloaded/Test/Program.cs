using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pDataSource;
using primeira.pNeuron.Core;
using pNeuronEditor.Topology;

namespace Test
{
    class Program
    {
        static primeira.pNeuron.Core.NeuralNetwork net;

static double[][] i;

static double[][] o;

 static Dictionary<string, int> dInputDomain ;
 static Dictionary<string, int> dOutputDomain;

static int ilogBase;
static int ologBase;


        static void Main(string[] args)
        {
            ilogBase = int.Parse(args[2]);
            ologBase = int.Parse(args[3]);

            TextFileToStringInputOutput.StringInputOutput s = TextFileToStringInputOutput.Transform(args[0]);

           dInputDomain = StringToDomain.Transform(s.Input);
           dOutputDomain = StringToDomain.Transform(s.Output);

            int[][] iiInput = StringToNeuronData.Transform(s.Input, dInputDomain, ilogBase);
            int[][] iiOutput = StringToNeuronData.Transform(s.Output, dOutputDomain, ologBase);

            Console.WriteLine(StringToNeuronData.Untransform(iiInput, dInputDomain, ilogBase));
            Console.ReadKey();


            pNeuronEditor.Business.EditorManager.RegisterEditors();

            net = ((NeuralNetworkDocument)NeuralNetworkDocument.ToObject(args[1])).NeuralNetwork;

            i = (from x in iiInput select (from y in x select y == 0 ? -.9d : .9d).ToArray()).ToArray();

            o = (from x in iiOutput select (from y in x select y == 0 ? -.9d : .9d).ToArray()).ToArray();

            net.OnRefreshCyclesSec += new primeira.pNeuron.Core.NeuralNetwork.OnRefreshCyclesSecDelegate(net_OnRefreshCyclesSec);

            net.OnStopTraing += new NeuralNetwork.OnStopTraingDelegate(net_OnStopTraing);
            
            net.Train(i, o);



        }

        static void net_OnStopTraing()
        {
            //double[] i = new double[] { -0.9d, 0.9d };

            for (int j = 0; j < i.Length; j++)
            {
                net.SetInputData(i[j]);
                
                net.Pulse();

                System.Threading.Thread.Sleep(500);

                double i1 = primeira.pNeuron.Core.SigmoidUtils.UnSigmoid(i[j][0]);

                Console.WriteLine(" in: " + (i1 < 0 ? -.9d : .9d));

                double i2 = primeira.pNeuron.Core.SigmoidUtils.UnSigmoid(i[j][1]);

                Console.WriteLine(" in: " + (i2 < 0 ? -.9d : .9d));

                double o1 = primeira.pNeuron.Core.SigmoidUtils.UnSigmoid(net.Neuron[net.Neuron.Count - 1].Value);

                Console.WriteLine("out: "+ (o1 < 0 ? -.9d : .9d));

                double o2 = primeira.pNeuron.Core.SigmoidUtils.UnSigmoid(net.Neuron[net.Neuron.Count - 2].Value);

                Console.WriteLine("out:" +(o2 < 0 ? -.9d : .9d));

                double d1 = primeira.pNeuron.Core.SigmoidUtils.UnSigmoid(o[j][0]);

                Console.WriteLine("out: " + (d1 < 0 ? -.9d : .9d));

                double d2 = primeira.pNeuron.Core.SigmoidUtils.UnSigmoid(o[j][1]);

                Console.WriteLine("out:" + (d2 < 0 ? -.9d : .9d));



                Console.WriteLine();
            }


            Console.ReadKey();
        }

        static void net_OnRefreshCyclesSec(int Times)
        {
            Console.Clear();
            Console.WriteLine(net.LastCalculatedGlobalError);
            Console.WriteLine();


            if (net.LastCalculatedGlobalError < 0.00001)
            {

                net.StopOnNextCycle();

                

                

            }


        }
    }
}
