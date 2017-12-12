using System.Collections.Generic;

namespace Core.Repository
{
    interface ISerielNumberGenerator
    {
        Dictionary<string, bool> GenerateSerielNumberDictionary(int amount);
    }
}