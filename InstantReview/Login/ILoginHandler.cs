using System.Net.Http;
using System.Threading.Tasks;

namespace InstantReview.Login
{
    public interface ILoginHandler
    {
        void SaveUsagePrivileges(string token);

        bool CheckUsagePrivileges();

        void DeletePrivileges();

        Task<HttpResponseMessage> Login(string email, string password);

        bool CheckTokenValidity(string token);
    }
}