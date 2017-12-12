﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.DataPresistence
{
    interface IFileUpStream
    {
        List<string> LoadLoginsFromFile();
        List<string> LoadSerielNumbersFromFile();
        List<Submission> LoadSubmissionsFromFile();
    }
}