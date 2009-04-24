using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace primeira.Data.Version
{
    public class VersionNumber : IRevision
    {
        private int _major;
        public int Major { get { return _major; } set { if (OnRevision != null) OnRevision(this, _major, value); _major = value; } }
        public int Minor { get; set; }
        public int Revision { get; set; }
        public string Label { get; set; }
        public int Quantifier { get; set; }

        public event OnRevisionDelegate OnRevision;
    }
}
