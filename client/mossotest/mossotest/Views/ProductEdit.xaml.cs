using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mossotest.Models;
using mossotest.ViewModels.ResourcesViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mossotest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductEdit : ContentPage
    {
        // set ViewModel for BindingContext
        ProductEditViewModel _viewModel
        {
            get { return BindingContext as ProductEditViewModel; }
            set { BindingContext = value; }
        }

        public ProductEdit() 
        {
            InitializeComponent();
        } 

        public ProductEdit(Product product) : this()
        {
            // setting BindingContext
            _viewModel = new ProductEditViewModel(product);

            _viewModel.LoadingStartedEvent += (sender, e) => { loading_view.IsVisible = true; };
            _viewModel.LoadingEndedEvent += (sender, e) => { loading_view.IsVisible = false; };
        }

        async Task SaveProduct_Handler(object sender, EventArgs e)
        {
            await _viewModel.SaveOrEditProduct();
            await Navigation.PopAsync();
        }

        void DataChanged_Handler(object sender, TextChangedEventArgs e)
        {
            button_save.IsEnabled = true
                && !string.IsNullOrWhiteSpace(entry_catid.Text)
                && !string.IsNullOrWhiteSpace(entry_productname.Text);

            if(_viewModel.Editing)
                button_save.IsEnabled &= e.OldTextValue != null;
        }
    }
}

