﻿using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pDataSource;

namespace Test
{
    class Program
    {
        static primeira.pNeuron.Core.NeuralNetwork net;

        static void Main(string[] args)
        {
            int ilogBase = int.Parse(args[2]);
            int ologBase = int.Parse(args[3]);

            TextFileToStringInputOutput.StringInputOutput s = TextFileToStringInputOutput.Transform(args[0]);

            Dictionary<string, int> dInputDomain = StringToDomain.Transform(s.Input);
            Dictionary<string, int> dOutputDomain = StringToDomain.Transform(s.Output);

            int[][] iiInput = StringToNeuronData.Transform(s.Input, dInputDomain, ilogBase);
            int[][] iiOutput = StringToNeuronData.Transform(s.Output, dOutputDomain, ologBase);

            Console.WriteLine(StringToNeuronData.Untransform(iiInput, dInputDomain, ilogBase));
            Console.ReadKey();

            net = primeira.pNeuron.Core.NeuralNetwork.ToObject(args[1]);

            double[][] i = (from x in iiInput select (from y in x select y == 0 ? -.9d : .9d).ToArray()).ToArray();

            double[][] o = (from x in iiOutput select (from y in x select y == 0 ? -.9d : .9d).ToArray()).ToArray();

            net.OnRefreshCyclesSec += new primeira.pNeuron.Core.NeuralNetwork.OnRefreshCyclesSecDelegate(net_OnRefreshCyclesSec);

            net.Train(i, o);

        }

        static void net_OnRefreshCyclesSec(int Times)
        {
            Console.WriteLine(net.LastCalculatedGlobalError);

            if (net.LastCalculatedGlobalError < 0.00000001)
                net.StopOnNextCycle();
        }
    }
}