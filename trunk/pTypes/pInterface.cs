using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace primeira.pTypes
{
    public interface IpSmartZoom
    {
        Size ZoomSize { get; }
    }

    public interface IpDomainProvider
    {
        String Domain();
    }
}
