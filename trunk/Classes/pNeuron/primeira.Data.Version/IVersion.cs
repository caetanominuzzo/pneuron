using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace primeira.Data.Version
{
    public interface IVersion
    {
        VersionData Version
        {
            get;
            set;
        }
    }
}
