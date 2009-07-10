using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pDataSource
{
    public static class IntToNeuronData
    {

        public static int[][] Transform(int[][] Value, Dictionary<string, int> Domain, int NeuronPerDomainId, int LogBase)
        {

            int TotalNeurons = Value[0].Length * NeuronPerDomainId;

            int[][] result = new int[Value.Length][];
            int[] _t;
            int yPos = 0;

            for (int x = 0, y = 0; x < result.Length; x++)
            {
                result[x] = new int[TotalNeurons];
                yPos = 0;

                for (y = 0; y < Value[x].Length; y++)
                {
                    
                    _t = DecimalToBase(Value[x][y], LogBase, NeuronPerDomainId);

                    foreach (int i in _t)
                    {
                        result[x][yPos++] = i;
                        
                    }
                }
            }

            return result;
        }

        public static int BaseToDecimal(int[] Value, int LogBase)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            
            if (LogBase < 2 || LogBase > characters.Length)
            {
                throw new ArgumentOutOfRangeException("nBase", LogBase, "Range: 2.." + characters.Length);
            }
            
            int result = 0;

            for (int i = 0; i < Value.Length; i++)
            {
                result += Value[i] * (int)Math.Pow(LogBase, Value.Length - i - 1);
            }

            //for (int i = 0; i < Value.Length; i++)
            //{
            //    int value = characters.IndexOf(Value[i]);
            //    if (value >= LogBase || value < 0)
            //    {
            //        throw new ArgumentOutOfRangeException("input[" + i + "]", Value[i], "This character is not valid for base " + LogBase);
            //    }
            //    result += value * (int)Math.Pow(LogBase, Value.Length - i - 1);
            //}
            //if (negative)
            //{
            //    result *= -1;
            //}

            return result;
        }

        private static int[] result;
        private static int xPos = 0;

        private static int[] DecimalToBase(int input, int nBase, int Length)
        {
            result = new int[Length];

            if (input == 0)
                return result;

            xPos = Length - 1;
            
            while (input != 0)
            {
                result[xPos] = input % nBase;
                input /= nBase;
                xPos--;
            }

            return result;
        }
    }
}