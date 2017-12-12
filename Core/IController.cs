using Core.Model;

namespace Core
{
    public interface IController
    {
        bool AddSerialToExistingSubmisson(string email, string serial);
        int AddSubmission(Submission submission);
    }
}