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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DMPrepHelper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CultureConfigPage : Page
    {
        private ViewModels.CultureConfigViewModel model;
        private StorageHelper storage;

        public CultureConfigPage()
        {
            this.InitializeComponent();
            storage = ((App)Application.Current).Storage;
            ViewModel = new ViewModels.CultureConfigViewModel(storage);
        }

        public ViewModels.CultureConfigViewModel ViewModel { get => model; set => model = value; }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as string;
            ViewModel.SelectItemCommand.Execute(clicked);
        }
    }
}
