using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mossotest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            UserButton.IsVisible = Settings.CurrentUserRole.Equals("ADMIN");
        }

        void ChangePage(object sender, EventArgs e)
        {
            var button = (sender as Button).Text;
            Page page = null;

            switch (button)
            {   
                case "Category":
                    page = new CategoryList();
                    break;
                case "Product":
                    page = new ProductList();
                    break;
                case "User":
                    page = new UsersListStatic();
                    break;
            }

            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);
        }
    }
}