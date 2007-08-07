using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace primeira.pRandom
{
    /// <summary>
    /// True random number generator.
    /// </summary>
    public class pTrueRandomGenerator
    {
        
        /// <summary>
        /// Used as contingency.
        /// </summary>
        private Random m_pseudoRandom;

        /// <summary>
        /// True random number generator.
        /// </summary>
        /// <param name="cacheSize">Number of doubles on cache.</param>
        public pTrueRandomGenerator(int cacheSize)
        {
            try
            {
                m_cache = new List<double>(cacheSize);
                RefreshCache();
            }
            catch
            {
                m_pseudoRandom = new Random(1);
                //TODO:Log warning
            }
        }

        private void RefreshCache()
        {
            StringBuilder sb = new StringBuilder();

            byte[] buf = new byte[1024];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    string.Format("http://random.org/decimal-fractions/?num={0}&dec=20&col=1&format=plain&rnd=new",
                                  m_cache.Capacity - m_cache.Count));


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                count = resStream.Read(buf, 0, buf.Length);

                if (count != 0)
                {
                    tempString = System.Text.Encoding.ASCII.GetString(buf, 0, count);

                    sb.Append(tempString);
                }
            }
            while (count > 0);

            string[] ss = sb.ToString().Split('\n');
            foreach (string s in ss)
            {
                if (s.Length == 0)
                    break;
                m_cache.Add(Convert.ToDouble(s, System.Globalization.CultureInfo.InvariantCulture));
            }
        }

        #region Fields

        List<double> m_cache;

        Random m_random = new Random();

        #endregion

        /// <summary>
        /// Gets a real random double.
        /// </summary>
        /// <returns></returns>
        public double GetDouble()
        {
            double d = 0;
            if (m_cache.Count > 0)
            {
                if (m_cache.Count < m_cache.Capacity / 10 )
                {
                    RefreshCache();
                }

                d = m_cache[0];
                m_cache.RemoveAt(0);
            }
            else
            {
                d = m_pseudoRandom.NextDouble();
            }

            return d;

        }
    }

}