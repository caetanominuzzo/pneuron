using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;

namespace primeira.pNeuron.Editor.Business
{
    [DataContract()]
    public abstract class DocumentBase
    {
        public abstract DocumentDefinition GetDefinition { get; }

    }
}
