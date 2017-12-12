using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.Model;


namespace Core.DataPresistence
{
    class FileDownStream : IFileDownStream
    {
        private readonly string _serielNumberFileName = "SerielNumbers.dat";
        private readonly string _submissionsFileName = "Submissions.bin";
        private readonly string _loginsFileName = "Logins.dat";

        public void SaveSerielNumbersToFile(Dictionary<string, bool> serielnumbers)
        {
            using (StreamWriter streamWriter = new StreamWriter(new FileStream(_serielNumberFileName, FileMode.Create)))
            {
                foreach (KeyValuePair<string, bool> kvp in serielnumbers)
                {
                    string save = kvp.Key + "|" + kvp.Value;
                    streamWriter.WriteLine(save);
                }
            }
        }

        public void SaveSubmissionsToFile(List<Submission> submissions)
        {

            using (Stream stream = File.Open(_submissionsFileName, FileMode.Create))
            {
                var bformatter = new IndiePortable.Formatter.BinaryFormatter();

                bformatter.Serialize(stream, submissions);
            }
            //using (StreamWriter streamWriter = new StreamWriter(new FileStream(_submissionsFileName, FileMode.Append)))
            //{
            //    foreach (Submission sub in submissions)
            //    {
            //        string save = sub.FirstName + "|" + sub.LastName + "|" + sub.Email + "|" + sub.Phonenumber + "|" + sub.Birthdate + "|" + sub.Password + "|" + sub.LotterySerial + "|" + sub.SubmissionDate;
            //        streamWriter.WriteLine(save);
            //    }
            //}
        }

        public void SaveLoginsToFile(string email, string password)
        {
            using (StreamWriter streamWriter = new StreamWriter(new FileStream(_loginsFileName, FileMode.Append)))
            {

                string save = email + "|" + password;
                streamWriter.WriteLine(save);

            }
        }
    }
}
