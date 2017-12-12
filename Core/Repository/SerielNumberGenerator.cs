using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    class SerielNumberGenerator : ISerielNumberGenerator
    {
        private readonly string[] _serielLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private Random _random;
        private int _amountOfSerielNumbers { get; set; }

        public SerielNumberGenerator()
        {
        }

        /// <summary>
        /// Generets a set amount of seriel numbers compriesed of a letter and three numbers ex. E452
        /// Theese seriel numbers are add to a Dictinary as the key and a true boolean value as the value. 
        /// The true boolean value means the the seriel number has not yet been claimed.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>A Dictionary</returns>
        public Dictionary<string, bool> GenerateSerielNumberDictionary(int amount)
        {
            Dictionary<string, bool> _serielDictionary = new Dictionary<string, bool>();
            _random = new Random();
            _amountOfSerielNumbers = amount;
            while (_serielDictionary.Count < _amountOfSerielNumbers)
            {
                string serielnumber = _serielLetters[_random.Next(0, 26)] + _random.Next(0, 10) + _random.Next(0, 10) +
                                      _random.Next(0, 10);
                if (!_serielDictionary.ContainsKey(serielnumber))
                {
                    _serielDictionary.Add(serielnumber, true);
                }
            }
            return _serielDictionary;
        }
    }
}
