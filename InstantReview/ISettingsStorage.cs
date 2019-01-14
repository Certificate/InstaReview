namespace InstantReview.Droid
{
    public interface ISettingsStorage
    {
        void SetValue(string key, string value);

        void SetValue(string key, bool value);

        string GetValue(string key, string defaultValue);

        bool GetValue(string key, bool defaultValue);

        void RemoveValue(string key);
    }
}