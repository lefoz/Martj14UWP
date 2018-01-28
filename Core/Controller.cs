using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.SerialCommunication;
using Windows.Media.Playback;
using Core.DataPresistence;
using Core.Model;
using Core.Repository;
using Core.Security;
using WinUX;
using WinUX.Collections.ObjectModel;

namespace Core
{
    public class Controller : IController
    {
        private static Controller instance;
        private ILoginInformation loginInformation;
        private ISerielNumberRepository serielNumberRepository;
        private ISerielNumberGenerator serielNumberGenerator;
        private IFileDownStream putDownStream;
        private IFileUpStream getFileUpStream;
        private IList<Submission> submissionList;
        private IDictionary<string, string> logins;
        private readonly int serialsAmount = 100;
        private IDictionary<string, bool> lotteryDictionary;
        private readonly Windows.Storage.StorageFolder _storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
        }

        private Controller()
        {
            loginInformation = new LoginInformation();
            serielNumberRepository = new SerielNumberRepository();
            putDownStream = new FileDownStream();
            getFileUpStream = new FileUpStream();
            serielNumberGenerator = new SerielNumberGenerator();
            logins = new Dictionary<string, string>();
            lotteryDictionary = new Dictionary<string, bool>();
            submissionList = new List<Submission>();
            if (getFileUpStream.isFilePresent(0).Result)
                submissionList = getFileUpStream.LoadSubmissionsFromFileAsync().Result;
            if (getFileUpStream.isFilePresent(1).Result)
                lotteryDictionary = serielNumberRepository.SerielNumbersFromFile().Result;
            if (lotteryDictionary.IsNullOrEmpty())
                Task.Run(() =>lotteryDictionary = serielNumberGenerator.GenerateSerielNumberDictionary(serialsAmount));
            if (getFileUpStream.isFilePresent(2).Result)
                logins = loginInformation.LoadLoginsToDictinary().Result;
            if (logins.IsNullOrEmpty())
                logins.Add("admin", "admin");
        }

        public int AddSubmission(Submission submission)
        {
            int serielchecker = 0;
            if (CheckForPriorRegistration(submission.LastName, submission.Email, submission.Birthdate))
            {
               serielchecker = serielNumberRepository.LookUpSerielNumber(submission.LotterySerial[0], lotteryDictionary);
                if (serielchecker==2)
                {
                        //Seriel is unclamied, now being claimed by this submission
                        submissionList.Add(new Submission(submission.FirstName, submission.LastName, submission.Email, submission.Phonenumber, submission.Birthdate, submission.Password, submission.LotterySerial[0]));
                        loginInformation.AddLogin(submission.Email, submission.Password, logins);
                        putDownStream.SaveSubmissionsToFileAsync(submissionList);
                        putDownStream.SaveLoginsToFileAsync(logins);
                        putDownStream.SaveSerielNumbersToFileAsync(lotteryDictionary);
                        return 2;
                }

            }
            return serielchecker;
        }

        private bool CheckForPriorRegistration(string lastName, string email, DateTimeOffset birthdate)
        {
            var alreadyPresent = submissionList.Where(x => x.Email.Equals(email) && x.LastName.Equals(lastName) && x.Birthdate.Equals(birthdate));
            if (!alreadyPresent.Any()) return true;
            return false;
        }

        public bool AddSerialToExistingSubmisson(string email, string serial)
        {
            int result = serielNumberRepository.LookUpSerielNumber(serial, lotteryDictionary);
            if (result == 2)
            {
                foreach (var sub in submissionList)
                {
                    if (sub.Email.Equals(email)) sub.LotterySerial.Add(serial);
                    return true;
                }
            }
            return false;
        }

        public bool LoginChecker(string email, string password)
        {
            return loginInformation.CheckLogin(email, password, logins);
        }

        public IList<SerialModel> GetSerialList(int code)
        {
            if (code == 42)
                return serielNumberRepository.GetSerials(lotteryDictionary);
            else
            {
                List<SerialModel> fail = new List<SerialModel>(); 
                fail.Add(new SerialModel("fail", false));
                return fail;
            }
        }
        public IList<Submission> GetSubmissionList(int code)
        {
            if (code == 42)
                return submissionList;
            else
            {
                List<Submission> fail = new List<Submission>();
                fail.Add(new Submission("fail", "fail", "fail", "fail",DateTimeOffset.Now, "fail", "fail"));
                return fail;
            }
        }
        
    }
}
