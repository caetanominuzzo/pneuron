﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace primeira.pNeuron.Editor.Business
{
    public interface IRecentFileManager
    {
        void AddRecent(string filename);

        string[] GetRecent();
    }
}