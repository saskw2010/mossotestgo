using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using mossotest.Extensions;
using mossotest.Models;
using mossotest.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mossotest.ViewModels.ResourcesViewModel
{
    public class ProductListViewModel : BaseViewModel
    {
        #region Constants

        readonly static string POPUP_DELETE_MESSAGE = "Are you sure you want to delete this Product?";

        #endregion

        #region Attributes and Properties

        // this collection main purpose is to store data from API request
        ObservableCollection<Product> _supportList;

        // this one instead is the ItemSource of the iew. 
        // this allows to modify without any exceptions, the elements of the ListView.
        ObservableCollection<Product> _productList;

        public ObservableCollection<Product> ProductList
        {
            get { return _productList; }
            set { SetValue(ref _productList, value); }
        }

        #endregion

        #region Commands

        public ICommand LoadDataCommand => new Command(async obj => await RefreshProductList());
        public ICommand DeleteProductCommand => new Command<Product>(async obj => await DeleteProductFromList(obj));
        public ICommand AddOrEditProductCommand => new Command<Product>(async obj => await AddOrEditProduct(obj));
        public ICommand SearchCommand => new Command<string>(obj => FilterProductList(obj));

        #endregion
        
        async Task AddOrEditProduct(Product toEdit)
        {   
            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ProductEdit(toEdit));
        }

        async Task RefreshProductList()
        {
            OnLoadingStarted(EventArgs.Empty);

            _supportList = await App.ProductService.GETList();
            ProductList = _supportList;

            OnLoadingEnded(EventArgs.Empty);
        }

        public void RestoreProductList()
        {
            ProductList = _supportList;
        }

        async Task DeleteProductFromList(Product product) 
        {
            CustomAlertPopUp popUp = new CustomAlertPopUp(POPUP_DELETE_MESSAGE);
            popUp.ButtonClickedEvent += async (sender, e) =>
            {
                await App.ProductService.DELETE(product.Id);
                await RefreshProductList();
            };

            await PopupNavigation.Instance.PushAsync(popUp);
        }

        void FilterProductList(string query) 
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                // the filtering of elements is based on the elements Id. 
                // in case you wish to change, just replace el.Id with el.OtherField
                var tempRecords = _supportList.Where(el => el.Id.ToLower().Contains(query.ToLower()));
                ProductList = new ObservableCollection<Product>(tempRecords);
            }
            else
                ProductList = new ObservableCollection<Product>();
        }
    }
}