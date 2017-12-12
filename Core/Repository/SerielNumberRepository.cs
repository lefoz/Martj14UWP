using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataPresistence;

namespace Core.Repository
{
    class SerielNumberRepository : ISerielNumberRepository
    {
        /// <summary>
        /// Calls serielnumber generator method
        /// </summary>
        /// <param name="amount">The amount of serielnumbers to be generated</param>
        public void GenerateNewSerielNumbers(int amount, Dictionary<string, bool> lotteryDictionary)
        {
            ISerielNumberGenerator newGenereter = new SerielNumberGenerator();
            lotteryDictionary = newGenereter.GenerateSerielNumberDictionary(amount);
        }

        /// <summary>
        /// Recives a string list containing the serielnumbers c´recovered from a file on the file system
        /// Splits each string in the a serielnumber and a bool value, adding these as key and value in the seriel dictionary
        /// </summary>
        /// <returns>A bool</returns>
        public bool SerielNumbersFromFile(Dictionary<string, bool> lotteryDictionary)
        {
            FileUpStream fileUpStream = new FileUpStream();
            List<string> serielnumbers = fileUpStream.LoadSerielNumbersFromFile();
            foreach (var item in serielnumbers)
            {
                string seriel = item.Substring(0, 4);
                string value = item.Substring(5);
                lotteryDictionary.Add(seriel, Convert.ToBoolean(value));

            }
            if (lotteryDictionary.Count > 0) return true;
            return false;
        }

        /// <summary>
        /// Save seerielnumbers to in a file on the file system
        /// </summary>
        public void SerielNumbersToFile(Dictionary<string, bool> lotteryDictionary)
        {
            FileDownStream fileDownStream = new FileDownStream();
            fileDownStream.SaveSerielNumbersToFile(lotteryDictionary);
        }

        /// <summary>
        /// Checks Dictionary for serielnumber
        /// If Dictionary contains serielnumber, its value reprencents if this serielnumber has been submitted
        /// If true = unsubmitted return, value is set to false and the method returns 2
        /// If false = already submitted and the method returns 1
        /// If the Dictionary do not contain serielnumber the method returns 0  
        /// </summary>
        /// <param name="serielnumber"></param>
        /// <returns></returns>
        public int LookUpSerielNumber(string serielnumber, Dictionary<string, bool> lotteryDictionary)
        {
            int lookUpConfirmation = 0;
            if (lotteryDictionary.ContainsKey(serielnumber))
            {
                if (lotteryDictionary[serielnumber].Equals(true))
                {
                    lotteryDictionary[serielnumber] = false;
                    lookUpConfirmation = 2;
                    return lookUpConfirmation;
                }
                else
                {
                    lookUpConfirmation = 1;
                    return lookUpConfirmation;
                }
            }
            return lookUpConfirmation;
        }
    }
}
