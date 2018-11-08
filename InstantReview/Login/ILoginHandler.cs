namespace InstantReview.Login
{
    public interface ILoginHandler
    {
        void SaveUsagePrivileges();

        bool CheckUsagePrivileges();

        void DeletePrivileges();
    }
}