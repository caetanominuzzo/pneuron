using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace pDataSource
{

    public static class StringToNeuronData
    {

        public static int[][] Transform(string Value, Dictionary<string, int> Domain, int LogBase)
        {
            //Been sent an extra \n the string Value must be changed.
            if (Value[Value.Length - 1] == '\n')
                Value = Value.Substring(0, Value.Length - 1);

            string[] tmpLines = Value.Split('\n');

            int[][] tmpData = new int[tmpLines.Length][];

            int Length = tmpLines[0].Length;

            for (int x = 0; x < tmpData.Length; x++)
            {
                tmpData[x] = new int[Length];

                for (int y = 0; y < Length; y++)
                {
                    tmpData[x][y] = Domain[tmpLines[x][y].ToString()];
                }
            }

            int neuronPerDomainId = (int)Math.Ceiling(Math.Log(Domain.Count, LogBase));

            if (neuronPerDomainId == 0)
                neuronPerDomainId = 1;

            return IntToNeuronData.Transform(tmpData, Domain, neuronPerDomainId, LogBase);
        }

        public static string Untransform(int[][] Value, Dictionary<string, int> Domain, int LogBase)
        {
            int neuronPerDomainId = (int)Math.Ceiling(Math.Log(Domain.Count, LogBase));

            int[] tmpData = new int[neuronPerDomainId];

            StringBuilder sb = new StringBuilder();
            int yPos = 0;

            for (int x = 0; x < Value.Length; x++)
            {
                for (int y = 0; y < Value[x].Length; y++)
                {
                    tmpData[yPos++] = Value[x][y];

                    if (yPos == neuronPerDomainId)
                    {
                        yPos = 0;
                        string s= (from xx in Domain where xx.Value == IntToNeuronData.BaseToDecimal(tmpData, LogBase) select (string)xx.Key).FirstOrDefault();
                        sb.Append(s);
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
