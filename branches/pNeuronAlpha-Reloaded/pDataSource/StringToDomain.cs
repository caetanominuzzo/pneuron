using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pDataSource
{
    public static class StringToDomain
    {
        public static Dictionary<string, int> Transform(string Value)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (char c in Value)
                if (c != '\n' && !result.ContainsKey(c.ToString()))
                    result.Add(c.ToString(), result.Count);

            return result;
        }
    }
}
