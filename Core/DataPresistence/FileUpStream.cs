using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
        private const string _submissionsFileName = "Submissions.xml";
        private const string _loginsFileName = "Logins.dat";
        private readonly Windows.Storage.StorageFolder _storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;                                                                                              

        public async Task<bool> isFilePresent(int stage)
        {
            
            switch (stage)
            {
                case 0:
                    var subFile = await _storageFolder.TryGetItemAsync(_submissionsFileName);
                    return subFile != null;
                case 1:
                    var serialFile = await _storageFolder.TryGetItemAsync(_serielNumberFileName);
                    return serialFile != null;
                case 2:
                    var loginFile = await _storageFolder.TryGetItemAsync(_loginsFileName);
                    return loginFile != null;
                default:
                    return false;
            }
        }

        public async Task<IDictionary<string, bool>> LoadSerielNumbersFromFile()
        {
            var SerialList = new Dictionary<string, bool>();

            var serializer = new DataContractSerializer(typeof(Dictionary<string, bool>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_serielNumberFileName))
            {
                SerialList = (Dictionary<string, bool>)serializer.ReadObject(stream);
            }
            //await Task.Delay(1000);
            return SerialList;
        }

        //Old Code-------------------------------------------
        //using (StreamReader streamReader = new StreamReader(new FileStream(_serielNumberFileName, FileMode.Open)))
        //{
        //    while ((line = streamReader.ReadLine()) != null)
        //    {
        //        serielNum.Add(line);
        //    }
        //}
        //Old Code---------------------------------------------
    

        public async Task<IList<Submission>> LoadSubmissionsFromFileAsync()
        {
            IList<Submission> submissionList = new List<Submission>();
            var serializer = new DataContractSerializer(typeof(List<Submission>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(
                      _submissionsFileName))
            {
                submissionList = (List<Submission>)serializer.ReadObject(stream);
            }
            //await Task.Delay(1000);
            return  submissionList;

            //Old Code---------------------------------------------
            //using (Stream stream = File.Open(_submissionsFileName,FileMode.Open))
            //{
            //    var bformatter = new IndiePortable.Formatter.BinaryFormatter();

            //    submissionList = (List<Submission>)bformatter.Deserialize(stream);
            //}
            //using (StreamReader streamReader = new StreamReader(_submissionsFileName))
            //{
            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        submissionList.Add(line);
            //    }
            //}
            //Old Code---------------------------------------------

        }

        public async Task<IDictionary<string, string>> LoadLoginsFromFileAsync()
        {
            var loginsList = new Dictionary<string, string>();

            var serializer = new DataContractSerializer(typeof(Dictionary<string, string>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_loginsFileName))
            {
                loginsList = (Dictionary<string, string>)serializer.ReadObject(stream);
            }
            //await Task.Delay(1000);
            return loginsList;
        }

            //Old Code---------------------------------------------
            //using (StreamReader streamReader = new StreamReader(new FileStream(_loginsFileName, FileMode.Open)))
            //{
            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        loginsList.Add(line);
            //    }
            //}
            //Old Code---------------------------------------------
            //return loginsList;
        
    }
}
