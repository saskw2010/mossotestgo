using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mossotest.ViewModels
{
    public class ChangePasswordPopUpViewModel : BaseViewModel
    {
        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {   
            OnLoadingStarted(EventArgs.Empty);

            bool success = await App.LoginService.ChangePassword(oldPassword, newPassword);
            Settings.Password = newPassword;

            OnLoadingEnded(EventArgs.Empty);

            return success;
        }
    }
}
