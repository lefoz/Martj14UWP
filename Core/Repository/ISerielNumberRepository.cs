using System.Collections.Generic;

namespace Core.Repository
{
    interface ISerielNumberRepository
    {
        void GenerateNewSerielNumbers(int amount, Dictionary<string, bool> lotteryDictionary);
        int LookUpSerielNumber(string serielnumber, Dictionary<string, bool> lotteryDictionary);
        bool SerielNumbersFromFile(Dictionary<string, bool> lotteryDictionary);
        void SerielNumbersToFile(Dictionary<string, bool> lotteryDictionary);
    }
}