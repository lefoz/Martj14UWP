using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataPresistence;

namespace Core.Security
{
    class LoginInformation : ILoginInformation
    {
        private IFileDownStream saveToFile = new FileDownStream();
        private IFileUpStream loadFromFile = new FileUpStream();

        public void AddLogin(string email, string password, IDictionary<string, string> logins)
        {
            logins.Add(email, password);
            saveToFile.SaveLoginsToFileAsync(logins);
        }

        public bool CheckLogin(string email, string password, IDictionary<string, string> logins)
        {
            if (logins.ContainsKey(email))
            return (logins[email].Equals(password));
            else
            return false;
        }

        public async Task<IDictionary<string, string>> LoadLoginsToDictinary()
        {
            return await loadFromFile.LoadLoginsFromFileAsync();
            //IDictionary<string, string> Logins = new Dictionary<string, string>();
            

            //if (tempList != null && tempList.Count > 0)
            //{
            //    foreach (var item in tempList)
            //    {

            //        int devidIndex = item.IndexOf("|");
            //        string email = item.Substring(0, devidIndex);
            //        string password = item.Substring(devidIndex + 1);
            //        Logins.Add(email, password);
            //    }
            //}
            //return Logins;
        }
    }
}
