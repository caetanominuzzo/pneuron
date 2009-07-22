using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace pShortcutManager.Business
{
    internal class pShortcutCommand
    {
        internal string Name;
        internal string Description;
        internal MethodInfo Method;
        internal object Object;
        internal string Escope;
    }
}
