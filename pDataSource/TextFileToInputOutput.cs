using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace pDataSource
{
    public static class TextFileToStringInputOutput
    {
        public struct StringInputOutput
        {
            public string Input { get; internal set; }
            public string Output { get; internal set; }
        }
        
        /// <summary>
        /// Transforms a text file content in the format:
        /// <example>content input|contentoutput\n</example>
        /// in a struct StringInputOutput.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static StringInputOutput Transform(string filePath)
        {
          
            StringInputOutput result = new StringInputOutput();

            string[] lines = File.ReadAllLines(filePath);

            StringBuilder input = new StringBuilder();
            StringBuilder output = new StringBuilder();

            int inputLength = lines[0].IndexOf('|');

            foreach (string line in lines)
            {
                input.AppendLine(line.Substring(0, inputLength));
                output.AppendLine(line.Substring(inputLength + 1));
            }


            input.Replace("\r", "");
            output.Replace("\r", "");

            result.Input = input.ToString();
            result.Output = output.ToString();

            return result;
        }

        
    }

    

}

