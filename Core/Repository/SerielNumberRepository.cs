using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataPresistence;
using Core.Model;

namespace Core.Repository
{
    class SerielNumberRepository : ISerielNumberRepository
    {
        /// <summary>
        /// Calls serielnumber generator method
        /// </summary>
        /// <param name="amount">The amount of serielnumbers to be generated</param>
        public void GenerateNewSerielNumbers(int amount, IDictionary<string, bool> lotteryDictionary)
        {
            ISerielNumberGenerator newGenereter = new SerielNumberGenerator();
            lotteryDictionary = newGenereter.GenerateSerielNumberDictionary(amount);
        }

        /// <summary>
        /// Recives a string list containing the serielnumbers recovered from a file on the file system
        /// Splits each string in the a serielnumber and a bool value, adding these as key and value in the seriel dictionary
        /// </summary>
        /// <returns>A bool</returns>
        public async Task<IDictionary<string,bool>>SerielNumbersFromFile()
        {
            IFileUpStream fileUpStream = new FileUpStream();
            return await fileUpStream.LoadSerielNumbersFromFile();
            //IList <string> serielnumbers;
            //IDictionary<string,bool> lotteryDictionary = new Dictionary<string, bool>();
            //serielnumbers = await fileUpStream.LoadSerielNumbersFromFile();

            //foreach (var item in serielnumbers)
            //{
            //    string seriel = item.Substring(0, 4);
            //    string value = item.Substring(5);
            //    lotteryDictionary.Add(seriel, Convert.ToBoolean(value));

            //}
            //return lotteryDictionary;
        }

        /// <summary>
        /// Save seerielnumbers to in a file on the file system
        /// </summary>
        public async Task SerielNumbersToFile(IDictionary<string, bool> lotteryDictionary)
        {
            FileDownStream fileDownStream = new FileDownStream();
            if (lotteryDictionary != null) await fileDownStream.SaveSerielNumbersToFileAsync(lotteryDictionary);
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
        public int LookUpSerielNumber(string serielnumber, IDictionary<string, bool> lotteryDictionary)
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

        public List<SerialModel> GetSerials(IDictionary<string, bool> lotteryDictionary )
        {
            List<SerialModel> serialList = new List<SerialModel>();
            foreach (var item in lotteryDictionary)
            {
                serialList.Add(new SerialModel(item.Key,item.Value));
            }
            return serialList;
        }
    }
}
