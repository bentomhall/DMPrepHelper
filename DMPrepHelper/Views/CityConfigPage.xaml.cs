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
            var clicked = e.ClickedItem as string;
            ViewModel.SelectItemCommand.Execute(clicked);
        }
    }
}
