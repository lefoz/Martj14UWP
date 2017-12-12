using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Core.Model;

namespace Core.DataPresistence
{
    class FileUpStream : IFileUpStream
    {
        private const string _serielNumberFileName ="SerielNumbers.dat";
        private const string _submissionsFileName = "Submissions.bin";
        private const string _loginsFileName = "Logins.dat";

        public List<string> LoadSerielNumbersFromFile()
        {
            string line = "";
            List<string> serielNum = new List<string>();
            using (StreamReader streamReader = new StreamReader(new FileStream(_serielNumberFileName, FileMode.Open)))
            {
                
                while ((line = streamReader.ReadLine()) != null)
                {
                    serielNum.Add(line);
                }
            }
            return serielNum;
        }

        public List<Submission> LoadSubmissionsFromFile()
        {
            List<Submission> submissionList = new List<Submission>();
            using (Stream stream = File.Open(_submissionsFileName,FileMode.Open))
            {
                var bformatter = new IndiePortable.Formatter.BinaryFormatter();

                submissionList = (List<Submission>)bformatter.Deserialize(stream);
            }
            //using (StreamReader streamReader = new StreamReader(_submissionsFileName))
            //{
            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        submissionList.Add(line);
            //    }
            //}
            return submissionList;
        }

        public List<string> LoadLoginsFromFile()
        {
            string line = "";
            List<string> loginsList = new List<string>();
            
            using (StreamReader streamReader = new StreamReader(new FileStream(_loginsFileName, FileMode.Open)))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    loginsList.Add(line);
                }
            }
            return loginsList;
        }
    }
}
