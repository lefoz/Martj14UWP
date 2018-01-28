using System.Collections.Generic;
using Core.Model;

namespace Core
{
    public interface IController
    {
        
        bool AddSerialToExistingSubmisson(string email, string serial);
        int AddSubmission(Submission submission);
        bool LoginChecker(string email, string password);
        IList<SerialModel> GetSerialList(int code);
        IList<Submission> GetSubmissionList(int code);
    }
}