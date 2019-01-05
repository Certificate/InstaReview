using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using InstantReview.ViewModels;

namespace InstantReview.Login
{
    public interface IConnectionHandler
    {
        void SaveUsagePrivileges(string token);

        bool CheckUsagePrivileges();

        void DeletePrivileges();

        Task<HttpResponseMessage> Login(string email, string password);

        bool CheckTokenValidity(string token);

        Task<HttpResponseMessage> Register(string email, string password, string name, string birthday, string gender);

        Task<EditableReview> DownloadReview(int id);

        Task<Stream> DownloadImage(string filename);

        Task<bool> UploadReview();

        Task<List<Review>> DownloadReviewList();

        Task<bool> UploadEditedReview(EditPageViewModel.EditedReview edited);

        Task<Stream> DownloadThumbnail(int id);
    }
}