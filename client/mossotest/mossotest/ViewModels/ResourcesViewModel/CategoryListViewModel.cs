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
    public class CategoryListViewModel : BaseViewModel
    {
        #region Constants

        readonly static string POPUP_DELETE_MESSAGE = "Are you sure you want to delete this Category?";

        #endregion

        #region Attributes and Properties

        // this collection main purpose is to store data from API request
        ObservableCollection<Category> _supportList;

        // this one instead is the ItemSource of the iew. 
        // this allows to modify without any exceptions, the elements of the ListView.
        ObservableCollection<Category> _categoryList;

        public ObservableCollection<Category> CategoryList
        {
            get { return _categoryList; }
            set { SetValue(ref _categoryList, value); }
        }

        #endregion

        #region Commands

        public ICommand LoadDataCommand => new Command(async obj => await RefreshCategoryList());
        public ICommand DeleteCategoryCommand => new Command<Category>(async obj => await DeleteCategoryFromList(obj));
        public ICommand AddOrEditCategoryCommand => new Command<Category>(async obj => await AddOrEditCategory(obj));
        public ICommand SearchCommand => new Command<string>(obj => FilterCategoryList(obj));

        #endregion
        
        async Task AddOrEditCategory(Category toEdit)
        {   
            await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new CategoryEdit(toEdit));
        }

        async Task RefreshCategoryList()
        {
            OnLoadingStarted(EventArgs.Empty);

            _supportList = await App.CategoryService.GETList();
            CategoryList = _supportList;

            OnLoadingEnded(EventArgs.Empty);
        }

        public void RestoreCategoryList()
        {
            CategoryList = _supportList;
        }

        async Task DeleteCategoryFromList(Category category) 
        {
            CustomAlertPopUp popUp = new CustomAlertPopUp(POPUP_DELETE_MESSAGE);
            popUp.ButtonClickedEvent += async (sender, e) =>
            {
                await App.CategoryService.DELETE(category.Id);
                await RefreshCategoryList();
            };

            await PopupNavigation.Instance.PushAsync(popUp);
        }

        void FilterCategoryList(string query) 
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                // the filtering of elements is based on the elements Id. 
                // in case you wish to change, just replace el.Id with el.OtherField
                var tempRecords = _supportList.Where(el => el.Id.ToLower().Contains(query.ToLower()));
                CategoryList = new ObservableCollection<Category>(tempRecords);
            }
            else
                CategoryList = new ObservableCollection<Category>();
        }
    }
}