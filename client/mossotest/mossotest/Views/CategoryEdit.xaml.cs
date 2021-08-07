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
    public partial class CategoryEdit : ContentPage
    {
        // set ViewModel for BindingContext
        CategoryEditViewModel _viewModel
        {
            get { return BindingContext as CategoryEditViewModel; }
            set { BindingContext = value; }
        }

        public CategoryEdit() 
        {
            InitializeComponent();
        } 

        public CategoryEdit(Category category) : this()
        {
            // setting BindingContext
            _viewModel = new CategoryEditViewModel(category);

            _viewModel.LoadingStartedEvent += (sender, e) => { loading_view.IsVisible = true; };
            _viewModel.LoadingEndedEvent += (sender, e) => { loading_view.IsVisible = false; };
        }

        async Task SaveCategory_Handler(object sender, EventArgs e)
        {
            await _viewModel.SaveOrEditCategory();
            await Navigation.PopAsync();
        }

        void DataChanged_Handler(object sender, TextChangedEventArgs e)
        {
            button_save.IsEnabled = true
                && !string.IsNullOrWhiteSpace(entry_name.Text);

            if(_viewModel.Editing)
                button_save.IsEnabled &= e.OldTextValue != null;
        }
    }
}

