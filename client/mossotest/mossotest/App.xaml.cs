using mossotest.Extensions;
using mossotest.Rest;
using mossotest.Rest.Security;
using mossotest.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace mossotest
{
    public partial class App : Application
    {
        #region Constants

        readonly static string POPUP_TOKEN_EXPIRED = "Looks like your token has expired, please log in again.";

        #endregion
        
        #region Services
        
        public static UserRestService UserService = new UserRestService();
        public static CategoryRestService CategoryService = new CategoryRestService();
        public static ProductRestService ProductService = new ProductRestService();
        public static LoginRestService LoginService = new LoginRestService();
        
        #endregion

        public App()
        {
            InitializeComponent();

            ShowLoginPage();
        }

        protected override void OnStart()
        {
            // when the app starts, it subscribe to the client handler that check the presence of token
            MessagingCenter.Subscribe<TokenExpiredHandler, bool>(this, TokenExpiredHandler.TOKEN_EXPIRED_MESSAGE, async (arg1, arg2) =>
            {
                CustomAlertPopUp popup = new CustomAlertPopUp(POPUP_TOKEN_EXPIRED);
                popup.ButtonClickedEvent += (sender, e) => ShowLoginPage();
                popup.DismissTappedEvent += (sender, e) => ShowLoginPage();

                await PopupNavigation.Instance.PushAsync(popup);
            });
        }

        protected override async void OnResume()
        {
            if (!await LoginService.VerifyToken(Settings.AuthenticationToken))
                ShowLoginPage();
            else
                MainPage = new MasterPage();
        }

        void ShowLoginPage()
        {
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
