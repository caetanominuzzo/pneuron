using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace primeira.pTypes
{
    public interface IpSmartZoom
    {
        Size ZoomSize { get; }
    }

    public interface IpDomainProvider
    {
        DialogResult ShowDialog();
        //string DomainName;
    }
}
