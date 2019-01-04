using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using InstantReview.ViewModels;
using Xamarin.Forms;



namespace InstantReview.ViewModels
{
    public class AccountDetailsViewModel : BaseViewModel
    {
        public ICommand ChangePasswordCommand => new Command(StartChangePasswordProcess);
        public ICommand ReturnToMainCommand => new Command(NavigateToMainpage);




        private void StartChangePasswordProcess(object obj)
        {
            var success = false;
            try
            {
                var response = await _connectionHandler.
                var responseBody = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Response was not OK. Aborting. Reason: " + responseBody);
                }

                var token = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());

                var isActive = _connectionHandler.CheckTokenValidity(token.token);

                if (isActive)
                {
                    _connectionHandler.SaveUsagePrivileges(token.token);
                    dialogService.ShowLoginToast();
                    success = true;
                }
            }
            catch (Exception e)
            {
                Log.Error("Error while logging in!", e);
            }
            finally
            {
                Password = string.Empty;
                OnPropertyChanged(nameof(Password));
            }

            if (success)
            {
                Log.Debug("Logged in successfully!");
                Username = string.Empty;
                OnPropertyChanged(nameof(Username));
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
        }



        public async void NavigateToMainpage()
        {
            Log.Debug("Navigating to Main menu!");
            await navigation.PushAsyncSingle(CreateMainpage());
        }

        private Page CreateMainpage()
        {
            return pageFactory.CreatePage<ThankYouPage, ThankYouPageViewModel>(thankYouPageViewModel);
        }


        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}