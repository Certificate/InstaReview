using System;
using Android.App;
using Android.Content;
using Common.Logging;

namespace InstantReview.Droid
{
    public class SettingsStorage : ISettingsStorage
    {
        private const string SettingsStoreName = "InstaReviewSettings";

        private static readonly ILog Log = LogManager.GetLogger<SettingsStorage>();

        public void SetValue(string key, string value)
        {
            var prefs = Application.Context.GetSharedPreferences(SettingsStoreName, FileCreationMode.Private);
            var editor = prefs.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public void SetValue(string key, bool value)
        {
            var prefs = Application.Context.GetSharedPreferences(SettingsStoreName, FileCreationMode.Private);
            var editor = prefs.Edit();
            editor.PutString(key, value.ToString());
            editor.Commit();
        }

        public string GetValue(string key, string defaultValue)
        {
            var prefs = Application.Context.GetSharedPreferences(SettingsStoreName, FileCreationMode.Private);
            return prefs.GetString(key, defaultValue);
        }

        public bool GetValue(string key, bool defaultValue)
        {
            var prefs = Application.Context.GetSharedPreferences(SettingsStoreName, FileCreationMode.Private);
            return Convert.ToBoolean(prefs.GetString(key, defaultValue.ToString()));
        }

        public void RemoveValue(string key)
        {
            var prefs = Application.Context.GetSharedPreferences(SettingsStoreName, FileCreationMode.Private);
            var editor = prefs.Edit();
            editor.Remove(key);
            editor.Commit();
        }
    }
}