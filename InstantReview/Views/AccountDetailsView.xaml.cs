using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview.Views
{
    public partial class AccountDetailsView : ContentPage
    {
        public AccountDetailsView(AccountDetailsViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }

        private void PasswordTextChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (password.Text != null && checkPassword.Text != null &&!password.Text.Equals(checkPassword.Text))
                {
                    password.TextColor = Color.Red;
                    checkPassword.TextColor = Color.Red;
                    passwordErrorText.IsVisible = true;
                }
                else
                {
                    password.TextColor = Color.Default;
                    checkPassword.TextColor = Color.Default;
                    passwordErrorText.IsVisible = false;
                }
                CheckButtonState();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void CheckButtonState()
        {
            var isValidEmail = false;
            
            // Regex for valid email address
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!string.IsNullOrEmpty(email.Text))
            {
                isValidEmail = regex.IsMatch(email.Text);
            }

            if (!string.IsNullOrEmpty(email.Text) && 
                password.Text != null && 
                checkPassword.Text != null && 
                password.Text.Equals(checkPassword.Text) &&
                isValidEmail)
            {
                registerButton.IsEnabled = true;
                registerButton.BorderColor = Color.Salmon;
            }
            else
            {
                registerButton.IsEnabled = false;
                registerButton.BorderColor = Color.Default;
            }
        }

        private void EmailTextChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            CheckButtonState();
            
        }
    }
}
