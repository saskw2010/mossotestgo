using System;
using System.Threading.Tasks;
using mossotest.Models;

namespace mossotest.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Attributes and Properties

        User _user;

        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        #endregion

        public ProfilePageViewModel(User userLogged)
        {
            User = userLogged;
        }

        public async Task UpdateUserInfo()
        {
            OnLoadingStarted(EventArgs.Empty);

            await App.UserService.PUT(User);
            User = await App.UserService.GETId(User.Id);

            OnLoadingEnded(EventArgs.Empty);
        }
    }
}
