using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Security
{
    interface ILoginInformation
    {
        void AddLogin(string email, string password, IDictionary<string, string> logins);
        bool CheckLogin(string email, string password, IDictionary<string, string> logins);
        Task<IDictionary<string, string>> LoadLoginsToDictinary();
    }
}