using System;
using Common.Logging;
using InstantReview.Droid;
using InstantReview.ViewModels;

namespace InstantReview.Login
{
    public class LoginHandler : ILoginHandler
    {
        private readonly ISettingsStorage storage;
        private static readonly ILog Log = LogManager.GetLogger<LoginPageViewModel>();

        public LoginHandler(ISettingsStorage storage)
        {
            this.storage = storage;
        }

        public void SaveUsagePrivileges()
        {
            Log.Debug("Save usage privilege.");
            storage.SetValue("user", "toBeHashed");
        }

        public bool CheckUsagePrivileges()
        {
            var hasPrivilege = false;
            Log.Debug("Read usage privilege");
            var readValue = storage.GetValue("user", string.Empty);
            if (readValue != String.Empty)
            {
                hasPrivilege = true;
            }
            Log.Debug(": " + readValue);
            return hasPrivilege;
        }

        public void DeletePrivileges()
        {
            storage.RemoveValue("user");
        }
    }
}