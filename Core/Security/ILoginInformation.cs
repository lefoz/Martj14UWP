using System.Collections.Generic;

namespace Core.Security
{
    interface ILoginInformation
    {
        void AddLogin(string email, string password, Dictionary<string, string> logins);
        bool CheckLogin(string email, string password, Dictionary<string, string> logins);
        void LoadLoginsToDictinary(Dictionary<string, string> Logins);
    }
}