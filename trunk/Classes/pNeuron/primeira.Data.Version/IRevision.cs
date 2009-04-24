using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace primeira.Data.Version
{
    public delegate void OnRevisionDelegate(object sender, object oldValue, object newValue);
    public interface IRevision
    {
        event OnRevisionDelegate OnRevision;
    }
}
