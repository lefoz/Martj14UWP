using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.DataPresistence
{
    interface IFileUpStream
    {
        Task<bool> isFilePresent(int stage);
        Task<IDictionary<string, string>> LoadLoginsFromFileAsync();
        Task<IDictionary<string, bool>> LoadSerielNumbersFromFile();
        Task<IList<Submission>> LoadSubmissionsFromFileAsync();
    }
}