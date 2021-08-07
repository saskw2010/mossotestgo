using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using mossotest.Extensions;
using mossotest.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mossotest.ViewModels.ResourcesViewModel
{
    public class CategoryEditViewModel : BaseViewModel
    {   
        
        
        
        
        #region Attributes and Properties

        public bool Editing = true;

        Category _category;
        public Category Category
        {
            get { return _category; }
            set { SetValue(ref _category, value); }
        }
        
        
        #endregion

        
        
        

        public CategoryEditViewModel(Category categoryToEdit)
        {
            if (categoryToEdit == null)
            {
                categoryToEdit = new Category();
                Editing = false;
            }

            Category = categoryToEdit;
            
        }
        
        public async Task SaveOrEditCategory()
        {
            OnLoadingStarted(EventArgs.Empty);
            
            
            if (Editing)
                await App.CategoryService.PUT(_category);
            else
                await App.CategoryService.POST(_category);

            OnLoadingEnded(EventArgs.Empty);
        }
        
        
    }
}