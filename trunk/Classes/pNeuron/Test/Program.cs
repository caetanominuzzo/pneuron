using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using primeira.Data;
using primeira.Data.Version;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            VersionData v = new VersionData();
            v.OnRevision += new OnRevisionDelegate(v_OnRevision);
            v.Authorship.author = "C";
            v.Number.Major = 10;
        }

        static void v_OnRevision(object sender, object oldValue, object newValue)
        {
            Console.WriteLine("{0}\n{1}\n{2}", sender.ToString(), oldValue, newValue);
            Console.ReadKey();

        }
    }
}
