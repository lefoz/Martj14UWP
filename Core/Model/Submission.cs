using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
   [Serializable]
    public class Submission 
    {
       
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phonenumber { get; }
        public DateTimeOffset Birthdate { get; }
        public string Password { get; }
        public IList<string> LotterySerial;
        public DateTime SubmissionDate { get; }

        /// <summary>
        /// Model for Submission
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phonenumber"></param>
        /// <param name="birthdate"></param>
        /// <param name="password"></param>
        /// <param name="lotterySerial"></param>
        /// <param name="submissionDate"></param>
        public Submission(string firstName, string lastName, string email, string phonenumber, DateTimeOffset birthdate, string password, string lotterySerial)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phonenumber = phonenumber;
            Birthdate = birthdate;
            Password = password;
            LotterySerial.Add(lotterySerial);
            SubmissionDate = DateTime.Now;
        }
    }
}
