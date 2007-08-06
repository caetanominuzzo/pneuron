using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron.pRandom
{
    /// <summary>
    /// True random number generator.
    /// </summary>
    public class pRandom
    {

        public pRandom()
        {
            Refresh();
        }

        private String readHtmlPage(string url)
        {
            String result;
            HttpWebRequest makeReq = (HttpWebRequest)WebRequest.Create("http://random.org/decimal-fractions/?num=100&dec=20&col=1&format=plain&rnd=new");
            NetworkCredential giveCred = new NetworkCredential("caetano", "0123456789", "CWIPOA");
            CredentialCache putCache = new CredentialCache();
            putCache.Add(new Uri("http://10.0.101.226:8080/"), "Basic", giveCred);
            makeReq.Credentials = putCache;
            WebResponse objResponse;
            objResponse = makeReq.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }

        private void Refresh()
        {
            //readHtmlPage("");

            //  m_cache.Clear();

            // used to build entire input
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];

            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(
                    string.Format("http://random.org/decimal-fractions/?num={0}&dec=20&col=1&format=plain&rnd=new",
                                  m_cache.Capacity - m_cache.Count));


            //            NetworkCredential giveCred = new NetworkCredential
            //("caetano", "0123456789", "CWIPOA"); 


            //            WebProxy proxyObject = new WebProxy("http://10.0.101.226:8080/",true);



            //            request.Proxy = proxyObject;

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = System.Text.Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            string[] ss = sb.ToString().Split('\n');
            foreach (string s in ss)
            {
                if (s.Length == 0)
                    break;
                m_cache.Add(Convert.ToDouble(s, System.Globalization.CultureInfo.InvariantCulture));
            }
        }

        #region Fields

        List<double> m_cache = new List<double>(20);

        Random m_random = new Random();

        #endregion

        /// <summary>
        /// Gets a real random double.
        /// </summary>
        /// <returns></returns>
        public double GetDouble()
        {
            if (m_cache.Count < 10)
            {
                Refresh();
            }

            double d = m_cache[0];
            m_cache.RemoveAt(0);

            return d;

        }
    }

}
