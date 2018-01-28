using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.DataPresistence
{
    interface IFileDownStream
    {
        Task SaveLoginsToFileAsync(IDictionary<string,string> logins);
        Task SaveSerielNumbersToFileAsync(IDictionary<string, bool> serielnumbers);
        Task SaveSubmissionsToFileAsync(IList<Submission> submissions);
    }
}