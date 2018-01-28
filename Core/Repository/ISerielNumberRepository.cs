using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Repository
{
    interface ISerielNumberRepository
    {
        void GenerateNewSerielNumbers(int amount, IDictionary<string, bool> lotteryDictionary);
        int LookUpSerielNumber(string serielnumber, IDictionary<string, bool> lotteryDictionary);
        Task<IDictionary<string, bool>>SerielNumbersFromFile();
        Task SerielNumbersToFile(IDictionary<string, bool> lotteryDictionary);
        List<SerialModel> GetSerials(IDictionary<string, bool> lotteryDictionary);
    }
}