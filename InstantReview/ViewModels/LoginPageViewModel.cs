using Common.Logging;
using InstantReview.Droid;

namespace InstantReview.ViewModels
{
    public class LoginPageViewModel : ILoginPageViewModel
    {
        private readonly ISettingsStorage storage;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();
        
        public void SaveUsagePrivileges()
        {
            Log.Debug("Save usage privilege.");
            storage.SetValue("user", "toBeHashed");
        }
        
        public bool CheckUsagePrivileges()
        {
            var readValue = storage.GetValue("user", string.Empty);
            return true;
        }
    }
}