using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Windows.Storage;
using Core.Model;


namespace Core.DataPresistence
{
    class FileDownStream : IFileDownStream
    {
        private readonly string _serielNumberFileName = "SerielNumbers.dat";
        private readonly string _submissionsFileName = "Submissions.xml";
        private readonly string _loginsFileName = "Logins.dat";
        private readonly StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public async Task SaveSerielNumbersToFileAsync(IDictionary<string, bool> serielnumbers)
        {
            var serializer = new DataContractSerializer(typeof(Dictionary<string, string>));

            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(_serielNumberFileName, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, serielnumbers);
            }
        }
        
            //OLD Code----------------------------------------------
            //using (StreamWriter streamWriter = new StreamWriter(newFile)) ;
            //{
            //    foreach (KeyValuePair<string, bool> kvp in serielnumbers)
            //    {
            //        string save = kvp.Key + "|" + kvp.Value;
            //        streamWriter.WriteLine(save);
            //    }
            //}
            //OLD Code----------------------------------------------
        

        public async Task SaveSubmissionsToFileAsync(IList<Submission> submissions)
        {
            var serializer = new DataContractSerializer(typeof(IList<Submission>));

            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                      _submissionsFileName,
                      CreationCollisionOption.ReplaceExisting))
            { 
                    serializer.WriteObject(stream, submissions);
            }
            //OLD Code----------------------------------------------
            //using (Stream stream = File.Open(_submissionsFileName, FileMode.Create))
            //{
            //    var bformatter = new IndiePortable.Formatter.BinaryFormatter();

            //    bformatter.Serialize(stream, submissions);
            //}
            //using (StreamWriter streamWriter = new StreamWriter(new FileStream(_submissionsFileName, FileMode.Append)))
            //{
            //    foreach (Submission sub in submissions)
            //    {
            //        string save = sub.FirstName + "|" + sub.LastName + "|" + sub.Email + "|" + sub.Phonenumber + "|" + sub.Birthdate + "|" + sub.Password + "|" + sub.LotterySerial + "|" + sub.SubmissionDate;
            //        streamWriter.WriteLine(save);
            //    }
            //}
            //OLD Code----------------------------------------------
        }

        public async Task SaveLoginsToFileAsync(IDictionary<string,string> logins )
        {
            var serializer = new DataContractSerializer(typeof(Dictionary<string, string>));

            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(_loginsFileName, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, logins);
            }

            //}
            //Old Code---------------------------------------------------------
            //using (StreamWriter streamWriter = new StreamWriter(new FileStream(_loginsFileName, FileMode.Append)))
            //{
            //    string save = email + "|" + password;
            //    streamWriter.WriteLine(save);
            //}
            //Old Code---------------------------------------------------------
        }
    }
}
