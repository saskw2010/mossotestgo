using System;
using System.Collections.Generic;
using mossotest.Support;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mossotest.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPopUp : PopupPage
    {
        #region Events

        public event EventHandler ItemSelectedEvent;

        #endregion

        public PickerPopUp()
        {
            InitializeComponent();

            // setting self BingingContext
            BindingContext = this;
        }

        public PickerPopUp(IEnumerable<BaseBindableObject> items) : this()
        {
            listView.ItemsSource = items;
        }

        void Dismiss_PopUp_Handler(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        void ItemSelected_Handler(object sender, ItemTappedEventArgs e)
        {
            OnItemSelected(e.Item as BaseBindableObject);
            (sender as ListView).SelectedItem = null;
            PopupNavigation.Instance.PopAsync();
        }

        protected virtual void OnItemSelected(BaseBindableObject item)
        {
            ItemSelectedEvent?.Invoke(item, EventArgs.Empty);
        }
    }
}
