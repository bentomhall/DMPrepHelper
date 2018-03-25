using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DMPrepHelper.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DMPrepHelper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CityConfigPage : Page
    {
        private StorageHelper storage;
        public CityConfigPage()
        {
            this.InitializeComponent();
            storage = ((App)Application.Current).Storage;
            ViewModel = new CityConfigViewModel(storage);
            ViewModel.SetItems();
        }

        public CityConfigViewModel ViewModel { get; set; }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ViewModel.SelectedCity != null)
            {
                ViewModel.DidEditItem();
            }
            ViewModel.SelectItemCommand.Execute((e.ClickedItem as CityViewModel).Data.Name);
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var buttonName = (sender as Button).Name;
            if (buttonName == "AddListItemButton")
            {
                ViewModel.AddListItemCommand.Execute(null);
            }
            else if (buttonName == "DeleteListItemButton")
            {
                ViewModel.RemoveListItemCommand.Execute("");
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            ViewModel.SelectItemCommand.Execute((e.AddedItems[0] as CityViewModel).Data.Name);
        }
    }
}
