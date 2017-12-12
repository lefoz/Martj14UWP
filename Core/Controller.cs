using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataPresistence;
using Core.Model;
using Core.Repository;
using Core.Security;

namespace Core
{
    public class Controller : IController
    {
        private ILoginInformation loginInformation;
        private ISerielNumberRepository serielNumberRepository;
        private IFileDownStream putDownStream;
        private IFileUpStream getFileUpStream;
        private List<Submission> submissionList;
        private Dictionary<string, string> logins;
        private Dictionary<string, bool> lotteryDictionary;

        public Controller()
        {
            loginInformation = new LoginInformation();
            serielNumberRepository = new SerielNumberRepository();
            putDownStream = new FileDownStream();
            getFileUpStream = new FileUpStream();
            logins = new Dictionary<string, string>();
            lotteryDictionary = new Dictionary<string, bool>();
            submissionList = new List<Submission>();
            Task.Run(() => submissionList = getFileUpStream.LoadSubmissionsFromFile());
            Task.Run(() => loginInformation.LoadLoginsToDictinary(logins));
            Task.Run(() => serielNumberRepository.SerielNumbersFromFile(lotteryDictionary));

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
                        putDownStream.SaveSubmissionsToFile(submissionList);
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
    }
}
