using System.Collections.Generic;
using Core.Model;

namespace Core.DataPresistence
{
    interface IFileDownStream
    {
        void SaveLoginsToFile(string email, string password);
        void SaveSerielNumbersToFile(Dictionary<string, bool> serielnumbers);
        void SaveSubmissionsToFile(List<Submission> submissions);
    }
}