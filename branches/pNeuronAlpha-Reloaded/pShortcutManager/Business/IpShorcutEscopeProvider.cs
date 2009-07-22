using System;
using System.Collections.Generic;
using System.Text;

namespace pShortcutManager.Business
{
    public interface IpShorcutEscopeProvider
    {
        bool IsAtiveByControl(string controlName);

        bool IsAtiveByEscope(string escope);
    }
}
