using System.Collections.Generic;

namespace Core.Repository
{
    interface ISerielNumberGenerator
    {
        IDictionary<string, bool> GenerateSerielNumberDictionary(int amount);
    }
}