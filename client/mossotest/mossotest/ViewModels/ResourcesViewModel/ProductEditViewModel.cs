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
    public class ProductEditViewModel : BaseViewModel
    {   
        
        
        
        
        #region Attributes and Properties

        public bool Editing = true;

        Product _product;
        public Product Product
        {
            get { return _product; }
            set { SetValue(ref _product, value); }
        }
        
        // this is the collection is used as SelectedItem for the Category picker 
        Category _catpro;
        public Category Catpro
        {
            get { return _catpro; }
            set { SetValue(ref _catpro, value); Product.Catpro = value.Id; }
        }

        // this collection is used to store all Category available 
        ObservableCollection<Category> _categoryList;

        // this is the collection is used as ItemSource for the Category picker 
        public ObservableCollection<Category> CategoryList
        {
            get { return _categoryList; }
            set { SetValue(ref _categoryList, value); }
        }
        
        #endregion

        
        
        

        public ProductEditViewModel(Product productToEdit)
        {
            if (productToEdit == null)
            {
                productToEdit = new Product();
                Editing = false;
            }

            Product = productToEdit;
            
            // async load lists
            Task.Factory.StartNew(GetData);
            
        }
        
        async Task GetData()
        {
            OnLoadingStarted(EventArgs.Empty);
            
            CategoryList = await App.CategoryService.GETList();

            if (Editing)
            {   
                // get the Category from the CategoryList (the Product object only has its id)
                Catpro = _categoryList.Single((arg) => arg.Id.Equals(_product.Catpro));
            }
            

            OnLoadingEnded(EventArgs.Empty);
        }
        
        
        public async Task SaveOrEditProduct()
        {
            OnLoadingStarted(EventArgs.Empty);
            
            
            if (Editing)
                await App.ProductService.PUT(_product);
            else
                await App.ProductService.POST(_product);

            OnLoadingEnded(EventArgs.Empty);
        }
        
        
    }
}