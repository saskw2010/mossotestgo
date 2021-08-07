using System;
using System.Threading.Tasks;
using mossotest.Extensions;
using mossotest.Models;
using Rg.Plugins.Popup.Services;

namespace mossotest.ViewModels
{
    public class RegisterNewUserViewModel : BaseViewModel
    {
        #region Attributes and Properties

        User _user;
        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        string _role;
        public string Role
        {
            get { return _role; }
            set { SetValue(ref _role, value); User.Roles = new string[] { value }; }
        }

        #endregion

        public RegisterNewUserViewModel()
        {
            User = new User();
        }

        public async Task AddUser()
        {
            OnLoadingStarted(EventArgs.Empty);

            await App.UserService.POST(_user);

            OnLoadingEnded(EventArgs.Empty);
        }
    }
}